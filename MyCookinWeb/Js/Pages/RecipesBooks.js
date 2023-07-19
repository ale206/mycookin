function checkboxChange(chekboxId, imgId) {
    if ($("#" + chekboxId).attr('checked')) {
        $("#" + chekboxId).attr('checked', false);

        $("#" + imgId).attr('src', $("#" + imgId).attr('src').replace("-on", "-off"));
    }
    else {
        $("#" + chekboxId).attr('checked', true);
        $("#" + imgId).attr('src', $("#" + imgId).attr('src').replace("-off", "-on"));
    }
    ResetAndReloadRecipes();
}

function changeImage(chekboxId, imgId, direction) {
    if (direction == "over") {
        if (!$("#" + chekboxId).attr('checked')) {
            $("#" + imgId).attr('src', $("#" + imgId).attr('src').replace("-off", "-on"));
        }
    }
    else {
        if (!$("#" + chekboxId).attr('checked')) {
            $("#" + imgId).attr('src', $("#" + imgId).attr('src').replace("-on", "-off"));
        }
    }
}
function LoadMoreRecipe() {
    try {
        var IDUser = $('#hfIDUser').val();;
        var IDRequester = $('#hfIDRequester').val();
        var RecipeType = 0;
        var RecipeSource = 0;
        RecipeType = $('#ddlRecipeType').data('ddslick').selectedData['value'];
        RecipeSource = $('#ddlRecipeSource').data('ddslick').selectedData['value'];
        var OffsetRows = parseInt($('#hfRowOffSet').val());
        var FetchRows = 15;
        var Vegan = $('#chkVegan').is(":checked");
        var Vegetarian = $('#chkVegetarian').is(":checked");
        var GlutenFree = $('#chkGlutenFree').is(":checked");
        var LightThreshold = 10000;
        var QuickThreshold = 10000;
        var IDLanguage = $('#hfIDLanguage').val();
        var SearchString = $('#txtRecipeFilter').val();
        if ($('#chkLight').is(":checked"))
        { LightThreshold = $('#hfLightRecipeThreshold').val() }
        if ($('#chkQuick').is(":checked"))
        { QuickThreshold = $('#hfQuickRecipeThreshold').val() }
        //alert("{ IDUser: '" + IDUser + "',IDRequester: '" + IDRequester + "',RecipeType: '" + RecipeType + "',ShowFilter: '" + RecipeSource + "',RecipeNameFilter: '" + SearchString + "', RowOffSet: '" + OffsetRows + "', FetchRows: '" + FetchRows + "', Vegan: '" + Vegan + "', Vegetarian: '" + Vegetarian + "', GlutenFree: '" + GlutenFree + "', LightThreshold: '" + LightThreshold + "', QuickThreshold: '" + QuickThreshold + "', IDLanguage: '" + IDLanguage + "'}");
        var _dataToAppend = "";
        $('#boxLoading').css({ 'visibility': 'visible' });
        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/Recipe/GetRecipesByType.asmx/GetRecipesInRecipesBook",
            data: "{ IDUser: '" + IDUser + "',IDRequester: '" + IDRequester + "',RecipeType: '" + RecipeType + "',ShowFilter: '" + RecipeSource + "',RecipeNameFilter: '" + SearchString + "', RowOffSet: '" + OffsetRows + "', FetchRows: '" + FetchRows + "', Vegan: '" + Vegan + "', Vegetarian: '" + Vegetarian + "', GlutenFree: '" + GlutenFree + "', LightThreshold: '" + LightThreshold + "', QuickThreshold: '" + QuickThreshold + "', IDLanguage: '" + IDLanguage + "'}",
            success: function (msg) {
                $.each(msg.d, function () {
                    $('#RecipesListContainer').append(this.replace("{RecipeOf}", $('#hfRecipeOf').val()).replace("{RecipeOf2}", $('#hfRecipeOf2').val()).replace("{EditRecipe}", $('#hfEditRecipeText').val()));
                });
                $('.pnlBackground').height($('.pnlContent').height());
                if (msg.d.length < 15) {
                    $('#pnlNextPage').css({ 'visibility': 'hidden' });
                }
                else {
                    $('#pnlNextPage').css({ 'visibility': 'visible' });
                }
                if (OffsetRows == 0)
                {
                    $('#pnlPrevPage').css({ 'visibility': 'hidden' });
                }
                else
                {
                    $('#pnlPrevPage').css({ 'visibility': 'visible' });
                }
                if (msg.d.length == 0 && OffsetRows == 0)
                {
                    $('pnlNoSearchResult').css({ 'display': 'block' });
                    $('#pnlPrevPage').css({ 'visibility': 'hidden' });
                    $('#pnlNextPage').css({ 'visibility': 'hidden' });
                }
                else
                {
                    $('pnlNoSearchResult').css({ 'display': 'none' });
                }
                $('#boxLoading').css({ 'visibility': 'hidden' });
                $('#hfRowOffSet').val(OffsetRows)
            }
        });
        
    }
    catch (err) {

    }
}

function ResetAndReloadRecipes() {
    $('#pnlPrevPage').css({ 'visibility': 'hidden' });
    $('#hfRowOffSet').val('0');
    $('#RecipesListContainer').html("");
    LoadMoreRecipe();
}

function Next() {
    $('#RecipesListContainer').html("");
    var OffsetRows = parseInt($('#hfRowOffSet').val());
    OffsetRows += 15;
    $('#hfRowOffSet').val(OffsetRows);
    LoadMoreRecipe();
}

function Prev() {
    $('#RecipesListContainer').html("");
    var OffsetRows = parseInt($('#hfRowOffSet').val());
    OffsetRows -= 15;
    $('#hfRowOffSet').val(OffsetRows);
    LoadMoreRecipe();
}