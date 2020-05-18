const newButton = document.querySelector("[data-new-btn]");
const organizationNameInput = document.querySelector("[data-organizationname-input]");
const organizationBusinessIdInput = document.querySelector("[data-organizationbusinessid-input]");
const organizationInfoInput = document.querySelector("[data-organizationinfo-input]");
const editOrganizations = document.querySelectorAll('.edit');
const actionButton = document.querySelector("[data-action-btn]");
const deleteOrganization = document.querySelector("[data-delete-organization]");
const idInput = document.querySelector("[data-id-input]");
const organizationName = document.getElementById("organizationName");
const addApplicationBtn = document.querySelector("[data-add-application-btn]");
const organization = document.getElementById("organizationValue");
const organizationEmailInput = document.querySelector("[data-organizationemail-input]");
const applicationsBtn = document.querySelector("[data-applications-btn]");

newButton.addEventListener('click', () => {
    openOrganizationPanel("new");
});

addApplicationBtn.addEventListener('click', () => {
    document.getElementById("addForm").style.display = "block";
});

function openOrganizationPanel(type) {
    if (type === "edit") {
        deleteOrganization.style.display = "block";
    }
    else if (type === "new") {
        deleteOrganization.style.display = "none";
        idInput.style.display = "none";
        clearValues();
        applicationsBtn.disabled = true;
        changePage(event, 'organization');
    }
    document.getElementById("organizationSidepanel").style.width = "500px";
}

function closeOrganizationPanel() {
    document.getElementById("organizationSidepanel").style.width = "0";
    
}

function hideSettings(child) {
    child.hidden = true;
}

function clearValues() {
    organizationNameInput.value = "";
    organizationBusinessIdInput.value = "";
    organizationInfoInput.value = "";
    organizationEmailInput.value = "";
    actionButton.value = "Add";
}


function insertValuesToOrganizationForm(organization, id) {
    let children = organization.childNodes;
    for (let i = 0; i < children.length; i++) {
        let child = children[i];
        if (child.id === "name") {
            organizationNameInput.value = child.innerHTML;
            organization.value = child.innerHTML;
            organizationName.value = child.innerHTML;
        } else if (child.id === "businessid") {
            organizationBusinessIdInput.value = child.innerHTML;
        } else if (child.id === "info") {
            organizationInfoInput.value = child.innerHTML;
        } else if (child.id === "email") {
            organizationEmailInput.value = child.innerHTML;
        }
    }
    idInput.value = id;
    deleteOrganization.href = `/Organizations/Delete?Id=${id}`;
    idInput.style.display = "none";
    actionButton.value = "Update";
    applicationsBtn.disabled = false;
}

function editOrganization(organization, id) {
    insertValuesToOrganizationForm(organization, id);
    openOrganizationPanel("edit");
    filterApplicationsTable(organizationName.value);
    sortSelect(document.getElementById("applicationDrobdown"));
    sortTable();
    changeApplicationDrobdown(organizationName.value);
}

Array.prototype.forEach.call(editOrganizations, function addClickListener(btn) {
    btn.addEventListener('click', function (event) {
        let id;
        let organization;
        id = event.target.parentElement.id;
        organization = event.target.parentElement;
        editOrganization(organization, id);
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

function filterApplicationsTable(value) {
    let filter, table, tr, td, i, txtValue;
    filter = value.toUpperCase();
    table = document.getElementById("apps");
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

function changeApplicationDrobdown(organization) {
    const table = document.getElementById("apps");
    let tr = table.getElementsByTagName("tr");
    let applications = new Array();
    for (let i = 0; i < tr.length; i++) {
        let td = tr[i].getElementsByTagName("td")[1];
        let organizationtd = tr[i].getElementsByTagName("td")[0];
        if (organizationtd) {
            if (td) {
                if (organizationtd.innerText === organization) {
                    let txtValue = td.textContent || td.innerText;
                    applications.push(txtValue);
                }
            }
        }
    }
    const applicationDrobdown = document.getElementById("applicationDrobdown");
    applicationDrobdown.value = "";
    for (let i = 0; i < applications.length; i++) {
        let txt = applicationDrobdown.options[i].value;
        let app = applications[i];
        if (txt === app) {
            applicationDrobdown.options[i].style.display = "none";
        } else {
            applicationDrobdown.options[i].style.display = "block";
        }
    }
}

function sortSelect(selElem) {
    let tmpAry = new Array();
    for (let i = 0; i < selElem.options.length; i++) {
        tmpAry[i] = new Array();
        tmpAry[i][0] = selElem.options[i].text;
        tmpAry[i][1] = selElem.options[i].value;
    }
    tmpAry.sort();
    while (selElem.options.length > 0) {
        selElem.options[0] = null;
    }
    for (let i = 0; i < tmpAry.length; i++) {
        let op = new Option(tmpAry[i][0], tmpAry[i][1]);
        selElem.options[i] = op;
    }
    return;
}

function filterFunction(value, select) {
    for (let i = 0; i < select.length; i++) {
        let txt = select.options[i].value;
        if (value.toLowerCase() !== txt.toLowerCase()) {
            select.options[i].style.display = "block";
        } else {
            select.options[i].style.display = "none";
        }
    }
} 

function sortTable() {
    let table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("apps");
    switching = true;
    while (switching) {
        switching = false;
        rows = table.rows;
        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("TD")[1];
            y = rows[i + 1].getElementsByTagName("TD")[1];
            if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}