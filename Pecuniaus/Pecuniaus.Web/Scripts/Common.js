
$(document).ready(
       function () {
           $("#ContSrchMdl").on('show.bs.modal', function (e) {
               var $modl = $(this);
               var $modBody = $modl.find('.modal-body');
               $modBody.html('Loading...');
           });

           $("#ContSrchMdl").on('shown.bs.modal', function (e) {
               var $modl = $(this);
               var $modBody = $modl.find('.modal-body');
               var loadURL = $modBody.attr("data-source-url");
               var name = $("#sMerchantName").val();
               var task = $("#sTaskName").val();
               var status = $("#sTaskStatus").val();
               var workFlowId = $("#sWorkFlowID").val();
               var searchType = $("#sSearchType").val();
               var contId = $("#sContId").val();
               var userId = $("#sUserId").val();

               var paramsURL = "name=" + name + "&task=" + task + "&status=" + status
                   + "&workFlowID=" + workFlowId + "&SearchType=" + searchType + "&contractId=" + contId + "&userid=" + userId;
               $modBody.attr("data-params", paramsURL)

               $modBody.load(loadURL, { name: name, task: task, status: status, workFlowID: workFlowId, SearchType: searchType, contractId: contId, userId: userId });
           });

           $("#modal-sf-list").on('show.bs.modal', function (e) {
               var $modl = $(this);
               var $modBody = $modl.find('.modal-body');
               $modBody.html('Loading...');
           });

           $("#modal-sf-list").on('shown.bs.modal', function (e) {
               var $modl = $(this);
               var $modBody = $modl.find('.modal-body');
               var loadURL = $modBody.attr("data-source-url");
               $modBody.load(loadURL);
           });


           $("#modal-notes").on('show.bs.modal', function (e) {
               var $modl = $(this);
               var $modBody = $modl.find('.modal-body');
               $modBody.html('Loading...');
           });
           $("#modal-notes").on('shown.bs.modal', function (e) {
               var $modl = $(this);
               var $modBody = $modl.find('.modal-body');
               var loadURL = $modBody.attr("data-source-url");
               $modBody.load(loadURL);
           });

           $("#mod_notes_add").on('show.bs.modal', function (e) {
               var $modl = $(this);
               var $modBody = $modl.find('.modal-body');
               $modBody.html('Loading...');
           });
           $("#mod_notes_add").on('shown.bs.modal', function (e) {
               var $modl = $(this);
               var $modBody = $modl.find('.modal-body');
               var loadURL = $modBody.attr("data-source-url");
               $modBody.load(loadURL);
               $("#modal-notes").modal('hide')
           });

           $("#mod_notes_add").on('hide.bs.modal', function (e) {
               $("#modal-notes").modal('show')
           });


           $("#mod_decline").on('shown.bs.modal', function (e) {
               var $modl = $(this);
               var $modBody = $modl.find('.modal-body');
               var loadURL = $modBody.attr("data-source-url");

               $modBody.load(loadURL);
           });

           $("#mod_SendAceptanceMail").on('show.bs.modal', function (e) {
               var button = $(e.relatedTarget) // Button that triggered the modal
               var offerModified = button.data('offermodified') // Extract info from data-* attributes
               if (offerModified) {
                   alert(button.data('errmsg'))
                   return e.preventDefault();
               }
           });

           $("#mod_SendAceptanceMail").on('shown.bs.modal', function (e) {
               var $modl = $(this);
               var $modBody = $modl.find('.modal-body');
               var loadURL = $modBody.attr("data-source-url");
               $modBody.load(loadURL);
           });

           $("#mod_SendOfferEmail").on('show.bs.modal', function (e) {
               var button = $(e.relatedTarget) // Button that triggered the modal
               var offerModified = button.data('offermodified') // Extract info from data-* attributes
               if (offerModified) {
                   alert(button.data('errmsg'))
                   return e.preventDefault();
               }
           });

           $("#mod_SendOfferEmail").on('shown.bs.modal', function (e) {
               var $modl = $(this);
               var $modBody = $modl.find('.modal-body');
               var loadURL = $modBody.attr("data-source-url");
               $modBody.load(loadURL);
           });

           //$("#modScanDoc").on('show.bs.modal', function (e) {
           //    var $modBody = $(this).find('.modal-body');
           //    var htmlLenght = $.trim($modBody.html()).length
           //    if (htmlLenght == 0) {
           //        $modBody.html('Loading...');
           //    }
           //});
           //$("#modScanDoc").on('shown.bs.modal', function (e) {
           //    var $modl = $(this);
           //    var $modBody = $modl.find('.modal-body');
           //    if ($modBody.text() == 'Loading...') {
           //        $modBody.load($modl.attr('data-source-url'), function () { ajaxifyGridMvc("#dvDocsListModal"); });
           //        $(document).on('change', '.mdocType', function () {
           //            GetDocsList($(this).val(), 'dvDocsListModal')
           //        });
           //    }
           //});
           /* Ajaxify Accordin*/
           $("div[data-ajax-accord=true]").on('show.bs.collapse', function (e) {
               var $target = $(e.target)
               var url = $target.attr('data-source-url');
               if (url != 'undefined') {
                   var $body = $target.find('.panel-body');
                   if ($.trim($body.html()).length == 0) {
                       $body.load(url);
                   }
               }
           });

           ajaxifyGridMvc("#dvMSrchRes");
           ajaxifyGridMvc("#modal-sf-listchild");
           ajaxifyGridMvc("#modal-sf-listchildMP");
           ajaxifyGridMvc("#dvNotesList");
           ajaxifyGridMvc("div[data-ajax-grid=true]");

           //$("div[data-ajax-accord2=true]").on('show.bs.collapse', function (e) {
           window.setTimeout(function () {
               $(".alert-success").fadeTo(1500, 0).slideUp(500, function () {
                   $(this).remove();
               });
           }, 5000);
       });

function GetDocsList(selectedItem, targetControlId) {
  
    var $targetDiv = $('#' + targetControlId);
        var loadLink = $targetDiv.attr('data-source-url');
    if (loadLink) {
        var seprator = "?";
        if (loadLink.indexOf('?') > 0) {
            seprator = "&";
        }
        $targetDiv.load(loadLink + seprator + 'docType=' + selectedItem);
    }
}

// Autoclose alert

function NoteAdded() {
    $("#mod_notes_add").modal('hide');
    //rebind notes list
}


$(document).ajaxComplete(function () {
    //$(".grid-mvc").each(function () {
    //    $(".grid-mvc").gridmvc();
    //});
    //$.validator.unobtrusive.parse(document);
    if (typeof $.validator.unobtrusive === 'undefined') {
    }
    else {
        $.validator.unobtrusive.parse('form');
    }

    initDatePicker();
    $("#loading").modal('hide');

    //   , "#modal-sf-listchild .grid-wrap .row"
    $('#dvMSrchRes .grid-wrap .popup_hover, #modal-sf-listchild .grid-wrap .popup_hover').click(function () {
        var $hdn = $(this).find("input[type=hidden]");
        var merchantID = $hdn.val();
        var taskTypeID = $hdn.attr("data-taskid");
        var contractID = $hdn.attr("data-contractid");

        //   window.location = "?merchantid=" + merchantID + "&ttid=" + taskTypeID;
        //window.location = "/Common/Search/SrchRedirect?merchantId=" + merchantID + "&ttid=" + taskTypeID;        
        window.location = "/Common/Search/SrchRedirect?merchantId=" + merchantID + "&ttid=" + taskTypeID + "&contractId=" + contractID;
        $('#loading').modal('show')
    });

    if ($('.modal.in').length > 0)
    {
        $(document.body).addClass('modal-open')
    }
    else
    { 
        $(document.body).removeClass('modal-open')
    }
});

$(document).ajaxStart(function () {
    ShowLoading();
});

function ShowLoading() {
    $('#loading').modal('show');
}

function initDatePicker() {
    $(".input-datepicker").datepicker({ weekStart: 1 })
    $(".input-datepicker-close").datepicker({ weekStart: 1 }).on("changeDate", function (e) { $(this).datepicker("hide") })
}


var ServiceURL = "localhost/PecuniausBridge/";

function CallAjaxRequest(MethodRoute) {
    var Result;
    $.ajax({
        url: ServiceURL + MethodRoute,
        dataType: "json",
        async: false,
        success: function (data) {
            Result = data;
        },
        error: function (xhr, status, error) {
        },
        complete: function () {
        }
    });
    return Result;
}

function ajaxifyGridMvc(gridContainerSelector, successCallback) {
    $(gridContainerSelector).on("click", ".grid-header  a, .grid-pager li a",
        function (event) {
            var link = $(this).attr("href");
            // var $grid_container = $(event.delegateTarget);
            LoadGridMvc(event.delegateTarget, link, successCallback)
            return false;
        });
}

function LoadGridMvc(grid, link, successCallback) {
    $grid_container = $(grid);
    var baseUrl = $grid_container.attr("data-source-url");
    var qindex = baseUrl.indexOf('?');
    if (qindex > 0)
        baseUrl = baseUrl.substring(0, qindex);
    var extraParams = $grid_container.attr("data-params")
    if (typeof extraParams != 'undefined') {
        if (extraParams.length > 0) {
            link = link + "&" + extraParams;
        }
    }

    $.get(baseUrl + link, function (data) {
        if (data.Status <= 0) {
            alert(data.Message);
            return;
        }
        $grid_container.html(data);
        $grid_container.find(".grid-mvc").gridmvc();

        if ($.isFunction(successCallback))
            successCallback();
    });
}

function FetchNotes() {
    $.ajax({
        url: urlFetchNotes,
        async: false,
        data: { Mode: 'P' },
        success: function (data) {
            $('#dvNotesList').html(data);
        }
    });
}
function AddNote(rnc, businessName, legalName, ownerName) {
    $.ajax({
        url: urlAddNote,
        async: false,
        success: function (data) {
            $('#modal-search-noteschild').html(data);
        }
    });
}
function SaveNote(rnc, businessName, legalName, ownerName) {
    $.ajax({
        url: urlSaveNote,
        async: false,
        success: function (data) {
            $('#modal-search-noteschild').html(data);
        }
    });
} function FetchSFMerchants() {
    $.ajax({
        url: urlFetchSFMerchants,
        async: false,
        success: function (data) {
            $('#modal-sf-listchild').html(data);
        }
    });
}
function LoadSFMerchant(obj) {

    var MRCInfo = $.ajax({
        url: urlLoadMerchantInfo,
        async: false,
        data: { merchantId: obj },
        success: function (data) {
            return data;
        }
    });

    var SelectedMerchant = $.parseJSON(MRCInfo.responseText);
    alert(SelectedMerchant);

    $("#lblMerchantName").text(SelectedMerchant.merchantName);
    $("#lblLegalName").text(SelectedMerchant.legalName);
    $("#lblSalesRep1").text(SelectedMerchant.assignedSalesRep);
    $("#lblBusinessName").text(SelectedMerchant.businessName);
    $("#lblRNC").text(SelectedMerchant.rnc);

    $("#lblOwnerName").text(SelectedMerchant.ownerName);
    $("#lblOwnerAdd").text('');
    $("#lblSalesRep2").text(SelectedMerchant.assignedSalesRep);
    $("#lblOwnerSSN").text('');
    $("#lblOwnerPhone").text(SelectedMerchant.cellPhone);

    $("#txtRNC").val(SelectedMerchant.rnc);
    $("#txtBusinessName").val(SelectedMerchant.businessName);
    $("#txtLegalName").val(SelectedMerchant.legalName);
    $("#txtOwnerName").val(SelectedMerchant.ownerName);

    $("#txtBusinessStartDate").val(SelectedMerchant.businessStartDate);
    $("#txtRNCNumber").val(SelectedMerchant.rnc);
    $("#txtOwnerNameFrm").val('');
}

function LoadDupMerchant(obj) {
    var MRCInfo = $.ajax({
        url: urlLoadMerchantInfo,
        async: false,
        data: { merchantId: obj },
        success: function (data) {
            return data;
        }
    });
    alert(MRCInfo.responseText);
    var SelectedMerchant = $.parseJSON(MRCInfo.responseText);

    $("#lblMerchantName").text(SelectedMerchant.merchantName);
    $("#lblLegalName").text(SelectedMerchant.legalName);
    $("#lblSalesRep1").text(SelectedMerchant.assignedSalesRep);
    $("#lblBusinessName").text(SelectedMerchant.businessName);
    $("#lblRNC").text(SelectedMerchant.rnc);

    $("#lblOwnerName").text(SelectedMerchant.ownerName);
    $("#lblOwnerAdd").text(SelectedMerchant.OwnerAddress);
    $("#lblSalesRep1").text(SelectedMerchant.assignedSalesRep);
    $("#lblOwnerSSN").text(SelectedMerchant.SSN);
    $("#lblOwnerPhone").text(SelectedMerchant.cellPhone);

    $("#txtRNCNumber").val(SelectedMerchant.rnc);
    $("#txtBusinessName").val(SelectedMerchant.businessName);
    $("#txtLegalName").val(SelectedMerchant.legalName);
    $("#txtBusinessStartDate").val(SelectedMerchant.businessStartDate);
    $("#txtOwnerNameFrm").val(SelectedMerchant.ownerName);
    $("#txthdnMerchantId").val(SelectedMerchant.merchantId);
    alert(SelectedMerchant.TypeofBusinessentity);
    var $radios = $('input:radio[name=TypeofBusinessentity]');
    $radios.filter('[value=' + SelectedMerchant.TypeofBusinessentity + ']').prop('checked', true);
    $("#drpIndustry").val(SelectedMerchant.industryTypeId);
    $("#SalesRep").val(SelectedMerchant.salesRepId);


}

function LoadSFMerchantANDSetRecentVisit(obj, value) {
    //Set Recent Visit Value
    $.ajax({
        url: urlLoadSFMerchantANDSetRecentVisit,
        async: false,
        data: { MerchantID: value }
    });

    //alert(value);
    //var TempMerchants = '@Html.Raw(Session["SFMerchants"])';
    var TempMerchants = '@Html.Raw(Session["SFMerchants"])';
    //var TempMerchants = '@Html.Raw(@Html.ViewContext.Controller.TempData["SFMerchants"])';
    alert(TempMerchants);
    var SelectedMerchant = $.parseJSON(TempMerchants)[obj];
    //alert(SelectedMerchant.merchantId);

    $("#lblMerchantName").text(SelectedMerchant.merchantName);
    $("#lblLegalName").text(SelectedMerchant.legalName);
    $("#lblSalesRep1").text(SelectedMerchant.assignedSalesRep);
    $("#lblBusinessName").text(SelectedMerchant.businessName);
    $("#lblRNC").text(SelectedMerchant.rnc);

    $("#lblOwnerName").text(SelectedMerchant.ownerName);
    $("#lblOwnerAdd").text(SelectedMerchant.address);
    $("#lblSalesRep2").text(SelectedMerchant.assignedSalesRep);
    $("#lblOwnerSSN").text('');
    $("#lblOwnerPhone").text(SelectedMerchant.cellPhone);

    $("#txtRNC").val(SelectedMerchant.rnc);
    $("#txtBusinessName").val(SelectedMerchant.businessName);
    $("#txtLegalName").val(SelectedMerchant.legalName);
    $("#txtBusinessStartDate").val(SelectedMerchant.businessStartDate);
    $("#txtRNCNumber").val(SelectedMerchant.rnc);
    $("#txtOwnerNameFrm").val(SelectedMerchant.ownerName);


}

function FindANDLoadSFMerchant(liControl) {
    var merchantID = liControl.find("#hiddenmerchantID").val();
    //Set Recent Visit Value
    $.ajax({
        url: urlFindANDLoadSFMerchant,
        async: false,
        data: { Query: "merchantid=" + merchantID },
        success: function (data) {
            var SelectedMerchant = data[0];
            $("#lblMerchantName").text(SelectedMerchant.merchantName);
            $("#lblLegalName").text(SelectedMerchant.legalName);
            $("#lblSalesRep1").text(SelectedMerchant.assignedSalesRep);
            $("#lblBusinessName").text(SelectedMerchant.businessName);
            $("#lblRNC").text(SelectedMerchant.rnc);

            $("#lblOwnerName").text(SelectedMerchant.ownerName);
            $("#lblOwnerAdd").text('');
            $("#lblSalesRep2").text(SelectedMerchant.assignedSalesRep);
            $("#lblOwnerSSN").text('');
            $("#lblOwnerPhone").text(SelectedMerchant.cellPhone);

            $("#txtRNC").val(SelectedMerchant.rnc);
            $("#txtBusinessName").val(SelectedMerchant.businessName);
            $("#txtLegalName").val(SelectedMerchant.legalName);
            $("#txtOwnerName").val(SelectedMerchant.ownerName);

            $("#txtBusinessStartDate").val(SelectedMerchant.businessStartDate);
            $("#txtRNCNumber").val(SelectedMerchant.rnc);
            $("#txtOwnerNameFrm").val('');
        }
    });
}
function FetchMerchantSearch(MerchantInfo, TaskType, TaskStatus) {
    $.ajax({
        url: urlFetchMerchantSearch,
        async: false,
        data: { MerchantInfo: MerchantInfo, TaskType: TaskType, TaskStatus: TaskStatus },
        success: function (data) {
            $('#modal-sf-listchild').html(data);
        }
    });
}
function FetchMerchantSearchMP(MerchantID, MerchantStatusID, ProcessorID, processorNumber, ownerName, legalName, businessName, ownerID) {
    $.ajax({
        url: urlFetchMerchantSearch,
        async: false,
        data: { MerchantID: MerchantID, MerchantStatus: MerchantStatusID, ProcessorName: ProcessorID, ProcessorNumber: processorNumber, OwnerName: ownerName, LegalName: legalName, BusinessName: businessName, OwnerID: ownerID },
        success: function (data) {
            $('#modal-sf-listchildMP').html(data);
        }
    });
}
function FetchDuplicateMerchantSearch(rnc, businessName, legalName, ownerName) {
    $.ajax({
        url: urlFetchDuplicateMerchantSearch,
        async: false,
        data: { rnc: rnc, businessName: businessName, legalName: legalName, ownerName: ownerName, SearchType: 'T' },
        success: function (data) {
            $('#divMerchantSearch').html(data);
        }
    });
}
function Success() {
    //var Msg = '@ViewBag.SuccessMsg';
    //alert(Msg);
    $("#alertMessage").html("Operation Completed Successfully");
}

//By Sachin, 

var common =
    {

        BindHtml: function (templateID, data) {
            var templateHtml = $("#" + templateID).html();
            for (p in data) {
                var regExp = new RegExp('\\$Template\.' + p, 'g');
                templateHtml = templateHtml.replace(regExp, data[p]);
            }
            return templateHtml;
        },

        AjaxCall: function (templateId, URL, jsonParams, spanIdNoRecord) {
            var ajaxResult;
            var options = {
                type: "POST",
                dataType: "json",
                url: URL,
                async: false,
                contentType: "application/json; charset=utf-8",
                data: jsonParams,
                success: function (ajaxResult) {
                    if (ajaxResult != null) {
                        Bind_Template(templateId, ajaxResult, spanIdNoRecord);
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                }
            }
            $.ajax(options);
            return ajaxResult;
        },
        Bind_Template: function (templateId, ajaxResult, spanIdNoRecord) {
            $("#" + templateId).html('');
            var countInfo = 0;

            var data = JSON.parse(ajaxResult);
            if (data != null) {
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        var templateHTML = null;
                        templateHTML = BindHtml(templateId, data[i]);
                        $("#" + templateId).append(templateHTML);
                    }
                    //$('#divLoader').html('');
                }
                else {
                    showNoRecordMessage(spanIdNoRecord);
                }
            }
            else {
                showNoRecordMessage(spanIdNoRecord);
            }
        },
        showNoRecordMessage: function (spanIdNoRecord) {
            $('#' + spanIdNoRecord).html('No record found.');
        }

        , EnterNumberOnly: function (txtBoxID) {
            $("#" + txtBoxID).keydown(function (event) {
                // Allow only backspace and delete

                return (event.ctrlKey || event.altKey
                              || (47 < event.keyCode && event.keyCode < 58 && event.shiftKey == false)
                              || (95 < event.keyCode && event.keyCode < 106)
                              || (event.keyCode == 8) || (event.keyCode == 9)
                              || (event.keyCode > 34 && event.keyCode < 40)
                              || (event.keyCode == 46))


            });
        }
    }