
$(function () {
});

$(document).ready(function () {

    var params = {
        ajax: 1,
        cmd: 'ClubNamesList'
    };

    //Get the list of clubs
    var a = $(".findclubenq*").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: document.location,
                dataType: "json",
                data: {
                    term: request.term,
                    ajax: 1,
                    cmd: 'ClubNamesList'
                },

                success: function (data) {
                    if (data) {
                        response(data);
                    }
                }
            })
        },
        minLength: 2,
        select: function (event, ui) {
            $(".clubEmail*").val(ui.item.email);
            $(".clubGUID*").val(ui.item.clubGUID);
            //debug(ui.item.email);
        }
    });

});


function clubFinderAutocomplete() {
        if (google.maps.LatLngBounds !== undefined) {

            $(".clubfinder-enquiry").sameHeightCols();

            var guid = $("#va-overlay .update-class").data("guid");

            var params = {
                ajax: 1,
                cmd: "GetClubResultsUrl"
            }

            var defaultBounds = new google.maps.LatLngBounds(
            //Rough bounding lat/long for the UK:
			new google.maps.LatLng(49.83798, -5.97656),
			new google.maps.LatLng(59.68993, 1.75781));

            var input = document.getElementById("findclub"),
	        autocomplete = new google.maps.places.Autocomplete(input, {
	            bounds: defaultBounds,
	            types: ["geocode"]
	        });

            //Selects result by mouse or arrow keys
            google.maps.event.addListener(autocomplete, 'place_changed', function () {
                var resultPageUrl = $.get(document.location, params, function (data) {
                    return data;
                }).success(function () {

                    var place = autocomplete.getPlace();

                    if (place.geometry) {
                        var lat = place.geometry.location.lat(), lng = place.geometry.location.lng(), url = resultPageUrl.responseText + '?' + $.param({ lat: lat, lng: lng, searchloc: $("#findclub").val(), filter: guid, ste: $("#input-entered").val(), sty: 2 });
                        document.location = url;
                    }
                })
            });

            //Selects first result on the dropdown list if Enter is pressed
            $("#findclub").focusin(function () {

                $(document).keyup(function (e) {
                    var temp = $("#findclub").val();
                    $("#input-entered").val(temp);
                }),
	            $(document).keydown(function (e) {


	                var firstResult = $(".pac-container .pac-item:first").text(), noResultsSelected = false;

	                //Add slight delay to allow drop down list to populate
	                setTimeout(function () {

	                    //If autocomplete input is on an overlay, amend the styles to disable scrollable dropdown options 

	                    if ($(".pac-container").prev().hasClass("va-overlay")) {
	                        // only targeting desktop
	                        //console.log('3');

	                        if (navigator.userAgent.match(/iPad/i)) {
	                        } else if (navigator.userAgent.match(/iPhone/i)) {
	                        } else {
	                            $(".pac-container").addClass("pac-container-static").removeAttr("style").css({ top: (offsetTop + 199) + "px", left: (offsetLeft + 40) + "px", width: "428px" });
	                        }
	                    }
	                    //Check user hasn't already selected in item using arrow keys
	                    $(".pac-container .pac-item").each(function () {
	                        if ($(this).hasClass("pac-selected")) {
	                            noResultsSelected = true;
	                        }
	                    });

	                    //If no result currently highlighted and user presses enter:
	                    if ((noResultsSelected == false) && (e.which == 13)) {
	                        var resultPageUrl = $.get(document.location, params, function (data) {
	                            return data;
	                        }).success(function () {
	                            //JONFIX!!!
	                            if (typeof guid == 'undefined') {
	                                guid = "";
	                            }
	                            _filters = guid;
	                            //ENDFIX
	                            var geocoder = new google.maps.Geocoder();
	                            geocoder.geocode({ "address": firstResult }, function (results, status) {
	                                var lat = results[0].geometry.location.lat(),
	                                    lng = results[0].geometry.location.lng(),
	                                    url = resultPageUrl.responseText + '?' + $.param({ lat: lat, lng: lng, searchloc: firstResult, filter: _filters });
	                                document.location = url;
	                            });
	                        });
	                    }
	                }, 300);
	            });
            });

        }
        else {
            setTimeout(clubFinderAutocomplete, 100);
        }
    
}



function GetSuggestions(term) {
    var _homePageUrl = "";
    var arr1 = document.getElementsByTagName("input");
    for (i = 0; i < arr1.length; i++) {
        if (arr1[i].className == "homePageUrl") {
            _homePageUrl = arr1[i].value;
        }
    }
    //Perform an AJAX call to populate the workout section content
    var params = {
        ajax: 1,
        cmd: 'GetSuggestions',
        term: term,
        loc: '' + thisUrl
    };

    $.get(_homePageUrl, params, function (data) {
        $('<div id="result-filter" />').insertBefore(".club-finder-results").html(data);

        $("#result-filter li:gt(2)").addClass("adjust-width");
    });
}

function viewClubFinderResults() {
    //Assign height to .club-details for alignemnt, and to .club-showmap for ie7
    $(".club-summary .club-details").each(function () {
        var $this = $(this),
            h = $this.height();
        if (h < 70) {
            h = 78;
            $this.css({ minHeight: h + "px" });
        }
        $this.next(".club-showmap").css({ height: h + "px" });
    });

    $viewLinks = $(".view-links");
    $viewLinks.find("a").click(function (e) {
        e.preventDefault();
        showAllClubs = true;
        var $this = $(this),
			currLink = $this.parent().attr("class");

        $viewLinks.find("a").removeClass("active").filter($this).addClass("active");
        _ActiveTab = (currLink === "clubs-map-view") ? "map" : "list";

        updateResults()
    });
}

function showOnMap() {
    $showOnMapLink = $(".club-showmap a");
    $showOnMapLink.click(function (e) {
        e.preventDefault();
        $this = $(this);
        $(".view-links").find("a").removeClass("active").filter(".clubs-map-view a").addClass("active");

        _SelectedClubLat = $this.data("lat");
        _SelectedClubLng = $this.data("lng");
        _SelectedClub = $(".club-showmap a").index($this);
        showAllClubs = false;
        _ActiveTab = "map";
        updateResults();
    });
}

function updateResults() {
    cmdListName = (_ActiveTab === "map") ? "MapResults" : "ClubList";

    var params = {
        ajax: 1,
        cmd: cmdListName,
        lat: _ClubFindLat,
        lng: _ClubFindLng,
        loc: _ClubFindLocation,
        flt: _filters.toString()
    };

    var href = document.location.href;
    href = href.substring(0, href.indexOf("?"));

    $.get(href, params, function (data) {
        $("#results").html(data);
        if (_ActiveTab === "map") {$.va_init.functions.clubsGoogleMapsSetup();  }
        showOnMap();
    });
}

//Every time an item is added or removed to the filter, this method is called
function FilterTriggered() {
    //debug(_filters);
    updateResults();
}

function SetHomeClub() {
    $(".club-set-home-club").click(function () {
        CallSetHomeClub();
        return false;
    });
}

function CallSetHomeClub() {
    var _homePageUrl = "";
    var _clubId = "";
    var _clubIsHomeClub = "";
    var arr1 = document.getElementsByTagName("input");
    for (i = 0; i < arr1.length; i++) {
        if (arr1[i].className == "homePageUrl") {
            homePageUrl = arr1[i].value;
        }
        if (arr1[i].className == "clubId") {
            _clubId = arr1[i].value;
        }
        if (arr1[i].className == "clubIsHomeClub") {
            _clubIsHomeClub = arr1[i].value;
        }
    }
    var params = {
        ajax: 1,
        cmd: 'SetHomeClub',
        clubId: _clubId,
        clubIsHomeClub: _clubIsHomeClub
    };

    $.get(homePageUrl, params, function (data) {
        if (_clubIsHomeClub == 'true') {
            $('a.club-set-home-club').text('Make this your home club');
            $('input.clubIsHomeClub').val('false');
            $('a#logo').attr("href", homePageUrl);
        }
        else {
            $('a.club-set-home-club').text('Log out of club');
            $('input.clubIsHomeClub').val('true');
            var records = eval(data);
            var recordCount = records.length;
            for (i = 0; i < recordCount; i++) {
                a = records[i];
                if (a.resultUrl != null) {
                    $('a#logo').attr("href", a.resultUrl);
                }
            }
        }
    });

    return false;
}
//called by header
function SetHomeClub_Header() {
    var _homePageUrl = "";
    var _clubId = "";
    var arr1 = document.getElementsByTagName("input");
    for (i = 0; i < arr1.length; i++) {
        if (arr1[i].className == "homePageUrl") {
            homePageUrl = arr1[i].value;
        }
        if (arr1[i].className == "lastClubVisitedId") {
            _clubId = arr1[i].value;
        }
    }
    var params = {
        ajax: 1,
        cmd: 'SetHomeClub',
        clubId: _clubId,
        clubIsHomeClub: 'false'
    };

    $.get(homePageUrl, params, function (data) {
        var records = eval(data);
        var recordCount = records.length;
        for (i = 0; i < recordCount; i++) {
            a = records[i];
            if (a.resultUrl != null) {
                $('a#logo').attr("href", a.resultUrl);
            }
        }
    });

    return false;
}

function DisableCookies() {
    $(".disable-cookies").click(function () {
        CallDisableCookies();
        return false;
    });
}

function CallDisableCookies() {
    var _homePageUrl = "";
    var arr1 = document.getElementsByTagName("input");
    for (i = 0; i < arr1.length; i++) {
        if (arr1[i].className == "homePageUrl") {
            homePageUrl = arr1[i].value;
        }
    }
    var params = {
        ajax: 1,
        cmd: 'SetDisableCookies'
    };

    $.get(homePageUrl, params, function (data) {
        $("#cookie-ribbon").toggleClass("reveal").slideToggle(1000, "easeOutQuint");
        $(".showmore").css('display', '');
        $(".cookie-show").removeClass("cookie-show");
        $("#last-homeclub").css({ display: "none" });
        $(".set-home-club-cookie").css({ display: "none" });
    });

    return false;
}

function CallSetGalleryPreference(showGallery) {
    var _homePageUrl = "";
    var arr1 = document.getElementsByTagName("input");
    for (i = 0; i < arr1.length; i++) {
        if (arr1[i].className == "homePageUrl") {
            homePageUrl = arr1[i].value;
        }
    }
    var params = {
        ajax: 1,
        cmd: 'SetGalleryPreference',
        show: showGallery
    };

    $.get(homePageUrl, params, function (data) {
    });

    return false;
}


//function CallCampaignWriteToDB() {

//    $(".save-campaign").click(function (e) {
//        e.preventDefault();

//        if (typeof (Page_ClientValidate) == 'function') {
//            Page_ClientValidate();
//        }
//        if (Page_IsValid) {

//            //Position spinner
////            if (typeof window.innerWidth != 'undefined') {
////                winWidth = window.innerWidth,
////			        winHeight = window.innerHeight;
////            } else if (typeof document.documentElement != 'undefined' && typeof document.documentElement.clientWidth != 'undefined' && document.documentElement.clientWidth != 0) {
////                scrollBarWidth = 17;
////                winWidth = document.documentElement.clientWidth,
////			        winHeight = document.documentElement.clientHeight;
////            }

////            //Add spinner whilst web service is responding
////            $('<div id="progress-spinner-wrap" style="top:' + ((winHeight - 214) / 2) + 'px; left:' + ((winWidth - 214) / 2) + 'px; "><div id="progress-spinner" /></div>').appendTo("body").delay(300).animate({ opacity: 1 }, function () {
////                var $spinner = $("#progress-spinner");
////                if ($("html").hasClass("csstransforms")) {
////                    var degrees = 0,
////		            spinner = setInterval(function (e) {
////		                if ($('html').hasClass("csstransforms")) {
////		                    var degreeCSS = "rotate(" + degrees + "deg)";
////		                    degrees += 2;
////		                    $spinner.css({ "msTransform": degreeCSS, "-webkit-transform": degreeCSS, "-moz-transform": degreeCSS, "-o-transform": degreeCSS });
////		                }
////		            }, 10);
////                }
////            });

//		    CampaignWriteToDB();
//            return false;
//        }
//    });
//}

//function CampaignWriteToDB() {

//    var DTO = JSON.stringify({ image: image, });

//    //Save Campaign
//    $.ajax({
//          type: 'POST',
//          url: document.location,
//          data: data,
//          dataType: "json",
//          success: function (data) {
//            if (data) {
//                response(data);
//            }
//          }
//    });

//    return false;
//}

function SetClubContactDetails() {
    var container = $("#results");

    var params = {
        ajax: 1,
        cmd: 'ClubContactDetailsResults',
        clubId: $(".clubGUID").val()
    };

    $.get(document.location, params, function (data) {
        container.html(data);
    });
    return false;
}

function ViewClubContactDetails() {
    var $contactWrap = $(".contact-wrap");

    $contactWrap.find(".chzn-clublist").change(function () {
        //e.preventDefault();
        var container = $("#results");

        var params = {
            ajax: 1,
            cmd: 'ClubContactDetailsResults',
            clubId: $(".clubGUID").val()
        };

        $.get(document.location, params, function (data) {
            container.html(data);
        });

        return false;
    });
}


function ViewReciprocalAccessResults() {

    $(".club-access-list").click(function (e) {
        e.preventDefault();

        if (typeof (Page_ClientValidate) == 'function') {
            Page_ClientValidate();
        }
        if (Page_IsValid) {

            //Position spinner
            if (typeof window.innerWidth != 'undefined') {
                winWidth = window.innerWidth,
			        winHeight = window.innerHeight;
            } else if (typeof document.documentElement != 'undefined' && typeof document.documentElement.clientWidth != 'undefined' && document.documentElement.clientWidth != 0) {
                scrollBarWidth = 17;
                winWidth = document.documentElement.clientWidth,
			        winHeight = document.documentElement.clientHeight;
            }

            //Add spinner whilst web service is responding
            $('<div id="progress-spinner-wrap" style="top:' + ((winHeight - 214) / 2) + 'px; left:' + ((winWidth - 214) / 2) + 'px; "><div id="progress-spinner" /></div>').appendTo("body").delay(300).animate({ opacity: 1 }, function () {
                var $spinner = $("#progress-spinner");
                if ($("html").hasClass("csstransforms")) {
                    var degrees = 0,
		            spinner = setInterval(function (e) {
		                if ($('html').hasClass("csstransforms")) {
		                    var degreeCSS = "rotate(" + degrees + "deg)";
		                    degrees += 2;
		                    $spinner.css({ "msTransform": degreeCSS, "-webkit-transform": degreeCSS, "-moz-transform": degreeCSS, "-o-transform": degreeCSS });
		                }
		            }, 10);
                }
            });

            ShowReciprocalAccessResults();
            return false;
        }
    });
}

function ShowReciprocalAccessResults() {

    var container = $("#results");

    var params = {
        ajax: 1,
        cmd: 'ReciprocalAccessResults',
        memNo: $(".membership-number").val().replace(/ /g, ""),
        dob: $(".date-of-birth").val().replace(/\//g, "-").replace(/ /g, "")
    };

    $.get(document.location, params, function (data) {
        container.html(data);
        container.slideDown();
        $("#error-message").html("");
        $("#error-panel").removeClass("error");
        $("#error-wrap").removeClass("error-panel");
        var _errorMessage = $(".ph-error-message").val();
        if (_errorMessage != "''") {
            $("#error-message").html(_errorMessage);
            $("#error-panel").addClass("error");
            $("#error-wrap").addClass("error-wrap");

            $("#progress-spinner-wrap").remove();
        }

        if ($(".club-access-info").offset() != null) {
            $("html,body").animate({ scrollTop: ($(".club-access-info").offset().top - 76) }, 2000, "easeOutQuint");
        }

        if ($("#progress-spinner-wrap").length > 0) {
            $("#progress-spinner-wrap").animate({ opacity: 0 }).remove();
        }

        $(".close-btn a").click(function (e) {
            e.preventDefault();
            container.slideUp();
        });

    });

    return false;
}



function LoadWorkout(categoryGuid) {
    //debug(categoryGuid);

    var outputDiv = $('#workout-result');

    //Perform an AJAX call to populate the workout section content
    var params = {
        ajax: 1,
        cmd: 'GetWorkoutSection',
        catGuid: categoryGuid
    };

    $.get(document.location, params, function (data) {
        $(outputDiv).html(data);
        $.va_init.functions.workouts();
    });

}


function ViewClubTimetable() {
    $(".clubs-list-view*").click(function () {
        ShowClassTimetable();
        return false;
    });
}

//Every time an option is selected to filter the timetable, this method is called
function FilterTimetableTriggered() {
    //debug(_filters);
    ShowClassTimetable();
}

function ShowClassTimetable() {

    var params = {
        ajax: 1,
        cmd: 'ClubTimetable',
        clubId: _ClubId,
        filters: _filters.toString()
    };


    $.get(document.location, params, function (data) {
        container.html(data);
        $("p#result-count").html(_resultString);
    });

    return false;
}


