(function ($) {

    // Global settings
    var $html = $("html"),
        $train = $html.find("#train");
    var App = {};
    App.utils = {
        getDocHeight: function () {
            var D = document;
            return Math.max(
            Math.max(D.body.scrollHeight, D.documentElement.scrollHeight),
            Math.max(D.body.offsetHeight, D.documentElement.offsetHeight),
            Math.max(D.body.clientHeight, D.documentElement.clientHeight));
        },
        getDocWidth: function () {
            var D = document;
            return Math.max(
            Math.max(D.body.scrollWidth, D.documentElement.scrollWidth),
            Math.max(D.body.offsetWidth, D.documentElement.offsetWidth),
            Math.max(D.body.clientWidth, D.documentElement.clientWidth));
        },
        getScrollTop: function () {
            if(typeof pageYOffset!= 'undefined'){
                //most browsers
                return pageYOffset;
            }
            else{
                var B= document.body; //IE 'quirks'
                var D= document.documentElement; //IE with doctype
                D= (D.clientHeight)? D: B;
                return D.scrollTop;
            }
        }
    };

    App.overlay = {
        cachedContent: "",
        cachedVideo: $('.video', $('#video-overlay-content')),
        cachedVideoIframe: $('.video', $('#video-overlay-content')).html(),
        overlayElement: $('#overlay')
    };
    App.form = {
        hasSubmitted: false,
        validateEmail: function (email) {
            var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email);
        }
    };

    /* Image Slider    ------------------------------------------- */
    $("#scrollable").scrollable({ circular: true, speed: 800, easing: 'swing' }).autoscroll({ autoplay: true, interval: 10000 });
    window.scrollableApi = $("#scrollable").data("scrollable");

    /* Position carousel in center for screens larger than 980px  ------------------------------------------- */
    function centerCarousel() {
        if (App.utils.getDocWidth() > 980) {
            var diff = (App.utils.getDocWidth() - 1400) / 2;
            $('#scrollable').css({ 'left': diff + 'px' });
        }
    }
    centerCarousel();
    $(window).resize(function () {
        centerCarousel();
    });

    //Add movement to arrow on homepage training panel
    $train.wrapInner("<a href='#' />");
    $("#training").hover(function () {
        $(this).addClass("hover").children("#arrow").stop(true, true).animate({ top: "14px" });
    }, function () {
        $(this).removeClass("hover").children("#arrow").stop(true, true).animate({ top: "4px" });
    });

    $("#training").click(function () {
        $("html,body").animate({ scrollTop: "857px" }, 2000, "easeOutQuint");
    });

    /* Accordion list */
    function setupAccordions() {
        var accordionsList = $('.accordion-list .accordion-item');
        if (accordionsList.length > 0) {
            accordionsList.each(function () {
                var _self = $(this);
                var thisTrigger = _self.find('h5');
                var thisBody = _self.find('.accordion-body');

                thisTrigger.hover(function () {
                    $(this).addClass("accordian-hover");
                }, function () {
                    $(this).removeClass("accordian-hover");
                });
                thisTrigger.bind('click', function (e) {
                    e.preventDefault();
                    if (!thisBody.is(':visible')) {
                        thisTrigger.removeClass('closed').addClass('open');
                        thisBody.slideDown(300);
                    } else {
                        thisTrigger.removeClass('open').addClass('closed');
                        thisBody.slideUp(300);
                    };
                });
            });
        };
    };

    setupAccordions();

    /* Overlay    ------------------------------------------- */
    $('.open-overlay', $('#content')).bind('click', function (e) {
        e.preventDefault();

        if (scrollableApi != undefined) scrollableApi.stop(); // stop carousel


        //        if ($(this).hasClass('video-overlay')) {
        //            $('#overlay-bg').addClass('video-overlay');
        //            if (App.overlay.cachedVideo.html() === '') {
        //                App.overlay.cachedVideo.html(App.overlay.cachedVideoIframe);
        //            }
        //        }

        var target = $(this).attr('href');
        App.overlay.cachedContent = $(target);
        App.overlay.cachedContent.removeClass("hidden").css({'top': App.utils.getScrollTop() +'px'});
        App.overlay.overlayElement.fadeIn(300, function () { });
        $('#overlay-bg').removeClass("hidden");
        $('#overlay').addClass("show").css({ 'height': App.utils.getDocHeight() + 'px' });
    });

    $('.button-close', ('#overlay')).bind('click', function (e) {
        e.preventDefault();
        if ($(this).hasClass('video-overlay')) {
            App.overlay.cachedVideo.html('');
        }
        App.overlay.cachedContent.addClass("hidden");
        $('#overlay-bg').addClass("hidden");
        $('#overlay').removeClass("show");
        App.overlay.overlayElement.fadeOut(300);
        if (scrollableApi != undefined) scrollableApi.play(); // resume carousel
    });

    $('#overlay-bg').bind('click', function (e) {
        if ($(this).hasClass('video-overlay')) {
            App.overlay.cachedVideo.html('');
            $(this).removeClass('video-overlay');
        }
        App.overlay.cachedContent.addClass("hidden");
        $('#overlay-bg').addClass("hidden");
        $('#overlay').removeClass("show");
        App.overlay.overlayElement.fadeOut(300);
        if (scrollableApi != undefined) scrollableApi.play(); // resume carousel
    });

    /* Placeholder fallback ------------------------------------------- */
    function hasPlaceHolder() {
        return 'placeholder' in document.createElement('input');
    }

    var formFields = $('.input-email', $('#email-content'));

    function html5forms() {
        var formPlaceholder = hasPlaceHolder();
        if (formPlaceholder === false) { // Fallback for non-supporting browsers
            $(formFields).each(function () {
                if ($(this).attr('placeholder')) {
                    var placeholderText = $(this).attr('placeholder');
                    $(this).attr('value', placeholderText);
                }
            });
            emptyTextBox();
        } else {
            $(formFields).focus(function () {
                $(this).attr('placeholder', '');
                $(this).attr('value', '');
                $(this).removeClass('error');
            });
        }
    }

    function emptyTextBox() {
        $(formFields).each(function () {
            if ($(this).attr('value')) {
                var inputText = $(this).attr('value');
                $(this).focus(function () {
                    if ($(this).attr('value') == inputText) {
                        $(this).val('');
                        $(this).removeClass('error');
                    }
                }).blur(function () {
                    if ($(this).attr('value') == '') {
                        $(this).val(inputText);
                    }
                });
            }
        });
    }

    html5forms();

    /* Validation ------------------------------------------- */
    $(".input-submit", $('#email-content')).click(function (e) {
        var isValid = false;

        $('#email-content').find('.required').each(function (i) {
            var value = $(this).attr('value');
            if ($(this).hasClass('input-email')) {

                if (App.form.validateEmail(value)) {
                    isValid = true;
                } else {
                    var errorMsg = 'Please enter your email address';
                    $(this).attr('placeholder', errorMsg);
                    $(this).attr('value', errorMsg);
                    html5forms();
                    $(this).addClass('error');
                }
            }
        });

        if (isValid) {
            if (!App.form.hasSubmitted) {
                App.form.hasSubmitted = true;
                $(this).addClass("btn-disabled");
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }
);

} (jQuery));