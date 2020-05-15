(function ($) {

    function enableFormFeatures($forms, validate) {
        if ($forms.length) {
            $forms.each(function () {
                var $form = $(this);

                if (validate === true) {
                    $.validator.unobtrusive.parse($form);
                }

                var confirmText = $form.attr('data-confirm');
                if (confirmText) {
                    $form.submit(function (e) {
                        if (!$form.data('rocket-confirmed')) {
                            e.preventDefault();
                            rocket.message.confirm(confirmText).done(function (accepted) {
                                if (accepted) {
                                    $form.data('rocket-confirmed', true);
                                    $form.submit();
                                    $form.data('rocket-confirmed', undefined);
                                }
                            });
                        }
                    });
                }

                if ($form.attr('data-ajaxForm') === 'true') {
                    $form.rocketAjaxForm();
                }
            });
        }
    }

    function initializeScript($el) {
        $el.findWithSelf('[data-script-class]').each(function () {
            var scriptClassName = $(this).attr('data-script-class');
            if (!scriptClassName) {
                return;
            }

            var scriptClass = eval(scriptClassName);
            if (!scriptClass) {
                return;
            }

            var scriptObject = new scriptClass();
            $el.data('rocket-script-object', scriptObject);

            scriptObject.initDom && scriptObject.initDom($el);
        });
    }

    rocket.dom.onNodeAdded(function (args) {
        args.$el.findWithSelf('[data-toggle="tooltip"]').tooltip({
            container: 'body'
        });

        args.$el.findWithSelf('[data-toggle="popover"]').popover({
            container: 'body'
        });

        args.$el.findWithSelf('.timeago').timeago();

        enableFormFeatures(args.$el.findWithSelf('form'), true);

        initializeScript(args.$el);
    });

    rocket.dom.onNodeRemoved(function (args) {
        args.$el.findWithSelf('[data-toggle="tooltip"]').each(function () {
            $('#' + $(this).attr('aria-describedby')).remove();
        });
    });

    $(function () {
        enableFormFeatures($('form'));

        $('[data-toggle="tooltip"]').tooltip({
            container: 'body'
        });

        $('[data-toggle="popover"]').popover({
            container: 'body'
        });

        $('.timeago').timeago();

        $('[data-auto-focus="true"]').first().findWithSelf('input,select').focus();
    });

})(jQuery);