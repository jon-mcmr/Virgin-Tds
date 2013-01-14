(function ($) {

    // Global settings
    var App = {};
    App.overlay = {
        cachedContent: "",
        overlayElement: $('#overlay'),
        getDocHeight: function () {
            var D = document;
            return Math.max(
                Math.max(D.body.scrollHeight, D.documentElement.scrollHeight),
                Math.max(D.body.offsetHeight, D.documentElement.offsetHeight),
                Math.max(D.body.clientHeight, D.documentElement.clientHeight));
        }
    };

    App.chosen = {
        init : function() {
            var isiPad,
                isTouchNotiPad;
            $html = $("html");
            if (navigator.userAgent.match(/iPad/i)) {
                isiPad = true;
                $html.addClass("iPad");
            } else if (($html.hasClass("touch")) && (!navigator.userAgent.match(/iPad/i))) {
                isTouchNotiPad = true;
            }
            $clubNameSelect = $("#overlay .chosen-select");

            //iPad has styled selects, iPhone/ie6 is not styled, rest uses form porn
            if(isiPad){
                $clubNameSelect.styleSelects().change(function() {
                    App.chosen.gotoClubUrl($clubNameSelect);
                });
            } else if(isTouchNotiPad || $html.hasClass("ie6")){
                $clubNameSelect.change(function() {
                    App.chosen.gotoClubUrl($clubNameSelect);
                });
            } else {
                $clubNameSelect.chosen().change(function() {
                    App.chosen.gotoClubUrl($clubNameSelect);
                });
            }
        },
        gotoClubUrl : function (context) {
            var overlayListUrl = context.children("option:selected").val();
            setTimeout(function() {
                window.location = overlayListUrl;
            }, 100);
        }

    }

    /* Overlay    ------------------------------------------- */
    $('.open-overlay', $('#main')).bind('click', function (e) {
        e.preventDefault();
        var target = $(this).attr('href');
        App.overlay.cachedContent = $(target);
        App.overlay.cachedContent.removeClass("hidden");
        App.overlay.overlayElement.fadeIn(300, function () { });
        $('#overlay-bg').removeClass("hidden");
        $('#overlay').addClass("show").css({ 'height': App.overlay.getDocHeight() + 'px' });
    });

    $('.button-close', ('#overlay')).bind('click', function (e) {
        e.preventDefault();
        App.overlay.cachedContent.addClass("hidden");
        $('#overlay-bg').addClass("hidden");
        $('#overlay').removeClass("show");
        App.overlay.overlayElement.fadeOut(300);
    });

    $('#overlay-bg').bind('click', function (e) {
        App.overlay.cachedContent.addClass("hidden");
        $('#overlay-bg').addClass("hidden");
        $('#overlay').removeClass("show");
        App.overlay.overlayElement.fadeOut(300);
    });

    /* Chosen form plugin    ------------------------------------------- */
    App.chosen.init();

} (jQuery));