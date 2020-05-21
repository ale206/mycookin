//FUNCTIONS
//***************************************
var startValue_name = "";
var startValue_surname = "";
var startValue_birthdate = "";
var startValue_username = "";
var startValue_email = "";
var startValue_password = "";

//TRANSLATIONS
//*******************************

var RegisterNamePlaceHolderLang = {
    '2': 'Nome',
    '3': 'Nombre',
    '1': 'Name',
    '4': 'Nome',
    '5': 'Name'
};

var RegisterSurnamePlaceHolderLang = {
    '2': 'Cognome',
    '3': 'Apellido',
    '1': 'Surname',
    '4': 'Nom de famille',
    '5': 'Nachnamen'
};

var RegisterBirthdatePlaceHolderLang = {
    '2': 'Data di Nascita',
    '3': 'Fecha de nacimiento',
    '1': 'Date of Birth',
    '4': 'Date de naissance',
    '5': 'Geburtsdatum'
};

function GetNameCorrectPlaceHolder(idLanguage) {
    RegisterNamePlaceHolder = RegisterNamePlaceHolderLang[idLanguage];
    if (RegisterNamePlaceHolder == "") {
        RegisterNamePlaceHolder = RegisterNamePlaceHolderLang['1'];
    }
    return RegisterNamePlaceHolder;
}

function GetSurnameCorrectPlaceHolder(idLanguage) {
    RegisterSurnamePlaceHolder = RegisterSurnamePlaceHolderLang[idLanguage];
    if (RegisterSurnamePlaceHolder == "") {
        RegisterSurnamePlaceHolder = RegisterSurnamePlaceHolderLang['1'];
    }
    return RegisterSurnamePlaceHolder;
}

function GetBirthdateCorrectPlaceHolder(idLanguage) {
    RegisterBirthdatePlaceHolder = RegisterBirthdatePlaceHolderLang[idLanguage];
    if (RegisterBirthdatePlaceHolder == "") {
        RegisterBirthdatePlaceHolder = RegisterBirthdatePlaceHolderLang['1'];
    }
    return RegisterBirthdatePlaceHolder;
}
//*******************************

//Mandatory Name
//*******************************
function isMandatoryName() {

    if ($('#txtName').val() == "" || $('#txtName').val() == startValue_name) {
        $('#lblMandatoryName').show();
        $('#lblMandatoryName').tipsy('show');

        return false;
    }
    else {
        $('#lblMandatoryName').tipsy('hide');
        $('#lblMandatoryName').hide();

        return true;
    }
}
//*******************************

//Valid Name
//*******************************
function isValidName() {

    if (!hasSpecialChar_v2($('#txtName').val())) {
        $('#lblValidName').show();
        $('#lblValidName').tipsy('show');

        return false;
    }
    else {
        $('#lblValidName').tipsy('hide');
        $('#lblValidName').hide();

        return true;
    }
}
//*******************************

//Mandatory Surname
//*******************************
function isMandatorySurname() {
    if ($('#txtSurname').val() == "" || $('#txtSurname').val() == startValue_surname) {
        $('#lblMandatorySurname').show();
        $('#lblMandatorySurname').tipsy('show');

        return false;
    }
    else {
        $('#lblMandatorySurname').tipsy('hide');
        $('#lblMandatorySurname').hide();

        return true;
    }
}
//*******************************

//Valid Surname
//*******************************
function isValidSurname() {
    if (!hasSpecialChar_v2($('#txtSurname').val())) {
        $('#lblValidSurname').show();
        $('#lblValidSurname').tipsy('show');

        return false;
    }
    else {
        $('#lblValidSurname').tipsy('hide');
        $('#lblValidSurname').hide();

        return true;
    }
}
//*******************************

//Mandatory Birthdate
//*******************************
function isMandatoryBirthdate() {
    if ($('#txtBirthdate').val() == "" || $('#txtBirthdate').val() == startValue_birthdate) {
        $('#lblMandatoryBirthdate').show();
        $('#lblMandatoryBirthdate').tipsy('show');

        return false;
    }
    else {
        $('#lblMandatoryBirthdate').tipsy('hide');
        $('#lblMandatoryBirthdate').hide();

        return true;
    }
}
//*******************************

//Valid Birthdate
//*******************************
function isValidBirthdate() {
    return true;
}
//*******************************

//Mandatory Username
//*******************************
function isMandatoryUsername() {
    if ($('#txtUsername').val() == "" || $('#txtUsername').val() == startValue_username) {
        $('#lblMandatoryUsername').show();
        $('#lblMandatoryUsername').tipsy('show');

        return false;
    }
    else {
        $('#lblMandatoryUsername').tipsy('hide');
        $('#lblMandatoryUsername').hide();

        return true;
    }
}
//*******************************

//Valid Username
//*******************************
function isValidUsername() {
    if (!hasSpecialChar_v1($('#txtUsername').val())) {
        $('#lblValidUsername').show();
        $('#lblValidUsername').tipsy('show');

        return false;
    }
    else {
        $('#lblValidUsername').tipsy('hide');
        $('#lblValidUsername').hide();

        return true;
    }
}
//*******************************

//Existence Username
//*******************************
function checkExistenceUsername() {
    if ($('#txtCheckUsername').val() != "available") {

        //This is in Ajax Check (Register.aspx)
        //$('#lblExistenceUsername').show();
        //$('#lblExistenceUsername').tipsy('show');

        return false;
    }
    else {

        //This is in Ajax Check (Register.aspx)
        //$('#lblExistenceUsername').tipsy('hide');
        //$('#lblExistenceUsername').hide();

        return true;
    }
}
//*******************************

//Mandatory Email
//*******************************
function isMandatoryEmail() {
    if ($('#txtEmail').val() == "" || $('#txtEmail').val() == startValue_email) {
        $('#lblMandatoryEmail').show();
        $('#lblMandatoryEmail').tipsy('show');

        return false;
    }
    else {
        $('#lblMandatoryEmail').tipsy('hide');
        $('#lblMandatoryEmail').hide();

        return true;
    }
}
//*******************************

//Valid Email
//*******************************
function isValidEmail() {
    if (!checkEmail($('#txtEmail').val())) {

        $('#lblValidEmail').show();
        $('#lblValidEmail').tipsy('show');

        return false;
    }
    else {
        $('#lblValidEmail').tipsy('hide');
        $('#lblValidEmail').hide();

        return true;
    }
}
//*******************************

//Existence Email
//*******************************
function checkExistenceEmail() {
    if ($('#txtCheckEmail').val() != "available") {

        //This is in Ajax Check (Register.aspx)
        //$('#lblExistenceEmail').show();
        //$('#lblExistenceEmail').tipsy('show');

        return false;
    }
    else {
        //This is in Ajax Check (Register.aspx)
        //$('#lblExistenceEmail').tipsy('hide');
        //$('#lblExistenceEmail').hide();

        return true;
    }
}
//*******************************

//Mandatory Password
//*******************************
function isMandatoryPassword() {
    if ($('#txtPassword').val() == "" || $('#txtPassword').val() == startValue_password) {
        $('#lblMandatoryPassword').show();
        $('#lblMandatoryPassword').tipsy('show');

        return false;
    }
    else {
        $('#lblMandatoryPassword').tipsy('hide');
        $('#lblMandatoryPassword').hide();

        return true;
    }
}
//*******************************

//Valid Password
//*******************************
function isValidPassword() {
    if ($('#txtPassword').val().length < 6) {
        $('#lblValidPassword').show();
        $('#lblValidPassword').tipsy('show');

        return false;
    }
    else {
        $('#lblValidPassword').tipsy('hide');
        $('#lblValidPassword').hide();

        return true;
    }
}
//*******************************

//***************************************



$(document).ready(function () {

    $("#imgRegisterListLoader").hide();

    $('#txtName').placeholder('nome');

    //Get default value from textbox PlaceHolder
    //******************************************
    startValue_name = $('#txtName').val();
    startValue_surname = $('#txtSurname').val();
    startValue_birthdate = $('#txtBirthdate').val();
    startValue_username = $('#txtUsername').val();
    startValue_email = $('#txtEmail').val();
    startValue_password = $('#txtPassword').val();
    //******************************************

    //Hide all Error labels
    //*********************
    $('#lblMandatoryName').hide();
    $('#lblValidName').hide();
    $('#lblMandatorySurname').hide();
    $('#lblValidSurname').hide();
    $('#lblMandatoryBirthdate').hide();
    $('#lblValidBirthdate').hide();
    $('#lblMandatoryUsername').hide();
    $('#lblValidUsername').hide();
    $('#lblExistenceUsername').hide();
    $('#lblMandatoryEmail').hide();
    $('#lblValidEmail').hide();
    $('#lblExistenceEmail').hide();
    $('#lblMandatoryPassword').hide();
    $('#lblValidPassword').hide();
    //*********************

    //REALTIME ALERT
    //****************************************************
    //****************************************************

    //Show Mandatory/Valid Name Tooltip Alert onchange text
    //************************************************
    $('#txtName').live('change', function (e) {
        isMandatoryName();
        isValidName();
    })
    //************************************************

    //Show Mandatory/Valid Surname Tooltip Alert onchange text
    //************************************************
    $('#txtSurname').live('change', function (e) {
        isMandatorySurname();
        isValidSurname();
    })
    //************************************************

    //Show Mandatory/Valid Birthdate Tooltip Alert onchange text
    //************************************************
    $('#txtBirthdate').live('change', function (e) {
        isMandatoryBirthdate();
        isValidBirthdate();
    })
    //************************************************

    //Show Mandatory/Valid/Existence Username Tooltip Alert onchange text
    //************************************************
    $('#txtUsername').live('change', function (e) {
        isMandatoryUsername();
        isValidUsername();
        checkExistenceUsername();
    })
    //************************************************

    //Show Mandatory/Valid/Existence Email Tooltip Alert onchange text
    //************************************************
    $('#txtEmail').live('change', function (e) {
        isMandatoryEmail();
        isValidEmail();
        checkExistenceEmail();
    })
    //************************************************

    //Show Mandatory/Valid Password Tooltip Alert onchange text
    //************************************************
    $('#txtPassword').live('change', function (e) {
        isMandatoryPassword();
        isValidPassword();
    })
    //************************************************

    //****************************************************
    //****************************************************

    //BUTTON CHECK
    //****************************************************
    //****************************************************
    $('#lbtnRegister').click(function () {
        //Continue if no errors - Note:Use Parenthesis!
        if ((isMandatoryName()) && (isValidName()) && (isMandatorySurname()) && (isValidSurname()) && (isMandatoryBirthdate()) && (isValidBirthdate())
                                && (isMandatoryUsername()) && (isValidUsername()) && (checkExistenceUsername()) && (isMandatoryEmail()) && (isValidEmail())
                                && (checkExistenceEmail()) && (isMandatoryPassword()) && (isValidPassword())) {

            $("#imgRegisterListLoader").show();

            return true;
        }
        else {
            return false;
        }
    });
    //****************************************************
    //****************************************************

});


