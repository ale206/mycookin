<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlRecipePolaroid.ascx.cs" Inherits="MyCookinWeb.CustomControls.ctrlRecipePolaroid" %>
<%@ Register TagPrefix="MyCtrl" TagName="RecipeComplexity" Src="~/CustomControls/ctrlRecipeComplexity.ascx" %>

<asp:HiddenField ID="hfIDRecipe" runat="server" />
<asp:HiddenField ID="hfQueryStringParameters" runat="server" />
<asp:Panel ID="pnlCtrlRecipePolaroidMain" runat="server">
    <div style="display: block; padding-bottom: 60px; height: 285px;">
        <div id="divPolaroidBG" style="background-image: url(/Images/bg-polaroid.png); width: 270px;
            height: 285px;">
        </div>
        <div id="divRecipeContent" style="margin-top: -253px; margin-left: 26px;">
            <div id="divRecipePhoto" style="">
                <asp:HyperLink ID="lnkImage" runat="server" NavigateUrl="#">
                <asp:Image ID="impRecipePhoto" runat="server" ImageUrl="/Images/icon-recipe-big.png"
                    Height="187px" Width="187px" /></asp:HyperLink>
                <div id="divLabelRecipeName" style=" height:40px; width:187px;overflow:hidden; margin-top: -170px; opacity: 0.80; filter: alpha(opacity=80);">
                    <asp:HyperLink ID="lnkRecipeName" runat="server" NavigateUrl="#" ForeColor="#70644F" Font-Size="Large" Width="187" Height="55" BackColor="White"></asp:HyperLink>
                </div>
                <div id="divRecipeInfo" style="margin-top:132px;">
                    <div id="divCompexity" style="float:left">
                        <MyCtrl:RecipeComplexity ID="rcRecipe" runat="Server" ControlSize="35" />
                    </div>
                    <div id="divUserInfo" style="float:right; height:40px; width:150px; overflow:hidden; padding-right: 40px; padding-top: -10px;">
                        <asp:HyperLink ID="lnkRecipeOwner" runat="server" Font-Names="Verdana" ForeColor="#70644F" Font-Underline="false" Font-Size="Small" NavigateUrl="#"></asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
