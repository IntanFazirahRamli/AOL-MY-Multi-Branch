//var imageUrl = 'Images/imagegreen.jpg';
var imageUrl = '';
var imageWidth = 754;
var imageHeight = 510;

var map = setupMap();
var markers = setupImport(map);
var polylineLatlngs = [];
var activeItemName = "";
var hasActiveLine = false;
setupItems(map, markers);
//setupExport(markers);

//var isShowActivity = false;
var isShowActivity = 'None';
var previousMarkerBlink = [];

var catchElement = '';
var fullCalerderEvent = '';
map.on('click', function (e) {
    if (hasActiveLine) {
        polylineLatlngs.push(e.latlng);
        //L.polyline(polylineLatlngs, {
        //    color: 'red'
        //}).addTo(map);
    }
});


function setupMap() {
    var map = L.map('map', {
        minZoom: 5,
        maxZoom: 8,
        center: [0, 0],
        zoom: 5,
        zoomAnimation: false,
        crs: L.CRS.Simple
    });
    //map.fitWorld().zoomIn();
    var southWest = map.unproject([0, imageHeight]);
    var northEast = map.unproject([imageWidth, 0]);
    var bounds = new L.LatLngBounds(southWest, northEast);
    L.imageOverlay(imageUrl, bounds).addTo(map);
    map.setMaxBounds(bounds);

    return map;
}

function addItem(map, markers, item) {

    var isColorForIconDefined = false;

    if (!item.markerCnt || item.markerCnt <= 2) {
        var options = {
            draggable: false
        };

        
        if (item.itemtype == 'DEVICE') {
            var activityCountSpan = '';

            if ($('input[id*=HiddenShowHoursWithActivity]').val() != "False") {
                if (isShowActivity != "None") {
                    //activityCountSpan = '<span class="fa-stack-1x" style="color:black;padding: 5px;font-size: 9px;font-weight: 700;"> <h6 style="font-weight: 700; background-color: white;border-radius: 50%">' + item.CountValue + '</h6></span>';
                    activityCountSpan = '<span class="fa-stack-1x" style="padding: 6px;margin-top: -6px;"> <h6 style="font-size: 11px;color:black;font-weight: 700; background-color: white;border-radius: 50%">' + item.CountValue + '</h6></span>';
                }
            }

            if ((parseInt(item.CountValue) <= parseInt(item.DailyLow))) {
                if (item.DailyLowColor != "") {
                    isColorForIconDefined = true;
                    var fontAwesomeIcon = L.divIcon({
                        html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style=" color:' + item.DailyLowColor + ';font-size: 3em; !important"></i> ' + activityCountSpan,
                        iconSize: [25, 41],
                        iconAnchor: [12, 41],
                        popupAnchor: [1, -34],
                        className: 'myDivIcon'
                    });
                }
                else if (item.DefaultLowColor != "") {
                    isColorForIconDefined = true;
                    var fontAwesomeIcon = L.divIcon({
                        html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style=" color:' + item.DefaultLowColor + ';font-size: 3em; !important"></i>' + activityCountSpan,
                        iconSize: [25, 41],
                        iconAnchor: [12, 41],
                        popupAnchor: [1, -34],
                        className: 'myDivIcon'
                    });
                }
                else {
                    if (item.deviceUrl != "" && item.deviceUrl != "NoImage") {
                        //isColorForIconDefined = false;
                        //var redIcon = new L.Icon({
                        //    iconUrl: item.deviceUrl,
                        //    shadowUrl: 'Images/marker-shadow.png',
                        //    iconSize: [25, 41],
                        //    iconAnchor: [12, 41],
                        //    popupAnchor: [1, -34],
                        //    shadowSize: [41, 41]
                        //});
                        //options.icon = redIcon;

                        isColorForIconDefined = true;
                        var fontAwesomeIcon = L.divIcon({

                            html: '<img rel="popover" id="iddevice' + item.deviceid + '" onclick = "onclickIcon(' + item.deviceid + ');" width ="25px" height="41px"  src="' + item.deviceUrl + '" data-content="." data-container="body" data-toggle="popover" data-html="true" data-trigger="manual" />' + activityCountSpan,

                            //html: '<span id="iddevice' + item.deviceid + '" style="width: 25px; height: 41px; background-image: url(' + item.deviceUrl + '); display:block"  data-content="." data-container="body" data-toggle="popover"  />' + activityCountSpan,
                            //html: '<span style="width: 25px; height: 41px; background-image: url(' + item.deviceUrl + '); display:block" />',
                            //html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style="color:#267fca;font-size: 3em; !important"></i>' + activityCountSpan,
                            iconSize: [25, 41],
                            iconAnchor: [12, 41],
                            popupAnchor: [1, -34],
                            className: 'myDivIcon'
                        });



                    } else {
                        isColorForIconDefined = true;
                        var fontAwesomeIcon = L.divIcon({
                            html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style="color:#267fca;font-size: 3em; !important"></i>' + activityCountSpan,
                            iconSize: [25, 41],
                            iconAnchor: [12, 41],
                            popupAnchor: [1, -34],
                            className: 'myDivIcon'
                        });

                    }
                }

            }
            else if ((parseInt(item.CountValue) <= parseInt(item.DailyMedium)) && (item.DailyMedium != "0")) {
                if (item.DailyMediumColor != "") {
                    isColorForIconDefined = true;
                    var fontAwesomeIcon = L.divIcon({
                        html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style=" color:' + item.DailyMediumColor + ';font-size: 3em; !important"></i>' + activityCountSpan,
                        iconSize: [25, 41],
                        iconAnchor: [12, 41],
                        popupAnchor: [1, -34],
                        className: 'myDivIcon'
                    });

                }
                else if (item.DefaultMediumColor != "") {
                    isColorForIconDefined = true;
                    var fontAwesomeIcon = L.divIcon({
                        html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style=" color:' + item.DefaultMediumColor + ';font-size: 3em; !important"></i>' + activityCountSpan,
                        iconSize: [25, 41],
                        iconAnchor: [12, 41],
                        popupAnchor: [1, -34],
                        className: 'myDivIcon'
                    });
                }
                else {
                    if (item.deviceUrl != "" && item.deviceUrl != "NoImage") {
                       
                        isColorForIconDefined = true;
                        var fontAwesomeIcon = L.divIcon({
                            html: '<img rel="popover" id="iddevice' + item.deviceid + '" onclick = "onclickIcon(' + item.deviceid + ');" width ="25px" height="41px"src="' + item.deviceUrl + '" data-content="." data-container="body" data-toggle="popover" data-html="true"  data-trigger="manual"/>' + activityCountSpan,
                            iconSize: [25, 41],
                            iconAnchor: [12, 41],
                            popupAnchor: [1, -34],
                            className: 'myDivIcon'
                        });


                    } else {
                        isColorForIconDefined = true;
                        var fontAwesomeIcon = L.divIcon({
                            html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style="color:#267fca;font-size: 3em; !important"></i>' + activityCountSpan,
                            iconSize: [25, 41],
                            iconAnchor: [12, 41],
                            popupAnchor: [1, -34],
                            className: 'myDivIcon'
                        });

                    }
                }

            }
            else if ((parseInt(item.CountValue) <= parseInt(item.DailyHigh)) && (item.DailyHigh != "0")) {

                if (item.DailyHighColor != "") {
                    isColorForIconDefined = true;
                    var fontAwesomeIcon = L.divIcon({
                        html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style=" color:' + item.DailyHighColor + ';font-size: 3em; !important"></i>' + activityCountSpan,
                        iconSize: [25, 41],
                        iconAnchor: [12, 41],
                        popupAnchor: [1, -34],
                        className: 'myDivIcon'
                    });

                }
                else if (item.DefaultHighColor != "") {
                    isColorForIconDefined = true;
                    var fontAwesomeIcon = L.divIcon({
                        html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style=" color:' + item.DefaultHighColor + ';font-size: 3em; !important"></i>' + activityCountSpan,
                        iconSize: [25, 41],
                        iconAnchor: [12, 41],
                        popupAnchor: [1, -34],
                        className: 'myDivIcon'
                    });
                }
                else {
                    if (item.deviceUrl != "" && item.deviceUrl != "NoImage") {
                     
                        isColorForIconDefined = true;
                        var fontAwesomeIcon = L.divIcon({
                            html: '<img rel="popover" id="iddevice' + item.deviceid + '" onclick = "onclickIcon(' + item.deviceid + ');" width ="25px" height="41px" src="' + item.deviceUrl + '" data-content="." data-container="body" data-toggle="popover" data-html="true" data-trigger="manual"/>' + activityCountSpan,
                            iconSize: [25, 41],
                            iconAnchor: [12, 41],
                            popupAnchor: [1, -34],
                            className: 'myDivIcon'
                        });


                    } else {
                        isColorForIconDefined = true;
                        var fontAwesomeIcon = L.divIcon({
                            html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style="color:#267fca;font-size: 3em; !important"></i>' + activityCountSpan,
                            iconSize: [25, 41],
                            iconAnchor: [12, 41],
                            popupAnchor: [1, -34],
                            className: 'myDivIcon'
                        });

                    }
                }

            }
            else if ((parseInt(item.CountValue) >= parseInt(item.DailyVeryHigh)) && (item.DailyVeryHigh != "0")) {

                if (item.DailyVeryHighColor != "") {
                    isColorForIconDefined = true;
                    var fontAwesomeIcon = L.divIcon({
                        html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style=" color:' + item.DailyVeryHighColor + ';font-size: 3em; !important"></i>' + activityCountSpan,
                        iconSize: [25, 41],
                        iconAnchor: [12, 41],
                        popupAnchor: [1, -34],
                        className: 'myDivIcon'
                    });

                }
                else if (item.DefaultVeryHighColor != "") {
                    isColorForIconDefined = true;
                    var fontAwesomeIcon = L.divIcon({
                        html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style=" color:' + item.DefaultVeryHighColor + ';font-size: 3em; !important"></i>' + activityCountSpan,
                        iconSize: [25, 41],
                        iconAnchor: [12, 41],
                        popupAnchor: [1, -34],
                        className: 'myDivIcon'
                    });
                }
                else {
                    if (item.deviceUrl != "" && item.deviceUrl != "NoImage") {
                      
                        isColorForIconDefined = true;
                        var fontAwesomeIcon = L.divIcon({
                            html: '<img rel="popover" id="iddevice' + item.deviceid + '" onclick = "onclickIcon(' + item.deviceid + ');" width ="25px" height="41px" src="' + item.deviceUrl + '" data-content="." data-container="body" data-toggle="popover" data-html="true" data-trigger="manual"/>' + activityCountSpan,
                            iconSize: [25, 41],
                            iconAnchor: [12, 41],
                            popupAnchor: [1, -34],
                            className: 'myDivIcon'
                        });

                    } else {
                        isColorForIconDefined = true;
                        var fontAwesomeIcon = L.divIcon({
                            html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style="color:#267fca;font-size: 3em; !important"></i>' + activityCountSpan,
                            iconSize: [25, 41],
                            iconAnchor: [12, 41],
                            popupAnchor: [1, -34],
                            className: 'myDivIcon'
                        });

                    }
                }
            }
            else {
                if (parseInt(item.CountValue) <= parseInt(item.DefaultLow)) {
                    isColorForIconDefined = true;
                    var fontAwesomeIcon = L.divIcon({
                        html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style=" color:' + item.DefaultLowColor + ';font-size: 3em; !important" title: "test"></i>' + activityCountSpan,
                        iconSize: [25, 41],
                        iconAnchor: [12, 41],
                        popupAnchor: [1, -34],
                        className: 'myDivIcon'
                    });

                }
                else if (parseInt(item.CountValue) <= parseInt(item.DefaultMedium)) {
                    isColorForIconDefined = true;
                    var fontAwesomeIcon = L.divIcon({
                        html: ' <i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style=" color:' + item.DefaultMediumColor + ';font-size: 3em; !important"></i>' + activityCountSpan,
                        iconSize: [25, 41],
                        iconAnchor: [12, 41],
                        popupAnchor: [1, -34],
                        className: 'myDivIcon'
                    });
                }
                else if (parseInt(item.CountValue) <= parseInt(item.DefaultHigh)) {
                    isColorForIconDefined = true;
                    var fontAwesomeIcon = L.divIcon({
                        html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style=" color:' + item.DefaultHighColor + ';font-size: 3em; !important"></i>' + activityCountSpan,
                        iconSize: [25, 41],
                        iconAnchor: [12, 41],
                        popupAnchor: [1, -34],
                        className: 'myDivIcon'
                    });
                }
                else if (parseInt(item.CountValue) >= parseInt(item.DefaultHigh)) {
                    isColorForIconDefined = true;
                    var fontAwesomeIcon = L.divIcon({
                        html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style=" color:' + item.DefaultVeryHighColor + ';font-size: 3em; !important"></i>' + activityCountSpan,
                        iconSize: [25, 41],
                        iconAnchor: [12, 41],
                        popupAnchor: [1, -34],
                        className: 'myDivIcon'
                    });
                } else {
                    if (item.deviceUrl != "" && item.deviceUrl != "NoImage") {
                      
                        isColorForIconDefined = true;
                        var fontAwesomeIcon = L.divIcon({
                            html: '<img rel="popover" id="iddevice' + item.deviceid + '" onclick = "onclickIcon(' + item.deviceid + ');" width ="25px" height="41px" src="' + item.deviceUrl + '" data-content="." data-container="body" data-toggle="popover" data-html="true" data-trigger="manual"/>' + activityCountSpan,
                            iconSize: [25, 41],
                            iconAnchor: [12, 41],
                            popupAnchor: [1, -34],
                            className: 'myDivIcon'
                        });

                    } else {
                        isColorForIconDefined = true;
                        var fontAwesomeIcon = L.divIcon({
                            html: '<i class="fa fa-map-marker fa-stack-2x" id="iddevice' + item.deviceid + '" data-container="body" data-toggle="popover" data-content="." style="color:#267fca;font-size: 3em; !important"></i>' + activityCountSpan,
                            iconSize: [25, 41],
                            iconAnchor: [12, 41],
                            popupAnchor: [1, -34],
                            className: 'myDivIcon'
                        });

                    }
                }
            }

            if (item.TrapYesNo == "True") {
                if (item.deviceUrl != "" && item.deviceUrl != "NoImage") {
                   
                    isColorForIconDefined = true;
                    var fontAwesomeIcon = L.divIcon({
                        html: '<img rel="popover" id="iddevice' + item.deviceid + '" onclick = "onclickIcon(' + item.deviceid + ');" width ="25px" height="41px" src="' + item.deviceUrl + '" data-content="." data-container="body" data-toggle="popover" data-html="true" data-trigger="manual"/>' + activityCountSpan,
                        iconSize: [25, 41],
                        iconAnchor: [12, 41],
                        popupAnchor: [1, -34],
                        className: 'myDivIcon'
                    });
                }
            }
        }
        else if (item.itemtype == 'ARROW') {

            if (item.DeviceType == "UpArrow") {

                isColorForIconDefined = false;

                var redIcon = new L.Icon({
                    iconUrl: "Images/Arrowup.png",
                    iconSize: [25, 41],
                    iconAnchor: [12, 41],
                    popupAnchor: [1, -34],
                    shadowSize: [41, 41]
                });
                options.icon = redIcon;
            }
            else if (item.DeviceType == "DownArrow") {
                isColorForIconDefined = false;
                var redIcon = new L.Icon({
                    iconUrl: "Images/Arrowdown.png",
                    iconSize: [25, 41],
                    iconAnchor: [12, 41],
                    popupAnchor: [1, -34],
                    shadowSize: [41, 41]
                });
                options.icon = redIcon;
            }
            else if (item.DeviceType == "RightArrow") {
                isColorForIconDefined = false;
                var redIcon = new L.Icon({
                    iconUrl: "Images/Arrowright.png",
                    iconSize: [25, 41],
                    iconAnchor: [12, 41],
                    popupAnchor: [1, -34],
                    shadowSize: [41, 41]
                });
                options.icon = redIcon;
            }
            else if (item.DeviceType == "LeftArrow") {
                isColorForIconDefined = false;
                var redIcon = new L.Icon({
                    iconUrl: "Images/Arrowleft.png",
                    iconSize: [25, 41],
                    iconAnchor: [12, 41],
                    popupAnchor: [1, -34],
                    shadowSize: [41, 41]
                });
                options.icon = redIcon;
            }
            else if (item.DeviceType == "UpLeftArrow") {
                isColorForIconDefined = false;
                var redIcon = new L.Icon({
                    iconUrl: "Images/Arrowupleft.png",
                    iconSize: [25, 41],
                    iconAnchor: [12, 41],
                    popupAnchor: [1, -34],
                    shadowSize: [41, 41]
                });
                options.icon = redIcon;
            }
            else if (item.DeviceType == "UpRightArrow") {
                isColorForIconDefined = false;
                var redIcon = new L.Icon({
                    iconUrl: "Images/Arrowupright.png",
                    iconSize: [25, 41],
                    iconAnchor: [12, 41],
                    popupAnchor: [1, -34],
                    shadowSize: [41, 41]
                });
                options.icon = redIcon;
            }
            else if (item.DeviceType == "DownLeftArrow") {
                isColorForIconDefined = false;
                var redIcon = new L.Icon({
                    iconUrl: "Images/Arrowdownleft.png",
                    iconSize: [25, 41],
                    iconAnchor: [12, 41],
                    popupAnchor: [1, -34],
                    shadowSize: [41, 41]
                });
                options.icon = redIcon;
            }
            else if (item.DeviceType == "DownRightArrow") {
                isColorForIconDefined = false;
                var redIcon = new L.Icon({
                    iconUrl: "Images/Arrowdownright.png",
                    iconSize: [25, 41],
                    iconAnchor: [12, 41],
                    popupAnchor: [1, -34],
                    shadowSize: [41, 41]
                });
                options.icon = redIcon;
            }
        }
        else if (item.itemtype == "Blinker") {
            isColorForIconDefined = true;

            var fontAwesomeIcon = L.icon.pulse({ iconSize: [14, 14], color: 'red' });
        }



        var deviceid = item.deviceid;
        var position = L.latLng([item.lat, item.lng]);
        var html1 = '';

        if (isColorForIconDefined) {
            var activityRow = "";
            var movementrow = "";

            if ($('input[id*=HiddenShowHoursWithActivity]').val() != "False") {
                if (isShowActivity != "None") {
                    if (isShowActivity == "ShowNumberofMovement") {
                        activityRow = "<tr><td valign='top'  style='padding-top: 6px;' height='50'><b style='font-size: 18px;'>Activity: </b></td><td valign='top' style='padding-top: 6px;' height='50'><b style='font-size: 18px;'>" + item.CountValue + "</b></td></tr> <tr><td colspan='2'><span style='height:5px;'></span></td></tr> ";
                        if (($('input[id*=rdoFloorPlanDaily]').is(":checked")) || ($('input[id*=rdoFloorPlanWeekly]').is(":checked")) || ($('input[id*=rdoFloorPlanMonthly]').is(":checked"))) {
                            movementrow = "<tr><td colspan='2'><div class='row'><div class='col-md-12'><div id='idDeviceActivitypopup'></div> </div></div></td></tr><tr><td colspan='2'><a onclick='onclickMovement(" + deviceid + "," + item.enddateyear + "," + item.enddatemonth + "," + item.enddateday + ")'><span id='idMovement' style='cursor: pointer;'>Movement</span></a></td></tr>";
                        }
                    }
                    if (isShowActivity == "ShowNumberofCatches") {
                        activityRow = "<tr><td valign='top'  style='padding-top: 6px;' height='50'><b style='font-size: 18px;'>Catches : </b></td><td valign='top' style='padding-top: 6px;' height='50'><b style='font-size: 18px;'>" + item.CountValue + "</b></td></tr> <tr><td colspan='2'><span style='height:5px;'></span></td></tr> ";
                        if (($('input[id*=rdoFloorPlanMonthly]').is(":checked"))) {
                            movementrow = "<tr><td colspan='2'><div class='row'><div class='col-md-12'><div id='idDeviceActivitypopup'></div> </div></div></td></tr><tr><td colspan='2'><a onclick='onclickCatches(" + deviceid + "," + item.enddateyear + "," + item.enddatemonth + "," + item.enddateday + ")'><span id='idCatches' style='cursor: pointer;'>Catches</span></a></td></tr>";
                        }
                    }
                }
            }
            html1 = "<table style='width:100%;padding-top: 6px;font-size: 13px;' border='0' cellpadding='10' cellspacing='10'> " + activityRow + "<tr><td valign='top'  style='padding-top: 6px;width: 100px;'>Alias:</td><td valign='top' style='padding-top: 6px;'>" + item.Alias + "</td></tr><tr><td valign='top'  style='padding-top: 6px;width: 100px;'>Device Type:</td><td valign='top' style='padding-top: 6px;'>" + item.DeviceType + "</td></tr><tr><td valign='top'  style='padding-top: 6px;'>Device ID:</td><td valign='top'  style='padding-top: 6px;'>" + deviceid + "</td></tr><tr><td valign='top' style='padding-top: 6px;'>Description:</td><td valign='top' style='padding-top: 6px;'>" + item.Description + "</td></tr> " + movementrow + " </table>";
            var marker = L.marker(position, { icon: fontAwesomeIcon }).addTo(map);

        } else {

            var activityRow = "";
            var movementrow = "";

            if ($('input[id*=HiddenShowHoursWithActivity]').val() != "False") {
                if (isShowActivity != "None") {
                    if (isShowActivity == "ShowNumberofMovement") {
                        activityRow = "<tr><td valign='top' style='padding-top: 6px;' height='50'><b style='font-size: 18px;'>Activity : </b></td><td valign='top' style='padding-top: 6px;' height='50'><b style='font-size: 18px;'>" + item.CountValue + "</b></td></tr>  <tr><td colspan='2'><span style='height:5px;'></span></td></tr>  ";
                        if (($('input[id*=rdoFloorPlanDaily]').is(":checked")) || ($('input[id*=rdoFloorPlanWeekly]').is(":checked")) || ($('input[id*=rdoFloorPlanMonthly]').is(":checked"))) {
                            movementrow = "<tr><td colspan='2'><div class='row'><div class='col-md-12'><div id='idDeviceActivitypopup'></div> </div></div></td></tr><tr><td colspan='2'><a onclick='onclickMovement(" + deviceid + "," + item.enddateyear + "," + item.enddatemonth + "," + item.enddateday + ")'><span id='idMovement' style='cursor: pointer;'>Movement</span></a></td></tr>";
                        }
                    }
                    if (isShowActivity == "ShowNumberofCatches") {
                        activityRow = "<tr><td valign='top' style='padding-top: 6px;' height='50'><b style='font-size: 18px;'>Catches : </b></td><td valign='top' style='padding-top: 6px;' height='50'><b style='font-size: 18px;'>" + item.CountValue + "</b></td></tr>  <tr><td colspan='2'><span style='height:5px;'></span></td></tr>  ";
                        if (($('input[id*=rdoFloorPlanMonthly]').is(":checked"))) {
                            movementrow = "<tr><td colspan='2'><div class='row'><div class='col-md-12'><div id='idDeviceActivitypopup'></div> </div></div></td></tr><tr><td colspan='2'><a onclick='onclickCatches(" + deviceid + "," + item.enddateyear + "," + item.enddatemonth + "," + item.enddateday + ")'><span id='idCatches' style='cursor: pointer;'>Catches</span></a></td></tr>";
                        }
                    }
                }
            }

            html1 = "<table style='width:100%;font-size: 13px;'  border='0' cellpadding='10' cellspacing='10'> " + activityRow + " <tr><td valign='top'  style='padding-top: 6px;width: 100px;'>Alias:</td><td valign='top' style='padding-top: 6px;'>" + item.Alias + "</td></tr><tr><td valign='top' style='padding-top: 6px;width: 100px;'>Device Type:</td><td valign='top' style='padding-top: 6px;'>" + item.DeviceType + "</td></tr><tr><td valign='top' style='padding-top: 6px;'>Device ID:</td><td valign='top' style='padding-top: 6px;'>" + deviceid + "</td></tr><tr><td valign='top'  style='padding-top: 6px;'>Description:</td><td valign='top'  style='padding-top: 6px;'>" + item.Description + "</td></tr> " + movementrow + " </table>";
            var marker = L.marker(position, options).addTo(map);
        }

        marker.name = deviceid;
        marker.Lat = item.lat;
        marker.Lng = item.lng;
        marker.DeviceName = item.name;
        marker.html1 = html1;
        marker.on("click", clickMarker);

        if (item.itemtype == "Blinker") {
            previousMarkerBlink = marker;
        }

        markers.push(marker);
    }
}

function onclickIcon(id) {
    $("#" + id).popover('show');
}

function clickMarker() {
    //$Piechartpopup = this;
    //var alertNoData = "<span style='color:black;'> No Device by Activity Status found.</span>";

    //$("#iddevice16").popover('show');
    if ($('.popover').hasClass('in')) {
        $(this).popover('hide');
    }
    else {
        $("#iddevice" + this.name + "").attr('data-html', 'true');
        $("#iddevice" + this.name + "").attr('data-title', ' <a href="#" class="devicedetails" data-dismiss="alert"><img style="height:30px;" src="/Images/delete.png"/></a>   Device Details');
        $("#iddevice" + this.name + "").attr('data-content', this.html1);
        $("#iddevice" + this.name + "").popover('show');
    }

    if (!hasActiveLine) {
        polylineLatlngs.push(this.getLatLng());
        activeItemName = this.name;
        hasActiveLine = true;
    } else {
        if (this.name == activeItemName) {
            polylineLatlngs.push(this.getLatLng());
            //L.polyline(polylineLatlngs, {
            //    color: 'red'
            //}).addTo(map);
            this.polyline = polylineLatlngs;
            polylineLatlngs = [];
            hasActiveLine = false;
        }
    }
}

function setupItems(map, markers) {


    //$('.drag').draggable({
    //    helper: 'clone',
    //    containment: 'map',
    //    stop: function (evt, ui) {
    //        var position = map.mouseEventToLatLng(evt);
    //        var markerCnt = $(this).data("markerCnt");
    //        if (markerCnt != undefined)
    //            $(this).data("markerCnt", markerCnt + 1);
    //        else
    //            $(this).data("markerCnt", 1);

    //        var item = {
    //            name: $(this).text(),
    //            position: position,
    //            deviceid: $(this).deviceid,
    //            Lat: position.lat,
    //            Lng: position.lng,
    //            markerCnt: $(this).data("markerCnt")
    //        };


    //        addItem(map, markers, item);
    //    }
    //});
}

function setupExport(markers) {
    $('.export').click(function () {
        var result = [];

        for (var i = 0; i < markers.length; i++) {
            var data = {
                name: markers[i].name,
                position: markers[i]._latlng,
                polyline: markers[i].polyline,
                color: markers[i].color
            };
            result.push(data);
        }
        var json = JSON.stringify(result);
        $('#data').val(json);
    });
}

function setupImport(map) {
    var markers = [];

    $('.import').click(function () {
        if (markers) {
            for (i = 0; i < markers.length; i++) {
                map.removeLayer(markers[i]);
            }
        }
        var json = $('#data').val();
        var result = JSON.parse(json);
        if (result && $.isArray(result)) {
            for (var i = 0; i < result.length; i++) {
                addItem(map, markers, result[i]);
            }
        }
    });

    return markers;
}

function monthNames(arg) {
    var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
         "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

    var selectedMonthName = months[arg];

    return selectedMonthName;
}