let attractionInput = document.getElementById("search-attraction");
let searchBtn = document.getElementById("search-train-stations");

attractionInput.onkeyup = (text) => {
    let value = text.target.value;

    $.ajax({
        type: "get",
        url: "https://localhost:7266/Attraction/GetAllAttractionsWithCoordinatesByPartiallyName?partOfName=" + value,
        success: (resp) => {

            let listOfSuggestions = document.getElementById("suggestion-list");
            listOfSuggestions.innerHTML = "";

            for (let i = 0; i < resp.length; i++) {
                let suggestion = document.createElement("option");
                suggestion.style.cursor = "pointer";
                suggestion.innerHTML = resp[i].name;
                suggestion.value = resp[i].id;
                suggestion.dataset.longitude = resp[i].longitude;
                suggestion.dataset.latitude = resp[i].latitude;

                suggestion.addEventListener("click", (e) => {
                    let search = document.getElementById(attractionInput.id);
                    search.value = e.target.innerHTML;
                    search.dataset.attractionid = e.target.value;
                    search.dataset.latitude = e.target.dataset.latitude;
                    search.dataset.longitude = e.target.dataset.longitude;
                });
                listOfSuggestions.appendChild(suggestion);

            }
        },
        error: (err) => {
        }
    });
}

let trainStationsDiv = document.getElementById("train-stations");
let loadMoreDiv = document.getElementById("load-more");

let displayTrainStation = (trainStation) => {
    let div = document.createElement("div");
    div.classList.add("list-item");
    let h4 = document.createElement("h4");
    let divLocation = document.createElement("div");
    let divDistance = document.createElement("div");
    let iLocation = document.createElement("i");
    let iDistance = document.createElement("i");
    let bLocattion = document.createElement("b");
    let bDistance = document.createElement("b");

    bLocattion.style = "font-weight: normal; margin-left:7px";
    bLocattion.innerHTML = trainStation.location;

    bDistance.style = "font-weight: normal; margin-left:7px";
    bDistance.innerHTML = parseFloat(trainStation.distance).toFixed(2) + " km";

    iLocation.classList.add("fa-solid");
    iLocation.classList.add("fa-location-dot");

    iDistance.classList.add("fa-solid");
    iDistance.classList.add("fa-road");

    h4.innerHTML = trainStation.trainStationName;

    divLocation.appendChild(iLocation);
    divLocation.appendChild(bLocattion);

    divDistance.appendChild(iDistance);
    divDistance.appendChild(bDistance);

    div.appendChild(h4);
    div.appendChild(divLocation);
    div.appendChild(divDistance);

    trainStationsDiv.appendChild(div);
}

let putMarkerForTrainsStationOnMap = (latitude, longitude, map) => {
    let marker = new L.Marker([Number(latitude), Number(longitude)]);
    marker.addTo(map);
};

// create map
let map = new L.Map('map').setView([45.882, 24.868], 7);
var control = L.control.layers(null, null, {
    collapsed: false
}).addTo(map);
let layer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png');
var HikingTrails = L.tileLayer('https://tile.waymarkedtrails.org/{id}/{z}/{x}/{y}.png', {
    id: 'hiking',
    pointable: true,
    attribution: '&copy; <a href="http://waymarkedtrails.org">Sarah Hoffmann</a> (<a href="https://creativecommons.org/licenses/by-sa/3.0/">CC-BY-SA</a>)',
});
var CyclingTrails = L.tileLayer('https://tile.waymarkedtrails.org/{id}/{z}/{x}/{y}.png', {
    id: 'cycling',
    pointable: true,
    attribution: '&copy; <a href="http://waymarkedtrails.org">sarah hoffmann</a> (<a href="https://creativecommons.org/licenses/by-sa/3.0/">cc-by-sa</a>)',
});

control.addOverlay(HikingTrails, "Hiking Routes");
control.addOverlay(CyclingTrails, "Cycling Routes");
map.addLayer(layer);

searchBtn.addEventListener("click", () => {
    count = 0;
    document.getElementById("suggestion-list").innerHTML = "";
    document.getElementById("train-stations").innerHTML = "";
    document.getElementById("load-more").innerHTML = "";
    if (attractionInput.dataset.attractionid == "00000000-0000-0000-0000-000000000000") {
        
    }
    else {
        map.eachLayer(function (layer) {
            map.removeLayer(layer);
        });
        map.addLayer(layer);
        $.ajax({
            type: "get",
            url: "https://localhost:7266/TrainStation/GetClosestTrainStationsPaged?attractionId=" + attractionInput.dataset.attractionid + "&count=" + count,
            success: (resp) => {
                count++;

                for (let i = 0; i < resp.length; i++) {
                    displayTrainStation(resp[i]);
                }

                document.getElementById("suggestion-list").innerHTML = "";

                if (count === 1) {
                    a = document.createElement("a");
                    a.innerHTML = "Load More";
                    a.classList.add("btn");
                    a.classList.add("btn-secondary");
                    a.classList.add("rounded-pill");

                    loadMoreDiv.addEventListener("click", () => {
                        $.ajax({
                            type: "get",
                            url: "https://localhost:7266/TrainStation/GetClosestTrainStationsPaged?attractionId=" + attractionInput.dataset.attractionid + "&count=" + count,
                            success: (resp) => {
                                count++;
                                for (let i = 0; i < resp.length; i++) {
                                    displayTrainStation(resp[i]);
                                    putMarkerForTrainsStationOnMap(resp[i].latitude, resp[i].longitude, map);
                                }
                            },
                            error: (err) => {
                            }
                        });
                    });

                    loadMoreDiv.appendChild(a);

                    // create map
                    // put pin on attraction
                    //let mapOptions = {
                    //    center: [Number(attractionInput.dataset.latitude), Number(attractionInput.dataset.longitude)],
                    //    zoom: 15
                    //};

                    /*var map = new L.Map('map', mapOptions);*/
                    

                    
                    map.setView([Number(attractionInput.dataset.latitude), Number(attractionInput.dataset.longitude)], 12);
                    

                    let customIcon = L.icon({
                        iconUrl: "/PozeAplicatie/customMarker.png",
                        iconSize: [40, 40]
                    });

                    let marker = new L.Marker([Number(attractionInput.dataset.latitude), Number(attractionInput.dataset.longitude)], { icon: customIcon });
                    marker.addTo(map);
                    /*map.removeLayer(marker);*/
                }
                for (let i = 0; i < resp.length; i++) {
                    putMarkerForTrainsStationOnMap(resp[i].latitude, resp[i].longitude, map);
                }

            },
            error: (err) => {
            }
        });
    }

});

//let mapOptions = {
//    center: [45.00,25.00],
//    zoom: 15
//};

//let map = new L.Map('map');
//map.mapOptions = mapOptions;

//let layer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png');
//map.addLayer(layer);



