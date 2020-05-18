const newButton = document.querySelector("[data-new-btn]");
const applicationNameInput = document.querySelector("[data-applicationname-input]");
const editApplications = document.querySelectorAll('.edit');
const actionButton = document.querySelector("[data-action-btn]");
const deleteApplication = document.querySelector("[data-delete-application]");
const idInput = document.querySelector("[data-id-input]");
const applicationName = document.getElementById("applicationName");
const licenseBtn = document.querySelector("[data-license-btn]");
const addlicensesBtn = document.querySelector("[data-add-licenses]");

newButton.addEventListener('click', () => {
    openApplicationPanel("new");
});

function openApplicationPanel(type) {
    if (type === "edit") {
        deleteApplication.style.display = "block";
    }
    else if (type === "new") {
        deleteApplication.style.display = "none";
        idInput.style.display = "none";
        clearValues();
        licenseBtn.disabled = true;
        changePage(event, 'application');
    }
    document.getElementById("applicationSidepanel").style.width = "800px";
}

function closeApplicationPanel() {
    document.getElementById("applicationSidepanel").style.width = "0";
}

function clearValues() {
    applicationNameInput.value = "";
    actionButton.value = "Add";
}


function insertValuesToApplicationForm(account, id) {
    idInput.value = id;
    deleteApplication.href = `/Applications/Delete?Id=${id}`;
    idInput.style.display = "none";
    actionButton.value = "Update";
    let children = account.childNodes;
    for (let i = 0; i < children.length; i++) {
        let child = children[i];
        if (child.id === "name") {
            applicationNameInput.value = child.innerHTML;
            applicationName.value = child.innerHTML;
            addlicensesBtn.href = `/Applications/AddLicenses?App=${child.innerHTML}`;
        }
    }
    licenseBtn.disabled = false;
}

function editApplication(application, id) {
    insertValuesToApplicationForm(application, id);
    openApplicationPanel("edit");
    filterLicenseTable(applicationName.value);
}

Array.prototype.forEach.call(editApplications, function addClickListener(btn) {
    btn.addEventListener('click', function (event) {
        let id;
        let application;
        id = event.target.parentElement.id;
        application = event.target.parentElement;
        editApplication(application, id);
    });
});

function changePage(evt, page) {
    let i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(page).style.display = "block";
    evt.currentTarget.className += " active";
}


function filterLicenseTable(value) {
    let filter, table, tr, td, i, txtValue;
    filter = value.toUpperCase();
    table = document.getElementById("licenses");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}