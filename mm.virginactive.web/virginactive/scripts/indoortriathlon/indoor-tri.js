var indoorTry = function () {
    var privateProperties = {
        temp: "",
        lightbox_width: null
    }

	var isValidDate = function(d) {
	  if ( Object.prototype.toString.call(d) !== "[object Date]" )
	    return false;
	  return !isNaN(d.getTime());
	}

	//Lightbox used for photo gallery and videos, unfortunately is incompatible with .net forms.
    var lightbox = function () {
        var lightboxProperties = {
            overlayID: ""
        }

        var showLightbox = function () {
        	var $this = $(this);
            var linkedID = $this.attr('href');
			
			showLightboxCall(linkedID);
			
            
            return false;
        };
        
        var showLightboxCall = function(linkedID){
        	$('.indoor-va-overlay-prevent-clash').addClass('active');
        	
        	$('.indoor-va-overlay-prevent-clash .overlay_inners').html($(linkedID).html());
        	
        	
        	if(privateProperties.lightbox_width !== null){
        		$('.indoor-va-overlay-prevent-clash').css('width',privateProperties.lightbox_width);
        		privateProperties.lightbox_width = null;
        	};
        	
        	
        	if(!$('html').hasClass('touch')){
        	
        		//Reusing existing CSS so I can center the lightbox
        		var overlayHeight = parseInt($('.indoor-va-overlay-prevent-clash').height());
        		$('.indoor-va-overlay-prevent-clash').css('margin-top',- (overlayHeight / 2));
        		
        		//Reusing existing CSS so I can center the lightbox
        		var overlayWidth = parseInt($('.indoor-va-overlay-prevent-clash').width());
        		$('.indoor-va-overlay-prevent-clash').css('margin-left',- (overlayWidth / 2));
        	
        	}
        	else{
        		$('.indoor-va-overlay-prevent-clash').css('margin-top',"-150px");
        		$('.indoor-va-overlay-prevent-clash').css('margin-left',"-100px");
        	}
        	
        	
        	$('#toTop').fadeOut(300);
        	
        	//Show lightbox
        	$('.indoor-va-overlay-prevent-clash, #va-overlay-bg').fadeIn(300);
        };

        var closeLightbox = function (e) {
           
            $('.indoor-va-overlay-prevent-clash').removeClass('active');
			
			$('#toTop').fadeIn(300);
            $('.indoor-va-overlay-prevent-clash, #va-overlay-bg').fadeOut(300, function(){
            	$('#va-overlay .overlay_inners').html('');
            });
            
            return false;
        };

        return {
            init: function (triggerLink) {
                if ($('.indoor-va-overlay-prevent-clash').length === 0) {
                    $('body').append('<div id="va-overlay" class="va-overlay indoor-va-overlay indoor-va-overlay-prevent-clash"><a class="button-close" href="#">CLOSE<span>[X]</span></a><div id="xxva-overlay-wrap" class="va-overlay-indoor-tri">    <div id="overlay" class="overlay_inners"></div></div></div>');
                }
                
                if($('#va-overlay-bg').length === 0){
                	$('body').append('<div id="va-overlay-bg" style="display: block; opacity: 0.8; "></div>');
                }

                $('.indoor-va-overlay-prevent-clash, #va-overlay-bg').hide();

                $(triggerLink).click(showLightbox);
                $('.indoor-va-overlay-prevent-clash .button-close,#va-overlay-bg').click(closeLightbox);
            },
            jsTrigger: showLightboxCall
        }

    } ();
    
    
    var netLightbox = function(){
    	
    	var showLightbox = function () {
    	
	    	if(!$('html').hasClass('touch')){
	    	
	    		//Reusing existing CSS so I can center the lightbox
	    		var overlayHeight = parseInt($('.indoor-va-overlay-form').height());
	    		$('.indoor-va-overlay-form').css('margin-top',- (overlayHeight / 2));
	    		
	    		//Reusing existing CSS so I can center the lightbox
	    		var overlayWidth = parseInt($('.indoor-va-overlay-form').width());
	    		$('.indoor-va-overlay-form').css('margin-left',- (overlayWidth / 2));
	    	
	    	}
	    	else{
	    		$('.indoor-va-overlay-form').css('margin-top',"-150px");
	    		$('.indoor-va-overlay-form').css('margin-left',"-100px");
	    	}
	    	
        	$('#toTop').fadeOut(300);
        	$('.indoor-va-overlay-form, #va-overlay-bg').fadeIn(300);
            
            return false;
        };

        var closeLightbox = function (e) {
            $('.indoor-va-overlay-form').removeClass('active');

			$('#toTop').fadeIn(300);
            $('.indoor-va-overlay-form, #va-overlay-bg').fadeOut(300);
            
            return false;
        };
    	
    	return{
    		init:function(element){
    			$('.indoor-va-overlay-form').removeClass('hidden').css('visibility','visible').hide().css('z-index','9999');
    			
    			$(element).click(showLightbox);
    			$('.indoor-va-overlay-form .button-close,#va-overlay-bg').click(closeLightbox);
    		}
    	}	
    }();

	var vaValidator = function(){
		var expressions = {
			"email": /^([0-9a-zA-Z"](["-.+\w]*[0-9a-zA-Z"])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.?)+[a-zA-Z]{1,9})$/g
		}
		
		var validEmail = function(email){
			if(email.match(expressions.email)){
				return true;
			}
			else {
				return false;
			}
		};
		
		return{
			init: function(element){
				$(element).on("click",function(){
					var valid = true;
				
					$(this).removeClass('validForm');
				
					$email_field = $('.input-email');
					
					if($email_field.length){
						$email_field.each(function(index) {
						    var $this = $(this);
						    
						    if(!validEmail($this.val())){
						    	valid = false;
						    	$(this).parent().find('.error-msg').remove();
						    	$(this).parent().addClass('error').append('<div class="error-msg"><p>Please enter a valid email address</p></div>');
						    }
						    else {
						        $(this).parent().removeClass('error').find('error-msg').remove();
						    }

						});
					}
					
					if(valid){
						$(this).addClass('validForm');
						$(this).off();
						$(this).on("click", function(){
							return false;
						});
					}
					
					return valid;
				
				});
			}
		}
		
	}();

    var setupCarousel = function (containerID, showPager, itemsPerPage, activeItemElementType) {

        var $container = $(containerID);
        var carouselItems = $(containerID).find('.carousel ul li');
        var itemCount = carouselItems.length;
        var itemWidth = carouselItems.width();
        var carouselWidth = itemWidth * itemCount;
        var currentItem = 1;

		if(showPager){
			$container.append('<div class="nav-prev-contain"><a class="navPrev" href="#">Previous</a><p class="position">'+ '<strong>'+ (((currentItem * itemsPerPage) - itemsPerPage) + 1) +'</strong> to <strong>'+(currentItem * itemsPerPage)+'</strong> of <strong>'+(itemCount * itemsPerPage)+'</strong>'+'</p><a class="navNext" href="#">Next</a></div>');	
		}
		else{
        	$container.append('<div class="nav-prev-contain"><a class="navPrev" href="#">Previous</a> <a class="navNext" href="#">Next</a></div>');
		}
		
		if(activeItemElementType !== ''){
			carouselItems.eq(0).find('img').eq(0).addClass('active');
		}
		
        $container.find('.carousel ul').css('width', carouselWidth);
		
		$container.attr('data-currentItem', currentItem).attr('data-activeItemElementType',activeItemElementType).attr('data-itemsPerPage',itemsPerPage);
		
		if(itemsPerPage > 1){
			$container.attr('data-activeItem', 1)
		}
	
        $container.on("click", "a", function () {
            var $this = $(this);
            var speed = 300;

            if ($this.hasClass('navPrev')) {
            	currentItem = $container.attr('data-currentItem')
                if (currentItem > 1) {
                	
                    currentItem--;
                    $container.attr('data-currentItem', currentItem);
                    
                    if(showPager){
                    	var innerHTML = '<strong>'+ (((currentItem * itemsPerPage) - itemsPerPage) + 1) +'</strong> to <strong>'+(currentItem * itemsPerPage)+'</strong> of <strong>'+(itemCount * itemsPerPage)+'</strong>';
                    	$container.find('p.position').html(innerHTML);
                    }

                    if (currentItem === 1) {
                        $(containerID).find('.navPrev').addClass('disabled');
                    }

                    if (currentItem < itemCount) {
                        $(containerID).find('.navNext').removeClass('disabled');
                    }

                    var moveString = '+=' + itemWidth;

                    $this.parents('.carousel_container').find('ul').animate({ left: moveString }, speed);
                }
            }
            else if ($this.hasClass('navNext')) {
            	currentItem = $container.attr('data-currentItem');
                if (currentItem < itemCount) {
                    currentItem++;
                    $container.attr('data-currentItem', currentItem);
                    
                    
                    if(showPager){
                        var innerHTML = '<strong>' + (((currentItem * itemsPerPage) - itemsPerPage) + 1) + '</strong> to <strong>' + (currentItem * itemsPerPage) + '</strong> of <strong>' + (itemCount * itemsPerPage) + '</strong>';
                    	$container.find('p.position').html(innerHTML);
                    }

                    if (currentItem > 1) {
                        $this.parents('.carousel_container').find('.navPrev').removeClass('disabled');
                    }

                    if (currentItem === itemCount) {
                        $this.parents('.carousel_container').find('.navNext').addClass('disabled');
                    }

                    var moveString = '-=' + itemWidth;

                    $this.parents('.carousel_container').find('ul').animate({ left: moveString }, speed);
                }
            }

            return false;
        });

        $container.find('.navPrev').addClass('disabled');
    };
    
    
    //Add a separate controller to the main carousel
    var carouselController = function(){
    	var privateProperties = {
    	};
    	
    	var moveSteps = function(){
    		
    	};
    	
    	var mainCarouselNavigationArrows = function(){
    		var $this = $(this);
    		var $thisCarousel = $(this).parents('.carousel_container');
    		var $controllerCarousel = $($thisCarousel.attr('data-controller'));
    		
    		var thisCarouselItemPerPage = parseInt($thisCarousel.attr('data-itemsperpage'));
    		var thisCarouselCurrentItem = parseInt($thisCarousel.attr('data-currentitem'));
    		
    		var controllerCarouselItemPerPage = parseInt($controllerCarousel.attr('data-itemsperpage'));
    		var controllerCarouselCurrentItem = parseInt($controllerCarousel.attr('data-currentitem'));    		
    		var controllerCarouseActiveElement = $controllerCarousel.attr('data-activeitemelementtype');
    		
    		
    		if ($this.hasClass('navPrev')) {
    			if(controllerCarouselCurrentItem < controllerCarouselItemPerPage){
    				$controllerCarousel.find('.carousel ul .active').removeClass('active');
    				
    				var itemInVisibleItem = (thisCarouselCurrentItem - 1 - (controllerCarouselItemPerPage * (controllerCarouselCurrentItem - 1)));
    				
    				if(itemInVisibleItem === -1){
    					controllerCarouselCurrentItem--;
    					itemInVisibleItem = 6;
    					$controllerCarousel.find('.navPrev').click();
    				}
    				
    				$controllerCarousel.find('.carousel ul li').eq((controllerCarouselCurrentItem - 1)).find('img').eq(itemInVisibleItem).addClass('active');
    				$controllerCarousel.attr('data-activeitem',thisCarouselCurrentItem);
    				
    				
    			}
    		}
    		else if ($this.hasClass('navNext')) {
    			if(controllerCarouselCurrentItem < controllerCarouselItemPerPage){
    				$controllerCarousel.find('.carousel ul .active').removeClass('active');
    				
    				
    				var itemInVisibleItem = (thisCarouselCurrentItem - 1 - (controllerCarouselItemPerPage * (controllerCarouselCurrentItem - 1)));
    				
    				if(itemInVisibleItem === controllerCarouselItemPerPage){
    					controllerCarouselCurrentItem++;
    					itemInVisibleItem = 0;
    					$controllerCarousel.find('.navNext').click();
    				}
    				
    				$controllerCarousel.find('.carousel ul li').eq((controllerCarouselCurrentItem - 1)).find('img').eq(itemInVisibleItem).addClass('active');
    				$controllerCarousel.attr('data-activeitem',thisCarouselCurrentItem);
    				
    				//.addClass('active');
    				
    			}
    		}
    	};
    	
    	var controllerCarouselNavigationArrows = function(){
    		var $this = $(this);
    		var $thisCarousel = $(this).parents('.carousel_container');
    		var $childCarousel = $($thisCarousel.attr('data-carousel'));
    		
    		var childCarouselItemPerPage = parseInt($childCarousel.attr('data-itemsperpage'));
    		var childCarouselCurrentItem = parseInt($childCarousel.attr('data-currentitem'));
    		
    		var thisCarouselItemPerPage = parseInt($thisCarousel.attr('data-itemsperpage'));
    		var thisCarouselCurrentItem = parseInt($thisCarousel.attr('data-currentitem'));    		
    		var thisCarouseActiveElement = $thisCarousel.attr('data-activeitemelementtype');
    		
    		var activeitemelementtype = $thisCarousel.attr('data-activeitemelementtype');
    		
    		
    		if ($this.hasClass('navPrev')) {
    			if(thisCarouselCurrentItem !== 1){
	    			$thisCarousel.find(activeitemelementtype).removeClass('active');
	    			$thisCarousel.find('li').eq(thisCarouselCurrentItem - 2).find('a').eq(6).click();
    			}
    		}
    		else if ($this.hasClass('navNext')) {
    			if(thisCarouselCurrentItem !== 3){
	    			$thisCarousel.find(activeitemelementtype).removeClass('active');
	    			$thisCarousel.find('li').eq(thisCarouselCurrentItem).find('a').first().click();
	    		}
    		}
    		
    		//parentCurrentItem
    	};
    	
    	var thumbnailClick = function(){
    		var $this = $(this);
    		var $controllerContainer = $(this).parents('.carousel_container');
    		var activeitemelementtype = $controllerContainer.attr('data-activeitemelementtype');
    	
    		var toShowHref = $this.attr('href');
	    	var $parentObj = $this.parents('.carousel')
	    	var $mainCar = $($controllerContainer.attr('data-carousel'));
	    	 
	    	var currentItem = parseInt($mainCar.attr('data-currentitem'))
	    	var $carouselLi = $mainCar.find('.carousel ul li');
	    	var stepWidth = $carouselLi.width();
	    	var selected = 0;
	    	
	    	$parentObj.find(activeitemelementtype).removeClass('active')
	    	$this.find(activeitemelementtype).addClass('active');
	    	
	    	toShowHref = toShowHref.split('#')[1];
	    	 
	    	$carouselLi.each(function(index) {
	    	    if(toShowHref === $(this).attr('id')){
	    	    	selected = index
	    	   		return false;
	    	    }
	    	});
	    	
	    	if(selected === 0){
	    		$mainCar.find('.navPrev').addClass('disabled');
	    	}
	    	else{
	    		$mainCar.find('.navPrev').removeClass('disabled');
	    	}
	    	
	    	if(selected === ($controllerContainer.find('li a').length - 1)){
	    		$mainCar.find('.navNext').addClass('disabled');
	    	}
	    	else{
	    		$mainCar.find('.navNext').removeClass('disabled');
	    	}
	    	
	    	var move = selected - (currentItem - 1);
	    	var stepTotal = stepWidth * Math.abs(move);
	    	 
	    	if(move > 0){
	    		var moveString = '-=' + stepTotal;
	    		$mainCar.find('ul').animate({ left: moveString }, 600);
	    	}
	    	else if (move < 0){
	    		var moveString = '+=' + stepTotal;
	    		$mainCar.find('ul').animate({ left: moveString }, 600);
	    	}
	    	 
	    	currentItem = currentItem + move;
	    	$mainCar.attr('data-currentItem', currentItem);
	    	
	    	return false;
    	
    	};
    	
    	return {
    		init: function(element, linked_carousel){
    			var $carouselThumbs = $(element).find('.carousel');
    			var $arrows = $(element).find('.nav-prev-contain');
    			var $linked_carousel = $(linked_carousel);
    		
    			$(linked_carousel).attr('data-controller', element)
    		
    			$(element).attr('data-carousel', linked_carousel)
    			
    			$linked_carousel.on("click", "a", mainCarouselNavigationArrows);
    			
    			$arrows.on("click","a",controllerCarouselNavigationArrows);
    			
    			$carouselThumbs.on("click", "a", thumbnailClick);
    		}
    	}
    }();

	var setupValidation = function(){
		return{
			init: function () {
				jQuery.validator.addMethod("validateGroup",function(value, element, arr) {
					var valid = true;
				    return valid;
				}, "The group is invalid");
				
				jQuery.validator.addMethod("validateDate",function(value, element, settings) {
				    var day = parseInt($('[name="'+settings.day+'"]').val(),10), month = parseInt($('[name="'+settings.month+'"]').val(),10) -1, year = parseInt($('[name="'+settings.year+'"]').val(),10), dob = null, valid=true;
				    
				    if(day > 0 && year > 0 && month > -1){
				    	dob = new Date(year, month, day), valid= true;
				    	
				    	if(!isValidDate(dob)){
				    		valid = false;
				    	}
				    }
				    
				    
				    return valid;
				}, "Please insert a valid date");
				
				jQuery.validator.addMethod("checkAgainstDate",function(value, element, settings) {
				    var selected_date = $('[name="content_0$centre_0$drpRaceDate"]').val(), leap = 0;
				    selected_date = selected_date.split('/');
				    selected_date = new Date(selected_date['2'],selected_date['1']-1,selected_date['0']);
				    
				    var day = parseInt($('[name="'+settings.day+'"]').val(),10), month = parseInt($('[name="'+settings.month+'"]').val(),10) -1, year = parseInt($('[name="'+settings.year+'"]').val(),10), dob = null,valid=true;
				    
				    if(day > 0 && year > 0 && month > -1){
				    	dob = new Date(year, month, day), valid= true;
				    	
				    	if(!isValidDate(dob)){
				    		valid = false;
				    	}
				    
				    	if($('[name="content_0$centre_0$RaceType"]:checked').val() === "radRace2"){
				    		age = 16;
				    		leap = 5;
				    	}
				    	else{
				    		age = 10
				    		leap = 4;
				    	}
				    
					    msPerDay = 24 * 60 * 60 * 1000 
					    difference = (Math.floor((selected_date-dob)/ msPerDay) + 1 )- leap;
					    
					   if(difference < (age * 365)){
					   		valid = false;
					   }
					   else if(difference < (13 * 365)){
					   		$('[name="content_0$centre_0$chkParentalConsent"]').parents('.form_row').show();
					   }
					   else{
					   		$('[name="content_0$centre_0$chkParentalConsent"]').parents('.form_row').hide();
					   }
				   
				   }
				    				    
				    return valid;
				}, "Please insert a valid date");
				
			
				$('form').validate({
					onsubmit: false,
					onclick: false,
					onkeyup:false,
					errorElement: 'p',
					wrapper: 'span class="error-msg"',
		        	groups: {
		        		member1_name: "content_0$centre_0$txtFirstName content_0$centre_0$txtSurname",
		        		member1_address: "content_0$centre_0$txtAddressLine1 content_0$centre_0$txtAddressLine3 content_0$centre_0$txtPostcode",
		        		member1_dob: "content_0$centre_0$txtDOBDay content_0$centre_0$txtDOBMonth content_0$centre_0$txtDOBYear",
		        		member2_name: "content_0$centre_0$txtFirstName2 content_0$centre_0$txtSurname2",
		        		member2_dob: "content_0$centre_0$txtDOBDay2 content_0$centre_0$txtDOBMonth2 content_0$centre_0$txtDOBYear2",
		        		emergency_contact_name: "content_0$centre_0$txtNOKFirstName content_0$centre_0$txtNOKSurname"
		        	},rules: {
		        		content_0$centre_0$clubFindSelect: {
		        			required: true,
		        			notEqual: 'Select a club'
		        		},
		        		content_0$centre_0$drpRaceDate: {
		        			required: true,
		        			notEqual: 'Select a date'
		        		},
		        		content_0$centre_0$drpTimeSlot: {
		        			required: true,
		        			notEqual: 'Select a time slot'
		        		},
		        		content_0$centre_0$RaceType: "required",
		        		content_0$centre_0$RaceTime: "required",
		        		content_0$centre_0$TeamType: "required",
		        		content_0$centre_0$txtDOBDay: {
		        			required: true,
		        			number: true,
		        			isLessThan: 32,
		        			validateGroup: ['content_0$centre_0$txtDOBMonth', 'content_0$centre_0$txtDOBDay', 'content_0$centre_0$txtDOBYear'],
		        			validateDate: {
		        				year: "content_0$centre_0$txtDOBYear",
		        				month: "content_0$centre_0$txtDOBMonth",
		        				day: "content_0$centre_0$txtDOBDay"
		        			},
		        			checkAgainstDate: {
		        				year: "content_0$centre_0$txtDOBYear",
		        				month: "content_0$centre_0$txtDOBMonth",
		        				day: "content_0$centre_0$txtDOBDay",
		        			}
		        		},
		        		content_0$centre_0$txtDOBMonth: {
		        			required: true,
		        			number: true,
		        			isLessThan: 13,
		        			validateGroup: ['content_0$centre_0$txtDOBMonth', 'content_0$centre_0$txtDOBDay', 'content_0$centre_0$txtDOBYear'],
		        			validateDate: {
		        				year: "content_0$centre_0$txtDOBYear",
		        				month: "content_0$centre_0$txtDOBMonth",
		        				day: "content_0$centre_0$txtDOBDay"
		        			},
		        			checkAgainstDate: {
		        				year: "content_0$centre_0$txtDOBYear",
		        				month: "content_0$centre_0$txtDOBMonth",
		        				day: "content_0$centre_0$txtDOBDay"
		        			}
		        		},
		        		content_0$centre_0$txtDOBYear: {
		        			required: true,
		        			number: true,
		        			isLessThan: 2012,
		        			isGreaterThan: 1900,
		        			validateGroup: ['content_0$centre_0$txtDOBMonth', 'content_0$centre_0$txtDOBDay', 'content_0$centre_0$txtDOBYear'],
		        			validateDate: {
		        				year: "content_0$centre_0$txtDOBYear",
		        				month: "content_0$centre_0$txtDOBMonth",
		        				day: "content_0$centre_0$txtDOBDay"
		        			},
		        			checkAgainstDate: {
		        				year: "content_0$centre_0$txtDOBYear",
		        				month: "content_0$centre_0$txtDOBMonth",
		        				day: "content_0$centre_0$txtDOBDay"
		        			}
		        		},
		        		content_0$centre_0$VAStatus: "required",
		        		content_0$centre_0$txtFirstName: {
		        			required: true,
		        			notEqual: 'First',
		        			validateGroup: ['content_0$centre_0$txtFirstName', 'content_0$centre_0$txtSurname']
		        		},
		        		content_0$centre_0$txtSurname: {
		        			required: true,
		        			notEqual: 'Last',
		        			validateGroup: ['content_0$centre_0$txtFirstName', 'content_0$centre_0$txtSurname']
		        		},
		        		content_0$centre_0$Sex: "required",
		        		content_0$centre_0$txtAddressLine1: {
		        			required: true,
		        			notEqual: 'Address Line 1',
		        			validateGroup: ['content_0$centre_0$txtAddressLine1', 'content_0$centre_0$txtAddressLine3', 'content_0$centre_0$txtPostcode']
		        		},
		        		content_0$centre_0$txtAddressLine3: {
		        			required: true,
		        			notEqual: 'Town/City',
		        			validateGroup: ['content_0$centre_0$txtAddressLine1', 'content_0$centre_0$txtAddressLine3', 'content_0$centre_0$txtPostcode']
		        		},
		        		content_0$centre_0$txtPostcode: {
		        			required: true,
		        			notEqual: 'Post Code',
		        			regex: "^[a-zA-Z0-9 ]{5,9}$"
		        		},
		        		content_0$centre_0$txtPhoneNumber: {
		        			required: true,
		        			phoneUK: true
		        		},
		        		content_0$centre_0$txtEmail: {
		        			required: true,
		        			email: true
		        		},
		        		content_0$centre_0$txtDOBDay2: {
		        			required: true,
		        			number: true,
		        			isLessThan: 32,
		        			validateGroup: ['content_0$centre_0$txtDOBDay2', 'content_0$centre_0$txtDOBMonth2', 'content_0$centre_0$txtDOBYear2']
		        		},
		        		content_0$centre_0$txtDOBMonth2: {
		        			required: true,
		        			number: true,
		        			isLessThan: 13,
		        			validateGroup: ['content_0$centre_0$txtDOBDay2', 'content_0$centre_0$txtDOBMonth2', 'content_0$centre_0$txtDOBYear2']
		        		},
		        		content_0$centre_0$txtDOBYear2: {
		        			required: true,
		        			number: true,
		        			isLessThan: 2012,
		        			isGreaterThan: 1900,
		        			validateGroup: ['content_0$centre_0$txtDOBDay2', 'content_0$centre_0$txtDOBMonth2', 'content_0$centre_0$txtDOBYear2']
		        		},
		        		content_0$centre_0$txtFirstName2: {
		        			required: true,
		        			notEqual: 'First',
		        			validateGroup: ['content_0$centre_0$txtFirstName2', 'content_0$centre_0$txtSurname2']
		        		},
		        		content_0$centre_0$txtSurname2: {
		        			required: true,
		        			notEqual: 'Last',
		        			validateGroup: ['content_0$centre_0$txtFirstName2', 'content_0$centre_0$txtSurname2']
		        		},
		        		content_0$centre_0$txtTeamName: "required",
		        		content_0$centre_0$Sex2nd: "required",
		        		content_0$centre_0$txtNOKFirstName: {
		        			required: true,
		        			notEqual: 'First',
		        			validateGroup: ['content_0$centre_0$txtNOKFirstName', 'content_0$centre_0$txtNOKSurname']
		        		},
		        		content_0$centre_0$txtNOKSurname: {
		        			required: true,
		        			notEqual: 'Last',
		        			validateGroup: ['content_0$centre_0$txtNOKFirstName', 'content_0$centre_0$txtNOKSurname']
		        		},
		        		content_0$centre_0$txtNOKRelationship: "required",
		        		content_0$centre_0$txtNOKPhoneNumber: {
		        			required: true,
		        			phoneUK: true
		        		},
		        		content_0$centre_0$chkTermsAndConditions: "required",
		        		content_0$centre_0$chkParentalConsent: "required"
		         	},
        		   messages: {
        		     content_0$centre_0$clubFindSelect: "Please select a club",
        		     content_0$centre_0$drpRaceDate: "Please select a race date",
        		     content_0$centre_0$RaceType: "Please select a race type",
        		     content_0$centre_0$RaceTime: "Please select a race time",
        		     content_0$centre_0$drpTimeSlot: "Please select a time slot",
        		     content_0$centre_0$txtFirstName: "Please provide both your first and last name",
        		     content_0$centre_0$txtSurname: "Please provide both your first and last name",
        		     content_0$centre_0$Sex: "Please select your gender",
        		     content_0$centre_0$txtPhoneNumber: "Please provide your phone number",
        		     content_0$centre_0$txtEmail: {
        		     	required: "Please provide your email address",
        		     	email: "Please enter a valid email address"
        		     },
        		     content_0$centre_0$txtAddressLine1: "Please provide your full address",
        		     content_0$centre_0$txtAddressLine3: "Please provide your full address",
        		     content_0$centre_0$txtPostcode: "Please provide your full address",
        		     content_0$centre_0$txtDOBDay: {
        		     	required: "Please enter your date of birth",
        		     	checkAgainstDate: "You are not old enough to enter this event",
        		     	validateDate: "Please enter a valid date",
        		     	validateGroup: "Please enter your date of birth",
        		     	number: "Please enter a valid date",
        		     	isLessThan: "Please enter a valid date"
        		     },
        		     content_0$centre_0$txtDOBMonth: {
        		     	required: "Please enter your date of birth",
        		     	checkAgainstDate: "You are not old enough to enter this event",
        		     	validateDate: "Please enter a valid date",
        		     	validateGroup: "Please enter your date of birth",
        		     	number: "Please enter a valid date",
        		     	isLessThan: "Please enter a valid date"
        		     },
        		     content_0$centre_0$txtDOBYear: {
        		     	required: "Please enter your date of birth",
        		     	checkAgainstDate: "You are not old enough to enter this event",
        		     	validateDate: "Please enter a valid date",
        		     	validateGroup: "Please enter your date of birth",
        		     	number: "Please enter a valid date",
        		     	isLessThan: "Please enter a valid date",
        		     	isGreaterThan: "Please enter a valid date"
        		     },
        		     content_0$centre_0$VAStatus: "Please select your Virgin Active status",
        		     content_0$centre_0$TeamType: "Are you entering as an individual or as a team?",
        		     content_0$centre_0$txtDOBDay2: "Please enter your team members date of birth",
        		     content_0$centre_0$txtDOBMonth2: "Please enter your team members date of birth",
        		     content_0$centre_0$txtDOBYear2: "Please enter your team members date of birth",
        		     content_0$centre_0$txtFirstName2: "Please enter your team members name",
        		     content_0$centre_0$txtSurname2: "Please enter your team members name",
        		     content_0$centre_0$txtTeamName: "Please enter your team name",
        		     content_0$centre_0$Sex2nd: "Please enter your team members gender",
        		     content_0$centre_0$txtNOKFirstName: "Please provide your emergency contacts name",
        		     content_0$centre_0$txtNOKSurname: "Please provide your emergency contacts name",
        		     content_0$centre_0$txtNOKRelationship: "Please provide your relationship to your emergency contact",
        		     content_0$centre_0$txtNOKPhoneNumber: "Please provide your emergency contact's phone number",
        		     content_0$centre_0$chkTermsAndConditions: "Please agree to the terms and conditions",
        		     content_0$centre_0$chkParentalConsent: "Please confirm you agree with this condition"
        		   },
	        		
					// the errorPlacement has to take the table layout into account 
					errorPlacement: function(error, element) {
						element.parents('.form_row').addClass('error');
						error.appendTo( element.parents('.form_row'));
					},
					 highlight: function(element, errorClass) {
					 	var $element = $(element);
					 	if($element.hasClass('isvalid')){
					 		$element.removeClass('isvalid').removeClass('error');
					 	}
					 	else{
					 		$element.addClass('error')	
					 	}
					 	
					 },
					success: function(element){
		                var $wrap = element.parents('.form_row'),frmElement = null, replacement_error = "";
		                
		                if($wrap.find('.chzn-container').length){
		                    frmElement = $wrap.find('.chzn-container');
		                }
		                else{
		                    frmElement = $wrap.find('.form-element');
		                }
		                
		                $wrap.removeClass('error');
		                
		                	$wrap.find('span.valid, .error-msg').remove();
		                
		                
		                //$("<span class='valid'>valid</span>").insertAfter(frmElement);   
		                //$('span.valid').not(":first").remove();
					}
		        });
			
				$('ul.radio').on('click','input',function(){
					$(this).valid();
				});
				
				if (($('html').hasClass("touch")) && (!navigator.userAgent.match(/iPad/i))) {
				   $('select').change(function() {
				   	
				   	var $this = $(this), $wrap = $this.parents('.form_row'),selectedValue = $(this).find('option:selected').val();
				   	$wrap.find('select').val(selectedValue).valid();
				   });
				}
				else{
					$('select').chosen().change(function() {
						
						var $this = $(this), $wrap = $this.parents('.form_row'),selectedValue = $(this).find('option:selected').val();
						$wrap.find('select').val(selectedValue).valid();
					});
				}
				
				
			}
		}
	}();
	
	var formPostBackHandler = function(){
		return{
			form1: function(){
				var valid = true;
			
				if($('[name="content_0$centre_0$clubFindSelect"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$drpRaceDate"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$RaceType"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$RaceTime"]').valid() === 0){
					valid = false;
				}
				
				if($('#content_0_centre_0_pnlStep1').find('input.btn').hasClass('btn-disabled-xl')){
					valid = false
				}
				
				if(valid){
					$('#content_0_centre_0_pnlStep1').find('input.btn').addClass('btn-disabled-xl');
				}
			
				return valid;
			},
			form2: function(){
				var valid = true;
			
				if($('[name="content_0$centre_0$drpTimeSlot"]').valid() === 0){
					valid = false;
				}
				
				if($('#content_0_centre_0_pnlStep2').find('input.btn').not('.btn-restart').hasClass('btn-disabled-xl')){
					valid = false
				}
				
				if(valid){
					$('#content_0_centre_0_pnlStep2').find('input.btn').not('.btn-restart').addClass('btn-disabled-xl');
				}
			
				return valid;
			},
			form3: function(){
				var valid = true;
				
				if($('[name="content_0$centre_0$TeamType"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$VAStatus"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$txtFirstName"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$txtSurname"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$Sex"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$txtAddressLine1"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$txtAddressLine3"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$txtPostcode"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$txtPhoneNumber"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$txtEmail"]').valid() === 0){
					valid = false;
				}
							
				if($('[name="content_0$centre_0$txtDOBDay"]').valid() === 0){
					valid = false;
				}
				
				
				if($('[name="content_0$centre_0$txtDOBMonth"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$txtDOBYear"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$TeamType"]:checked').val() === "Team"){
					if($('[name="content_0$centre_0$txtDOBDay2"]').valid() === 0){
						valid = false;
					}
					
					if($('[name="content_0$centre_0$txtDOBMonth2"]').valid() === 0){
						valid = false;
					}
					
					if($('[name="content_0$centre_0$txtDOBYear2"]').valid() === 0){
						valid = false;
					}
				
					if($('[name="content_0$centre_0$txtFirstName2"]').valid() === 0){
						valid = false;
					}
					
					if($('[name="content_0$centre_0$txtSurname2"]').valid() === 0){
						valid = false;
					}
					
					if($('[name="content_0$centre_0$txtTeamName"]').valid() === 0){
						valid = false;
					}
					
					if($('[name="content_0$centre_0$Sex2nd"]').valid() === 0){
						valid = false;
					}
				}
				
				if($('[name="content_0$centre_0$txtNOKFirstName"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$txtNOKSurname"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$txtNOKRelationship"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$txtNOKPhoneNumber"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$chkTermsAndConditions"]').valid() === 0){
					valid = false;
				}
				
				if($('[name="content_0$centre_0$chkParentalConsent"]').parent('.form_row').css('display') == "block"){
					if($('[name="content_0$centre_0$chkParentalConsent"]').valid() === 0){
						valid = false;
					}
				}
				
				if(valid == false){
					indoorTry.scrollToBox.doit('#content_0_centre_0_pnlStep3 .error');
				}
				
				if($('#content_0_centre_0_pnlStep6').find('input.btn').not('.btn-restart').hasClass('btn-disabled-xl')){
					valid = false
				}
				
				if(valid){
					$('#content_0_centre_0_pnlStep6').find('input.btn').not('.btn-restart').addClass('btn-disabled-xl');
				}
				
			
				return valid;
			}
		}
	}();
	
	var formAfterPostBackHandler = function(){
		var disableForm1 = function(){
			$('#content_0_centre_0_pnlStep1').append('<div class="disabled" style=""></div>');
			$('#content_0_centre_0_pnlStep1').find('.disabled').css('height',$('#content_0_centre_0_pnlStep1').height());
		};
		
		var disableForm2 = function(){
			$('#content_0_centre_0_pnlStep2').append('<div class="disabled" style=""></div>');
			$('#content_0_centre_0_pnlStep2').find('.disabled').css('height',$('#content_0_centre_0_pnlStep2').height());
			setupExpireCountdown.init();
		};
		
		var groupToggle = function(){
			if($('[name="content_0$centre_0$TeamType"]:checked').val() === "Team"){
				$('#content_0_centre_0_pnlStep4').removeClass('hidden');
			}
			else{
				$('#content_0_centre_0_pnlStep4').addClass('hidden');
			}
		};
		
		return{
			form0: function(){
				indoorTry.scrollToBox.doit('#content_0_centre_0_pnlStep1');
				
				placeholderFix();
			},
			form1: function(){
				disableForm1();
				
				indoorTry.scrollToBox.doit('#content_0_centre_0_pnlStep2');
				
				$('[name="content_0$centre_0$drpTimeSlot"]').on('change',function(){
					$(this).parents('.form_row').find('.error-msg.dotnet').remove();
				});
				placeholderFix();
			},
			form2: function(){
				$('[name="content_0$centre_0$chkParentalConsent"]').parents('.form_row').hide();
				disableForm2();
				disableForm1();
				groupToggle();
				
				if($('[name="content_0$centre_0$txtDOBDay"]').val() !== "DD" && $('[name="content_0$centre_0$txtDOBDay"]').val() !== "" && $('[name="content_0$centre_0$txtDOBMonth"]').val() !== "MM" && $('[name="content_0$centre_0$txtDOBMonth"]').val() !== "" && $('[name="content_0$centre_0$txtDOBYear"]').val() !== "YYYY" && $('[name="content_0$centre_0$txtDOBYear"]').val() !== ""){
					$('[name="content_0$centre_0$txtDOBYear"]').valid()
				}
				
				if($('#content_0_centre_0_pnlStep6 .full_width_error').css('display') === "none" || $('#content_0_centre_0_pnlStep6 .full_width_error').length === 0){
					indoorTry.scrollToBox.doit('#content_0_centre_0_timeWarning');
				}
							
				$('[name="content_0$centre_0$TeamType"]').on('change',groupToggle);
				
				$.focusGroup('.form_row',function($group){}, function($group){
					
					$group.find('input,select').each(function(){
						$(this).valid();
					})
					if($group.find('input.error').length){
						$group.find('input.error').valid()
					}
					
				})
				
				$('ul.radio').on('click','input',function(){
					$(this).valid();
				});
				
				$('.form_row.checkbox').on('click','input',function(){
					$(this).valid();
				});
				
				placeholderFix();
			},
			reinit_basics: function(){
				
			},
			confirmation: function(){
			
				window.scrollTo(0,$('#content_0_centre_0_pnlConfirmation').position().top - 20);
			}
		}
	}();
	
	var setupTooltips = function(){
		return {
			init: function() {
				var $indoortri = $('.indoor-tri');
				
				$indoortri.off();
				$indoortri.on('mouseenter', 'a.regtip',function(){
					var $this = $(this);
					$this.find('.infoBox').stop().show()
				}).on('mouseleave', 'a.regtip',function(){
					var $this = $(this);
					$this.find('.infoBox').stop().hide()
				}).on('click','a.regtip',function(){
					return false
				});
			}
		}
	}();
	
	var scrollToBox = function(target_id){
		var $target = null, scrollTop = 0, top = 0, target_top = 0, direction = "down";
		
		var doMove = function(){
			if (document.body.scrollTop > 0) {
	            scrollTop = document.body.scrollTop;
	        }
	        else if (document.documentElement.scrollTop > 0) {
	            scrollTop = document.documentElement.scrollTop;
	        }
	       
	       if(direction === "up"){
		       	if (top === scrollTop && top <= target_top) {
		            top = top + 15;
		            window.scrollTo(0, top);
		
		            setTimeout(function () {
		                doMove();
		            }, 5);
		        }
		        else{
		        	window.scrollTo(0,target_top);
		        }
	        }
	        else{
	        	if (top === scrollTop && top >= target_top) {
    	            top = top - 30;
    	            window.scrollTo(0, top);
    	
    	            setTimeout(function () {
    	                doMove();
    	            }, 5);
    	        }
    	        else{
    	        	window.scrollTo(0,target_top);
    	        }
	        }
        };
        
        return{
        	doit: function(target_id){
        		if (document.body.scrollTop > 0) {
        			scrollTop = top = document.body.scrollTop;
        		}
        		else if (document.documentElement.scrollTop > 0) {
        		    scrollTop = top = document.documentElement.scrollTop;
        		}
        	
        		$target = $(target_id).eq(0);
        		if($target.length){
	        		target_top = $target.offset().top - 20;
	        		
	        		if(top >  target_top){
	        			direction = "down"
	        		}
	        		else{
	        			direction = "up"
	        		}
        		
        			doMove();
        		}
        	}
        }
        
	}();

	var setupExpireCountdown = function(){
		var pageExpires = function(){
			indoorTry.lightbox.jsTrigger('#page_expiry_overlay');
		};
	
		return{
			init: function() {
				indoorTry.lightbox.init('');
				privateProperties.lightbox_width = 540;
				setTimeout(pageExpires, 1800000);
			}
		}
	}();

    return {
        init: function () {
            setupCarousel('.testimonials',false,0,'');
           	setupTooltips.init();
           	placeholderFix();
           	
           // if($('.btn-video').length){
	            indoorTry.lightbox.init('.btn-video');
	            indoorTry.lightbox.init('#btn-photo');
       		//}else{
       			//netLightbox.init('#btn-register, .footer-cta .btn-cta-big');
       		//}
            
            vaValidator.init('.btn-submit');
            
            if(typeof Sys !== 'undefined'){
            
	            var prm = Sys.WebForms.PageRequestManager.getInstance();
	            
	            prm.add_endRequest(function() {
	               $('select').chosen();
	               
	               setupTooltips.init();
	               
	               if($('#content_0_centre_0_pnlStep3').hasClass('hidden') === false){
	               		formAfterPostBackHandler.form2();
	               }
	               else if($('#content_0_centre_0_pnlStep2').hasClass('hidden') === false){
	               		formAfterPostBackHandler.form1();
	               }
	               else if($('#content_0_centre_0_pnlConfirmation').hasClass('hidden') === false){
	               		formAfterPostBackHandler.confirmation();
	               }
	               
	            });
	        }
            
            if($('#content_0_centre_0_pnlStep1').length){
            		setupValidation.init();
            }
            
        },
        formPostBackHandler1: formPostBackHandler.form1,
        formPostBackHandler2: formPostBackHandler.form2,
        formPostBackHandler3: formPostBackHandler.form3,
        formAfterPostBackHandler1: formAfterPostBackHandler.form1,
        formAfterPostBackHandler2: formAfterPostBackHandler.form2,
        formAfterPostBackHandler3: formAfterPostBackHandler.form3,
        lightbox: lightbox,
        setupCarousel: setupCarousel,
        carouselController: carouselController,
        scrollToBox: scrollToBox
    }

} ();

$.focusGroup = function(parent_id, on_focus, on_blur){

	var $parent = $(parent_id);
	$parent.addClass('focusGroup');
	
	if($parent.length){
		$parent.on('focus','input,select',function(){
			var $parent_node = $(this).parents('.focusGroup');
			if($parent_node.hasClass('focusGroupFocused') === false){
				$('.focusGroupFocused').removeClass('focusGroupFocused');
				$parent_node.addClass('focusGroupFocused');
				
				if($.focusGroup.blur !== undefined && $.focusGroup.blur !== null){
					$.focusGroup.blur($.focusGroup.blurGroup);
					$.focusGroup.blur = null;
					$.focusGroup.blurGroup = null;
				}
				
				on_focus($parent_node);
			}
			
		});
	
		$parent.on('blur','input,select',function(){
			$.focusGroup.blur = on_blur;
			$.focusGroup.blurGroup = $(this).parents('.focusGroup');
		});
	}
};

var placeholderFix = function() {
    
    if(!('placeholder' in document.createElement("input"))) { //don't do any work if we don't have to!
    			var inputs = $("input[type='text']"),
    				origColour = inputs.eq(0).css("color");
    
    			inputs
    				.each(function(i) {
    					var $t = $(this);
    					$t.val($t.attr("placeholder")).css("color", "#585858");
    				})
    				.focus(function() {
    					var $t = $(this);
    					if($t.val() === $t.attr("placeholder")) $t.val('').css("color", origColour);
    				})
    				.blur(function() {
    					var $t = $(this);
    					if($t.val() === '') {
    						$t.val($t.attr("placeholder")).css("color", "#585858");
    					} 
    				})
    		}

}


$(document).ready(function () { //Define the animation speed for the Carousel 
    indoorTry.init();
  

  	
  
  
  
  
});



