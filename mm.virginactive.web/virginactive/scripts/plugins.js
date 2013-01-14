/*****************************
******************************
1  Placeholder
2  Easing
3  OS scrollbar width
4  Livequery
5  preLoadImages
6  styleSelects - Restyle form selects
7  Unique enhancement
8  sameHeightCols
9  vaOverlay
10 Orbit - carousel
11 UItoTop
12 Innershiv
13 Chosen - form porn
******************************
******************************/

/*
* Placeholder plugin for jQuery
* Copyright 2010, Daniel Stocks (http://webcloud.se)
* Released under the MIT, BSD, and GPL Licenses. */
(function(b){function d(a){this.input=a;a.attr("type")=="password"&&this.handlePassword();b(a[0].form).submit(function(){if(a.hasClass("placeholder")&&a[0].value==a.attr("placeholder"))a[0].value=""})}d.prototype={show:function(a){if(this.input[0].value===""||a&&this.valueIsPlaceholder()){if(this.isPassword)try{this.input[0].setAttribute("type","text")}catch(b){this.input.before(this.fakePassword.show()).hide()}this.input.addClass("placeholder");this.input[0].value=this.input.attr("placeholder")}},
hide:function(){if(this.valueIsPlaceholder()&&this.input.hasClass("placeholder")&&(this.input.removeClass("placeholder"),this.input[0].value="",this.isPassword)){try{this.input[0].setAttribute("type","password")}catch(a){}this.input.show();this.input[0].focus()}},valueIsPlaceholder:function(){return this.input[0].value==this.input.attr("placeholder")},handlePassword:function(){var a=this.input;a.attr("realType","password");this.isPassword=!0;if(b.browser.msie&&a[0].outerHTML){var c=b(a[0].outerHTML.replace(/type=(['"])?password\1/gi,
"type=$1text$1"));this.fakePassword=c.val(a.attr("placeholder")).addClass("placeholder").focus(function(){a.trigger("focus");b(this).hide()});b(a[0].form).submit(function(){c.remove();a.show()})}}};var e=!!("placeholder"in document.createElement("input"));b.fn.placeholder=function(){return e?this:this.each(function(){var a=b(this),c=new d(a);c.show(!0);a.focus(function(){c.hide()});a.blur(function(){c.show(!1)});b.browser.msie&&(b(window).load(function(){a.val()&&a.removeClass("placeholder");c.show(!0)}),
a.focus(function(){if(this.value==""){var a=this.createTextRange();a.collapse(!0);a.moveStart("character",0);a.select()}}))})}})(jQuery);


/* jQuery Easing v1.3 - http://gsgd.co.uk/sandbox/jquery/easing/
 * Uses the built in easing capabilities added In jQuery 1.1 to offer multiple easing options
 * TERMS OF USE - jQuery Easing: Open source under the BSD License.  Copyright © 2008 George McGinley Smith. All rights reserved.
*/ // t: current time, b: begInnIng value, c: change In value, d: duration
jQuery.easing['jswing']=jQuery.easing['swing'];jQuery.extend(jQuery.easing,{def:'easeOutQuad',swing:function(x,t,b,c,d){return jQuery.easing[jQuery.easing.def](x,t,b,c,d);},easeInQuad:function(x,t,b,c,d){return c*(t/=d)*t+b;},easeOutQuad:function(x,t,b,c,d){return-c*(t/=d)*(t-2)+b;},easeInOutQuad:function(x,t,b,c,d){if((t/=d/2)<1)return c/2*t*t+b;return-c/2*((--t)*(t-2)-1)+b;},easeInCubic:function(x,t,b,c,d){return c*(t/=d)*t*t+b;},easeOutCubic:function(x,t,b,c,d){return c*((t=t/d-1)*t*t+1)+b;},easeInOutCubic:function(x,t,b,c,d){if((t/=d/2)<1)return c/2*t*t*t+b;return c/2*((t-=2)*t*t+2)+b;},easeInQuart:function(x,t,b,c,d){return c*(t/=d)*t*t*t+b;},easeOutQuart:function(x,t,b,c,d){return-c*((t=t/d-1)*t*t*t-1)+b;},easeInOutQuart:function(x,t,b,c,d){if((t/=d/2)<1)return c/2*t*t*t*t+b;return-c/2*((t-=2)*t*t*t-2)+b;},easeInQuint:function(x,t,b,c,d){return c*(t/=d)*t*t*t*t+b;},easeOutQuint:function(x,t,b,c,d){return c*((t=t/d-1)*t*t*t*t+1)+b;},easeInOutQuint:function(x,t,b,c,d){if((t/=d/2)<1)return c/2*t*t*t*t*t+b;return c/2*((t-=2)*t*t*t*t+2)+b;},easeInSine:function(x,t,b,c,d){return-c*Math.cos(t/d*(Math.PI/2))+c+b;},easeOutSine:function(x,t,b,c,d){return c*Math.sin(t/d*(Math.PI/2))+b;},easeInOutSine:function(x,t,b,c,d){return-c/2*(Math.cos(Math.PI*t/d)-1)+b;},easeInExpo:function(x,t,b,c,d){return(t==0)?b:c*Math.pow(2,10*(t/d-1))+b;},easeOutExpo:function(x,t,b,c,d){return(t==d)?b+c:c*(-Math.pow(2,-10*t/d)+1)+b;},easeInOutExpo:function(x,t,b,c,d){if(t==0)return b;if(t==d)return b+c;if((t/=d/2)<1)return c/2*Math.pow(2,10*(t-1))+b;return c/2*(-Math.pow(2,-10*--t)+2)+b;},easeInCirc:function(x,t,b,c,d){return-c*(Math.sqrt(1-(t/=d)*t)-1)+b;},easeOutCirc:function(x,t,b,c,d){return c*Math.sqrt(1-(t=t/d-1)*t)+b;},easeInOutCirc:function(x,t,b,c,d){if((t/=d/2)<1)return-c/2*(Math.sqrt(1-t*t)-1)+b;return c/2*(Math.sqrt(1-(t-=2)*t)+1)+b;},easeInElastic:function(x,t,b,c,d){var s=1.70158;var p=0;var a=c;if(t==0)return b;if((t/=d)==1)return b+c;if(!p)p=d*.3;if(a<Math.abs(c)){a=c;var s=p/4;}
else var s=p/(2*Math.PI)*Math.asin(c/a);return-(a*Math.pow(2,10*(t-=1))*Math.sin((t*d-s)*(2*Math.PI)/p))+b;},easeOutElastic:function(x,t,b,c,d){var s=1.70158;var p=0;var a=c;if(t==0)return b;if((t/=d)==1)return b+c;if(!p)p=d*.3;if(a<Math.abs(c)){a=c;var s=p/4;}
else var s=p/(2*Math.PI)*Math.asin(c/a);return a*Math.pow(2,-10*t)*Math.sin((t*d-s)*(2*Math.PI)/p)+c+b;},easeInOutElastic:function(x,t,b,c,d){var s=1.70158;var p=0;var a=c;if(t==0)return b;if((t/=d/2)==2)return b+c;if(!p)p=d*(.3*1.5);if(a<Math.abs(c)){a=c;var s=p/4;}
else var s=p/(2*Math.PI)*Math.asin(c/a);if(t<1)return-.5*(a*Math.pow(2,10*(t-=1))*Math.sin((t*d-s)*(2*Math.PI)/p))+b;return a*Math.pow(2,-10*(t-=1))*Math.sin((t*d-s)*(2*Math.PI)/p)*.5+c+b;},easeInBack:function(x,t,b,c,d,s){if(s==undefined)s=1.70158;return c*(t/=d)*t*((s+1)*t-s)+b;},easeOutBack:function(x,t,b,c,d,s){if(s==undefined)s=1.70158;return c*((t=t/d-1)*t*((s+1)*t+s)+1)+b;},easeInOutBack:function(x,t,b,c,d,s){if(s==undefined)s=1.70158;if((t/=d/2)<1)return c/2*(t*t*(((s*=(1.525))+1)*t-s))+b;return c/2*((t-=2)*t*(((s*=(1.525))+1)*t+s)+2)+b;},easeInBounce:function(x,t,b,c,d){return c-jQuery.easing.easeOutBounce(x,d-t,0,c,d)+b;},easeOutBounce:function(x,t,b,c,d){if((t/=d)<(1/2.75)){return c*(7.5625*t*t)+b;}else if(t<(2/2.75)){return c*(7.5625*(t-=(1.5/2.75))*t+.75)+b;}else if(t<(2.5/2.75)){return c*(7.5625*(t-=(2.25/2.75))*t+.9375)+b;}else{return c*(7.5625*(t-=(2.625/2.75))*t+.984375)+b;}},easeInOutBounce:function(x,t,b,c,d){if(t<d/2)return jQuery.easing.easeInBounce(x,t*2,0,c,d)*.5+b;return jQuery.easing.easeOutBounce(x,t*2-d,0,c,d)*.5+c*.5+b;}});


/* From Alexandre Gomes: http://www.alexandre-gomes.com/?p=115 */
/*** Gets the width of the OS scrollbar */
(function($) {var scrollbarWidth=0; $.getScrollbarWidth=function(){var inner=document.createElement('p'); inner.style.width="100%"; inner.style.height="200px"; var outer=document.createElement('div'); outer.style.position="absolute"; outer.style.top="0px"; outer.style.left="0px"; outer.style.visibility="hidden"; outer.style.width="200px"; outer.style.height="150px"; outer.style.overflow="hidden"; outer.appendChild(inner); document.body.appendChild(outer); var w1 = inner.offsetWidth; outer.style.overflow='scroll'; var w2=inner.offsetWidth; if (w1==w2) w2=outer.clientWidth; document.body.removeChild(outer); return (w1-w2);};})(jQuery);
 
 
/* Copyright (c) 2010 Brandon Aaron (http://brandonaaron.net)
 * Dual licensed under the MIT (MIT_LICENSE.txt) and GPL Version 2 (GPL_LICENSE.txt) licenses.
 * Version: 1.1.1. Requires jQuery 1.3+. Docs: http://docs.jquery.com/Plugins/livequery */
(function(a){a.extend(a.fn,{livequery:function(e,d,c){var b=this,f;if(a.isFunction(e)){c=d,d=e,e=undefined}a.each(a.livequery.queries,function(g,h){if(b.selector==h.selector&&b.context==h.context&&e==h.type&&(!d||d.$lqguid==h.fn.$lqguid)&&(!c||c.$lqguid==h.fn2.$lqguid)){return(f=h)&&false}});f=f||new a.livequery(this.selector,this.context,e,d,c);f.stopped=false;f.run();return this},expire:function(e,d,c){var b=this;if(a.isFunction(e)){c=d,d=e,e=undefined}a.each(a.livequery.queries,function(f,g){if(b.selector==g.selector&&b.context==g.context&&(!e||e==g.type)&&(!d||d.$lqguid==g.fn.$lqguid)&&(!c||c.$lqguid==g.fn2.$lqguid)&&!this.stopped){a.livequery.stop(g.id)}});return this}});a.livequery=function(b,d,f,e,c){this.selector=b;this.context=d;this.type=f;this.fn=e;this.fn2=c;this.elements=[];this.stopped=false;this.id=a.livequery.queries.push(this)-1;e.$lqguid=e.$lqguid||a.livequery.guid++;if(c){c.$lqguid=c.$lqguid||a.livequery.guid++}return this};a.livequery.prototype={stop:function(){var b=this;if(this.type){this.elements.unbind(this.type,this.fn)}else{if(this.fn2){this.elements.each(function(c,d){b.fn2.apply(d)})}}this.elements=[];this.stopped=true},run:function(){if(this.stopped){return}var d=this;var e=this.elements,c=a(this.selector,this.context),b=c.not(e);this.elements=c;if(this.type){b.bind(this.type,this.fn);if(e.length>0){a.each(e,function(f,g){if(a.inArray(g,c)<0){a.event.remove(g,d.type,d.fn)}})}}else{b.each(function(){d.fn.apply(this)});if(this.fn2&&e.length>0){a.each(e,function(f,g){if(a.inArray(g,c)<0){d.fn2.apply(g)}})}}}};a.extend(a.livequery,{guid:0,queries:[],queue:[],running:false,timeout:null,checkQueue:function(){if(a.livequery.running&&a.livequery.queue.length){var b=a.livequery.queue.length;while(b--){a.livequery.queries[a.livequery.queue.shift()].run()}}},pause:function(){a.livequery.running=false},play:function(){a.livequery.running=true;a.livequery.run()},registerPlugin:function(){a.each(arguments,function(c,d){if(!a.fn[d]){return}var b=a.fn[d];a.fn[d]=function(){var e=b.apply(this,arguments);a.livequery.run();return e}})},run:function(b){if(b!=undefined){if(a.inArray(b,a.livequery.queue)<0){a.livequery.queue.push(b)}}else{a.each(a.livequery.queries,function(c){if(a.inArray(c,a.livequery.queue)<0){a.livequery.queue.push(c)}})}if(a.livequery.timeout){clearTimeout(a.livequery.timeout)}a.livequery.timeout=setTimeout(a.livequery.checkQueue,20)},stop:function(b){if(b!=undefined){a.livequery.queries[b].stop()}else{a.each(a.livequery.queries,function(c){a.livequery.queries[c].stop()})}}});a.livequery.registerPlugin("append","prepend","after","before","wrap","attr","removeAttr","addClass","removeClass","toggleClass","empty","remove","html");a(function(){a.livequery.play()})})(jQuery);



/*** hoverIntent r6 // 2011.02.26 // jQuery 1.5.1+ <http://cherne.net/brian/resources/jquery.hoverIntent.html>
* @author    Brian Cherne brian(at)cherne(dot)net
*/
(function($){$.fn.hoverIntent=function(f,g){var cfg={sensitivity:7,interval:100,timeout:0};cfg=$.extend(cfg,g?{over:f,out:g}:f);var cX,cY,pX,pY;var track=function(ev){cX=ev.pageX;cY=ev.pageY};var compare=function(ev,ob){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);if((Math.abs(pX-cX)+Math.abs(pY-cY))<cfg.sensitivity){$(ob).unbind("mousemove",track);ob.hoverIntent_s=1;return cfg.over.apply(ob,[ev])}else{pX=cX;pY=cY;ob.hoverIntent_t=setTimeout(function(){compare(ev,ob)},cfg.interval)}};var delay=function(ev,ob){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);ob.hoverIntent_s=0;return cfg.out.apply(ob,[ev])};var handleHover=function(e){var ev=jQuery.extend({},e);var ob=this;if(ob.hoverIntent_t){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t)}if(e.type=="mouseenter"){pX=ev.pageX;pY=ev.pageY;$(ob).bind("mousemove",track);if(ob.hoverIntent_s!=1){ob.hoverIntent_t=setTimeout(function(){compare(ev,ob)},cfg.interval)}}else{$(ob).unbind("mousemove",track);if(ob.hoverIntent_s==1){ob.hoverIntent_t=setTimeout(function(){delay(ev,ob)},cfg.timeout)}}};return this.bind('mouseenter',handleHover).bind('mouseleave',handleHover)}})(jQuery);



/* Pre load images */
(function($) {
	var cache = [];
	// Arguments are image paths relative to the current page.
	$.preLoadImages = function() {
		var args_len = arguments.length;
		for (var i = args_len; i--;) {
			var cacheImage = document.createElement("img");
			cacheImage.src = arguments[i];
			cache.push(cacheImage);
		}
	}
})(jQuery);


//Restyles form selects and search 
(function($){
	$.fn.extend({
 		styleSelects : function(options) {
	 		if(!$.browser.msie || ($.browser.msie&&$.browser.version>6)){
				return this.each(function() {
					var $this = $(this);
					$this.wrap('<div class="select-wrap"></div>');
					var currentSelected = $this.find(':selected');
					$this.after('<span class="styleSelectWrap"><span class="styleSelect select-text" value="' + currentSelected.val() + '">' + currentSelected.text() + '</span></span>').css({opacity:0, width:(parseInt($this.next().css("width")) + 3)});
					var $styleSelectWrap = $this.next(), $styleSelect = $styleSelectWrap.children();	
					$this.change(function(){
						$styleSelect.text($this.find(':selected').text()).attr("value", $this.find(':selected').val()).removeClass("select-text");
					});
		  		});
	  		}
		}
 	});
})(jQuery);


//$.unique enhancement from Paul Irish (Duck Punching with jQuery!)
(function($){
    var _old = $.unique;
    $.unique = function(arr){
        // do the default behavior only if we got an array of elements
        if (!!arr[0].nodeType){
            return _old.apply(this,arguments);
        } else {
            // reduce the array to contain no dupes via grep/inArray
            return $.grep(arr,function(v,k){
                return $.inArray(v,arr) === k;
            });
        }
    };
})(jQuery);


//Check heights of children and apply inline height style
(function($) {
    $.fn.sameHeightCols = function(options) {
		var defaults = {  
			groupByContainerClass:false
     	};
        var options = $.extend(defaults, options); 
		
		$(this).each(function(){
			var h = 0, hh = 0;
			
			if(options.groupByContainerClass){
				var $this = $(this),
					classArray = [];
				$this.children().each(function(i){
					var $thisSection = $(this),
						currClass = $thisSection.attr("class"),
						nextClass = $thisSection.next().attr("class");
					
					if(nextClass == currClass){
						classArray.push(currClass);
					}
				});
				classArray = $.unique(classArray);
				
				var len = classArray.length;
				for(var i=0; i<len; i++){
					$this.children("." + classArray[i]).each(function(){
						$thisPanelInner = $(this).find(".panel-inner");
						$thisPanelContent = $(this).find(".panel-content");
						if($thisPanelContent.height() > h){
							h = $thisPanelContent.height();
						}
						if($thisPanelInner.height() > hh){
							hh = $thisPanelInner.height();
						}
					});
					$this.children("." + classArray[i]).find(".panel-inner").css({height:hh + "px" }).children(".panel-content").css({height:h + "px" });
				}
			} else {
				$(this).children().each(function(){
					if($(this).height() > h){
						h = $(this).height();
					}
				});
				$(this).children().css({height:h + "px" });
			}
		})
		return this;
	}
})(jQuery); 

//Special plugin for first main nav item
(function($) {
    $.fn.sameHeightColsFirstDropdown = function(options) {
		
		$(this).each(function(){
			var h = 0, hh = 0;

			$(this).children().each(function(){
				if($(this).height() > h){
					h = $(this).height();
				}
			});
			$(this).children().css({height:h + "px" });
		})
		return this;
	}
})(jQuery); 


/* Cookie Functions */
function createCookie(name,value,days) {
    if (days) {
		var date = new Date();
		date.setTime(date.getTime()+(days*24*60*60*1000));
		var expires = "; expires="+date.toGMTString();
	}
	else var expires = "";
    	document.cookie = name+"="+value+expires+"; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for(var i=0;i < ca.length;i++) {
		var c = ca[i];
		while (c.charAt(0)==' ') c = c.substring(1,c.length);
		if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
    }
    return null;
}

function eraseCookie(name) {
    createCookie(name,"",-1);
}


//Find percentage and convert to degrees
(function($) {
    $.fn.pcToDegree = function(options) {
		$(this).each(function(){
			var h = 0;
			$(this).children().each(function(){
				if($(this).height() > h){
					h = $(this).height();
				}
			});
			$(this).children().css({height:h + "px" });
		})
		return this;
	}
})(jQuery); 


//Check if device is iPhone 
var isMobileDevice = false;

if((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i)) || (navigator.userAgent.match(/blackberry.*/))  || (navigator.userAgent.match(/android/i)) ) {

   isMobileDevice = true;
   $("body").addClass("isMobileDevice");
}


//Overlay plugin: builds overlays for popups for member login/kids and also club map/streetview
(function($) {
    $.fn.vaOverlay = function(options) {
		
        var defaults = {  
			updateClasses: false,
			replaceDivContent: true,
			showMap: false,
			showStreetview: false,
			updateClubFilterList: false
     	};  
        
        var options = $.extend(defaults, options); 
		
		var $overlayBg = $("<div id='va-overlay-bg'></div>"),
			$overlayDiv = $('<div id="va-overlay" class="va-overlay"></div>'),
			$overlayWrap = $("#va-overlay-wrap"),
			overlayContent, 
			$html = $("html");
			
        return this.each(function() {
					  
			$(this).click(function(e) {
				e.preventDefault();
				
				$("body").append($overlayBg.click(function() {closeOverlay(); }));
				$overlayBg.css({opacity:0.8 }).fadeIn(150);
				$(document).keypress(handleEscape);
				
				//Add content
				if(options.replaceDivContent){
					overlayContent = $overlayWrap.html();
					$overlayWrap.children("#overlay").replaceWith('<div id="va-overlay-temp" />');
				}
				if(options.showMap){
					if($html.hasClass("no-touch")){
						var $overlayInfo = $(this).closest(".map-overlay-parent").next("#map-overlay-info");
						addMapContent($overlayInfo);
					} else {
						closeOverlay(); 
						document.location = "http://maps.google.com/maps?q=" + $(".vcard .lat").text() + "," + $(".vcard .lng").text();
					}
				}
				if (options.updateClubFilterList){
					var $overlayLink = $(this);
					addClubFilters($overlayLink);
				}
				
				//Add overlay content, assign focus to the overlay, active close button and ensure ovelay stays central in the browser
				$("body").append($overlayDiv.append(overlayContent));
				$('<div id="close-overlay"><a href="#closebutton" class="rep"><span></span>Close</a></div>').prependTo($overlayDiv);
				if($html.is(".ie6, .ie7, .ie8")){
					$overlayDiv.css({display:"block" }).find("#close-overlay a").click(function(e) {e.preventDefault(); closeOverlay(); }).end().attr("tabIndex",-1).focus();
				} else {
					$overlayDiv.fadeIn(50).css({opacity:1 }).find("#close-overlay a").click(function(e) {e.preventDefault(); closeOverlay(); }).end().attr("tabIndex",-1).focus();
				}
				
				//Position overlay
				getViewport();
				if (!isMobileDevice){
					window.onresize = getViewport;
				} 

				
           		if(options.updateClasses) {
					var $classType = $(this).closest(".details").children("h3"), 
						guid = $classType.data("guid");
					$overlayDiv.find(".update-class").text($classType.text()).attr("data-guid", guid);
					clubFinderAutocomplete();
				}
				
				if(options.showMap){
					$.va_init.functions.clubsMapOverlay();
				}
				
				if(options.showStreetview){
					$.va_init.functions.streetview();
				}
				
			});
		});
		
		
		function addClubFilters($overlayLink){
			var addClubNames,
				overlayTitle = $overlayLink.data("list-title"),
				overlayIntro = $overlayLink.data("list-intro"),
				overlayLabel = $overlayLink.data("list-label"),
				overlayFilter = $overlayLink.data("filter"),
                overlayExcludeEx = $overlayLink.data("excludeex"),
				overlayFormSubmit = $overlayLink.data("form-submit"),
				overlayListUrl = $overlayLink.data("list-url"),
                clubTrueName = '';
				
			overlayContent = '<div id="xxva-overlay-wrap" class="va-overlay-mini">';
            overlayContent += '    <div id="overlay">';
            overlayContent += '        <h2>' + overlayTitle + '</h2>';
            overlayContent += '        <div id="clubfinderform">';
            overlayContent += '            <p>' + overlayIntro + '</p>';
            overlayContent += '            <fieldset class="find">';
            overlayContent += '                <label class="ffb" for="findclub">' + overlayLabel + '</label>';
			overlayContent += '				   <div id="add-club-names" />';
            overlayContent += '            </fieldset>';
            overlayContent += '        </div>';
			overlayContent += '		</div>';
			overlayContent += '</div>';
			
			var homePageUrl = "";
            var arr1 = document.getElementsByTagName("input");
            for (i = 0; i < arr1.length; i++) {
                if (arr1[i].className == "homePageUrl") {
                    homePageUrl = arr1[i].value;
                }
            }
                			
			$.ajax({
				url: homePageUrl,
				dataType: "json",
				data: {
					ajax: 1,
					cmd: "ClubNamesList",
					filter: overlayFilter,
                    excludeEx: overlayExcludeEx
				},
		
				success: function (data) {
					if (data) {
						var isiPad,
							isTouchNotiPad;
						if (navigator.userAgent.match(/iPad/i)) {
							isiPad = true;
							$html.addClass("iPad");
						} else if (($html.hasClass("touch")) && (!navigator.userAgent.match(/iPad/i))) {
							isTouchNotiPad = true;
						}
						
						addClubNames = '<select class="chosen-select">';
						addClubNames += '<option value="">Select your club...</option>';
						$.each(data, function (key, val) {
							addClubNames += '<option data-name="' + val.name + '" value="' + val.memberLoginUrl + '" data-club-email="' + val.email + '">' + val.label + '</option>';
						});
						
						addClubNames += "</select>";
						$("#add-club-names").replaceWith(addClubNames);
						
						$clubNameSelect = $("#va-overlay .chosen-select");
						
						
						//iPad has styled selects, iPhone/ie6 is not styled, rest uses form porn
						if(isiPad){
							$clubNameSelect.styleSelects().change(function() {
								calculateClubUrl();
							});
						} else if(isTouchNotiPad || $html.hasClass("ie6")){
							$clubNameSelect.change(function() {
								calculateClubUrl();
							});
						} else {
							$clubNameSelect.chosen({"hasSearch":true}).change(function() {
								calculateClubUrl();
							});
						}
					}
				}
			})

			//Grab correct url depending on which overlay is used
			function calculateClubUrl(){
				if(overlayListUrl === "MemberLoginUrl"){                    
					overlayListUrl = $clubNameSelect.children("option:selected").val();
				} else {
					overlayListUrl = overlayListUrl.replace("clubname", $clubNameSelect.children("option:selected").data("name"));
				}
				if(overlayFormSubmit){
                    //JONFIX!!!
                    if (overlayListUrl.indexOf("undefined") == -1)
                    {
					    window.location.href = overlayListUrl;
					    closeOverlay();
                    }
                    else
                    {
                        //error finding club in drop down -do nothing and reset url
                        overlayListUrl = $overlayLink.data("list-url");
                    }
                    //ENDFIX
				} 
			}

		}
		
		function addMapContent($mapInfo){
			var $titles = $mapInfo.children(".maptitle");
			
			overlayContent = '<div id="overlay" class="map-overlay">';
			overlayContent += '<h2>' + $titles.text() + '</h2>';
			overlayContent += '<div id="content">';
			overlayContent += 	'<div class="aside"><h3 class="get-directions-title">' + $titles.data("getdirections") + '</h3><div class="get-directions">';
            overlayContent += 		'<div id="directions-panel"><ul class="transport"><li><a href="#" id="driving" class="rep"><span class="active"></span>Driving</a></li><li><a href="#" id="walking" class="rep"><span></span>Walking</a></li></ul>'; 
			overlayContent += 		'<ul id="directions-form"><li class="from"><div class="marker">A</div><input type="text" placeholder="' + $titles.data("starting") + '" /></li><li class="to"><div class="marker">B</div><input type="text" placeholder="' + $mapInfo.children(".clubname").text() + '" disabled="disabled" /></li></ul>';
            overlayContent += 		'</div>';
			overlayContent += 		'<p id="reverse" class="rep"><a href="#"><span></span>Get reverse directions</a></p>';
            overlayContent += 		'<a href="#" class="btn btn-cta-large">' + $titles.data("getdirections") + '</a></div>';
			overlayContent += 		'<div class="lower-panel">';
			overlayContent +=			'<div class="tab-upper"><h3 class="address-tab"><a href="#" class="active">' + $titles.data("address") + '</a></h3> <h3 class="directions-tab"><a href="#" class="inactive">' + $titles.data("directions") + '</a></h3></div>';
			overlayContent +=			'<div class="tab-lower">';
			overlayContent +=				'<div class="tab-detail location"><ul>';
			overlayContent +=					'<li class="marker"><div class="location-address">';
			overlayContent +=					'<span class="location-title"><a href="#" data-lat="' + $mapInfo.find(".lat").text() + '" data-lng="' + $mapInfo.find(".lng").text() + '">' + $mapInfo.children(".clubname").text() + '</a></span>';
			overlayContent +=					$mapInfo.children(".adr").html();
			overlayContent +=					'</div></li>';
			overlayContent +=					'<li>' + $mapInfo.children(".tel").html(); + '</li>';
			overlayContent +=					'<li class="info">' + $mapInfo.children(".other-info").html() + '</li>';
			overlayContent +=				'</ul></div>';
			overlayContent +=				'<div class="tab-detail directions"><div class="routes-list"><h4>' + $titles.data("suggestedroutes") + '</h4></div><ul class="directions-list"></ul></div>';
			overlayContent +=			'</div>';
			overlayContent +=		'</div>';
			overlayContent +=	'</div>';
			overlayContent += 	'<div id="primary-r"><div id="title-wrap"><h3>' + $titles.data("map") + '</h3>';
			overlayContent += 	'<ul class="icon-list"><li><a class="icon-print" href="#">Print</a></li></ul></div><div id="map-large"></div></div>';
			overlayContent += '</div>';
		}
		
		function closeOverlay() {
			$(document).unbind("keypress", handleEscape)
			var remove = function() {
				$(this).remove();
			}
			$overlayBg.fadeOut(remove);
			$overlayDiv.fadeOut(remove).empty(); 
			$("body").removeClass("hasMapOverlay");
			if($(".pac-container").length > 0){
				$(".pac-container").css({display:"none" });
			}
			if(options.replaceDivContent){
				$("#va-overlay-temp").replaceWith(overlayContent);
			}
		}
		function handleEscape(e) {
			if (e.keyCode == 27) {
				closeOverlay();
			}
		}
		function getViewport() {
			var scrollBarWidth;
			if (typeof window.innerWidth != 'undefined') {
				scrollBarWidth = $.getScrollbarWidth();
				winWidth = window.innerWidth - scrollBarWidth,
				winHeight = window.innerHeight;
			} else if (typeof document.documentElement != 'undefined' && typeof document.documentElement.clientWidth != 'undefined' && document.documentElement.clientWidth != 0) {
				scrollBarWidth = 17;
				winWidth = document.documentElement.clientWidth,
				winHeight = document.documentElement.clientHeight;
			} 
			offsetLeft = (((winWidth - $overlayDiv.width())/2) > 0) ? ((winWidth - $overlayDiv.width())/2) : 0;
			offsetTop = (((winHeight - $overlayDiv.height())/2) > 0) ? ((winHeight - $overlayDiv.height())/2) : 0;
			
			if(isMobileDevice){
				$overlayDiv.css({top:0, left:0 }, 200);
			} else {
				$overlayDiv.css({top:offsetTop, left:offsetLeft }, 200);
			}
		}
    }
})(jQuery); 



/*
 * jQuery Orbit Plugin 1.2.3
 * www.ZURB.com/playground
 * Copyright 2010, ZURB
 * Free to use under the MIT license.
 * http://www.opensource.org/licenses/mit-license.php
*/
/*  test  */
(function($) {

    $.fn.orbit = function(options) {

        //Defaults to extend options
        var defaults = {  
			resize: true,						// should carousel resize?
            animation: 'horizontal-push', 		// fade, horizontal-push
            animationSpeed: 1000, 				// how fast slide animations are
            timer: false, 						// true or false to have the timer
            advanceSpeed: 10000, 				// if timer is enabled, time between transitions 
            pauseOnHover: false, 				// if you hover pauses the slider
            startClockOnMouseOut: false, 		// if clock should start on MouseOut
            startClockOnMouseOutAfter: 500, 	// how long after MouseOut should the timer start again
            directionalNav: false, 				// manual advancing directional navs
            captions: false, 					// do you want captions?
            captionAnimation: 'fade', 			// fade, slideOpen, none
            controlsAnimationSpeed: 300, 		// if so how quickly should they animate in
            bullets: false,						// true or false to activate the bullet navigation
            bulletThumbs: false,				// thumbnails for the bullets
			rotateIcons: false,					// rotate icons as image changes?
			hotspots: false,					// add hotspots?
			thumbnails: false,					// add thumbnails?
			viewGallery: false,					// add view gallery link to club pages
            afterSlideChange: function(){} 		// empty function 
     	};  
        
        //Extend those options
        var options = $.extend(defaults, options); 
	
        return this.each(function() {
        
// ==============
// ! SETUP   
// ==============
            
            //Global Variables
            var activeSlide = 0,
				nextActiveSlide = 1,
            	numberSlides = 0,
            	orbitWidth,
            	orbitHeight,
            	locked,
           		scrollOffset = 0,
				headerOffset = $("#carousel-wrap").offset().top || 0,
				hotspot_timer, noTransformsTimer, clubsHeaderOffset,
				$html = $("html"),
				origX, finalX, changeX;
			
			var imgWidth = $("#carousel").find("img").width(),
				imgHeight = $("#carousel").find("img").height(),
				imgAR = imgWidth / imgHeight;
				
            //Initialize
            var orbit = $(this).addClass("orbit"),
				$carouselWrap = $("#carousel-wrap"),
				$carouselControls = $("#carousel-controls"),
				$viewPhotos = $("#view-photos"),
				$clubsPanel = $("#clubs"),
				$slides = $carouselWrap.children("#carousel").children("li").addClass("slide"),
				$carouselThumbs = $("#carousel-thumbs"),
				$thumbs = $carouselThumbs.find("li"),
				numberSlides = $slides.length;
			
			
			//Add numbered slides for hotspot positioning
			$slides.each(function(i) {
				$(this).attr("id", "slide" + i).children("img").css({display:"block" });
			});
			
			

			
			$slides.eq(activeSlide).addClass("slide-active").css({left:"0", display:"none" }).delay(200).fadeIn();
			
            //Animation locking functions
            function unlock() {
                locked = false;
            }
            function lock() { 
                locked = true;
            }
            
            //If there is only a single slide remove nav, timer and bullets
            if($slides.length == 1) {
            	options.directionalNav = false;
            	options.timer = false;
            	options.bullets = false;
            }
			
			
			var resizeTimer, timerWasRunning, nextPrevArrows, viewGalleryLink, viewPhotosLinkOffset, visibleWinHeight, clubHeaderHeight, outerWinWidth, outerWinHeight, winAR;
			
			onScreenResize();
			window.onresize = onScreenResize;
			
			function onScreenResize() {
				var scrollBarWidth;
				if (typeof window.innerWidth != 'undefined') {
					scrollBarWidth = $.getScrollbarWidth();
					winWidth = window.innerWidth - scrollBarWidth,
					winHeight = window.innerHeight - headerOffset;
					outerWinWidth = window.document.body.clientWidth;
					outerWinHeight = window.document.body.clientHeight;
				} else if (typeof document.documentElement != 'undefined' && typeof document.documentElement.clientWidth != 'undefined' && document.documentElement.clientWidth != 0) {
					scrollBarWidth = 17;
					winWidth = document.documentElement.clientWidth,
					winHeight = document.documentElement.clientHeight - headerOffset;
				} 
				
				finalX = 0;  //Reset finalX co-ord if screen orientation changes, to avoid activating shift()
				winWidth = Math.max(winWidth - scrollOffset, 1020);
				winHeight = Math.max(winHeight, 404);
				
                $("#carousel-wrap, #carousel, #carousel_hotspots").width(winWidth).height(winHeight);
				$carouselThumbs.css({left:(winWidth - ($carouselThumbs.width()+5))/2 + "px" });
				$slides.not(":eq(" + activeSlide + ")").css({left:"-" + winWidth + "px" });
				
				if(options.viewGallery){
					nextPrevArrows = (winWidth-980)/2;
					viewGalleryLink = (winWidth/2)+490-$viewPhotos.innerWidth();
					visibleWinHeight = winHeight + headerOffset;
					clubHeaderHeight = ($clubsPanel.children("h1").outerHeight() + $clubsPanel.children(".subnav").outerHeight());
					viewPhotosLinkOffset = ($viewPhotos.offset().top + $viewPhotos.outerHeight());
					$viewPhotos.css({left:viewGalleryLink + "px", display:"block" });
					
					if(!$viewPhotos.children("a").hasClass("view-photos-open")){
						$clubsPanel.css({marginTop:"-" + (visibleWinHeight - viewPhotosLinkOffset) + "px" });
					} 
				}
				
				if (clock != null){
					if (timerRunning){
						timerWasRunning = true;
						stopClock();
					}
					if (timerWasRunning){
						if (resizeTimer) clearTimeout(resizeTimer);
					    resizeTimer = setTimeout(startClock, 1000);
					}
				}
				
				winAR = winWidth / winHeight;
				
				$slides.each(function() {
					var $slide = $(this);
					if (imgAR > winAR) {
						$slide.children("img").height(winHeight).width(winHeight * imgAR);
						
						if($html.hasClass("no-touch")){
							centerImage($slide);
						} else {
							//Centre images on smaller devices with no scrollbars (portrait)
							$slide.children("img").css({left:"-" + ((winHeight * imgAR)/4) + "px" });
						}
					} else {
						$slide.children("img").width(winWidth).height(winWidth / imgAR);
						if($html.hasClass("no-touch")){
							centerImage($slide);
						} else {
							//Reset images on smaller devices with no scrollbars (landscape)
							$slide.children("img").css({left:"0" });
						}
					}
				})
			}

			function centerImage($slide){
				var $img = $slide.find("img"), img_width = $img.width(), slide_width = $slide.width(), left = 0, img_height = $img.height(), slide_height = $slide.height(), top = 0;

				if(img_width > slide_width){
					left = left - ((img_width - slide_width) / 2);
				}

				if(img_height > slide_height){
					top = top - ((img_height - slide_height) / 2);
				}

				$img.css('left',left)

			}
			
			if($html.hasClass("touch")){
				swipeThreshold = 30;
				var orientation = window.orientation;
				
				function touchStart(event) {
					origX = event.targetTouches[0].pageX;
				}
				function touchMove(event) {
					finalX = event.targetTouches[0].pageX; 
				}
				function touchEnd(event) {
					changeX = finalX - origX;
					
					//If carousel swiped left, shift carousel and restart the clock
					if((finalX > 0) && (origX > finalX) && (changeX < -swipeThreshold)){
						shift("next");  
						startClock("resetClock");
					//Else if carousel swiped right
					} else if ((finalX > 0) && (origX < finalX) && (changeX > swipeThreshold)) {
						shift("prev"); 
						startClock("resetClock");
					}
				}
				// Add gestures to all swipable areas
				this.addEventListener("touchstart", touchStart, false);
				this.addEventListener("touchmove", touchMove, false);
				this.addEventListener("touchend", touchEnd, false);
			}
            
// ==============
// ! TIMER   
// ==============

            //Timer Execution
            function startClock(option) {
            	if(!options.timer  || options.timer == 'false') { 
            		return false;
            	//if timer is hidden, don't need to do crazy calculations
            	} else if(timer.is(':hidden')) {
		            clock = setInterval(function(e){
						shift("next");  
		            }, options.advanceSpeed);            		
		        //if timer is visible and working, let's do some math
            	} else {
					if(option === "resetClock"){
						clearInterval(clock);
						if(degrees > 180) {
							rotator.removeClass("move");
							mask.removeClass("move");
						}
						degrees = 0;
					}
		            timerRunning = true;
		            pause.removeClass("active");
		            clock = setInterval(function(e){
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
								shift("next");
							}
						}
		            }, options.advanceSpeed/180);
					
					if ($html.hasClass("no-csstransforms")){
						noTransformsTimer = setInterval(function (){ timerForIe() }, 10000);
						$("#timer").css({opacity:0 }).animate({opacity:1 }, 9800); 
					}
					
				}
	        }
			
			function timerForIe() {
				if(timerRunning){
					$("#timer").css({opacity:0 }).stop(true, true).animate({opacity:1 }, 10000 );
					shift("next");
				}
			}
			
			
	        function stopClock() {
	        	if(!options.timer || options.timer == "false") { return false; } else {
		            timerRunning = false;
		            clearInterval(clock);
					clearInterval(noTransformsTimer);
		            pause.addClass("active");
				}
	        }  
            
            //Timer Setup
            if(options.timer) {         	
				var timerHTML = '<div id="timer"><span id="mask"><span id="rotator"></span></span><span id="pause"></span></div>';
				$carouselControls.append(timerHTML);

				if (!$html.hasClass("csstransforms")){
					$("#timer").parent("#carousel-controls").addClass("timer_ie");
				}
				
                var timer = $('div#timer'),
                	timerRunning;
                if(timer.length != 0) {
                    var rotator = $('div#timer span#rotator'),
                    	mask = $('div#timer span#mask'),
                    	pause = $('div#timer span#pause'),
                    	degrees = 0,
                    	clock; 
                    startClock();
                    timer.click(function() {
                        if(!timerRunning) {
                            startClock();
                        } else { 
                            stopClock();
                        }
                    });
                    if(options.startClockOnMouseOut){
                        var outTimer;
                        $carouselWrap.mouseleave(function() {
                            outTimer = setTimeout(function() {
                                if(!timerRunning){
                                    startClock();
                                }
                            }, options.startClockOnMouseOutAfter)
                        })
                        $carouselWrap.mouseenter(function() {
                            clearTimeout(outTimer);
                        })
                    }
                }
            }  

			//Stop carousel if other interactive elements are clicked
			$("#streetview-wrap a, #member-login a, .showmore p.show a").click(function() {
				stopClock(); 
			});
			$(".hotspot").live("click", function () {
				stopClock(); 
			});
			
	        //Pause Timer on hover
	        if(options.pauseOnHover) {
		        $carouselWrap.mouseenter(function() {
		        	stopClock(); 
		        });
		   	}
            
// ==============
// ! CAPTIONS   
// ==============
                    
            //Caption Setup
            if(options.captions) {
                var i, captionHTML = '<ul id="carousel-captions">';
				for (i=0; i<numberSlides; i++){
					if($slides.eq(i).find("img").data("promoclass") == "carousel-promo"){
						captionHTML += '<li class="carousel-promo"><span>' + $slides.eq(i).find("img").attr("alt") + ' </span>' + $slides.eq(i).find("img").data("caption") + '</li>';
					} else {
						captionHTML += '<li><span>' + $slides.eq(i).find("img").attr("alt") + ' </span>' + $slides.eq(i).find("img").data("caption") + '</li>';
					}
				}
				captionHTML += '</ul>';
                $(captionHTML).insertBefore(".carousel-btns");
				
				var $carouselCaptions = $("#carousel-captions li");
				$carouselCaptions.eq(0).css({display:"block" });
				$carouselCaptions.not($carouselCaptions.eq(activeSlide)).css({opacity:0 });
            }
			
			function updateCaption(prevActiveSlide){
            	if(!options.captions || options.captions =="false") {
            		return false; 
            	} else {
					$carouselCaptions.eq(prevActiveSlide).stop(true, true).animate({opacity:0 }, options.controlsAnimationSpeed, function() {
						$carouselCaptions.eq(prevActiveSlide).css({display:"none" }).end().eq(activeSlide).stop(true, true).animate({opacity:1 }).css({display:"block" });
					});
				}
			}
			
            
// ==================
// ! DIRECTIONAL NAV   
// ==================

            if(options.directionalNav) {
                var directionalNavHTML = '<div id="carousel-arrows"><a class="left">Left</a><a class="right">Right</a></div>';
                $carouselWrap.append(directionalNavHTML);
                var $carouselBtn = $carouselWrap.children("#carousel-arrows").children("a"),
					$leftBtn = $carouselWrap.children("#carousel-arrows").children(".left"),
                	$rightBtn = $carouselWrap.children("#carousel-arrows").children(".right");
				
				if(!$html.is(".ie7, .ie8")){
					$carouselBtn.hover(function(){
						$(this).stop(true, true).animate({opacity:1 });
					}, function(){
						$(this).stop(true, true).animate({opacity:0.7 });
					});
				} 
				
                $leftBtn.click(function() {
                    shift("prev");
                });
                $rightBtn.click(function() {
                    shift("next")
                });
				
				moveArrows();
				$(window).resize(function() {
					moveArrows();
				});

            }
			
			function moveArrows(){
				$leftBtn.css({left:nextPrevArrows + "px" });
				$rightBtn.css({right:nextPrevArrows + "px" });
			}
            
// ==================
// ! THUMBNAILS  
// ==================

			if(options.thumbnails) { 
				setThumb();
				
				$carouselThumbs.find("a").click(function(e){
					e.preventDefault();
					shift($("#carousel-thumbs a").index($(this)));
				});
			}
			
			function setThumb(){
				if(!options.thumbnails || options.thumbnails == "false") { return false; }
				
				$carouselThumbs.find(".border").remove();
				$carouselThumbs.find("a").removeClass("active").end().find("li:eq(" + activeSlide + ") a").addClass("active");
				$carouselThumbs.find(".active").append('<div class="border" />');
			}
			
			
			
// ==================
// ! VIEW GALLERY   
// ==================
            

			if(options.viewGallery){
				var clubsMargin = ($clubsPanel.innerHeight() - $clubsPanel.outerHeight(true));
				var $viewPhotosLink = $viewPhotos.children("a"),
					textViewPhotos = $viewPhotosLink.data("viewphotos"),
					textCloseGallery = $viewPhotosLink.data("closegallery"),
					viewPhotosLinkOffset = ($viewPhotos.offset().top + $viewPhotos.outerHeight()),
					viewCloseGalleryText = textCloseGallery;
				
                if($('body').hasClass("gallery_open") === false){
                    viewCloseGalleryText = textViewPhotos;
                }

				$viewPhotosLink.text(viewCloseGalleryText);
				
				if(viewCloseGalleryText == textCloseGallery){
					$clubsPanel.css({marginTop:"-" + clubHeaderHeight + "px" });
					$viewPhotosLink.addClass("view-photos-open");
				} else {
					$clubsPanel.css({marginTop:"-" + (visibleWinHeight - viewPhotosLinkOffset) + "px" });
					$viewPhotosLink.removeClass("view-photos-open");
				}
				
				$viewPhotos.children("a").click(function(e){
					e.preventDefault();
					var $this = $(this),
						text = $this.text(),
						gaqClubName;
						
					$this.toggleClass("view-photos-open").text(text == textViewPhotos ? textCloseGallery : textViewPhotos).blur();
					gaqClubName = $this.closest("#carousel-wrap").next().children("h1").clone().find("span").remove().end().text();
					
					if($this.text() == textViewPhotos) {
                        if (typeof (_gaq) !== "undefined") {
						    _gaq.push(["_trackEvent", "Gallery", "Close", gaqClubName]);
                        }
						//update session and cookie -closing gallery
                        CallSetGalleryPreference(false);
						if ($html.is(".ie6, .ie7, .ie8, .touch")){
							$clubsPanel.css({marginTop:"-" + (visibleWinHeight - viewPhotosLinkOffset) + "px" });
						} else {
							$clubsPanel.stop(true, true).animate({marginTop:"-" + (visibleWinHeight - viewPhotosLinkOffset) + "px" }, 2000, "easeOutQuint");
						}
					} else {
                        if (typeof (_gaq) !== "undefined") {
						    _gaq.push(["_trackEvent", "Gallery", "Opening", gaqClubName]);
                        }
					    //update session and cookie -opening gallery
                        CallSetGalleryPreference(true);
						if($html.is(".ie6, .ie7, .ie8, .touch")){
							$clubsPanel.css({marginTop:"-" + clubHeaderHeight + "px" });
						} else {
							$clubsPanel.stop(true, true).animate({marginTop:"-" + clubHeaderHeight + "px" }, 2000, "easeOutQuint");
						}
					}
				});
			}
		 
// ==================
// ! BULLET NAV   
// ==================
            
            if(options.bullets) { 
            	var bulletHTML = '<ul class="orbit-bullets"></ul>';            	
            	$carouselWrap.append(bulletHTML);
            	var bullets = $('ul.orbit-bullets');
            	for(i=0; i<numberSlides; i++) {
            		var liMarkup = $('<li>'+(i+1)+'</li>');
            		if(options.bulletThumbs) {
            			var	thumbName = $slides.eq(i).data('thumb');
            			if(thumbName) {
            				var liMarkup = $('<li class="has-thumb">'+i+'</li>')
            				liMarkup.css({"background" : "url("+options.bulletThumbLocation+thumbName+") no-repeat"});
            			}
            		} 
            		$('ul.orbit-bullets').append(liMarkup);
            		liMarkup.data('index',i);
            		liMarkup.click(function() {
            			stopClock();
            			shift($(this).data('index'));
            		});
            	}
            	setActiveBullet();
            }
            
        	function setActiveBullet() { 
        		if(!options.bullets) { return false; } 
	        	bullets.children('li').removeClass('active').eq(activeSlide).addClass('active');
        	}


// ====================
// ! SET HOTSPOTS  
// ====================

			if(options.hotspots) { 
				for(var i=0; i<numberSlides; i++){
					if($slides.eq(i).children("img").data("hotspot1") != ""){
						var hotspotHTML = '<ul class="hotspots"><li class="hotspot-marker hotspot1"><div class="hotspot-glow"></div><a href="#" class="hotspot"></a></li></ul>';
						$(hotspotHTML).appendTo("#slide" + i);
					}
				}
				
				//Chrome: hotspot glow causes issues with the cta buttons on carousel control bar, so adding class to disable it
				if (navigator.userAgent.indexOf('Safari') != -1 && navigator.userAgent.indexOf('Chrome') == -1){
					$(".hotspots").addClass("isSafari");
				}
				
				//Add pulse to hotspot if browser can handle animations, else degrade gracefully
				if($html.hasClass("cssanimations")){
					$(".hotspot-glow").bind("animationend webkitAnimationEnd mozAnimationEnd", function() { 
						$thisGlow = $(this);
						$thisGlow.parent(".hotspot-marker").addClass("glow-complete");
						if(!$thisGlow.siblings(".hotspot").hasClass("hotspot-active")){
							$thisGlow.siblings(".hotspot").animate({opacity:0.6 }); 
						}
					})
				} else {
					hotspot_timer = setTimeout(function() {
						$(".hotspot-glow").siblings("a").css({backgroundPosition:"-55px -607px" });
					}, 2000);
				}
				
				//Add hover effects if hotspot is activated (ie showing video content bubble)
				$(".glow-complete a").live({mouseenter:function(){
					$(this).stop(true, true).animate({opacity:1 }).addClass("hotspot-hover");
				}, mouseleave: function(){
					if(!$(this).hasClass("hotspot-active")){
						$(this).stop(true, true).animate({opacity:0.6 }).removeClass("hotspot-hover");
					}
				}
				});
				
				//Show/hide hotspot content is hotspot is clicked on
				$(".hotspot").live("click", function(){
					$hotspot = $(this);
					$hotspot.toggleClass("hotspot-active");
					
					var videoUrl = $hotspot.closest(".hotspots").prev().data("hotspot1");
					if (videoUrl.indexOf("virginactiveuk") != -1) {
						videoUrl = "http://www.youtube.com/embed/" + videoUrl.substring(videoUrl.lastIndexOf("/")+1) + "?showinfo=0";
					} else if (videoUrl.indexOf("&") != -1){
						videoUrl = "http://www.youtube.com/embed/" + videoUrl.substring(videoUrl.indexOf("=")+1, videoUrl.indexOf("&")) + "?showinfo=0";
					} else {
						videoUrl = "http://www.youtube.com/embed/" + videoUrl.substring(videoUrl.indexOf("=")+1) + "?showinfo=0";
					}
					
					//On first click - activate hotspot content
					if($hotspot.hasClass("hotspot-active")){
						$('<div class="hotspot-content"><div class="hotspot-inner"><a href="#" class="close"></a><iframe width="335" height="200" src="' + videoUrl + '" frameborder="0" allowfullscreen></iframe></div></div>').insertAfter($hotspot);
						if($hotspot.offset().top <= 458){
							$(".hotspot-content").addClass("hotspot-content-under");
						}
						$(".hotspot-content").fadeIn();
					//On 2nd click, close hotspot and restart clock
					} else {
						$hotspot.removeClass("hotspot-active hotspot-hover").next(".hotspot-content").css({display:"none" }).remove();
						if($hotspot.parent(".hotspot-marker").hasClass("glow-complete")){
							$hotspot.animate({opacity:0.6 });
						}
						startClock();
					}
					
					//Add ga tracking
                    if (typeof (_gaq) !== "undefined") {
					    var gaqCategory = "Hotspot",
						    gaqAction = "Open",
						    gaqLabel = $hotspot.closest(".slide").children("img").data("hotspotname");
                        
					        _gaq.push(["_trackEvent", gaqCategory, gaqAction, gaqLabel]);
                        }
				});
				
				//Remove hotspot if close icon is clicked on and restart clock
				$(".hotspot-content a").live("click", function(){
					var $hotspotCloseBtn = $(this),
						$hotspot = $hotspotCloseBtn.closest(".hotspot-content").prev();
						
					$hotspot.removeClass("hotspot-active hotspot-hover");
					if ($hotspot.parent(".hotspot-marker").hasClass("glow-complete")){
						$hotspot.animate({opacity:0.6 });
					}
					$hotspotCloseBtn.closest(".hotspot-content").css({display:"none" }).remove();
					startClock();
				});
			}

// ====================
// ! SET HOMEPAGE ICONS   
// ====================

            //Initialise icons
			if(options.rotateIcons) {
				var i, iconsHTML = '<ul id="carousel-icons">';
				for (i=0; i<numberSlides; i++){
					iconsHTML += '<li id="icon' + (i+1) + '" class="' + $slides.eq(i).children("img").data("icon") + '"><a href="#"></a></li>';
				}
				iconsHTML += '</ul>';
               	$(iconsHTML).insertAfter("#streetview-wrap");
				
				var $icons = $carouselControls.find("#carousel-icons").children("li");
				$icons.children("a").css({opacity:0.5 }).filter(":eq(1)").css({opacity:1 });
				$("#carousel-icons li:gt(2)").hide();
				
				var iconsArray = [];
				$icons.each(function(){
					iconsArray.push($(this).attr("class"));		
				});
				iconsArray.unshift(iconsArray.pop());
				$icons.each(function(){
					if($icons.index($(this))-1 < 0){
						$icons.eq($icons.index($(this))).attr("class", iconsArray[0]);
					} else {
						$icons.eq($icons.index($(this))).attr("class", iconsArray[$icons.index($(this))]);
					}
				});
				$icons.click(function(e) {
					e.preventDefault();
					direction = ($icons.index($(this)) == 2) ? "next" : "prev";
					//stopClock();
					shift(direction);
				});
				
			} 
			
			function setIcon(direction) {
				if(!options.rotateIcons || options.rotateIcons =="false") {return false; }
				if(direction == "next"){
					iconsArray.push(iconsArray.shift());
				} else if (direction == "prev") {
					iconsArray.unshift(iconsArray.pop());
				}
				
				$icons.stop(true, true).animate({opacity:0 }, options.controlsAnimationSpeed, function() {
					$icons.eq($icons.index($(this))).attr("class", iconsArray[$icons.index($(this))]);
				}).animate({opacity:1 });
			}


// ====================
// ! SHIFT ANIMATIONS   
// ====================
            
            //Animating the shift!
            function shift(direction) {
				clearTimeout(hotspot_timer);
        	    //remember previous activeSlide
                var prevActiveSlide = activeSlide,
                	slideDirection = direction;
                //exit function if bullet clicked is same as the current image
                if(prevActiveSlide == slideDirection) { return false; }
                function resetAndUnlock() {
                    $slides.eq(prevActiveSlide).css({"z-index":0 });
                    unlock();
                }
                if($slides.length == "1") { return false; }
                if(!locked) {
					///nextActiveSlide = 1;
                    lock();
					 //deduce the proper activeImage
                    if(direction == "next") {
                        activeSlide++
                        if(activeSlide == numberSlides) {
                            activeSlide = 0;
                        }
                    } else if(direction == "prev") {
                        activeSlide--
                        if(activeSlide < 0) {
                            activeSlide = numberSlides-1;
                        }
                    } else {
                        activeSlide = direction;
                        if (prevActiveSlide < activeSlide) { 
                            slideDirection = "next";
                        } else if (prevActiveSlide > activeSlide) { 
                            slideDirection = "prev"
                        }
                    }
                    //set to correct bullet
                     setActiveBullet();  
                     
                    //fade
                    ///if(options.animation == "fade") {
                    ///    $slides.eq(activeSlide).css({"opacity":0, "z-index":3 }).animate({"opacity":1 }, options.animationSpeed, resetAndUnlock);
                    ///}
					
                    //horizontal-push
                    if(options.animation == "horizontal-push") {
                        if(slideDirection == "next") {
                            $slides.eq(activeSlide).addClass("slide-active").css({"left":winWidth + "px", "z-index":1 }).stop(true, true).animate({"left":0 }, options.animationSpeed, resetAndUnlock);
                            $slides.eq(prevActiveSlide).removeClass("slide-active").css({"z-index":0 }).stop(true, true).animate({"left":-winWidth + "px" }, options.animationSpeed).find(".hotspot-marker").removeClass("glow-complete").children("a").css({opacity:1 });
                        }
                        if(slideDirection == "prev") {
                            $slides.eq(activeSlide).css({"left":-winWidth + "px", "z-index":1 }).stop(true, true).animate({"left":0 }, options.animationSpeed, resetAndUnlock);
							$slides.eq(prevActiveSlide).css({"z-index":0 }).stop(true, true).animate({"left":winWidth + "px" }, options.animationSpeed);
                        }
                    }
					updateCaption(prevActiveSlide);
					setIcon(direction); //home page
					setThumb();         //club pages
					finalX = 0; //Required to stop taps activating shift()
                } //lock
            }//orbit function
        });//each call
    }//orbit plugin call
})(jQuery);
        
/*
|--------------------------------------------------------------------------
| UItoTop jQuery Plugin 1.1
| http://www.mattvarone.com/web-design/uitotop-jquery-plugin/
| MODIFIED BY JONATHAN FIELDING, DO NOT UPDATE TO A NEWER VERSION
|--------------------------------------------------------------------------*/

(function($){
	$.fn.UItoTop = function(options) {

 		var defaults = {
			text: 'To Top',
			min: 200,
			inDelay:600,
			outDelay:400,
  			containerID: 'toTop',
			containerHoverID: 'toTopHover',
			scrollSpeed: 1200,
			easingType: 'linear'
 		};

 		var settings = $.extend(defaults, options);
		var containerIDhash = '#' + settings.containerID;
		var containerHoverIDHash = '#'+settings.containerHoverID;
		
		$(containerIDhash).hide().prepend('<span id="'+settings.containerHoverID+'"></span>')
		.hover(function() {
				$(containerHoverIDHash, this).stop().animate({
					'opacity': 1
				}, 600, 'linear');
			}, function() { 
				$(containerHoverIDHash, this).stop().animate({
					'opacity': 0
				}, 700, 'linear');
			});
					
		$(window).scroll(function() {
			var sd = $(window).scrollTop();
			if(typeof document.body.style.maxHeight === "undefined") {
				$(containerIDhash).css({
					'position': 'absolute',
					'top': $(window).scrollTop() + $(window).height() - 50
				});
			}
			if ( sd > settings.min ) 
				$(containerIDhash).fadeIn(settings.inDelay);
			else 
				$(containerIDhash).fadeOut(settings.Outdelay);
		});
		
		$.autoscroll.init('#toTop');

};
})(jQuery);


// http://bit.ly/ishiv | WTFPL License
window.innerShiv=function(){function h(c,e,b){return/^(?:area|br|col|embed|hr|img|input|link|meta|param)$/i.test(b)?c:e+"></"+b+">"}var c,e=document,j,g="abbr article aside audio canvas datalist details figcaption figure footer header hgroup mark meter nav output progress section summary time video".split(" ");return function(d,i){if(!c&&(c=e.createElement("div"),c.innerHTML="<nav></nav>",j=c.childNodes.length!==1)){for(var b=e.createDocumentFragment(),f=g.length;f--;)b.createElement(g[f]);b.appendChild(c)}d=d.replace(/^\s\s*/,"").replace(/\s\s*$/,"").replace(/<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>/gi,"").replace(/(<([\w:]+)[^>]*?)\/>/g,h);c.innerHTML=(b=d.match(/^<(tbody|tr|td|col|colgroup|thead|tfoot)/i))?"<table>"+d+"</table>":d;b=b?c.getElementsByTagName(b[1])[0].parentNode:c;if(i===!1)return b.childNodes;for(var f=e.createDocumentFragment(),k=b.childNodes.length;k--;)f.appendChild(b.firstChild);return f}}();



// Chosen, a Select Box Enhancer for jQuery and Protoype
// by Patrick Filler for Harvest, http://getharvest.com
// 
// McCormack and Morrison Version
// Full source at https://github.com/harvesthq/chosen
// Copyright (c) 2011 Harvest http://getharvest.com

// MIT License, https://github.com/harvesthq/chosen/blob/master/LICENSE.md
(function() {
  var SelectParser;

  SelectParser = (function() {

    function SelectParser() {
      this.options_index = 0;
      this.parsed = [];
    }

    SelectParser.prototype.add_node = function(child) {
      if (child.nodeName === "OPTGROUP") {
        return this.add_group(child);
      } else {
        return this.add_option(child);
      }
    };

    SelectParser.prototype.add_group = function(group) {
      var group_position, option, _i, _len, _ref, _results;
      group_position = this.parsed.length;
      this.parsed.push({
        array_index: group_position,
        group: true,
        label: group.label,
        children: 0,
        disabled: group.disabled
      });
      _ref = group.childNodes;
      _results = [];
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        option = _ref[_i];
        _results.push(this.add_option(option, group_position, group.disabled));
      }
      return _results;
    };

    SelectParser.prototype.add_option = function(option, group_position, group_disabled) {
      if (option.nodeName === "OPTION") {
        if (option.text !== "") {
          if (group_position != null) {
            this.parsed[group_position].children += 1;
          }
          this.parsed.push({
            array_index: this.parsed.length,
            options_index: this.options_index,
            value: option.value,
            text: option.text,
            html: option.innerHTML,
            selected: option.selected,
            disabled: group_disabled === true ? group_disabled : option.disabled,
            group_array_index: group_position,
            classes: option.className,
            style: option.style.cssText
          });
        } else {
          this.parsed.push({
            array_index: this.parsed.length,
            options_index: this.options_index,
            empty: true
          });
        }
        return this.options_index += 1;
      }
    };

    return SelectParser;

  })();

  SelectParser.select_to_array = function(select) {
    var child, parser, _i, _len, _ref;
    parser = new SelectParser();
    _ref = select.childNodes;
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      child = _ref[_i];
      parser.add_node(child);
    }
    return parser.parsed;
  };

  this.SelectParser = SelectParser;

}).call(this);

/*
Chosen source: generate output using 'cake build'
Copyright (c) 2011 by Harvest
*/


(function() {
  var AbstractChosen, root;

  root = this;

  AbstractChosen = (function() {

    function AbstractChosen(form_field, options) {
      this.form_field = form_field;
      this.options = options != null ? options : {};
      this.set_default_values();
      this.is_multiple = this.form_field.multiple;
      this.set_default_text();
      this.setup();
      this.set_up_html();
      this.register_observers();
      this.finish_setup();
    }

    AbstractChosen.prototype.set_default_values = function() {
      var _this = this;
      this.click_test_action = function(evt) {
        return _this.test_active_click(evt);
      };
      this.activate_action = function(evt) {
        return _this.activate_field(evt);
      };
      this.active_field = false;
      this.mouse_on_container = false;
      this.results_showing = false;
      this.result_highlighted = null;
      this.result_single_selected = null;
      this.allow_single_deselect = (this.options.allow_single_deselect != null) && (this.form_field.options[0] != null) && this.form_field.options[0].text === "" ? this.options.allow_single_deselect : false;
      this.disable_search_threshold = this.options.disable_search_threshold || 0;
      this.search_contains = this.options.search_contains || false;
      this.choices = 0;
      this.single_backstroke_delete = this.options.single_backstroke_delete || false;
      return this.max_selected_options = this.options.max_selected_options || Infinity;
    };

    AbstractChosen.prototype.set_default_text = function() {
      if (this.form_field.getAttribute("data-placeholder")) {
        this.default_text = this.form_field.getAttribute("data-placeholder");
      } else if (this.is_multiple) {
        this.default_text = this.options.placeholder_text_multiple || this.options.placeholder_text || "Select Some Options";
      } else {
        this.default_text = this.options.placeholder_text_single || this.options.placeholder_text || "Select an Option";
      }
      return this.results_none_found = this.form_field.getAttribute("data-no_results_text") || this.options.no_results_text || "No results match";
    };

    AbstractChosen.prototype.mouse_enter = function() {
      return this.mouse_on_container = true;
    };

    AbstractChosen.prototype.mouse_leave = function() {
      return this.mouse_on_container = false;
    };

    AbstractChosen.prototype.input_focus = function(evt) {
      var _this = this;
      if (!this.active_field) {
        return setTimeout((function() {
          return _this.container_mousedown();
        }), 50);
      }
    };

    AbstractChosen.prototype.input_blur = function(evt) {
      var _this = this;
      if (!this.mouse_on_container) {
        this.active_field = false;
        return setTimeout((function() {
          return _this.blur_test();
        }), 100);
      }
    };

    AbstractChosen.prototype.result_add_option = function(option) {
      var classes, style;
      if (!option.disabled) {
        option.dom_id = this.container_id + "_o_" + option.array_index;
        classes = option.selected && this.is_multiple ? [] : ["active-result"];
        if (option.selected) {
          classes.push("result-selected");
        }
        if (option.group_array_index != null) {
          classes.push("group-option");
        }
        if (option.classes !== "") {
          classes.push(option.classes);
        }
        style = option.style.cssText !== "" ? " style=\"" + option.style + "\"" : "";
        return '<li id="' + option.dom_id + '" class="' + classes.join(' ') + '"' + style + '>' + option.html + '</li>';
      } else {
        return "";
      }
    };

    AbstractChosen.prototype.results_update_field = function() {
      if (!this.is_multiple) {
        this.results_reset_cleanup();
      }
      this.result_clear_highlight();
      this.result_single_selected = null;
      return this.results_build();
    };

    AbstractChosen.prototype.results_toggle = function() {
      if (this.results_showing) {
        return this.results_hide();
      } else {
        return this.results_show();
      }
    };

    AbstractChosen.prototype.results_search = function(evt) {
      if (this.results_showing) {
        return this.winnow_results();
      } else {
        return this.results_show();
      }
    };

    AbstractChosen.prototype.keyup_checker = function(evt) {
      var stroke, _ref;
      stroke = (_ref = evt.which) != null ? _ref : evt.keyCode;
      this.search_field_scale();
      switch (stroke) {
        case 8:
          if (this.is_multiple && this.backstroke_length < 1 && this.choices > 0) {
            return this.keydown_backstroke();
          } else if (!this.pending_backstroke) {
            this.result_clear_highlight();
            return this.results_search();
          }
          break;
        case 13:
          evt.preventDefault();
          if (this.results_showing) {
            return this.result_select(evt);
          }
          break;
        case 27:
          if (this.results_showing) {
            this.results_hide();
          }
          return true;
        case 9:
        case 38:
        case 40:
        case 16:
        case 91:
        case 17:
          break;
        default:
          return this.results_search();
      }
    };

    AbstractChosen.prototype.generate_field_id = function() {
      var new_id;
      new_id = this.generate_random_id();
      this.form_field.id = new_id;
      return new_id;
    };

    AbstractChosen.prototype.generate_random_char = function() {
      var chars, newchar, rand;
      chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      rand = Math.floor(Math.random() * chars.length);
      return newchar = chars.substring(rand, rand + 1);
    };

    return AbstractChosen;

  })();

  root.AbstractChosen = AbstractChosen;

}).call(this);

/*
Chosen source: generate output using 'cake build'
Copyright (c) 2011 by Harvest
*/


(function() {
  var $, Chosen, get_side_border_padding, root,
    __hasProp = {}.hasOwnProperty,
    __extends = function(child, parent) { for (var key in parent) { if (__hasProp.call(parent, key)) child[key] = parent[key]; } function ctor() { this.constructor = child; } ctor.prototype = parent.prototype; child.prototype = new ctor(); child.__super__ = parent.prototype; return child; };

  root = this;

  $ = jQuery;

  $.fn.extend({
    chosen: function(options) {
      if ($.browser.msie && ($.browser.version === "6.0")) {
        return this;
      }
      return this.each(function(input_field) {
        var $this;
        $this = $(this);
        if (!$this.hasClass("chzn-done")) {
          return $this.data('chosen', new Chosen(this, options));
        }
      });
    }
  });

  Chosen = (function(_super) {

    __extends(Chosen, _super);

    function Chosen() {
      return Chosen.__super__.constructor.apply(this, arguments);
    }

    Chosen.prototype.setup = function() {
      this.form_field_jq = $(this.form_field);
      this.current_value = this.form_field_jq.val();
      return this.is_rtl = this.form_field_jq.hasClass("chzn-rtl");
    };

    Chosen.prototype.finish_setup = function() {
      return this.form_field_jq.addClass("chzn-done");
    };

    Chosen.prototype.set_up_html = function() {
      var container_div, dd_top, dd_width, sf_width;
      this.container_id = this.form_field.id.length ? this.form_field.id.replace(/[^\w]/g, '_') : this.generate_field_id();
      this.container_id += "_chzn";
      this.f_width = this.form_field_jq.outerWidth();
      container_div = $("<div />", {
        id: this.container_id,
        "class": "chzn-container" + (this.is_rtl ? ' chzn-rtl' : ''),
        style: 'width: ' + this.f_width + 'px;'
      });
      if (this.is_multiple) {
        container_div.html('<ul class="chzn-choices"><li class="search-field"><input type="text" value="' + this.default_text + '" class="default" autocomplete="off" style="width:25px;" /></li></ul><div class="chzn-drop" style="left:-9000px;"><ul class="chzn-results"></ul></div>');
      }
      else if(this.options.hasSearch){
        container_div.html('<a href="javascript:void(0)" class="chzn-single chzn-default"><span>' + this.default_text + '</span><div><b></b></div></a><div class="chzn-drop" style="left:-9000px;"><div class="chzn-search"><input type="text" autocomplete="off" /></div><ul class="chzn-results"></ul></div>');
      }
      else{
     	 container_div.html('<a href="javascript:void(0)" class="chzn-single chzn-default"><span>' + this.default_text + '</span><div><b></b></div></a><div class="chzn-drop no-search" style="left:-9000px;"><div class="chzn-search hidden"><input type="text" autocomplete="off" /></div><ul class="chzn-results"></ul></div>');
      }
      
      this.form_field_jq.hide().after(container_div);
      this.container = $('#' + this.container_id);
      this.container.addClass("chzn-container-" + (this.is_multiple ? "multi" : "single"));
      this.dropdown = this.container.find('div.chzn-drop').first();
      dd_top = this.container.height();
      dd_width = this.f_width - get_side_border_padding(this.dropdown);
      this.dropdown.css({
        "width": dd_width + "px",
        "top": dd_top + "px"
      });
      this.search_field = this.container.find('input').first();
      this.search_results = this.container.find('ul.chzn-results').first();
      this.search_field_scale();
      this.search_no_results = this.container.find('li.no-results').first();
      if (this.is_multiple) {
        this.search_choices = this.container.find('ul.chzn-choices').first();
        this.search_container = this.container.find('li.search-field').first();
      } else {
        this.search_container = this.container.find('div.chzn-search').first();
        this.selected_item = this.container.find('.chzn-single').first();
        sf_width = dd_width - get_side_border_padding(this.search_container) - get_side_border_padding(this.search_field);
        this.search_field.css({
          "width": sf_width + "px"
        });
      }
      this.results_build();
      this.set_tab_index();
      return this.form_field_jq.trigger("liszt:ready", {
        chosen: this
      });
    };

    Chosen.prototype.register_observers = function() {
      var _this = this;
      this.container.mousedown(function(evt) {
        return _this.container_mousedown(evt);
      });
      this.container.mouseup(function(evt) {
        return _this.container_mouseup(evt);
      });
      this.container.mouseenter(function(evt) {
        return _this.mouse_enter(evt);
      });
      this.container.mouseleave(function(evt) {
        return _this.mouse_leave(evt);
      });
      this.search_results.mouseup(function(evt) {
        return _this.search_results_mouseup(evt);
      });
      this.search_results.mouseover(function(evt) {
        return _this.search_results_mouseover(evt);
      });
      this.search_results.mouseout(function(evt) {
        return _this.search_results_mouseout(evt);
      });
      this.form_field_jq.bind("liszt:updated", function(evt) {
        return _this.results_update_field(evt);
      });
      this.search_field.blur(function(evt) {
        return _this.input_blur(evt);
      });
      this.search_field.keyup(function(evt) {
        return _this.keyup_checker(evt);
      });
      this.search_field.keydown(function(evt) {
        return _this.keydown_checker(evt);
      });
      if (this.is_multiple) {
        this.search_choices.click(function(evt) {
          return _this.choices_click(evt);
        });
        return this.search_field.focus(function(evt) {
          return _this.input_focus(evt);
        });
      } else {
        return this.container.click(function(evt) {
          return evt.preventDefault();
        });
      }
    };

    Chosen.prototype.search_field_disabled = function() {
      this.is_disabled = this.form_field_jq[0].disabled;
      if (this.is_disabled) {
        this.container.addClass('chzn-disabled');
        this.search_field[0].disabled = true;
        if (!this.is_multiple) {
          this.selected_item.unbind("focus", this.activate_action);
        }
        return this.close_field();
      } else {
        this.container.removeClass('chzn-disabled');
        this.search_field[0].disabled = false;
        if (!this.is_multiple) {
          return this.selected_item.bind("focus", this.activate_action);
        }
      }
    };

    Chosen.prototype.container_mousedown = function(evt) {
   		$('.chzn-container').css('z-index', '98');
   		$(this.container.selector).css('z-index','99');
    
      var target_closelink;
      if (!this.is_disabled) {
        target_closelink = evt != null ? ($(evt.target)).hasClass("search-choice-close") : false;
        if (evt && evt.type === "mousedown" && !this.results_showing) {
          evt.stopPropagation();
        }
        if (!this.pending_destroy_click && !target_closelink) {
          if (!this.active_field) {
            if (this.is_multiple) {
              this.search_field.val("");
            }
            $(document).click(this.click_test_action);
            this.results_show();
          } else if (!this.is_multiple && evt && (($(evt.target)[0] === this.selected_item[0]) || $(evt.target).parents("a.chzn-single").length)) {
            evt.preventDefault();
            this.results_toggle();
          }
          return this.activate_field();
        } else {
          return this.pending_destroy_click = false;
        }
      }
    };

    Chosen.prototype.container_mouseup = function(evt) {
      if (evt.target.nodeName === "ABBR" && !this.is_disabled) {
        return this.results_reset(evt);
      }
    };

    Chosen.prototype.blur_test = function(evt) {
      if (!this.active_field && this.container.hasClass("chzn-container-active")) {
        return this.close_field();
      }
    };

    Chosen.prototype.close_field = function() {
      $(document).unbind("click", this.click_test_action);
      if (!this.is_multiple) {
        this.selected_item.attr("tabindex", this.search_field.attr("tabindex"));
        this.search_field.attr("tabindex", -1);
      }
      this.active_field = false;
      this.results_hide();
      this.container.removeClass("chzn-container-active");
      this.winnow_results_clear();
      this.clear_backstroke();
      this.show_search_field_default();
      return this.search_field_scale();
    };

    Chosen.prototype.activate_field = function() {
      if (!this.is_multiple && !this.active_field) {
        this.search_field.attr("tabindex", this.selected_item.attr("tabindex"));
        this.selected_item.attr("tabindex", -1);
      }
      this.container.addClass("chzn-container-active");
      this.active_field = true;
      this.search_field.val(this.search_field.val());
      return this.search_field.focus();
    };

    Chosen.prototype.test_active_click = function(evt) {
      if ($(evt.target).parents('#' + this.container_id).length) {
        return this.active_field = true;
      } else {
        return this.close_field();
      }
    };

    Chosen.prototype.results_build = function() {
      var content, data, _i, _len, _ref;
      this.parsing = true;
      this.results_data = root.SelectParser.select_to_array(this.form_field);
      if (this.is_multiple && this.choices > 0) {
        this.search_choices.find("li.search-choice").remove();
        this.choices = 0;
      } else if (!this.is_multiple) {
        this.selected_item.addClass("chzn-default").find("span").text(this.default_text);
        if (this.form_field.options.length <= this.disable_search_threshold) {
          this.container.addClass("chzn-container-single-nosearch");
        } else {
          this.container.removeClass("chzn-container-single-nosearch");
        }
      }
      content = '';
      _ref = this.results_data;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        data = _ref[_i];
        if (data.group) {
          content += this.result_add_group(data);
        } else if (!data.empty) {
          content += this.result_add_option(data);
          if (data.selected && this.is_multiple) {
            this.choice_build(data);
          } else if (data.selected && !this.is_multiple) {
            this.selected_item.removeClass("chzn-default").find("span").text(data.text);
            if (this.allow_single_deselect) {
              this.single_deselect_control_build();
            }
          }
        }
      }
      this.search_field_disabled();
      this.show_search_field_default();
      this.search_field_scale();
      this.search_results.html(content);
      return this.parsing = false;
    };

    Chosen.prototype.result_add_group = function(group) {
      if (!group.disabled) {
        group.dom_id = this.container_id + "_g_" + group.array_index;
        return '<li id="' + group.dom_id + '" class="group-result">' + $("<div />").text(group.label).html() + '</li>';
      } else {
        return "";
      }
    };

    Chosen.prototype.result_do_highlight = function(el) {
      var high_bottom, high_top, maxHeight, visible_bottom, visible_top;
      if (el.length) {
        this.result_clear_highlight();
        this.result_highlight = el;
        this.result_highlight.addClass("highlighted");
        maxHeight = parseInt(this.search_results.css("maxHeight"), 10);
        visible_top = this.search_results.scrollTop();
        visible_bottom = maxHeight + visible_top;
        high_top = this.result_highlight.position().top + this.search_results.scrollTop();
        high_bottom = high_top + this.result_highlight.outerHeight();
        if (high_bottom >= visible_bottom) {
          return this.search_results.scrollTop((high_bottom - maxHeight) > 0 ? high_bottom - maxHeight : 0);
        } else if (high_top < visible_top) {
          return this.search_results.scrollTop(high_top);
        }
      }
    };

    Chosen.prototype.result_clear_highlight = function() {
      if (this.result_highlight) {
        this.result_highlight.removeClass("highlighted");
      }
      return this.result_highlight = null;
    };

    Chosen.prototype.results_show = function() {
      var dd_top;
      if (!this.is_multiple) {
        this.selected_item.addClass("chzn-single-with-drop");
        if (this.result_single_selected) {
          this.result_do_highlight(this.result_single_selected);
        }
      } else if (this.max_selected_options <= this.choices) {
        this.form_field_jq.trigger("liszt:maxselected", {
          chosen: this
        });
        return false;
      }
      dd_top = this.is_multiple ? this.container.height() : this.container.height() - 1;
      this.form_field_jq.trigger("liszt:showing_dropdown", {
        chosen: this
      });
      this.dropdown.css({
        "top": dd_top + "px",
        "left": 0
      });
      this.results_showing = true;
      this.search_field.focus();
      this.search_field.val(this.search_field.val());
      return this.winnow_results();
    };

    Chosen.prototype.results_hide = function() {
      if (!this.is_multiple) {
        this.selected_item.removeClass("chzn-single-with-drop");
      }
      this.result_clear_highlight();
      this.form_field_jq.trigger("liszt:hiding_dropdown", {
        chosen: this
      });
      this.dropdown.css({
        "left": "-9000px"
      });
      return this.results_showing = false;
    };

    Chosen.prototype.set_tab_index = function(el) {
      var ti;
      if (this.form_field_jq.attr("tabindex")) {
        ti = this.form_field_jq.attr("tabindex");
        this.form_field_jq.attr("tabindex", -1);
        if (this.is_multiple) {
          return this.search_field.attr("tabindex", ti);
        } else {
          this.selected_item.attr("tabindex", ti);
          return this.search_field.attr("tabindex", -1);
        }
      }
    };

    Chosen.prototype.show_search_field_default = function() {
      if (this.is_multiple && this.choices < 1 && !this.active_field) {
        this.search_field.val(this.default_text);
        return this.search_field.addClass("default");
      } else {
        this.search_field.val("");
        return this.search_field.removeClass("default");
      }
    };

    Chosen.prototype.search_results_mouseup = function(evt) {
      var target;
      target = $(evt.target).hasClass("active-result") ? $(evt.target) : $(evt.target).parents(".active-result").first();
      if (target.length) {
        this.result_highlight = target;
        return this.result_select(evt);
      }
    };

    Chosen.prototype.search_results_mouseover = function(evt) {
      var target;
      target = $(evt.target).hasClass("active-result") ? $(evt.target) : $(evt.target).parents(".active-result").first();
      if (target) {
        return this.result_do_highlight(target);
      }
    };

    Chosen.prototype.search_results_mouseout = function(evt) {
      if ($(evt.target).hasClass("active-result" || $(evt.target).parents('.active-result').first())) {
        return this.result_clear_highlight();
      }
    };

    Chosen.prototype.choices_click = function(evt) {
      evt.preventDefault();
      if (this.active_field && !($(evt.target).hasClass("search-choice" || $(evt.target).parents('.search-choice').first)) && !this.results_showing) {
        return this.results_show();
      }
    };

    Chosen.prototype.choice_build = function(item) {
      var choice_id, link,
        _this = this;
      if (this.is_multiple && this.max_selected_options <= this.choices) {
        this.form_field_jq.trigger("liszt:maxselected", {
          chosen: this
        });
        return false;
      }
      choice_id = this.container_id + "_c_" + item.array_index;
      this.choices += 1;
      this.search_container.before('<li class="search-choice" id="' + choice_id + '"><span>' + item.html + '</span><a href="javascript:void(0)" class="search-choice-close" rel="' + item.array_index + '"></a></li>');
      link = $('#' + choice_id).find("a").first();
      return link.click(function(evt) {
        return _this.choice_destroy_link_click(evt);
      });
    };

    Chosen.prototype.choice_destroy_link_click = function(evt) {
      evt.preventDefault();
      if (!this.is_disabled) {
        this.pending_destroy_click = true;
        return this.choice_destroy($(evt.target));
      } else {
        return evt.stopPropagation;
      }
    };

    Chosen.prototype.choice_destroy = function(link) {
      this.choices -= 1;
      this.show_search_field_default();
      if (this.is_multiple && this.choices > 0 && this.search_field.val().length < 1) {
        this.results_hide();
      }
      this.result_deselect(link.attr("rel"));
      return link.parents('li').first().remove();
    };

    Chosen.prototype.results_reset = function() {
      this.form_field.options[0].selected = true;
      this.selected_item.find("span").text(this.default_text);
      if (!this.is_multiple) {
        this.selected_item.addClass("chzn-default");
      }
      this.show_search_field_default();
      this.results_reset_cleanup();
      this.form_field_jq.trigger("change");
      if (this.active_field) {
        return this.results_hide();
      }
    };

    Chosen.prototype.results_reset_cleanup = function() {
      return this.selected_item.find("abbr").remove();
    };

    Chosen.prototype.result_select = function(evt) {
      var high, high_id, item, position;
      if (this.result_highlight) {
        high = this.result_highlight;
        high_id = high.attr("id");
        this.result_clear_highlight();
        if (this.is_multiple) {
          this.result_deactivate(high);
        } else {
          this.search_results.find(".result-selected").removeClass("result-selected");
          this.result_single_selected = high;
          this.selected_item.removeClass("chzn-default");
        }
        high.addClass("result-selected");
        position = high_id.substr(high_id.lastIndexOf("_") + 1);
        item = this.results_data[position];
        item.selected = true;
        this.form_field.options[item.options_index].selected = true;
        if (this.is_multiple) {
          this.choice_build(item);
        } else {
          this.selected_item.find("span").first().text(item.text);
          if (this.allow_single_deselect) {
            this.single_deselect_control_build();
          }
        }
        if (!(evt.metaKey && this.is_multiple)) {
          this.results_hide();
        }
        this.search_field.val("");
        if (this.is_multiple || this.form_field_jq.val() !== this.current_value) {
          this.form_field_jq.trigger("change", {
            'selected': this.form_field.options[item.options_index].value
          });
        }
        this.current_value = this.form_field_jq.val();
        return this.search_field_scale();
      }
    };

    Chosen.prototype.result_activate = function(el) {
      return el.addClass("active-result");
    };

    Chosen.prototype.result_deactivate = function(el) {
      return el.removeClass("active-result");
    };

    Chosen.prototype.result_deselect = function(pos) {
      var result, result_data;
      result_data = this.results_data[pos];
      result_data.selected = false;
      this.form_field.options[result_data.options_index].selected = false;
      result = $("#" + this.container_id + "_o_" + pos);
      result.removeClass("result-selected").addClass("active-result").show();
      this.result_clear_highlight();
      this.winnow_results();
      this.form_field_jq.trigger("change", {
        deselected: this.form_field.options[result_data.options_index].value
      });
      return this.search_field_scale();
    };

    Chosen.prototype.single_deselect_control_build = function() {
      if (this.allow_single_deselect && this.selected_item.find("abbr").length < 1) {
        return this.selected_item.find("span").first().after("<abbr class=\"search-choice-close\"></abbr>");
      }
    };

    Chosen.prototype.winnow_results = function() {
      var found, option, part, parts, regex, regexAnchor, result, result_id, results, searchText, startpos, text, zregex, _i, _j, _len, _len1, _ref;
      this.no_results_clear();
      results = 0;
      searchText = this.search_field.val() === this.default_text ? "" : $('<div/>').text($.trim(this.search_field.val())).html();
      regexAnchor = this.search_contains ? "" : "^";
      regex = new RegExp(regexAnchor + searchText.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, "\\$&"), 'i');
      zregex = new RegExp(searchText.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, "\\$&"), 'i');
      _ref = this.results_data;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        option = _ref[_i];
        if (!option.disabled && !option.empty) {
          if (option.group) {
            $('#' + option.dom_id).css('display', 'none');
          } else if (!(this.is_multiple && option.selected)) {
            found = false;
            result_id = option.dom_id;
            result = $("#" + result_id);
            if (regex.test(option.html)) {
              found = true;
              results += 1;
            } else if (option.html.indexOf(" ") >= 0 || option.html.indexOf("[") === 0) {
              parts = option.html.replace(/\[|\]/g, "").split(" ");
              if (parts.length) {
                for (_j = 0, _len1 = parts.length; _j < _len1; _j++) {
                  part = parts[_j];
                  if (regex.test(part)) {
                    found = true;
                    results += 1;
                  }
                }
              }
            }
            if (found) {
              if (searchText.length) {
                startpos = option.html.search(zregex);
                text = option.html.substr(0, startpos + searchText.length) + '</em>' + option.html.substr(startpos + searchText.length);
                text = text.substr(0, startpos) + '<em>' + text.substr(startpos);
              } else {
                text = option.html;
              }
              result.html(text);
              this.result_activate(result);
              if (option.group_array_index != null) {
                $("#" + this.results_data[option.group_array_index].dom_id).css('display', 'list-item');
              }
            } else {
              if (this.result_highlight && result_id === this.result_highlight.attr('id')) {
                this.result_clear_highlight();
              }
              this.result_deactivate(result);
            }
          }
        }
      }
      if (results < 1 && searchText.length) {
        return this.no_results(searchText);
      } else {
        return this.winnow_results_set_highlight();
      }
    };

    Chosen.prototype.winnow_results_clear = function() {
      var li, lis, _i, _len, _results;
      this.search_field.val("");
      lis = this.search_results.find("li");
      _results = [];
      for (_i = 0, _len = lis.length; _i < _len; _i++) {
        li = lis[_i];
        li = $(li);
        if (li.hasClass("group-result")) {
          _results.push(li.css('display', 'auto'));
        } else if (!this.is_multiple || !li.hasClass("result-selected")) {
          _results.push(this.result_activate(li));
        } else {
          _results.push(void 0);
        }
      }
      return _results;
    };

    Chosen.prototype.winnow_results_set_highlight = function() {
      var do_high, selected_results;
      if (!this.result_highlight) {
        selected_results = !this.is_multiple ? this.search_results.find(".result-selected.active-result") : [];
        do_high = selected_results.length ? selected_results.first() : this.search_results.find(".active-result").first();
        if (do_high != null) {
          return this.result_do_highlight(do_high);
        }
      }
    };

    Chosen.prototype.no_results = function(terms) {
      var no_results_html;
      no_results_html = $('<li class="no-results">' + this.results_none_found + ' "<span></span>"</li>');
      no_results_html.find("span").first().html(terms);
      return this.search_results.append(no_results_html);
    };

    Chosen.prototype.no_results_clear = function() {
      return this.search_results.find(".no-results").remove();
    };

    Chosen.prototype.keydown_arrow = function() {
      var first_active, next_sib;
      if (!this.result_highlight) {
        first_active = this.search_results.find("li.active-result").first();
        if (first_active) {
          this.result_do_highlight($(first_active));
        }
      } else if (this.results_showing) {
        next_sib = this.result_highlight.nextAll("li.active-result").first();
        if (next_sib) {
          this.result_do_highlight(next_sib);
        }
      }
      if (!this.results_showing) {
        return this.results_show();
      }
    };

    Chosen.prototype.keyup_arrow = function() {
      var prev_sibs;
      if (!this.results_showing && !this.is_multiple) {
        return this.results_show();
      } else if (this.result_highlight) {
        prev_sibs = this.result_highlight.prevAll("li.active-result");
        if (prev_sibs.length) {
          return this.result_do_highlight(prev_sibs.first());
        } else {
          if (this.choices > 0) {
            this.results_hide();
          }
          return this.result_clear_highlight();
        }
      }
    };

    Chosen.prototype.keydown_backstroke = function() {
      if (this.pending_backstroke) {
        this.choice_destroy(this.pending_backstroke.find("a").first());
        return this.clear_backstroke();
      } else {
        this.pending_backstroke = this.search_container.siblings("li.search-choice").last();
        if (this.single_backstroke_delete) {
          return this.keydown_backstroke();
        } else {
          return this.pending_backstroke.addClass("search-choice-focus");
        }
      }
    };

    Chosen.prototype.clear_backstroke = function() {
      if (this.pending_backstroke) {
        this.pending_backstroke.removeClass("search-choice-focus");
      }
      return this.pending_backstroke = null;
    };

    Chosen.prototype.keydown_checker = function(evt) {
      var stroke, _ref;
      stroke = (_ref = evt.which) != null ? _ref : evt.keyCode;
      this.search_field_scale();
      if (stroke !== 8 && this.pending_backstroke) {
        this.clear_backstroke();
      }
      switch (stroke) {
        case 8:
          this.backstroke_length = this.search_field.val().length;
          break;
        case 9:
          if (this.results_showing && !this.is_multiple) {
            this.result_select(evt);
          }
          this.mouse_on_container = false;
          break;
        case 13:
          evt.preventDefault();
          break;
        case 38:
          evt.preventDefault();
          this.keyup_arrow();
          break;
        case 40:
          this.keydown_arrow();
          break;
      }
    };

    Chosen.prototype.search_field_scale = function() {
      var dd_top, div, h, style, style_block, styles, w, _i, _len;
      if (this.is_multiple) {
        h = 0;
        w = 0;
        style_block = "position:absolute; left: -1000px; top: -1000px; display:none;";
        styles = ['font-size', 'font-style', 'font-weight', 'font-family', 'line-height', 'text-transform', 'letter-spacing'];
        for (_i = 0, _len = styles.length; _i < _len; _i++) {
          style = styles[_i];
          style_block += style + ":" + this.search_field.css(style) + ";";
        }
        div = $('<div />', {
          'style': style_block
        });
        div.text(this.search_field.val());
        $('body').append(div);
        w = div.width() + 25;
        div.remove();
        if (w > this.f_width - 10) {
          w = this.f_width - 10;
        }
        this.search_field.css({
          'width': w + 'px'
        });
        dd_top = this.container.height();
        return this.dropdown.css({
          "top": dd_top + "px"
        });
      }
    };

    Chosen.prototype.generate_random_id = function() {
      var string;
      string = "sel" + this.generate_random_char() + this.generate_random_char() + this.generate_random_char();
      while ($("#" + string).length > 0) {
        string += this.generate_random_char();
      }
      return string;
    };

    return Chosen;

  })(AbstractChosen);

  get_side_border_padding = function(elmt) {
    var side_border_padding;
    return side_border_padding = elmt.outerWidth() - elmt.width();
  };

  root.get_side_border_padding = get_side_border_padding;

}).call(this);


//McCormack and Morrison conversions
$.conversions = function(){
	return {
		KgtoLbs: function(pK) {
		    var nearExact = pK/0.45359237;
		    var lbs = Math.floor(nearExact);
		    var oz = (nearExact - lbs) * 16;
		    return {
		        pounds: lbs,
		        ounces: oz
		    };
		},
		LbstoKg: function(pK) {
		    var nearExact = pK/2.2;
		    var kg = Math.floor(nearExact);
		    var grams = (nearExact - kg) * 1000;
		    return {
		        kg: kg,
		        grams: parseInt(grams)
		    };
		},
		CmstoInches: function(pK) {
		    var nearExact = pK/2.54;
		    return Math.floor(nearExact);
		},
		InchestoCms: function(pK) {
		    var nearExact = pK*2.54;
		    return Math.floor(nearExact);
		}
	}
}();



//McCormack and Morrison Smooth autoscroller with user override
$.autoscroll = function(){
	var privateProperties = {
	    "height": 		0,
	    "top": 			0,
	    "viewHeight": 	0,
	    "scrollHeight": 0,
	    "step": 		0,
	    "scrollTop": 	0,
	    "targetTop": 	0
	};
	
	var updatePrivateProperties = function(){
		privateProperties.height = document.body.scrollHeight;
	
		//Set the current position of the page
		if (document.body.scrollTop > 0) {
			privateProperties.scrollTop = privateProperties.top = document.body.scrollTop;
		}
		else if (document.documentElement.scrollTop > 0) {
		    privateProperties.scrollTop = privateProperties.top = document.documentElement.scrollTop;
		}
		
		//Show the visible height
		if (window.innerHeight) {
		    privateProperties.viewHeight = window.innerHeight;
		}
		else {
		    privateProperties.viewHeight = document.documentElement.clientHeight;
		}
		
		privateProperties.scrollHeight = privateProperties.height - privateProperties.viewHeight;
		
		window.scrollBy(0, 1)
		
		//Check which scroll variable to use and set the scrollTop variable
		if (document.body.scrollTop > 0) {
		    privateProperties.scrollTop = privateProperties.top = document.body.scrollTop;
		}
		else if (document.documentElement.scrollTop > 0) {
		    privateProperties.scrollTop = privateProperties.top = document.documentElement.scrollTop;
		}
	}

	var clicked = function(){
		var $this = $(this), href = $this.attr('href').split('#')[1];
		
		updatePrivateProperties();
		
		if(href === "bottom"){
			//scroll to bottom
			animateScrollBottom();
		}
		else if(href === "top"){
			animateScrollTop();
		}
		else{
			privateProperties.targetTop = $('#' + href).offset().top - 10;
			
			animateScrollElement();
		}
		
		return false;
	};
	
	var animateScrollElement = function () {
        
        if (document.body.scrollTop > 0) {
            privateProperties.scrollTop = document.body.scrollTop;
        }
        else if (document.documentElement.scrollTop > 0) {
            privateProperties.scrollTop = document.documentElement.scrollTop;
        }
       
        if (privateProperties.top === privateProperties.scrollTop && privateProperties.top <= privateProperties.targetTop) {
            privateProperties.top = privateProperties.top + 30;
            window.scrollTo(0, privateProperties.top);

            setTimeout(function () {
                animateScrollElement();
            }, 5);
        }
        else{
        	window.scrollTo(0,privateProperties.targetTop);
        }
    };
	
	var animateScrollTop = function () {
        if (document.body.scrollTop > 0) {
            privateProperties.scrollTop = document.body.scrollTop;
        }
        else if (document.documentElement.scrollTop > 0) {
            privateProperties.scrollTop = document.documentElement.scrollTop;
        }
    
    	if(privateProperties.top === privateProperties.scrollTop & privateProperties.top !== 0){
    		privateProperties.top = privateProperties.top - 30;
    		
    		if(privateProperties.top < 0){
    			window.scrollTo(0, 0);
    		}
    		else{
    			window.scrollTo(0, privateProperties.top);
    		}
    		
    		setTimeout(function(){
    				animateScrollTop();
    		}, 5);
    	}
    };
	
	var animateScrollBottom = function () {
		if (document.body.scrollTop > 0) {
            privateProperties.scrollTop = document.body.scrollTop;
        }
        else if (document.documentElement.scrollTop > 0) {
            privateProperties.scrollTop = document.documentElement.scrollTop;
        }
       
        if (privateProperties.top === privateProperties.scrollTop && privateProperties.top <= privateProperties.scrollHeight) {
            privateProperties.top = privateProperties.top + 30;
            window.scrollTo(0, privateProperties.top);

            setTimeout(function () {
                animateScrollBottom();
            }, 5);
        }
        else{
        	window.scrollTo(0,privateProperties.scrollHeight);
        }
    };

	return {
		init: function(element){
			var $element = $(this);
			$(element).click(clicked);
		}
	}
}();