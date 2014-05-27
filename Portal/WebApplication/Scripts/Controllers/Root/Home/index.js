require([
      'bforms-grid',
    'bforms-toolbar',
    'bootstrap',
    'jquery',
        'bforms-inlineQuestion',
    'main-menu'
], function () {
    var HomeIndex = function (options) {
        //options are set in controller action body 
        //eg: HomeController action Index RequireJsOptions.Add("option_name", option_value);
        this.options = $.extend(true, {}, options);
    };

    HomeIndex.prototype.init = function () {
        this.initCarousel();
        this.initSelectors();
        this.addDelegates();
        
       
    };

    HomeIndex.prototype.initSelectors = function() {
        this.$workingSelector = $('.js-work');
        this.$hiringSelector = $('js.hire');
        this.$signUpSelector = $('.js-signUp');
    };

    HomeIndex.prototype.addDelegates = function() {
      //  this.$signUpSelector.on('click',this.$workingSelector,$.proxy(this._onClickWorking,this));
    };


    HomeIndex.prototype._onClickWorking = function(e) {
        e.preventDefault();
        $('.js-links').hide();
        $('.js-form').show();


    };
    
    HomeIndex.prototype.initCarousel = function() {
        // Activates the Carousel
        $('.carousel').carousel({
            interval: 5000
        });

        // Activates Tooltips for Social Links
        $('.tooltip-social').tooltip({
            selector: "a[data-toggle=tooltip]"
        });
    };

    $(document).ready(function () {
        var ctrl = new HomeIndex(window.requireConfig.pageOptions);
        
        ctrl.init();
        
    });
    



});
