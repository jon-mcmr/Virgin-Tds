/****** 200 ALDERSGATE BY VIRGIN ACTIVE CLASSIC ******/

var virginactive_classic = function(){
	
	//Print module
	var print = function(){
		return{
			init: function(){
				$('.icon-print').click(function(){
					window.print();
					return false;
				});
			}
		}
	}();
	
	//Timetable module based on the timetable code from main site
	var timetable = function() {
		var daySwitcher = function(){
			var $this = $(this);
			var href = $this.attr('href');
			
			$('#select_day .active').removeClass('active');
			$(this).addClass('active');
		
			if($this.attr('href') === "#timetable_week"){
				$('.timetable').hide().not('.no-classes').show();
			}
			else{
				$('.timetable').hide();
				$(href).show();
			}
			
			addRowHighlight();
			
			return false
		};
	
		var addRowHighlight = function() {
            $(".timetable tr").removeClass("highlight").filter(":visible:odd").addClass("highlight");
        };

        var createIds = function(array) {
            var i, temp, len = array.length, out = [];
            for (i = 0; i < len; i++) {
                temp = array[i].replace(/ & |, | /g, "-");
                out.push(temp);
            }
            return out.sort();
        };

        var createClassesList = function(array, addClass) {
            var i, len = array.length, addClasses = '<ul id="list-classes">';
            for (var i = 0; i < len; i++) {
                addClasses += '<li><a href="#" class="' + addClass[i] + '">' + array[i] + '</a></li>';
            }
            addClasses += "</ul>";
            $('#timetable-filter').append(addClasses);
            $('#list-classes').attr('data-height', $('#list-classes').outerHeight());
        };
        
        var onlyUnique = function( my_array ) {
            my_array.sort();
            for ( var i = 1; i < my_array.length; i++ ) {
                if ( my_array[i] === my_array[ i - 1 ] ) {
                            my_array.splice( i--, 1 );
                }
            }
            return my_array;
        };
		
		
		return{
			init: function(to_init){
				addRowHighlight();
			
				$(to_init).find('.classtip').click(function(){
					return false;
				});
				
				$('#select_day').on('click','a',daySwitcher);
				
								
				//Recycle code from main site
                var $timetableWrap = $(".timetable-wrap");
                var $day_selector = $(".day_selector");
                
                $(".day_selector").append('<div id="timetable-filter" class=""><p class="filter-class" style="width: 135px; "><a href="#" data-filterbyclass="Filter by class" data-clearfilter="Clear filter" class="">Filter by class</a></p></div>');
				
				var $timetablefilter = $('#timetable-filter');
				
                //Grab array of exercise classes
                var classesArray = [], classesId;
                $timetableWrap.find("span.classname").each(function () {
                    classesArray.push($(this).text());
                });
				
                if (classesArray.length > 0) {
                    classesArray = onlyUnique(classesArray);
                    var classesId = createIds(classesArray);
                    createClassesList(classesArray.sort(), classesId);
                }
                //Add name of exercise class to table row
                $timetableWrap.find("span.classname").each(function () {
                    $this = $(this);
                    var classType = $this.text();
                    classType = classType.replace(/ & |, | /g, "-");
                    $this.parent().parent().addClass(classType);
                });

                $(".filter-class").css({ width: $(".filter-class a").outerWidth() });

                //Toggle dropdown list of classes
                $(".filter-class a").click(function (e) {
                    e.preventDefault();
                    $this = $(this);

                    //Check whether classes have been filtered and update .timetable
                    if ($this.hasClass("clear-filter")) {
                    	var day = $('#select_day a.active').attr('href').replace('#','');
                    	
                        $(".filter-class a").removeClass("clear-filter").text($this.text() == $this.data("clearfilter") ? $this.data("filterbyclass") : $this.data("clearfilter")).blur().closest(".timetable-wrap").removeClass("timetable-filtered");
                        $(".timetable-wrap").children(".timetable").each(function () {
                            $thisTimetable = $(this);
                            
                            if(day === "timetable_week"){
                            	$thisTimetable.show();
                            }
                            
                            $thisTimetable.removeClass("hide-filtered no-classes").find(".no-classes").remove();
                            $thisTimetable.find("tr").each(function () {
	                                $(this).show();
                            });
                        });
                        
                        addRowHighlight();
                        $('.single-article').css('minHeight','0px');
                    	
                    } else {
                        $this.parent().next().slideToggle(600, "easeOutQuint");
                        if(parseInt($('.single-article').css('minHeight')) > 0){
	                        $('.single-article').css('minHeight','0px');
                    		
                    	}
                    	else{
                    		$('.single-article').css('minHeight','0px');
                    		$('.single-article').css('minHeight',parseInt($('#list-classes').attr('data-height')) + (parseInt($('.single-article').height()) - parseInt($('.timetable-wrap').height())) + 100);
                    		
                    	}
                    }
                });

                //Hide dropdown list once filter has been selected
                $("#list-classes a").click(function (e) {
                    e.preventDefault();
                    $clicked = $(this);

                    $clicked.closest("ul").hide();
                    $('.single-article').css('minHeight','0px');
                    $(".tab a").removeClass("active").filter(":first").addClass("active");

                    $filterByClass = $(".filter-class a");
                    $filterByClass.addClass("clear-filter").text($filterByClass.text() == $this.data("filterbyclass") ? $this.data("clearfilter") : $this.data("filterbyclass")).closest(".timetable-wrap").addClass("timetable-filtered");

                    $(".timetable").find("tr").show();

                    //Turn off rows that do not match the filtered class name
                    $(".timetable-wrap tr").each(function () {
                        $this = $(this);
                        if($this.find('th').length === 0){
	                        if (!$this.hasClass($clicked.attr("class"))) {
	                            $this.hide();
	                        }
                        }
                    });
                    
                   	var day = $('#select_day a.active').attr('href').replace('#','');
                    
                    //Hide day if all the rows in the table are hidden
                    $(".timetable-wrap .timetable").each(function () {
                        $this = $(this);
                        $this.show();
                        
	                        if ($this.find("tr td:visible").length === 0) {
	                        	
	                            if(day !== $this.attr('id')){
	                            	$this.hide();
	                            }
	                            
	                            $this.addClass('no-classes');
	                            
	                            if ($this.find("p .no-classes").length === 0) {
	                                $this.append('<p class="no-classes">Sorry, there is no class information for ' + $clicked.text() + ' available today</p>');
	                            }
	                        }
	                        else{
		                        if(day !== $this.attr('id') && day !== "timetable_week"){
		                        	$this.hide();
		                        }
	                        }
                        
                    });

                    addRowHighlight();
                });
			
//				$(to_init).find('.classtip').qtip({
//					position: {
//						my: 'bottom center',
//						at: 'top center'
//					},
//					style: {
//						width: 138,
//						classes: 'ui-tooltip ui-tooltip-va'
//					}
//				});
			}
		}
	}();

	//Homepage module
	var homepage_carousel = function(){
		var privateProperties = {
			timer: null,
			image: {},
			transition_time: 500,
			isIE7: false
		};
	
		if($('html').hasClass('ie7')){
			privateProperties.isIE7 = true;
		}
	
		var controllerClick = function(){
			var $this = $(this), href = $this.attr('href'), current_href = $('.current-slide a').attr('href'), $side_panel = null;
			
			
			if($this.parent('li').hasClass('current-slide')){
				if($('html').hasClass('ie7')){
					$('#slide-list').fadeOut(100);
				}
				
				privateProperties.panels.find(href).css('z-index','auto').find('.side_panel').animate({'left':'0px'}, privateProperties.transition_time);
				resetPanelScroll($(href).find('.side_panel .scroll_container'));
				
				//Setup the scrollbars
				$('.scroll_container').each(function(index){
					var height = $(window).height() - 130;
					$(this).css('height', height);
					$('#' + $(this).attr('id')).mCustomScrollbar("vertical",200,"easeOutCirc",0,"fixed","yes","no",0);
				});
				
				clearInterval(privateProperties.timer);
			}
			else{
			
				$('.current-slide').removeClass('current-slide');
			
				if($this.parents('.panel-list').length){
					$side_panel = $this.parents('.side_panel');
					$('#slide-list a[href$="'+href+'"]').parent('li').addClass('current-slide');
					$side_panel.fadeOut(privateProperties.transition_time, function(){
						$(this).css('left','-290px').fadeIn(0);
						animatePanelTransition($this,href,current_href);
					});
				}else{
					$this.parent('li').addClass('current-slide');
					animatePanelTransition($this,href,current_href);
				}
		
				
			}
		};
		
		var animatePanelTransition = function($this,href,current_href){
			privateProperties.panels.find(current_href).css('z-index','87');
			privateProperties.panels.find(href).css('z-index','88').fadeIn(privateProperties.transition_time, function(){
				privateProperties.panels.find(current_href).fadeOut(0).css('z-index','auto');
				privateProperties.panels.find(href).css('z-index','auto')
			});
			
			
			if($this.parents('.panel-list').length){
				setTimeout(function(){
					$('#slide-list a[href$="'+href+'"]').click();
				}, privateProperties.transition_time)
			}
			
			clearInterval(privateProperties.timer);
			privateProperties.timer = setInterval(rotatePanels, 5000);
		};
		
		var closeSidebar = function($this){
			var $side_panel = $this.parents('.side_panel');
			
			
			var $active_panel = $('.panels').find('li.panel:visible').eq(0);
			
			$active_panel.find('.galleryimg').fadeOut(privateProperties.transition_time, function(){
				$active_panel.find('.galleryimg').remove();
			})
			
			$side_panel.animate({'left':'-290px'}, privateProperties.transition_time);
			privateProperties.timer = setInterval(rotatePanels, 5000);
			
			if($('html').hasClass('ie7')){
				$('#slide-list').fadeIn(100);
			}
			
			return false;
		};
		
		var rotatePanels = function(){
			var $next = $('#slide-list').find('.current-slide').next(), $click = null;
			
			if($next.length){
				$click = $('#slide-list').find('.current-slide').next().find('a');
				if(!privateProperties.isIE7){
					window.location.hash = $click.attr('href');
				}
				$click.click();
			}
			else{
				$click = $('#slide-list').find('li').first().find('a');
				if(!privateProperties.isIE7){
					window.location.hash = $click.attr('href');
				}
				$click.click();
			}
		};
		
		var resizeImages = function(){
			var aval_height = $(window).height();
			
			if($('#cookie-ribbon').length){
				aval_height = aval_height - $('#cookie-ribbon').height();
			}
			
			$('.site_container').height(aval_height);
			
			var $site_container = $('.site_container'),browser_ratio = ($site_container.height()/$site_container.width()).toFixed(2);
			var height = 1006,width = 1500, image_ratio = width/height, left = 0, top = 0;
			
			var $active_panel = $('.panels').find('li.panel:visible').eq(0).find('img.supersized');
				
			$active_panel.css('top','0px');
			$active_panel.css('left','0px');
			
			if (image_ratio < browser_ratio){
				$active_panel.css('height',$site_container.height());
				$active_panel.css('width',$site_container.height() * image_ratio);
				left = ($site_container.width() - $active_panel.width()) / 2;
				$active_panel.css('left',left);
			}
			else{
				$active_panel.css('width',$site_container.width());
				$active_panel.css('height',$site_container.width() / image_ratio);
				
				if($site_container.height() > $active_panel.height()){
					$active_panel.css('width','auto');
					$active_panel.css('height',$site_container.height());
					left = ($site_container.width() - $active_panel.width()) / 2;
					$active_panel.css('left',left);
				}else{
					top = ($site_container.height() - $active_panel.height()) / 2;
					$active_panel.css('top',top);
				}
			}
			
			//Store everything in private properties
			privateProperties.image.top = top;
			privateProperties.image.left = left;
			privateProperties.image.width = $active_panel.width();
			privateProperties.image.height = $active_panel.height();
			
			$('.panels li.panel img.supersized').css('top',privateProperties.image.top).css('left',privateProperties.image.left).css('width',privateProperties.image.width).css('height',privateProperties.image.height)
			
		};
		
		var galleryClick = function(){
			var $this = $(this), href = $this.attr('href'), current_image = null;
			
			var $active_panel = $('.panels').find('li.panel:visible').eq(0);
			
			$active_panel.find('.active-thumb').removeClass('active-thumb');
			$this.addClass('active-thumb');
			
			if(href === "#"){
				$active_panel.find('.galleryimg').fadeOut(privateProperties.transition_time, function(){
					$active_panel.find('.galleryimg').remove();
				})
			}
			else{
				$current_image = $active_panel.find('img.supersized').last();
				
				$('<img src="'+href+'" class="galleryimg supersized" style="display:none;" />').insertAfter($current_image).fadeOut(0).load(function(){
					$(this).fadeOut(0).fadeIn(500, function(){
						$active_panel.find('.galleryimg').not(this).remove();
					});
				});
				
				$('.panels li.panel img.supersized').css('top',privateProperties.image.top).css('left',privateProperties.image.left).css('width',privateProperties.image.width).css('height',privateProperties.image.height)
			}
			
			return false;
		};
		
		var resetPanelScroll = function(target) {
            var $customScrollBox = target.find(".customScrollBox");
            var $customScrollBox_container = target.find(".container");
            var $dragger = target.find(".dragger");

            $dragger.css("top", 0);
            $customScrollBox_container.css("top", 0);
            $customScrollBox.data("contentHeight", $customScrollBox_container.height());
            
            
	    }
		
		return {
			init: function(page){
				var $page = $(page), $slidelist = $page.find('#slide-list'), $side_panel = null, aval_height = $(window).height();
				
				if($('html').hasClass('ie7') || $('html').hasClass('ie8')){
					privateProperties.transition_time = 1000;
				}
				
				//Setup the scrollbars
				$('.scroll_container').each(function(index){
					var height = $(window).height() - 130;
					$(this).css('height', height);
					$('#' + $(this).attr('id')).mCustomScrollbar("vertical",200,"easeOutCirc",0,"fixed","yes","no",0);
				});
				
				privateProperties.panels = $page.find('.panels');
				
				if($('#cookie-ribbon').length){
					aval_height = aval_height - $('#cookie-ribbon').height();
				}
				
				$('.site_container').height(aval_height);
				
				$page.find('#panelNOJS').remove();
				privateProperties.panels.find('li.panel').not(':first').fadeOut(0);
				
				$slidelist.find('li:first').addClass('current-slide');
				
				$slidelist.on('click','a',controllerClick);
								
				$side_panel = privateProperties.panels.find('li.panel .side_panel');
				
				$side_panel.find('h1').before('<a href="#close" class="close ir">Close</a>');
				$side_panel.find('.close').click(function(){
					$this = $(this);			
					return closeSidebar($this)
				});
				$side_panel.find('.gallery').on('click', 'a', galleryClick);
				
				$side_panel.find('.also ul.panel-list').on('click','a',controllerClick);

				privateProperties.timer = setInterval(rotatePanels, 5000);
				
				document.body.onload = resizeImages;
				window.onresize = function(){
					//Setup the scrollbars
					$('.scroll_container').each(function(index){
						var height = $(window).height() - 130;
						$(this).css('height', height);
						$('#' + $(this).attr('id')).mCustomScrollbar("vertical",200,"easeOutCirc",0,"fixed","yes","no",0);
					});
					
					resizeImages();
				};
				
				//If hash then do something
				if(window.location.hash){
					if(privateProperties.panels.find('li.panel').first().attr('id') !== window.location.hash.replace('#','')){
						$('#slide-list a[href$="'+window.location.hash+'"]').click();
					}
				}
				
			}
		}
	}();

	var are_cookies_enabled = function(){
		var cookieEnabled = (navigator.cookieEnabled) ? true : false;
	
		if (typeof navigator.cookieEnabled == "undefined" && !cookieEnabled)
		{ 
			document.cookie="testcookie";
			cookieEnabled = (document.cookie.indexOf("testcookie") != -1) ? true : false;
		}
		return (cookieEnabled);
	};

	var autosizeMemberBlocks = function(){
		
		
		return {
			init: function(){
				var maxHeight = 0;
			
				$('.membership_options .copy').each(function() { 
					maxHeight = Math.max(maxHeight, $(this).height()); 
				}).height(maxHeight);
				
				maxHeight = 0;
				
				$('.membership_options li').not('li li').each(function() { 
					maxHeight = Math.max(maxHeight, $(this).height()); 
				}).height(maxHeight);
				
			}
		}
	}();

	return {
		init: function(){
		
			if(!are_cookies_enabled()){
				$('#cookie-ribbon').remove();
				$('body').removeClass('cookie-show');
			}
		
			if($('.timetable').length){
				timetable.init('.timetable');
			}
			
			if($('.club-alert-panel').length){
				$('.club-alert-panel').on('click', function(){
					$(this).slideUp();
				});
			}
			
			if($('.icon-print').length){
				print.init();
			};
			
			if($('.home').length){
				homepage_carousel.init('.home');
			};
			
			autosizeMemberBlocks.init();
	
			if(($('html').hasClass('no-touch') || (navigator.userAgent.indexOf('iPhone') === -1))){
			   
                $(".lightbox").colorbox({ 
                    href: $(".lightbox").attr('href') + " #content",
                });
			}

		}
	};
}();

$(document).ready(function(){
	virginactive_classic.init();
});



				
