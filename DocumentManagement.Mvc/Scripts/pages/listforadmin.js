const getAllEmployeesUrl = `${hostUrl}/api/employee/getall`;
const getAirTicketsByStatusUrl = `${hostUrl}/api/airticketdetail/getairticketsbystatus?ticketStatus=${ticketStatus}`;
const getAllAirTicketDetailsUrl = `${hostUrl}/api/airticketdetail/getall`;
const getAllAirTicketStatusUrl = `${hostUrl}/api/airticketstatus/getall`;
const listForAdminHref = `${hostUrl}/airticket/listforadmin`;
const sendMailUrl = `${hostUrl}/api/airticketdetail/sendmail`;
const airTicketFeedbackUrl = `${hostUrl}/airticket/feedback`;
const updateAirTicketByColumnUrl = `${hostUrl}/api/airticketdetail/UpdateByColumn`;
const cancelAirTicketsUrl = `${hostUrl}/api/airticketdetail/cancelairtickets`;
const importAirTicketDetailsUrl = `${hostUrl}/DataImport/ImportAirTicketsFromExcel`;

$(document).ready(function () {
    let employees = [];
    let airTickets = [];
    let airTicketStatus = [];

    // Get masterdata
    function getAllEmployees() {
        return $.getJSON(getAllEmployeesUrl,
            function (data) {
                employees = data;
            });
    }

    function getAllAirTickets() {
        if (ticketStatus > 0) {
            return $.getJSON(getAirTicketsByStatusUrl,
                function (data) {
                    airTickets = data;
                });
        }
        /*return $.ajax({
            url: getAllAirTicketDetailsUrl,
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                airTickets = data;
            }
        });*/
        return $.getJSON(getAllAirTicketDetailsUrl,
            function (data) {
                airTickets = data;
            });
    }

    function getAllAirTicketStatus() {
        return $.getJSON(getAllAirTicketStatusUrl,
            function (data) {
                airTicketStatus = data;
            });
    }
   
    Promise.all([getAllEmployees(),
    getAllAirTickets(),
    getAllAirTicketStatus(),
    ]).then(() => {
        initGrid();
        // all requests finished successfully
    }).catch((reason) => {
        // all requests finished but one or more failed
    })

    function initGrid() {

        let employeesSource = {
            datatype: "array",
            datafields: [
                { name: 'fullNameUpper', type: 'string' },
                { name: 'id', type: 'string' }
            ],
            localdata: employees
        };

        let employeesAdapter = new $.jqx.dataAdapter(employeesSource, {
            autoBind: true
        });

        let statusSource = {
            datatype: "array",
            datafields: [
                { name: 'status', type: 'string' },
                { name: 'id', type: 'int' }
            ],
            localdata: airTicketStatus
        };

        let statusAdapter = new $.jqx.dataAdapter(statusSource, {
            autoBind: true
        });

        let countriesSource =
        {
            datatype: "array",
            datafields: [
                { name: 'text', type: 'string' },
                { name: 'id', type: 'string' }
            ],
            localdata: countries
        };

        let countriesAdapter = new $.jqx.dataAdapter(countriesSource, {
            autoBind: true
        });

        let observableArrayTickets = new $.jqx.observableArray(airTickets);
        let source =
        {
            localdata: observableArrayTickets,
            datatype: "obserableArray",
            id: "id",
            updaterow: function (rowid, rowdata, commit) {
                // synchronize with the server - send update command
                // call commit with parameter true if the synchronization with the server is successful
                // and with parameter false if the synchronization failder.
                commit(true);
            },
            pagesize: 20,
            pagesizeoptions: ["20", "40", "60"],
            datafields:
                [
                    { name: 'id', type: 'int' },
                    { name: 'airTicketId', type: 'int' },
                    { name: 'airTicketNumber', type: 'string' },
                    { name: 'fullName', type: 'string' },
                    { name: 'country', type: 'string' },
                    { name: 'airRequire', type: 'string' },
                    { name: 'airTicketInform', type: 'string' },
                    { name: 'costUsd', type: 'number' },
                    { name: 'costVnd', type: 'number' },
                    { name: 'fromDate', type: 'date' },
                    { name: 'toDate', type: 'date' },
                    { name: 'fromTime', type: 'string' },
                    { name: 'toTime', type: 'string' },
                    { name: 'subscriber', type: 'string' },
                    { name: 'assignedToDisplay', type: 'string' },
                    { name: 'ticketDate', type: 'date' },
                    { name: 'departmentName', type: 'string' },
                    { name: 'status', type: 'string' },
                    { name: 'expirationTime', type: 'string' },
                    { name: 'expirationDate', type: 'date' },
                    { name: 'statusId', value: 'statusId', values: { source: statusAdapter.records, value: 'id', name: 'status' } },
                    { name: 'sendMail', type: 'bool' },
                    { name: 'countryCode', value: 'countryCode', values: { source: countriesAdapter.records, value: 'id', name: 'text' } },
                    { name: 'assignedTo', value: 'assignedTo', values: { source: employeesAdapter.records, value: 'id', name: 'fullNameUpper' } },
                ]
        };

        let dataAdapter = new $.jqx.dataAdapter(source);

        // initialize jqxGrid
        $("#grdAirTicket").jqxGrid(
            {
                width: '100%',
                height: 600,
                source: dataAdapter,
                pageable: true,
                autorowheight: true,
                columnsresize: true,
                showfilterrow: true,
                filterable: true,
                altrows: true,
                editable: true,
                pagesizeoptions: ["20", "40", "60"],
                localization: getLocalization('vi'),
                selectionmode: 'singlerow',
                editmode: 'singlecell',
                showstatusbar: true,
                renderstatusbar: function (statusbar) {
                    let cancelAirTicketsButton = $(`<button id="btnCancelAirTickets"
                            type="button"
                            class="btn btn-default pull-right margin btn-xs">
                            <i class="fa fa-trash-o"></i> Hủy
                            </button>`);

                    var sendMailButton = $(`<button id="btnSendMain"
                            type="button"
                            class="btn btn-success pull-right margin btn-xs">
                            <i class="fa fa-envelope-o"></i> Gửi mail
                            </button>`);
                    statusbar.append(cancelAirTicketsButton);
                    statusbar.append(sendMailButton);

                    cancelAirTicketsButton.click(function (event) {
                        let ids = [];
                        $.each(dataAdapter.records, function (indexInArray, row) {
                            if (row.sendMail === true) {
                                ids.push(row.id);
                            }
                        });

                        if (ids.length > 0) {
                            $.ajax({
                                url: cancelAirTicketsUrl,
                                type: "POST",
                                data: JSON.stringify({ ids: ids }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (event, settings, xhr) {
                                    xhr.message = {
                                        infor: 'Hủy thành công',
                                        returnUrl: listForAdminHref
                                    };
                                }
                            });
                        }
                    });

                    sendMailButton.click(function (event) {
                        // $("#grdAirTicket").jqxGrid({ selectionmode: 'checkbox' });
                        let ids = [];
                        $.each(dataAdapter.records, function (indexInArray, row) {
                            if (row.sendMail === true) {
                                ids.push(row.id);
                            }
                        });

                        if (ids.length > 0) {
                            $.ajax({
                                url: sendMailUrl,
                                type: "POST",
                                data: JSON.stringify(ids),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (event, settings, xhr) {
                                    xhr.message = {
                                        infor: 'Gởi mail thành công',
                                        returnUrl: listForAdminHref
                                    };
                                }
                            });
                        }
                    });
                },
                columns: [
                    {
                        text: 'STT',
                        sortable: false,
                        filterable: false,
                        editable: false,
                        groupable: false,
                        draggable: false,
                        pinned: true,
                        datafield: '',
                        columntype: 'number',
                        width: 30,
                        cellsrenderer: function (row, column, value) {
                            return "<div style='margin:4px;'>" + (value + 1) + "</div>";
                        }
                    },
                    {
                        text: 'Phản hồi',
                        columntype: 'textbox',
                        datafield: 'id',
                        pinned: true,
                        editable: false,
                        filterable: false,
                        width: 20,
                        cellsrenderer: function (row, column, value, defaultHtml) {
                            var element = $(defaultHtml);
                            element.html(`<a href="${airTicketFeedbackUrl}?ticketId=${dataAdapter.records[row].id}"> <i class="fa fa-pencil"></i> </a>`);
                            return element[0].outerHTML;
                        }
                    },
                    {
                        text: 'Mã số vé',
                        columntype: 'textbox',
                        pinned: true,
                        datafield: 'airTicketNumber',
                        width: 100
                    },
                    {
                        text: 'Họ và tên khách',
                        datafield: 'fullName',
                        columntype: 'textbox',
                        pinned: true,
                        width: 150,
                        createeditor: function (row, value, editor) {
                            editor.jqxInput({ placeHolder: "Nhập họ tên", height: 30, width: 250, minLength: 1, source: employees.map(e => e.fullNameUpper) });
                        },
                        validation: function (cell, value) {
                            const reg = new RegExp("^[-A-Z ]+$");
                            const test = reg.test(value);
                            if (test === false) {
                                return { result: false, message: "Họ tên là chữ in Hoa không dấu!" };
                            }
                            return true;
                        }
                    },
                    {
                        text: 'Quốc gia đến',
                        datafield: 'countryCode',
                        filterable: false,
                        displayField: 'country',
                        columntype: 'dropdownlist',
                        width: 100,
                        createeditor: function (row, value, editor) {
                            editor.jqxDropDownList({
                                source: countriesAdapter,
                                displayMember: 'text',
                                valueMember: 'id'
                            });
                        }
                    },
                    {
                        text: 'Yêu cầu vé',
                        datafield: 'airRequire',
                        columntype: 'template',
                        width: 200,
                        createeditor: function (row, cellvalue, editor, celltext, cellwidth, cellheight) {
                            let editorElement = $('<textarea id="airRequire' + row + '"></textarea>').prependTo(editor);
                            editorElement.jqxTextArea({
                                height: '100%',
                                width: '100%'
                            });
                        },
                        initeditor: function (row, cellvalue, editor, celltext, pressedChar) {
                            editor.find('textarea').val(cellvalue);
                            editor.find('textarea').on("keyup", function (e) {
                                const key = event.charCode ? event.charCode : event.keyCode ? event.keyCode : 0;
                                if (key === 13) {
                                    const pos = getCursorPos(this);
                                    const val = $(this).val();
                                    if (pos.start < val.length) {
                                        if (pos.start === pos.end) {
                                            let newVal = '';
                                            newVal = `${val.substring(0, pos.start)}\n${val.substring(pos.start, val.length)}`;
                                            $(this).val(newVal);
                                            setCursorPos(this, pos.start + 1);
                                        }
                                    } else if (pos.start === val.length) {
                                        $(this).val(`${val}\n`);
                                    }

                                }
                            });
                            editor.find('textarea').focus();
                        },
                        geteditorvalue: function (row, cellvalue, editor) {
                            return editor.find('textarea').val();
                        },
                        cellsrenderer: function (row, column, value, defaultHtml) {
                            let element = $(defaultHtml);
                            element.html('<p>' + value.replace('\n', '</br>') + '</p>');
                            return element[0].outerHTML;
                        }
                    },
                    {
                        text: 'Thông tin vé',
                        datafield: 'airTicketInform',
                        columntype: 'template',
                        width: 200,
                        createeditor: function (row, cellvalue, editor, celltext, cellwidth, cellheight) {
                            let editorElement = $('<textarea id="airTicketInform' + row + '"></textarea>').prependTo(editor);
                            editorElement.jqxTextArea({
                                height: '100%',
                                width: '100%'
                            });
                        },
                        initeditor: function (row, cellvalue, editor, celltext, pressedChar) {
                            editor.find('textarea').val(cellvalue);
                            editor.find('textarea').on("keyup", function (e) {
                                const key = event.charCode ? event.charCode : event.keyCode ? event.keyCode : 0;
                                if (key === 13) {
                                    const pos = getCursorPos(this);
                                    const val = $(this).val();
                                    if (pos.start < val.length) {
                                        if (pos.start === pos.end) {
                                            let newVal = '';
                                            newVal = `${val.substring(0, pos.start)}\n${val.substring(pos.start, val.length)}`;
                                            $(this).val(newVal);
                                            setCursorPos(this, pos.start + 1);
                                        }
                                    } else if (pos.start === val.length) {
                                        $(this).val(`${val}\n`);
                                    }

                                }
                            });
                            editor.find('textarea').focus();
                        },
                        geteditorvalue: function (row, cellvalue, editor) {
                            return editor.find('textarea').val();
                        },
                        cellsrenderer: function (row, column, value, defaultHtml) {
                            let element = $(defaultHtml);
                            element.html('<p>' + value.replace('\n', '</br>') + '</p>');
                            return element[0].outerHTML;
                        }
                    },
                    {
                        text: 'Chi phí USD',
                        datafield: 'costUsd',
                        width: 150, align: 'right',
                        cellsalign: 'right',
                        cellsformat: 'c2',
                        columntype: 'numberinput',
                        validation: function (cell, value) {
                            if (value < 0) {
                                return { result: false, message: "Số tiền phải lớn hơn hoặc bằng 0" };
                            }
                            return true;
                        },
                        createeditor: function (row, cellvalue, editor) {
                            editor.jqxNumberInput({
                                decimalDigits: 0,
                            });
                        }
                    },
                    {
                        text: 'Chi phí VNĐ',
                        datafield: 'costVnd',
                        width: 150,
                        align: 'center',
                        cellsalign: 'right',
                        columntype: 'numberinput',
                        cellsformat: 'c',
                        validation: function (cell, value) {
                            if (value < 0) {
                                return { result: false, message: "Số tiền phải lớn hơn hoặc bằng 0" };
                            }
                            return true;
                        },
                        createeditor: function (row, cellvalue, editor) {
                            editor.jqxNumberInput({
                                symbol: " đ",
                                symbolPosition: 'right',
                                decimalDigits: 0,
                                digits: 13
                            });
                        },
                        cellsrenderer: function (row, column, value, defaultHtml) {
                            let element = $(defaultHtml);
                            element.html(`<p>${value.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</p>`);
                            return element[0].outerHTML;
                        }
                    },
                    {
                        text: 'Ngày đi',
                        datafield: 'fromDate',
                        columntype: 'datetimeinput',
                        width: 110,
                        align: 'right',
                        cellsalign: 'right',
                        cellsformat: 'd',
                        validation: function (cell, value) {
                            const year = value.getFullYear();
                            if (year < 2019) {
                                return { result: false, message: "Ngày đi phải từ năm 2019" };
                            }
                            return true;
                        }
                    },
                    {
                        text: 'Giờ đi',
                        datafield: 'fromTime',
                        columntype: 'datetimeinput',
                        width: 110,
                        align: 'right',
                        cellsalign: 'right',
                        cellsformat: 't',
                        createeditor: function (row, value, editor) {
                            editor.jqxDateTimeInput({ formatString: 't', showTimeButton: true, showCalendarButton: false });
                        }
                    },
                    {
                        text: 'Ngày về',
                        datafield: 'toDate',
                        columntype: 'datetimeinput',
                        width: 110,
                        align: 'right',
                        cellsalign: 'right',
                        cellsformat: 'd',
                        validation: function (cell, value) {
                            const year = value.getFullYear();
                            if (year < 2019) {
                                return { result: false, message: "Ngày về phải từ năm 2019" };
                            }
                            return true;
                        }
                    },
                    {
                        text: 'Giờ về',
                        datafield: 'toTime',
                        columntype: 'datetimeinput',
                        width: 110,
                        align: 'right',
                        cellsalign: 'right',
                        cellsformat: 't',
                        createeditor: function (row, value, editor) {
                            editor.jqxDateTimeInput({ formatString: 't', showTimeButton: true, showCalendarButton: false });
                        }
                    },
                    {
                        text: 'Người đăng ký',
                        columntype: 'textbox',
                        datafield: 'subscriber', width: 120
                    },
                    {
                        text: 'Người liên quan',
                        datafield: 'assignedTo',
                        displayField: 'assignedToDisplay',
                        columntype: 'dropdownlist',
                        width: 200,
                        createeditor: function (row, value, editor) {
                            editor.jqxDropDownList({
                                source: employeesAdapter,
                                filterable: true,
                                checkboxes: true,
                                displayMember: 'fullNameUpper',
                                valueMember: 'id'
                            });
                        },
                        initeditor: function (row, cellvalue, editor, celltext, pressedkey) {
                            // set the editor's current value. The callback is called each time the editor is displayed.
                            const items = editor.jqxDropDownList('getItems');
                            const assignedTo = dataAdapter.records[row].assignedTo;
                            editor.jqxDropDownList('uncheckAll');
                            const values = assignedTo.split(/,\s*/);
                            for (let j = 0; j < values.length; j++) {
                                for (let i = 0; i < items.length; i++) {
                                    if (items[i].originalItem.id === values[j]
                                        || items[i].originalItem.fullNameUpper === values[j]) {
                                        editor.jqxDropDownList('checkIndex', i);
                                    }
                                }
                            }
                        },
                        geteditorvalue: function (row, cellvalue, editor) {
                            // return the editor's value.
                            dataAdapter.records[row].assignedToDisplay = editor.text();
                            return editor.val();
                        }
                    },
                    {
                        text: 'Hạn xuất vé(giờ)',
                        datafield: 'expirationTime',
                        columntype: 'datetimeinput',
                        width: 110,
                        align: 'right',
                        cellsalign: 'right',
                        cellsformat: 't',
                        createeditor: function (row, value, editor) {
                            editor.jqxDateTimeInput({ formatString: 't', showTimeButton: true, showCalendarButton: false });
                        }
                    },
                    {
                        text: 'Hạn xuất vé(ngày)',
                        datafield: 'expirationDate',
                        columntype: 'datetimeinput',
                        width: 110,
                        align: 'right',
                        cellsalign: 'right',
                        cellsformat: 'd',

                    },
                    {
                        text: 'Ngày xuất vé',
                        datafield: 'ticketDate',
                        columntype: 'datetimeinput',
                        width: 110,
                        align: 'right',
                        cellsalign: 'right',
                        cellsformat: 'd',
                        validation: function (cell, value) {
                            return true;
                        }
                    },
                    {
                        text: 'Tình trạng',
                        filterable: false,
                        datafield: 'statusId',
                        displayField: 'status',
                        columntype: 'dropdownlist',
                        width: 200,
                        createeditor: function (row, value, editor) {
                            editor.jqxDropDownList({
                                source: statusAdapter,
                                displayMember: 'status',
                                valueMember: 'id'
                            });
                        }
                    },
                    {
                        text: 'Gửi mail',
                        filterable: false,
                        datafield: 'sendMail',
                        columntype: 'checkbox',
                        editable: true,
                        width: 67
                    }
                ],
                handlekeyboardnavigation: function (event) {
                    const key = event.charCode ? event.charCode : event.keyCode ? event.keyCode : 0;
                    const element = $(event.target);

                    if (element.is("textarea")) {
                        if (key == 13) {
                            event.stopPropagation();
                            //const text = element.val();
                            //console.log(getCursorPos(element));
                            //element.val(text + '\n');
                            return true;
                        }
                    }
                    return false;
                },
            });

        $("#grdAirTicket").on('cellendedit', function (event) {
            const args = event.args;
            const datafield = event.args.datafield;
            if (datafield != 'sendMail') {

                const row = args.row;
                let data = {
                    id: row.id,
                    airTicketId: row.airTicketId,
                    columnName: getColumnName(datafield),
                    value: ''
                };

                if (typeof args.value === 'object' && args.value !== null) {
                    if (args.value instanceof Date) {
                        data.value = args.value.toLocaleString();
                    }
                    else {
                        data.value = args.value.value;
                    }
                } else {
                    data.value = args.value;
                }

                if (data.value !== row[`${datafield}`]) {
                    $.ajax({
                        cache: false,
                        url: updateAirTicketByColumnUrl,
                        type: "POST",
                        data: data,
                        dataType: "json",
                        success: function (data, status, xhr) {
                            // update command is executed.
                        },
                        error: function () {
                            // cancel changes.
                        }
                    });
                }
            }
        });
    }
});

function getCursorPos(input) {
    if ("selectionStart" in input && document.activeElement == input) {
        return {
            start: input.selectionStart,
            end: input.selectionEnd
        };
    }
    else if (input.createTextRange) {
        var sel = document.selection.createRange();
        if (sel.parentElement() === input) {
            var rng = input.createTextRange();
            rng.moveToBookmark(sel.getBookmark());
            for (var len = 0; rng.compareEndPoints("EndToStart", rng) > 0; rng.moveEnd("character", -1)) {
                len++;
            }
            rng.setEndPoint("StartToStart", input.createTextRange());
            for (var pos = { start: 0, end: len }; rng.compareEndPoints("EndToStart", rng) > 0; rng.moveEnd("character", -1)) {
                pos.start++;
                pos.end++;
            }
            return pos;
        }
    }
    return -1;
}

function setCursorPos(input, start, end) {
    if (arguments.length < 3) end = start;
    if ("selectionStart" in input) {
        setTimeout(function () {
            input.selectionStart = start;
            input.selectionEnd = end;
        }, 1);
    }
    else if (input.createTextRange) {
        var rng = input.createTextRange();
        rng.moveStart("character", start);
        rng.collapse();
        rng.moveEnd("character", end - start);
        rng.select();
    }
}

function getColumnName(dataField) {
    let columnName = ''
    switch (dataField) {
        case 'airTicketNumber':
            columnName = 'airTicketNumber';
            break;
        case 'fullName':
            columnName = 'fullName';
            break;
        case 'countryCode':
            columnName = 'countryTo';
            break;
        case 'airRequire':
            columnName = 'airRequire';
            break;
        case 'airTicketInform':
            columnName = 'airTicketInform';
            break;
        case 'costUsd':
            columnName = 'costUsd';
            break;
        case 'costVnd':
            columnName = 'costVnd';
            break;
        case 'fromDate':
            columnName = 'fromDate';
            break;
        case 'toDate':
            columnName = 'toDate';
            break;
        case 'ticketDate':
            columnName = 'ticketDate';
            break;
        case 'subscriber':
            columnName = 'subscriber';
            break;
        case 'assignedTo':
            columnName = 'assignedTo';
            break;
        case 'statusId':
            columnName = 'statusId';
            break;
        case 'expirationDate':
            columnName = 'expirationDate';
            break;
        case 'expirationTime':
            columnName = 'expirationTime';
            break;
        case 'fromTime':
            columnName = 'fromTime';
            break;
        case 'toTime':
            columnName = 'toTime';
            break;
    }
    return columnName;
}

$(document).ready(function () {
    const btnImport = $('#btnImport');
    const fileImport = $('#fileImport');
    const btnSubmitImport = $('#btnSubmitImport');

    fileImport.change(function () {
        btnSubmitImport.click();
        // Create an FormData object
        var formData = $("#formImport").submit(function (e) {
            return;
        });
        //formData[0] contain form data only
        // You can directly make object via using form id but it require all ajax operation inside $("form").submit(<!-- Ajax Here   -->)
        var formData = new FormData(formData[0]);
        $.ajax({
            url: importAirTicketDetailsUrl,
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            cache: false,
            success: function (event, settings, xhr) {
                console.log(xhr);
                if (xhr.status == 200) {
                    if (JSON.parse(xhr.responseJSON) > 0) {
                        xhr.message = {
                            infor: 'Import AirTickets thành công',
                            returnUrl: listForAdminHref
                        };
                    }
                    else {
                        $.toast({
                            heading: 'Thông báo',
                            text: 'Import không thành công bạn vui lòng kiểm tra lại data',
                            position: 'top-right',
                            icon: 'error',
                            loader: true,        // Change it to false to disable loader
                            loaderBg: '#9EC600',  // To change the background,
                            hideAfter: 3000
                        })
                    }
                } else {
                    $.toast({
                        heading: 'Thông báo',
                        text: 'Import không thành công bạn vui lòng kiểm tra lại data',
                        position: 'top-right',
                        icon: 'error',
                        loader: true,        // Change it to false to disable loader
                        loaderBg: '#9EC600',  // To change the background,
                        hideAfter: 3000
                    })
                }
            }
        });
    });

    btnImport.click(function () {
        fileImport.click();
    });
});
