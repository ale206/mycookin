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
function LoadMoreRecipe()
{
    try
    {
        var RecipeType = $('#ddlRecipeType').data('ddslick').selectedData['value'];
        var OffsetRows = parseInt($('#hfRowOffSet').val());
        var FetchRows = 12;
        var Vegan = $('#chkVegan').is(":checked");
        var Vegetarian = $('#chkVegetarian').is(":checked");
        var GlutenFree = $('#chkGlutenFree').is(":checked");
        var LightThreshold = 10000;
        var QuickThreshold = 10000;
        var IDLanguage = $('#hfIDLanguage').val();
        if ($('#chkLight').is(":checked"))
        { LightThreshold = $('#hfLightRecipeThreshold').val() }
        if ($('#chkQuick').is(":checked"))
        { QuickThreshold = $('#hfQuickRecipeThreshold').val() }
        //alert("{ RecipeType: '" + RecipeType + "', OffsetRows: '" + OffsetRows + "', FetchRows: '" + FetchRows + "', Vegan: '" + Vegan + "', Vegetarian: '" + Vegetarian + "', GlutenFree: '" + GlutenFree + "', LightThreshold: '" + LightThreshold + "', QuickThreshold: '" + QuickThreshold + "', IDLanguage: '" + IDLanguage + "'}");
        var _dataToAppend = "";
        $('#boxLoading').css({ 'visibility': 'visible' });
        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/Recipe/GetRecipesByType.asmx/GetRecipesListHTML",
            data: "{ RecipeType: '" + RecipeType + "', OffsetRows: '" + OffsetRows + "', FetchRows: '" + FetchRows + "', Vegan: '" + Vegan + "', Vegetarian: '" + Vegetarian + "', GlutenFree: '" + GlutenFree + "', LightThreshold: '" + LightThreshold + "', QuickThreshold: '" + QuickThreshold + "', IDLanguage: '" + IDLanguage + "'}",
            success: function (msg) {
                $.each(msg.d, function () {
                    $('#RecipesListContainer').append(this.replace("{RecipeOf}", $('#hfRecipeOf').val()).replace("{RecipeOf2}", $('#hfRecipeOf2').val()));
                });
                $('.pnlBackground').height($('.pnlContent').height());
                if(msg.d.length < 12)
                {
                    $('#boxLoadButton').css({ 'visibility': 'hidden' });
                }
                else
                {
                    $('#boxLoadButton').css({ 'visibility': 'visible' });
                }
            }
        });
        $('#boxLoading').css({ 'visibility': 'hidden' });
        OffsetRows += FetchRows;
        $('#hfRowOffSet').val(OffsetRows)
    }
    catch(err)
    {

    }
}

function ResetAndReloadRecipes()
{
    $('#hfRowOffSet').val('0');
    $('#RecipesListContainer').html("");
    LoadMoreRecipe();
}