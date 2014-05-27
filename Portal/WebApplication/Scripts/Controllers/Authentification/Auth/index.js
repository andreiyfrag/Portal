require([
        'jquery',
        'bforms-validate-unobtrusive',
], function () {

    $(function () {

        $('.js-loginForm').find('.form_container').removeClass('loading');
        $('.js-registerForm').find('.form_container').removeClass('loading');
        
        $('.js-loginSubmit').on('click', function (e) {
            var $form = $('.js-loginForm');
            $.validator.unobtrusive.parse($form);
            if ($form.valid()) {
                $('.js-loginSubmit').hide();
            }
        });
        

        $('.js-registerSubmit').on('click', function (e) {
            var $form = $('.js-registerForm');
            $.validator.unobtrusive.parse($form);
            if ($form.valid()) {
                $('.js-registerSubmit').hide();
            }
        });

    });

})