let acceptButtons = document.getElementsByClassName("accept");
let declineButtons = document.getElementsByClassName("decline");

for (let i = 0; i < acceptButtons.length; i++) {
    acceptButtons[i].addEventListener("click", (e) => {
        $.ajax({
            type: "post",
            url: "https://localhost:7266/Follow/ApproveFollowRequest?followingUserId=" + e.target.dataset.followinguserid,
            success: (resp) => {
                location.href = "https://localhost:7266/Follow/GetAllPendingFollowRequests"
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
            url: "https://localhost:7266/Follow/DenyFollowRequest?followingUserId=" + e.target.dataset.followinguserid,
            success: (resp) => {
                location.href = "https://localhost:7266/Follow/GetAllPendingFollowRequests"
            },
            error: (err) => {
                else if(err.status == 404) {
            location.href = "https://localhost:7266/CustomError/Error404";
        }
                else if (err.status == 400) {
            location.href = "https://localhost:7266/CustomError/Error_BadRequest";
        }
            }
        })
    });
}

