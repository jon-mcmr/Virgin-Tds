var ratio = 1;
var cropStyle="free";
var iCroppedWidth = 0;
var iCroppedHeight = 0;

var coordsInit = new Object();
coordsInit.x1 = 0;
coordsInit.y1 = 0;
coordsInit.x2 = 0;
coordsInit.y2 = 0;

var x1ControlName,y1ControlName,x2ControlName,y2ControlName,ratio2Origin,cropDDL;

//x1ControlName = "ctl00_ctl00_ctl00_ctl00_X1";
//y1ControlName = "ctl00_ctl00_ctl00_ctl00_Y1";
//x2ControlName = "ctl00_ctl00_ctl00_ctl00_X2";
//y2ControlName = "ctl00_ctl00_ctl00_ctl00_Y2";

var prevCrop = new Object();
prevCrop.x1 = 0;
prevCrop.y1 = 0;
prevCrop.x2 = 0;
prevCrop.y2 = 0;

var allowCroppedSizeChanging = false;		
	
// setup the callback function
function onEndCrop( coords, dimensions ) {
	    $( x1ControlName ).value = coords.x1 > 0 ? Math.round(coords.x1/ratio) : 0;
	    $( y1ControlName ).value = coords.y1 > 0 ? Math.round(coords.y1/ratio) : 0;
	    $( x2ControlName ).value = coords.x2 > 0 ? Math.round(coords.x2/ratio) : 0;
	    $( y2ControlName ).value = coords.y2 > 0 ? Math.round(coords.y2/ratio) : 0;
	    
        coordsInit.x1 = $( x1ControlName ).value;
        coordsInit.y1 = $( y1ControlName ).value;
        coordsInit.x2 = $( x2ControlName ).value;
        coordsInit.y2 = $(y2ControlName).value;

        prevCrop.x1 = coordsInit.x1;
        prevCrop.y1 = coordsInit.y1;
        prevCrop.x2 = coordsInit.x2;
        prevCrop.y2 = coordsInit.y2;
}

function setCroppedSize(obj) {
    var iRatio; 
}

function onCroppingStyleClick(obj) {
    cropStyle = obj;
}

if( typeof(dump) != 'function' ) {
	Debug.init(true, '/');
	
	function dump( msg ) {
		Debug.raise( msg );
	};
} else {
    //dump( '---------------------------------------\n' );
    //alert("cropping file");
}

/**
 * A little manager that allows us to swap the image dynamically
 *
 */
var CropImageManager = {
    /**
    * Holds the current Cropper.Img object
    * @var obj
    */
    curCrop: null,

    /**
    * Initialises the cropImageManager
    *
    * @access public
    * @return void
    */
    init: function () {
        ratio = $(ratio2Origin).value;
        //alert(ratio);
        this.attachCropper();
    },

    /**
    * Handles the changing of the select to change the image, the option value
    * is a pipe seperated list of imgSrc|width|height
    * 
    * @access public
    * @param obj event
    * @return void
    */
    onChange: function (e) {
        var vals = $F(Event.element(e)).split('|');
        this.setImage(vals[0], vals[1], vals[2]);
    },

    /**
    * Sets the image within the element & attaches/resets the image cropper
    *
    * @access private
    * @param string Source path of new image
    * @param int Width of new image in pixels
    * @param int Height of new image in pixels
    * @return void
    */
    setImage: function (imgSrc, w, h) {
        $('imgOriginal').src = imgSrc;
        $('imgOriginal').width = w;
        $('imgOriginal').height = h;
        this.attachCropper();
    },

    /** 
    * Attaches/resets the image cropper
    *
    * @access private
    * @return void
    */
    attachEmptyCropper: function () {
        if (this.curCrop == null) this.curCrop = new Cropper.Img('imgOriginal', { onEndCrop: onEndCrop });
        else this.curCrop.reset();
    },
    //
    attachCropper: function () {
        //this.removeCropper();
        ratio = parseFloat($(ratio2Origin).value);
        if (this.curCrop == null) {
            if ($(x1ControlName).value != "" && $(x2ControlName).value != "" && $(y1ControlName).value != "" && $(y2ControlName).value != "") {

                if ($(x1ControlName).value == 0 && $(y1ControlName).value == 0 && prevCrop.x1 > 0 && prevCrop.y1 > 0) {
                    this.curCrop = new Cropper.Img(
	                    'imgOriginal',
	                    {	                    
	                        ratioDim: { x: parseInt($('RatioWidth').value), y: parseInt($('RatioHeight').value) },
	                        onloadCoords: { x1: parseInt(prevCrop.x1) * ratio, y1: parseInt(prevCrop.y1) * ratio, x2: parseInt(prevCrop.x2) * ratio, y2: parseInt(prevCrop.y2) * ratio },
	                        displayOnInit: true,
	                        onEndCrop: onEndCrop
	                    }
                    )

                } else {
                    this.curCrop = new Cropper.Img(
	                    'imgOriginal',
	                    {
	                        ratioDim: { x: parseInt($('RatioWidth').value), y: parseInt($('RatioHeight').value) },
	                        onloadCoords: { x1: parseInt($(x1ControlName).value) * ratio, y1: parseInt($(y1ControlName).value) * ratio, x2: parseInt($(x2ControlName).value) * ratio, y2: parseInt($(y2ControlName).value) * ratio },
	                        displayOnInit: true,
	                        onEndCrop: onEndCrop
	                    }
                    )
                }

            } else {
                this.curCrop = new Cropper.Img(
	                'imgOriginal',
	                {
	                    onEndCrop: onEndCrop
	                }
                )
            }
        }
        else this.curCrop.reset();
        coordsInit.x1 = 0;
        coordsInit.y1 = 0;
        coordsInit.x2 = 0;
        coordsInit.y2 = 0;

    },

    /**
    * Removes the cropper
    *
    * @access public
    * @return void
    */
    removeCropper: function () {
        if (this.curCrop != null) {
            this.curCrop.remove();
            this.curCrop = null;
        }
    },

    /**
    * Resets the cropper, either re-setting or re-applying
    *
    * @access public
    * @return void
    */
    resetCropper: function () {
        this.removeCropper();
        cropStyle = "free";
        this.attachCropper();
    }
};

		Event.observe( 
			window, 
			'load', 
			function() { 
				CropImageManager.init();
			}
		); 	

function selectCrop(idx) {
    $(cropDDL).selectedIndex = idx;
}
