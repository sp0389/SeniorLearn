const selectRoleElement = document.getElementById("SelectedRole");
const radioSelectElement = document.getElementById("radioSelect");
const renewalCheckboxDiv = document.getElementById("checkDiv");
const renewalCheckboxElement = document.getElementById("renewalCheckbox");
const renewalDateElement = document.getElementById("renewalDate");
const textBoxElementStandard = document.getElementById("textBoxStandard");
const textBoxElementProfessional = document.getElementById("textBoxProfessional");
const textBoxElementHonorary = document.getElementById("textBoxHonorary");

selectRoleElement.addEventListener("change", toggleAll);
renewalCheckboxElement.addEventListener("change", toggleAll);

function toggleAll() {
    toggleText();
    toggleCheck();
    toggleRadio();
    toggleDate();
}

function toggleRadio() {
    if (!renewalCheckboxElement.checked && selectRoleElement.value === "2") {
        radioSelectElement.style.display = "block";
    } else {
        radioSelectElement.style.display = "none";
    }
}

function toggleDate() {
    renewalDateElement.style.display = renewalCheckboxElement.checked ? "block" : "none";
}

function toggleCheck() {

    if (selectRoleElement.value === "3" || selectRoleElement.value === "0" || selectRoleElement.value === "") {
        renewalCheckboxDiv.style.display = "none";
    } else {
        renewalCheckboxDiv.style.display = "block";
    }
}

function toggleText() {
    textBoxElementStandard.style.display = selectRoleElement.value === "1" ? "block" : "none";
    textBoxElementProfessional.style.display = selectRoleElement.value === "2" ? "block" : "none";
    textBoxElementHonorary.style.display = selectRoleElement.value === "3" ? "block" : "none";
}

toggleAll();