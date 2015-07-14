
function InitCommertialNameVeri() {
    //docTypeId = $('input[type=hidden]#hComNameDocTId').val();
    ReloadDocGrid('#dvComNameDoc');
    //GetDocsList(docTypeId, 'dvComNameDoc')
    ajaxifyGridMvc('#dvComNameDoc');
    initDocUpload('#frmCommnDoc', '#dvComNameDoc');
  //  OwnerList();
}
