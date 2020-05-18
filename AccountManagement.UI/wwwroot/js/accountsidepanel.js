const newButton = document.querySelector("[data-new-btn]");
const usernameInput = document.querySelector("[data-username-input]");
const passwordInput = document.querySelector("[data-password-input]");
const passwordDiv = document.querySelector("[data-password-div]");
const emailInput = document.querySelector("[data-email-input]");
const phoneInput = document.querySelector("[data-phone-input]");
const idInput = document.querySelector("[data-id-input]");
const roleCombobox = document.querySelector("[data-role-combobox]");
const organizationCombobox = document.querySelector("[data-organization-combobox]");
const actionButton = document.querySelector("[data-action-button]");
const editAccounts = document.querySelectorAll('.edit');
const deleteAccount = document.querySelector("[data-delete-account]");
const accountName = document.getElementById("usernameValue");
const logBtn = document.querySelector("[data-log-btn]");

newButton.addEventListener('click', () => {
    openAccountPanel("new");
});

function openAccountPanel(type) {
    if (type === "edit") {
        passwordDiv.style.display = "none";
        passwordInput.required = false;
        deleteAccount.style.display = "block";
    }
    else if(type === "new") {
        passwordDiv.style.display = "block";
        deleteAccount.style.display = "none";
        passwordInput.required = true;
        clearValues();
        changePage(event, 'account');
        logBtn.disabled = true;
    }
    document.getElementById("accountSidepanel").style.width = "800px";
}

function closeSide() {
    document.getElementById("accountSidepanel").style.width = "0";
}

function clearValues() {
    usernameInput.value = "";
    emailInput.value = "";
    phoneInput.value = "";
    roleCombobox.value = "";
    organizationCombobox.value = "";
    idInput.value = "";
    passwordInput.value = "";
    actionButton.value = "Add";
}

function insertValuesToAccountForm(account, id) {
    let children = account.childNodes;
    for (let i = 0; i < children.length; i++) {
        let child = children[i];
        if (child.id === "username") {
            usernameInput.value = child.innerHTML;
            accountName.value = child.innerHTML;
        } else if (child.id === "email") {
            emailInput.value = child.innerHTML;
        } else if (child.id === "phone") {
            phoneInput.value = child.innerHTML;
        } else if (child.id === "role") {
            roleCombobox.value = child.innerHTML;
        } else if (child.id === "organization") {
            organizationCombobox.value = child.innerHTML;
        }
    }
    idInput.value = id;
    logBtn.disabled = false;
    deleteAccount.href = "/Accounts/Delete?Id=" + id;
    actionButton.value = "Update";
}

function editAccount(account, id) {
    insertValuesToAccountForm(account, id);
    openAccountPanel("edit");
    filterLogTable(accountName.value);
}

Array.prototype.forEach.call(editAccounts, function addClickListener(btn) {
    btn.addEventListener('click', function (event) {
        let id;
        let account;
        id = event.target.parentElement.id;
        account = event.target.parentElement;
        editAccount(account, id);
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

function filterLogTable(value) {
    let filter, table, tr, td, i, txtValue;
    filter = value.toUpperCase();
    table = document.getElementById("log");
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