import { error } from "jquery";

//modal
const exampleModal = document.getElementById('exampleModal');
exampleModal.addEventListener('show.bs.modal', event => {
    // Button that triggered the modal
    const button = event.relatedTarget
    // Extract info from data-bs-* attributes
    const recipient = button.getAttribute('data-bs-whatever')
    // If necessary, you could initiate an AJAX request here
    // and then do the updating in a callback.
    //
    // Update the modal's content.
    const modalTitle = exampleModal.querySelector('.modal-title')
    const modalBodyInput = exampleModal.querySelector('.modal-body input')

    modalTitle.textContent = `Suggest an attraction`
});

let approveModalButton = document.getElementById("modal-approve-button");
let closeModalButton = document.getElementById("modal-close-button");
let attractionName = document.getElementById("modal-attraction-name");
let description = document.getElementById("modal-description");
let attractiontype = document.getElementById("modal-attractionTypes");
let latitude = document.getElementById("modal-latitude");
let longitude = document.getElementById("modal-longitude");
let location1 = document.getElementById("modal-location");
let height = document.getElementById("modal-height");
let mountain = document.getElementById("modal-mountains");
let suggetionAttractionId;
approveModalButton.addEventListener("click", () => {
    
    const obj = {
        suggestionAttractionId: suggetionAttractionId,
        attractionName: attractionName.value,
        typeAttractionId: Number(attractiontype.value),
        description: description.value,
        latitude: Number(latitude.value),
        longitude: Number(longitude.value),
        location: location1.value,
        height: height.value,
        mountains: mountain.value,
    }

    let modalErrorSpans = document.getElementsByClassName("modal-error");
    for (let i = 0; i < modalErrorSpans.length; i++) {
        modalErrorSpans[i].innerHTML = "";
    }

    $.ajax({
        type: "post",
        url: "https://localhost:7266/SuggestionAttraction/ApproveSuggestion",
        success: (resp) => {
            closeModalButton.click();
            attractionName.value = "";
            attractiontype.value = 1;
            description.value = "";
            latitude.value = "";
            longitude.value = "";
            location1.value = "";
            height.value = "";
            mountain.value = "";

            location.href = "https://localhost:7266/SuggestionAttraction/GetAllSuggestionAttractions";
        },
        error: (err) => {
            $(document).ready(function () {
                $('#exampleModal').modal('show');
            });
            for (let i = 0; i < err.responseJSON.length; i++) {
                let span = document.getElementById(err.responseJSON[i].propertyName).children[2];
                span.innerHTML = err.responseJSON[i].errorMessage;
            }
        },
        contentType: "application/json",
        data: JSON.stringify(obj)
    });
});


let seeDetailsButtons = document.getElementsByClassName("see-details");
for (let i = 0; i < seeDetailsButtons.length; i++) {
    seeDetailsButtons[i].addEventListener("click", (e) => {
        $.ajax({
            type: "get",
            url: "https://localhost:7266/SuggestionAttraction/GetSuggestionAttractionByIdAsAdmin?suggestionAttractionId=" + e.target.dataset.suggestionattractionid,
            success: (resp) => {
                attractionName.value = resp.attractionName;
                attractiontype.value = resp.typeAttractionId;
                description.value = resp.description;
                latitude.value = resp.latitude;
                longitude.value = resp.longitude;
                location1.value = resp.location;
                height.value = resp.height;
                mountain.value = resp.mountains;
                suggetionAttractionId = e.target.dataset.suggestionattractionid;

            },
            error: (err) => {
            }
        });
    })
}


