//FUNCTIONS
//***************************************
//***************************************

var startValue_password = "";

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
//***************************************

$(document).ready(function () {

    //Get default value from textbox PlaceHolder
    //******************************************
    startValue_password = $('#txtPassword').val();
    //******************************************

    //Hide all Error labels
    //*********************
    $('#lblMandatoryPassword').hide();
    $('#lblValidPassword').hide();
    //*********************

    //REALTIME ALERT
    //****************************************************

    //Show Mandatory Password Tooltip Alert onchange text
    //************************************************
    $('#txtPassword').live('change', function (e) {
        isMandatoryPassword();
        isValidPassword();
    });
    //************************************************

    //****************************************************

    //BUTTON CHECK
    //****************************************************
    //****************************************************

    $('#lbtnResetPsw').click(function () {
        //Continue if no errors
        if ((isMandatoryPassword()) && (isValidPassword())) {
            return true;
        }
        else {
            return false;
        }
    });
    //****************************************************
    //****************************************************
});
