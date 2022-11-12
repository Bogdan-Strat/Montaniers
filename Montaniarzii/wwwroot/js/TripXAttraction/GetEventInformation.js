//import '/C:/Users/bogdan.strat/source/repos/Montaniarzii/Montaniarzii/package.json/leaflet'
//// import script after leaflet
//import 'leaflet-simple-map-screenshoter'



// create map

let map = new L.Map('map').setView([attractionsCoordinates[0][0], attractionsCoordinates[0][1]], 7);
map.createPane("snapshot-pane");
map.createPane("dont-include");

var control = L.control.layers(null, null, {
    collapsed: false
}).addTo(map);
let layer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {pane: "snapshot-pane"});
var HikingTrails = L.tileLayer('https://tile.waymarkedtrails.org/{id}/{z}/{x}/{y}.png', {
    id: 'hiking',
    pointable: true,
    attribution: '&copy; <a href="http://waymarkedtrails.org">Sarah Hoffmann</a> (<a href="https://creativecommons.org/licenses/by-sa/3.0/">CC-BY-SA</a>)',
    pane: "snapshot-pane"
});
var CyclingTrails = L.tileLayer('https://tile.waymarkedtrails.org/{id}/{z}/{x}/{y}.png', {
    id: 'cycling',
    pointable: true,
    attribution: '&copy; <a href="http://waymarkedtrails.org">sarah hoffmann</a> (<a href="https://creativecommons.org/licenses/by-sa/3.0/">cc-by-sa</a>)',
    pane: "dont-include"
});

control.addOverlay(HikingTrails, "Hiking Routes");
control.addOverlay(CyclingTrails, "Cycling Routes");
map.addLayer(layer);

let marker1, marker2;
let latlngs = Array(); 

//marker1 = new L.Marker([attractionsCoordinates[0][0], attractionsCoordinates[0][1]]);
//marker1.addTo(map);
//latlngs.push(marker1.getLatLng());

//coordinates = "";
//coordinates += attractionsCoordinates[0][0].toString() + ",";
//coordinates += attractionsCoordinates[0][1].toString() + ";";
//coordinates += attractionsCoordinates[attractionsCoordinates.length - 1][0].toString() + ",";
//coordinates += attractionsCoordinates[attractionsCoordinates.length - 1][1].toString();
//console.log(coordinates);


    //$.ajax({
    //    type: "get",
    //    url: "https://router.project-osrm.org/route/v1/hiking/" + coordinates + "?geometries=polyline&steps=true&annotations=false&overview=full",
    //    success: (resp) => {
            
    //        console.log(resp.routes[0].legs[0].steps);
    //        /*console.log(resp.routes[0].geometry.coordinates);*/
    //        let coord = [];
    //        for (let i = 0; i < resp.routes[0].legs[0].steps.length; i++) {
    //            for (let j = 0; j < resp.routes[0].legs[0].steps[i].intersections.length; j++) {
    //                coord.push(resp.routes[0].legs[0].steps[i].intersections[j].location);
    //            }
                
    //        }
    //        console.log(coord);
    //        debugger
    //        for (let i = 0; i < coord.length; i++) {
    //            latlngs.push(new L.Marker([coord[i][0], coord[i][1]]).getLatLng());
    //        }

    //        for (let i = 1; i < attractionsCoordinates.length; i++) {
    //            marker2 = new L.Marker([attractionsCoordinates[i][0], attractionsCoordinates[i][1]]);
    //            marker2.addTo(map);
    //            latlngs.push(marker2.getLatLng());
    //        }
    //        //let polyline = L.polyline(latlngs, { color: 'red' }).addTo(map);

    //        //// zoom the map to the polyline
    //        //map.fitBounds(polyline.getBounds());
    //    },
    //    error: (err) => {

    //    }
    //});


for (let i = 0; i < attractionsCoordinates.length; i++) {
    marker2 = new L.Marker([attractionsCoordinates[i][0], attractionsCoordinates[i][1]],
        {
            pane: "snapshot-pane"
        });
    marker2.addTo(map);
    /*latlngs.push(marker2.getLatLng());*/
}

const snapshotOptions = {
    hideElementsWithSelectors: [
        ".leaflet-control-container",
        ".leaflet-dont-include-pane",
        "#snapshot-button"
    ],
    hidden: true
};

// Add screenshotter to map
const screenShotter = new L.simpleMapScreenshoter(snapshotOptions);
screenShotter.addTo(map);

const takeScreenShot = () => {
    screenShotter
        .takeScreen("image")
        .then((image) => {
            let img = new Image();
        })
};
var elementToCapture = document.getElementById('map');
const button = document.getElementById("screenshot-button");
button.addEventListener("click", (e) => {
    html2canvas(elementToCapture, html2canvasConfiguration).then(function (canvas) {
        var link = document.createElement('a');
        link.download = 'test.png';
        link.href = canvas.toDataURL();
        link.click();
        link.remove();
    });
});

var html2canvasConfiguration = {
    useCORS: true,
    width: map._size.x,
    height: map._size.y,
    backgroundColor: null,
    logging: true,
    imageTimeout: 0
};


//html2canvas(elementToCapture, html2canvasConfiguration).then(function (canvas) {
//    var link = document.createElement('a');
//    link.download = 'test.png';
//    link.href = canvas.toDataURL();
//    link.click();
//    link.remove();
//})