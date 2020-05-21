//This is here because is necessary in the masterpage as well

//Mark Messages as read
function MarkMessagesAsRead(IDUser) {
    try
    {
        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/Message/MessageManager.asmx/MarkAllConversationMessagesAsViewed",
            data: "{ IDUserConversationOwner: '" + IDUser + "'}",
            success: function (msg) {
                $("#lblCountMessages").html('');

                //Change CSS to remove box with notification number
                $("#lblCountMessages").removeClass("lblCountMessagesOn");
                $("#lblCountMessages").addClass("lblCountMessagesOff");
            },
            error: function (msg) {
                console.log(msg.status + ' ' + msg.statusText);
            }
        });
    }
    catch (err) {
    }
}