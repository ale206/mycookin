<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master" AutoEventWireup="true" CodeBehind="ShowIngredient.aspx.cs" Inherits="MyCookinWeb.IngredientWeb.ShowIngredient" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register TagName="ShowRecipe" TagPrefix="MyCtrl" Src="~/CustomControls/ctrlRecipePolaroid.ascx" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
 <link rel="Stylesheet" href="/Styles/PageStyle/ShowIngredient.min.css?ver=140302" />
 <meta property="og:title" content="<%=hfOgpTitle.Value %>" />
 <meta property="og:url" content="<%=hfOgpUrl.Value %>" />
 <meta property="og:description" content="<%=hfOgpDescription.Value %>" />
 <meta property="og:image" content="<%=hfOgpImage.Value %>" />
 <meta property="og:fb_app_id" content="<%=hfOgpFbAppID.Value %>" />
 <meta name="twitter:card" content="summary" />
 <meta name="twitter:site" content="@mycookin" />
 <meta name="twitter:creator" content="@mycookin" />
 <meta property="og:type" content="website" />
 <meta property="og:site_name" content="MyCookin" />

 <meta name="title" content="<%=hfOgpTitle.Value %>"/>
 <meta name="description" content="<%=hfOgpDescription.Value %>"/>
 <meta name="keywords" content="<%=hfKeywords.Value %>"/>
 <meta name="author" content="MyCookin"/>
 <meta name="copyright" content="MyCookin"/>
 <meta http-equiv="Reply-to" content="alessio@mycookin.com;saverio@mycookin.com"/>
 <meta http-equiv="content-language" content="<%=hfLanguageCode.Value %>"/>
 <meta http-equiv="Content-Type" content="text/html; iso-8859-1"/>
 <meta name="ROBOTS" content="INDEX,FOLLOW"/>
 <meta name="creation_Date" content="<%=hfCreationDate.Value %>"/>
 <meta name="revisit-after" content="7 days"/>
</asp:Content>
<asp:Content ID="cntMain" ContentPlaceHolderID="cphMain" runat="server">
    <asp:HiddenField ID="hfReferrerURL" runat="server" />
    <asp:HiddenField ID="hfKeywords" runat="server" />
    <asp:HiddenField ID="hfLanguageCode" runat="server" />
    <asp:HiddenField ID="hfCreationDate" runat="server" />
    <asp:HiddenField ID="hfOgpTitle" runat="server" />
    <asp:HiddenField ID="hfOgpDescription" runat="server" />
    <asp:HiddenField ID="hfOgpUrl" runat="server" />
    <asp:HiddenField ID="hfOgpImage" runat="server" />
    <asp:HiddenField ID="hfOgpFbAppID" runat="server" />
    <asp:Panel ID="pnlMain" runat="server" CssClass="pnlMain" 
        meta:resourcekey="pnlMainResource1">
        <asp:Panel ID="pnlBackground" runat="server" CssClass="pnlBackground" 
            meta:resourcekey="pnlBackgroundResource1">
            <div id="bgHeadBottom" class="bgHeadBottom">
            </div>
            <div id="bgHead" class="bgHead">
                <div id="bgHeadLeft" class="bgHeadLeft">
                </div>
                <div id="bgHeadRight" class="bgHeadRight">
                </div>
            </div>
            <div id="bgCenter" class="bgCenter">
                <div id="bgCenterLeft" class="bgCenterLeft">
                </div>
                <div id="bgCenterRight" class="bgCenterRight">
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlContent" runat="server" CssClass="pnlContent" 
            meta:resourcekey="pnlContentResource1">
        <asp:Panel ID="pnlButtonsManage" runat="server" CssClass="pnlButtonsManage" 
                meta:resourcekey="pnlButtonsManageResource1">
                    <asp:ImageButton ID="btnGoBack" runat="server" 
                        CssClass="MyTooltip btnAction" ToolTip="Torna Indietro" 
                        ImageUrl="/Images/icon-goBack.png" Visible="False" 
                         OnClick="btnGoBack_Click" Width="40px" Height="40px" 
                        meta:resourcekey="btnGoBackResource1"/>
                </asp:Panel>
            <asp:Panel ID="pnlContentHead" runat="server" CssClass="pnlContentHead" 
                meta:resourcekey="pnlContentHeadResource1">
                <asp:Panel ID="pnlIngredientImage" runat="server" CssClass="pnlIngredientImage" 
                    meta:resourcekey="pnlIngredientImageResource1">
                    <asp:Image ID="imgIngredient" runat="server" CssClass="imgIngredient" 
                        Width="200px" Height="200px" meta:resourcekey="imgIngredientResource1" />
                </asp:Panel>
                
                <asp:Panel ID="pnlIngredientName" runat="server" CssClass="pnlIngredientName" 
                    meta:resourcekey="pnlIngredientNameResource1">

                    <h1><asp:Label ID="lblIngredientName" runat="server" 
                        Text="Olio d'oliva Extra Vergine" CssClass="lblIngredientName" 
                        meta:resourcekey="lblIngredientNameResource1"></asp:Label></h1>
                    
                    <asp:Label ID="lblIngredientDesc" runat="server" Text="Descrizione" 
                        CssClass="lblIngredientDesc" meta:resourcekey="lblIngredientDescResource1"></asp:Label>
                </asp:Panel>
                
            </asp:Panel>
            <asp:Panel ID="pnlKcalAndPreparationRecipe" runat="server" 
                CssClass="pnlKcalAndPreparationRecipe" 
                meta:resourcekey="pnlKcalAndPreparationRecipeResource1">
                    <asp:Panel ID="pnlKcal" runat="server" CssClass="pnlIconKcal" 
                        meta:resourcekey="pnlKcalResource1">
                        <div  class="displayBlock40px">
                            <asp:Label ID="lblKcal" runat="server" Text="Kcal" CssClass="lblKcal" 
                                meta:resourcekey="lblKcalResource1"></asp:Label>
                            <asp:Label ID="lblKcalValue" runat="server" Text="133" CssClass="lblKcalValue" 
                                meta:resourcekey="lblKcalValueResource1"></asp:Label>
                            <asp:Label ID="lbl100g" runat="server" Text="(x 100g)" CssClass="lbl100g" 
                                meta:resourcekey="lbl100gResource1"></asp:Label>
                        </div>
                        <div class="displayBlock40px">
                            
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlPreparationRecipe" runat="server" 
                        meta:resourcekey="pnlPreparationRecipeResource1">
                        <asp:HyperLink ID="lnkPreparationRecipe" runat="server">
                            <div style="float:left; height:30px; margin-top:8px;">
                            <asp:Image ID="imgPreparationRecipe" runat="server" 
                                    ImageUrl="/Images/icon-recipe.png" Width="30px" Height="30px" 
                                    meta:resourcekey="imgPreparationRecipeResource1" />
</div>
                             <div style="float:left; padding-top: 15px; padding-left: 5px;">
                            <asp:Label ID="lblPreparationRecipe" runat="server" Text="Vai alla ricetta" 
                                     CssClass="lblPreparationRecipe" 
                                     meta:resourcekey="lblPreparationRecipeResource1"></asp:Label>
</div>
                        </asp:HyperLink>
                    </asp:Panel>
            </asp:Panel>
            <div id="space6" style="display:block; width:80%; height:70px;"></div>
            <asp:Panel ID="pnlFoodInfo" runat="server" CssClass="pnlFoodInfo" 
                meta:resourcekey="pnlFoodInfoResource1">
                <asp:Panel ID="pnlIconInfo" runat="server" CssClass="pnlIconInfo" 
                    meta:resourcekey="pnlIconInfoResource1">
                    <asp:Panel ID="pnlVegan" runat="server" CssClass="pnlIcon" 
                        meta:resourcekey="pnlVeganResource1">
                        <asp:Image ID="imgVegan" runat="server" ImageUrl="/Images/vegan-gray.png" 
                            CssClass="imgIcon" Width="40px" Height="40px" 
                            meta:resourcekey="imgVeganResource1" />
                        <asp:Label ID="lblVegan" runat="server" Text="Vegano" CssClass="lblIcon" 
                            Width="150px" meta:resourcekey="lblVeganResource1"></asp:Label>
                    </asp:Panel>
                    <asp:Panel ID="pnlVegetarian" runat="server" CssClass="pnlIcon" 
                        meta:resourcekey="pnlVegetarianResource1">
                        <asp:Image ID="imgVegetarian" runat="server" 
                            ImageUrl="/Images/vegetarian-gray.png" CssClass="imgIcon" Width="40px" 
                            Height="40px" meta:resourcekey="imgVegetarianResource1" />
                        <asp:Label ID="lblVegetarian" runat="server" Text="Vegetariano" 
                            CssClass="lblIcon" Width="150px" meta:resourcekey="lblVegetarianResource1"></asp:Label>
                    </asp:Panel>
                    <asp:Panel ID="pnlGlutenFree" runat="server" CssClass="pnlIcon" 
                        meta:resourcekey="pnlGlutenFreeResource1">
                        <asp:Image ID="imgGlutenFree" runat="server" 
                            ImageUrl="/Images/gluten-free-gray.png" CssClass="imgIcon" Width="40px" 
                            Height="40px" meta:resourcekey="imgGlutenFreeResource1" />
                        <asp:Label ID="lblGlutenFree" runat="server" Text="Senza Glutine" 
                            CssClass="lblIcon" Width="150px" meta:resourcekey="lblGlutenFreeResource1"></asp:Label>
                    </asp:Panel>
                    <asp:Panel ID="pnlHotSpicy" runat="server" CssClass="pnlIcon" 
                        meta:resourcekey="pnlHotSpicyResource1">
                        <asp:Image ID="imgHotSpicy" runat="server" ImageUrl="/Images/icon-HotSpicy.png" 
                            CssClass="imgIcon" Width="40px" Height="40px" 
                            meta:resourcekey="imgHotSpicyResource1" />
                        <asp:Label ID="lblHotSpicy" runat="server" Text="Piccante" CssClass="lblIcon" 
                            Width="150px" meta:resourcekey="lblHotSpicyResource1"></asp:Label>
                    </asp:Panel>
                </asp:Panel>
                <div id="Space2" style="display:block; height:35px"></div>
                <asp:Panel ID="pnlNutrictionalFacts" runat="server" 
                    CssClass="pnlNutrictionalFacts" 
                    meta:resourcekey="pnlNutrictionalFactsResource1">
                    <div id="Space3" style="display: block; height: 70px">
                    </div>
                    <asp:Panel ID="pnlBalance" runat="server" CssClass="pnlIconBalance" 
                        Visible="False" meta:resourcekey="pnlBalanceResource1">
                        <asp:Image ID="imgBalance" runat="server" ImageUrl="/Images/ico-bilancia.png" 
                            Width="40px" Height="40px" meta:resourcekey="imgBalanceResource1" />
                    </asp:Panel>
                    <asp:Panel ID="pnlNutritionalInfo" runat="server" CssClass="pnlNutritionalInfo" 
                        meta:resourcekey="pnlNutritionalInfoResource1">
                        <asp:Panel ID="pnlProteins" runat="server" CssClass="pnlNutritionalInfoDetails" 
                            meta:resourcekey="pnlProteinsResource1">
                            <asp:Label ID="lblProteins" runat="server" Text="{0}" 
                                CssClass="lblNutritionalFactsInfo" meta:resourcekey="lblProteinsResource1"></asp:Label>
                            <asp:Label ID="lblProteinsTitle" runat="server" Text="Proteine" 
                                CssClass="lblNutritionalFactsInfoTitle" 
                                meta:resourcekey="lblProteinsTitleResource1"></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="pnlFats" runat="server" CssClass="pnlNutritionalInfoDetails" 
                            meta:resourcekey="pnlFatsResource1">
                            <asp:Label ID="lblFats" runat="server" Text="{0}" 
                                CssClass="lblNutritionalFactsInfo" meta:resourcekey="lblFatsResource1"></asp:Label>
                            <asp:Label ID="lblFatsTitle" runat="server" Text="Grassi" 
                                CssClass="lblNutritionalFactsInfoTitle" 
                                meta:resourcekey="lblFatsTitleResource1"></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="pnlCarbohydrates" runat="server" 
                            CssClass="pnlNutritionalInfoDetails" 
                            meta:resourcekey="pnlCarbohydratesResource1">
                            <asp:Label ID="lblCarbohydrates" runat="server" Text="{0}" 
                                CssClass="lblNutritionalFactsInfo" meta:resourcekey="lblCarbohydratesResource1"></asp:Label>
                            <asp:Label ID="lblCarbohydratesTitle" runat="server" Text="Carboidrati" 
                                CssClass="lblNutritionalFactsInfoTitle" 
                                meta:resourcekey="lblCarbohydratesTitleResource1"></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="pnlAlcohol" runat="server" CssClass="pnlNutritionalInfoDetails" 
                            meta:resourcekey="pnlAlcoholResource1">
                            <asp:Label ID="lblAlcohol" runat="server" Text="{0}" 
                                CssClass="lblNutritionalFactsInfo" meta:resourcekey="lblAlcoholResource1"></asp:Label>
                            <asp:Label ID="lblAlcoholTitle" runat="server" Text="Alcool" 
                                CssClass="lblNutritionalFactsInfoTitle" 
                                meta:resourcekey="lblAlcoholTitleResource1"></asp:Label>
                        </asp:Panel>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
            <div style="display:block; width:300px; margin-left:80px; margin-top:23px; margin-bottom:30px;">
                <asp:HyperLink ID="lnkAllIngredients" runat="server" meta:resourcekey="lnkAllIngredientsResource1" CssClass="lblPreparationRecipe"></asp:HyperLink>
            </div>
            <div style="display:block; width:100%; min-width:800px;">
                <asp:Panel ID="pnlRecipeForIngredint" runat="server" 
                    CssClass="pnlRecipeForIngredint" 
                    meta:resourcekey="pnlRecipeForIngredintResource1">
                    <asp:Panel ID="pnlRecipe1" runat="server" CssClass="pnlRecipeLeft" 
                        meta:resourcekey="pnlRecipe1Resource1">
                        <MyCtrl:ShowRecipe ID="ShowRecipe1" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlRecipe2" runat="server" CssClass="pnlRecipeRight" 
                        meta:resourcekey="pnlRecipe2Resource1">
                       <MyCtrl:ShowRecipe ID="ShowRecipe2" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlRecipe3" runat="server" CssClass="pnlRecipeLeft" 
                        meta:resourcekey="pnlRecipe3Resource1">
                        <MyCtrl:ShowRecipe ID="ShowRecipe3" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlRecipe4" runat="server" CssClass="pnlRecipeRight" 
                        meta:resourcekey="pnlRecipe4Resource1">
                        <MyCtrl:ShowRecipe ID="ShowRecipe4" runat="server" />
                    </asp:Panel>
                </asp:Panel>
            </div>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
