//FUNCTIONS
//***************************************
//***************************************

var startValue_email = "";

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

//Mandatory Security Answer
//*******************************
function isMandatorySecurityAnswer() {
    if ($('#txtSecurityAnswer').val() == "") {
        $('#lblMandatorySecurityAnswer').show();
        $('#lblMandatorySecurityAnswer').tipsy('show');

        return false;
    }
    else {
        $('#lblMandatorySecurityAnswer').tipsy('hide');
        $('#lblMandatorySecurityAnswer').hide();

        return true;
    }
}
//*******************************

$(document).ready(function () {

    //Get default value from textbox PlaceHolder
    //******************************************
    startValue_email = $('#txtEmail').val();
    //******************************************

    //Hide all Error labels
    //*********************
    $('#lblMandatoryEmail').hide();
    $('#lblCorrectEmail').hide();
    $('#lblMandatorySecurityAnswer').hide();
    //*********************

    //REALTIME ALERT
    //****************************************************

    //Show Mandatory Security Answer Tooltip Alert onchange text
    //************************************************
    $('#txtEmail').live('change', function (e) {
        isMandatoryEmail();
        isValidEmail();
    });
    //************************************************

    //Show Mandatory Email Tooltip Alert onchange text
    //************************************************
    $('#txtSecurityAnswer').live('change', function (e) {
        isMandatorySecurityAnswer();
    });

    //************************************************
    //BUTTONS CHECK
    //****************************************************
    //****************************************************

    $('#lbtnForgotPsw1').click(function () {
        //Continue if no errors
        if ((isMandatoryEmail()) && (isValidEmail())) {
            return true;
        }
        else {
            return false;
        }
    });

    $('#lbtnForgotPsw2').click(function () {
        //Continue if no errors - Note:Use Parenthesis!
        if ((isMandatorySecurityAnswer())) {
            return true;
        }
        else {
            return false;
        }
    });
    //****************************************************
    //****************************************************
});
