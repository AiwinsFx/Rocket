var rocket = rocket || {};
(function ($) {
    if (!sweetAlert || !$) {
        return;
    }

    /* DEFAULTS *************************************************/

    rocket.libs = rocket.libs || {};
    rocket.libs.sweetAlert = {
        config: {
            'default': {

            },
            info: {
                icon: 'info'
            },
            success: {
                icon: 'success'
            },
            warn: {
                icon: 'warning'
            },
            error: {
                icon: 'error'
            },
            confirm: {
                icon: 'warning',
                title: 'Are you sure?',
                buttons: ['Cancel', 'Yes']
            }
        }
    };

    /* MESSAGE **************************************************/

    var showMessage = function (type, message, title) {
        if (!title) {
            title = message;
            message = undefined;
        }

        var opts = $.extend(
            {},
            rocket.libs.sweetAlert.config['default'],
            rocket.libs.sweetAlert.config[type],
            {
                title: title,
                text: message
            }
        );

        return $.Deferred(function ($dfd) {
            sweetAlert(opts).then(function () {
                $dfd.resolve();
            });
        });
    };

    rocket.message.info = function (message, title) {
        return showMessage('info', message, title);
    };

    rocket.message.success = function (message, title) {
        return showMessage('success', message, title);
    };

    rocket.message.warn = function (message, title) {
        return showMessage('warn', message, title);
    };

    rocket.message.error = function (message, title) {
        return showMessage('error', message, title);
    };

    rocket.message.confirm = function (message, titleOrCallback, callback, closeOnEsc) {

        var userOpts = {
            text: message
        };

        if ($.isFunction(titleOrCallback)) {
            closeOnEsc = callback;
            callback = titleOrCallback;
        } else if (titleOrCallback) {
            userOpts.title = titleOrCallback;
        };

        userOpts.closeOnEsc = closeOnEsc;

        var opts = $.extend(
            {},
            rocket.libs.sweetAlert.config['default'],
            rocket.libs.sweetAlert.config.confirm,
            userOpts
        );

        return $.Deferred(function ($dfd) {
            sweetAlert(opts).then(function (isConfirmed) {
                callback && callback(isConfirmed);
                $dfd.resolve(isConfirmed);
            });
        });
    };

    rocket.event.on('rocket.configurationInitialized', function () {
        var l = rocket.localization.getResource('RocketUi');

        rocket.libs.sweetAlert.config.confirm.title = l('AreYouSure');
        rocket.libs.sweetAlert.config.confirm.buttons = [l('Cancel'), l('Yes')];
    });

})(jQuery);