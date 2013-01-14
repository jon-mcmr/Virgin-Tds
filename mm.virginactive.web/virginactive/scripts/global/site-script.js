var mcmr = {};

//Create the virgin active global site module
mcmr.va_global = function(){
	var club_dropdown = function(){
		var $this = $(this),$content = $('#content'), $club_list = $this.next(), content_height = $content.height();
		
		if($this.hasClass('active')){
			closeClubList($club_list, $content);
		}
		else{
			if($('.clubs .active').length){
				closeClubList($('.clubs .active').toggleClass('active').next(), $content, function(){openClubList($club_list, $content);});
			}
			else {
				openClubList($club_list, $content);
			}
		}
		
		$this.toggleClass('active');
	};
	
	var closeButton = function(){
		var $this = $(this),$content = $('#content'), $club_list = $this.parents('div .list');
		$club_list.prev().toggleClass('active');
		closeClubList($club_list, $content, function(){
		
		});
	};
	
	var openClubList = function($club_list, $content, return_func){
		var height = parseInt($club_list.attr('data-height')), 
			content_height = parseInt($content.attr('data-height')) + height, 
			speed = 600;
		
		return_func = return_func || function(){};
		
		$club_list.show();
		$club_list.stop().animate({
			'height': height
		}, speed);
		$content.stop().animate({
			'height': content_height
		}, speed, '', function(){return_func();});
	};
	
	var closeClubList = function($club_list, $content, return_func){
		var height = $club_list.attr('data-height'), speed = 600,content_height = parseInt($content.attr('data-height'));
		
		return_func = return_func || function(){};
		
		$club_list.stop().animate({
			'height': '-='+height
		}, speed);
		$content.stop().animate({
			'height': content_height
		}, speed,'',function(){$club_list.hide();  return_func();})
	};

	return{
		init: function(){
			var $club_dropdown = $('.clubs .dropdown'), $club_list = null;
			
			$('#content').attr('data-height',$('#content').height()).css('height',$('#content').height());
			
			$club_dropdown.each(function(index){
				$club_list = $(this).next();
				$club_list.attr('data-height',$club_list.height()).css('height','0px').hide();
				$club_list.append('<div class="close ir">close</div>');
			});
			
			$('.clubs').on('click','div.dropdown',club_dropdown);
			$club_dropdown.next().find('.close').on('click',closeButton);
		}
	}
}();

window.onload = function(){
	mcmr.va_global.init();
};