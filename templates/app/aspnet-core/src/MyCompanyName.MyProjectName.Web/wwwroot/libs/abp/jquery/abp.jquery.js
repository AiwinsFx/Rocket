var rocket = rocket || {};
(function($) {

    if (!$) {
        throw "rocket/jquery library requires the jquery library included to the page!";
    }

    // ABP CORE OVERRIDES /////////////////////////////////////////////////////

    rocket.message._showMessage = function (message, title) {
        alert((title || '') + ' ' + message);

        return $.Deferred(function ($dfd) {
            $dfd.resolve();
        });
    };

    rocket.message.confirm = function (message, titleOrCallback, callback) {
        if (titleOrCallback && !(typeof titleOrCallback == 'string')) {
            callback = titleOrCallback;
        }

        var result = confirm(message);
        callback && callback(result);

        return $.Deferred(function ($dfd) {
            $dfd.resolve(result);
        });
    };

    rocket.utils.isFunction = function (obj) {
        return $.isFunction(obj);
    };

    // JQUERY EXTENSIONS //////////////////////////////////////////////////////

    $.fn.findWithSelf = function (selector) {
        return this.filter(selector).add(this.find(selector));
    };

    // DOM ////////////////////////////////////////////////////////////////////

    rocket.dom = rocket.dom || {};

    rocket.dom.onNodeAdded = function (callback) {
        rocket.event.on('rocket.dom.nodeAdded', callback);
    };

    rocket.dom.onNodeRemoved = function (callback) {
        rocket.event.on('rocket.dom.nodeRemoved', callback);
    };

    var mutationObserverCallback = function (mutationsList) {
        for (var i = 0; i < mutationsList.length; i++) {
            var mutation = mutationsList[i];
            if (mutation.type === 'childList') {
                if (mutation.addedNodes && mutation.removedNodes.length) {
                    for (var k = 0; k < mutation.removedNodes.length; k++) {
                        rocket.event.trigger(
                            'rocket.dom.nodeRemoved',
                            {
                                $el: $(mutation.removedNodes[k])
                            }
                        );
                    }
                }

                if (mutation.addedNodes && mutation.addedNodes.length) {
                    for (var j = 0; j < mutation.addedNodes.length; j++) {
                        rocket.event.trigger(
                            'rocket.dom.nodeAdded',
                            {
                                $el: $(mutation.addedNodes[j])
                            }
                        );
                    }
                }
            }
        }
    };

    new MutationObserver(mutationObserverCallback).observe(
        $('body')[0],
        {
            subtree: true,
            childList: true
        }
    );

    // AJAX ///////////////////////////////////////////////////////////////////

    rocket.ajax = function (userOptions) {
        userOptions = userOptions || {};

        var options = $.extend(true, {}, rocket.ajax.defaultOpts, userOptions);

        options.success = undefined;
        options.error = undefined;

        return $.Deferred(function ($dfd) {
            $.ajax(options)
                .done(function (data, textStatus, jqXHR) {
                    $dfd.resolve(data);
                    userOptions.success && userOptions.success(data);
                }).fail(function (jqXHR) {
                    if (jqXHR.getResponseHeader('_RocketErrorFormat') === 'true') {
                        rocket.ajax.handleRocketErrorResponse(jqXHR, userOptions, $dfd);
                    } else {
                        rocket.ajax.handleNonRocketErrorResponse(jqXHR, userOptions, $dfd);
                    }
                });
        });
    };

    $.extend(rocket.ajax, {
        defaultOpts: {
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'X-Requested-With': 'XMLHttpRequest'
            }
        },

        defaultError: {
            message: 'An error has occurred!',
            details: 'Error detail not sent by server.'
        },

        defaultError401: {
            message: 'You are not authenticated!',
            details: 'You should be authenticated (sign in) in order to perform this operation.'
        },

        defaultError403: {
            message: 'You are not authorized!',
            details: 'You are not allowed to perform this operation.'
        },

        defaultError404: {
            message: 'Resource not found!',
            details: 'The resource requested could not found on the server.'
        },

        logError: function (error) {
            rocket.log.error(error);
        },

        showError: function (error) {
            if (error.details) {
                return rocket.message.error(error.details, error.message);
            } else {
                return rocket.message.error(error.message || rocket.ajax.defaultError.message);
            }
        },

        handleTargetUrl: function (targetUrl) {
            if (!targetUrl) {
                location.href = rocket.appPath;
            } else {
                location.href = targetUrl;
            }
        },

        handleErrorStatusCode: function (status) {
            switch (status) {
                case 401:
                    rocket.ajax.handleUnAuthorizedRequest(
                        rocket.ajax.showError(rocket.ajax.defaultError401),
                        rocket.appPath
                    );
                    break;
                case 403:
                    rocket.ajax.showError(rocket.ajax.defaultError403);
                    break;
                case 404:
                    rocket.ajax.showError(rocket.ajax.defaultError404);
                    break;
                default:
                    rocket.ajax.showError(rocket.ajax.defaultError);
                    break;
            }
        },

        handleNonRocketErrorResponse: function (jqXHR, userOptions, $dfd) {
            if (userOptions.rocketHandleError !== false) {
                rocket.ajax.handleErrorStatusCode(jqXHR.status);
            }

            $dfd.reject.apply(this, arguments);
            userOptions.error && userOptions.error.apply(this, arguments);
        },

        handleRocketErrorResponse: function (jqXHR, userOptions, $dfd) {
            var messagePromise = null;

            if (userOptions.rocketHandleError !== false) {
                messagePromise = rocket.ajax.showError(jqXHR.responseJSON.error);
            }

            rocket.ajax.logError(jqXHR.responseJSON.error);

            $dfd && $dfd.reject(jqXHR.responseJSON.error, jqXHR);
            userOptions.error && userOptions.error(jqXHR.responseJSON.error, jqXHR);

            if (jqXHR.status === 401 && userOptions.rocketHandleError !== false) {
                rocket.ajax.handleUnAuthorizedRequest(messagePromise);
            }
        },

        handleUnAuthorizedRequest: function (messagePromise, targetUrl) {
            if (messagePromise) {
                messagePromise.done(function () {
                    rocket.ajax.handleTargetUrl(targetUrl);
                });
            } else {
                rocket.ajax.handleTargetUrl(targetUrl);
            }
        },

        blockUI: function (options) {
            if (options.blockUI) {
                if (options.blockUI === true) { //block whole page
                    rocket.ui.setBusy();
                } else { //block an element
                    rocket.ui.setBusy(options.blockUI);
                }
            }
        },

        unblockUI: function (options) {
            if (options.blockUI) {
                if (options.blockUI === true) { //unblock whole page
                    rocket.ui.clearBusy();
                } else { //unblock an element
                    rocket.ui.clearBusy(options.blockUI);
                }
            }
        },

        ajaxSendHandler: function (event, request, settings) {
            var token = rocket.security.antiForgery.getToken();
            if (!token) {
                return;
            }

            if (!settings.headers || settings.headers[rocket.security.antiForgery.tokenHeaderName] === undefined) {
                request.setRequestHeader(rocket.security.antiForgery.tokenHeaderName, token);
            }
        }
    });

    $(document).ajaxSend(function (event, request, settings) {
        return rocket.ajax.ajaxSendHandler(event, request, settings);
    });

    rocket.event.on('rocket.configurationInitialized', function () {
        var l = rocket.localization.getResource('RocketUi');

        rocket.ajax.defaultError.message = l('DefaultErrorMessage');
        rocket.ajax.defaultError.details = l('DefaultErrorMessageDetail');
        rocket.ajax.defaultError401.message = l('DefaultErrorMessage401');
        rocket.ajax.defaultError401.details = l('DefaultErrorMessage401Detail');
        rocket.ajax.defaultError403.message = l('DefaultErrorMessage403');
        rocket.ajax.defaultError403.details = l('DefaultErrorMessage403Detail');
        rocket.ajax.defaultError404.message = l('DefaultErrorMessage404');
        rocket.ajax.defaultError404.details = l('DefaultErrorMessage404Detail');
    });

    // RESOURCE LOADER ////////////////////////////////////////////////////////

    /* UrlStates enum */
    var UrlStates = {
        LOADING: 'LOADING',
        LOADED: 'LOADED',
        FAILED: 'FAILED'
    };

    /* UrlInfo class */
    function UrlInfo(url) {
        this.url = url;
        this.state = UrlStates.LOADING;
        this.loadCallbacks = [];
        this.failCallbacks = [];
    }

    UrlInfo.prototype.succeed = function () {
        this.state = UrlStates.LOADED;
        for (var i = 0; i < this.loadCallbacks.length; i++) {
            this.loadCallbacks[i]();
        }
    };

    UrlInfo.prototype.failed = function () {
        this.state = UrlStates.FAILED;
        for (var i = 0; i < this.failCallbacks.length; i++) {
            this.failCallbacks[i]();
        }
    };

    UrlInfo.prototype.handleCallbacks = function (loadCallback, failCallback) {
        switch (this.state) {
            case UrlStates.LOADED:
                loadCallback && loadCallback();
                break;
            case UrlStates.FAILED:
                failCallback && failCallback();
                break;
            case UrlStates.LOADING:
                this.addCallbacks(loadCallback, failCallback);
                break;
        }
    };

    UrlInfo.prototype.addCallbacks = function (loadCallback, failCallback) {
        loadCallback && this.loadCallbacks.push(loadCallback);
        failCallback && this.failCallbacks.push(failCallback);
    };

    /* ResourceLoader API */

    rocket.ResourceLoader = (function () {

        var _urlInfos = {};

        function getCacheKey(url) {
            return url;
        }

        function appendTimeToUrl(url) {

            if (url.indexOf('?') < 0) {
                url += '?';
            } else {
                url += '&';
            }

            url += '_=' + new Date().getTime();

            return url;
        }

        var _loadFromUrl = function (url, loadCallback, failCallback, serverLoader) {

            var cacheKey = getCacheKey(url);

            var urlInfo = _urlInfos[cacheKey];

            if (urlInfo) {
                urlInfo.handleCallbacks(loadCallback, failCallback);
                return;
            }

            _urlInfos[cacheKey] = urlInfo = new UrlInfo(url);
            urlInfo.addCallbacks(loadCallback, failCallback);

            serverLoader(urlInfo);
        };

        var _loadScript = function (url, loadCallback, failCallback) {
            _loadFromUrl(url, loadCallback, failCallback, function (urlInfo) {
                $.get({
                    url: url,
                    dataType: 'text'
                })
                .done(function (script) {
                    $.globalEval(script);
                    urlInfo.succeed();
                })
                .fail(function () {
                    urlInfo.failed();
                });
            });
        };

        var _loadStyle = function (url) {
            _loadFromUrl(url, undefined, undefined, function (urlInfo) {

                $('<link/>', {
                    rel: 'stylesheet',
                    type: 'text/css',
                    href: appendTimeToUrl(url)
                }).appendTo('head');
            });
        };

        return {
            loadScript: _loadScript,
            loadStyle: _loadStyle
        }
    })();

})(jQuery);