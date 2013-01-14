var virginactive = {};
virginactive.mobile = function () {

    //Geolocation module
    var geolocation = function () {

        var handleLocation = function (position) {
            var lat = position.coords.latitude;
            var lon = position.coords.longitude;

            if(lat == 0 && long == 0){
                alert('We were unable to get your location, please move somewhere with better GPS signal');
                $('.button.geolocation').removeClass('loading').find('strong').html('Near my current location');
            }
            else{
                
                window.location.href = '/club-finder/results/?lat=' + lat + '&lng=' + lon;
            }
        };

        var errorHandler = function (error) {
            //Do something
        };

        return {
            init: function () {
                if (navigator.geolocation) {
                    $('.button.geolocation').on('click', function () {
                        $(this).addClass('loading').find('strong').html('Checking your location');

                        navigator.geolocation.getCurrentPosition(handleLocation, errorHandler);

                        return false;
                    });
                }
            }
        };
    } ();

    //Image resize module

    var imgScale = function ($elements) {

        var imgResize = function () {
            var bodyWidth = $('body').width();
            $(".max-width").width(bodyWidth);
            $(".max-width").css("margin", "0 -20px");
        };

        $(window).on('resize', function () {
            imgResize();
        });

        imgResize();
    };

    //Carousel module
    var carousel = function () {
        var is_active = false;
        var startX;
        var startY;
        var isMoving = false;

        var prepareCarousel = function ($elements) {
            var bodyWidth = $('body').width();

            $elements.each(function () {
                var $this = $(this), $slides = $this.find('ul.items li'), slidesCount = $slides.length, i = 0, pager_html = '<ul class="pager">', $active_slide = null;

                $slides.css('float', 'left').find('img,p').css('width', $slides.width());

                $slides.first().addClass('active');

                $slides.not(':first').css('width', '0px').hide();

                $slides.each(function () {
                    var page_no = (i + 1);
                    pager_html += "<li>" + page_no + '</li>';
                    $(this).addClass('slideno_' + i);
                    i++;
                });
                $slides.last().prependTo($this.find('ul.items'));

                pager_html += "</ul>";

                $this.append(pager_html);
                $this.find('.pager li:first').addClass('active');

                $active_slide = $this.find('ul.items li.active');

                $slides.css('height', $active_slide.find('img').height()).css('position', 'absolute');
                $this.find('ul.items').css('height', $active_slide.find('img').height());

            });

            if ('ontouchstart' in document.documentElement) {
                document.documentElement.addEventListener('touchstart', onTouchStart, false);
            }

            $(window).on('resize', function () {
                resizeCarousel($elements);
            });

            resizeCarousel($elements);

        };

        var resizeCarousel = function ($elements) {
            var bodyWidth = $('body').width();

            $elements.each(function () {
                var $this = $(this), $slides = $this.find('ul.items li'), $active_slide = $this.find('ul.items li.active'), slidesCount = $slides.length;

                $this.find('li.active').css('width', bodyWidth);
                $slides.find('img,p').css('width', bodyWidth);

                $slides.css('height', $active_slide.find('img').height());

                $this.find('ul.items').css('height', $active_slide.find('img').height());
            });
        };

        var animateSlide = function ($enlarge, $shrink, full_width, shrink_start, enlarge_start, speed) {
            var enlarge_new_width = enlarge_start + speed;
            var shrink_new_width = shrink_start - speed;

            $enlarge.show();

            if (enlarge_new_width >= full_width) {
                $enlarge.css('width', full_width).addClass('active');
                $shrink.css('width', '0').hide().removeClass('active');

                var $carousel = $('.carousel ul.items');

                if ($carousel.find('li:last').hasClass('active')) {
                    $carousel.find('li:first').appendTo($carousel);
                }
                else if ($carousel.find('li:first').hasClass('active')) {
                    $carousel.find('li:last').prependTo($carousel);
                }

                is_active = false;
            }
            else {
                $enlarge.css('width', enlarge_new_width);
                $shrink.css('width', shrink_new_width);


                setTimeout(function () {

                    animateSlide($enlarge, $shrink, full_width, shrink_new_width, enlarge_new_width, speed);

                }, 10);


            }
        };

        var animateMethodSwitcher = function ($enlarge, $shrink, full_width, shrink_start, enlarge_start, speed) {
            animateSlide($enlarge, $shrink, full_width, shrink_start, enlarge_start, speed);
        };

        var animateTrigger = function (direction) {
            if (is_active === false) {
                var speed = 40, $carousel = $('.carousel ul.items'), $pager = $('.carousel ul.pager'), $pager_active = $pager.find('.active'), $active = $carousel.find('li.active');
                is_active = true;

                if (direction === 'next') {
                    $active.next().css('left', 'auto').css('right', '0px');
                    $active.css('right', 'auto').css('left', '0px');

                    animateMethodSwitcher($active.next(), $active, $active.width(), $active.width(), 0, speed);

                    if ($pager_active.next().length) {
                        $pager_active.next().addClass('active');
                    }
                    else {
                        $pager.find('li').first().addClass('active');
                    }

                    $pager_active.removeClass('active');
                }
                else {
                    $active.prev().css('right', 'auto').css('left', '0px');
                    $active.css('left', 'auto').css('right', '0px');

                    animateMethodSwitcher($active.prev(), $active, $active.width(), $active.width(), 0, speed);

                    if ($pager_active.prev().length) {
                        $pager_active.prev().addClass('active');
                    }
                    else {
                        $pager.find('li').last().addClass('active');
                    }

                    $pager_active.removeClass('active');

                }
            }
        };

        var cancelTouch = function () {
            //this.removeEventListener('touchmove', onTouchMove);
            startX = null;
            isMoving = false;
        };

        var onTouchMove = function (e) {


            if (isMoving) {
                var x = e.originalEvent.touches[0].pageX, y = e.originalEvent.touches[0].pageY, dx = startX - x, dy = startY - y;

                if (Math.abs(dx) >= 10) {
                    cancelTouch();

                    if (dx > 0) {
                        nextSlide();
                    }
                    else {
                        prevSlide();
                    }

                    e.preventDefault();
                }
            }
        };

        var onTouchStart = function (e) {

            if (e.touches.length === 1) {
                startX = e.touches[0].pageX;
                startY = e.touches[0].pageY;
                isMoving = true;

                $(document).bind('touchmove', onTouchMove);
            }
        };

        nextSlide = function () {
            animateTrigger('next');
        };

        prevSlide = function () {
            animateTrigger('prev');
        };

        return {
            init: function (elements) {
                var $elements = $(elements);

                if ($elements.length) {
                    prepareCarousel($elements);
                }
            }
        };
    } ();

    //Maps module
    var maps = function () {

        var resizeMap = function () {
            var mapwidth = parseInt($(".data_content").width(), 10);
            var ratio = 1.54444444444444;
            var mapheight = parseInt(mapwidth / ratio - 20, 10);
            var $map_content = $(".map .map_content");
            var long = $map_content.attr('data-lng');
            var lat = $map_content.attr('data-lat');

            $map_content.html('');
            var mapSRC = "http://maps.googleapis.com/maps/api/staticmap?zoom=14&size=" + mapwidth + "x" + mapheight + "&maptype=roadmap&markers=color:red|color:red|size:small|" + lat + "," + long + "&sensor=false";
            $map_content.append('<a href="https://maps.google.co.uk/?q=' + lat + ',' + long + '"><img src="' + mapSRC + '" /></a>');
        };

        return {
            init: function () {

                $(window).on('resize', resizeMap);
                resizeMap();
            }
        };
    } ();

    //Maps module
    var validation = function () {
        var submittted = false;


        var validateClick = function () {
            var valid = false;

            if(submittted === false){

                valid = validateForm();
                if (valid) {

                    $('.button.red').removeClass('red').addClass('loading').html('<span><strong>Sending...</strong></span>');
                    
                    setTimeout(function(){
                        var itemname = $("#form_fields .btn-submit").attr('name');
                        sendAnalytics();
                        submittted = true;
                        __doPostBack(itemname, '');
                    },100);
                }
            }

            return false;
        };

        //Send the google analytics code
        var sendAnalytics = function () {
            //this is a web lead -load open tag for web lead confirmation 
            if (typeof (_gaq) !== undefined) {
                var ot = document.createElement('script');
                ot.async = true;
                ot.src = ('//d3c3cq33003psk.cloudfront.net/opentag-32825-68231.js');
                var s = document.getElementsByTagName('script')[0];
                s.parentNode.insertBefore(ot, s);

                //record trackevent
                var gaqCategory = "Form",
						gaqAction = "Submit",
						gaqLabel = $('fieldset').attr('data-name');
                _gaq.push(["_trackEvent", gaqCategory, gaqAction, gaqLabel]);

            }
        };

        var validateForm = function () {
            var $formFields = $('fieldset input, fieldset select, fieldset textarea'), valid = true;

            $formFields.each(function (event) {
                var $this = $(this), validationClass = $this.attr('class') || "", furtherValidation = true, value = $this.val() || "";

                $this.removeClass('error');

                if(($this.next().prop('tagName') === "P") && ($this.next().hasClass('error'))){
                    $this.next().remove();
                }

                //Validate if it is required
                if (validationClass.indexOf("required") !== -1) {
                    if (validateRequired(value) === false) {
                        valid = false;
                        setError($this, $this.attr('data-required'),true);

                        furtherValidation = false;

                    }
                }

                //If has passsed required OR is not required but requires valid value then validate furthe
                if (furtherValidation) {
                    if (validationClass.indexOf("email") !== -1) {
                        if (validateEmail(value) === false) {
                            valid = false;
                            setError($this, $this.attr('data-email'),false);
                        }
                    }
                    else if (validationClass.indexOf("phone") !== -1) {
                        if (validatePhone(value) === false) {
                            valid = false;
                            setError($this, $this.attr('data-phone'),false);

                        }
                    }
                }

            });


            $formFields.off();
            $formFields.on('change', validateForm);

            return valid;
        };

        var setError = function ($this, error, isPlaceholder) {
            var tag = $this.prop('tagName');

            $this.addClass('error');

            if(isPlaceholder){
                if (tag === "SELECT") {
                    $this.find('.default').html(error);
                } else {
                    $this.attr('placeholder', error);
                }
            }
            else{
                $('<p class="error">'+ error +'</p>').insertAfter($this);
            }

            
        };

        var validateEmail = function (value) {
            var valid = false;

            if (value.match(/^((?:(?:(?:\w[\.\-\+]?)*)\w)+)\@((?:(?:(?:\w[\.\-\+]?){0,62})\w)+)\.(\w{2,6})$/)) {
                valid = true;
            }

            return valid;
        };

        var validateRequired = function (value) {
            var valid = true;

            if (value === null || value === "") {
                valid = false;
            }

            return valid;
        };

        var validatePhone = function (value) {
            var valid = false;

            if (value.match(/^(0(\d|\s){8,12}$)|(\+(\d|\s){8,12}$)/)) {
                valid = true;
            }

            return valid;
        };

        return {
            validate: validateClick
        };
    } ();
    return {
        init: function () {
            geolocation.init();
            carousel.init('.carousel');
            maps.init();
            imgScale();

            setTimeout(function () {
                window.scrollTo(0, 1);
            }, 0);

            // setupTransitions();
        },
        validation: validation
    };

} ();

$(window).load(virginactive.mobile.init);

//Callback for phone number placement on VA mobile
var adInsightPostReplacement = function() {
    //get the placeholder element and use the tracking number to set the trackingNum variable
    var trackingNum = document.getElementById('divPhone').innerHTML.split(' ').join('');
    var trackingNum = trackingNum.substr(trackingNum.indexOf('>') + 1, 11);
    //use the updated trackingNum variable to set the href value on the click to call button
    var myAnchor = document.getElementById('aTagPhone').href = "tel:" + trackingNum;
    document.getElementById('divPhone').innerHTML = trackingNum;
};
