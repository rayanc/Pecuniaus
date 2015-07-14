
function InitVeriCall() {
    $('.qrWR:checked').each(function () {
        SetCtrlAttr(this);
    });

    $('.qrWR').on('change', function () {
        SetCtrlAttr(this);
    });


    $("select").on("focus mousedown mouseup click", function (e) {
        if ($(this).is('[readonly]')) {
            e.preventDefault();
            e.stopPropagation();
        }
    });
  }


function AjaxInitVeriCall()
{
    InitVeriCall();
    $('#frmVeriCall').ajaxForm({
        complete: function (xhr) {
            RvwVeriCallUpdated();
        }
    });
}

//method for partial view
function RvwVeriCallUpdated() {
    $target = $("#cntRevw_VerCall");
    var url = $target.attr('data-source-url');

    var $body = $target.find('.panel-body');
    $body.load(url, function () {
        AjaxInitVeriCall();
    });

}
