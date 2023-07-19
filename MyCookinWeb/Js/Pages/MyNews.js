function LoadSimilarRecipes(pnlRecipeName, pnlRecipesLoadingName) {
    try {
        var IDLanguage = $('#hfIDLanguage').val();
        var _dataToAppend = "";

        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/Recipe/GetRecipesByType.asmx/GetSimilarRecipesListHTML",
            data: "{ RecipeName: '', IDRecipe: '00000000-0000-0000-0000-000000000000', Vegan: 'false', Vegetarian: 'true', GlutenFree: 'false', IDLanguage: '" + IDLanguage + "'}",
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