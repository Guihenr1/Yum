function ShowConfirmationModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('yum-modal')).show();
}

function HideConfirmationModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('yum-modal')).hide();
}