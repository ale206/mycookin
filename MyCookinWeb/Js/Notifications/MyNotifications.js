// Hello.
//
// This is JSHint, a tool that helps to detect errors and potential
// problems in your JavaScript code.
//
// To start, simply enter some JavaScript anywhere on this page. Your
// report will appear on the right side.
//
// Additionally, you can toggle specific options in the Configure
// menu.

//Global variable for the timer
var NotificationTimer;

//Global variable for the timer
var MessageCountTimer;

//Load Notifications List
function UsersNotificationsLoad(ContainerNotificationList, IDUser) {

    $("#" + ContainerNotificationList + " ul").html('');

    $.ajax({
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "http://" + WebServicesPath + "/UserBoard/UserNotifications.asmx/GetNotificationsForUser",
        data: "{ IDUser: '" + IDUser + "'}",
        success: function (msg) {
            $("#imgLoader").hide();
            if (msg.d.length > 0) {

                var NumberOfNotificationsDontRead = 0;

                $.each(msg.d, function () {

                    //Count notifications don't read
                    if (this['ViewedOn'] === null) {
                        NumberOfNotificationsDontRead++;

                        $("#" + ContainerNotificationList + " ul ").append("<li class=\"notification\">" + this['UserNotification'] + "</li>");

                        //Show Noty if we have a new notification and mark as notified

                        if (this['NotifiedOn'] === null) {
                            ShowNotification(IDUser, this['IDUserNotification'], this['UserNotification']);
                        }
                    }
                    else {
                        //Add Particular class to read notifications....
                        $("#" + ContainerNotificationList + " ul ").append("<li class=\"notificationRead\">" + this['UserNotification'] + "</li>");
                    }
                });

                if (NumberOfNotificationsDontRead > 0) {
                    $("#lblCountNotifications").html(NumberOfNotificationsDontRead);

                    //Change CSS to set icon On
                    $("#hlImageNotification").removeClass("hlImageNotification");
                    $("#hlImageNotification").addClass("hlImageNotificationOn");

                    //Change CSS to show number inside a box
                    $("#lblCountNotifications").removeClass("lblCountNotificationsOff");
                    $("#lblCountNotifications").addClass("lblCountNotificationsOn");

                    var CurrentDocTitle = document.title;
                    var n = CurrentDocTitle.indexOf(")");

                    //If we have not "()"
                    if (n == -1) {
                        document.title = "(" + msg.d.length + ") " + CurrentDocTitle;
                    }
                    else {
                        //We have already "()". Sum notifications
                        //console.log(parseInt($("#lblCountMessages").html()));

                        var numberOfNotifications = 0;

                        try {
                            numberOfNotifications = parseInt($("#lblCountMessages").html(), 10);

                            if (isNaN(numberOfNotifications)) {
                                numberOfNotifications = 0;
                            }
                        }
                        catch (err) {
                            numberOfNotifications = 0;
                        }

                        //var numberOfNotifications = isNaN($("#lblCountMessages").html()) ? $("#lblCountMessages").html() : 0;

                        var NewNumber = parseInt(numberOfNotifications, 10) + parseInt(msg.d.length, 10);

                        document.title = "(" + NewNumber + ") " + CurrentDocTitle.substring(n + 1, CurrentDocTitle.length);
                    }

                }
                else {
                    //Change CSS to remove box with notification number
                    $("#lblCountNotifications").removeClass("lblCountNotificationsOn");
                    $("#lblCountNotifications").addClass("lblCountNotificationsOff");

                    //                    var CurrentDocTitle = document.title;
                    //                    var n = CurrentDocTitle.indexOf(")");

                    if ((n > -1) && !isNaN($("#lblCountMessages").html())) {
                        document.title = CurrentDocTitle.substring(n + 1, CurrentDocTitle.length);
                    }
                }




            }
            else {
                //No notifications

                //                var LabelContent = $("#lblNoNotifications").html();
                //                $("#lblNoNotifications").html("<p class=\"notificationRead\">" + LabelContent + "</p>");
                //                $("#lblNoNotifications").show();




            }
        }
    });
}

//Mark Notifications as read and empty the list
function MarkNotificationsAsRead(ContainerNotificationList, IDUserOwnerRelatedObject) {
    $.ajax({
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "http://" + WebServicesPath + "/UserBoard/UserNotifications.asmx/MarkNotificationsAsRead",
        data: "{ IDUserOwnerRelatedObject: '" + IDUserOwnerRelatedObject + "'}",
        success: function (msg) {
            if (msg.d) {
                $("#lblCountNotifications").html('');

                //Change CSS to set icon Off
                $("#lblCountNotifications").removeClass("lblCountNotificationsOn");
                $("#lblCountNotifications").addClass("lblCountNotificationsOff");
            }
        }
    });
}

//Show Noty and mark as notified
function ShowNotification(IDUser, IDUserNotification, UserNotification) {
    //alert(IDUserNotification);
    $.ajax({
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "http://" + WebServicesPath + "/UserBoard/UserNotifications.asmx/MarkNotificationsAsNotified",
        data: "{ IDUser: '" + IDUser + "', IDUserNotification: '" + IDUserNotification + "'}",
        success: function (msg) {
            if (msg.d) {
                noty({ text: UserNotification });
            }
        }
    });
}

//This just Count Messages to read
function MessagesNotificationsLoad(IDUserConversationOwner) {
    $.ajax({
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "http://" + WebServicesPath + "/Message/MessageManager.asmx/GetMessagesToRead",
        data: "{ IDUserConversationOwner: '" + IDUserConversationOwner + "'}",
        success: function (msg) {
            if (msg.d.length > 0) {
                $("#lblCountMessages").html(msg.d.length);

                //Change CSS to set icon On
                $("#hlImageMessageNotification").removeClass("hlImageMessageNotification");
                $("#hlImageMessageNotification").addClass("hlImageMessageNotificationOn");

                //Change CSS to show number inside a box
                $("#lblCountMessages").removeClass("lblCountMessagesOff");
                $("#lblCountMessages").addClass("lblCountMessagesOn");

                var CurrentDocTitle = document.title;
                var n = CurrentDocTitle.indexOf(")");

                //If we have not "()"
                if (n == -1) {
                    document.title = "(" + msg.d.length + ") " + CurrentDocTitle;
                }
                else {
                    //We have already "()". Sum notifications
                    var numberOfNotifications = 0;

                    try {
                        numberOfNotifications = parseInt($("#lblCountNotifications").html(), 10);

                        if (isNaN(numberOfNotifications)) {
                            numberOfNotifications = 0;
                        }
                    }
                    catch (err) {
                        numberOfNotifications = 0;
                    }



                    //var numberOfNotifications = isNaN($("#lblCountNotifications").html()) ? $("#lblCountNotifications").html() : 0;
                    var NewNumber = parseInt(numberOfNotifications, 10) + parseInt(msg.d.length, 10);

                    document.title = "(" + NewNumber + ") " + CurrentDocTitle.substring(n + 1, CurrentDocTitle.length);
                }
            }
            else {
                $("#hlImageMessageNotification").removeClass("hlImageMessageNotificationOn");
                $("#hlImageMessageNotification").addClass("hlImageMessageNotification");

                //Change CSS to remove box with notification number
                $("#lblCountMessages").removeClass("lblCountMessagesOn");
                $("#lblCountMessages").addClass("lblCountMessagesOff");

                var CurrentDocTitle = document.title;
                var n = CurrentDocTitle.indexOf(")");

                if ((n > -1) && !isNaN($("#lblCountNotifications").html())) {
                    document.title = CurrentDocTitle.substring(n + 1, CurrentDocTitle.length);
                }
            }

        }
    });
}