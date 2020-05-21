<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlBeverageRecipe.ascx.cs" Inherits="MyCookinWeb.CustomControls.ctrlBeverageRecipe" %>
<asp:HiddenField ID="hfIDBeverageRecipe" runat="server"  />
<asp:HiddenField ID="hfIDRecipe" runat="server" />
<asp:HiddenField ID="hfIDBeverage" runat="server" />
<asp:HiddenField ID="hfIDUserSuggestedBy" runat="server" />
<asp:HiddenField ID="hfDateSuggestion" runat="server" />
<asp:HiddenField ID="hfBeverageRecipeAvgRating" runat="server" />
<asp:HiddenField ID="hfIDLanguage" runat="server" />
<asp:HiddenField ID="hfShowEditButton" Value="false" runat="server" />
<asp:HiddenField ID="hfControlLoaded" Value="false" runat="server" />
<asp:HiddenField ID="hfShowInfoPanel" Value="false" runat="server" />

<link href="/Styles/jQueryUiCss/PopBox/popbox.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript" src="/Js/CustomControls/ctrlShowlRecipeIngredient.js"></script>
<script type="text/javascript" language="javascript" src="/Js/PopBox/popbox.js"></script>

<asp:Panel ID="pnlBeverageInfoBox" runat="server" CssClass="popbox">
    <asp:HyperLink ID="lnkBeverage" runat="server" CssClass="linkPopBox" NavigateUrl="#"></asp:HyperLink>
    <asp:ImageButton ID="btnDeleteRecipeBeverage" ImageUrl="/Images/deleteX.png" 
        runat="server" Width="16" Height="16" onclick="btnDeleteRecipeBeverage_Click" />
    <asp:Label ID="lblBeverageNote" runat="server" Text="" CssClass="lblRecipeIngredientNote"></asp:Label>
    <div class="collapse">
        <asp:Panel ID="pnlBoxInternal" CssClass="box" runat="server">
            <div id="pnlBeverageInfoBoxContent" class="pnlIngredientInfoBoxContent">
                <asp:Image ID="imgBeveragePhoto" runat="server" CssClass="IngredientInfoBoxImage" />
                <asp:Label ID="lblBeverageLink" runat="server" Text="" CssClass="lblRecipeIngredientLink"></asp:Label><br />
                <div id="pnlBeverageBasicInfo" class="pnlIngredientBasicInfo">
                    <asp:Label ID="lblBeverageBasicInfo" runat="server" Text="" CssClass="lblRecipeIngredientBasicInfo"></asp:Label>
                </div>
                <br />
                 <asp:Panel ID="pnlUserAndRating" CssClass="pnlAltIngredient" runat="server">
                  <asp:HyperLink ID="lnkUser" runat="server"></asp:HyperLink>
                    <asp:Label ID="lblDateInfo" CssClass="lblRecipeIngredientNote" runat="server"></asp:Label>
                </asp:Panel>
            </div>
        </asp:Panel>
    </div>
</asp:Panel>