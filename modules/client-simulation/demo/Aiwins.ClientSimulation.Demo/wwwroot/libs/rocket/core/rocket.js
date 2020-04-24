var rocket = rocket || {};
(function () {

    /* Application paths *****************************************/

    //Current application root path (including virtual directory if exists).
    rocket.appPath = rocket.appPath || '/';

    rocket.pageLoadTime = new Date();

    //Converts given path to absolute path using rocket.appPath variable.
    rocket.toAbsAppPath = function (path) {
        if (path.indexOf('/') == 0) {
            path = path.substring(1);
        }

        return rocket.appPath + path;
    };

    /* LOGGING ***************************************************/
    //Implements Logging API that provides secure & controlled usage of console.log

    rocket.log = rocket.log || {};

    rocket.log.levels = {
        DEBUG: 1,
        INFO: 2,
        WARN: 3,
        ERROR: 4,
        FATAL: 5
    };

    rocket.log.level = rocket.log.levels.DEBUG;

    rocket.log.log = function (logObject, logLevel) {
        if (!window.console || !window.console.log) {
            return;
        }

        if (logLevel != undefined && logLevel < rocket.log.level) {
            return;
        }

        console.log(logObject);
    };

    rocket.log.debug = function (logObject) {
        rocket.log.log("DEBUG: ", rocket.log.levels.DEBUG);
        rocket.log.log(logObject, rocket.log.levels.DEBUG);
    };

    rocket.log.info = function (logObject) {
        rocket.log.log("INFO: ", rocket.log.levels.INFO);
        rocket.log.log(logObject, rocket.log.levels.INFO);
    };

    rocket.log.warn = function (logObject) {
        rocket.log.log("WARN: ", rocket.log.levels.WARN);
        rocket.log.log(logObject, rocket.log.levels.WARN);
    };

    rocket.log.error = function (logObject) {
        rocket.log.log("ERROR: ", rocket.log.levels.ERROR);
        rocket.log.log(logObject, rocket.log.levels.ERROR);
    };

    rocket.log.fatal = function (logObject) {
        rocket.log.log("FATAL: ", rocket.log.levels.FATAL);
        rocket.log.log(logObject, rocket.log.levels.FATAL);
    };

    /* LOCALIZATION ***********************************************/

    rocket.localization = rocket.localization || {};

    rocket.localization.values = {};

    rocket.localization.localize = function (key, sourceName) {
        sourceName = sourceName || rocket.localization.defaultResourceName;

        var source = rocket.localization.values[sourceName];

        if (!source) {
            rocket.log.warn('Could not find localization source: ' + sourceName);
            return key;
        }

        var value = source[key];
        if (value == undefined) {
            return key;
        }

        var copiedArguments = Array.prototype.slice.call(arguments, 0);
        copiedArguments.splice(1, 1);
        copiedArguments[0] = value;

        return rocket.utils.formatString.apply(this, copiedArguments);
    };

    rocket.localization.getResource = function (name) {
        return function () {
            var copiedArguments = Array.prototype.slice.call(arguments, 0);
            copiedArguments.splice(1, 0, name);
            return rocket.localization.localize.apply(this, copiedArguments);
        };
    };

    rocket.localization.defaultResourceName = undefined;

    /* AUTHORIZATION **********************************************/

    rocket.auth = rocket.auth || {};

    rocket.auth.policies = rocket.auth.policies || {};

    rocket.auth.grantedPolicies = rocket.auth.grantedPolicies || {};

    rocket.auth.isGranted = function (policyName) {
        return rocket.auth.policies[policyName] != undefined && rocket.auth.grantedPolicies[policyName] != undefined;
    };

    rocket.auth.isAnyGranted = function () {
        if (!arguments || arguments.length <= 0) {
            return true;
        }

        for (var i = 0; i < arguments.length; i++) {
            if (rocket.auth.isGranted(arguments[i])) {
                return true;
            }
        }

        return false;
    };

    rocket.auth.areAllGranted = function () {
        if (!arguments || arguments.length <= 0) {
            return true;
        }

        for (var i = 0; i < arguments.length; i++) {
            if (!rocket.auth.isGranted(arguments[i])) {
                return false;
            }
        }

        return true;
    };

    rocket.auth.tokenCookieName = 'Rocket.AuthToken';

    rocket.auth.setToken = function (authToken, expireDate) {
        rocket.utils.setCookieValue(rocket.auth.tokenCookieName, authToken, expireDate, rocket.appPath, rocket.domain);
    };

    rocket.auth.getToken = function () {
        return rocket.utils.getCookieValue(rocket.auth.tokenCookieName);
    }

    rocket.auth.clearToken = function () {
        rocket.auth.setToken();
    }

    /* SETTINGS *************************************************/

    rocket.setting = rocket.setting || {};

    rocket.setting.values = rocket.setting.values || {};

    rocket.setting.get = function (name) {
        return rocket.setting.values[name];
    };

    rocket.setting.getBoolean = function (name) {
        var value = rocket.setting.get(name);
        return value == 'true' || value == 'True';
    };

    rocket.setting.getInt = function (name) {
        return parseInt(rocket.setting.values[name]);
    };

    /* NOTIFICATION *********************************************/
    //Defines Notification API, not implements it

    rocket.notify = rocket.notify || {};

    rocket.notify.success = function (message, title, options) {
        rocket.log.warn('rocket.notify.success is not implemented!');
    };

    rocket.notify.info = function (message, title, options) {
        rocket.log.warn('rocket.notify.info is not implemented!');
    };

    rocket.notify.warn = function (message, title, options) {
        rocket.log.warn('rocket.notify.warn is not implemented!');
    };

    rocket.notify.error = function (message, title, options) {
        rocket.log.warn('rocket.notify.error is not implemented!');
    };

    /* MESSAGE **************************************************/
    //Defines Message API, not implements it

    rocket.message = rocket.message || {};

    rocket.message._showMessage = function (message, title) {
        alert((title || '') + ' ' + message);
    };

    rocket.message.info = function (message, title) {
        rocket.log.warn('rocket.message.info is not implemented!');
        return rocket.message._showMessage(message, title);
    };

    rocket.message.success = function (message, title) {
        rocket.log.warn('rocket.message.success is not implemented!');
        return rocket.message._showMessage(message, title);
    };

    rocket.message.warn = function (message, title) {
        rocket.log.warn('rocket.message.warn is not implemented!');
        return rocket.message._showMessage(message, title);
    };

    rocket.message.error = function (message, title) {
        rocket.log.warn('rocket.message.error is not implemented!');
        return rocket.message._showMessage(message, title);
    };

    rocket.message.confirm = function (message, titleOrCallback, callback) {
        rocket.log.warn('rocket.message.confirm is not properly implemented!');

        if (titleOrCallback && !(typeof titleOrCallback == 'string')) {
            callback = titleOrCallback;
        }

        var result = confirm(message);
        callback && callback(result);
    };

    /* UI *******************************************************/

    rocket.ui = rocket.ui || {};

    /* UI BLOCK */
    //Defines UI Block API and implements basically

    var $rocketBlockArea = document.createElement('div');
    $rocketBlockArea.classList.add('rocket-block-area');

    /* opts: { //Can be an object with options or a string for query a selector
     *  elm: a query selector (optional - default: document.body)
     *  busy: boolean (optional - default: false)
     *  promise: A promise with always or finally handler (optional - auto unblocks the ui if provided)
     * }
     */
    rocket.ui.block = function (opts) {
        if (!opts) {
            opts = {};
        } else if (typeof opts == 'string') {
            opts = {
                elm: opts
            };
        }

        var $elm = document.querySelector(opts.elm) || document.body;

        if (opts.busy) {
            $rocketBlockArea.classList.add('rocket-block-area-busy');
        } else {
            $rocketBlockArea.classList.remove('rocket-block-area-busy');
        }

        if (document.querySelector(opts.elm)) {
            $rocketBlockArea.style.position = 'absolute';
        } else {
            $rocketBlockArea.style.position = 'fixed';
        }

        $elm.appendChild($rocketBlockArea);

        if (opts.promise) {
            if (opts.promise.always) { //jQuery.Deferred style
                opts.promise.always(function () {
                    rocket.ui.unblock({
                        $elm: opts.elm
                    });
                });
            } else if (opts.promise['finally']) { //Q style
                opts.promise['finally'](function () {
                    rocket.ui.unblock({
                        $elm: opts.elm
                    });
                });
            }
        }
    };

    /* opts: {
     *    
     * }
     */
    rocket.ui.unblock = function (opts) {
        var element = document.querySelector('.rocket-block-area');
        if (element) {
            element.classList.add('rocket-block-area-disappearing');
            setTimeout(function () {
                if (element) {
                    element.classList.remove('rocket-block-area-disappearing');
                    element.parentElement.removeChild(element);
                }
            }, 250);
        }
    };

    /* UI BUSY */
    //Defines UI Busy API, not implements it

    rocket.ui.setBusy = function (opts) {
        if (!opts) {
            opts = {
                busy: true
            };
        } else if (typeof opts == 'string') {
            opts = {
                elm: opts,
                busy: true
            };
        }

        rocket.ui.block(opts);
    };

    rocket.ui.clearBusy = function (opts) {
        rocket.ui.unblock(opts);
    };

    /* SIMPLE EVENT BUS *****************************************/

    rocket.event = (function () {

        var _callbacks = {};

        var on = function (eventName, callback) {
            if (!_callbacks[eventName]) {
                _callbacks[eventName] = [];
            }

            _callbacks[eventName].push(callback);
        };

        var off = function (eventName, callback) {
            var callbacks = _callbacks[eventName];
            if (!callbacks) {
                return;
            }

            var index = -1;
            for (var i = 0; i < callbacks.length; i++) {
                if (callbacks[i] === callback) {
                    index = i;
                    break;
                }
            }

            if (index < 0) {
                return;
            }

            _callbacks[eventName].splice(index, 1);
        };

        var trigger = function (eventName) {
            var callbacks = _callbacks[eventName];
            if (!callbacks || !callbacks.length) {
                return;
            }

            var args = Array.prototype.slice.call(arguments, 1);
            for (var i = 0; i < callbacks.length; i++) {
                callbacks[i].apply(this, args);
            }
        };

        // Public interface ///////////////////////////////////////////////////

        return {
            on: on,
            off: off,
            trigger: trigger
        };
    })();


    /* UTILS ***************************************************/

    rocket.utils = rocket.utils || {};

    /* Creates a name namespace.
    *  Example:
    *  var taskService = rocket.utils.createNamespace(rocket, 'services.task');
    *  taskService will be equal to rocket.services.task
    *  first argument (root) must be defined first
    ************************************************************/
    rocket.utils.createNamespace = function (root, ns) {
        var parts = ns.split('.');
        for (var i = 0; i < parts.length; i++) {
            if (typeof root[parts[i]] == 'undefined') {
                root[parts[i]] = {};
            }

            root = root[parts[i]];
        }

        return root;
    };

    /* Find and replaces a string (search) to another string (replacement) in
    *  given string (str).
    *  Example:
    *  rocket.utils.replaceAll('This is a test string', 'is', 'X') = 'ThX X a test string'
    ************************************************************/
    rocket.utils.replaceAll = function (str, search, replacement) {
        var fix = search.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
        return str.replace(new RegExp(fix, 'g'), replacement);
    };

    /* Formats a string just like string.format in C#.
    *  Example:
    *  rocket.utils.formatString('Hello {0}','Tuana') = 'Hello Tuana'
    ************************************************************/
    rocket.utils.formatString = function () {
        if (arguments.length < 1) {
            return null;
        }

        var str = arguments[0];

        for (var i = 1; i < arguments.length; i++) {
            var placeHolder = '{' + (i - 1) + '}';
            str = rocket.utils.replaceAll(str, placeHolder, arguments[i]);
        }

        return str;
    };

    rocket.utils.toPascalCase = function (str) {
        if (!str || !str.length) {
            return str;
        }

        if (str.length === 1) {
            return str.charAt(0).toUpperCase();
        }

        return str.charAt(0).toUpperCase() + str.substr(1);
    }

    rocket.utils.toCamelCase = function (str) {
        if (!str || !str.length) {
            return str;
        }

        if (str.length === 1) {
            return str.charAt(0).toLowerCase();
        }

        return str.charAt(0).toLowerCase() + str.substr(1);
    }

    rocket.utils.truncateString = function (str, maxLength) {
        if (!str || !str.length || str.length <= maxLength) {
            return str;
        }

        return str.substr(0, maxLength);
    };

    rocket.utils.truncateStringWithPostfix = function (str, maxLength, postfix) {
        postfix = postfix || '...';

        if (!str || !str.length || str.length <= maxLength) {
            return str;
        }

        if (maxLength <= postfix.length) {
            return postfix.substr(0, maxLength);
        }

        return str.substr(0, maxLength - postfix.length) + postfix;
    };

    rocket.utils.isFunction = function (obj) {
        return !!(obj && obj.constructor && obj.call && obj.apply);
    };

    /**
     * parameterInfos should be an array of { name, value } objects
     * where name is query string parameter name and value is it's value.
     * includeQuestionMark is true by default.
     */
    rocket.utils.buildQueryString = function (parameterInfos, includeQuestionMark) {
        if (includeQuestionMark === undefined) {
            includeQuestionMark = true;
        }

        var qs = '';

        function addSeperator() {
            if (!qs.length) {
                if (includeQuestionMark) {
                    qs = qs + '?';
                }
            } else {
                qs = qs + '&';
            }
        }

        for (var i = 0; i < parameterInfos.length; ++i) {
            var parameterInfo = parameterInfos[i];
            if (parameterInfo.value === undefined) {
                continue;
            }

            if (parameterInfo.value === null) {
                parameterInfo.value = '';
            }

            addSeperator();

            if (parameterInfo.value.toJSON && typeof parameterInfo.value.toJSON === "function") {
                qs = qs + parameterInfo.name + '=' + encodeURIComponent(parameterInfo.value.toJSON());
            } else if (Array.isArray(parameterInfo.value) && parameterInfo.value.length) {
                for (var j = 0; j < parameterInfo.value.length; j++) {
                    if (j > 0) {
                        addSeperator();
                    }

                    qs = qs + parameterInfo.name + '[' + j + ']=' + encodeURIComponent(parameterInfo.value[j]);
                }
            } else {
                qs = qs + parameterInfo.name + '=' + encodeURIComponent(parameterInfo.value);
            }
        }

        return qs;
    }

    /**
     * Sets a cookie value for given key.
     * This is a simple implementation created to be used by ROCKET.
     * Please use a complete cookie library if you need.
     * @param {string} key
     * @param {string} value 
     * @param {Date} expireDate (optional). If not specified the cookie will expire at the end of session.
     * @param {string} path (optional)
     */
    rocket.utils.setCookieValue = function (key, value, expireDate, path) {
        var cookieValue = encodeURIComponent(key) + '=';

        if (value) {
            cookieValue = cookieValue + encodeURIComponent(value);
        }

        if (expireDate) {
            cookieValue = cookieValue + "; expires=" + expireDate.toUTCString();
        }

        if (path) {
            cookieValue = cookieValue + "; path=" + path;
        }

        document.cookie = cookieValue;
    };

    /**
     * Gets a cookie with given key.
     * This is a simple implementation created to be used by ROCKET.
     * Please use a complete cookie library if you need.
     * @param {string} key
     * @returns {string} Cookie value or null
     */
    rocket.utils.getCookieValue = function (key) {
        var equalities = document.cookie.split('; ');
        for (var i = 0; i < equalities.length; i++) {
            if (!equalities[i]) {
                continue;
            }

            var splitted = equalities[i].split('=');
            if (splitted.length != 2) {
                continue;
            }

            if (decodeURIComponent(splitted[0]) === key) {
                return decodeURIComponent(splitted[1] || '');
            }
        }

        return null;
    };

    /**
     * Deletes cookie for given key.
     * This is a simple implementation created to be used by ROCKET.
     * Please use a complete cookie library if you need.
     * @param {string} key
     * @param {string} path (optional)
     */
    rocket.utils.deleteCookie = function (key, path) {
        var cookieValue = encodeURIComponent(key) + '=';

        cookieValue = cookieValue + "; expires=" + (new Date(new Date().getTime() - 86400000)).toUTCString();

        if (path) {
            cookieValue = cookieValue + "; path=" + path;
        }

        document.cookie = cookieValue;
    }

    /* SECURITY ***************************************/
    rocket.security = rocket.security || {};
    rocket.security.antiForgery = rocket.security.antiForgery || {};

    rocket.security.antiForgery.tokenCookieName = 'XSRF-TOKEN';
    rocket.security.antiForgery.tokenHeaderName = 'X-XSRF-TOKEN';

    rocket.security.antiForgery.getToken = function () {
        return rocket.utils.getCookieValue(rocket.security.antiForgery.tokenCookieName);
    };

})();