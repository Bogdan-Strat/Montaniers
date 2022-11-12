window.onload = () => {
    followRequestsButton.click();
}

// follow requests
let followRequestsZone = document.getElementById("follow-requests-zone");
let followRequestsButton = document.getElementById("follow-requests-button");

let displayFollowRequest = (follow) => {
    let div = document.createElement("div");
    div.classList.add("list-item");
    let i = document.createElement("i");
    let b = document.createElement("b");
    let a = document.createElement("a");
    let divButtons = document.createElement("div");
    let acceptButton = document.createElement("button");
    let declineButton = document.createElement("button");

    acceptButton.type = "button";
    acceptButton.innerHTML = "Accept";
    acceptButton.dataset.followinguserid = follow.followingUserId;
    acceptButton.addEventListener("click", (e) => {
        $.ajax({
            type: "post",
            url: "https://localhost:7266/Follow/ApproveFollowRequest?followingUserId=" + e.target.dataset.followinguserid,
            success: (resp) => {
                followRequestsButton.click();
            },
            error: (err) => {
            }
        });
    });

    declineButton.type = "button";
    declineButton.innerHTML = "Decline";
    declineButton.dataset.followinguserid = follow.followingUserId;
    declineButton.addEventListener("click", (e) => {
        $.ajax({
            type: "post",
            url: "https://localhost:7266/Follow/DenyFollowRequest?followingUserId=" + e.target.dataset.followinguserid,
            success: (resp) => {
                followRequestsButton.click();
            },
            error: (err) => {
            }
        })
    });

    acceptButton.classList.add("btn");
    acceptButton.classList.add("btn-success");
    acceptButton.classList.add("rounded-pill");

    declineButton.classList.add("btn");
    declineButton.classList.add("btn-danger");
    declineButton.classList.add("rounded-pill");

    divButtons.append(acceptButton);
    divButtons.append(declineButton);

    b.innerHTML = " has requested to follow you";
    b.style = "font-weight: normal;";
    i.classList.add("fa-regular");
    i.classList.add("fa-user");

    a.innerHTML = follow.followingUsername;
    a.href = "/UserAccount/GetProfilePage?userid=" + follow.followingUserId;

    div.appendChild(a);
    div.appendChild(b);
    div.appendChild(divButtons);

    followRequestsZone.appendChild(div);




}

followRequestsButton.addEventListener("click", () => {
    followRequestsZone.innerHTML = "";
    $.ajax({
        type: "get",
        url: "https://localhost:7266/Follow/GetAllPendingFollowRequestsForJS",
        success: (resp) => {

            if (resp.length == 0) {
                followRequestsZone.innerHTML = "You have 0 follow requests";
            }

            for (let i = 0; i < resp.length; i++) {
                displayFollowRequest(resp[i]);
            }
        },
        error: (err) => {
            cosnole.log(err);
        }
    })
});

// pending requests
let pendingInvitationsZone = document.getElementById("pending-invitations-zone");
let pendingInvitationsButton = document.getElementById("pending-invitations-button");

let displayPendingInvitation = (invitation) => {
    let div = document.createElement("div");
    div.classList.add("list-item");
    let i = document.createElement("i");
    let aUser = document.createElement("a");
    let aEvent = document.createElement("a");
    let b = document.createElement("b");
    let divButtons = document.createElement("div");
    let acceptButton = document.createElement("button");
    let declineButton = document.createElement("button");

    acceptButton.type = "button";
    acceptButton.innerHTML = "Accept";
    acceptButton.dataset.tripid = invitation.tripId;
    acceptButton.addEventListener("click", (e) => {
        $.ajax({
            type: "post",
            url: "https://localhost:7266/Invitation/AcceptInvitation?tripId=" + e.target.dataset.tripid,
            success: (resp) => {
                pendingInvitationsButton.click();
            },
            error: (err) => {
            }
        });
    });


    declineButton.type = "button";
    declineButton.innerHTML = "Decline";
    declineButton.dataset.tripid = invitation.tripId;
    declineButton.addEventListener("click", (e) => {
        $.ajax({
            type: "post",
            url: "https://localhost:7266/Invitation/DeclineInvitation?tripId=" + e.target.dataset.tripid,
            success: (resp) => {
                pendingInvitationsButton.click();
            },
            error: (err) => {
            }

        });
    });

    acceptButton.classList.add("btn");
    acceptButton.classList.add("btn-success");
    acceptButton.classList.add("rounded-pill");

    declineButton.classList.add("btn");
    declineButton.classList.add("btn-danger");
    declineButton.classList.add("rounded-pill");

    divButtons.append(acceptButton);
    divButtons.append(declineButton);
    

    b.innerHTML = " has invitede you to this ";
    b.style = "font-weight: normal;";
    i.classList.add("fa-regular");
    i.classList.add("fa-user");

    aUser.innerHTML = invitation.usernameEventCreator;
    aUser.href = "/UserAccount/GetProfilePage?userid=" + invitation.userId;

    aEvent.innerHTML = "Event";
    aEvent.href = "/TripXAttractions/GetEventInformation?tripid=" + invitation.tripId;

    div.appendChild(aUser);
    div.appendChild(b);
    div.appendChild(aEvent);
    div.appendChild(divButtons);

    pendingInvitationsZone.appendChild(div);
}

pendingInvitationsButton.addEventListener("click", () => {
    pendingInvitationsZone.innerHTML = "";
    $.ajax({
        type: "get",
        url: "https://localhost:7266/Invitation/GetAllPendingInvitationsForJS",
        success: (resp) => {

            if (resp.length == 0) {
                pendingInvitationsZone.innerHTML = "You have 0 invitations in pending";
            }

            for (let i = 0; i < resp.length; i++) {
                displayPendingInvitation(resp[i]);
            }
        },
        error: (err) => {
        }
    });
});

// pending suggestions
let pendingSuggestionsZone = document.getElementById("pending-suggestions-zone");
let pendingSuggestionsButton = document.getElementById("pending-suggestions-button");

let displaySuggestion = (suggestion) => {
    let div = document.createElement("div");
    div.classList.add("list-item");
    let divMessage = document.createElement("div");
    let divStatus = document.createElement("div");
    let divDate = document.createElement("div");
    let a = document.createElement("a");
    let b = document.createElement("b");

    divDate.innerHTML = "Created date: " + suggestion.cretedDate.slice(0,10);
    divStatus.innerHTML = "Status: ";
    divMessage.innerHTML = "You suggest ";

    if (suggestion.isAccepted == null) {
        let divBadge = document.createElement("div");
        divBadge.classList.add("badge");
        divBadge.classList.add("rounded-pill");
        divBadge.classList.add("bg-info");
        divBadge.classList.add("text-dark");
        divBadge.innerHTML = "Pending";

        divStatus.appendChild(divBadge);
        a.innerHTML = suggestion.typeAttraction + " " + suggestion.attractionName;
        divMessage.appendChild(a);
        b.innerHTML = " to our administration.";
        b.style = "font-weight: normal;";
        divMessage.appendChild(b);
    }
    else if (suggestion.isAccepted === true) {
        let divBadge = document.createElement("div");
        divBadge.classList.add("badge");
        divBadge.classList.add("rounded-pill");
        divBadge.classList.add("bg-success");
        divBadge.innerHTML = "Accepted";

        divStatus.appendChild(divBadge);
        divMessage.innerHTML += suggestion.typeAttraction + " " + suggestion.attractionName + " to our administration.";
    }
    else {
        let divBadge = document.createElement("div");
        divBadge.classList.add("badge");
        divBadge.classList.add("rounded-pill");
        divBadge.classList.add("bg-danger");
        divBadge.innerHTML = "Rejected";

        divStatus.appendChild(divBadge);
        divMessage.innerHTML += suggestion.typeAttraction + " " + suggestion.attractionName + " to our administration.";
    }

    div.appendChild(divMessage);
    div.appendChild(divStatus);
    div.appendChild(divDate);

    pendingSuggestionsZone.appendChild(div);
}

pendingSuggestionsButton.addEventListener("click", () => {
    pendingSuggestionsZone.innerHTML = "";
    $.ajax({
        type: "get",
        url: "https://localhost:7266/SuggestionAttraction/GetAllYourSuggestionAttractionForJS",
        success: (resp) => {
            if (resp.length == 0) {
                pendingSuggestionsZone.innerHTML = "You have suggested 0 attractions to our administration."
            }
            for (let i = 0; i < resp.length; i++) {
                displaySuggestion(resp[i]);
            }
        },
        error: (err) => {
        }
    })
});
