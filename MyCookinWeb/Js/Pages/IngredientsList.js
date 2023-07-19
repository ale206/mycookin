function checkboxChange(chekboxId, imgId) {
    if ($("#" + chekboxId).attr('checked')) {
        $("#" + chekboxId).attr('checked', false);

        $("#" + imgId).attr('src', $("#" + imgId).attr('src').replace("-on", "-off"));
    }
    else {
        $("#" + chekboxId).attr('checked', true);
        $("#" + imgId).attr('src', $("#" + imgId).attr('src').replace("-off", "-on"));
    }
    ResetAndReloadIngredients();
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
function LoadMoreIngredients()
{
    try
    {
        var StartWith = $('#ddlIngrStartWith').data('ddslick').selectedData['value'];
        var OffSetRow = parseInt($('#hfRowOffSet').val());
        var FetchRows = 12;
        var Vegan = $('#chkVegan').is(":checked");
        var Vegetarian = $('#chkVegetarian').is(":checked");
        var GlutenFree = $('#chkGlutenFree').is(":checked");
        var HotSpicy = $('#chkHotSpicy').is(":checked");
        var IDLanguage = $('#hfIDLanguage').val();
        //alert("{ RecipeType: '" + RecipeType + "', OffsetRows: '" + OffsetRows + "', FetchRows: '" + FetchRows + "', Vegan: '" + Vegan + "', Vegetarian: '" + Vegetarian + "', GlutenFree: '" + GlutenFree + "', LightThreshold: '" + LightThreshold + "', QuickThreshold: '" + QuickThreshold + "', IDLanguage: '" + IDLanguage + "'}");
        var _dataToAppend = "";
        $('#boxLoading').css({ 'visibility': 'visible' });
        $.ajax({
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "http://" + WebServicesPath + "/Ingredient/SearchIngredient.asmx/IngredientsList",
            data: "{ StartWith: '" + StartWith + "', OffSetRow: '" + OffSetRow + "', FetchRows: '" + FetchRows + "', Vegan: '" + Vegan + "', Vegetarian: '" + Vegetarian + "', GlutenFree: '" + GlutenFree + "', HotSpicy: '" + HotSpicy + "', IDLanguage: '" + IDLanguage + "'}",
            success: function (msg) {
                $.each(msg.d, function () {
                    $('#IngredientsListContainer').append(this);
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
        OffSetRow += FetchRows;
        $('#hfRowOffSet').val(OffSetRow)
    }
    catch(err)
    {

    }
}

function ResetAndReloadIngredients()
{
    $('#hfRowOffSet').val('0');
    $('#IngredientsListContainer').html("");
    LoadMoreIngredients();
}