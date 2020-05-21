function LoadSimilarRecipes(pnlRecipeName, pnlRecipesLoadingName) {
    try {
        var IDRecipe = $('#hfIDRecipe').val();
        var RecipeName = $('#hfRecipeName').val();
        var Vegan = $('#hfVegan').val();
        var Vegetarian = $('#hfVegetarian').val();
        var GlutenFree = $('#hfGlutenFree').val();
        var IDLanguage = $('#hfIDLanguage').val();
        var _dataToAppend = "";
        
        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/Recipe/GetRecipesByType.asmx/GetSimilarRecipesListHTML",
            data: "{ RecipeName: '" + RecipeName + "', IDRecipe: '" + IDRecipe + "', Vegan: '" + Vegan + "', Vegetarian: '" + Vegetarian + "', GlutenFree: '" + GlutenFree + "', IDLanguage: '" + IDLanguage + "'}",
            success: function (msg) {
                $('#' + pnlRecipeName).html("");
                $.each(msg.d, function () {
                    $('#' + pnlRecipeName).append(this.replace("{RecipeOf}", $('#hfRecipeOf').val()).replace("{RecipeOf2}", $('#hfRecipeOf2').val()));
                });
                $('#' + pnlRecipesLoadingName).css({ 'display': 'none' });
            }
        });
    }
    catch (err) {

    }
}
function LoadUsers(pnlUsersName, pnlUsersLoadingName) {
    try {
        var IDUser = $('#hfIDUser').val();
        var RowOffset = 0;
        var FetchRows = 4;
        var _dataToAppend = "";

        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/User/FindUser.asmx/GetUsersSuggestionListHTML",
            data: "{ IDRequester: '" + IDUser + "', RowOffset: '" + RowOffset + "', FetchRows: '" + FetchRows + "'}",
            success: function (msg) {
                $('#' + pnlUsersName).html("");
                $.each(msg.d, function () {
                    $('#' + pnlUsersName).append(this);
                });
                $('#' + pnlUsersLoadingName).css({ 'display': 'none' });
            }
        });
    }
    catch (err) {

    }
}
function LikeUnLikeRecipe(imgId) {
    try {
        var IDUser = $('#hfIDUser').val();
        var IDRecipe = $('#hfIDRecipe').val();
        var IDRecipeOwner = $('#hfIDRecipeOwner').val();
        var IDLanguage = $('#hfIDLanguage').val();
        var Username = $('#hfCurrentUsername').val();
        var RecipeURL = $('#hfCurrentRecipeUrl').val();

        if ($("#" + imgId).attr('src').indexOf('-off') > -1) {
            $("#" + imgId).attr('src', $("#" + imgId).attr('src').replace("-off", "-on"));
        }
        else
        {
            $("#" + imgId).attr('src', $("#" + imgId).attr('src').replace("-on", "-off"));
        }
        $("#" + imgId).attr("disabled", "disabled");
        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/Recipe/RecipeFeedbacks.asmx/LikeRecipe",
            data: "{ IDRecipe: '" + IDRecipe + "',IDUser: '" + IDUser + "',IDRecipeOwner: '" + IDRecipeOwner + "', IDLanguage: '" + IDLanguage + "', Username: '" + Username + "', RecipeURL: '" + RecipeURL + "'}",
            success: function (msg) {
                if (msg.d.indexOf('--like--') > -1) {
                    $("#" + imgId).attr('src', $("#" + imgId).attr('src').replace("-off", "-on"));
                }
                if (msg.d.indexOf('--unlike--') > -1) {
                    $("#" + imgId).attr('src', $("#" + imgId).attr('src').replace("-on", "-off"));
                }
                if (msg.d.indexOf('error') > -1) {
                    console.log(msg.d);
                }
                $("#" + imgId).removeAttr("disabled");
                RecipeLikesDeteils('pnlLikeDetails', 'pnlLikeDetailsInt', 'lnkLikesNumber');
            }
        });
    }
    catch (err) {

    }
}
function RecipeLikesDeteils(pnlLikeDetail, pnlLikeDetailInternal, lnkLikeDetails) {
    try {
        var IDRecipe = $('#hfIDRecipe').val();
        var RowOffset = 0;
        var FetchRows = 50;
        var hfLikeDetailBaseText = $('#hfLikeDetailBaseText').val();
        var _dataToAppend = "";
        $('#' + lnkLikeDetails).removeAttr("href");
        $('#' + lnkLikeDetails).unbind( "click" );
        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/Recipe/RecipeFeedbacks.asmx/LikesDetailsForRecipe",
            data: "{ IDRecipe: '" + IDRecipe + "', OffsetRows: '" + RowOffset + "', FetchRows: '" + FetchRows + "'}",
            success: function (msg) {
                $('#' + pnlLikeDetailInternal).html("");
                $.each(msg.d, function () {
                    $('#' + pnlLikeDetailInternal).append(this);
                });
                if (msg.d.length > 0) {
                    $('#' + lnkLikeDetails).prop("href", "#");
                    $('#' + lnkLikeDetails).click(function () {
                        if ($("#" + pnlLikeDetail).attr("class").indexOf('-hide') > -1) {
                            $("#" + pnlLikeDetail).attr("class", "boxLikeDetails-visible");
                        }
                        else
                        {
                            $("#" + pnlLikeDetail).attr("class", "boxLikeDetails-hide");
                        }
                        return false;
                    });
                }
                $('#' + lnkLikeDetails).text(hfLikeDetailBaseText.replace('{0}', msg.d.length));
            }
        });
    }
    catch (err) {

    }
}
function CloseDetails(pnlLikeDetail) {
    $("#" + pnlLikeDetail).attr("class", "boxLikeDetails-hide");
}
function ListRecipeComments(pnlComments, pnlBackground, pnlContent) {
    try {
        var IDRecipe = $('#hfIDRecipe').val();
        var IDUserRequester = $('#hfIDUser').val();
        var RowOffset = 0;
        var FetchRows = 50;
        var _dataToAppend = "";

        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/Recipe/RecipeFeedbacks.asmx/ListCommentsForRecipe",
            data: "{ IDRecipe: '" + IDRecipe + "', OffsetRows: '" + RowOffset + "', FetchRows: '" + FetchRows + "', IDUserRequester: '" + IDUserRequester + "'}",
            success: function (msg) {
                $('#' + pnlComments).html("");
                $.each(msg.d, function () {
                    $('#' + pnlComments).append(this);
                });
                if (msg.d.length > 5) {
                    $("#" + pnlComments).attr("class", "pnlCommentsScroll");
                }
                else
                {
                    $("#" + pnlComments).attr("class", "");
                }
                ResizeBackground(pnlBackground, pnlContent);
            }
        });
    }
    catch (err) {

    }
}

function AddComment(pnlComments, pnlBackground, pnlContent,insertButton) {
    try {
        var IDUser = $('#hfIDUser').val();
        var IDRecipe = $('#hfIDRecipe').val();
        var IDRecipeOwner = $('#hfIDRecipeOwner').val();
        var IDLanguage = $('#hfIDLanguage').val();
        var Username = $('#hfCurrentUsername').val();
        var RecipeURL = $('#hfCurrentRecipeUrl').val();
        var CommentText = $('#txtNewComment').val();
        
        if (CommentText != null && CommentText != '') {
            CommentText = CommentText.replace('\'', '\\\'');
            $("#" + insertButton).attr("disabled", "disabled");
            $.ajax({
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                type: "POST",
                url: "http://" + WebServicesPath + "/Recipe/RecipeFeedbacks.asmx/AddComment",
                data: "{ IDRecipe: '" + IDRecipe + "',IDUser: '" + IDUser + "',IDRecipeOwner: '" + IDRecipeOwner + "', IDLanguage: '" + IDLanguage + "', Username: '" + Username + "', RecipeURL: '" + RecipeURL + "', Text: '" + CommentText + "'}",
                success: function (msg) {
                    if (msg.d.indexOf('200 OK') > -1) {
                        $('#txtNewComment').val("");
                        ListRecipeComments(pnlComments, pnlBackground, pnlContent);
                        $("#" + insertButton).removeAttr("disabled");
                    }
                }
            });
        }
    }
    catch (err) {

    }
    
}

function DeleteComment(IDComment, IDBox) {
    try {
        var IDUser = $('#hfIDUser').val();
        var IDRecipe = $('#hfIDRecipe').val();

        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/Recipe/RecipeFeedbacks.asmx/DeleteComment",
            data: "{ IDRecipeFeedback: '" + IDComment + "',IDRecipe: '" + IDRecipe + "',IDUser: '" + IDUser + "'}",
            success: function (msg) {
                if (msg.d.indexOf('200 OK') > -1) {
                    $('#' + IDBox).css("display", "none");
                }
            }
        });

    }
    catch (err) {
        console.log(err);
    }
}

function ResizeBackground(pnlBackground, pnlContent) {
    $('#' + pnlBackground).height($('#' + pnlContent).height());
}