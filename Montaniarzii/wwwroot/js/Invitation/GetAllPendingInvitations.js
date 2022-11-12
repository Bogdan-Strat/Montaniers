let acceptButtons = document.getElementsByClassName("accept");
let declineButtons = document.getElementsByClassName("decline");

for (let i = 0; i < acceptButtons.length; i++) {
    acceptButtons[i].addEventListener("click", (e) => {
        $.ajax({
            type: "post",
            url: "https://localhost:7266/Invitation/AcceptInvitation?tripId=" + e.target.dataset.tripid,
            success: (resp) => {
                location.href = "https://localhost:7266/Invitation/GetAllPendingInvitations"
            },
            error: (err) => {
            }
        })
    });
}
for (let i = 0; i < declineButtons.length; i++) {
    declineButtons[i].addEventListener("click", (e) => {
        $.ajax({
            type: "post",
            url: "https://localhost:7266/Invitation/DeclineInvitation?tripId=" + e.target.dataset.tripid,
            success: (resp) => {
                location.href = "https://localhost:7266/Invitation/GetAllPendingInvitations"
            },
            error: (err) => {
            }

        })
    });
}

