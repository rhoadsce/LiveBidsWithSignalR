/// <reference path="jquery-1.6.4-vsdoc.js" />

var my = my || {};
$(document).ready(function () {
    var connection = $.connection('/echo');

    my.LiveBid = (function () {
    })();

    $('#FormContainer').delegate('form', 'submit', function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (response) {
                if (response == "SUCCESS") {
                    connection.send("RefreshBid");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.responseText);
                alert(thrownError);
            }
        });
        return false;
    });

    connection.received(function (eventType) {
        if (eventType == 'RefreshBid') {
            GetCurrentBid();
        }
    });

    connection.start();
    GetCurrentBid();

});

function GetCurrentBid() {
    var url = '/LiveBid/GetCurrentBid/@Model.LiveBidItemId';
    $.getJSON(url, function (data) {
        if (data && data != 'ERROR') {
            $('#CurrentBid').html(data.Bid);
        }
    });
}