$(document).ready(function () {

    $("#pnlSendResult").hide();
    $("#lblThanks").hide();
    $("#lblError").hide();
    $("#lblContactFormError").hide();
    $("#lblContactEmailError").hide();

    $('#imgSendNewContactRequestLoader').hide();
});

function SendNewContactRequest() {
    try {
        $("#lblContactEmailError").hide();

        var IDLanguage = $('#hfIDLangage').val();
        var FirstName = $('#txtFirstName').val();
        var LastName = $('#txtLastName').val();
        var Email = $('#txtEmail').val();
        var RequestText = $('#txtRequestText').val();
        var PrivacyAccept = $('#chkPrivacy:checked').val();
        var IpAddress = $('#hfIpAddress').val();
        var IDContactRequestType = $('#ddlContactRequestType').val();

//                console.log("IDLanguage: " + IDLanguage);
//                console.log("FirstName: " + FirstName);
//                console.log("LastName: " + LastName);
//                console.log("Email: " + Email);
//                console.log("RequestText: " + RequestText);
//                console.log("PrivacyAccept: " + PrivacyAccept);
//                console.log("IpAddress: " + IpAddress);
//                console.log("IDContactRequestType: " + IDContactRequestType);
//        console.log(checkEmail(Email));
        //Controlla l'esistenza di tutti i campi obbligatori...

        if (!checkEmail(Email)) {
            $('#imgSendNewContactRequestLoader').hide();
            $("#lblContactEmailError").show();

        }
        else {
            if ((FirstName != "") && (LastName != "") && (checkEmail(Email)) && (PrivacyAccept == "on")) {

                var PrivacyAccept = true;

                $.ajax({
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    url: "http://" + WebServicesPath + "/Contact/ContactManager.asmx/SendNewContactRequest",
                    data: "{ IDLanguage: '" + IDLanguage + "', FirstName: '" + FirstName + "', LastName: '" + LastName + "', Email: '" + Email
                                        + "', RequestText: '" + RequestText + "', PrivacyAccept: '" + PrivacyAccept + "', IpAddress: '" + IpAddress
                                        + "', IDContactRequestType: '" + IDContactRequestType + "'}",
                    success: function (msg) {
                        //This Call Return: CreatedOn, IDConversation, IDMessage, IDMessageRecipient, IDUserConversation, IDUserSender, Message, SentOn, Name, Surname

                        $('#imgSendNewContactRequestLoader').hide();

                        var IDConversation = "";

                        if (msg.d.length > 0) {

                            _IsError = this['_IsError'];

                            if (_IsError) {
                                //Show error

                                $("#pnlContactForm").hide();
                                $("#pnlSendResult").show();
                                $("#lblError").show();

                            }
                            else {
                                //Empty TextBoxes
                                $("#txtFirstName").val('');
                                $("#txtLastName").val('');
                                $("#txtEmail").val('');
                                $("#txtRequestText").val('');
                                $('#chkPrivacy:checked').attr('checked', false);

                                //Show thanks message
                                $("#lblContactFormError").hide();
                                $("#pnlContactForm").hide();
                                $("#pnlSendResult").show();
                                $("#lblThanks").show();

                            }

                            // $("#pnlContact").dialog('close');


                        }
                    },
                    error: function (msg) {
                        console.log("error: " + msg.status + " " + msg.statusText + " " + this['USPReturnValue']);

                        $("#pnlContactForm").show();
                        $("#pnlSendResult").show();
                        $('#imgSendNewContactRequestLoader').hide();
                        $("#lblError").show();

                    }
                });
            }
            else {
                $("#lblContactFormError").show();
                $('#imgSendNewContactRequestLoader').hide();
            }
        }
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}