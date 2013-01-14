var virginactive;

if(typeof virginactive !== "object"){
	virginactive = {};
}

virginactive.home = function(){

	var homepageCarousel = (function(){
		var $targetIconList = $("ul#carousel-icons"),
			$targetCarousel = $('.carousel-images'),
			timer = null,
			visible_timer = null,
			changeInterval = 6000,
			$targetCarouselButtons = $(".carousel-btns li, .has-tooltip"),
			autoplay = true,
			rotator = $('div#timer span#rotator'),
        	mask = $('div#timer span#mask'),
        	pause = $('div#timer span#pause'),
        	degrees = 0,
        	$hotspots = $(".hotspots"),
        	$hotspotsContent = $(".hotspot-data");
		
		var getCarouselData = function(activeSlide){
			var currentIcon = $(activeSlide).children("img").attr("data-icon"), currentCaption = $(activeSlide).children("img").attr("data-caption"), currentSubCaption = $(activeSlide).children("img").attr("data-subcaption"), previousSlide = $(activeSlide).prev(), nextSlide = $(activeSlide).next(), previousIcon = null, nextIcon = null;
			//If there is a next slide grab the data-icon attribute data from its image.
			if (nextSlide.length !== 0){
				nextIcon = nextSlide.children("img").attr("data-icon");
			}
			//Else its the last slide so grab the data from the first image
			else{
				nextIcon = $targetCarousel.find('li').first().children("img").attr("data-icon");
			}
			//If there is a previous slide grab the data-icon attribute data from its image.
			if (previousSlide.length !== 0){
				previousIcon = previousSlide.children("img").attr("data-icon");
			}
			//Else its the last slide so grab the data from the last image
			else{
				previousIcon = $targetCarousel.find('li').last().children("img").attr("data-icon");
			}
			//Use the data gathered above to set the icon images for the previous, current and next links
			setCarouselData(nextIcon, previousIcon, currentIcon, currentCaption,currentSubCaption);
		};
		
		var setCarouselData = function(nextIcon, previousIcon, currentIcon, currentCaption,currentSubCaption){
			var $prevLink, $currentLink = $targetIconList.children("li.current").find("a"), $nextLink;

			//Clear all classes from any previous revolutions of the carousel and delete the previous caption.
			$targetIconList.find("li a").attr('class', 'ir');
			$("p.caption").remove();
			if($currentLink.hasClass("play")===true){
				$currentLink.removeClass("play");
			}

			$prevLink = $targetIconList.children("li.prev").find("a");
			$nextLink = $targetIconList.children("li.next").find("a");
			
			//Add the classes to the pagination links
			$prevLink.addClass(previousIcon);
			$currentLink.addClass(currentIcon);
			$nextLink.addClass(nextIcon);
			//Insert the caption
			$('<p class="caption"><span>'+currentCaption+'</span> ' + currentSubCaption +'</p>').insertAfter($targetIconList).fadeIn("slow");	
			

		};
		
		var changeSlide = function(slideDirection){
			//Find current active slide
			var activeSlide = $targetCarousel.find("li.active");
			
			var nextSlide = null;
			
			//Add the active class to the next slide
			
			if(slideDirection === 'next'){
				

				nextSlide = activeSlide.next("li");

				if(!nextSlide.length){
					nextSlide = $targetCarousel.children('li:first');
				}
				
				
			}
			else if (slideDirection === 'prev'){
				nextSlide = activeSlide.prev("li");

				if(!nextSlide.length){
					nextSlide = $targetCarousel.children('li:last');
				}
			}
			
			//Fade out current slide and remove the active class
			activeSlide.stop(true, true).fadeOut(300,function(){
				nextSlide.stop(true, true).fadeIn(300).addClass("active").find(".hotspot-data").hide();
			}).removeClass("active");
			

			//Get the icons for the previous,current and next slide
			getCarouselData(nextSlide);
		};
		
		//On click handler for navigation
		var paginationHandler = function(){
			var $this = $(this), $thisParent = $this.parents('li');
			
			clearInterval(timer);
			
			if($thisParent.hasClass('next')){
				resetClock();
				$thisParent.parent().stop(true, true).fadeOut();
				changeSlide('next');
				$thisParent.parent().stop(true, true).fadeIn("slow");
				
				$hotspotsContent.hide();
				$hotspotsContent.find('iframe').attr('src',$hotspotsContent.find('iframe').attr('src'));
				$('.hotspot.active').removeClass('active');
				autoplay = true;
			}
			else if($thisParent.hasClass('prev')){
				resetClock();
				$thisParent.parent().stop(true, true).fadeOut();
				changeSlide('prev');
				$thisParent.parent().stop(true, true).fadeIn("slow");
				
				
				$hotspotsContent.hide();
				$hotspotsContent.find('iframe').attr('src',$hotspotsContent.find('iframe').attr('src'));
				$('.hotspot.active').removeClass('active');
				autoplay = true;
			}
			
			else {
				if(autoplay===true){
					autoplay = false;
					$(this).addClass('play');
				}
				else{
					$hotspotsContent.hide();
					$hotspotsContent.find('iframe').attr('src',$hotspotsContent.find('iframe').attr('src'));
					$('.hotspot.active').removeClass('active');
					autoplay=true;
					$(this).removeClass('play');
					timer = setInterval(clock,changeInterval/180);
				}
			}
			
			return false;
			
		};
		
		var buttonTooltips = function(){
			var $this = $(this), $tooltip = $this.siblings(".tooltip"),$thisWidth = $this.width();
			if($this.hasClass("streetview")!==true){
				if(!$tooltip.hasClass("tooltip-loaded")){
					$tooltip.css({width : $thisWidth+"px"});
					$tooltip.css({height: $tooltip.height()+"px"});
					$tooltip.addClass("tooltip-loaded");
				}
				
			}
			
			$tooltip.stop(true, true).fadeIn();
			
		};
		
		var buttonTooltipsOff = function(){
			var $this = $(this), $tooltip;
			$tooltip = $this.siblings(".tooltip");
			$tooltip.stop(true, true).fadeOut();
			
		};
				
		var setupInitialState = function(){
			var activeSlide = $targetCarousel.find("li.active");
			$targetCarousel.children("li").not($(activeSlide)).hide();
			getCarouselData(activeSlide);
		};

		var clock = function(){
			if ($('html').hasClass("csstransforms")){
				var degreeCSS = "rotate("+degrees+"deg)";
				degrees += 2;
				rotator.css({"msTransform": degreeCSS, "-webkit-transform": degreeCSS, "-moz-transform": degreeCSS, "-o-transform": degreeCSS });
				
				if(degrees > 180) {
					rotator.addClass("move");
					mask.addClass("move");
				}
				if(degrees > 360) {
					rotator.removeClass("move");
					mask.removeClass("move");
					degrees = 0;
					changeSlide('next');
				}
			}
			else{
				degrees += 2;
				
				if(degrees > 360) {
					degrees = 0;
					$('#timer #mask').stop(true, true).fadeIn(0).fadeOut(changeInterval);

					changeSlide('next');
				}
			}
		};

		var resetClock = function(){
			if ($('html').hasClass("csstransforms")){
				var degreeCSS = "rotate("+0+"deg)";
				rotator.removeClass("move");
				mask.removeClass("move");
				degrees = 0;
				rotator.css({"msTransform": degreeCSS, "-webkit-transform": degreeCSS, "-moz-transform": degreeCSS, "-o-transform": degreeCSS });
				timer = setInterval(clock,changeInterval/180);
			}
			else{
				degrees = 0;
				$('#timer #mask').stop(true, true).fadeIn(0).fadeOut(changeInterval);
			}
		};
		
		var hotspotContentHandler = function(){
			var $this = $(this), active = $this.hasClass('active');
			
			$hotspotsContent.hide();
			$hotspotsContent.find('iframe').attr('src',$hotspotsContent.find('iframe').attr('src'));

			$('.hotspot.active').removeClass('active');
			
			if(active === false){
			
				$this.addClass('active');
				$this.siblings().fadeIn();
				$('#timer a').not('.play').click();
			}
			else{
				$('#timer a').click();
			}
			
			
			
			return false;
		};
		
		return {
			init: function(){
				$targetIconList.on('click','li a', paginationHandler);
				$targetCarouselButtons.find("a").hover(buttonTooltips , buttonTooltipsOff);
				$hotspots.on('click', 'li a', hotspotContentHandler);
				timer = setInterval(clock,changeInterval/180);
				setupInitialState();
				
				$(".blog-content .wrapper").sameHeightCols();

				if ($('html').hasClass("csstransforms") === false){
					$('#timer #mask').stop(true, true).fadeIn(0).fadeOut(changeInterval);
				}
			}
		};
	
	}());
	
	var homepageColumns = function(){
		var $halfPanels = $('#content .half-panel'), thisHeight = 0, nextHeight = 0, $thisElement = null;

		for (var i = 0; i < $halfPanels.length; i = i + 2) {
			$thisElement = $halfPanels.eq(i);

			thisHeight = $thisElement.height();
			nextHeight = $thisElement.next().height();

			if(thisHeight > nextHeight){
				$thisElement.next().css('height', thisHeight);
				$thisElement.css('height', thisHeight);
			}
			else {
				$thisElement.css('height', nextHeight);
				$thisElement.next().css('height', nextHeight);
			}

		};
	};

	return {
		init: function(){
			homepageCarousel.init();

			$(window).on('load', homepageColumns);
		}
	};
}();


$(document).ready(function(){
	virginactive.home.init();
});
