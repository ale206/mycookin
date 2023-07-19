function UsersLikesLoad(TypeOfLike, ContainerUserList, IDUserActionFather) {
    try {
        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/UserBoard/UsersLikes.asmx/UsersLikesList",
            data: "{ ActionType: '" + TypeOfLike + "', IDUserActionFather: '" + IDUserActionFather + "'}",
            success: function (msg) {
                $("#" + ContainerUserList).html('');

                if (msg.d.length > 0) {
                    $.each(msg.d, function () {
                        $("#" + ContainerUserList).append("<div style=\"margin:4px;\"><a href='/User/" + this['UserName'] + "'>" + this['Name'] + " " + this['Surname'] + "</a></div>");

                    });
                }
                else {
                    $("#" + ContainerUserList).html('&nbsp;');
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