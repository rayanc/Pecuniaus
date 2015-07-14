
function InitCorpDocsVeri() {
   // docTypeId = $('input[type=hidden]#hCopDocTId').val();
    ReloadDocGrid('#dvCorpDocPrev');
   // GetDocsList(docTypeId, 'dvCorpDocPrev')
    ajaxifyGridMvc('#dvCorpDocPrev');
    CECorpOwner();
    OwnerList();
    initDocUpload('#frmCorpDoc', '#dvCorpDocPrev');
}



function CorpOwnerUpdated() {
    CECorpOwner();
  //  $('#dvAddCorpDoc').hide();
    OwnerList();
}

function OwnerList() {
    var $dvListCorpDoc = $('#dvListCorpDoc');
    $dvListCorpDoc.load($dvListCorpDoc.attr('data-source-url'));
}

function EditCorpDoc(ownerID) {
    $dvAddCorpDoc = $('#dvAddCorpDoc');
    $('#dvAddCorpDoc').show().load($dvAddCorpDoc.attr('data-source-edit-url') + '?id=' + ownerID);
}
function CECorpOwner() {
    var $dv = $('#dvAddCorpDoc');
    $dv.load($dv.attr('data-source-url'));
   // $('#dvAddCorpDoc').hide();
}