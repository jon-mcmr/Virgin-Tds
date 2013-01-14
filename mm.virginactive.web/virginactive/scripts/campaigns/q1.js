/*global google:true $:true */

//Define the virgin active object that we will work within
var virginactive = {'campaigns':{}};

//Define the q1 campaign
virginactive.campaigns.q1 = (function(){

	//The maps autocomplete coming from google places api
	var mapsAutocomplete = (function(){
		var autocomplete = null;

		var initialize = function() {
			var defaultBounds = new google.maps.LatLngBounds(
				new google.maps.LatLng(49.83798, -5.97656),
				new google.maps.LatLng(59.68993, 1.75781)
			);

			var options = {
				bounds: defaultBounds
			};

			var input = document.getElementById('searchTextField');
			autocomplete = new google.maps.places.Autocomplete(input, options);

			$('div.lightbox').addClass('visuallyhidden');

			//Binding of click and enter events
			google.maps.event.addListener(autocomplete, 'place_changed',selectLocation);
			$('#searchTextField').keypress(enterPress);


			
		};

		var enterPress = function(e) {
			if (e.which === 13) {
				var firstResult = $('.pac-container .pac-item:first').text();

				var geocoder = new google.maps.Geocoder();
				geocoder.geocode({'address':firstResult }, function(results, status) {
					if (status === google.maps.GeocoderStatus.OK) {
						var place = {
							'lat' : results[0].geometry.location.lat(),
							'lng' : results[0].geometry.location.lng(),
							'placeName' : results[0].address_components[0].long_name,
							'searchTerm' : $('#searchTextField').val()
						};

						$('.pac-container .pac-item:first').addClass('pac-selected');
						$('.pac-container').css('display','none');
						$('#searchTextField').val(firstResult);
						$('.pac-container').css('visibility','hidden');

						getClubs.request(place);

					}
				});
			} else {
				$('.pac-container').css('visibility','visible');
			}
		};

		var selectLocation = function(){
			//Selects result by mouse or arrow keys
			var googlePlace = autocomplete.getPlace(), place = null;

			if(googlePlace.id !== undefined){
				place = {
					'lat' : googlePlace.geometry.location.Ya,
					'lng' : googlePlace.geometry.location.Za,
					'placeName' : googlePlace.address_components[0].long_name,
					'searchTerm' : $('#searchTextField').val()
				};

				getClubs.request(place);
			}
		};

		return {
			init: function(){
				google.maps.event.addDomListener(window, 'load', initialize);
			}
		};
	})();

	var selectClub = function(){
		var $this = $(this), value = $this.val();

		window.location.href = value;
	};

	//Handle getting the clubs from the ajax api
	var getClubs = (function(){

		var loadingResults = false;
		var sendRequestWithPlace = function(place){
			var url = "/landing-pages/whatever-gets-you-going?ajax=1&cmd=ClubDetailsList&lat="+place.lat+"&lng="+place.lng+"&loc="+place.searchTerm+"&landingid="+$('body').attr('data-landing')+"&ste=sw18&sty=2";

			//var data = [{"clubname":"Wandsworth, Southside","distanceFromSource":"0.68","clubGUID":"{5650F853-159F-4B9A-9898-AFFA000FDAEB}"},{"clubname":"Wandsworth, Smugglers Way","distanceFromSource":"1.1","clubGUID":"{FBFE4DD1-D696-4656-B11E-F5A59D61AEB3}"},{"clubname":"Putney","distanceFromSource":"1.28","clubGUID":"{BE166B76-2195-4A69-B2D8-EB6ACFB4DAFA}"}];

			if(!loadingResults){
				loadingResults = true;

				$.getJSON(url,handleResults);
			}
			
		};

		var sendRequestWithDefaultLoc = function(){
			var $body = $('body'), url = "/landing-pages/whatever-gets-you-going?ajax=1&cmd=ClubDetailsList&lat="+$body.attr('data-lat')+"&lng="+$body.attr('data-long')+"&loc="+$body.attr('data-region')+"&landingid="+$('body').attr('data-landing')+"&ste=sw18&sty=2";

			//var data = [{"clubname":"Wandsworth, Southside","distanceFromSource":"0.68","clubGUID":"{5650F853-159F-4B9A-9898-AFFA000FDAEB}"},{"clubname":"Wandsworth, Smugglers Way","distanceFromSource":"1.1","clubGUID":"{FBFE4DD1-D696-4656-B11E-F5A59D61AEB3}"},{"clubname":"Putney","distanceFromSource":"1.28","clubGUID":"{BE166B76-2195-4A69-B2D8-EB6ACFB4DAFA}"}];

			if(!loadingResults){
				loadingResults = true;

				$.getJSON(url,handleResults);
			}
		};

		var handleResults = function(data){

			var html = '<ul class="results">';

			for (var i = 0; i < 3; i++) {
				html += "<li>";

				html += '<a href="'+data[i].clubURL+'">' + data[i].clubname + '</a> ';
				html += '<span>' + data[i].distanceFromSource + ' miles</span>';

				html += "</li>";
			};

			html += "</ul><a href='#' id='show_more'>Show more...</a>";

			$('.places_search ul, .places_search #show_more').remove();
			$('.places_search').append(html);

			$('#show_more').on('click', function(){
				var newHtml = '';

				for (var i = 3; i < data.length; i++) {
					newHtml += "<li>";

					newHtml += '<a href="'+data[i].clubURL+'">' + data[i].clubname + '</a> ';
					newHtml += '<span>' + data[i].distanceFromSource + ' miles</span>';

					newHtml += "</li>";
				};

				$('.places_search ul').append(newHtml);
				$(this).remove();

				$.colorbox.resize();

				return false;
			});

			$.colorbox.resize();

			loadingResults = false;
		};

		return {
			request : sendRequestWithPlace,
			requestDefault : sendRequestWithDefaultLoc,
			reset: function(){
				loadingResults = false;
			}
		};
	})();

	//Form validation
	var formValidation = (function () {
        var submitted = false;

        var validateClick = function () {
            var valid = false;

            if (!submitted) {
				valid = validateForm(false);

				if (valid === true) {
					submitted = triedSubmission = true;
					$('input[type="submit"]').addClass('loading');

					__doPostBack($('input[type="submit"]').attr('name'), '');
				}
				else {
					triedSubmission = true;
				}
			}

            return false;
        };

        var validateForm = function () {
            var $formFields = $('fieldset input[type="text"], fieldset select, fieldset textarea'), valid = true;
			
			$('p.error').remove();

			$formFields.each(function(event){
                if(validateField($(this)) === false){
                	valid = false;
                }
            });

            return valid;
        };

        var changeValidation = function(){
        	validateField($(this));
        };

        var validateField = function($this){

			var valid = true, validationClass = $this.attr('class') || '', furtherValidation = true, value = $this.val() || '', default_value = $this.attr('data-required');

            $this.removeClass('error').off('click').off('blur').parent().removeClass('valid').find('p.error').remove();

            //Validate if it is required
            if (validationClass.indexOf('required') !== -1) {
                if (validateRequired(value,default_value) === false) {
                    valid = false;
                    setError($this, $this.attr('data-required'),true);

                    furtherValidation = false;
                }
            }

            //If has passsed required OR is not required but requires valid value then validate furthe
            if (furtherValidation) {
                if(validationClass.indexOf('email') !== -1) {
                    if (validateEmail(value) === false) {
                        valid = false;
                        setError($this, $this.attr('data-email'),false);
                    }
                }
                else if(validationClass.indexOf('phone') !== -1) {
                    if (validatePhone(value) === false) {
                        valid = false;
                        setError($this, $this.attr('data-phone'),false);

                    }
                }
            }

            if(valid){
            	$this.parent().addClass('valid')
            }

        	return valid;
        };

        var setError = function ($this, error, isPlaceholder) {
            var tag = $this.prop('tagName');

            $this.addClass('error');

            if(isPlaceholder && hasPlaceholderSupport()){
                $this.attr('placeholder', error);
            }
            else if(isPlaceholder){
            	if(validateRequired($this.val(),$this.attr('data-required')) === false){
            		$this.val(error).on('click',enablePlaceholderSupport.click).blur(enablePlaceholderSupport.blur);
            	}
            }
            else{
                $('<p class="error">'+ error +'</p>').insertAfter($this);
            }
            
        };

        var enablePlaceholderSupport =  {
			click: function(){
				$(this).val('');
			},
			blur: function(){
				var $this = $(this);

				if(validateRequired($this.val(), $this.attr('data-required')) === false){
					$this.val($this.attr('data-required'));
				}
			}
		};

        var hasPlaceholderSupport = function(){
			var input = document.createElement('input');
			return ('placeholder' in input);
		};

        var validateEmail = function (value) {
            var valid = false;

            if (value.match(/^((?:(?:(?:\w[\.\-\+]?)*)\w)+)\@((?:(?:(?:\w[\.\-\+]?){0,62})\w)+)\.(\w{2,6})$/)) {
                valid = true;
            }

            return valid;
        };

        var validateRequired = function (value, default_value) {
            var valid = true;

            if (value === null || value === '' || default_value === value) {
                valid = false;
            }

            return valid;
        };

        var validatePhone = function (value) {
            var valid = false;

            // if (value.match(/^(((\+44\s?\d{4}|\(?0\d{4}\)?)\s?\d{3}\s?\d{3})|((\+44\s?\d{3}|\(?0\d{3}\)?)\s?\d{3}\s?\d{4})|((\+44\s?\d{2}|\(?0\d{2}\)?)\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$/)) {
            //     valid = true;
            // }

            if (value.match(/^(0(\d|\s){8,12}$)|(\+(\d|\s){8,12}$)/)) {
                valid = true;
            }

            return valid;
        };

        return {
            validate: validateClick,
            init: function(){
            	var $formFields = $('fieldset input[type="text"], fieldset select, fieldset textarea');
            	$formFields.on('change', changeValidation);
            }
        };
    })();

    var closeLightbox  = function(){
		$('.places_search ul').remove();
		$('.places_search input').val('');
		getClubs.reset();
    };

    var openLightbox = function(){
    	if($('.places_search input').attr('data-default')){
			$('.places_search input').val($('.places_search input').attr('data-default'));
			getClubs.requestDefault();
		}
    };

    var tweetBox = (function(){
    	var showTweet = function(){
    		var current = $('.tweet li:visible'), next = $('.tweet li:visible').next(); 

    		if(next.length === 0){
    			next = $('.tweet li').first();
    		}

    		current.fadeOut('fast', function(){
    			next.fadeIn();
    		});
    		

    		setTimeout(showTweet, 6000);
    	};

    	return {
    		init: function(){
    			setTimeout(showTweet, 6000);
    		}
    	}
    }());

	return {
		init: function(){
			
			if($('#contentDiv').hasClass('home') || $('#contentDiv').hasClass('club')){

				if($('body').attr('data-region')){
					$('.places_search input').attr('data-default', $('body').attr('data-region'));
				}

				mapsAutocomplete.init();
				$('select').chosen();
				$('a.lightbox').colorbox({inline:true, width:"900px", onClosed: closeLightbox, onOpen: openLightbox});
				$('.club_dropdown select').on('change', selectClub);


			}

			tweetBox.init();


			formValidation.init();
		},
		validateForm: function(){
			var returnval = formValidation.validate();
			return returnval;
		}
	};
}());

//Document is ready so execute
$(document).ready(virginactive.campaigns.q1.init);