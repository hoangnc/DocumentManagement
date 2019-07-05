const formatDateVN = 'dd/mm/yyyy';
const enterKeyUp = $.Event("keyup", { keyCode: 13 });
let employees = [];
let selectCountry = { 'text': 'Viet Nam', 'id': 'VN' };
let departments = [];

const getAllEmployeesUrl = `${hostUrl}/api/employee/getall`;
const getAllDepartmentUrl = `${hostUrl}/api/department/getall`;
const createAirTicketUrl = `${hostUrl}/api/airticketapi/create`;
const getDepartmentByUserUrl = `${hostUrl}/api/department/getbyuser`;
const getPassportByUserAndDeparmentUrl = `${hostUrl}/api/passport/getPassportByUserAndDeparmentid`;

let passports = [
    {
        "id": 1,
        "select": false,
        "userCode": '',
        "fullName": '',
        "department": '',
        "departmentId": 0,
        "registrationDate": '',
        "expirationDate": '',
        "passportCode": '',
        "confirm": false
    },
    {
        "id": 2,
        "select": false,
        "userCode": '',
        "fullName": '',
        "department": '',
        "departmentId": 0,
        "registrationDate": '',
        "expirationDate": '',
        "passportCode": '',
        "confirm": false
    }
];

// Get masterdata
function getAllDepartments() {
    return $.getJSON(getAllDepartmentUrl,
        function (data) {
            departments = data;
        });
}

function getAllEmployees() {
    return $.getJSON(getAllEmployeesUrl,
        function (data) {
            employees = data;
        });
}

const cellbeginedit = function (row, datafield, columntype, value) {
    if (selectCountry && selectCountry.id === 'VN')
        return false;
};

const cellsrenderer = function (row, column, value, defaultHtml) {
    if (selectCountry && selectCountry.id === 'VN') {
        var element = $(defaultHtml);
        element.css('color', '#EEEEEE');
        return element[0].outerHTML;
    }
    return defaultHtml;
};

const cellclass = function (row, columnfield, value) {
    if (selectCountry && selectCountry.id === 'VN')
        return 'disable';
    return 'enable';
}

let observableArrayPassport = new $.jqx.observableArray(passports);

let passportSource =
{
    localdata: observableArrayPassport,
    datatype: "obserableArray",
    id: "id",
    updaterow: function (rowid, rowdata, commit) {
        // synchronize with the server - send update command
        // call commit with parameter true if the synchronization with the server is successful
        // and with parameter false if the synchronization failder.
        commit(true);
    },
    pagesize: 20,
    datafields:
        [
            { name: 'id', type: 'int' },
            { name: 'select', type: 'bool' },
            { name: 'userCode', type: 'string' },
            { name: 'fullName', type: 'string' },
            { name: 'department', type: 'string' },
            { name: 'departmentId', type: 'int' },
            { name: 'registrationDate', type: 'date' },
            { name: 'expirationDate', type: 'date' },
            { name: 'passportCode', type: 'string' },
            { name: 'confirm', type: 'bool' },
        ]
};

let dataPassportAdapter = new $.jqx.dataAdapter(passportSource);

function initGridPassport() {

    let employeesSource = {
        datatype: "array",
        datafields: [
            { name: 'fullName', type: 'string' },
            { name: 'fullNameUpper', type: 'string' },
            { name: 'departmentId', type: 'int' },
            { name: 'id', type: 'string' }
        ],
        localdata: employees
    };

    let employeesAdapter = new $.jqx.dataAdapter(employeesSource, {
        autoBind: true
    });

    let departmentSource =
    {
        localdata: departments,
        datatype: "array",
        id: "id",
        updaterow: function (rowid, rowdata, commit) {
            // synchronize with the server - send update command
            // call commit with parameter true if the synchronization with the server is successful
            // and with parameter false if the synchronization failder.
            commit(true);
        },
        pagesize: 20,
        datafields:
            [
                { name: 'shortName', type: 'string' },
                { name: 'code', type: 'string' },
                { name: 'id', type: 'int' },
            ]
    };

    let dataDepartmentAdapter = new $.jqx.dataAdapter(departmentSource, {
        autoBind: true
    });
    // initialize jqxGrid
    $("#gridPassport").jqxGrid(
        {
            width: '100%',
            height: 150,
            source: dataPassportAdapter,
            columnsresize: true,
            altrows: false,
            editable: true,
            localization: getLocalization('vi'),
            selectionmode: 'singlecell',
            columns: [
                {
                    text: 'Chọn',
                    columntype: 'checkbox',
                    datafield: 'select',
                    editable: true,
                    width: 80
                },
                {
                    text: '',
                    sortable: false,
                    filterable: false,
                    editable: false,
                    groupable: false,
                    draggable: false,
                    pinned: false,
                    datafield: '',
                    columntype: 'number',
                    width: 50,
                    cellsrenderer: function (row, column, value) {
                        return "<div style='margin:4px;'>" + (value + 1) + "</div>";
                    }
                },
                {
                    text: 'Họ và tên',
                    columntype: 'textbox',
                    displayField: 'fullName',
                    datafield: 'fullName',
                    width: 150,
                    createeditor: function (row, value, editor) {
                        editor.jqxInput({ placeHolder: "Nhập họ tên", height: 30, width: 250, minLength: 1, source: employees.map(e => e.fullNameUpper) });
                    },
                    validation: function (cell, value) {
                        var reg = new RegExp("^[-A-Z ]+$");
                        var test = reg.test(value);
                        if (test === false) {
                            return { result: false, message: "Họ tên là chữ in Hoa không dấu!" };
                        }
                        return true;
                    }
                },
                {
                    text: 'Phòng ban',
                    datafield: 'departmentId',
                    displayField: 'department',
                    columntype: 'dropdownlist',
                    width: 200,
                    createeditor: function (row, value, editor) {

                    },
                    initeditor: function (row, cellvalue, editor, celltext, pressedChar) {
                        const fullNameUpper = observableArrayPassport[row].fullName;
                        const employee = employees.find(e => e.fullNameUpper === fullNameUpper);
                        let departmentsForUser = [];
                        departmentsForUser.push({
                            "id": 9999,
                            "shortName": "Khách"
                        });
                        if (employee) {
                            if (employee.departmentId) {
                                const departmentOfUser = departments.find(e => e.id === employee.departmentId);
                                if (departmentOfUser) {
                                    departmentsForUser.push({
                                        "id": departmentOfUser.id,
                                        "shortName": departmentOfUser.shortName
                                    });
                                }
                            }
                        }
                        editor.jqxDropDownList({
                            source: departmentsForUser,
                            displayMember: 'shortName',
                            valueMember: 'id'
                        });
                    }
                },
                {
                    text: 'Ngày sinh',
                    datafield: 'registrationDate',
                    columntype: 'datetimeinput',
                    width: 110,
                    align: 'right',
                    cellsalign: 'right',
                    cellsformat: 'd',
                    cellbeginedit: cellbeginedit,
                    cellclassname: cellclass,
                    validation: function (cell, value) {
                        return true;
                    }
                },
                {
                    text: 'Số passport',
                    columntype: 'textbox',
                    datafield: 'passportCode',
                    editable: true,
                    cellbeginedit: cellbeginedit,
                    cellsrenderer: cellsrenderer,
                    cellclassname: cellclass,
                    width: 120
                },
                {
                    text: 'Ngày hết hạn',
                    datafield: 'expirationDate',
                    columntype: 'datetimeinput',
                    width: 110,
                    align: 'right',
                    cellsalign: 'right',
                    cellsformat: 'd',
                    cellbeginedit: cellbeginedit,
                    cellsrenderer: cellsrenderer,
                    cellclassname: cellclass,
                    validation: function (cell, value) {
                        return true;
                    }
                },
                {
                    text: 'Xác nhận',
                    columntype: 'checkbox',
                    datafield: 'confirm',
                    editable: true,
                    width: 80
                },
            ]
        });
    $("#gridPassport").on('cellendedit', function (event) {
        if (event.args.datafield === 'fullName') {
            var fullNameUpper = event.args.value;
            var employee = employees.find(e => e.fullNameUpper === fullNameUpper);
            let passport = {};
            if (employee) {
                let userCode = fullNameUpper;
                let departmentId = 9999;
                if (employee.departmentId) {
                    $.getJSON(`${getDepartmentByUserUrl}?fullName=${fullNameUpper}`,
                        function (data) {
                            departmentId = data.id;
                            observableArrayPassport.set(`${event.args.rowindex}.departmentId`, data.id);
                            observableArrayPassport.set(`${event.args.rowindex}.department`, data.shortName);
                            $.getJSON(`${getPassportByUserAndDeparmentUrl}?userCode=${userCode}&departmentId=${departmentId}`,
                                function (data) {
                                    passport = data;
                                    if (passport && passport.fullName && selectCountry.id !== 'VN') {
                                        observableArrayPassport.set(`${event.args.rowindex}.passportCode`, passport.passportCode);
                                        observableArrayPassport.set(`${event.args.rowindex}.registrationDate`, passport.registrationDate);
                                        observableArrayPassport.set(`${event.args.rowindex}.expirationDate`, passport.expirationDate);
                                    } else {
                                        observableArrayPassport.set(`${event.args.rowindex}.passportCode`, '');
                                        observableArrayPassport.set(`${event.args.rowindex}.registrationDate`, '');
                                        observableArrayPassport.set(`${event.args.rowindex}.expirationDate`, '');
                                    }
                                });
                        });
                }
                else {
                    observableArrayPassport.set(`${event.args.rowindex}.departmentId`, 9999);
                    observableArrayPassport.set(`${event.args.rowindex}.department`, 'Khách');
                    $.getJSON(`${getPassportByUserAndDeparmentUrl}?userCode=${userCode}&departmentId=${departmentId}`,
                        function (data) {
                            passport = data;
                            if (passport && selectCountry.id !== 'VN') {
                                observableArrayPassport.set(`${event.args.rowindex}.passportCode`, passport.passportCode);
                                observableArrayPassport.set(`${event.args.rowindex}.registrationDate`, passport.registrationDate);
                                observableArrayPassport.set(`${event.args.rowindex}.expirationDate`, passport.expirationDate);
                            } else {
                                observableArrayPassport.set(`${event.args.rowindex}.passportCode`, '');
                                observableArrayPassport.set(`${event.args.rowindex}.registrationDate`, '');
                                observableArrayPassport.set(`${event.args.rowindex}.expirationDate`, '');
                            }
                        });
                }
            }
        } else if (event.args.datafield === 'departmentId') {
            let departmentId = 9999;
            departmentId = event.args.value.value;
            var fullName = observableArrayPassport[event.args.rowindex].fullName;
            if (selectCountry.id !== 'VN') {
                $.getJSON(`${getPassportByUserAndDeparmentUrl}?userCode=${fullName}&departmentId=${departmentId}`,
                    function (data) {
                        passport = data;
                        if (passport) {
                            observableArrayPassport.set(`${event.args.rowindex}.passportCode`, passport.passportCode);
                            observableArrayPassport.set(`${event.args.rowindex}.registrationDate`, passport.registrationDate);
                            observableArrayPassport.set(`${event.args.rowindex}.expirationDate`, passport.expirationDate);
                        }
                    });
            } else {
                observableArrayPassport.set(`${event.args.rowindex}.passportCode`, '');
                observableArrayPassport.set(`${event.args.rowindex}.registrationDate`, '');
                observableArrayPassport.set(`${event.args.rowindex}.expirationDate`, '');
            }
        }
    });
}

function formatUserResult(user) {
    if (!user.id)
        return user.text;
    const text = user.text;
    const fullName = text;
    const department = user.department;
    return $(`<span>${fullName}</span><div><small style="color: #a5a0a0">Phòng ban: ${department}</small></div>`);
}

function formatUserSelection(user) {
    if (!user.id)
        return user.text;
    const text = user.text;
    return $(`<span>${text}</span>`);
}

$(document).ready(function () {
    Promise.all([getAllEmployees(),
    getAllDepartments(),
    ]).then(() => {
        $('#selectAssignedTo').select2({
            data: employees,
            templateResult: formatUserResult,
            templateSelection: formatUserSelection
        });
        initGridPassport();
        // all requests finished successfully
    }).catch(() => {
        // all requests finished but one or more failed
    })

    $('#selectCountry').select2({
        data: countries
    }).addClass('select2-offscreen').show();

    $("#selectCountry").select2().val('VN').trigger('change');

    $('#selectCountry').on('select2:select', function (e) {
        const country = e.params.data;
        if (country) {
            selectCountry = country;
        }
        $("#gridPassport").jqxGrid('updatebounddata', 'cells');
    });

    // From date
    $('#dpFromDate').datepicker({
        format: formatDateVN,
        showOn: 'focus',
        autoclose: true
    }).on("changeDate", function (e) {
        $(this).trigger(enterKeyUp);
        $(this).focus();
    });

    // To date
    $('#dpToDate').datepicker({
        format: formatDateVN,
        showOn: 'focus',
        autoclose: true
    }).on("changeDate", function (e) {
        $(this).trigger(enterKeyUp);
        $(this).focus();
    });

    $('#dpFromDate,#dpToDate').datepicker();

    /*$('#dpToTime, #dpFromTime').timepicker({
        showInputs: false,
       timeFormat: 'H:mm:ss'
    });
    $('#dpToTime, #dpFromTime').val('');*/
    $('#dpToTime, #dpFromTime').inputmask({
        mask: "h:s t\\m",
        placeholder: "hh:mm p",
        alias: "datetime",
        hourFormat: "24"
    });
    //Datemask dd/mm/yyyy
    $('#dpToDate').inputmask(formatDateVN, { 'placeholder': formatDateVN });
    $('#dpFromDate').inputmask(formatDateVN, { 'placeholder': formatDateVN });

    $.validate({
        form: '#formAirTicket',
        addValidClassOnAll: true
    });

    $('#formAirTicket').jqxValidator({
        //hintType: "label",
        position: "top",
        rules: [
            {
                input: '#dpToDate',
                message: 'Ngày về phải lớn hơn hoặc bằng ngày đi!',
                action: 'keyup, blur, valueChanged',
                rule: function (input, commit) {
                    // call commit with false, when you are doing server validation and you want to display a validation error on this field.

                    if (!isVariableHaveDefaltVal($('#dpToDate').val())) {
                        const fromDate = new Date($('#dpFromDate').datepicker('getDate'));
                        const toDate = new Date($('#dpToDate').datepicker('getDate'));
                        if (toDate >= fromDate) {
                            $("#dpFromDate").trigger("valueChanged");
                            return true;
                        }
                        return false;
                    }
                    return true;
                }
            },
            {
                input: '#dpFromDate',
                message: 'Ngày đi phải bé hơn hoặc bằng ngày về!',
                action: 'keyup, blur, valueChanged',
                rule: function (input, commit) {
                    // call commit with false, when you are doing server validation and you want to display a validation error on this field.
                    if (!isVariableHaveDefaltVal($('#dpToDate').val())) {
                        const fromDate = new Date($('#dpFromDate').datepicker('getDate'));
                        const toDate = new Date($('#dpToDate').datepicker('getDate'));
                        if (toDate < fromDate) {
                            $("#dpToDate").trigger("valueChanged");
                            return false;
                        }
                        return true;
                    }
                    return true;
                }
            }
        ]
    });

});

function findPassportSelected(arr, value) {
    return arr.filter(function (ele) {
        return ele.select != value;
    });
}

$(document).ready(function () {
    $('#btnAddPeople').click(function () {
        observableArrayPassport.push(
            {
                "id": 0,
                "select": false,
                "fullName": '',
                "department": '',
                "departmentId": 0,
                "registrationDate": '',
                "expirationDate": '',
                "passportCode": '',
                "confirm": false
            });
    });

    $('#btnRemovePeople').click(function () {
        for (let i = 0; i < observableArrayPassport.length; i++) {
            if (observableArrayPassport[i].select === true) {
                observableArrayPassport.splice(i, 1);
                i--;
            }
        }
    });

    $('#formAirTicket').submit(function (event) {
        event.preventDefault();
        if ($('#formAirTicket').jqxValidator('validate') === true) {
            var airTicket = getAirTicket();
            airTicket.passports = getPassports();
            let validates = validatePassports();
            if (validates.length > 0) {
                $.toast({
                    heading: 'Thông báo',
                    text: 'Bạn vui lòng kiểm tra và điền đầy đủ các thông tin về hành khách!',
                    position: 'top-right',
                    icon: 'error',
                    loader: true,        // Change it to false to disable loader
                    loaderBg: '#9EC600',  // To change the background,
                    hideAfter: 3000
                })
            } else {
                if (airTicket.passports.length === dataPassportAdapter.records.length) {
                    $.ajax({
                        url: createAirTicketUrl,
                        type: "POST",
                        data: JSON.stringify(airTicket),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (event, settings, xhr) {
                            xhr.message = {
                                infor: 'Đăng ký vé thành công',
                                returnUrl: returnAirTicketCreatedReturnUrl
                            };
                        }
                    });
                }
                else {
                    $.toast({
                        heading: 'Thông báo',
                        text: 'Bạn phải xác nhận thông tin hành khách',
                        position: 'top-right',
                        icon: 'error',
                        loader: true,        // Change it to false to disable loader
                        loaderBg: '#9EC600',  // To change the background,
                        hideAfter: 3000
                    })
                }
            }
            return;
        }
    });
});

function convertStrToDate(dateStr) {
    const [dd, mm, yyyy] = dateStr.split("/");
    return new Date(`${yyyy}-${mm}-${dd}`);
}

function convertStrToDateTime(dateStr, timeStr) {
    const [dd, mm, yyyy] = dateStr.split("/");
    return new Date(`${yyyy}-${mm}-${dd} ${timeStr}`);
}


function validatePassports() {
    let passports = [];
    for (let i = 0; i < dataPassportAdapter.records.length; i++) {
        let p = dataPassportAdapter.records[i];
        if (selectCountry.id !== 'VN') {
            if (isVariableHaveDefaltVal(p.fullName)
                || isVariableHaveDefaltVal(p.department)
                || isVariableHaveDefaltVal(p.registrationDate)
                || isVariableHaveDefaltVal(p.expirationDate)
                || isVariableHaveDefaltVal(p.passportCode)) {
                passports.push(p);
            }
        }
        else {
            if (isVariableHaveDefaltVal(p.fullName)
                || isVariableHaveDefaltVal(p.department)) {
                passports.push(p);
            }
        }
    }
    return passports;
}

function typeOfVar(obj) {
    return {}.toString.call(obj).split(' ')[1].slice(0, -1).toLowerCase();
}

function isVariableHaveDefaltVal(variable) {
    if (typeof (variable) === 'string') {  // number, boolean, string, object 
        console.log(' Any data Between single/double Quotes is treated as String ');
        return (variable.trim().length === 0) ? true : false;
    } else if (typeof (variable) === 'boolean') {
        console.log('boolean value with default value \'false\'');
        return (variable === false) ? true : false;
    } else if (typeof (variable) === 'undefined') {
        console.log('EX: var a; variable is created, but has the default value of undefined.');
        return true;
    } else if (typeof (variable) === 'number') {
        console.log('number : ' + variable);
        return (variable === 0) ? true : false;
    } else if (typeof (variable) === 'object') {
        //   -----Object-----
        if (typeOfVar(variable) === 'array' && variable.length === 0) {
            console.log('\t Object Array with length = ' + [].length); // Object.keys(variable)
            return true;
        } else if (typeOfVar(variable) === 'string' && variable.length === 0) {
            console.log('\t Object String with length = ' + variable.length);
            return true;
        } else if (typeOfVar(variable) === 'boolean') {
            console.log('\t Object Boolean = ' + variable);
            return (variable === false) ? true : false;
        } else if (typeOfVar(variable) === 'number') {
            console.log('\t Object Number = ' + variable);
            return (variable === 0) ? true : false;
        } else if (typeOfVar(variable) === 'regexp' && variable.source.trim().length === 0) {
            console.log('\t Object Regular Expression : ');
            return true;
        } else if (variable === null) {
            console.log('\t Object null value');
            return true;
        }
    }
    return false;
}

function getPassports() {
    let passports = dataPassportAdapter.records.filter(function (p) {
        return p.confirm === true;
    });
    return passports;
}

function getAirTicket() {

    const description = $('#txtDescription').val();
    const country = $('#selectCountry option:selected').val();
    const fromDate = $('#dpFromDate').val();
    const fromTime = $('#dpFromTime').val();
    const toTime = $('#dpToTime').val();
    const toDate = $('#dpToDate').val();
    const require = $('#txtRequire').val();
    const assignedTo = $('#selectAssignedTo').select2().val().join(',');

    let airTicket = {
        description: description,
        countryTo: country,
        fromDate: convertStrToDate(fromDate),
        toDate: convertStrToDate(toDate),
        assignedTo: assignedTo,
        airRequire: require,
        fromTime: fromTime,
        toTime: toTime
    };

    return airTicket;
}
