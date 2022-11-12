let main = document.getElementsByTagName("main");
main[0].style.overflowY = "scroll";
main[0].style.height = "1000px";

let bigDiv = document.getElementsByClassName("container")[0];

let checkboxListMountains = document.getElementById("checkbox-list-mountains");
let searchLocationInput = document.getElementById("search-location");

let displayCheckbox = (mountain) => {
    let option = document.createElement("option");

    option.innerHTML = mountain;
    option.value = mountain;
    option.classList.add("1");
    $('.js-example-basic-multiple').on('select2:select', function (e) {
        count = 0;
        attractionsDiv.innerHTML = "";
        getFunction("https://localhost:7266/Attraction/GetAllAttractionsPagedFilteredByPartiallyNameMountainsHeightAndLocation?partOfName=" + searchInput.value + "&mountains=" + collectAllCheckboxes() + "&height=" + rangeHeight.value + "&location=" + searchLocationInput.value + "&count=" + count);
    });
    $('.js-example-basic-multiple').on('select2:unselect', function (e) {
        count = 0;
        attractionsDiv.innerHTML = "";
        getFunction("https://localhost:7266/Attraction/GetAllAttractionsPagedFilteredByPartiallyNameMountainsHeightAndLocation?partOfName=" + searchInput.value + "&mountains=" + collectAllCheckboxes() + "&height=" + rangeHeight.value + "&location=" + searchLocationInput.value + "&count=" + count);
    });

    checkboxListMountains.appendChild(option);
}

$.ajax({
    type: "get",
    url: "https://localhost:7266/Attraction/GeAllMountains",
    success: (resp) => {
        $(document).ready(function () {
            $('.js-example-basic-multiple').select2({
                placeholder: "Select mountains"
            });

        });
        for (let i = 0; i < resp.length; i++) {
            displayCheckbox(resp[i]);
        }
    },
    error: (err) => {
        if (err.status == 404) {
            location.href = "https://localhost:7266/CustomError/Error404";
        }
        else if (err.status == 400) {
            location.href = "https://localhost:7266/CustomError/Error_BadRequest";
        }
    }
});


var attractionsDiv = document.getElementById("attractions");
let searchInput = document.getElementById("search-attraction");
let searchBtn = document.getElementById("search-btn");

let canDoRequest = true;
// checkbox list attraction

let collectAllCheckboxes = () => {
    let arrcheckboxes = $('.js-example-basic-multiple').find(':selected')
    let mountains = [];
    for (let i = 0; i < arrcheckboxes.length; i++) {
        mountains.push(arrcheckboxes[i].text)
    }

    return mountains
}

let getFunction = (url) => {
    if (canDoRequest) {
        canDoRequest = false;
        $.ajax({
            type: "get",
            url: url,
            success: (resp) => {
                count++;
                if (resp.length == 0) {
                    let div = document.createElement("div");
                    let h4 = document.createElement("h4");

                    if (attractionsDiv.innerHTML === "") {
                        h4.innerHTML = "No data is matching with your filters";
                    }
                    
                    div.appendChild(h4);
                    attractionsDiv.appendChild(div);

                    canDoRequest = true;
                }
                else {
                    for (let i = 0; i < resp.length; i++) {
                        displayAttractions(resp[i]);
                    }
                    canDoRequest = true;
                }

            },
            error: (err) => {
                canDoRequest = true;
            }
        });
    }
}
let displayAttractions = (attraction) => {
    let divAttraction = document.createElement("div");
    divAttraction.classList.add("list-item");
    let ul = document.createElement("ul");
    let h4 = document.createElement("h4");
    let a = document.createElement("a");

    a.href = "/Attraction/GetAttractionInformation?attractionId=" + attraction.attractionId
    h4.innerHTML = attraction.attractionType + " " + attraction.attractionName;

    let locationLi = document.createElement("li");
    let heightLi = document.createElement("li");
    let mountainsLi = document.createElement("li");

    if (!(attraction.height === undefined)) {
        heightLi.innerHTML = "Height: " + attraction.height + " m";
    }
    else {
        heightLi.innerHTML = "Height: Unknown";
    }

    if (!(attraction.mountains === undefined)) {
        mountainsLi.innerHTML = "Mountains: " + attraction.mountains;
    }
    else {
        mountainsLi.innerHTML = "Mountains: Unknown";
    }

    if (!(attraction.mountains === undefined)) {
        locationLi.innerHTML = "Location: " + attraction.location;
    }
    else {
        locationLi.innerHTML = "Location: " + attraction.attractionName;
    }


    ul.appendChild(locationLi);
    ul.appendChild(heightLi);
    ul.appendChild(mountainsLi);

    a.appendChild(h4);
    divAttraction.appendChild(a);
    divAttraction.appendChild(ul);
    attractionsDiv.appendChild(divAttraction);
}

//$(window).scroll(function () {
//    if ($(window).scrollTop() + $(window).height() == $(document).height()) {
        
//        getFunction("https://localhost:7266/Attraction/GetAllAttractionsPagedFilteredByPartiallyNameMountainsAndHeight?partOfName=" + searchInput.value + "&mountains=" + collectAllCheckboxes() + "&height=" + rangeHeight.value + "&count=" + count);

//    }
//});

main[0].addEventListener("scroll", (e) => {
    if ((main[0].scrollTop + main[0].offsetHeight >= main[0].scrollHeight - 100) && (main[0].scrollTop + main[0].offsetHeight <= main[0].scrollHeight + 100) ) {
        getFunction("https://localhost:7266/Attraction/GetAllAttractionsPagedFilteredByPartiallyNameMountainsHeightAndLocation?partOfName=" + searchInput.value + "&mountains=" + collectAllCheckboxes() + "&height=" + rangeHeight.value + "&location=" + searchLocationInput.value + "&count=" + count);
    }
});


searchInput.onkeyup = (text) => {
    let value = text.target.value;
    count = 0;
    attractionsDiv.innerHTML = "";
    getFunction("https://localhost:7266/Attraction/GetAllAttractionsPagedFilteredByPartiallyNameMountainsHeightAndLocation?partOfName=" + value + "&mountains=" + collectAllCheckboxes() + "&height=" + rangeHeight.value + "&location=" + searchLocationInput.value + "&count=" + count);

}

// location

searchLocationInput.onkeyup = (text) => {
    let value = text.target.value;
    count = 0;
    attractionsDiv.innerHTML = "";
    getFunction("https://localhost:7266/Attraction/GetAllAttractionsPagedFilteredByPartiallyNameMountainsHeightAndLocation?partOfName=" + searchInput.value + "&mountains=" + collectAllCheckboxes() + "&height=" + rangeHeight.value + "&location=" + value + "&count=" + count);
}


// range height
let rangeHeight = document.getElementById("range-height");
let rangeValue = document.getElementById("range-value");


$.ajax({
    type: "get",
    url: "https://localhost:7266/Attraction/GetMaxHeightOfAttraction",
    success: (resp) => {
        rangeHeight.max = resp;
    },
    error: (err) => {
    }
});

$.ajax({
    type: "get",
    url: "https://localhost:7266/Attraction/GetMinHeightOfAttraction",
    success: (resp) => {
        rangeHeight.min = resp;
        rangeValue.innerHTML = ">= " + resp + " m ";
    },
    error: (err) => {
    }
});


rangeHeight.addEventListener("change", () => {
    rangeValue.innerHTML = ">= " + rangeHeight.value;
    count = 0;
    attractionsDiv.innerHTML = "";
    getFunction("https://localhost:7266/Attraction/GetAllAttractionsPagedFilteredByPartiallyNameMountainsHeightAndLocation?partOfName=" + searchInput.value + "&mountains=" + collectAllCheckboxes() + "&height=" + rangeHeight.value + "&location=" + searchLocationInput.value + "&count=" + count);
});




