/*const { error } = require("jquery");*/

window.onload = () => {

    //let userid = window.CurrentUserId;

    //delete window.CurrentUserId;

    if (CurrentUserId != ProfileUserId) {
        let followButton = document.getElementById("follow-button");
        let followZone = document.getElementById("follow-zone");
        let buttonsDropdown = document.getElementById("buttons-dropdown");
        let statusRequest;
        let getStatusRequest = () => {
            $.ajax({
                type: "get",
                url: "https://localhost:7266/Follow/GetStatusRequest?followedUserId=" + ProfileUserId,
                success: (resp) => {
                    if (resp === false) {
                        followButton.innerHTML = "Pending";
                        //followZone.classList.add("dropdown");
                        //let retrieveButton = document.createElement("li");
                        //retrieveButton.innerHTML = "Retrieve request";
                        //retrieveButton.classList.add("dropdown-item");
                        //buttonsDropdown.classList.add("dropdown-menu");
                        //buttonsDropdown.appendChild(retrieveButton);
                        //followButton.classList.add("dropdown-toggle");
                        //followButton.ariaExpanded = "false";

                        followButton.addEventListener("mouseover", (e) => {
                            document.getElementById("follow-button").innerHTML = "Retrieve request";
                        });
                        followButton.addEventListener("mouseout", (e) => {
                            document.getElementById("follow-button").innerHTML = "Pending";
                        });

                        followButton.addEventListener("click", () => {
                            $.ajax({
                                type: "post",
                                url: "https://localhost:7266/Follow/Unfollow?followedUserId=" + ProfileUserId,
                                success: (resp) => {
                                    location.href = "https://localhost:7266/UserAccount/GetProfilePage?userId=" + ProfileUserId;
                                },
                                error: (err) => {
                                }
                            });
                        });

                    }
                    else if (resp === true) {
                        followButton.innerHTML = "Following";
                        //followZone.classList.add("dropdown");
                        //let unfollowButton = document.createElement("li");
                        //unfollowButton.innerHTML = "Unfollow";
                        //unfollowButton.classList.add("dropdown-item");
                        //buttonsDropdown.classList.add("dropdown-menu");
                        //buttonsDropdown.appendChild(unfollowButton);
                        //followButton.classList.add("dropdown-toggle");
                        //followButton.ariaExpanded = "false";

                        followButton.addEventListener("mouseover", (e) => {
                            document.getElementById("follow-button").innerHTML = "Unfollow";
                        });
                        followButton.addEventListener("mouseout", (e) => {
                            document.getElementById("follow-button").innerHTML = "Following";
                        });

                        followButton.addEventListener("click", () => {
                            $.ajax({
                                type: "post",
                                url: "https://localhost:7266/Follow/Unfollow?followedUserId=" + ProfileUserId,
                                success: (resp) => {
                                    location.href = "https://localhost:7266/UserAccount/GetProfilePage?userId=" + ProfileUserId;
                                },
                                error: (err) => {
                                }
                            });
                        });


                    }
                    else if (resp === undefined) {
                        followButton.innerHTML = "Follow";
                    }

                },
                error: (err) => {
                }
            });
        }

        getStatusRequest();

        

        followButton.addEventListener("click", () => {
            if (followButton.innerHTML === "Pending") {
                followButton.innerHTML = "Pending";
            }
            else if (followButton.innerHTML === "Following") {
                followZone.classList.add("dropdown");
                unfollowButton = document.createElement("li");
                unfollowButton.innerHTML = "Unfollow";
                unfollowButton.classList.add("dropdown-item");
                buttonsDropdown.classList.add("dropdown-menu");
                followButton.classList.add("dropdown-toogle");
                followButton.ariaExpanded = "false";
                

            }
            else if (statusRequest === undefined) {
                let obj = {
                    FollowingUserId: CurrentUserId,
                    FollowedUserId: ProfileUserId
                }
                $.ajax({
                    type: "post",
                    url: "https://localhost:7266/Follow/RequestFollow",
                    success: (resp) => {
                        location.href = "https://localhost:7266/UserAccount/GetProfilePage?userId=" + ProfileUserId;
                    },
                    error: (err) => {
                    },
                    contentType: "application/json",
                    data: JSON.stringify(obj)

                });
            }
        });
    }

    //delete trip
    let deleteTripButtons = document.getElementsByClassName("deleteButton");
    for (let i = 0; i < deleteTripButtons.length; i++) {
        deleteTripButtons[i].addEventListener("click", (e) => {
            $.ajax({
                type: "post",
                url: "https://localhost:7266/Trip/DeleteTrip?tripId=" + e.target.dataset.tripid,
                success: (resp) => {
                    location.href = "https://localhost:7266/UserAccount/GetProfilePage?userId=" + ProfileUserId;
                },
                error: (err) => {
                }
            });
        });
    }
    if (CurrentUserId == ProfileUserId) {
        // upcoming events
        let upcomingEventsDiv = document.getElementById("upcoming-events");

        let displayTrip = (trip, targetDiv) => {
            let div = document.createElement("div");
            div.classList.add("list-item");
            let ul = document.createElement("ul");
            let i = document.createElement("i");
            let auser = document.createElement("a");
            let aTrip = document.createElement("a");
            let b = document.createElement("b");
            let liDuration = document.createElement("li");

            b.innerHTML = " has created this event ";
            b.style = "font-weight: normal;";
            i.classList.add("fa-regular");
            i.classList.add("fa-user");
            div.appendChild(i);
            auser.href = "/UserAccount/GetProfilePage?userid=" + trip.userId;
            auser.innerHTML = trip.userNameCreator;
            aTrip.href = "/TripXAttractions/GetEventInformation?tripid=" + trip.tripId;
            aTrip.innerHTML = trip.attractionsName[0] + "-" + trip.attractionsName[1];

            liDuration.innerHTML = "Duration: " + trip.duration + " hours";

            ul.appendChild(liDuration);

            div.appendChild(i);
            div.appendChild(auser);
            div.appendChild(b);
            div.appendChild(aTrip);
            div.appendChild(ul);

            targetDiv.appendChild(div);

        }

        $.ajax({
            type: "get",
            url: "https://localhost:7266/Trip/GetUpcomingEvents",
            success: (resp) => {
                for (let i = 0; i < resp.length; i++) {
                    displayTrip(resp[i], upcomingEventsDiv);
                }
            },
            error: (err) => {
            }
        });

        // past events
        let pastEventsDiv = document.getElementById("past-events");

        $.ajax({
            type: "get",
            url: "https://localhost:7266/Trip/GetPastEvents",
            success: (resp) => {
                for (let i = 0; i < resp.length; i++) {
                    displayTrip(resp[i], pastEventsDiv);
                }
            }
        });

        // modal following people
        let buttonFollowing = document.getElementById("following-people-button");
        let modalFollowing = document.getElementById("following-people");

        let displayPeopleFollowing = (people) => {
            let div = document.createElement("div");
            let a = document.createElement("a");

            a.href = "/UserAccount/GetProfilePage?userId=" + people.id;
            a.innerHTML = people.name;

            div.appendChild(a);
            modalFollowing.appendChild(div);


        }
        buttonFollowing.addEventListener("click", () => {
            modalFollowing.innerHTML = "";
            $.ajax({
                type: "get",
                url: "https://localhost:7266/Follow/GetPeopleThatYouFollowForJS",
                success: (resp) => {
                    for (let i = 0; i < resp.length; i++) {
                        displayPeopleFollowing(resp[i]);
                    }
                },
                error: (err) => {
                }
            });
        });


        // modal followed people
        let buttonFollowed = document.getElementById("followed-people-button");
        let modalFollowed = document.getElementById("followed-people");

        let displayPeopleFollowed = (people) => {
            let div = document.createElement("div");
            let a = document.createElement("a");

            a.href = "/UserAccount/GetProfilePage?userId=" + people.id;
            a.innerHTML = people.name;

            div.appendChild(a);
            modalFollowed.appendChild(div);


        }
        buttonFollowed.addEventListener("click", () => {
            modalFollowed.innerHTML = "";
            $.ajax({
                type: "get",
                url: "https://localhost:7266/Follow/GetPeopleThatFollowYouForJS",
                success: (resp) => {
                    for (let i = 0; i < resp.length; i++) {
                        displayPeopleFollowed(resp[i]);
                    }
                },
                error: (err) => {
                }
            });
        });
    }
    
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
    
    
};












