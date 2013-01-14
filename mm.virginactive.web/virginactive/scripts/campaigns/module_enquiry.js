$.virginactive_campaign = {};
$.virginactive_campaign.timer = null;

$.virginactive_campaign.module_enquiry = function () {
	var timeoutSpeed = 10000;

    var setupCaptions = function () {
	    var $items = $('#carouselCaptions li');
	    var max = -1;
			
	    $items.each(function() {
		    var h = $(this).height(); 
		    max = h > max ? h : max;
	    });
			
		$items.css('height',max);    
    }

    var setupForms = function () {
        $(".chzn-clublist").chosen({ "hasSearch": true });
        $('.chzn-selectbox').chosen();
        
        $.validator.setDefaults({ ignore: [] });
        
        $('form').validate({
        	rules: {
        		content_0$drpHowDidYouFindUs: {
        			required: true,
        			notEqual: 'default'
        		},
        		content_0$txtName: 'required',
        		content_0$clubFindSelect: 'required',
        		content_0$txtEmail: {
        			required: true,
        			email: true
        		},
        		content_0$txtPhone: {
        			required: true,
        			phoneUK: true
        		},
         	}, 
			// the errorPlacement has to take the table layout into account 
			errorPlacement: function(error, element) { 
				element.parents('.wrap').addClass('error');
				
				error.appendTo( element.parent() ); 
			},
			success: function(element){
                var $wrap = element.parents('.wrap'),frmElement = null;
                
                if($wrap.find('.chzn-container').length){
                    frmElement = $wrap.find('.chzn-container');
                }
                else{
                    frmElement = $wrap.find('.form-element');
                } 				
                
                element.parents('.wrap').removeClass('error');
                element.parents('.wrap').find('span.valid').remove();
                $("<span class='valid'>valid</span>").insertAfter(frmElement);   
                //$('span.valid').not(":first").remove();
			}
        })
        
        $('.chzn-clublist,.chzn-selectbox').chosen().change(function() {
        	var $this = $(this), $wrap = $this.parents('.wrap'),selectedValue = $(this).find('option:selected').val();
        	$wrap.find('select').val(selectedValue).valid();
        });
        
//        $('.chzn-clublist').chosen().change(function() {
//        	var selectedValue = $(this).find('option:selected').val();
//        	$('#content_0_clubFindSelect').val(selectedValue);
//        	$('#content_0_clubFindSelect').valid();
//        });
//        
//        $('.chzn-selectbox').chosen().change(function() {
//        	var selectedValue = $(this).find('option:selected').val();
//        	$('#content_0_drpHowDidYouFindUs').val(selectedValue);
//        	$('#content_0_drpHowDidYouFindUs').valid();
//        });
        
        //$('select').change(function(){
        	//$(this).valid();
        	//$('#content_0_txtClubGUID').val($(this).val());
        //});
    }

    var setupCarousel = function () {


        var clickTitle = function () {
        
            if($('.carousel-modal.active').length === 0){
            
	            var $this = $(this), href = $this.attr('href');
	
	            $('#carouselCaptions li').removeClass('active');
	            $(this).parents('li').addClass('active');
	
	            $('#carouselImages li').stop(true, true).hide();
	            $(href).stop().fadeIn(500);
			
			}

            return false;
        };

        var carouselTimer = function () {
            var $nextSlide = $('#carouselCaptions li.active').next();

            if (!$nextSlide.length) {
                $nextSlide = $('#carouselCaptions li').first();
            }

            $nextSlide.find('h3 a').click();
        };


        return {
            init: function () {
                $('#carouselCaptions').on('click', 'h3 a', clickTitle);
                $('#carouselCaptions li h3 a').first().click();
                setInterval(carouselTimer, timeoutSpeed);
            },
            carouselTimer: carouselTimer
        };
    } ();

    var setupLightboxes = function () {
        var infoButtonClick = function () {
            var $this = $(this), href = $this.attr('href'), top = $('#carouselImages').position().top, left = $('#carouselImages').position().left;
            
			$this.parents('li').find('h3 a').click();
			 window.scrollTo(0,top-85)
			
			$(href).addClass('active');
            $('.overlay').fadeIn();
            $(href).css('top', top).css('left', left).fadeIn();
            $(href).prepend('<a href="#close" class="close">close</a>');
            $('.overlay, div.carousel-modal a.close').on('click', lightboxClose);
            return false
        };

        var lightboxClose = function () {
            $('.overlay,.carousel-modal').fadeOut().removeClass('active');
            $('.carousel-modal a.close').remove();


            return false;
        };

        return {
            init: function () {
                $('#carouselCaptions').on('click', 'a.more-info', infoButtonClick);
                $('body').append('<div class="overlay" style="display:none;">');
                $('.overlay').on('click', lightboxClose);
            }
        };
    } ();

    return {
        init: function () {
            setupCaptions();
            setupForms();
            setupLightboxes.init();
            setupCarousel.init();
        },
        form_validation: function(){
            var $this = $('#content_0_btnSubmit'), 
            valid = false, $enquirypanel = $('.enquirypanel'), 
            formType = $enquirypanel.attr('data-formtype'),
            gaqCategory = $enquirypanel.attr('data-gaqcategory'),
            gaqAction = $enquirypanel.attr('data-gaqaction'),
            gaqLabel = $enquirypanel.attr('data-gaqlabel');

            if($(this).hasClass('form-submitted') === false){
               valid = $('form').validate().form();
                if(valid){
                	
					//Google analytics
					if (typeof (_gaq) !== undefined) {
						if ("webleads" === formType) {
							//this is a web lead -load open tag for web lead confirmation e.g. mensfitness
							var ot = document.createElement('script');
							ot.async = true;
							ot.src = ('//d3c3cq33003psk.cloudfront.net/opentag-32825-68231.js');
							var s = document.getElementsByTagName('script')[0];
							s.parentNode.insertBefore(ot, s);
					
							
						}
						
						_gaq.push(["_trackEvent", gaqCategory, gaqAction, gaqLabel]);             	
                	}
                
                    $this.addClass('form-submitted'); 
                }
            }

        	return valid;
        }
    }
} ();

$(document).ready(function () {
    $.virginactive_campaign.module_enquiry.init();
});