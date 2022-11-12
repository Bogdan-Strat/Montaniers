let searchBar = document.getElementById("search-community");
searchBar.onkeyup = (text) => {
    let value = text.target.value;

    $.ajax({
        type: "get",
        url: "https://localhost:7266/UserAccount/GetUserByPartiallyUsername?partOfName=" + value,
        success: (resp) => {
            let listOfSuggestions = document.getElementById("suggestions-list");
            listOfSuggestions.innerHTML = "";

            for (let i = 0; i < resp.length; i++) {
                let suggestion = document.createElement("option");
                suggestion.style.cursor = "pointer";
                suggestion.innerHTML = resp[i].name;
                suggestion.value = resp[i].id;
                suggestion.addEventListener("click", (e) => {
                    let search = document.getElementById("search-community");
                    search.value = e.target.innerHTML;
                    search.dataset.id = e.target.value;
                    //searchBar = search;
                });
                listOfSuggestions.appendChild(suggestion);
            }
        },
        error: (err) => {
        }
    })
}


let searchButton = document.getElementById("search-button");
searchButton.addEventListener("click", () => {
    if (searchBar.value != "") {
        let userId = searchBar.dataset.id;
        location.href = "https://localhost:7266/UserAccount/GetProfilePage?userId=" + userId;
    }
    
});


// popover notifications
var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
    return new bootstrap.Popover(popoverTriggerEl)
})