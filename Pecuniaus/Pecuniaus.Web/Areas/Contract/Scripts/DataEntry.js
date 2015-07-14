$(document).ready(
    function () {
        InitDE();
    });
function InitDE() {
    var $dvEditProcessor = $('#dvEditProcessor');
    $dvEditProcessor.load($dvEditProcessor.attr('data-source-url'));

    var $dvEditTradeReference = $('#dvEditTradeReference');
    $dvEditTradeReference.load($dvEditTradeReference.attr('data-source-url'));

    var $dvEditOwner = $('#dvEditOwner');
    $dvEditOwner.load($dvEditOwner.attr('data-source-url'));

    var $dvEditStatement = $('#dvEditStatement');
    $dvEditStatement.load($dvEditStatement.attr('data-source-url'));

    LoadLandlord();
    RefreshStatementList();

    ajaxifyGridMvc("#dvOwners");
    ajaxifyGridMvc("#dvProcessor");
    ajaxifyGridMvc("#dvTradeReference");
    ajaxifyGridMvc("#dvStatement");
}
function OwnerUpdated() {
    RefreshOwnerList();
};
function OwnerDeleted() {
    RefreshOwnerList();
};

function EditOwner(ownerID) {
    var $dv = $('#dvEditOwner');
    $dv.load($dv.attr('data-edit-url') + '?id=' + ownerID);
}
function CEOwner() {
    var $dv = $('#dvEditOwner');
    $dv.load($dv.attr('data-source-url'));
}
function RefreshOwnerList() {
    var $dvOwners = $('#dvOwners');
    var url = $dvOwners.attr("data-source-url");
    $dvOwners.load(url);
}

function ProcessorUpdated() {
    RefreshProcessorList();
};
function EditProcessor(ownerID) {
    var $dvEdit = $('#dvEditProcessor');
    $dvEdit.load($dvEdit.attr("data-edit-url") + '?ID=' + ownerID);
}
function ProcessorDeleted() {
    RefreshProcessorList();
}
function CEProcessor() {
    var $dv = $('#dvEditProcessor');
    $dv.load($dv.attr('data-source-url'));
}

function RefreshProcessorList() {
    var $dvProcessor = $('#dvProcessor');
    var url = $dvProcessor.attr("data-source-url");
    $dvProcessor.load(url);
}

function TradeReferenceUpdated() {
    RefreshTradeReferenceList();
};
function EditTradeReference(refID) {
    var $dvTR = $('#dvEditTradeReference');
    $dvTR.load($dvTR.attr("data-edit-url") + '?id=' + refID);
}
function TradeReferenceDeleted() {
    RefreshTradeReferenceList();
}
function CETradeReference() {
    var $dvTR = $('#dvEditTradeReference');
    $dvTR.load($dvTR.attr("data-source-url"));
}

function RefreshTradeReferenceList() {
    var $dvTradeReference = $('#dvTradeReference');
    var url = $dvTradeReference.attr("data-source-url");
    $dvTradeReference.load(url);
}

function StatementUpdated() {
    RefreshStatementList();
};
function EditStatement(refID) {
    var $edv = $('#dvEditStatement');
    $edv.load($edv.attr("data-edit-url")+'?ID=' + refID);
}
function StatementDeleted() {
    RefreshStatementList();
}
function CEStatement() {
    var $dv = $('#dvEditStatement');
    $dv.load($dv.attr('data-source-url'));
}

function RefreshStatementList() {
    var $dvStatement = $('#dvStatement');
    var url = $dvStatement.attr("data-source-url");
    $dvStatement.load(url);
}

function LoadLandlord() {
    var propType = $('#drpPropertyType');
    var $dvland = $('#dvLandlord');
    var $RentAmount = $('#RentAmount');
    if (propType.val() == "200002") {
        $dvland.hide();        
        $RentAmount.attr("disabled", "disabled");
        $RentAmount.val("0.00");
    }
    else {
        $dvland.show();
        $RentAmount.removeAttr("disabled");
    }
}

function AjaxInitDE()
{
    $('#frmDE').ajaxForm({
        complete: function (xhr) {
            RvwDeUpdated();
        }
    });
    InitDE();
}

//method for partial view
function RvwDeUpdated() {
    $target = $("#cntRevw_DE");
    var url = $target.attr('data-source-url');

    var $body = $target.find('.panel-body');
    $body.load(url, function () {
        AjaxInitDE();
    });
}