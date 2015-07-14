
function openDialog(dvsource, target, url) {
    $(dvsource).attr('data-source-url', url);
    $('#btnTrigger').attr('data-target', target)
    $("#btnTrigger").click();
}

$(document).ready(function () {
    ajaxifyGridMvc("#divcreditreport");
    $("#btngeneratescore").click(function () {
        if ($('#score').val() != '') {
            var retVal = confirm("Do you wish to get new score?");
            if (retVal == true) {
                getscore();
            }
            else {
                return false;
            }
        }
        else {
            getscore();
        }

    })
});

function getscore() {
    $.ajax({
        type: "GET",
        url: "/MerchantEvaluation/GetScore",
        dataType: "json",
        success: function (data) {
            if (data.Error == 'NoPull')
            { alert('Please pull credit report before scoring.'); }
            else{
                $('#divscore').html('');
                $('#divfinalletter').html('');
                $('#score').val(data.score);
                $('#finalletter').val(data.finalletter);
                $('#roundedscore').val(data.roundedscore);
                $('<b>' + data.score + '</b>').appendTo('#divscore');
                $('<b>' + data.finalletter + '</b>').appendTo('#divfinalletter');
            }
        },
        error: function (ex) {
            alert(ex);
        }
    });
    return false;
}

//Credit Pull

$(document).ready(function () {
        $("#btnrequest").click(function () {
            if ($("#typeid").val() == '0') {
                alert('Please Select credit pull type');
                return false;
            }
            else {
                var drpvalue = $("#typeid").val();
                if (drpvalue == "1") {

                    if ($("#hdnowner").val() == '1') {
                        var retVal = confirm("Do you wish to get owner credit report?");
                        if (retVal == true) {
                            getcreditpull();
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        getcreditpull();
                    }
                }
                if (drpvalue == "2") {

                    if ($("#hdncompany").val() == '1') {
                        var retVal = confirm("Do you wish to get company credit report?");
                        if (retVal == true) {
                            getcreditpull();
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        getcreditpull();
                    }
                }
            }

        })
});

function getcreditpull() {
    $.ajax({
        type: "POST",
        url: "/MerchantEvaluation/InsertCreditReport",
        dataType: "html",
        data: { type: $("#typeid").val(), chkNoReport: $("#chkNoReport").prop("checked") },
        success: function (data) {
            $("#divcreditreport").html(data);
        },
        error: function (ex) {
            alert('Failed');
        }
    });
    return false;
}
