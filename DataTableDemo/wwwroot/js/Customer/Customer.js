var requestedColumn = "";
var ddSelectedOptions = [];
var isGenericSearch = false;
$(document).ready(function () {

    $(".customerDropDown").select2();

    $(() => {
        $('.customerDropDown')
            .select2MultiCheckboxes({
                placeholder: "--select--",
                templateSelection: function (selected, total) {
                    if (selected.length == 0) {
                        return '--select--';
                    }
                    else {
                        return "Selected " + selected.length + " of " + total;
                    }
                },
                language: {
                }
            });
    });

    /*************************DATATABLE*************************/
    var table = $('#CustomerTable').DataTable(
        {
            fnInitComplete: function () {
                $('.dataTables_scrollBody').css({
                    'border': '0',
                    'width': '1600px!important',
                    'overflow-x': 'auto',
                    'max-height': '510px'
                });
                $('.dataTables_scrollHead').css('overflow-x', 'auto', 'overflow-y', 'hidden', 'width', '1600px!important', 'position', 'sticky');

                var scrolling = false;
                $("div.dataTables_scrollHead").scroll(function (e) {

                    if (scrolling) {
                        scrolling = false;
                        return true;
                    }
                    scrolling = true;
                    $(".dataTables_scrollBody").scrollLeft($("div.dataTables_scrollHead").scrollLeft());
                });
                $(".dataTables_scrollBody").scroll(function (e) {

                    if (scrolling) {
                        scrolling = false;
                        return true;
                    }
                    scrolling = true;
                    $("div.dataTables_scrollHead").scrollLeft($(".dataTables_scrollBody").scrollLeft());
                });

            },
            "scrollX": true,
            "scrollCollapse": true,

            "colResize": {
                "tableWidthFixed": false,
            },

            "infoFiltered": "",
            "deferRender": true,
            "processing": true,
            "serverSide": true,
            "bPaginate": true,
            "orderCellsTop": true,
            "language": {
                "infoFiltered": ""
            },
            colReorder: true,
            "ajax":
            {

                "url": "/Customers/GetCustomerData",

                "type": "POST",
                contentType: "application/json",
                dataType: "json",

                data: function (d) {
                    dtData = {};
                    dtData = d;
                    return JSON.stringify(d);
                },
                "beforeSend": function () {
                    $("#loading").show();
                    $(".customerDropDown").select2("close");
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log({ "jqXHR": jqXHR });
                },
                complete: function (json) {
                    if (isGenericSearch) {
                        sessionStorage.clear();
                        isGenericSearch = false;
                    }
                    $("#loading").hide();
                }
            },

            columns: [
                { data: "id" },
                { data: "name" },
                { data: "address" },
                { data: "phoneNumber" },
                { data: "email" }
            ]
            ,
            // Column Definitions
            columnDefs: [
                {
                    className: "text-center custom-middle-align",
                    targets: [0, 1, 2, 3, 4]
                }
            ]
        });
/**************************************************************************/


    $('#Id').change(function () {
        requestedColumn = "id";
        changeFilter(table, $(this));
    });
    $('#PhoneNumber').change(function () {
        requestedColumn = "phoneNumber";
        changeFilter(table, $(this));
    });
    $('#Email').change(function () {
        requestedColumn = "email";
        changeFilter(table, $(this));
    });

    $('.customerDropDown').change(function () {

        setRequestedColumn($(this));

        var sessionData = sessionStorage[dtData.requestedFilterColumn];
        sessionStorage.clear();
        sessionStorage[dtData.requestedFilterColumn] = sessionData;

        if (this.value != "") {
            $(this).siblings(".select2-container").children("span.selection").children('.select2-selection--single').addClass('highlightfilter');
        }
        else {
            $(this).siblings(".select2-container").children("span.selection").children('.select2-selection--single').removeClass('highlightfilter');
        }


        dtData.requestedFilterColumnIndex = $(this).parent().index();
        var searchvalue = "";
        if ($(this).val() != null) {
            searchvalue = $(this).val().join('|');
        }
        if (searchvalue == "") {
            ddSelectedOptions = [];
        }
        else {
            ddSelectedOptions = $(this).val();
        }
        table.column($(this).parent().index() + ':visible').search(searchvalue).draw();
    });


});

function changeFilter(table, $this) {
    if ($this.val() != "") {
        $this.addClass('highlightfilter');
        sessionStorage.clear();
    }
    else {
        $this.removeClass('highlightfilter');
        sessionStorage.clear();
    }
    table.column($this.parent().index()).search($this.val()).draw();
}


function appendOptions(element) {
    if (dtData.requestedFilterColumn == "name") {
        addOptions(element);
    }
    if (dtData.requestedFilterColumn == "address") {
        addOptions(element);
    }
}

function addOptions(element) {
    console.log('inappendoptions');
    console.log(ddSelectedOptions);
    var selectedOptions = ddSelectedOptions;
    if (selectedOptions == null) {
        selectedOptions = [];
    }
    selectedOptions.sort();
    $(element).empty();
    var t = sessionStorage[dtData.requestedFilterColumn];
    var filter = JSON.parse(t);
    var allOptions = filter.map(function (item) {
        return item['filterColumn'];
    });
    var unSelectedOptions = $(allOptions).not(selectedOptions).get();
    unSelectedOptions.sort();

    if (selectedOptions != null) {
        for (var i = 0; i < selectedOptions.length; i++) {
            $(element).append(new Option(selectedOptions[i], selectedOptions[i], true, true));
        }
    }

    for (var i = 0; i < unSelectedOptions.length; i++) {
        $(element).append(new Option(unSelectedOptions[i], unSelectedOptions[i], false, false));
    }
}

//get the dropdown on which user click & set its column name for further data processing
function setRequestedColumn(element) {

    if ($(element).hasClass("namedropdown")) {

        dtData.requestedFilterColumn = "name";
        requestedColumn = dtData.requestedFilterColumn;
        return dtData.requestedFilterColumn;
    }
    if ($(element).hasClass("addressdropdown")) {

        dtData.requestedFilterColumn = "address";
        requestedColumn = dtData.requestedFilterColumn;
        return dtData.requestedFilterColumn;
    }
}

//this function calls when we click on filter-dropdown and this function get data by ajax call from controller and stored in sessionStorage...
async function GetFiltersData(element) {

    $.ajax({
        url: "/Customers/GetFilters",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        async: true,
        data: JSON.stringify(dtData),
        beforeSend: function () {
            ddSelectedOptions = $(element).val();
        },
        success: function (json) {

            if (json.name != null) {
                sessionStorage["name"] = JSON.stringify(json.name);
            }

            if (json.address != null) {
                sessionStorage["address"] = JSON.stringify(json.address);
            }
        },
        complete: function (json) {
            $(element).select2("close");
            $(element).select2("open");

            $("#loading").hide();
        },

        error: function (error) {

        }

    });
};
