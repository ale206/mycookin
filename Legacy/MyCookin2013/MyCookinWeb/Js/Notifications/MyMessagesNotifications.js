//Global variable for the timer
var MessageNotificationTimer;

//Load Notifications List - Not Show messages here...
//function MessagesNotificationsLoad(ContainerMessageList, IDUser) {
function MessagesNotificationsLoad(IDUser) {
    //$("#" + ContainerMessageNotificationList).html('');

    $.ajax({
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "http://" + WebServicesPath + "/UserBoard/MessageNotifications.asmx/GetMessagesForUser",
        data: "{ IDUser: '" + IDUser + "'}",
        success: function (msg) {
            //$("#imgMessageLoader").hide();
            if (msg.d.length > 0) {

                var NumberOfMessagesDontRead = 0;

                $.each(msg.d, function () {

                    //Count notifications don't read
                    if (this['ViewedOn'] == null) {
                        NumberOfMessagesDontRead++;

                        //$("#" + ContainerMessageList).append("<p class=\"notification\">" + this['Message'] + "</p>");

                        //Show Noty if we have a new notification and mark as notified
                        
                        if (this['NotifiedOn'] == null) {
                            ShowMessageNotification(IDUser, this['IDMessageRecipient'], "New Message!");
                        }
                    }
                    //else {
                        //Add Particular class to read notifications....
                        //$("#" + ContainerMessageList).append("<p class=\"notificationRead\">" + this['Message'] + "</p>");
                    //}
                });

                $("#lblCountMessagesNotifications").html("(" + NumberOfMessagesDontRead + ")");
            }
            else {
                //var LabelContent = $("#lblNoMessages").html();
                //$("#lblNoMessages").html("<p class=\"notificationRead\">" + LabelContent + "</p>");
                //$("#lblNoMessages").show();
            }
        }
    });
}

//Mark Messages as read and empty the list
//function MarkMessagesAsRead(ContainerMessageList, IDUser) {
//    $.ajax({
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        type: "POST",
//        url: "http://" + WebServicesPath + "/UserBoard/MessageNotifications.asmx/MarkMessageAsViewed",
//        data: "{ IDUser: '" + IDUser + "'}",
//        success: function (msg) {
//            if (msg.d) {
//                $("#lblCountNotifications").html('(0)');
//            }
//        }
//    });
//}

//Show Noty and mark as notified
function ShowMessageNotification(IDUser, IDMessageRecipient, Message) {
    //alert(IDUserNotification);
    $.ajax({
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "http://" + WebServicesPath + "/UserBoard/MessageNotifications.asmx/MarkMessageAsNotified",
        data: "{ IDMessageRecipient: '" + IDMessageRecipient + "'}",
        success: function (msg) {
            if (msg.d) {
                noty({ text: Message });
            }
        }
    });
}