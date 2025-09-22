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

var dragedDeviceId = "";
var isAlertShowed = false;
var markerPositionBeforeDrag = "";
var markerlatBeforeDrag = "";
var markerlngBeforeDrag = "";
var markerPositionAfterDrag = "";
var markerlatAfterDrag = "";
var markerlngAfterDrag = "";

var eventAfterDrag = [];

var isRcNoZero = false;
var hiddenDeviceID = "";
var previousMarkerBlink = [];

map.on('click', function (e) {
    if (hasActiveLine) {
        polylineLatlngs.push(e.latlng);
        //L.polyline(polylineLatlngs, {
        //    color: 'red'
        //}).addTo(map);
    }
});

map.on('zoomend', function () {
    map.invalidateSize();
});

function setupMap() {
    var map = L.map('map', {
        minZoom: 1,
        maxZoom: 15,
        center: [0, 0],
        zoom: 5,
        zoomAnimation:false,
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

function addItem(map, markers1, item) {
    var isColorForIconDefined = false;
    //if (!item.markerCnt) {
        var options = {
            draggable: true
        };
      
            if (item.itemtype == 'DEVICE')
            {
                if (item.deviceurl != "") {
                    if (item.deviceurl == "NoImage")
                    {
                        isColorForIconDefined = false;
                        var redIcon = new L.Icon({
                            iconUrl: 'Images/default.png',
                            shadowUrl: 'Images/marker-shadow.png',
                            iconSize: [25, 41],
                            iconAnchor: [12, 41],
                            popupAnchor: [1, -34],
                            shadowSize: [41, 41]
                        });
                        options.icon = redIcon;
                    }
                    else
                    {
                        isColorForIconDefined = false;
                        var redIcon = new L.Icon({
                            iconUrl: item.deviceurl,
                            shadowUrl: 'Images/marker-shadow.png',
                            iconSize: [30, 30],
                            iconAnchor: [12, 41],
                            popupAnchor: [1, -34],
                            shadowSize: [30, 30]
                        });
                        options.icon = redIcon;

                    }
                }
                else{

                    isColorForIconDefined = false;
                    var redIcon = new L.Icon({
                        iconUrl: 'Images/default.png',
                        shadowUrl: 'Images/marker-shadow.png',
                        iconSize: [25, 41],
                        iconAnchor: [12, 41],
                        popupAnchor: [1, -34],
                        shadowSize: [41, 41]
                    });
                    options.icon = redIcon;
                }

            }
            else if (item.itemtype == 'ARROW') {

                if (item.devicetype == "UpArrow") {

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
                else if (item.devicetype == "DownArrow"){
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
                else if (item.devicetype == "RightArrow") {
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
                else if (item.devicetype == "LeftArrow") {
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
                else if (item.devicetype == "UpLeftArrow") {
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
                else if (item.devicetype == "UpRightArrow") {
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
                else if (item.devicetype == "DownLeftArrow") {
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
                else if (item.devicetype == "DownRightArrow") {
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

            
    //else {
    //            isColorForIconDefined = false;
    //            var redIcon = new L.Icon({
    //                iconUrl: 'Images/default.png',
    //                shadowUrl: 'Images/marker-shadow.png',
    //                iconSize: [25, 41],
    //                iconAnchor: [12, 41],
    //                popupAnchor: [1, -34],
    //                shadowSize: [41, 41]
    //            });
    //            options.icon = redIcon;
    //    }

    //var deviceid = item.deviceid;

            var showlabelFloorPlan = '';
            if ($('input[id*=rdoShowLabelOn]').is(":checked")) {
                showlabelFloorPlan = "True";
            }
            else {
                showlabelFloorPlan = "False";
            }


            var position = L.latLng([item.position.lat, item.position.lng]);
            if (isColorForIconDefined) {
                if (showlabelFloorPlan == "True" && item.AliasName != "" || item.Description != "") {
                    
                    var marker = L.marker(position, { icon: fontAwesomeIcon }).addTo(map).bindPopup("<table style=\"width:100%\" border=\"0\" cellpadding=\"10\" cellspacing=\"10\"> <tr><td> Device Type:</td><td>" + item.devicetype + "</td></tr><tr><td> Device ID :</td><td>" + item.deviceid + "</td></tr><tr><td> Alias Name:</td><td>" + item.AliasName + "</td></tr> <tr><td> Device Placed date:</td><td>" + item.DevicePlacedDate + "</td><td><a onclick=\"return onclickHistory(" + item.deviceid + ")\">History</a></td></tr><tr><td> Description:</td><td>" + item.Description + "</td><td><i class='fa fa-edit'  onclick=\"return onclickDescriptionEdit(" + item.deviceid + ",'" + item.Description.replace("'", "\'") + "')\" ></i></td></tr><tr><td colspan=2 align=center><button type=\"button\" class=\"btn btn-warning\" id=\"btnDeletedevice\" title=\"Delete\" onclick=\"return onClickdeletedevice('" + item.deviceid + "','" + item.DeviceCount + "')\">Delete</button></td></tr></table>", { maxWidth:1200});
                }
                else {
                    var marker = L.marker(position, { icon: fontAwesomeIcon }).addTo(map).bindPopup("<table style=\"width:100%\" border=\"0\" cellpadding=\"10\" cellspacing=\"10\"> <tr><td> Device Type:</td><td>" + item.devicetype + "</td></tr><tr><td> Device ID :</td><td>" + item.deviceid + "</td></tr><tr><td> Device Placed date:</td><td>" + item.DevicePlacedDate + "</td><td><a onclick=\"return onclickHistory(" + item.deviceid + ")\">History</a></td></tr> <tr><td> Description:</td><td>" + item.Description + "</td><td><i class='fa fa-edit' onclick=\"return onclickDescriptionEdit(" + item.deviceid + ",'" + item.Description.replace("'", "\'") + "')\" ></i></td></tr><tr><td colspan=2 align=center><button type=\"button\" class=\"btn btn-warning\" id=\"btnDeletedevice\" title=\"Delete\" onclick=\"return onClickdeletedevice('" + item.deviceid + "','" + item.DeviceCount + "')\">Delete</button></td></tr></table>", { maxWidth: 1200 });
                }
                   //var marker = L.marker(position, { icon: fontAwesomeIcon }).addTo(map).bindPopup("<table style=\"width:100%\" border=\"0\" cellpadding=\"10\" cellspacing=\"10\"> <tr><td> Device Type:</td><td>" + item.devicetype + "</td></tr><tr><td> Device ID :</td><td>" + item.deviceid + "</td></tr><tr><td> Alias Name:</td><td>" + item.AliasName + "</td></tr> <tr><td> Device Placed date:</td><td>" + item.DevicePlacedDate + "</td></tr>   <tr><td colspan=2 align=center><button type=\"button\" class=\"btn btn-warning\" id=\"btnDeletedevice\" title=\"Delete\" onclick=\"return onClickdeletedevice('" + item.deviceid + "')\">Delete</button></td></tr></table>");
            }
            else 
            {
                if (showlabelFloorPlan == "True" && item.AliasName != "" || item.Description != "") {

                    var marker = L.marker(position, options).addTo(map).bindPopup("<table style=\"width:100%\" border=\"0\" cellpadding=\"10\" cellspacing=\"10\"> <tr><td> Device Type:</td><td>" + item.devicetype + "</td></tr><tr><td> Device ID :</td><td>" + item.deviceid + "</td></tr><tr><td> Alias Name:</td><td>" + item.AliasName + "</td></tr> <tr><td> Device Placed Date:</td><td>" + item.DevicePlacedDate + "</td><td><a onclick=\"return onclickHistory(" + item.deviceid + ")\">History</a></td></tr> <tr><td> Description:</td><td>" + item.Description + "</td><td><i class='fa fa-edit' onclick=\"return onclickDescriptionEdit(" + item.deviceid + ",'" + item.Description.replace("'", "\'") + "')\" ></i></td></tr> <tr><td colspan=2 align=center><button type=\"button\" class=\"btn btn-warning\" id=\"btnDeletedevice\" title=\"Delete\" onclick=\"return onClickdeletedevice('" + item.deviceid + "','" + item.DeviceCount + "')\">Delete</button></td></tr></table>", { maxWidth: 1200 });
                }
                else {
                    var marker = L.marker(position, options).addTo(map).bindPopup("<table style=\"width:100%\" border=\"0\" cellpadding=\"10\" cellspacing=\"10\"> <tr><td> Device Type:</td><td>" + item.devicetype + "</td></tr><tr><td> Device ID :</td><td>" + item.deviceid + "</td></tr><tr><td> Device Placed Date:</td><td>" + item.DevicePlacedDate + "</td><td><a onclick=\"return onclickHistory(" + item.deviceid + ")\">History</a></td></tr> <tr><td> Description:</td><td>" + item.Description + "</td><td><i class='fa fa-edit' onclick=\"return onclickDescriptionEdit(" + item.deviceid + ",'" + item.Description.replace("'", "\'") + "')\" ></i></td></tr>  <tr><td colspan=2 align=center><button type=\"button\" class=\"btn btn-warning\" id=\"btnDeletedevice\" title=\"Delete\" onclick=\"return onClickdeletedevice('" + item.deviceid + "','" + item.DeviceCount + "')\">Delete</button></td></tr></table>", { maxWidth: 1200 });
                }
            }
            marker.name = item.deviceid;
            marker.Lat = item.Lat;
            marker.Lng = item.Lng;
            marker.deviceid = item.deviceid;
            marker.itemtype = item.itemtype;
            marker.devicename = item.devicename;
            marker.devicetype = item.devicetype;
            marker.deviceurl = item.deviceurl;
            marker.RcNo = item.RcNo;
            marker.DevicePlacedDate = item.DevicePlacedDate;
            marker.AliasName = item.AliasName;
            marker.Description = item.Description.replace("'", "");
            marker.on("click", clickMarker);
            marker.on("drag", dragMarker);
            marker.on("dragend", dragend1);
            marker.on("dragstart", dragstart1);
    //marker.color = (item.markerCnt == 2 || item.color == "red") ? "red" : "blue";

            if (item.itemtype == "Blinker") {
                previousMarkerBlink = marker;
            }
            markers.push(marker);

            if (item.RcNo == 0) {
                hiddenDeviceID = item.deviceid;
                isRcNoZero = true;
                var alertText = "Are you  sure want move this device  - " + item.deviceid + "   dated ";
                $("#lblAlertdevicePlacedDate").text(alertText);
                $('#ContentPlaceHolder1_txtAlias').val('');
                $('#ContentPlaceHolder1_txtDescription').val(item.Description);
                $("#ContentPlaceHolder1_txtDescription").prop('disabled', true);
                $('#GetDevicePlacedDatePopup').modal('show');
            }
            else {
                isRcNoZero = false;
                $("#lblAlertdevicePlacedDate").text("");                
            }
        }
    
function dragend1(event) {

    markerlatAfterDrag = event.target._latlng.lat;
    markerlngAfterDrag = event.target._latlng.lng;
    markerPositionAfterDrag = event.target._latlng;

    //checkForExsistingDevice();

    eventAfterDrag = event.target;

    var alertText = "Are you sure want move this device  - " + dragedDeviceId + "   dated ";
    $("#lblAlertdevicePlacedDate").text(alertText);
    var currentDate = new Date();
    var currentDateYear = ''; var currentDateMonth = ''; var currentDateDay = '';

    var currentDateYear = currentDate.getFullYear();
    var currentDateMonth = (currentDate.getMonth() + 1);
    var currentDateDay = currentDate.getDate();

    var deviceDate = currentDateDay + "/" + currentDateMonth + "/" + currentDateYear;
    $("#ContentPlaceHolder1_dtpDevicePlacedDate").val(deviceDate);
    $("#ContentPlaceHolder1_txtAlias").val(eventAfterDrag.AliasName);
    $("#ContentPlaceHolder1_txtDescription").val(eventAfterDrag.Description);
    $("#ContentPlaceHolder1_HiddenEditAliasname").val(eventAfterDrag.AliasName);
    showDevicePlacedDatePopup();
    return false;


        //var r = confirm("Are you  sure want move this device " + dragedDeviceId + "  dated " + $("[id*=HiddenDatetimeNow]").val() + "  ?");
        //    if (!r) {

                //var markerRemove = null;
                //for (var i = 0; i < markers.length; i++) {

                //    if (markers[i].deviceid == event.target.deviceid)
                //    {

                //        var item = {
                //            name: event.target.name,
                //            devicename: event.target.name,
                //            devicetype: event.target.devicetype,
                //            itemtype: event.target.itemtype,
                //            deviceid: event.target.deviceid,
                //            deviceurl: event.target.deviceurl,
                //            RcNo: event.target.RcNo,
                //            Lat: markerlatBeforeDrag,
                //            Lng: markerlngBeforeDrag,
                //            position: markerPositionBeforeDrag,
                //            DevicePlacedDate: $("[id*=HiddenDatetimeNow]").val(),
                //        };
                //            markerRemove = markers[i];
                //            var index = markers.indexOf(markerRemove);
                //            if (index > -1) {
                //                markers.splice(index, 1);
                //            }

                //            map.removeLayer(markerRemove);
                //            addItem(map, markers, item);
                //        };
                //    }
                //}
            //    return false;
        }

function dragstart1(event) {

    markerPositionBeforeDrag = event.target._latlng;
    markerlatBeforeDrag = event.target._latlng.lat;
    markerlngBeforeDrag = event.target._latlng.lng;

}


function dragMarker() {

    dragedDeviceId = this.name;
}

function clickMarker() {

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

function setupItems(map, markers1) {
    $('.drag').draggable({
        helper: 'clone',
        containment: 'map',
        stop: function (evt, ui) {

            
            //var coords = $('#main').position();
            //coords.bottom = coords.top + $('#main').height();
            //coords.bottomRight = coords.left + $('#main').width();
            //console.log("markers start 2");
            //console.log(ui.position);
            //console.log("markers start 3");
            //console.log(coords);
            //console.log("markers start 4");
            //console.log($(this).data("uiDraggable").originalPosition);

            //if (ui.position.top >= coords.top && ui.position.top <= coords.bottom && ui.position.left >= coords.left && ui.position.left <= coords.bottomRight)
            //{
            //    console.info("inside");
            //} else {
            //    console.info("outside");
            //    return;
            //}


            var position = map.mouseEventToLatLng(evt);
            var markerCnt = $(this).data("markerCnt");

            if (markerCnt != undefined)
                $(this).data("markerCnt", markerCnt + 1);
            else
                $(this).data("markerCnt", 1);

            var arrowID = "0"

           var deviceid = "";

            if ($(this).attr("itemtype") == 'DEVICE') {
                deviceid = $(this).attr("deviceid");
            }
            else if ($(this).attr("itemtype") == 'ARROW')
            {
                arrowID = $("[id*=HiddenArrowID]").val();
                var arrowName = $(this).attr("devicetype");

                if (arrowID == null)
                {
                    arrowID = 1;
                }
                else
                {
                    arrowID = parseInt(arrowID) + 1;
                }

                deviceid = arrowName + arrowID;

                $("[id*=HiddenArrowID]").val(arrowID);
            }
            var item = {
                name: $(this).attr("deviceid"),
                devicename: $(this).attr("devicename"),
                devicetype: $(this).attr("devicetype"),
                itemtype: $(this).attr("itemtype"),
                deviceid: deviceid,
                deviceurl: $(this).attr("deviceurl"),
                RcNo: $(this).attr("RcNo"),
                Description: $(this).attr("devicedescription").replace("'", ""),
                Lat: position.lat,
                Lng: position.lng,
                position: position,
                DevicePlacedDate: $("[id*=HiddenDatetimeNow]").val(),
                AliasName: $("[id*=HiddenAliasName]").val(),
                //ShowLabels: $("[id*=HiddenShowLabel]").val(),
                ShowLabels: $(this).attr("ShowLabel"),

                markerCnt: $(this).data("markerCnt")
            };

            if ($(this).attr("itemtype") == 'DEVICE')
            {
                evt.preventDefault();
                var el = document.getElementById($(this).closest('li').attr('id'));
                el.parentNode.removeChild(el);
            }
            else if ($(this).attr("itemtype") == 'ARROW')
            {
                $(this).data("uiDraggable").originalPosition = {
                    top: 0,
                    left: 0
                };
            }

            addItem(map, markers, item);
        }
    });
   
}

//function setupExport(markers) {
//    $('.export').click(function () {
//        var result = [];

//        for (var i = 0; i < markers.length; i++) {
//            var data = {
//                name: markers[i].name,
//                position: markers[i]._latlng,
//                polyline: markers[i].polyline,
//                color: markers[i].color
//            };
//            result.push(data);
//        }
//        var json = JSON.stringify(result);
//        $('#data').val(json);
//    });
//}

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


