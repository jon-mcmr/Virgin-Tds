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
        $('<div id="result-filter" />').insertBefore(".results-head").html(data);
    });
}


function viewClubFinderResults() {
    $viewLinks = $(".view-links");
    $viewLinks.find("a").click(function (e) {
        e.preventDefault();
        showAllClubs = true;
        var $this = $(this).blur(),
			currLink = $this.parent().attr("class");

        $viewLinks.find("a").removeClass("active").filter($this).addClass("active");
        _ActiveTab = (currLink === "clubs-map-view") ? "map" : "list";

        updateResults()
    });

    updateResults(); //Initial call
}


function showOnMap() {
    $showOnMapLink = $(".club-showmap a");
    $showOnMapLink.click(function (e) {
        e.preventDefault();
        $this = $(this).blur();
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
    cmdListName = (_ActiveTab === "map") ? "GetMemberClubMapView" : "GetMemberClubListView";

    var params = {
        ajax: 1,
        cmd: cmdListName,
        loc: '' + thisUrl
    };

    $.get(document.location, params, function (data) {
        if ($("#results").length > 0) {
            $("#results").remove();
        }
        $('<div id="results" />').insertAfter(".results-head").html(data);

        if (_ActiveTab === "map") { clubsGoogleMapsSetup(); }
        showOnMap();
    });
}

//Stops the form submitting and refreshing the page when it should be using clubFinderAutocomplete() below
$("form").submit(function () { return false; });

function clubFinderAutocomplete() {
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
                lat = place.geometry.location.lat(), lng = place.geometry.location.lng(),
                url = thisUrl + '?' + $.param({ lat: lat, lng: lng, searchloc: $("#findclub").val() });

                document.location = url;
            });
        });

    //Selects first result on the dropdown list if Enter is pressed
    $("#findclub").focusin(function () {
        $(document).keyup(function (e) {
            $("#input-entered").text($("#findclub").val());
        }),
        $(document).keydown(function (e) {
            var firstResult = $(".pac-container .pac-item:first").text(),
                noResultsSelected = false;

            //Add slight delay to allow drop down list to populate
            setTimeout(function () {
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
                        var geocoder = new google.maps.Geocoder();
                        geocoder.geocode({ "address": firstResult }, function (results, status) {
                            var lat = results[0].geometry.location.lat(),
                                lng = results[0].geometry.location.lng(),
                                url = thisUrl + '?' + $.param({ lat: lat, lng: lng, searchloc: firstResult });

                            document.location = url;
                        });
                    });
                }
            }, 800);
        });
    });


    //Scroll to search input
    if ($("#wrapper #content").length > 0) {
        $("html,body").animate({ scrollTop: ($("#clubfinderform").offset().top + 1) }, 2000, "easeOutQuint");
    }
}

function clubsGoogleMapsSetup() {
    var mapPlaceHolder = $('#map-canvas');
    if (mapPlaceHolder.length > 0) {
        var vaClubsInfoPanels = $('#va-map-panels .map-info-panel');

        if (vaClubsInfoPanels.length > 0) {
            var initGoogleMap = false;
            var vaGoogleMap = 0; // placeholder var for google map
            var mapCenterOverride = false;

            // check is preferred map center location is defined - ie if user clicked on 'show on map' link of club search result
            var mapCenter = $('#va-map-panels .map-center');
            if (mapCenter.length > 0) {
                var latOverride = mapCenter.find('.va-marker-lat').val();
                var lngOverride = mapCenter.find('.va-marker-lng').val();
                mapCenterOverride = true;
            };

            var bounds = new google.maps.LatLngBounds();

            vaClubsInfoPanels.each(function (i) {
                var _thisClubPanel = $(this);
                var thisLat = _thisClubPanel.find('.va-marker-lat').val();
                var thisLng = _thisClubPanel.find('.va-marker-lng').val();
                var latlng = new google.maps.LatLng(thisLat, thisLng);

                if (showAllClubs === true) { bounds.extend(latlng); }

                // if map hasn't been initiated yet
                if (!initGoogleMap) {
                    if (showAllClubs === false) {
                        altLatLng = new google.maps.LatLng(_SelectedClubLat, _SelectedClubLng);
                        var myOptions = {
                            zoom: 14,
                            center: altLatLng,
                            mapTypeId: google.maps.MapTypeId.ROADMAP
                        };
                    } else {
                        var myOptions = {
                            zoom: 11,
                            center: latlng,
                            mapTypeId: google.maps.MapTypeId.ROADMAP
                        };
                    }

                    vaGoogleMap = new google.maps.Map(document.getElementById("map-canvas"), myOptions);
                    initGoogleMap = true;
                };

                // for each info panel:
                var markerImage = new google.maps.MarkerImage("/virginactive/images/panels/marker-va.png",
						new google.maps.Size(24, 34),
						new google.maps.Point(0, 0),
						new google.maps.Point(12, 34));
                var markerShadow = new google.maps.MarkerImage("/virginactive/images/panels/marker-shadow.png",
						new google.maps.Size(44, 34),
						new google.maps.Point(0, 0),
						new google.maps.Point(12, 34));
                var markerShape = { type: "poly", coord: [12, 0, 16, 1, 18, 2, 19, 3, 20, 4, 21, 5, 21, 6, 22, 7, 22, 8, 22, 9, 22, 10, 22, 11, 22, 12, 22, 13, 22, 14, 22, 15, 22, 16, 21, 17, 21, 18, 20, 19, 20, 20, 19, 21, 19, 22, 18, 23, 18, 24, 17, 25, 16, 26, 16, 27, 15, 28, 14, 29, 14, 30, 13, 31, 12, 32, 12, 33, 11, 33, 11, 32, 10, 31, 9, 30, 8, 29, 8, 28, 7, 27, 7, 26, 6, 25, 5, 24, 5, 23, 4, 22, 4, 21, 3, 20, 3, 19, 2, 18, 2, 17, 1, 16, 1, 15, 1, 14, 1, 13, 0, 12, 0, 11, 1, 10, 1, 9, 1, 8, 1, 7, 2, 6, 2, 5, 3, 4, 4, 3, 5, 2, 7, 1, 11, 0, 12, 0] };
                var marker = new google.maps.Marker({
                    map: vaGoogleMap,
                    draggable: false,
                    icon: markerImage,
                    shadow: markerShadow,
                    shape: markerShape,
                    position: latlng,
                    visible: true
                });

                var vaBoxHTML = _thisClubPanel.get(0);

                var infoBoxOptions = {
                    content: vaBoxHTML,
                    disableAutoPan: false,
                    maxWidth: 0,
                    alignBottom: true,
                    pixelOffset: new google.maps.Size(-184, -50),
                    zIndex: null,
                    boxStyle: {
                        width: "370px"
                    },
                    closeBoxURL: "",
                    infoBoxClearance: new google.maps.Size(10, 10),
                    isHidden: false,
                    pane: "floatPane",
                    enableEventPropagation: false
                };

                var vaInfoBox = new InfoBox(infoBoxOptions);

                //If "show on map" link clicked, show infoWindow
                if ((showAllClubs === false) && (i == _SelectedClub)) {
                    vaInfoBox.open(vaGoogleMap, marker);
                }

                //Add handlers to open/close panel on marker click
                google.maps.event.addListener(marker, "click", function (e) {
                    vaInfoBox.open(vaGoogleMap, this);
                });
                _thisClubPanel.find(".close").click(function () {
                    vaInfoBox.close();
                });
            });

            // If "Map View" tab is clicked, show all pins within map boundary
            if (showAllClubs === true) { vaGoogleMap.fitBounds(bounds); }
        };
    };
}

clubFinderAutocomplete();