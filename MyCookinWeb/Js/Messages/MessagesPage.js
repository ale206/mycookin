$(document).ready(function () {
    var IDUserRecipient = $('#hfIDUser').val();

    SenderListLoad("SenderList", IDUserRecipient);
})

//Load Notifications List
function SenderListLoad(ContainerSenderList, IDUserRecipient) {

    $("#" + ContainerSenderList).html('');

    $.ajax({
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "http://" + WebServicesPath + "/Message/MessageManager.asmx/GetListOfSenders",
        data: "{ IDUserRecipient: '" + IDUserRecipient + "'}",
        success: function (msg) {
            $("#imgLoader").hide();
            if (msg.d.length > 0) {
                
                $.each(msg.d, function () {
                    
                    //Add Tab
                    $("#" + ContainerSenderList).append("<li><a href=\"#"+ this['IDUser'] +"\">" + this['Name'] + " " + this['Surname'] + "</a></li>");
                    
                    //Add MessageContainer
                    AddMessageContainer("MessageContainer", this['IDUser'], IDUserRecipient)
                });

                //Vertical Tabs
                $(function () {
                    $("#pnlVerticalTab").tabs().addClass("ui-tabs-vertical ui-helper-clearfix");
                    $("#pnlVerticalTab li").removeClass("ui-corner-top").addClass("ui-corner-left");
                });
            }
            
        }
    });
}


function AddMessageContainer(ContainerPanel, IDUserSender, IDUserRecipient) {

    $("#" + ContainerPanel).append("<div id=\"" + IDUserSender + "\" ></div>");

    LoadMessages(IDUserSender, IDUserSender, IDUserRecipient);
    
}

function EmptyPanelWithMessages(ContainerMessagePanel) {
    $('#' + ContainerMessagePanel).html = "";
}

function LoadMessages(ContainerMessagePanel, IDUserSender, IDUserRecipient) {

    $.ajax({
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "http://" + WebServicesPath + "/Message/MessageManager.asmx/GetAllMessagesReceivedFromAUser",
        data: "{ IDUserRecipient: '" + IDUserRecipient + "', IDUserSender: '" + IDUserSender + "'}",
        success: function (msg) {
            $("#imgLoader").hide();

            var DateTimeFormat = $('#hfDateFormat').val();

            if (msg.d.length > 0) {

                $.each(msg.d, function () {

                    var dat = new Date(parseFloat(this['MessageSendDate'].replace("/Date(", "").replace(")/", "")));
                    //var dateFormatted = dat.getDate() + "/" + (dat.getMonth() + 1) + "/" + dat.getFullYear() + " " + dat.getHours() + ":" + dat.getMinutes() + ":" + dat.getSeconds();

                    var dateFormatted = dateFormat(dat, DateTimeFormat);
                    
                    $("#" + ContainerMessagePanel).append("<h2>" + dateFormatted + "</h2>" +
                                                          "<p>" + this['Message'] + "</p>"
                                                         );
                });
            }

            AddReplyPanel(IDUserSender, IDUserSender, IDUserRecipient);
        }
    });
}

function AddReplyPanel(ContainerMessagePanel, IDUserSender, IDUserRecipient) {

    $("#" + ContainerMessagePanel).append("<div>" +  
                                            "<input type=\"text\" class=\"ReplyTextBox\" />" +  
                                            "<input type=\"button\" value=\"Send\" />" +  
                                           "</div>"
                                           );
}


function MessageReply(IDUserSender, IDUserRecipient, Message) {
    $.ajax({
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "http://" + WebServicesPath + "/Message/MessageManager.asmx/MessageReply",
        data: "{ IDUserSender: '" + IDUserSender + "', IDUserRecipient: '" + IDUserRecipient + "', Message: '" + Message + "'}",
        success: function (msg) {
            $("#imgLoader").hide();

            EmptyPanelWithMessages(IDUserSender);

            LoadMessages(IDUserSender, IDUserSender, IDUserRecipient);

        }
    });
}