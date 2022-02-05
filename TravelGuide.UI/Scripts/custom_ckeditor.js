$(function () {

    var edt = CKEDITOR.replace("DateInformation");
    CKEDITOR.replace("WanderPlaces");
    CKEDITOR.replace("Foods");
    CKEDITOR.replace("OtherInformations");

    CKFinder.setupCKEditor(edt);
})