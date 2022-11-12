import markingTypes from "./MarkingTypes.js";
import attractionTypes from "./AttractionTypes.js";

window.onload = () => {


    var sectionsDiv = document.getElementById("sections");

    var addButtonDiv = document.getElementById("addbutton");
    var idAddSection = 0;
    var postButton = document.getElementById("post-button");
    var attractions = [];
    var attractionsDiv = [];
    var sectionsId = 0;
    let peopleInvited = [];
    let photoAdded = [];
    let idPhotoAdded = 0;
    let idPeopleInvited = 0;

    //photos
    let photoList = document.getElementById("photo-list");
    let photoButton = document.getElementById("photos");

    photoButton.addEventListener("change", async (e) => {

        //var fileUpload = $("#photos").get(0);
        //var files = fileUpload.files;
        //$.ajax({
        //    url: '/Photo/SaveAndGetIdOfPhoto',
        //    type: "POST",
        //    contentType: false, // Not to set any content header  
        //    processData: false, // Not to process data  
        //    data: fileData,
        //    success: function (result) {
        //        
        //        alert(result);
        //    },
        //    error: function (err) {
        //        alert(err.statusText);
        //    }
        //});

        for (let j = 0; j < e.target.files.length; j++) {
            let formData = new FormData();
            formData.append("file", e.target.files[j]);


            document.getElementById("span-photo").innerHTML = "";
            let response = await fetch("https://localhost:7266/Photo/SaveAndGetIdOfPhoto", {
                method: "post",
                body: formData
            });

            if (response.ok) {
                let data = await response.json();
                let div = document.createElement("div");
                let option = document.createElement("option");
                let i = document.createElement("i");

                option.innerHTML = e.target.files[j];

                i.classList.add("bi");
                i.classList.add("bi-file-earmark-excel-fill");

                i.addEventListener("click", (e) => {
                    let id = e.target.id;
                    let idDivToBeDeleted = "photo-" + e.target.id.slice(-1);
                    let divToBeDeletd = document.getElementById(idDivToBeDeleted);

                    var photoDivIndex = photoAdded.indexOf(divToBeDeletd);
                    if (photoDivIndex !== -1) {
                        photoAdded.splice(photoDivIndex, 1);
                    }
                    divToBeDeletd.remove();
                    photoButton.value = null;

                });

                div.dataset.id = data;
                div.dataset.name = e.target.files[j].name;
                div.innerHTML = e.target.files[j].name + " ";
                div.appendChild(i);

                idPhotoAdded++;
                i.id = "photo-" + idPhotoAdded;
                div.id = "photo-" + idPhotoAdded;
                photoAdded.push(div);
                photoList.appendChild(div);
            }
            else {
                if (response.status == 412) {
                    let err = await response.json();
                    let span = document.getElementById(err[0].propertyName).children[2];
                    span.innerHTML = err[0].errorMessage;
                }
                else if (response.status == 404) {
                    location.href = "https://localhost:7266//CustomError/Error404";
                }
                else if (response.status == 400) {
                    location.href = "https://localhost:7266//CustomError/Error_BadRequest";
                }

            }

        }


    })

    //

    let selectTypePost = document.getElementById("typepost");
    selectTypePost.addEventListener('change', () => {
        if (selectTypePost.value == "2") {
            let selectPrivacyType = document.getElementById("privacy");
            selectPrivacyType.value = "2";
            selectPrivacyType.disabled = true;

            let invitationsDiv = document.createElement("div");
            let invitationsLabel = document.createElement("label");
            let invitationsInput = document.createElement("input");
            invitationsInput.classList.add("form-control");
            invitationsInput.id = "invitationsInput";
            let divLabel = document.createElement("div");
            let divInput = document.createElement("div");
            divInput.style.display = "flex";
            let divSuggestions = document.createElement("div");
            divSuggestions.style.width = "100%";
            divSuggestions.style.overflow = "auto";
            divSuggestions.style.top = "100%";
            divSuggestions.style.left = "0";
            divSuggestions.style.background = "#fff";
            divSuggestions.style.boxShadow = "0 0.25rem 0.75rem rgb(0 0 0 / 15%)";
            divSuggestions.style.border = "1 px solid #dee2e6";

            let divPeople = document.createElement("div");
            let addButton = document.createElement("button");

            divInput.onkeyup = (text) => {
                let value = text.target.value;

                $.ajax({
                    type: "get",
                    url: "https://localhost:7266/UserAccount/GetUserByPartiallyUsername?partOfName=" + value,
                    success: (resp) => {

                        let listOfSuggestions = divSuggestions;
                        listOfSuggestions.innerHTML = "";

                        for (let i = 0; i < resp.length; i++) {
                            let suggestion = document.createElement("option");
                            suggestion.style.cursor = "pointer";
                            suggestion.innerHTML = resp[i].name;
                            suggestion.value = resp[i].id;
                            suggestion.addEventListener("click", (e) => {
                                let search = document.getElementById("invitationsInput");
                                search.value = e.target.innerHTML;
                                search.dataset.id = e.target.value;
                                divInput = search;
                            });
                            listOfSuggestions.appendChild(suggestion);
                            divSuggestions = listOfSuggestions;

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
            }



            addButton.innerHTML = "Add";
            addButton.classList.add("btn");
            addButton.classList.add("btn-primary");
            addButton.classList.add("rounded-pill");

            addButton.type = "button";
            addButton.addEventListener("click", () => {
                divSuggestions.innerHTML = "";
                if (invitationsInput.value != "") {
                    $.ajax({
                        type: "get",
                        url: "https://localhost:7266/UserAccount/IsUsernameValid?username=" + invitationsInput.value,
                        success: (resp) => {
                            if (resp == true) {
                                let div = document.createElement("div");
                                let removeIcon = document.createElement("i");

                                removeIcon.classList.add("fa-solid");
                                removeIcon.classList.add("fa-user-minus");
                                removeIcon.addEventListener("click", (e) => {
                                    let id = e.target.id;
                                    let divToBeDeletd = document.getElementById(id);

                                    var peopleDivIndex = peopleInvited.indexOf(divToBeDeletd);
                                    if (peopleDivIndex !== -1) {
                                        peopleInvited.splice(peopleDivIndex, 1);
                                    }
                                    divToBeDeletd.remove();

                                });

                                div.dataset.id = invitationsInput.dataset.id;
                                div.innerHTML = invitationsInput.value + " ";
                                div.appendChild(removeIcon);

                                idPeopleInvited++;
                                removeIcon.id = idPeopleInvited;
                                div.id = idPeopleInvited;
                                peopleInvited.push(div);
                                divPeople.appendChild(div);
                            }
                            else {
                                alert("You cannot invite an user that doesn't exist");
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


                    //

                }
            });


            invitationsLabel.innerHTML = "Invitations";
            invitationsInput.type = "text";

            divLabel.appendChild(invitationsLabel);
            divInput.appendChild(invitationsInput);
            divInput.appendChild(addButton);

            invitationsDiv.appendChild(divLabel);
            invitationsDiv.appendChild(divInput);
            invitationsDiv.appendChild(divSuggestions);
            invitationsDiv.appendChild(divPeople);

            let invitationsZone = document.getElementById("invitations");
            invitationsZone.appendChild(invitationsDiv);
        }
        else {
            let selectPrivacyType = document.getElementById("privacy");
            selectPrivacyType.value = "1";
            selectPrivacyType.disabled = false;

            let invitationsZone = document.getElementById("invitations");
            var child = invitationsZone.lastElementChild;
            while (child) {
                invitationsZone.removeChild(child);
                child = invitationsZone.lastElementChild;
            }
            peopleInvited = [];
        }
    });

    function addSectionButton() {
        let button = document.createElement("button");
        let div = document.createElement("div");
        button.type = "button";
        button.innerHTML = "Add Attraction";
        button.classList.add("btn");
        button.classList.add("btn-primary");
        button.classList.add("add-section");
        button.classList.add("rounded-pill");
        button.style.marginBottom = "10px";
        button.addEventListener("click", () => {
            addSection(sectionsId);
        });
        div.appendChild(button);
        addButtonDiv.appendChild(div);


    }

    function addDeleteSectionButton() {
        let button = document.createElement("button");
        let div = document.createElement("div");
        button.type = "button";
        button.innerHTML = "Remove Attraction";
        button.classList.add("btn");
        button.classList.add("btn-danger");
        button.classList.add("add-section");
        button.classList.add("rounded-pill");
        button.style.marginBottom = "10px";
        button.id = "delete-button-" + (attractionsDiv.length + 1).toString();
        button.addEventListener("click", (e) => {
            if (attractionsDiv.length <= 1) {
                alert("You need a Start Point and an End Point");
            }
            else {
                let id = e.target.id;
                let indexId = id.slice(-1);
                let idToBeDeleted = "Attractions[" + (parseInt(indexId)) + "]";
                let divToBeDeletd = document.getElementById(idToBeDeleted);

                var attDivIndex = attractionsDiv.indexOf(divToBeDeletd);
                if (attDivIndex !== -1) {
                    attractionsDiv.splice(attDivIndex, 1);
                }
                divToBeDeletd.remove();

                //put the correct index on divs
                for (let i = 0; i < attractionsDiv.length; i++) {
                    attractionsDiv[i].id = "Attractions[" + (i + 1) + "]";
                    // change id of remove button
                    Array.from(Array.from(attractionsDiv[i].children).slice(-1))[0].firstChild.id = "delete-button-" + (i + 1);

                    //change id of duration
                    Array.from(Array.from(attractionsDiv[i].children)[0].children)[1].id = "duration-" + (i + 1);

                    // change id of marking
                    Array.from(Array.from(attractionsDiv[i].children)[1].children)[1].id = "marking-" + (i + 1);

                    // change id of attraction
                    Array.from(Array.from(attractionsDiv[i].children)[2].children)[1].id = "attraction-" + (i + 1);
                    Array.from(Array.from(Array.from(attractionsDiv[i].children)[2].children)[1].children)[1].id = "attractionSelected-" + (i + 1);

                    // change id of divDuration
                    Array.from(attractionsDiv[i].children)[0].id = "Attractions[" + (i + 1) + "].Duration";

                    // change id of divMarking
                    Array.from(attractionsDiv[i].children)[1].id = "Attractions[" + (i + 1) + "].MarkingId";

                    // change id of divAttraction
                    Array.from(attractionsDiv[i].children)[2].id = "Attractions[" + (i + 1) + "].AttractionId";

                    // change id of divSuggestion
                    Array.from(attractionsDiv[i].children)[3].id = "suggestions-" + (i + 1);
                }
            }

            return;
        });
        div.appendChild(button);
        return addButtonDiv.appendChild(div);
    }
    function addSection() {
        let divDuration = document.createElement("div");
        divDuration.classList.add("form-group");

        let divMarking = document.createElement("div");
        divMarking.classList.add("form-group");

        let divAttraction = document.createElement("div");
        divAttraction.classList.add("form-group");
        var divSuggestions = document.createElement("div");
        let spanDuration = document.createElement("span");
        let spanMarking = document.createElement("span");
        let spanAttraction = document.createElement("span");
        let id = attractionsDiv.length.toString();

        let div = document.createElement("div");
        div.id = "Attractions[" + (parseInt(id) + 1).toString() + "]";
        divDuration.id = div.id + ".Duration";
        divMarking.id = div.id + ".MarkingId";
        divAttraction.id = div.id + ".AttractionId";

        let span = document.createElement("span");
        span.id = "error-attractions-" + (parseInt(id) + 1).toString();
        span.classList.add("text-danger");
        span.classList.add("create-error");

        spanDuration.classList.add("text-danger");
        spanDuration.classList.add("create-error");

        spanMarking.classList.add("text-danger");
        spanMarking.classList.add("create-error");

        spanAttraction.classList.add("text-danger");
        spanAttraction.classList.add("create-error");

        divSuggestions.id = "suggestions-" + (parseInt(id) + 1).toString();
        divSuggestions.style.width = "100%";
        divSuggestions.style.overflow = "auto";
        divSuggestions.style.top = "100%";
        divSuggestions.style.left = "0";
        divSuggestions.style.background = "#fff";
        divSuggestions.style.boxShadow = "0 0.25rem 0.75rem rgb(0 0 0 / 15%)";
        divSuggestions.style.border = "1 px solid #dee2e6";



        let durationInput = document.createElement("input");
        durationInput.classList.add("form-control");

        let durationLabel = document.createElement("label");
        durationLabel.classList.add("control-label");

        let markingSelect = document.createElement("select");
        markingSelect.classList.add("form-select");

        let markingLabel = document.createElement("label");
        markingLabel.classList.add("control-label");

        markingSelect.id = "marking-" + (parseInt(id) + 1).toString();
        let index = 1;
        for (let mark in markingTypes) {
            let option = document.createElement("option");
            option.value = index;
            option.innerText = mark;
            markingSelect.add(option);
            index++;
        }

        let divGroup = document.createElement('div');
        divGroup.classList.add("input-group");
        divGroup.classList.add("mb-3");

        let divTypeAttraction = document.createElement("div");
        divTypeAttraction.classList.add("input-group-prepend");

        let selectTypeAttraction = document.createElement("select");
        selectTypeAttraction.classList.add("form-select");

        for (let attraction in attractionTypes) {
            let option = document.createElement("option");
            option.innerHTML = attraction;
            selectTypeAttraction.add(option);
        }


        let attractionInput = document.createElement("input");
        attractionInput.classList.add("form-control");

        let attractionLabel = document.createElement("label");
        attractionLabel.classList.add("control-label");


        durationLabel.innerHTML = "Duration(h)";
        durationInput.type = "text";
        durationInput.value = "";
        durationInput.id = "duration-" + (parseInt(id) + 1).toString();


        divDuration.appendChild(durationLabel);
        divDuration.appendChild(durationInput);
        divDuration.appendChild(spanDuration);

        markingLabel.innerHTML = "Marking";
        //markingInput.type = "text";
        //markingInput.value = "";
        //markingInput.id = "marking-" + id;

        divMarking.appendChild(markingLabel);
        divMarking.appendChild(markingSelect);
        divMarking.appendChild(spanMarking);

        attractionLabel.innerHTML = "Attraction";
        attractionInput.type = "text";
        attractionInput.value = "";
        attractionInput.id = "attractionSelected-" + (parseInt(id) + 1).toString();
        attractionInput.dataset.id = "00000000-0000-0000-0000-000000000000";

        attractionInput.onkeyup = (text) => {
            let value = text.target.value;

            $.ajax({
                type: "get",
                url: "https://localhost:7266/Attraction/GetNameOfAttractionsByPartiallyName?partOfName=" + value,
                success: (resp) => {

                    let listOfSuggestions = document.getElementById(divSuggestions.id);
                    listOfSuggestions.innerHTML = "";

                    for (let i = 0; i < resp.length; i++) {
                        let suggestion = document.createElement("option");
                        suggestion.style.cursor = "pointer";
                        suggestion.innerHTML = resp[i].name;
                        suggestion.value = resp[i].id;
                        suggestion.addEventListener("click", (e) => {
                            let search = document.getElementById(attractionInput.id);
                            search.value = e.target.innerHTML;
                            search.dataset.id = e.target.value;
                        });
                        listOfSuggestions.appendChild(suggestion);

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
        }

        divTypeAttraction.appendChild(selectTypeAttraction);
        divGroup.appendChild(divTypeAttraction);
        divGroup.appendChild(attractionInput);

        divAttraction.appendChild(attractionLabel);
        divAttraction.appendChild(divGroup);
        divAttraction.appendChild(spanAttraction);

        div.appendChild(divDuration);
        div.appendChild(divMarking);
        div.appendChild(divAttraction);
        div.appendChild(divSuggestions);
        div.appendChild(span);
        div.appendChild(addDeleteSectionButton());

        if (attractionsDiv.length > 8) {
            alert("You can have maximum 9 attractions per trip");
        }
        else {
            attractionsDiv.push(div);
            sectionsDiv.appendChild(div);
        }


        sectionsId += 1;


    }

    //var f = function DeleteSection(id) {
    //    sectionsDiv.removeChild()
    //}
    addSection();
    addSectionButton();

    let description, date, rating, difficulty, equipment, privacy, typePost, startPoint;
    let users = [];
    let photos = [];
    //let photos = new FormData();
    const collectData = () => {
        description = document.getElementById("description").value;
        date = document.getElementById("date").value;
        rating = Number(document.getElementById("rating").value);
        difficulty = Number(document.getElementById("difficulty").value);
        equipment = document.getElementById("equipment").value;
        privacy = Number(document.getElementById("privacy").value);
        typePost = Number(document.getElementById("typepost").value);
        startPoint = document.getElementById("Attractions[0]").dataset.id;

        //for (let i = 0; i < document.getElementById("photos").files.length; i++) {
        //    photos.append("file", document.getElementById("photos").files[i]);
        //}

        let firstAttraction = {
            AttractionId: startPoint,
            MarkingId: 16,
            Duration: 1,
        }
        attractions.splice(0, 0, firstAttraction);

        for (let i = 0; i < attractionsDiv.length; i++) {
            let id = attractionsDiv[i].id.slice(-2)[0];
            let duration = Number(document.getElementById("duration-" + id).value);
            let marking = Number(document.getElementById("marking-" + id).value);
            let attraction = document.getElementById("attractionSelected-" + id).dataset.id;

            let att = {
                AttractionId: attraction,
                MarkingId: marking,
                Duration: duration

            }
            attractions.push(att);
        }

        for (let i = 0; i < peopleInvited.length; i++) {
            let guidId = peopleInvited[i].dataset.id;
            users.push(guidId);
        }

        for (let i = 0; i < photoAdded.length; i++) {
            let photoId = photoAdded[i].dataset.id;
            let photoName = photoAdded[i].dataset.name;
            let obj = {
                Id: photoId,
                Name: photoName
            }
            photos.push(obj);

        }
    }

    postButton.addEventListener("click", () => {
        attractions = [];
        photos = [];
        users = [];
        collectData();
        const obj = {
            Description: description,
            TripDate: new Date(date),
            RatingId: rating,
            DifficultyId: difficulty,
            Equipment: equipment,
            PrivacyId: privacy,
            TypePostId: typePost,
            //StartPointId: startPoint,
            Attractions: attractions,
            UsersId: users,
            Photos: photos,
        };

        let createErrorSpans = document.getElementsByClassName("create-error");
        for (let i = 0; i < createErrorSpans.length; i++) {
            createErrorSpans[i].innerHTML = "";
        }
        $.ajax({
            type: "post",
            url: "https://localhost:7266/Trip/CreateTrip",
            success: (resp) => {
                location.href = "https://localhost:7266/UserAccount/GetProfilePage?userId=" + currentUserId
            },
            error: (err) => {
                if (err.status == 400) {
                    location.href = "https://localhost:7266/CustomError/Error_BadRequest";
                }
                else if (err.status == 404) {
                    location.href = "https://localhost:7266/CustomError/Error404";
                }
                else {
                    for (let i = 0; i < err.responseJSON.length; i++) {
                        if (err.responseJSON[i].propertyName === "Attractions[0].Duration" || err.responseJSON[i].propertyName === "Attractions[0].MarkingId") {
                            continue;
                        }
                        if (err.responseJSON[i].propertyName != "Attractions[0].AttractionId") {
                            if (err.responseJSON[i].propertyName.includes("Attractions[")) {
                                let span = document.getElementById(err.responseJSON[i].propertyName).children[2];
                                span.innerHTML = err.responseJSON[i].errorMessage;
                            }
                            else {
                                let span = document.getElementById(err.responseJSON[i].propertyName).children[2];
                                span.innerHTML = err.responseJSON[i].errorMessage;
                            }

                        }
                        else {
                            let span = document.getElementById(err.responseJSON[i].propertyName).children[2];
                            span.innerHTML = err.responseJSON[i].errorMessage;
                        }

                    }
                }

            },
            contentType: "application/json",
            data: JSON.stringify(obj)
        });

    });



    // cool search bar
    let searchBox = document.getElementById("Attractions[0].AttractionId");
    searchBox.onkeyup = (text) => {
        let value = text.target.value;

        $.ajax({
            type: "get",
            url: "https://localhost:7266/Attraction/GetNameOfAttractionsByPartiallyName?partOfName=" + value,
            success: (resp) => {

                let listOfSuggestions = document.getElementById("suggestions");
                listOfSuggestions.innerHTML = "";

                for (let i = 0; i < resp.length; i++) {
                    let suggestion = document.createElement("option");
                    suggestion.style.cursor = "pointer";
                    suggestion.innerHTML = resp[i].name;
                    suggestion.value = resp[i].id;
                    suggestion.addEventListener("click", (e) => {
                        let search = document.getElementById("Attractions[0]");
                        search.value = e.target.innerHTML;
                        search.dataset.id = e.target.value;
                    });
                    listOfSuggestions.appendChild(suggestion);

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
    }



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

    let sendModalButton = document.getElementById("modal-send-button");
    let closeModalButton = document.getElementById("modal-close-button");

    sendModalButton.addEventListener("click", () => {
        let attractionName = document.getElementById("modal-attraction-name");
        let description = document.getElementById("modal-description");
        let attractiontype = document.getElementById("modal-attractionTypes");
        let latitude = document.getElementById("modal-latitude");
        let longitude = document.getElementById("modal-longitude");
        let location = document.getElementById("modal-location");
        let height = document.getElementById("modal-height");
        let mountain = document.getElementById("modal-mountains");

        let modalErrorSpans = document.getElementsByClassName("modal-error");
        for (let i = 0; i < modalErrorSpans.length; i++) {
            modalErrorSpans[i].innerHTML = "";
        }

        const obj = {
            attractionName: attractionName.value,
            typeAttractionId: Number(attractiontype.value),
            description: description.value,
            latitude: Number(latitude.value),
            longitude: Number(longitude.value),
            location: location.value,
            height: height.value,
            mountains: mountain.value,
        }
        $.ajax({
            type: "post",
            url: "https://localhost:7266/SuggestionAttraction/CreateSuggestionAttraction",
            success: (resp) => {
                closeModalButton.click();
                attractionName.value = "";
                attractiontype.value = 1;
                description.value = "";
                latitude.value = "";
                longitude.value = "";
                location.value = "";
                height.value = "";
                mountain.value = "";
            },
            error: (err) => {
                if (err.status == 412) {
                    $(document).ready(function () {
                        $('#exampleModal').modal('show');
                    });
                    for (let i = 0; i < err.responseJSON.length; i++) {
                        let span = document.getElementById(err.responseJSON[i].propertyName).children[2];
                        span.innerHTML = err.responseJSON[i].errorMessage;
                    }
                }
                else if (err.status == 404) {
                    location.href = "https://localhost:7266/CustomError/Error404";
                }
                else if (err.status == 400) {
                    location.href = "https://localhost:7266/CustomError/Error_BadRequest";
                }
            },
            contentType: "application/json",
            data: JSON.stringify(obj)
        });
    });
}