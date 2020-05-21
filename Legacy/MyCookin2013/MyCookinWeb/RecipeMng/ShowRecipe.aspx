<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master"
    AutoEventWireup="true" CodeBehind="ShowRecipe.aspx.cs" Inherits="MyCookinWeb.RecipeWeb.ShowRecipe" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register TagPrefix="MyCtrl" TagName="ShowRecipeIngr" Src="~/CustomControls/ctrlShowRecipeIngredient.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="Rating" Src="~/CustomControls/ctrlRating.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="RecipeComplexity" Src="~/CustomControls/ctrlRecipeComplexity.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="RecipeBeverage" Src="~/CustomControls/ctrlBeverageRecipe.ascx" %>
<%@ Register TagName="RecipeBlock" TagPrefix="MyCtrl" Src="~/CustomControls/ctrlMyRecipesBookBlock.ascx" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/ShowRecipe.min.css?ver=140618" />
    <link href="/Styles/jQueryUiCss/PopBox/popbox.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/PageStyle/IngredientPopBox.min.css" rel="stylesheet" type="text/css" />
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
     <meta name="author" content="<%=hfRecipeOwner.Value %>"/>
     <meta name="copyright" content="MyCookin"/>
     <meta http-equiv="Reply-to" content="alessio@mycookin.com;saverio@mycookin.com"/>
     <meta http-equiv="content-language" content="<%=hfLanguageCode.Value %>"/>
     <meta http-equiv="Content-Type" content="text/html; iso-8859-1"/>
     <meta name="ROBOTS" content="INDEX,FOLLOW"/>
     <meta name="creation_Date" content="<%=hfCreationDate.Value %>"/>
     <meta name="revisit-after" content="7 days"/>
</asp:Content>
<asp:Content ID="cntMain" ContentPlaceHolderID="cphMain" runat="server">
<script language="javascript" type="text/javascript">
<%--    function pageLoad() {
        //$('#<%=pnlBackground.ClientID%>').height($('#<%=pnlContent.ClientID%>').height());
    }--%>
</script>
    <asp:HiddenField ID="hfRowOffSet" runat="server" />
    <asp:HiddenField ID="hfKeywords" runat="server" />
    <asp:HiddenField ID="hfLanguageCode" runat="server" />
    <asp:HiddenField ID="hfCreationDate" runat="server" />
    <asp:HiddenField ID="hfOgpTitle" runat="server" />
    <asp:HiddenField ID="hfOgpDescription" runat="server" />
    <asp:HiddenField ID="hfOgpUrl" runat="server" />
    <asp:HiddenField ID="hfOgpImage" runat="server" />
    <asp:HiddenField ID="hfOgpFbAppID" runat="server" />
    <asp:HiddenField ID="hfReferrerURL" runat="server" />
    <asp:HiddenField ID="hfPrevRecipe" runat="server" />
    <asp:HiddenField ID="hfNextRecipe" runat="server" />
    <asp:HiddenField ID="hfRecipeOwner" runat="server" />
    <asp:HiddenField ID="hfIDLanguage" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfLikeDetailBaseText" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfIDRecipeOwner" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfIDRecipe" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfRecipeName" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfVegan" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfVegetarian" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfGlutenFree" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfIDUser" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfCurrentUsername" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfCurrentRecipeUrl" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfRecipeOf" ClientIDMode="Static" runat="server" Value=""/>
    <asp:HiddenField ID="hfRecipeOf2" ClientIDMode="Static" runat="server" Value=""/>
    <asp:HiddenField ID="hfThumbnailUrl" runat="server" />
    <asp:HiddenField ID="hfPrepTime" runat="server" />
    <asp:HiddenField ID="hfCookTime" runat="server" />
    <asp:Panel ID="pnlMain" runat="server" CssClass="pnlMain" 
        meta:resourcekey="pnlMainResource1">
        <asp:Panel ID="pnlBackground" runat="server" CssClass="pnlBackground" ClientIDMode="Static">
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
        <asp:Panel ID="pnlContent" runat="server" CssClass="pnlContent" ClientIDMode="Static">
            <meta itemprop="ThumbnailUrl" content="<%=hfThumbnailUrl.Value %>"/>
            <meta itemprop="description" content="<%=hfOgpDescription.Value %>"/>
            <meta itemprop="datePublished" content="<%=hfCreationDate.Value %>"/>
            <meta itemprop="prepTime" content="<%=hfPrepTime.Value %>"/>
            <meta itemprop="cookTime" content="<%=hfCookTime.Value %>"/>
            <asp:Panel ID="pnlLeftPanel" runat="server" CssClass="pnlLeftPanel" 
                meta:resourcekey="pnlLeftPanelResource1">
                <asp:Panel ID="pnlRecipeImage" runat="server" CssClass="pnlRecipeImage" 
                    meta:resourcekey="pnlRecipeImageResource1">
                    <asp:Image ID="imgRecipe" runat="server" CssClass="imgRecipe" ImageUrl="/Images/icon-recipe-big.png"
                        Width="200px" Height="200px" meta:resourcekey="imgRecipeResource1" />
                </asp:Panel>
                <asp:Panel ID="pnlLikes" runat="server" CssClass="pnlLeftInternalLike">
                    <asp:ImageButton ID="btnLike" runat="server" CssClass="imgIconInfo" ToolTip="Like it"
                        Width="40px" Height="40px" ImageUrl="/Images/icon-like-off.png"
                        OnClientClick="LikeUnLikeRecipe('btnLike'); return false;"
                        ClientIDMode="Static" meta:resourcekey="btnLikeResource1"  />
                    <asp:HyperLink runat="server" ID="lnkLikesNumber" Text="Like a 0 cookers" CssClass="lnkIngredient  lblInfoLike" 
                        ClientIDMode="Static"></asp:HyperLink>
                     <asp:Panel ID="pnlLikeDetails" runat="server" CssClass="boxLikeDetails-hide" ClientIDMode="Static">
                         <div id="closeDetailPannel">
                             <img id="imgCloseDetail" class="imgCloseDetail" src="/Images/deleteX.png" onclick="CloseDetails('pnlLikeDetails');" />
                         </div>
                         <div id="pnlLikeDetailsInt"></div>
                     </asp:Panel>
                </asp:Panel>
                 <asp:Panel ID="pnlRecipeComplexity" runat="server" CssClass="pnlLeftInternal" 
                    meta:resourcekey="pnlRecipeComplexityResource1">
                   <div class="imgIconInfo"><MyCtrl:RecipeComplexity ID="rcRecipe" runat="server" 
                           ControlSize="60" /></div>
                    <asp:Label ID="lblComplexity" runat="server" CssClass="lblInfo" 
                         meta:resourcekey="lblComplexityResource1"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlRecipeOwner" runat="server" CssClass="pnlLeftInternal" 
                    meta:resourcekey="pnlRecipeOwnerResource1">
                    <asp:Image ID="imgRecipeOwner" runat="server" 
                        ImageUrl="/Images/icon-user-color-Orange.png" CssClass="imgUserIconInfo" 
                        Width="55px" Height="55px" meta:resourcekey="imgRecipeOwnerResource1" />
                    <asp:HyperLink ID="lnkRecipeOwner" runat="server" CssClass="lnkRecipeOwner" 
                        meta:resourcekey="lnkRecipeOwnerResource1"></asp:HyperLink>
                </asp:Panel>
                <asp:Panel ID="pnlPortionKcal" runat="server" CssClass="pnlLeftInternal" 
                    meta:resourcekey="pnlPortionKcalResource1">
                    <asp:Image ID="imgKcal" runat="server" ToolTip="Kcal a porzione" 
                        ImageUrl="/Images/ico-bilancia.png" CssClass="imgIconInfo MyTooltip" 
                        Width="60px" Height="60px" meta:resourcekey="imgKcalResource1" />
                    <asp:Label ID="lblKcal" runat="server" CssClass="lblInfoKcal" 
                        meta:resourcekey="lblKcalResource1"></asp:Label>
                    <asp:Label ID="lblKcalLabel" runat="server" Text="Kcal" 
                        CssClass="lblInfoKcalLabel" meta:resourcekey="lblKcalLabelResource1"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlNumPeople" runat="server" CssClass="pnlLeftInternal" 
                    meta:resourcekey="pnlNumPeopleResource1">
                    <asp:Image ID="imgNumPeople" runat="server" 
                        ImageUrl="/Images/icon-groups.png" ToolTip="Ricetta per" 
                        CssClass="imgIconInfo MyTooltip" Width="60px" Height="60px" 
                        meta:resourcekey="imgNumPeopleResource1" />
                    <asp:Label ID="lblNumPeopleSingular" runat="server" Text="{0} Persona" 
                        CssClass="lblInfo" meta:resourcekey="lblNumPeopleSingularResource1"></asp:Label>
                    <asp:Label ID="lblNumPeoplePlural" runat="server" Text="{0} Persone" 
                        CssClass="lblInfo" meta:resourcekey="lblNumPeoplePluralResource1"></asp:Label>
                </asp:Panel>
                 <asp:Panel ID="pnlPreparationTime" runat="server" CssClass="pnlLeftInternal" 
                    meta:resourcekey="pnlPreparationTimeResource1">
                    <asp:Image ID="imgPreparation" runat="server" 
                         ImageUrl="/Images/icon-preparation-time.png" ToolTip="Tempo di Preparazione" 
                         CssClass="imgIconInfo MyTooltip" Width="60px" Height="60px" 
                         meta:resourcekey="imgPreparationResource1" />
                    <asp:Label ID="lblPreparation" runat="server" Text="{0} minuti" 
                         CssClass="lblInfo" meta:resourcekey="lblPreparationResource1"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlCookingTime" runat="server" CssClass="pnlLeftInternal" 
                    meta:resourcekey="pnlCookingTimeResource1">
                    <asp:Image ID="imgCookingTime" runat="server" 
                        ImageUrl="/Images/icon-cooking-time.png" ToolTip="Tempo di Cottura" 
                        CssClass="imgIconInfo MyTooltip" Width="60px" Height="60px" 
                        meta:resourcekey="imgCookingTimeResource1" />
                    <asp:Label ID="lblCookingTime" runat="server" Text="{0} minuti" 
                        CssClass="lblInfo" meta:resourcekey="lblCookingTimeResource1"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlRecipeRegion" runat="server" CssClass="pnlLeftInternal" 
                    meta:resourcekey="pnlRecipeRegionResource1">
                    <asp:Image ID="imgRegion" runat="server" ImageUrl="/Images/icon-location.png" 
                        ToolTip="Origine Ricetta" CssClass="imgIconInfo MyTooltip" Width="60px" 
                        Height="60px" meta:resourcekey="imgRegionResource1" />
                    <asp:Label ID="lblRegion" runat="server" Text="Non specificata" 
                        CssClass="lblInfo" meta:resourcekey="lblRegionResource1"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlCookingType" runat="server" CssClass="pnlLeftInternal" 
                    meta:resourcekey="pnlCookingTypeResource1">
                    <asp:Image ID="imgCookingType" runat="server" 
                        ImageUrl="/Images/icon-cooking-type.png" ToolTip="Tipo di Cottura" 
                        CssClass="imgIconInfo MyTooltip" Width="60px" Height="60px" 
                        meta:resourcekey="imgCookingTypeResource1" />
                    <asp:Label ID="lblCookingType" runat="server" Text="Non specificata" 
                        CssClass="lblInfo" meta:resourcekey="lblCookingTypeResource1"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlPreservation" runat="server" CssClass="pnlLeftInternal">
                    <asp:Image ID="imgPreservation" runat="server" 
                        ImageUrl="/Images/icon-preservation.png" ToolTip="" 
                        CssClass="imgIconInfo MyTooltip" Width="60px" Height="60px" 
                        meta:resourcekey="imgPreservationResource1" />
                    <asp:Label ID="lblPreservation" runat="server" Text="Non specificata" CssClass="lblInfo"></asp:Label>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="pnlRightPanel" runat="server" CssClass="pnlRightPanel" 
                meta:resourcekey="pnlRightPanelResource1">
                <asp:Panel ID="pnlRecipeBookManage" runat="server" 
                    CssClass="pnlRecipeBookManage" meta:resourcekey="pnlRecipeBookManageResource1">
                    <asp:ImageButton ID="btnGoBack" runat="server" 
                        CssClass="MyTooltip btnRecipeBook" ToolTip="Torna Indietro" 
                        ImageUrl="/Images/icon-goBack.png" Visible="False" 
                        OnClick="btnGoBack_Click" Width="35px" Height="35px" 
                        meta:resourcekey="btnGoBackResource1"/>
                    <asp:ImageButton ID="btnAddToRecipeBook" runat="server" 
                        CssClass="MyTooltip btnRecipeBook" ToolTip="Aggiungi al tuo ricettario" 
                        ImageUrl="/Images/icon-addToRecipeBook.png" Visible="False" 
                        onclick="btnAddToRecipeBook_Click" Width="35px" Height="35px" 
                        meta:resourcekey="btnAddToRecipeBookResource1"/>
                    <asp:ImageButton ID="btnRemoveFromRecipeBook" runat="server" 
                        CssClass="MyTooltip btnRecipeBook" ToolTip="Rimuovi dal tuo ricettario" 
                        ImageUrl="/Images/icon-removeFromRecipeBook.png" Visible="False" 
                        onclick="btnRemoveFromRecipeBook_Click" Width="35px" Height="35px" 
                        meta:resourcekey="btnRemoveFromRecipeBookResource1"/>
                    <asp:ImageButton ID="btnAddYourRecipe" runat="server" 
                        CssClass="MyTooltip btnRecipeBook" ToolTip="Aggiungi la tua versione di questa ricetta" 
                        ImageUrl="/Images/icon-AddRecipe-color.png" Visible="False" 
                        onclick="btnAddYourRecipe_Click" Width="39px" Height="39px" 
                        meta:resourcekey="btnAddYourRecipeResource1"/>
                    <asp:ImageButton ID="btnUserCooking" runat="server" 
                        CssClass="MyTooltip btnRecipeBook" ToolTip="Lo sto cucinando" 
                        ImageUrl="/Images/icon-cooking.png" Visible="False" 
                       onclick="btnUserCooking_Click" Width="35px" Height="35px" 
                        meta:resourcekey="btnUserCookingResource1"/>
                    <asp:ImageButton ID="btnShareFacebook" runat="server" 
                        CssClass="MyTooltip btnRecipeBook" ToolTip="Condividi su Facebook" 
                        ImageUrl="/Images/fb.png" Visible="False" 
                        onclick="btnShareFacebook_Click" Width="35px" Height="35px" 
                        meta:resourcekey="btnShareFacebookResource1"/>
                    <asp:ImageButton ID="btnShareTwitter" runat="server" 
                        CssClass="MyTooltip btnRecipeBook" ToolTip="Condividi su Twitter" 
                        ImageUrl="/Images/tw.png" Visible="False" 
                        onclick="btnShareTwitter_Click" Width="36px" Height="36px" 
                        meta:resourcekey="btnShareTwitterResource1"/>
                </asp:Panel>
                <asp:Panel ID="pnlRecipeName" runat="server" CssClass="pnlRightInternalTitle" 
                    meta:resourcekey="pnlRecipeNameResource1">
                    <h1><asp:Label ID="lblRecipeName" runat="server" CssClass="lblRecipeNameBig" 
                        meta:resourcekey="lblRecipeNameResource1"></asp:Label></h1>
                    <MyCtrl:Rating runat="server" ID="rtgRecipe1" />
                </asp:Panel><br />
                <asp:Panel ID="pnlRecipeIngredients" runat="server" CssClass="pnlRightInternal" 
                    meta:resourcekey="pnlRecipeIngredientsResource1">
                    <asp:Repeater ID="repRecipeIngredient" runat="server">
                        <ItemTemplate>
                            <MyCtrl:ShowRecipeIngr ID="sriRecipeIngr" runat="server" IDIngredient='<%# Eval("Ingredient.IDIngredient") %>'
                                IDRecipe='<%# Eval("Recipe.IDRecipe") %>' IDRecipeIngredient='<%# Eval("IDRecipeIngredient") %>'
                                IsPrincipal='<%# Eval("IsPrincipalIngredient") %>' Quantity='<%# Eval("Quantity") %>'
                                QuantityNotSpecified='<%# Eval("QuantityNotSpecified") %>' QuantityNotStd='<%# Eval("QuantityNotStd") %>'
                                QuantityNotStdType='<%# Eval("QuantityNotStdType.IDQuantityNotStd") %>' QuantityType='<%# Eval("QuantityType.IDIngredientQuantityType") %>'
                                ShowInvalidIngr="false" ShowPrincipalIngr="false" RecipeIngredientGroupNumber='<%# Eval("RecipeIngredientGroupNumber") %>'
                                RecipeIngredientGroupNumberChange='<%# Eval("RecipeIngredientGroupNumberChange") %>' IDLanguage='<%#hfIDLanguage.Value %>' 
                                EditIngredientRelevance="false" ShowInfoPanel="true" ShowEditButton="false" DeleteButtonToolTip="Cancella Ingrediente da questa Ricetta"
                                DeleteButtonOnClientClick="return JCOnfirm(this,'Cancella','Eliminare questo ingrediente?','Sicuro?','Annulla');" />
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <div class="pnlStepSeparator"></div><div class="pnlStepSeparator"></div>
                <asp:Panel ID="pnlRecipeNutritionalInfoIcon" runat="server" 
                    CssClass="pnlRightInternal" 
                    meta:resourcekey="pnlRecipeNutritionalInfoIconResource1">
                    <asp:Panel ID="pnlVegan" runat="server" CssClass="pnlIcon" 
                        meta:resourcekey="pnlVeganResource1">
                        <asp:Image ID="imgVegan" runat="server" ImageUrl="/Images/vegan-gray.png" 
                            CssClass="imgIcon" Width="40px" Height="40px" 
                            meta:resourcekey="imgVeganResource1" />
                        <asp:Label ID="lblVegan" runat="server" Text="Vegana" CssClass="lblIcon" 
                            Width="150px" meta:resourcekey="lblVeganResource1"></asp:Label>
                    </asp:Panel>
                    <asp:Panel ID="pnlVegetarian" runat="server" CssClass="pnlIcon" 
                        meta:resourcekey="pnlVegetarianResource1">
                        <asp:Image ID="imgVegetarian" runat="server" 
                            ImageUrl="/Images/vegetarian-gray.png" CssClass="imgIcon" Width="40px" 
                            Height="40px" meta:resourcekey="imgVegetarianResource1" />
                        <asp:Label ID="lblVegetarian" runat="server" Text="Vegetariana" 
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
                    <div class="pnlStepSeparator"></div><div class="pnlStepSeparator"></div><div class="pnlStepSeparator"></div><div class="pnlStepSeparator"></div>
                     <asp:Panel ID="pnlRecipeNutritionalInfo" runat="server" 
                    CssClass="pnlRightInternal" 
                    meta:resourcekey="pnlRecipeNutritionalInfoResource1">
                    <asp:Panel ID="pnlProteins" runat="server" CssClass="pnlIcon" 
                             meta:resourcekey="pnlProteinsResource1">
                            <asp:Label ID="lblProteins" runat="server" Text="{0}" 
                                CssClass="lblNutritionalFactsInfo" meta:resourcekey="lblProteinsResource1"></asp:Label>
                            <asp:Label ID="lblProteinsTitle" runat="server" Text="gr di Proteine" 
                                CssClass="lblNutritionalFactsInfoTitle" 
                                meta:resourcekey="lblProteinsTitleResource1"></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="pnlFats" runat="server" CssClass="pnlIcon" 
                             meta:resourcekey="pnlFatsResource1">
                            <asp:Label ID="lblFats" runat="server" Text="{0}" 
                                CssClass="lblNutritionalFactsInfo" meta:resourcekey="lblFatsResource1"></asp:Label>
                            <asp:Label ID="lblFatsTitle" runat="server" Text="gr di Grassi" 
                                CssClass="lblNutritionalFactsInfoTitle" 
                                meta:resourcekey="lblFatsTitleResource1"></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="pnlCarbohydrates" runat="server" CssClass="pnlIcon" 
                             meta:resourcekey="pnlCarbohydratesResource1">
                            <asp:Label ID="lblCarbohydrates" runat="server" Text="{0}" 
                                CssClass="lblNutritionalFactsInfo" meta:resourcekey="lblCarbohydratesResource1"></asp:Label>
                            <asp:Label ID="lblCarbohydratesTitle" runat="server" Text="gr di Carboidrati" 
                                CssClass="lblNutritionalFactsInfoTitle" 
                                meta:resourcekey="lblCarbohydratesTitleResource1"></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="pnlAlcohol" runat="server" CssClass="pnlIcon" 
                             meta:resourcekey="pnlAlcoholResource1">
                            <asp:Label ID="lblAlcohol" runat="server" Text="{0}" 
                                CssClass="lblNutritionalFactsInfo" meta:resourcekey="lblAlcoholResource1"></asp:Label>
                            <asp:Label ID="lblAlcoholTitle" runat="server" Text="gr di Alcool" 
                                CssClass="lblNutritionalFactsInfoTitle" 
                                meta:resourcekey="lblAlcoholTitleResource1"></asp:Label>
                        </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="pnlPreparationStepsTitle" runat="server" 
                    CssClass="pnlRightInternal" 
                    meta:resourcekey="pnlPreparationStepsTitleResource1">
                    <asp:Label ID="lblPreparationStepsTitle" runat="server" Text="Preparazione" 
                        CssClass="lblPreparationStepsTitle" 
                        meta:resourcekey="lblPreparationStepsTitleResource1"></asp:Label>
                    <asp:Label ID="lblAutoTranslate" runat="server" Text="" CssClass="lblAutoTranslate"  
                        meta:resourcekey="lblAutoTranslateResource1" Visible="false"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlPreparationSteps" runat="server" CssClass="pnlRightInternalScroll" 
                    meta:resourcekey="pnlPreparationStepsResource1">
                </asp:Panel>
                <asp:Panel ID="pnlRecipeBeverage" runat="server" CssClass="pnlRightInternal" 
                    meta:resourcekey="pnlRecipeBeverageResource1">
                    <div class="pnlStepSeparator"></div><div class="pnlStepSeparator"></div><div class="pnlStepSeparator"></div>
                    <asp:Image ID="imgRecipeBeverage" runat="server" Width="60px" Height="60px" ImageUrl="/Images/icon-Beverage.png"
                        ToolTip="Bevande Consigliate" CssClass="popbox imgIconInfo MyTooltip" 
                        meta:resourcekey="imgRecipeBeverageResource1" />
                    <asp:Label ID="lblRecipeBeverage" runat="server" Text="Bevande Consigliate" 
                        CssClass="lblOtherInfo" Height="60px" Width="100px" Visible="False" 
                        meta:resourcekey="lblRecipeBeverageResource1"></asp:Label>
                    <div style="float:left; width:410px; padding-bottom:10px; margin-left:5px; margin-top:5px;">
                    <asp:Repeater ID="rptRecipeBeverage" runat="server">
                        <ItemTemplate>
                            <MyCtrl:RecipeBeverage runat="server" ID="brBeverageRecipe" IDBeverageRecipe='<%# Eval("IDBeverageRecipe") %>'
                                IDRecipe='<%# Eval("IDRecipe") %>' IDBeverage='<%# Eval("IDBeverage.IDBeverage") %>'
                                IDUserSuggestedBy='<%# Eval("IDUser.IDUser") %>' DateSuggestion='<%# Eval("DateSuggestion") %>'
                                BeverageRecipeAvgRating='<%# Eval("AvgRating") %>' IDLanguage='<%#hfIDLanguage.Value %>' ShowEditButton="false"
                                ShowInfoPanel="false" />
                        </ItemTemplate>
                    </asp:Repeater></div>
                </asp:Panel>
                <asp:Panel ID="pnlRecipeSuggestion" runat="server" CssClass="pnlRightInternal pnlRightInternalScroll" 
                    meta:resourcekey="pnlRecipeSuggestionResource1">
                     <div class="pnlStepSeparator"></div><div class="pnlStepSeparator"></div>
                    <asp:Image ID="imgRecipeSuggestion" runat="server" Width="60px" Height="60px" 
                        ImageUrl="/Images/icon-suggestion.png" ToolTip="Suggerimenti" 
                        CssClass="imgIconInfo MyTooltip" 
                        meta:resourcekey="imgRecipeSuggestionResource1" />
                    <asp:Label ID="lblRecipeSuggestion" runat="server" CssClass="lblOtherInfo" 
                        meta:resourcekey="lblRecipeSuggestionResource1"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlRecipeHistory" runat="server" CssClass="pnlRightInternal pnlRightInternalScroll" 
                    meta:resourcekey="pnlRecipeHistoryResource1">
                    <div class="pnlStepSeparator"></div><div class="pnlStepSeparator"></div>
                    <asp:Image ID="imgRecipeHistory" runat="server" Width="60px" Height="60px" 
                        ImageUrl="/Images/icon-history.png" ToolTip="Storia della ricetta" 
                        CssClass="imgIconInfo MyTooltip" meta:resourcekey="imgRecipeHistoryResource1" />
                    <asp:Label ID="lblRecipeHistory" runat="server" CssClass="lblOtherInfo" 
                        meta:resourcekey="lblRecipeHistoryResource1"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlSpecialNote" runat="server" CssClass="pnlRightInternal pnlRightInternalScroll">
                    <div class="pnlStepSeparator"></div><div class="pnlStepSeparator"></div>
                     <asp:Image ID="imgSpecialNote" runat="server" Width="60px" Height="60px" 
                        ImageUrl="/Images/icon-other-note.png" ToolTip="Altre Note" 
                        CssClass="imgIconInfo MyTooltip" meta:resourcekey="imgSpecialNoteResource1" />
                    <asp:Label ID="lblSpecialNote" runat="server" CssClass="lblOtherInfo"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlContainerComments" runat="server" CssClass="pnlRightInternal">
                    <div class="pnlStepSeparator"></div><div class="pnlStepSeparator"></div>
                    <asp:Panel ID="pnlAddComment" runat="server" CssClass="pnlAddComment">
                        <asp:Label ID="lblWriteComment" runat="server" Text="Aggiungi un commento"
                            meta:resourcekey="lblWriteCommentResource1"></asp:Label>
                        <div class="pnlStepSeparator"></div>
                        <asp:TextBox ID="txtNewComment" CssClass="AutoSizeTextArea" runat="server" TextMode="MultiLine"
                            Rows="1" Width="430px" PlaceHolder="" ClientIDMode="Static" MaxLength="550"></asp:TextBox>
                        <asp:ImageButton ID="ibtnComment" runat="server"
                            ImageUrl="/Images/icon-forward.png" Width="34px" Height="34px" OnClientClick="AddComment('pnlComments', 'pnlBackground', 'pnlContent','ibtnComment'); return false;" />
                    </asp:Panel>
                    <div class="pnlStepSeparator"></div><div class="pnlStepSeparator"></div>
                    <asp:Panel ID="pnlComments" runat="server" ClientIDMode="Static">
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    <asp:HyperLink ID="lnkNavigateNextRecipe" runat="server" CssClass="btnNavigateNextRecipe" Visible="false">
        <img alt="next" src="/Images/icon-QuickNext.png"/>
    </asp:HyperLink>
    <asp:HyperLink ID="lnkNavigatePrevRecipe" runat="server" CssClass="btnNavigatePrevRecipe" Visible="false">
        <img alt="prev" src="/Images/icon-QuickPrev.png"/>
    </asp:HyperLink>
    <asp:Panel ID="pnlLeftContainer" runat="server" ClientIDMode="static" CssClass="pnlLeftContainer">
        <asp:Panel ID="pnlLeft1" runat="server" ClientIDMode="static" CssClass="pnlLeftContainerInternal1">
            <asp:Label ID="lblBanner1" runat="server" Text="Banner1" CssClass="lblContainerTitles" meta:resourcekey="lblBanner1Resource1"></asp:Label>
            <a href="/user/howitworks.aspx"><img src="/Images/icon-food-blogger.png" alt="HowItWorks" style="display:block;width:80px; height:80px; margin-left:auto; margin-right:auto; margin-top:30px;"/></a>
        </asp:Panel>
        <asp:Panel ID="pnlLeft2" runat="server" ClientIDMode="static" CssClass="pnlLeftContainerInternal2">
            <asp:Label ID="lblUsers" runat="server" Text="Utenti da seguire" CssClass="lblContainerTitles" meta:resourcekey="lblUsersResource1"></asp:Label>
            <div id="boxUsers">
            </div>
            <div id="boxLoadingUsers">
                <asp:Image ID="imgLoadUsers" runat="server" ImageUrl="/Images/loadingLineOrange.gif"
                        Height="20px" Width="220px" />
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnlRightContainer" runat="server" ClientIDMode="static" CssClass="pnlRightContainer">
        <asp:Panel ID="pnlRight1" runat="server" ClientIDMode="static" CssClass="pnlRightContainerInternal1" Visible="false">
            <asp:Label ID="lblBanner2" runat="server" Text="Banner2" CssClass="lblContainerTitles" meta:resourcekey="lblBanner2Resource1"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlRight2" runat="server" ClientIDMode="static" CssClass="pnlRightContainerInternal2">
            <asp:Label ID="lblRecipesList" runat="server" Text="Altre Ricette" CssClass="lblContainerTitles" meta:resourcekey="lblRecipesListResource1"></asp:Label>
            <div id="boxSimilarRecipes">
            </div>
            <div id="boxLoadingSimilarRecipes">
                <asp:Image ID="imgLoading" runat="server" ImageUrl="/Images/loadingLineOrange.gif"
                        Height="20px" Width="220px"/>
            </div>
            <div class="lblContainerTitles">
                <asp:HyperLink ID="lnkOtherRecipes" runat="server" CssClass="lnkIngredient" meta:resourcekey="lnkOtherRecipesResource1">Vedi tutto</asp:HyperLink>
            </div>
        </asp:Panel>
    </asp:Panel>
    <script type="text/javascript" language="javascript" src="/Js/Pages/ShowRecipe.min.js"></script>
    <script language="javascript" type="text/javascript">
        $('#boxSimilarRecipes').css({ 'display': 'block' });
        $('#boxLoadingUsers').css({ 'display': 'block' });
        LoadSimilarRecipes('boxSimilarRecipes', 'boxLoadingSimilarRecipes');
        LoadUsers('boxUsers', 'boxLoadingUsers');
        RecipeLikesDeteils('pnlLikeDetails', 'pnlLikeDetailsInt', 'lnkLikesNumber');
        TextAreaAutoGrow('txtNewComment');
        ListRecipeComments('pnlComments', 'pnlBackground', 'pnlContent');
    </script>
</asp:Content>
