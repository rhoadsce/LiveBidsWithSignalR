/// <reference path="jquery-1.6.4-vsdoc.js" />

var my = my || {};

$(document).ready(function () {
    var connection = $.connection('echo');

    my.item = function () {
        this.liveBidItemId = ko.observable();
        this.name = ko.observable();
        this.description = ko.observable();
    };

    my.liveBidItems = (function () {
        var items = ko.observableArray([new my.item()]);
        var selectedItem = ko.observable(new my.item());
        var loadItems = function () {
            var url = '/LiveBidItem/GetAll';
            $.getJSON(url, function (data) {
                if (data && data != 'ERROR') {
                    items(data);
                }
            });
        };
        var setSelectedItem = function () {
            my.liveBidItems.selectedItem(this);
            $('#ModifyActions').show();
            $('#CreateActions').hide();
        };
        var navigateToBidPage = function () {
            window.location.href = '/LiveBid/Index/' + this.liveBidItemId;
        };
        var createItem = function () {
            saveItem('/LiveBidItem/Create');
        };
        var removeItem = function () {
            saveItem('/LiveBidItem/Delete');
        };
        var updateItem = function () {
            saveItem('/LiveBidItem/Update');
        };
        var saveItem = function (url) {
            $.ajax({
                url: url,
                type: 'POST',
                data: $.param(my.liveBidItems.selectedItem()),
                success: function (response) {
                    if (response == "SUCCESS") {
                        connection.send("RefreshItemList");
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                    alert(thrownError);
                }
            });
            ResetFields();
        };

        return {
            items: items,
            selectedItem: selectedItem,
            loadItems: loadItems,
            setSelectedItem: setSelectedItem,
            createItem: createItem,
            removeItem: removeItem,
            updateItem: updateItem,
            navigateToBidPage: navigateToBidPage
        };
    })();

    connection.received(function (eventType) {
        if (eventType == 'RefreshItemList') {
            my.liveBidItems.loadItems();
        }
    });

    connection.start();

    my.liveBidItems.loadItems();

    ko.applyBindings(my.liveBidItems);

    $('#Cancel, #Clear').click(function () {
        ResetFields();
    });
    $('#ModifyActions').hide();

    function ResetFields() {
        $('#ModifyActions').hide();
        $('#CreateActions').show();
        $('tr#Fields th#Field input, tr#Fields th#Field select').val('');
    }
});