<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlMyRecipesBookBlock.ascx.cs" Inherits="MyCookinWeb.CustomControls.ctrlMyRecipesBookBlock" %>
<asp:Panel ID="pnlMyRecipeBookBlock" runat="server" 
    meta:resourcekey="pnlMyRecipeBookBlockResource1">
    <asp:UpdatePanel ID="upnMyRecipeBookBlock" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="MyRecipeBookBlockInt" runat="server" 
                CssClass="pnlMyRecipeBookBlockSmall" 
                meta:resourcekey="MyRecipeBookBlockIntResource1">
                <asp:HiddenField ID="hfIDRecipe" runat="server" />
                <asp:HiddenField ID="hfIDUser" runat="server" />
                <asp:HiddenField ID="hfIDLanguage" runat="server" />
                <asp:HiddenField ID="hfQueryStringParameters" runat="server" />
                <asp:Panel ID="pnlRecipePhoto" runat="server" CssClass="pnlRecipePhotoSmall" 
                    meta:resourcekey="pnlRecipePhotoResource1">
                    <asp:ImageButton ID="btnImgRecipe" runat="server" CssClass="imgRecipeSmall" 
                        ImageUrl="/Images/icon-recipe-big.png" Width="80px" Height="80px"
                        OnClick="btnImgRecipe_Click" meta:resourcekey="btnImgRecipeResource1"  />
                </asp:Panel>
                <asp:Panel ID="pnlRecipeName" runat="server" CssClass="pnlRecipeNameSmall" 
                    meta:resourcekey="pnlRecipeNameResource1">
                    <asp:HyperLink ID="lnkRecipeName" runat="server" CssClass="lnkRecipeNameSmall" 
                        meta:resourcekey="lnkRecipeNameResource1"></asp:HyperLink>
                    <asp:HyperLink ID="lnkRecipeOwner" runat="server" 
                        CssClass="lnkRecipeOwnerSmall" meta:resourcekey="lnkRecipeOwnerResource1">Ricetta di {0}</asp:HyperLink>
                    <asp:ImageButton ID="btnDeleteFromRecipeBook" runat="server" AlternateText="Delete"
                        ToolTip="Elimina dal ricettario" ImageUrl="/Images/deleteX.png" OnClick="btnDeleteFromRecipeBook_Click"
                        CssClass="btnDeleteFromRecipeBook" Height="16px" Width="16px" 
                        meta:resourcekey="btnDeleteFromRecipeBookResource1" />
                    <asp:HyperLink ID="btnEditRecipe" runat="server" CssClass="MyButtonSmall" meta:resourcekey="btnEditRecipeResource1"/></asp:HyperLink>
                </asp:Panel>
                <asp:Panel ID="pnlRecipeInfo" runat="server" CssClass="pnlRecipeInfoSmall" 
                    meta:resourcekey="pnlRecipeInfoResource1">
                    <asp:Panel ID="pnlVegan" runat="server" CssClass="pnlIconColor" 
                        meta:resourcekey="pnlVeganResource1">
                        <asp:Image ID="imgVegan" runat="server" ImageUrl="/Images/vegan-on.png" CssClass="imgIconColor"
                            Width="40px" Height="40px" meta:resourcekey="imgVeganResource1" />
                    </asp:Panel>
                    <asp:Panel ID="pnlVegetarian" runat="server" CssClass="pnlIconColor" 
                        meta:resourcekey="pnlVegetarianResource1">
                        <asp:Image ID="imgVegetarian" runat="server" ImageUrl="/Images/vegetarian-on.png"
                            CssClass="imgIconColor" Width="40px" Height="40px" 
                            meta:resourcekey="imgVegetarianResource1" />
                    </asp:Panel>
                    <asp:Panel ID="pnlGlutenFree" runat="server" CssClass="pnlIconColor" 
                        meta:resourcekey="pnlGlutenFreeResource1">
                        <asp:Image ID="imgGlutenFree" runat="server" ImageUrl="/Images/gluten-free-on.png"
                            CssClass="imgIconColor" Width="40px" Height="40px" 
                            meta:resourcekey="imgGlutenFreeResource1" />
                    </asp:Panel>
                    <asp:Panel ID="pnlHotSpicy" runat="server" CssClass="pnlIconColor" 
                        meta:resourcekey="pnlHotSpicyResource1">
                        <asp:Image ID="imgHotSpicy" runat="server" 
                            ImageUrl="/Images/icon-HotSpicy-on.png" CssClass="imgIconColor"
                            Width="40px" Height="40px" meta:resourcekey="imgHotSpicyResource1" />
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
