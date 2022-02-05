$(function () {
    $('#memberDeleteModal').on('show.bs.modal', function (e) {

        var btn = $(e.relatedTarget)
        var id = btn.data("member-id");
        $("#memberDeleteModal").find("#deleteMemberId").val(id);
    });
});
function btnDeleteMember_Click() {
    $("#deleteMemberForm").submit()
}