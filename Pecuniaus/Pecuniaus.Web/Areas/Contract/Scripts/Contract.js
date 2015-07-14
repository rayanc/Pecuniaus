$(function () {

});
/*Scan Doc*/

function EditDoc(id) {
    $('#fileDoc').show();
    $('#lkdocAddNew').hide();
    $('#hDocID').val(id);
}
$('#lkdocAddNew').on('click', function () {
    $('#fileDoc').show();
    $('#lkdocAddNew').hide();
    $('#hDocID').val('0');
});

$(document).ajaxComplete(function () {
    $('#fileDoc').hide();
    $('#lkdocAddNew').show();
    $('#hDocID').val('0');
});

function SetVeriTabs() {

    $('#VeriTabs a[data-toggle="tab"]').on('show.bs.tab', function (e) {
        var paneID = $(e.target).attr('href');
        $target = $(paneID);
        var src = $target.attr('data-url');

        $target.load(src, function () {
            if ($target.attr('id') == 'tBankInfo') {
                InitBankInfo();
            }
            else if ($target.attr('id') == 'tLLVeri') {
                InitLLWR();
            }
            else if ($target.attr('id') == 'tCorpDoc') {
                InitCorpDocsVeri();
            }
            else if ($target.attr('id') == 'tCommName') {
                InitCommertialNameVeri();
            }

        });
    });

    $('#VeriTabs a[data-toggle="tab"]:first').tab('show');
}

function InitLLWR() {
    $('.qrWR:checked').each(function () {
        SetCtrlAttr(this);
    });

    $('.qrWR').on('change', function () {
        SetCtrlAttr(this);
    });

    $('#frmLLVeri').ajaxForm({
        complete: function (xhr) {
            var $file = $("#file")
            resetFormInputElement($file);
        }
    });
}
function GetImageData(selectedItem, imgId) {
    $.ajax({
        url: '/Contract/DocumentScan/GetImage/?docType=' + selectedItem,
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            $('#' + imgId).attr('src', data.FilePath);
        },
        error: function (x, y, z) {
            //     alert(x + '\n' + y + '\n' + z);
        }
    });
}

function ShowSuccessMessage(msg) {
}

function PopQuesSubmit(var1) {
    $text = $('#' + var1);
    $.post("/Common/Notes/AddVeriCall", { notes: $text.val() });
}


function SetCtrlAttr(s) {
    var $rdo = $(s);
    var $ctrls = $rdo.parent().siblings("input[type=text],select");
    if ($rdo.val() == "true") {
        //if ($ctrls.hasClass("input-datepicker-close"))
        //{
        //    $ctrls.unbind();
        //    $ctrls.addClass("input-datepicker-close")
        //}
        $ctrls.attr('readonly', 'readonly');
    }
    else {
        $ctrls.removeAttr('readonly');
        //if ($ctrls.hasClass("input-datepicker-close")) {
        //    $ctrls.datepicker({ weekStart: 1 }).on("changeDate", function (e) { $(this).datepicker("hide") });
        //}
       
    }
}

function resetFormInputElement(e) {
    e.wrap('<form>').closest('form').get(0).reset();
    e.unwrap();
}
function ReloadDocGrid(divId) {
    var $targetDiv = $(divId);
    var loadLink = $targetDiv.attr('data-source-url');
    $targetDiv.load(loadLink);
}

function initDocUpload(formid, docGridId) {
    var $addLink = $(formid + ' a[data-addnewdoc]');
    var $editLink = $(formid + ' a[data-editdoc]');
    var $uploadcont = $(formid + ' div[data-uploadcont]');
    var $cancDoc = $(formid + ' a[data-canceldoc]');
    var $saveDoc = $(formid + ' a[data-savedoc]');
    //var $currDocId = $(formid + ' input[name=grdDocID]');
    var $docId = $(formid + ' input[data-docId]');

    $saveDoc.off('click').on("click", function () {
        $(formid).submit();
    });

    $cancDoc.off('click').on("click", function () {
        $addLink.show();
        $editLink.show();
        $uploadcont.hide();
    });

    $addLink.off('click').on("click", function () {
        $docId.val('0');
        $addLink.hide();
        $editLink.hide();
        $uploadcont.show();
    });

    $editLink.off('click').on("click", function () {
        $editLink.hide();
        var did = $(formid + ' input[name=grdDocID]').val()
        if (did == '' || typeof did === "undefined")
        { did = 0; }
        $docId.val(did);

        $addLink.hide();
        $uploadcont.show();
    });

    (function () {
        $(formid).ajaxForm({
            complete: function (xhr) {
                var $file = $(formid + " input[type=file]")
                resetFormInputElement($file);
                ReloadDocGrid(docGridId)
                $addLink.show();
                $editLink.show();
                $uploadcont.hide();
            }
        });
    })();
}
