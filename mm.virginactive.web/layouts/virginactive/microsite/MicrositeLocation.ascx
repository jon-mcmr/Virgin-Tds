<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MicrositeLocation.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.microsite.MicrositeLocation" %>
<%@ Import Namespace="mm.virginactive.controls.Model" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<div id="content_0_pageContainer" class="site_container">
    

<div class="club_location">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.js"></script>
    
    <script src='/virginactive/scripts/microsites/plugins.js' type='text/javascript'></script>
    <script type="text/javascript">
        function are_cookies_enabled() {
            var cookieEnabled = (navigator.cookieEnabled) ? true : false;

            if (typeof navigator.cookieEnabled == "undefined" && !cookieEnabled) {
                document.cookie = "testcookie";
                cookieEnabled = (document.cookie.indexOf("testcookie") != -1) ? true : false;
            }
            return (cookieEnabled);
        }

        if (!are_cookies_enabled()) {
            $('#cookie-ribbon').remove();
            $('body').removeClass('cookie-show');
        }


        if (typeof microsite === 'undefined') {
            var microsite = {
            }
        }

        if (typeof microsite.map === 'undefined') {
            microsite.map = function (mapElementId, lat, lng) {
                var mapOptions = this.mapOptions;

                // set map center lat&lng to the club
                var latCenter = '<%= ActiveClub.Lat.Rendered %>';
                var lngCenter = '<%= ActiveClub.Long.Rendered %>';
                mapOptions.center = new google.maps.LatLng(latCenter, lngCenter);

                mapOptions.currentClub = new google.maps.LatLng(lat, lng);

                this.map = new google.maps.Map(document.getElementById(mapElementId), mapOptions);
//                this.restrictBounds();
            }

//            microsite.map.prototype.restrictBounds = function () {
//                var swBound = new google.maps.LatLng();
//                var neBound = new google.maps.LatLng();
//                var strictBounds = new google.maps.LatLngBounds(swBound, neBound);
//                var map = this.map;
//                google.maps.event.addListener(map, 'dragend', function () {
//                    if (strictBounds.contains(map.getCenter())) return;
//                    var c = map.getCenter(),
//                    x = c.lng(),
//                    y = c.lat(),
//                    maxX = strictBounds.getNorthEast().lng(),
//                    maxY = strictBounds.getNorthEast().lat(),
//                    minX = strictBounds.getSouthWest().lng(),
//                    minY = strictBounds.getSouthWest().lat();

//                    if (x < minX) x = minX;
//                    if (x > maxX) x = maxX;
//                    if (y < minY) y = minY;
//                    if (y > maxY) y = maxY;

//                    map.setCenter(new google.maps.LatLng(y, x));
//                });

//            }

            microsite.map.prototype.mapOptions = {
                zoom: <%= ZoomLevel %>,
                scrollwheel: true,
                disableDefaultUI: true,
                disableDoubleClickZoom: false,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                navigationControl: false,
                mapTypeControl: false,
                zoomControl: true,
                zoomControlOptions: {
                	style: google.maps.ZoomControlStyle.LARGE,
                	position: google.maps.ControlPosition.LEFT_BOTTOM
                }
                
            };

            /** custom pin and pin shadow icons **/
            microsite.map.prototype.customIconSecondary = {
                icon: "/virginactive/images/microsites/marker-va.png",
                iconSize: new google.maps.Size(35, 47),
                iconOrigin: google.maps.Point(0, 0),
                iconAnchor: google.maps.Point(50, 57),
                shadow: "/virginactive/images/microsites/marker-shadow.png",
                shadowSize: new google.maps.Size(40, 47),
                shadowOrigin: google.maps.Point(0, 0),
                shadowAnchor: google.maps.Point(45, 37)
            };

            microsite.map.prototype.customIconsMain = {
                icon: "/virginactive/images/microsites/marker-200.png",
                iconSize: new google.maps.Size(87, 53),
                iconOrigin: new google.maps.Point(0, 0),
                shadow: "/virginactive/images/microsites/marker-200-shadow.png",
                shadowSize: new google.maps.Size(52, 35),
                shadowOrigin: new google.maps.Point(0, 0),
                shadowAnchor: new google.maps.Point(35, 34)
            };

            microsite.map.prototype.createMainMarker = function () {
                var self = this;
                var marker = this.customIconsMain;
                var iconObj = new google.maps.MarkerImage(marker.icon, marker.iconSize, marker.iconOrigin, marker.iconAnchor);
                var shadowObj = new google.maps.MarkerImage(marker.shadow, marker.shadowSize, marker.shadowOrigin, marker.shadowAnchor);
                var marker = new google.maps.Marker({
                    position: this.mapOptions.currentClub,
                    map: this.map,
                    draggable: false,
                    icon: iconObj,
                    shadow: shadowObj
                });

                var boxText = '<div id="main-info-box" class="info-box-wrap"><div class="info-box"><a href="#" class="close"></a><%= WriteAddress() %></div><div class="info-box-bottom"></div></div>';
                var infoBoxOptions = {
                    content: boxText,
                    pixelOffset: new google.maps.Size(-154, 20),
                    closeBoxURL: "",
                    boxClass: "infoBox infoBox-current-club"
                };

                var ib = new InfoBox(infoBoxOptions);
                ib.open(self.map, marker);
                google.maps.event.addListener(marker, "click", function (e) {
                    ib.open(self.map, this);
                });

                google.maps.event.addListener(ib, 'domready', function () {
                    $('#main-info-box .close').click(function () {
                        ib.close();
                    });
                });

            };

            /** Adds markers to a map based on what is shown on a page */
            microsite.map.prototype.processMarkers = function (elementSelector, latSelector, longSelector, markerSelector) {
                var self = this;
                this.markers = [];
                this.bounds = new google.maps.LatLngBounds();
                $(elementSelector).each(function (index) {
                    self.processMarker(index, this, latSelector, longSelector, markerSelector);
                });
                //this.map.fitBounds(this.bounds);
            };

            microsite.map.prototype.processMarker = function (index, element, latSelector, lngSelector, markerSelector) {
                var lat = parseFloat($(latSelector, element).first().text());
                var lng = parseFloat($(lngSelector, element).first().text());
                var position = new google.maps.LatLng(lat, lng);
                var markerHtml = $(markerSelector, element).first().html();
                this.markers[index] = this.createMarker(index, position, markerHtml, this.customIconSecondary);
                this.bounds.extend(position);
            };

            microsite.map.prototype.createMarker = function (index, point, html, marker) {
                var self = this;
                var iconObj = new google.maps.MarkerImage(marker.icon, marker.iconSize, marker.iconOrigin, marker.iconAnchor);
                var shadowObj = new google.maps.MarkerImage(marker.shadow, marker.shadowSize, marker.shadowOrigin, marker.shadowAnchor);

                var marker = new google.maps.Marker({
                    map: this.map,
                    position: point,
                    icon: iconObj,
                    shadow: shadowObj,
                    draggable: false
                });

                var infoBoxId = 'infobox-' + index;
                var infoBoxOptions = {
                    content: '<div id="' + infoBoxId + '">' + html + '</div>',
                    pixelOffset: new google.maps.Size(-137, -103)
                };

                marker.infowindow = new InfoBox(infoBoxOptions);
                google.maps.event.addListener(marker, 'click', function () {
                    this.infowindow.open(self.map, this);
                });
                google.maps.event.addListener(marker.infowindow, 'domready', function () {
                    var self = this;
                    $('#' + infoBoxId).click(function () { self.close(); });
                });

                return marker;
            };

            //google.maps.event.trigger(microsite.map, "resize");

        }


        $(document).ready(function () {
            var lat = '<%= ActiveClub.Lat.Rendered %>';
            var lng = '<%= ActiveClub.Long.Rendered %>';
            var myMap = new microsite.map("map_canvas", lat, lng);
            myMap.processMarkers(".map-info-wrapper", ".va-marker-lat", ".va-marker-lng", ".info-box-wrap");
            myMap.createMainMarker();
            var winHeight;



            onScreenResize();
            window.onresize = onScreenResize;

            function onScreenResize() {
                winHeight = $(window).height();
                $("#map_canvas").css({ height: winHeight });
            }



        });
        
    </script>
     
  <div id="location">
    <div id="map_canvas" style="width:100%;height:100%;"></div>
   


    <div id="va-map-panels" class="map-panels-holder">
	    
        <asp:Repeater runat="server" ID="ClubRepeater">
            <ItemTemplate>
                <div class="map-info-wrapper">
			        <div class="va-marker-lat" ><%# (Container.DataItem as Club).ClubItm.Lat.Rendered %></div>
			        <div class="va-marker-lng" ><%# (Container.DataItem as Club).ClubItm.Long.Rendered%></div>
			        <div class="info-box-wrap">
                        <div class="info-box">
                            <a href="#" class="close"></a>
					        <h2><a href="<%# Sitecore.Links.LinkManager.GetItemUrl((Container.DataItem as Club).ClubItm.InnerItem) %>" target="_blank"><%# (Container.DataItem as Club).ClubItm.Clubname.Rendered%></a></h2>                                   
			            </div>
                        <div class="info-box-bottom"></div>
                    				
                    </div>

                </div>
            </ItemTemplate>
        </asp:Repeater>
                    
            
            
    </div>
  </div>
</div>

</div>
<script src='/virginactive/scripts/microsites/infobox_packed.js' type='text/javascript'></script>   