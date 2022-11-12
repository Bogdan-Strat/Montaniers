let trending = document.getElementById("trending");
let trips = document.getElementById("trips");

let displayAttraction = function (attraction) {
    let div = document.createElement("div");
    let h5 = document.createElement("h6");
    let a = document.createElement("a");
    a.style.marginBottom = "8px";
    a.classList.add("badge");
    a.classList.add("rounded-pill");
    a.classList.add("bg-info");
    a.classList.add("text-dark");

    h5.innerHTML = attraction.name;
    a.href = "/Attraction/GetAttractionInformation?attractionId=" + attraction.id;
    a.appendChild(h5);

    div.appendChild(a);
    trending.appendChild(div);

}

let displayTrip = (trip) => {
    let div = document.createElement("div");
    div.classList.add("list-item");
    let ul = document.createElement("ul");
    let i = document.createElement("i");
    let auser = document.createElement("a");
    let aTrip = document.createElement("a");
    let b = document.createElement("b");
    let liDuration = document.createElement("li");
    liDuration.style.display = "flex";
    let liDate = document.createElement("li");
    liDate.style.display = "flex";

    b.innerHTML = " has created this trip ";
    b.style = "font-weight: normal;";
    i.classList.add("fa-regular");
    i.classList.add("fa-user");
    div.appendChild(i);
    auser.href = "/UserAccount/GetProfilePage?userid=" + trip.userId;
    auser.innerHTML = trip.userNameCreator;
    aTrip.href = "/Trip/GetTripInformation?tripid=" + trip.tripId;
    aTrip.innerHTML = trip.attractionsName[0] + "-" + trip.attractionsName[1];
    /*<li><i class="fa-regular fa-calendar-days"></i> @trip.Date.ToShortDateString()</li>*/
    let iDuration = document.createElement("i");
    iDuration.classList.add("fa-regular");
    iDuration.classList.add("fa-clock");
    liDuration.appendChild(iDuration);

    let div1 = document.createElement("div");
    div1.innerHTML = trip.duration + " hours";
    liDuration.appendChild(div1);

    let iDate = document.createElement("i");
    iDate.classList.add("fa-regular");
    iDate.classList.add("fa-calendar-days");
    let div2 = document.createElement("div");
    div2.innerHTML = trip.date.slice(0, 10);
    liDate.appendChild(i);
    liDate.appendChild(div2);

    ul.appendChild(liDuration);
    ul.appendChild(liDate);

    div.appendChild(i);
    div.appendChild(auser);
    div.appendChild(b);
    div.appendChild(aTrip);
    div.appendChild(ul);

    trips.appendChild(div);
}

$.ajax({
    type: "get",
    url: "https://localhost:7266/Attraction/GetTrendingAttractions",
    success: (resp) => {
        for (let i = 0; i < resp.length; i++) {
            displayAttraction(resp[i]);
        }
    },
    error: (err) => {
    }
});

$(window).scroll(function () {
    if ($(window).scrollTop() + $(window).height() == $(document).height()) {
        $.ajax({
            type: "get",
            url: "https://localhost:7266/Trip/GetAllTripsPagedForAListOfUsers?count=" + count,
            success: (resp) => {
                count++;
                for (let i = 0; i < resp.length; i++) {
                    displayTrip(resp[i]);
                }
            },
            error: (err) => {
            }
        })

    }
});

// warning zone
let warningsZone = document.getElementById("warnings-zone");

let displayWarning = (warning) => {
    let div = document.createElement("div");
    div.classList.add("text-light");
    div.classList.add("alert");
    div.classList.add("bg-danger");

    div.innerHTML = "Alert emitted!\n" + warning.warningMessage + '\n' +
        "starts from " + warning.createDate.slice(0, 10) + " until " + warning.endDate.slice(0, 10) +
        ". \nTake care!";

    warningsZone.appendChild(div);
}

$.ajax({
    type: "get",
    url: "https://localhost:7266/Warning/GetActiveWarningsAsUser",
    success: (resp) => {
        for (let i = 0; i < resp.length; i++) {
            displayWarning(resp[i]);
        }

    },
    error: (err) => {
    }
})

// like button
let likeButtons = document.getElementsByClassName("like-button");
let numberOfLikesDivs = document.getElementsByClassName("number-of-likes");

let arrLikeButtons = Array.from(likeButtons);
let arrNumberOfLikesDivs = Array.from(numberOfLikesDivs);

let getNumberOfLikes = (tripId, index) => {
    $.ajax({
        type: "get",
        url: "https://localhost:7266/Like/GetNumberOfLikesForATrip?tripId=" + tripId,
        success: (resp) => {
            console.log(resp);

            let div = arrNumberOfLikesDivs[index];
            div.innerHTML = resp;
        },
        error: (err) => {
            console.log(err);
        }
    });
}
function delay(time) {
    return new Promise(resolve => setTimeout(resolve, time));
}



let isLiked = (btn) => {
    $.ajax({
        type: "get",
        url: "https://localhost:7266/Like/IsTripLikedByCurrentUser?tripId=" + btn.dataset.tripid,
        success: (resp) => {
            if (resp == true) {
                btn.className = "fa-solid fa-heart like-button";
            }
            else {
                btn.className = "fa-regular fa-heart like-button";
            }
        },
        error: (err) => {

        }
    });
}

let like = (btn) => {
    $.ajax({
        type: "post",
        url: "https://localhost:7266/Like/LikeTrip?tripId=" + btn.dataset.tripid,
        success: (resp) => {
            console.log(resp);

            btn.onclick = () => {
                dislike(btn);
                delay(50).then(() => isLiked(btn));
            };

            getNumberOfLikes(btn.dataset.tripid, arrLikeButtons.indexOf(btn));
        },
        error: (err) => {
            console.log(err);
        }
    });
}

let dislike = (btn) => {
    $.ajax({
        type: "post",
        url: "https://localhost:7266/Like/DislikeTrip?tripId=" + btn.dataset.tripid,
        success: (resp) => {
            console.log(resp);

            btn.onclick = () => {
                like(btn);
                delay(50).then(() => isLiked(btn));
            };

            getNumberOfLikes(btn.dataset.tripid, arrLikeButtons.indexOf(btn));

        },
        error: (err) => {
            console.log(err);
        }
    });
}

for (let i = 0; i < likeButtons.length; i++) {
    $.ajax({
        type: "get",
        url: "https://localhost:7266/Like/IsTripLikedByCurrentUser?tripId=" + likeButtons[i].dataset.tripid,
        success: (resp) => {
            console.log(resp);
            if (resp == true) {

                likeButtons[i].className = "fa-solid fa-heart like-button";

                likeButtons[i].onclick = () => {
                    dislike(likeButtons[i]);
                    delay(50).then(() => isLiked(likeButtons[i]));
                };

                getNumberOfLikes(likeButtons[i].dataset.tripid, i);
            }
            else {
                likeButtons[i].onclick = () => {
                    like(likeButtons[i]);
                    delay(50).then(() => isLiked(likeButtons[i]));
                };

                getNumberOfLikes(likeButtons[i].dataset.tripid, i);
            }
        },
        error: (err) => {
            console.log(err);
        }
    });


}