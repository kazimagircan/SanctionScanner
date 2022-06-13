var sanctionScanner = {
    domain: 'https://localhost:44381',
    stepCodes: {
        1: "Awaiting approval",
        2: "Approved",
        3: "Rejected",
        4: "Payment pending",
        5: "Payment completed"
    },
    currentCost: null,
    totalCost: 0,
    calculateCost: function () {
        this.totalCost = parseFloat($('#price').val()) * parseFloat($('#quantity').val());
        if (this.totalCost > 0)
            $('#total').text(this.totalCost);
        else
            $('#total').text(0);

        if (sanctionScanner.currentCost !== null) {
            sanctionScanner.totalCost = parseFloat($('#' + sanctionScanner.currentCost.id + '-price').val()) * parseFloat($('#' + sanctionScanner.currentCost.id + '-quantity').val());
            if (sanctionScanner.totalCost > 0)
                $('#' + sanctionScanner.currentCost.id + '-total').text(sanctionScanner.totalCost);
            else
                $('#' + sanctionScanner.currentCost.id + '-total').text(0);
        }
    },
    listenInputs: function () {
        $('#quantity,#price').on('input', this.calculateCost);
    },
    showNestedRow: function (rowData) {
        this.currentCost = rowData;

        var editForm = '<div class="container">' +
            '<h2 class="text-center"> Edit Cost</h2>' +
            '<div class="form-group">' +
            '<input type = "text" class="form-control form-control-lg" id="' + rowData.id + '-description" placeholder="Enter description" name="description">' +
            '</div >' +
            '<div class="form-group"> ' +
            '<input type="number" class="form-control form-control-lg quantity" id="' + rowData.id + '-quantity" placeholder="Enter quantity" name="quantity">' +
            '</div> ' +
            '<div class="form-group"> ' +
            '<input type="number" class="form-control form-control-lg price" id="' + rowData.id + '-price" placeholder="Enter unit price" name="price">' +
            '</div> ' +
            '<div class="form-group"> ' +
            '<input type="date" class="form-control form-control-lg" id="' + rowData.id + '-date" name = "date">' +
            '</div >' +
            '<div class="form-group">' +
            '<label > Total:</label>' +
            '<label id="' + rowData.id + '-total" class="total"> </label>' +
            '</div> ' +
            '<div class="form-group"> ' +
            ' <button type="button" id="' + rowData.id + '" class="btn btn-primary create-cost-btn editCost" > Edit</button> ' +
            ' </div> ' +
            ' </div> ';

        return editForm;


    },
    showManagerNestedRow: function (rowData) {
        this.currentCost = rowData;

        var editForm = '<div class="container">' +
            '<h2 class="text-center"> Edit Cost</h2>' +
            '<div class="form-group">' +
            '<input type = "text" class="form-control form-control-lg" id="' + rowData.id + '-description" placeholder="Enter description" name="description" disabled>' +
            '</div >' +
            '<div class="form-group"> ' +
            '<input type="number" class="form-control form-control-lg quantity" id="' + rowData.id + '-quantity" placeholder="Enter quantity" name="quantity" disabled>' +
            '</div> ' +
            '<div class="form-group"> ' +
            '<input type="number" class="form-control form-control-lg price" id="' + rowData.id + '-price" placeholder="Enter unit price" name="price" disabled>' +
            '</div> ' +
            '<div class="form-group"> ' +
            '<input type="date" class="form-control form-control-lg" id="' + rowData.id + '-date" name = "date" disabled>' +
            '</div >' +
            '<div class="form-group"> ' +
            '<input type="text" class="form-control form-control-lg" id="' + rowData.id + '-rejectReason" placeholder="Enter reject reason" name="rejectReason">' +
            '</div >' +
            '<div class="form-group">' +
            '<label > Total:</label>' +
            '<label id="' + rowData.id + '-total" class="total"> </label>' +
            '</div> ' +
            '<div class="form-group"> ' +
            ' <button type="button" id="' + rowData.id + '" class="btn btn-primary create-cost-btn rejectCost" > Reject</button> ' +
            ' <button type="button" id="' + rowData.id + '" class="btn btn-primary create-cost-btn approvedCost" > Approve</button> ' +
            ' </div> ' +
            ' </div> ';

        return editForm;


    },
    showAccountantNestedRow: function (rowData) {
        this.currentCost = rowData;

        var editForm = '<div class="container">' +
            '<h2 class="text-center"> Edit Cost</h2>' +
            '<div class="form-group">' +
            '<input type = "text" class="form-control form-control-lg" id="' + rowData.id + '-description" placeholder="Enter description" name="description" disabled>' +
            '</div >' +
            '<div class="form-group"> ' +
            '<input type="number" class="form-control form-control-lg quantity" id="' + rowData.id + '-quantity" placeholder="Enter quantity" name="quantity" disabled>' +
            '</div> ' +
            '<div class="form-group"> ' +
            '<input type="number" class="form-control form-control-lg price" id="' + rowData.id + '-price" placeholder="Enter unit price" name="price" disabled>' +
            '</div> ' +
            '<div class="form-group"> ' +
            '<input type="date" class="form-control form-control-lg" id="' + rowData.id + '-date" name = "date" disabled>' +
            '</div >' +
            '<div class="form-group">' +
            '<label > Total:</label>' +
            '<label id="' + rowData.id + '-total" class="total"> </label>' +
            '</div> ' +
            '<div class="form-group"> ' +
            ' <button type="button" id="' + rowData.id + '" class="btn btn-primary create-cost-btn payCost" > Pay</button> ' +
            ' </div> ' +
            ' </div> ';

        return editForm;


    },

    initRowData: function () {

        $('#' + this.currentCost.id + '-description').val(this.currentCost.description);
        $('#' + this.currentCost.id + ' - date').val(this.currentCost.date);
        $('#' + this.currentCost.id + '-quantity').val(this.currentCost.quantity);
        $('#' + this.currentCost.id + '-price').val(this.currentCost.unitPrice);

        this.totalCost = parseFloat(this.currentCost.unitPrice) * parseFloat(this.currentCost.quantity);
        $('#' + this.currentCost.id + '-total').text(this.totalCost);

    },
    getRejectedCosts: function () {
        $.ajax({
            type: "GET",
            enctype: 'multipart/form-data',
            url: this.domain + "/User/GetRejectedCost",
            processData: false,
            contentType: false,
            cache: false,
            success: function (data) {

                var table = $('#rejectedCostList').DataTable({
                    "data": data,
                    language: {
                        search: "_INPUT_",
                        searchPlaceholder: "Search records"
                    },
                    "columns": [
                        {
                            "className": 'details-control',
                            "orderable": false,
                            "data": null,
                            "defaultContent": ''
                        },
                        { "data": "id" },
                        { "data": "description" },
                        { "data": "date" },
                        { "data": "quantity" },
                        { "data": "unitPrice" },
                        { "data": "total" },
                        {
                            "data": function (data) {
                                return sanctionScanner.stepCodes[data.status];

                            }
                        },
                        { "data": "rejectReason" },
                        { "data": "createdDate" },
                        { "data": "updatedDate" }

                    ],
                    "order": [[10, 'desc']]
                });
                $('#rejectedCostList tbody').unbind();
                $('#rejectedCostList tbody').on('click', 'td.details-control', function () {
                    var tr = $(this).closest('tr');
                    var row = table.row(tr);

                    if (row.child.isShown()) {
                        // This row is already open - close it
                        row.child.hide();
                        tr.removeClass('shown');
                        this.currentCost = null;
                    }
                    else {
                        // Open this row
                        row.child(sanctionScanner.showNestedRow(row.data())).show();
                        tr.addClass('shown');
                        $('.quantity,.price').on('input', sanctionScanner.calculateCost);
                        sanctionScanner.initRowData();

                    }
                });

            },
            error: function (e) {
                alert(e);
            }
        });
    },
    getAwaitingCosts: function () {
        $.ajax({
            type: "GET",
            enctype: 'multipart/form-data',
            url: this.domain + "/Manager/GetAwaitingCosts",
            processData: false,
            contentType: false,
            cache: false,
            success: function (data) {

                var table = $('#rejectedCostList').DataTable({
                    "data": data,
                    language: {
                        search: "_INPUT_",
                        searchPlaceholder: "Search records"
                    },
                    "columns": [
                        {
                            "className": 'details-control',
                            "orderable": false,
                            "data": null,
                            "defaultContent": ''
                        },
                        { "data": "id" },
                        { "data": "description" },
                        { "data": "date" },
                        { "data": "quantity" },
                        { "data": "unitPrice" },
                        { "data": "total" },
                        {
                            "data": function (data) {
                                return sanctionScanner.stepCodes[data.status];

                            }
                        },
                        { "data": "rejectReason" },
                        { "data": "createdDate" },
                        { "data": "updatedDate" }

                    ],
                    "order": [[10, 'desc']]
                });
                $('#rejectedCostList tbody').unbind();
                $('#rejectedCostList tbody').on('click', 'td.details-control', function () {
                    var tr = $(this).closest('tr');
                    var row = table.row(tr);

                    if (row.child.isShown()) {
                        // This row is already open - close it
                        row.child.hide();
                        tr.removeClass('shown');
                        this.currentCost = null;
                    }
                    else {
                        // Open this row
                        row.child(sanctionScanner.showManagerNestedRow(row.data())).show();
                        tr.addClass('shown');
                        sanctionScanner.initRowData();

                    }
                });

            },
            error: function (e) {
                alert(e);
            }
        });
    },
    getWaitingPaymentCosts: function () {
        $.ajax({
            type: "GET",
            enctype: 'multipart/form-data',
            url: this.domain + "/Accountant/GetWaitingPaymentCosts",
            processData: false,
            contentType: false,
            cache: false,
            success: function (data) {

                var table = $('#rejectedCostList').DataTable({
                    "data": data,
                    language: {
                        search: "_INPUT_",
                        searchPlaceholder: "Search records"
                    },
                    "columns": [
                        {
                            "className": 'details-control',
                            "orderable": false,
                            "data": null,
                            "defaultContent": ''
                        },
                        { "data": "id" },
                        { "data": "description" },
                        { "data": "date" },
                        { "data": "quantity" },
                        { "data": "unitPrice" },
                        { "data": "total" },
                        {
                            "data": function (data) {
                                return sanctionScanner.stepCodes[data.status];

                            }
                        },
                        { "data": "rejectReason" },
                        { "data": "createdDate" },
                        { "data": "updatedDate" }

                    ],
                    "order": [[10, 'desc']]
                });
                $('#rejectedCostList tbody').unbind();
                $('#rejectedCostList tbody').on('click', 'td.details-control', function () {
                    var tr = $(this).closest('tr');
                    var row = table.row(tr);

                    if (row.child.isShown()) {
                        // This row is already open - close it
                        row.child.hide();
                        tr.removeClass('shown');
                        this.currentCost = null;
                    }
                    else {
                        // Open this row
                        row.child(sanctionScanner.showAccountantNestedRow(row.data())).show();
                        tr.addClass('shown');
                        sanctionScanner.initRowData();

                    }
                });

            },
            error: function (e) {
                alert(e);
            }
        });
    },
    getCosts: function () {
        $.ajax({
            type: "GET",
            enctype: 'multipart/form-data',
            url: this.domain + "/User/GetCosts",
            processData: false,
            contentType: false,
            cache: false,
            success: function (data) {

                $('#costList').DataTable({
                    "data": data,
                    language: {
                        search: "_INPUT_",
                        searchPlaceholder: "Search records"
                    },
                    "columns": [
                        {
                            "className": '',
                            "orderable": false,
                            "data": null,
                            "defaultContent": ''
                        },
                        { "data": "id" },
                        { "data": "description" },
                        { "data": "date" },
                        { "data": "quantity" },
                        { "data": "unitPrice" },
                        { "data": "total" },
                        {
                            "data": function (data) {
                                return sanctionScanner.stepCodes[data.status]

                            }
                        },
                        { "data": "rejectReason" },
                        { "data": "createdDate" },
                        { "data": "updatedDate" }

                    ],
                    "order": [[10, 'desc']]
                });
            },
            error: function (e) {
                alert(e);
            }
        });
    },
    getManagerCosts: function () {
        $.ajax({
            type: "GET",
            enctype: 'multipart/form-data',
            url: this.domain + "/Manager/GetCosts",
            processData: false,
            contentType: false,
            cache: false,
            success: function (data) {

                var table = $('#costList').DataTable({
                    "data": data,
                    language: {
                        search: "_INPUT_",
                        searchPlaceholder: "Search records"
                    },
                    "columns": [
                        {
                            "className": '',
                            "orderable": false,
                            "data": null,
                            "defaultContent": ''
                        },
                        { "data": "id" },
                        { "data": "description" },
                        { "data": "date" },
                        { "data": "quantity" },
                        { "data": "unitPrice" },
                        { "data": "total" },
                        {
                            "data": function (data) {
                                if (data.status === 4) {
                                    data.status = 2;
                                }
                                return sanctionScanner.stepCodes[data.status];

                            }
                        },
                        { "data": "rejectReason" },
                        { "data": "createdDate" },
                        { "data": "updatedDate" }

                    ],
                    "order": [[10, 'desc']]
                });
            },
            error: function (e) {
                alert(e);
            }
        });
    },
    getAccountantCosts: function () {
        $.ajax({
            type: "GET",
            enctype: 'multipart/form-data',
            url: this.domain + "/Accountant/GetCosts",
            processData: false,
            contentType: false,
            cache: false,
            success: function (data) {

                var table = $('#costList').DataTable({
                    "data": data,
                    language: {
                        search: "_INPUT_",
                        searchPlaceholder: "Search records"
                    },
                    "columns": [
                        {
                            "className": '',
                            "orderable": false,
                            "data": null,
                            "defaultContent": ''
                        },
                        { "data": "id" },
                        { "data": "description" },
                        { "data": "date" },
                        { "data": "quantity" },
                        { "data": "unitPrice" },
                        { "data": "total" },
                        {
                            "data": function (data) {
                                return sanctionScanner.stepCodes[data.status];

                            }
                        },
                        { "data": "rejectReason" },
                        { "data": "createdDate" },
                        { "data": "updatedDate" }

                    ],
                    "order": [[10, 'desc']]
                });
            },
            error: function (e) {
                alert(e);
            }
        });
    },
    checkRoute: function () {
        var sPageURL, indexOfLastSlash, route;
        if (document.URL.indexOf("#") === -1) {
            sPageURL = window.location.href;
        }
        indexOfLastSlash = sPageURL.lastIndexOf("/");

        if (indexOfLastSlash > 0 && sPageURL.length - 1 !== indexOfLastSlash) {
            route = sPageURL.substring(indexOfLastSlash + 1);

        }
        if (sPageURL.includes('User')) {
            if (route === 'Cost') {
                this.getCosts();
            } else if (route === 'User' | route === 'Index') {
                this.getRejectedCosts();

            } else if (route === 'CreateCost') {
                this.createCost();

            }
        } else if (sPageURL.includes('Manager')) {
            if (route === 'Cost') {
                this.getManagerCosts();
            } else if (route === 'Manager' | route === 'Index') {
                this.getAwaitingCosts();
                this.rejectCost();
                this.approvedCost();

            }
        } else if (sPageURL.includes('Accountant')) {
            if (route === 'Cost') {
                this.getAccountantCosts();
            } else if (route === 'Accountant' | route === 'Index') {
                this.getWaitingPaymentCosts();
                this.payCost();

            }
        }


    },
    createCost: function () {
        var currentDomain = this.domain;
        $('#createCost').on('click', function () {

            var fd = new FormData();
            fd.append('Description', $('#description').val());
            fd.append('Date', $('#date').val());
            fd.append('Quantity', $('#quantity').val());
            fd.append('UnitPrice', $('#price').val());
            fd.append('Total', this.totalCost);

            $.ajax({
                type: "post",
                enctype: 'multipart/form-data',
                url: currentDomain + "/User/CreateCost",
                data: fd,
                processData: false,
                contentType: false,
                cache: false,
                success: function (data) {
                    console.log(data);
                    alert("The cost request sent succesfully");
                    window.location = currentDomain + "/User/Cost";


                },
                error: function (e) {
                    console.log(e);

                }
            });

        });
    },
    editCost: function () {
        var currentDomain = this.domain;

        $(document).on('click', ".editCost", function () {

            var fd = new FormData();
            fd.append('Description', $('#' + this.id + '-description').val());
            fd.append('Date', $('#' + this.id + ' - date').val());
            fd.append('Quantity', $('#' + this.id + '-quantity').val());
            fd.append('UnitPrice', $('#' + this.id + '-price').val());
            fd.append('Total', sanctionScanner.totalCost);
            fd.append('Id', sanctionScanner.currentCost.id);
            fd.append('UserId', sanctionScanner.currentCost.userId);
            fd.append('Status', 1);

            if (sanctionScanner.currentCost.rejectReason !== null)
                fd.append('RejectReason', sanctionScanner.currentCost.rejectReason);


            $.ajax({
                type: "post",
                enctype: 'multipart/form-data',
                url: currentDomain + "/User/EditCost",
                data: fd,
                processData: false,
                contentType: false,
                cache: false,
                success: function (data) {
                    console.log(data);
                    alert("The cost edited succesfully");
                    window.location = currentDomain + "/User/Cost";

                },
                error: function (e) {
                    console.log(e);

                }
            });
        });
    },
    rejectCost: function () {
        var currentDomain = this.domain;

        $(document).on('click', ".rejectCost", function () {

            var fd = new FormData();
            fd.append('Description', sanctionScanner.currentCost.description);
            fd.append('Date', sanctionScanner.currentCost.date);
            fd.append('Quantity', sanctionScanner.currentCost.quantity);
            fd.append('UnitPrice', sanctionScanner.currentCost.unitPrice);
            fd.append('Total', sanctionScanner.totalCost);
            fd.append('Id', sanctionScanner.currentCost.id);
            fd.append('UserId', sanctionScanner.currentCost.userId);
            fd.append('Status', 3);
            fd.append('RejectReason', $('#' + this.id + '-rejectReason').val());


            $.ajax({
                type: "post",
                enctype: 'multipart/form-data',
                url: currentDomain + "/Manager/EditCost",
                data: fd,
                processData: false,
                contentType: false,
                cache: false,
                success: function (data) {
                    console.log(data);
                    alert("The cost rejected succesfully");
                    window.location = currentDomain + "/Manager/Cost";

                },
                error: function (e) {
                    console.log(e);

                }
            });
        });
    },
    payCost: function () {
        var currentDomain = this.domain;

        $(document).on('click', ".payCost", function () {

            var fd = new FormData();
            fd.append('Description', sanctionScanner.currentCost.description);
            fd.append('Date', sanctionScanner.currentCost.date);
            fd.append('Quantity', sanctionScanner.currentCost.quantity);
            fd.append('UnitPrice', sanctionScanner.currentCost.unitPrice);
            fd.append('Total', sanctionScanner.totalCost);
            fd.append('Id', sanctionScanner.currentCost.id);
            fd.append('UserId', sanctionScanner.currentCost.userId);
            fd.append('Status', 5);
            if (sanctionScanner.currentCost.rejectReason !== null)
                fd.append('RejectReason', sanctionScanner.currentCost.rejectReason);

            $.ajax({
                type: "post",
                enctype: 'multipart/form-data',
                url: currentDomain + "/Accountant/EditCost",
                data: fd,
                processData: false,
                contentType: false,
                cache: false,
                success: function (data) {
                    console.log(data);
                    alert("The cost paid succesfully");
                    window.location = currentDomain + "/Accountant/Cost";

                },
                error: function (e) {
                    console.log(e);

                }
            });
        });
    },
    approvedCost: function () {
        var currentDomain = this.domain;

        $(document).on('click', ".approvedCost", function () {

            var fd = new FormData();
            fd.append('Description', sanctionScanner.currentCost.description);
            fd.append('Date', sanctionScanner.currentCost.date);
            fd.append('Quantity', sanctionScanner.currentCost.quantity);
            fd.append('UnitPrice', sanctionScanner.currentCost.unitPrice);
            fd.append('Total', sanctionScanner.totalCost);
            fd.append('Id', sanctionScanner.currentCost.id);
            fd.append('UserId', sanctionScanner.currentCost.userId);
            fd.append('Status', 4);
            if (sanctionScanner.currentCost.rejectReason !== null)
                fd.append('RejectReason', sanctionScanner.currentCost.rejectReason);

            $.ajax({
                type: "post",
                enctype: 'multipart/form-data',
                url: currentDomain + "/Manager/EditCost",
                data: fd,
                processData: false,
                contentType: false,
                cache: false,
                success: function (data) {
                    console.log(data);
                    alert("The cost approved succesfully");
                    window.location = currentDomain + "/Manager/Cost";

                },
                error: function (e) {
                    console.log(e);

                }
            });
        });
    },
    logout: function () {
        var currentDomain = this.domain;
        //if (window.location.href.includes('User')) {
        //    currentDomain = this.domain + "/User/Logout";
        //} else if (window.location.href.includes('Manager')) {
        //    currentDomain = this.domain + "/Manager/Logout";
        //} else if (window.location.href.includes('Accountant')) {
        //    currentDomain = this.domain + "/Accountant/Logout";
        //}
        $('.user-logout').on('click', function () {

            $.ajax({
                type: "post",
                enctype: 'multipart/form-data',
                url: currentDomain + "/User/Logout",
                data: null,
                processData: false,
                contentType: false,
                cache: false,
                success: function (data) {
                    console.log("User log out.");
                    window.location = currentDomain + "/Home/Login";
                },
                error: function (e) {
                    console.log(e);
                }
            });

        });

        $('.manager-logout').on('click', function () {

            $.ajax({
                type: "post",
                enctype: 'multipart/form-data',
                url: currentDomain + "/Manager/Logout",
                data: null,
                processData: false,
                contentType: false,
                cache: false,
                success: function (data) {
                    console.log("User log out.");
                    window.location = currentDomain + "/Home/Login";
                },
                error: function (e) {
                    console.log(e);
                }
            });

        });

        $('.accountant-logout').on('click', function () {

            $.ajax({
                type: "post",
                enctype: 'multipart/form-data',
                url: currentDomain + "/Accountant/Logout",
                data: null,
                processData: false,
                contentType: false,
                cache: false,
                success: function (data) {
                    console.log("User log out.");
                    window.location = currentDomain + "/Home/Login";
                },
                error: function (e) {
                    console.log(e);
                }
            });

        });
    },
    init: function () {
        this.checkRoute();
        this.listenInputs();
        this.editCost();
        this.logout();
    }
}
$(document).ready(function () {
    sanctionScanner.init();
});


