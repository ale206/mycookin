//Global variable for the timer
var CheckNewMessageCountTimer;

$(document).ready(function () {

    //Start Tooltip
    $(document).tooltip();

    //AutoSize for Textbox
    TextAreaAutoGrow('txtNewMessage');
    TextAreaAutoGrow('txtMessage');

    $("#pnlNewMessage").hide();
    $("#ReplyPanel").hide();
    $("#imgMessageContainerLoader").hide();
    $("#imgSendReplyLoader").hide();
    $('#imgSendNewMessageLoader').hide();

    $("#lblMandatoryField").hide(); 
    $("#lblMandatoryReplyField").hide();

    //Get User ID from the hidden field
    var IDUser = $('#hfIDUser').val();

    //Load List of Users with active conversations
    SenderListLoad("SenderList", IDUser);
});

function EmptyPanelWithMessages(ContainerMessagePanel) {
    $('#' + ContainerMessagePanel).html("");
}

//Load Senders List - People with messages (active conversations)
function SenderListLoad(ContainerSenderList, IDUser) {
    
    try
    {
        //Empty ContainerSenderList
        $("#" + ContainerSenderList + " ul ").html('');

        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/Message/MessageManager.asmx/GetListOfUsersConversations",
            data: "{ IDUser: '" + IDUser + "'}",
            success: function (msg) {

                $("#imgSenderListLoader").hide();

                if (msg.d.length > 0) {
                    var i = 1;
                    var numberOfMessages = msg.d.length;

                    $.each(msg.d, function () {
                        //This Call Return: IDUser, IDUserConversation, IDConversation, Name, Surname, UserIsOnLine

                        //Load messages of the first of the list
                        
                        if (i == 1) {
                            MarkMessagesAsRead(IDUser);
                            CheckNewMessagesActivateTimer();
                            ShowReplyPanel();
                            LoadMessages('MessageContainer', IDUser, this['IDUserRecipient'], this['IDConversation'], '0', $("#hfPageSize").val(), true);
                        }

                        i = i + 1;
                        

                        var imgOnline = "";

                        if (this['UserIsOnLine']) {
                            imgOnline = "<img src=\"/Images/icon-green-circle.png\" width=\"10\" /> ";
                        }
                        else {
                            imgOnline = "<img src=\"/Images/icon-empty-circle.png\" width=\"10\" /> ";
                        }

                        //Create List appending row to the ContainerSenderList div
                        $("#" + ContainerSenderList + " ul ").append(
                            "<li class=\"messagesUserList\">" +
                            "<a class=\"archiveConversationIcon\" href=\"#\" onclick=\" ArchiveConversation('" + this['IDUserConversation'] + "'); \"><img src=\"/Images/deleteX.png\" width=\"10\" /></a>" +
                            "" + imgOnline + "" +
                            "<a href=\"#\" " +
                                    "onclick=\" MarkMessagesAsRead('" + IDUser + "'); " +
                        //" $('#imgMessageContainerLoader').show();" +
                                              " CheckNewMessagesActivateTimer(); " +
                                              " ShowReplyPanel(); " +
                                              " LoadMessages('MessageContainer', '" + IDUser + "', '" + this['IDUserRecipient'] + "', '" + this['IDConversation'] + "', '0', '" + $("#hfPageSize").val() + "', true) " +
                                              " \">" + this['Name'] + " " + this['Surname'] + "</a>" +
                            "</li>"
                         );
                    });

                    //Add click function to list
                    $("#" + ContainerSenderList + " ul li").click(function () {
                        $("#imgMessageContainerLoader").show();

                    });

                }
            },
            error: function (msg) {
                console.log(msg.status + ' ' + msg.statusText);
            }
        });

        //Activate Scrollbar for the Sender List
        ActivateScroller('SenderList');
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

function ActivateScrollerWithCallback(DivId) {
    try
    {
        $("#" + DivId).mCustomScrollbar({
            autoHideScrollbar: true,
            advanced: { updateOnContentResize: true },
            callbacks: {
                onTotalScrollOffset: 150,        //When activate CallBack (pixels from bottom)
                onTotalScroll: function () {

                    //$('#imgMoreMessagesLoader').show();


                    //Get pagesize (number of message per page we want to show)
                    var PageSize = $('#hfPageSize').val();

                    //Get Current Offset
                    var CurrentPagingOffset = $('#hfCurrentPagingOffset').val();

                    //Get the number of messages for this conversation
                    var NumberOfMessages = $('#hfNumberOfMessages').val();

                    //Calculate Next Offset
                    var NextPagingOffset = parseInt(CurrentPagingOffset) + parseInt(PageSize);

                    //Check whether the Current Offset is less than Number of messages
                    //If is greater we finished our block of pages, either memorize in hfCurrentPagingOffset and load new messages
                    if (parseInt(CurrentPagingOffset) < parseInt(NumberOfMessages)) {

                        $('#hfCurrentPagingOffset').val(NextPagingOffset);

                        LoadMessages('MessageContainer', $('#hfIDUser').val(), '', $('#hfIDConversation').val(), NextPagingOffset, PageSize, false);
                    }
                    else {
                        //$('#imgMoreMessagesLoader').hide();
                    }
                }
            }
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
};

function LoadMessages(ContainerMessagePanel, IDUserConversationOwner, IDUserRecipient, IDConversation, Offset, PageSize, IsFirstShow) {
    try
    {
        //Store IDConversation
        $('#hfIDConversation').val(IDConversation);

        //First time IsFirstShow is set to true. Then set hfCurrentPagingOffset to 0. Possible updated value is stored by ActivateScrollerWithCallback()
        if (IsFirstShow) {
            $('#hfCurrentPagingOffset').val("0");

            //Store IDUserRecipient
            $('#hfRecipientID').val(IDUserRecipient);

            //Empty panel with messages
            EmptyPanelWithMessages(ContainerMessagePanel);
        }

        //Get the number of messages in this conversation. Then store the value in hfNumberOfMessages
        //*************************************************
        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/Message/MessageManager.asmx/GetNumberOfMessages",
            data: "{ IDUserConversationOwner: '" + IDUserConversationOwner + "', IDConversation: '" + IDConversation + "' }",
            success: function (msg) {
                //Store the number of messages
                $('#hfNumberOfMessages').val(msg.d);
            },
            error: function (msg) {
                console.log(msg.status + ' ' + msg.statusText);
            }
        });

        //*************************************************

        //Load Conversation Messages
        //*************************************************
        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/Message/MessageManager.asmx/ViewConversationPaged",
            data: "{ IDUserConversationOwner: '" + IDUserConversationOwner + "', IDConversation: '" + IDConversation + "', Offset: '" + Offset + "', PageSize: '" + PageSize + "'}",
            success: function (msg) {
                //This Call Return: CreatedOn, IDConversation, IDMessage, IDMessageRecipient, IDUserConversation, IDUserSender, Message, SentOn, Name, Surname

                $("#imgMessageContainerLoader").hide();
                $('#imgMoreMessagesLoader').hide();

                var DateTimeFormat = $('#hfDateFormat').val();
            
                if (msg.d.length > 0) {
                
                    $.each(msg.d, function () {

                        //Format Date
                        var dat = new Date(parseFloat(this['SentOn'].replace("/Date(", "").replace(")/", "")));
                        var dateFormatted = dateFormat(dat, DateTimeFormat);

                        if (IsFirstShow) {
                            //Append messages to the ContainerMessagePanel
                            $("#" + ContainerMessagePanel).append("<p><a class=\"messagesUser\" href=\"/User/UserProfile.aspx?IDUserRequested=" + this['IDUserSender'] + "\">" + this['Name'] + " " + this['Surname'] + "</a></p>" +
                                                                  "<p class=\"messagesDate\">" + dateFormatted + "</p>" +
                                                                  "<p class=\"messageText\">" + this['Message'] + "</p>" +
                                                                  "<p class=\"messageSeparator\">&nbsp;</p>" +
                                                                  "<p>&nbsp;</p>"
                                                             );
                        }
                        else {
                            //Append messages to the ContainerMessagePanel
                            $("#" + ContainerMessagePanel + " .mCSB_container").append(
                                                                  "<p><a href=\"/User/UserProfile.aspx?IDUserRequested=" + this['IDUserSender'] + "\">" + this['Name'] + " " + this['Surname'] + "</a></p>" +
                                                                  "<p class=\"messagesDate\">" + dateFormatted + "</p>" +
                                                                  "<p class=\"messageText\">" + this['Message'] + "</p>" +
                                                                  "<p class=\"messageSeparator\">&nbsp;</p>" +
                                                                  "<p>&nbsp;</p>"
                                                             );
                        }
                    });

                    //Load personal scrollbar with callback function when go to the end of scroll
                    if (IsFirstShow) {
                        ActivateScrollerWithCallback("MessageContainer");
                    }
                }
            },
            error: function (msg) {
                console.log(msg.status + ' ' + msg.statusText);
            }
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

function SendReplyMessage() {
    try
    {
        var IDUserSender = $('#hfIDUser').val();
        var RecipientsIDs = $('#hfRecipientID').val();
        var Message = $('#txtMessage').val();
        Message = Message.replace("'", "\\'");
        //console.log('qui: ' + Message);
        var IDLanguage = $('#hfIDLangage').val();

        //Controlla l'esistenza di tutti i campi obbligatori...
        if ((Message != "") && (RecipientsIDs != "")) {
            $.ajax({
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                type: "POST",
                url: "http://" + WebServicesPath + "/Message/MessageManager.asmx/SendNewMessage",
                data: "{ IDUserSender: '" + IDUserSender + "', RecipientsIDs: '" + RecipientsIDs + "', Message: '" + Message + "', IDLanguage: '" + IDLanguage + "'}",
                success: function (msg) {
                    $("#imgSendReplyLoader").hide();
                    $("#lblMandatoryReplyField").hide();
                    $("#txtMessage").val('');

                    //Empty panel with messages
                    EmptyPanelWithMessages('MessageContainer');

                    //Reload Messages as the first time - change it in future...
                    LoadMessages('MessageContainer', IDUserSender, RecipientsIDs, $("#hfIDConversation").val(), '0', $("#hfPageSize").val(), true);
                }
            });
        }
        else {
            //Recipient or message mandatory
            $("#lblMandatoryReplyField").show();
            $('#imgSendReplyLoader').hide();
        }
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

function SendNewMessage() {
    try
    {
        var IDUserSender = $('#hfIDUser').val();
        var RecipientsIDs = $("input[name$='txtObjectID']").val();
        var Message = $('#txtNewMessage').val();
        Message = Message.replace("'", "\\'");
        var IDLanguage = $('#hfIDLangage').val();

//        console.log("IDUserSender: " + IDUserSender);
//        console.log("RecipientsIDs: " + RecipientsIDs);
//        console.log("Message: " + Message);

        //Controlla l'esistenza di tutti i campi obbligatori...
        if ((Message != "") && (RecipientsIDs != "")) {
            $.ajax({
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                type: "POST",
                url: "http://" + WebServicesPath + "/Message/MessageManager.asmx/SendNewMessage",
                data: "{ IDUserSender: '" + IDUserSender + "', RecipientsIDs: '" + RecipientsIDs + "', Message: '" + Message + "', IDLanguage: '" + IDLanguage + "'}",
                success: function (msg) {
                    //This Call Return: CreatedOn, IDConversation, IDMessage, IDMessageRecipient, IDUserConversation, IDUserSender, Message, SentOn, Name, Surname

                    $('#imgSendNewMessageLoader').hide();
                    $("#lblMandatoryField").hide();

                    var IDConversation = "";

                    if (msg.d.length > 0) {

                        $.each(msg.d, function () {

                            IDConversation = this['IDConversation'];

                            //Empty TextBoxes
                            $("#txtNewMessage").val('');
                            $("input[name$='txtObjectName']").val('');

                            $("#pnlNewMessage").dialog('close');

                        });

                        //Empty panel with messages
                        EmptyPanelWithMessages('MessageContainer');

                        //Load List of Users with active conversations
                        SenderListLoad("SenderList", IDUserSender);

                        //Reload Messages as the first time - change it in future...
                        LoadMessages('MessageContainer', IDUserSender, RecipientsIDs, IDConversation, '0', $("#hfPageSize").val(), true)
                    }
                },
                error: function (msg) {
                    console.log(msg.status + ' ' + msg.statusText);
                }
            });
        }
        else {
            //Recipient or message mandatory
            $("#lblMandatoryField").show();
            $('#imgSendNewMessageLoader').hide();
           
        }
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

function ArchiveConversation(IDUserConversation) {
    try
    {
        var IDUser = $('#hfIDUser').val();

        //Controlla l'esistenza di tutti i campi obbligatori...
        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/Message/MessageManager.asmx/ArchiveConversation",
            data: "{ IDUserConversation: '" + IDUserConversation + "'}",
            success: function (msg) {
                $("#imgLoader").hide();

                //Empty panel with messages
                EmptyPanelWithMessages('MessageContainer');

                //Load List of Users with active conversations
                SenderListLoad("SenderList", IDUser);
            },
            error: function (msg) {
                console.log(msg.status + ' ' + msg.statusText);
            }
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

//Activate Timer to reload messages
function CheckNewMessagesActivateTimer() { 
    try
    {
        CheckNewMessageCountTimer = setInterval(function () { CheckNewMessages(); }, 5000);
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

function CheckNewMessages() {
    try
    {
        var IDUserSender = $('#hfRecipientID').val();
        var IDUserConversationOwner = $('#hfIDUser').val();

        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/Message/MessageManager.asmx/GetMessagesToReadByUser",
            data: "{ IDUserConversationOwner: '" + IDUserConversationOwner + "', IDUserSender: '" + IDUserSender + "'}",
            success: function (msg) {
                //This Call Return: IDMessage, IDUserSender, Message, SentOn, Name, Surname
                var DateTimeFormat = $('#hfDateFormat').val();

                if (msg.d.length > 0) {

                    //QUESTO IN FUTURO, QUANDO SI INVERTE L'ORDINE DEI MESSAGGI
                    //$.each(msg.d, function () {

                    //Format Date
                    //var dat = new Date(parseFloat(this['SentOn'].replace("/Date(", "").replace(")/", "")));
                    //var dateFormatted = dateFormat(dat, DateTimeFormat);

                    //Append messages to the ContainerMessagePanel 
                    //                    $("#" + ContainerMessagePanel + " .mCSB_container").append("<h2>" + dateFormatted + "</h2>" +
                    //                                                        "<p>" + this['Name'] + ": " + this['Message'] + "</p>" +
                    //                                                        "<p>&nbsp;</p>"
                    //                                                        );

                    //Empty panel with messages
                    EmptyPanelWithMessages('MessageContainer');

                    //Reload Messages as the first time - change it in future...
                    LoadMessages('MessageContainer', IDUserConversationOwner, IDUserSender, $("#hfIDConversation").val(), '0', $("#hfPageSize").val(), true);

                    //Mark Messages as read
                    MarkMessagesAsRead(IDUserConversationOwner);

                    //});
                }
            },
            error: function (msg) {
                console.log(msg.status + ' ' + msg.statusText);
            }
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

//Open Popup to write new mwssage
function OpenNewMessageDialog() {
    try
    {
        //Show BoxDialog
        $("#pnlNewMessage").show();

        $("#pnlNewMessage").dialog({
            resizable: false,
            draggable: false,
            width: 460,
            height: 285,
            position: { my: "center center", at: "center", of: window },
            //title: TitleText.toString(),
           // close: function () { },
            closeOnEscape: true,
            modal: true
    //        buttons: {
    //            Ok: function () {
    //                __doPostBack(ButtoCropID, "");
    //                $(this).dialog('close');
    //            }
    //        }
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
};

function ShowReplyPanel() {
    $("#ReplyPanel").show();
}