
function InitBankInfo() {
    ReloadDocGrid('#dvBankDocPrev');
    ajaxifyGridMvc('#dvBankDocPrev');
    initDocUpload('#frmBankDoc', '#dvBankDocPrev');
}

//$(document).ajaxComplete(function () {
//    initDocUpload('#frmBankDoc', '#dvBankDocPrev');
//});

//$('#ddlBankInformation').change(function () {
//    var BankId = $(this).val();

//    $.ajax({
//        url: '../VerificationTask/FilBankInformation',
//        data: { BankId: BankId },
//        success: function (result) {
//            $('input#txtAccNo').val(result.AccountNumber);
//            $('input#txtBankCode').val(result.BankCode);
//            $('input#txtaccName').val(result.AccountName);
//            $('input[type=hidden]#BankDetailId').val(result.BankDetailId);
//        },
//        error: function (errors) {
//            //alert(errors);
//        }
//    });
//});
//function RvwBankInfoUpdated()
//{
//    $target = $("#cntRevw_Veri");
//    var url = $target.attr('data-source-url');

//    var $body = $target.find('.panel-body');
//    $body.load(url, function () {
//        InitBankInfo();
//    });

//}