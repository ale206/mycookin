//FUNCTIONS
//***************************************
//***************************************

var startValue_email = "";
var startValue_password = "";

//Mandatory Email
//*******************************
function isMandatoryEmail() {
    if ($('#txtEmail').val() == "" || $('#txtEmail').val() == "Email") {
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
    if (checkEmail($('#txtEmail').val())) {
        $('#lblCorrectEmail').tipsy('hide');
        $('#lblCorrectEmail').hide();

        return true;
    }
    else {
        $('#lblCorrectEmail').show();
        $('#lblCorrectEmail').tipsy('show');

        return false;
    }
}

//Mandatory Password
//*******************************
function isMandatoryPassword() {
    if ($('#txtPsw').val() == "" || $('#txtPsw').val() == startValue_password) {
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

//***************************************
//***************************************

$(document).ready(function () {

    //Get default value from textbox PlaceHolder
    //******************************************
    startValue_email = $('#txtEmail').val();
    startValue_password = $('#txtPsw').val();
    //******************************************

    //Hide all Error labels
    //*********************
    $('#lblMandatoryEmail').hide();
    $('#lblCorrectEmail').hide();
    $('#lblMandatoryPassword').hide();
    //*********************

    //REALTIME ALERT
    //****************************************************

    //Show Mandatory Email Tooltip Alert onchange text
    //************************************************
    $('#txtEmail').live('change', function (e) {
        isMandatoryEmail();
        isValidEmail();
    });
    //************************************************

    //Show Mandatory Password Tooltip Alert onchange text
    //************************************************
    $('#txtPsw').live('change', function (e) {
        isMandatoryPassword();
    });
    //************************************************

    //****************************************************

    //BUTTON CHECK
    //****************************************************
    //****************************************************

    $('#lbtnUserLogin').click(function () {
        //Continue if no errors - Note:Use Parenthesis!
        if ((isMandatoryEmail()) && (isValidEmail()) && (isMandatoryPassword())) {
        //if ((isValidEmail()) && (isMandatoryPassword())) {
            return true;
        }
        else {
            return false;
        }
    });
    //****************************************************
    //****************************************************
});
