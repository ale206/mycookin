<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master" AutoEventWireup="true" CodeBehind="EditRecipes.aspx.cs" Inherits="MyCookinWeb.RecipeWeb.EditRecipes" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register TagName="EditImage" TagPrefix="MyCtrl" Src="~/CustomControls/ctrlEditImage.ascx" %>
<%@ Register TagName="multiUp" TagPrefix="MyCtrl" Src="~/CustomControls/ctrlMultiUpload.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="AddRecipeIngr" Src="~/CustomControls/ctrlAddRecipeIngredient.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="ShowRecipeIngr" Src="~/CustomControls/ctrlShowRecipeIngredient.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="RecipeBeverage" Src="~/CustomControls/ctrlBeverageRecipe.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="AutoComplete" Src="~/CustomControls/AutoComplete.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="Rating" Src="~/CustomControls/ctrlRating.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/ShowRecipe.min.css?ver=140316" />
    <link rel="Stylesheet" href="/Styles/jQueryUiCss/UserControl/ctrlEditImage.css" />
    <link href="/Styles/jQueryUiCss/JCrop/jquery.Jcrop.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jQueryUiCss/PopBox/popbox.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/PageStyle/IngredientPopBox.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="cntMain" ContentPlaceHolderID="cphMain" runat="server">
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            $("#pnlMainTab").tabs({
                activate: function (event, ui) {
                    $('#<%=pnlBackground.ClientID%>').height($('#<%=pnlContent.ClientID%>').height());
                }
            });
            $('#sortableStep').sortable({
                opacity: 0.8
            });
            //$('#sortableStep').disableSelection();
            $('#<%=pnlBackground.ClientID%>').height($('#<%=pnlContent.ClientID%>').height());
            AddRecipeIngredientStartAutoComplete();
            try {
                $(".MyTooltip").tooltip({
                    position: {
                        my: "center bottom-20",
                        at: "center top",
                        using: function (position, feedback) {
                            $(this).css(position);
                            $("<div>").addClass("arrow").addClass(feedback.vertical).addClass(feedback.horizontal).appendTo(this);
                        }
                    }
                });
            }
            catch (err) {
                console.log("Ops..: " + err);
            }

        }
        function GetStepOrder() {
            var sortedIDs = $('#sortableStep').sortable("toArray");
            $("#hfStepsOrder").val(sortedIDs.toString());
        }
        function CheckDraft(direction, cssClass)
        {
            if (direction == "over") {
                if ($("#chkDraft").attr('checked')) {
                    $("#chkDraft").next("label").text($("#hfPublicRecipe").val());
                    $("." + cssClass).css("color", "#00642E");
                }
                else {
                    $("#chkDraft").next("label").text($("#hfRecipeInDraft").val());
                    $("." + cssClass).css("color", "#BD141B");
                }
            }
            else {
                if ($("#chkDraft").attr('checked')) {
                    $("#chkDraft").next("label").text($("#hfRecipeInDraft").val());
                    $("." + cssClass).css("color", "#BD141B");
                }
                else {
                    $("#chkDraft").next("label").text($("#hfPublicRecipe").val());
                    $("." + cssClass).css("color", "#00642E");
                }
            }
        }
        function CheckDraftClick(cssClass)
        {
            if ($("#chkDraft").attr('checked')) {
                $("#chkDraft").next("label").text($("#hfRecipeInDraft").val());
                $("." + cssClass).css("color", "#BD141B");
            }
            else {
                $("#chkDraft").next("label").text($("#hfPublicRecipe").val());
                $("." + cssClass).css("color", "#00642E");
            }
        }
    </script>
    <asp:HiddenField ID="hfRecipePercentageCompleteBase" runat="server" Value="completa al {0}%" />
    <asp:HiddenField ID="hfIDLanguage" runat="server" />
    <asp:HiddenField ID="hfStepsOrder" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfRecipeInDraft" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfPublicRecipe" runat="server" ClientIDMode="Static" />
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="/Js/JCrop/jquery.color.js" />
            <asp:ScriptReference Path="/Js/JCrop/jquery.Jcrop.min.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server" 
        meta:resourcekey="pnlResultResource1">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server" 
            meta:resourcekey="lblResultResource1"></asp:Label>
    </asp:Panel>
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
        <asp:Panel ID="pnlContent" ClientIDMode="Static" runat="server" 
            CssClass="pnlContent" meta:resourcekey="pnlContentResource1">
            <asp:UpdatePanel ID="upnRecipeHead" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlRecipeHead" runat="server" CssClass="pnlRecipeHead" 
                        meta:resourcekey="pnlRecipeHeadResource1">
                        <asp:Label ID="lblRecipeName" runat="server" Text="Nome ricetta" 
                            CssClass="lblRecipeName" meta:resourcekey="lblRecipeNameResource1" Visible="false"></asp:Label>
                        <asp:HyperLink ID="lnkRecipeName" runat="server" CssClass="lblRecipeName">HyperLink</asp:HyperLink>
                        <asp:Label ID="lblRecipeCompletePerc" runat="server" 
                            CssClass="lblRecipeCompletePerc" 
                            meta:resourcekey="lblRecipeCompletePercResource1"></asp:Label>
                        <asp:ImageButton ID="ibtnDelete"
                            OnClick="ibtnDelete_Click" ToolTip="Elimina questo post"
                            OnClientClick="return JCOnfirm(this, 'Remove', 'Sicuro', 'SI', 'NO');" ImageUrl="/Images/deleteX.png"
                            runat="server" Width="16px" Height="16px" CssClass="ibtnDelete" 
                            meta:resourcekey="ibtnDeleteResource1" />
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Panel ID="pnlMainTab" runat="server" ClientIDMode="Static" 
                CssClass="pnlMainTab" meta:resourcekey="pnlMainTabResource1">
                <ul>
                    <li>
                        <asp:HyperLink ID="lnkGeneralInfo" runat="server" Text="Ricetta" 
                            NavigateUrl="#upnlGeneralInfo" meta:resourcekey="lnkGeneralInfoResource1"></asp:HyperLink></li>
                    <li>
                        <asp:HyperLink ID="lnkIngredients" runat="server" Text="Ingredienti" 
                            NavigateUrl="#upnlIngredients" meta:resourcekey="lnkIngredientsResource1"></asp:HyperLink></li>
                    <li>
                        <asp:HyperLink ID="lnkRecipeSteps" runat="server" Text="Procedimento" 
                            NavigateUrl="#upnlRecipeSteps" meta:resourcekey="lnkRecipeStepsResource1"></asp:HyperLink></li>
                    <li>
                        <asp:HyperLink ID="lnkRecipeTags" runat="server" Text="Info Aggiuntive" 
                            NavigateUrl="#upnlRecipeTags" meta:resourcekey="lnkRecipeTagsResource1"></asp:HyperLink></li>
                    <li>
                        <asp:HyperLink ID="lnkOtherInfo" runat="server" Text="Suggerimenti" 
                            NavigateUrl="#upnlOtherInfo" meta:resourcekey="lnkOtherInfoResource1"></asp:HyperLink></li>
                </ul>
                <asp:UpdatePanel ID="upnlGeneralInfo" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <asp:Panel ID="pnlGeneralInfo" runat="server" 
                            meta:resourcekey="pnlGeneralInfoResource1">
                                    <asp:Panel ID="pnlManageRecipePhoto" runat="server" 
                                        CssClass="pnlManageRecipePhoto" 
                                        meta:resourcekey="pnlManageRecipePhotoResource1">
                                        <asp:UpdatePanel ID="upnGeneralInfo" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <MyCtrl:EditImage ID="imgRecipe" runat="server" />
                                                <asp:Image ID="imgNoPhoto" runat="server" Width="150px" Height="150px" 
                                                    Visible="False" meta:resourcekey="imgNoPhotoResource1" />
                                                <asp:Panel ID="pnlChangeImage" runat="server" 
                                                    meta:resourcekey="pnlChangeImageResource1">
                                                    <MyCtrl:multiUp ID="multiup" runat="server" OnFilesUploaded="FileUploaded" />
                                                </asp:Panel>
                                                <asp:Label ID="lblGeneralUploadError" runat="server" Text="Foto troppo grande" 
                                                    CssClass="lblError" Width="150px" Visible="False" 
                                                    meta:resourcekey="lblGeneralUploadErrorResource1"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                        <asp:Panel ID="pnlRecipeName" runat="server" CssClass="pnlRecipeName" 
                                        meta:resourcekey="pnlRecipeNameResource1">
                            <div class="pnlTable">
                                <div class="pnlTableRow">
                                    <div class="pnlTableCol">
                                        <asp:Label ID="lblChangeRecipeName" runat="server" CssClass="editRecipeLabel" 
                                            Text="Nome Ricetta" meta:resourcekey="lblChangeRecipeNameResource1"></asp:Label>
                                    </div>
                                </div>
                                <div class="pnlTableRow">
                                    <div class="pnlTableCol">
                                        <asp:TextBox ID="txtRecipeName" runat="server" Width="430px" 
                                            onkeypress="return isSpecialHTMLChar(event)"
                                            meta:resourcekey="txtRecipeNameResource1" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="pnlTable">
                                <div class="pnlTableRow">
                                    <div class="pnlTableCol">
                                        <asp:Label ID="lblNumberOfPerson" runat="server" CssClass="editRecipeLabel" 
                                            Text="Numero di persone" meta:resourcekey="lblNumberOfPersonResource1"></asp:Label>
                                    </div>
                                    <div class="pnlTableCol">
                                        <asp:TextBox ID="txtNumberOfPerson" runat="server" MaxLength="2" onpaste="return false"
                                            onkeypress="return isNumberKey(event)" Width="60px" 
                                            meta:resourcekey="txtNumberOfPersonResource1"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="pnlTableRow">
                                    <div class="pnlTableCol">
                                        <asp:Label ID="lblPreparationTime" runat="server" CssClass="editRecipeLabel" 
                                            Text="Tempo di preparazione (minuti)" 
                                            meta:resourcekey="lblPreparationTimeResource1"></asp:Label>
                                    </div>
                                    <div class="pnlTableCol">
                                        <asp:TextBox ID="txtPreparationTimeHours" runat="server" MaxLength="2" Width="30px" onpaste="return false"
                                            onkeypress="return isNumberKey(event)" ClientIDMode="Static"></asp:TextBox>
                                        <asp:Label ID="lblHours1" runat="server" Text="ore" meta:resourcekey="lblHoursResource"></asp:Label>
                                        <asp:TextBox ID="txtPreparationTimeMinutes" runat="server" MaxLength="2" Width="30px" onpaste="return false"
                                            onkeypress="return isNumberKey(event)" onkeyup="CheckMinuteField('txtPreparationTimeHours','txtPreparationTimeMinutes');" ClientIDMode="Static"></asp:TextBox>
                                        <asp:Label ID="lblMinutes1" runat="server" Text="minuti" meta:resourcekey="lblMinutesResource"></asp:Label>
                                    </div>
                                </div>
                                <div class="pnlTableRow">
                                    <div class="pnlTableCol">
                                        <asp:Label ID="lblCookingTime" runat="server" CssClass="editRecipeLabel" 
                                            Text="Tempo di cottura" meta:resourcekey="lblCookingTimeResource1"></asp:Label>
                                    </div>
                                    <div class="pnlTableCol">
                                        <asp:TextBox ID="txtCookingTimeHours" runat="server" MaxLength="2" Width="30px" onpaste="return false"
                                            onkeypress="return isNumberKey(event)" ClientIDMode="Static"></asp:TextBox>
                                        <asp:Label ID="lblHours2" runat="server" Text="ore" meta:resourcekey="lblHoursResource"></asp:Label>
                                        <asp:TextBox ID="txtCookingTimeMinutes" runat="server" MaxLength="2" Width="30px" onpaste="return false"
                                            onkeypress="return isNumberKey(event)" onkeyup="CheckMinuteField('txtCookingTimeHours','txtCookingTimeMinutes');" ClientIDMode="Static"></asp:TextBox>
                                        <asp:Label ID="lblMinutes2" runat="server" Text="minuti" meta:resourcekey="lblMinutesResource"></asp:Label>
                                    </div>
                                </div>
                                <div class="pnlTableRow">
                                    <div class="pnlTableCol">
                                        <asp:Label ID="lblRecipeDifficulties" runat="server" CssClass="editRecipeLabel" 
                                            Text="Difficoltà ricetta" meta:resourcekey="lblRecipeDifficultiesResource1"></asp:Label>
                                    </div>
                                    <div class="pnlTableCol">
                                        <asp:DropDownList ID="ddlRecipeDifficulties" runat="server" 
                                            meta:resourcekey="ddlRecipeDifficultiesResource1">
                                            <asp:ListItem Value="1" meta:resourcekey="ListItemResource1">Facile</asp:ListItem>
                                            <asp:ListItem Value="2" meta:resourcekey="ListItemResource2">Media</asp:ListItem>
                                            <asp:ListItem Value="3" meta:resourcekey="ListItemResource3">Difficile</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="pnlTableRow">
                                    <div class="pnlTableCol">
                                        <asp:Label ID="lblHotSpicy" runat="server" CssClass="editRecipeLabel" 
                                            Text="Ricetta Piccante" meta:resourcekey="lblHotSpicyResource1"></asp:Label>
                                    </div>
                                    <div class="pnlTableCol">
                                        <asp:CheckBox ID="chkHotSpicy" runat="server" 
                                            meta:resourcekey="chkHotSpicyResource1" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upnlIngredients" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="display:block; margin-bottom:15px;">
                            <asp:Label ID="lblIngredientsTitle" CssClass="UsersNote" runat="server" Text="" meta:resourcekey="lblIngredientsTitleResource"></asp:Label>
                        </div>
                        <asp:Panel ID="pnlIngredients" runat="server" 
                            meta:resourcekey="pnlIngredientsResource1">
                            <div class="pnlTable">
                                <div class="pnlTableRow">
                                    <div class="pnlTableCol">
                                        <asp:UpdatePanel ID="upnlRecipeIngredient" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Repeater ID="repRecipeIngredient" runat="server">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="upnRecipeIngredient" UpdateMode="Conditional" runat="server">
                                                            <ContentTemplate>
                                                                <MyCtrl:ShowRecipeIngr ID="sriRecipeIngr" runat="server" IDIngredient='<%# Eval("Ingredient.IDIngredient") %>'
                                                                    IDLanguage='<%#hfIDLanguage.Value %>' IDRecipe='<%# Eval("Recipe.IDRecipe") %>' IDRecipeIngredient='<%# Eval("IDRecipeIngredient") %>'
                                                                    IsPrincipal='<%# Eval("IsPrincipalIngredient") %>' Quantity='<%# Eval("Quantity") %>'
                                                                    QuantityNotSpecified='<%# Eval("QuantityNotSpecified") %>' QuantityNotStd='<%# Eval("QuantityNotStd") %>'
                                                                    QuantityNotStdType='<%# Eval("QuantityNotStdType.IDQuantityNotStd") %>' QuantityType='<%# Eval("QuantityType.IDIngredientQuantityType") %>'
                                                                    ShowInvalidIngr="true" ShowPrincipalIngr="false" RecipeIngredientGroupNumber='<%# Eval("RecipeIngredientGroupNumber") %>'
                                                                    RecipeIngredientGroupNumberChange='<%# Eval("RecipeIngredientGroupNumberChange") %>' ShowInfoPanel="false"
                                                                    EditIngredientRelevance="true" ShowEditButton="true" DeleteButtonToolTip="Cancella Ingrediente da questa Ricetta"
                                                                    DeleteButtonOnClientClick="return JCOnfirm(this,'Cancella','Eliminare questo ingrediente?','Sicuro?','Annulla');" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ariIngredient" EventName="IngredientAdded" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="pnlTableRow">
                                    <div class="pnlTableCol">
                                        <asp:UpdatePanel ID="upnAddRecipeIngredient" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <MyCtrl:AddRecipeIngr ID="ariIngredient" runat="server" 
                                                    OnIngredientAdded="ariIngredient_IngredientAdded" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upnlRecipeSteps" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblRecipeSteps" runat="server" Text="Descrivi qui come si prepara la tua ricetta"
                            CssClass="editRecipeLabel" meta:resourcekey="lblRecipeStepsResource1"></asp:Label>
                        <div class="pnlSeparator"></div>
                        <asp:UpdatePanel ID="upnlDynSteps" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <ul id="sortableStep" runat="server" clientidmode="Static">
                                </ul>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="multiupSteps" EventName="FilesUploaded" />
                                <asp:AsyncPostBackTrigger ControlID="btnAddNoPhotoStep" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="upnlDynStepsMng" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <MyCtrl:multiUp ID="multiupSteps" runat="server" OnFilesUploaded="StepsUploaded" SelectFilesCssClass="MyButtonSmall" OnSelectFileClientClick="GetStepOrder()" />
                                <asp:Button ID="btnAddNoPhotoStep" runat="server" Text="Aggiungi passaggio senza foto" CssClass="MyButtonSmall" OnClick="btnAddNoPhotoStep_Click" OnClientClick="GetStepOrder()"  meta:resourcekey="btnAddNoPhotoStep" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                         <asp:UpdateProgress ID="upRecipeStepsLoading" runat="server" 
                            AssociatedUpdatePanelID="upnlDynStepsMng">
                            <ProgressTemplate>
                                <asp:Label ID="lblCreatingSteps" runat="server" Text="Aggiunta in corso" 
                                    CssClass="btnSaveRecipe" meta:resourcekey="lblCreatingSteps"></asp:Label>
                                <br /><asp:Image ID="imgStepsLoading" runat="server" ImageUrl="/Images/loadingLineOrange.gif"
                                    Height="20px" Width="220px" CssClass="btnSaveRecipe"/>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upnlRecipeTags" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="pnlTable">
                            <asp:UpdatePanel ID="upnlDynamicProperty" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="pnlSeparator">
                            </div>
                            <asp:Label ID="lblAddBeverage" runat="server" 
                                Text="Suggerisci una o più bevande da abbinare a questa ricetta" 
                                CssClass="editRecipeLabel" meta:resourcekey="lblAddBeverageResource1"></asp:Label>
                            <div class="pnlTableRow">
                                <div class="pnlTableCol">
                                    <asp:UpdatePanel ID="upnRecipeBeverage" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:Repeater ID="rptRecipeBeverage" runat="server">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <MyCtrl:RecipeBeverage runat="server" ID="brBeverageRecipe" IDBeverageRecipe='<%# Eval("IDBeverageRecipe") %>'
                                                                IDRecipe='<%# Eval("IDRecipe") %>' IDBeverage='<%# Eval("IDBeverage.IDBeverage") %>'
                                                                IDUserSuggestedBy='<%# Eval("IDUser.IDUser") %>' DateSuggestion='<%# Eval("DateSuggestion") %>'
                                                                BeverageRecipeAvgRating='<%# Eval("AvgRating") %>' IDLanguage="2" ShowEditButton="true"
                                                                ShowInfoPanel="false" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAddBeverageRecipe" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="pnlTableRow">
                                <asp:UpdatePanel ID="upnAddRecipeBeverage" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="pnlTableCol width410px">
                                            <MyCtrl:AutoComplete runat="server" ID="acBeverage" />
                                        </div>
                                        <div class="pnlTableCol">
                                         <div style="display:inline-block;top:10px;vertical-align:middle;margin-bottom:5px;"><img src="/Images/icon-add.png" alt="Add" width="15" height="15"/></div>
                                         <div style="display:inline-block;">
                                            <asp:Button ID="btnAddBeverageRecipe" runat="server" Text="Aggiungi bevanda" 
                                                OnClick="btnAddBeverageRecipe_Click" CssClass="MyButtonSmall" 
                                                meta:resourcekey="btnAddBeverageRecipeResource1" />
                                                </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upnlOtherInfo" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="pnlTable">
                            <div class="pnlTableRow">
                                <div class="pnlTableCol">
                                    <asp:Label ID="lblRecipeHistory" runat="server" CssClass="IngredientInfoFieldTitle"
                                        Text="Storia della ricetta" meta:resourcekey="lblRecipeHistoryResource1"></asp:Label>
                                </div>
                                <div class="pnlTableCol">
                                    <asp:TextBox ID="txtRecipeHistory" runat="server" TextMode="MultiLine" Rows="4" 
                                        onkeypress="return isSpecialHTMLChar(event)"
                                        Columns="60" meta:resourcekey="txtRecipeHistoryResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="pnlTableRow">
                                <div class="pnlTableCol">
                                    <asp:Label ID="lblRecipeNote" runat="server" CssClass="IngredientInfoFieldTitle"
                                        Text="Note particolari" meta:resourcekey="lblRecipeNoteResource1"></asp:Label>
                                </div>
                                <div class="pnlTableCol">
                                    <asp:TextBox ID="txtRecipeNote" runat="server" TextMode="MultiLine" Rows="4" 
                                        onkeypress="return isSpecialHTMLChar(event)"
                                        Columns="60" meta:resourcekey="txtRecipeNoteResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="pnlTableRow">
                                <div class="pnlTableCol">
                                    <asp:Label ID="lblRecipeSuggestion" runat="server" CssClass="IngredientInfoFieldTitle"
                                        Text="Suggerimenti" meta:resourcekey="lblRecipeSuggestionResource1"></asp:Label>
                                </div>
                                <div class="pnlTableCol">
                                    <asp:TextBox ID="txtRecipeSuggestion" runat="server" TextMode="MultiLine" Rows="4"
                                        onkeypress="return isSpecialHTMLChar(event)"
                                        Columns="60" meta:resourcekey="txtRecipeSuggestionResource1"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <div class="pnlSeparator"></div>
            <div style="display:block;">
                <div style="float:left; margin-left:60px; margin-top:10px;">
                    <asp:CheckBox ID="chkDraft" runat="server" ClientIDMode="Static" 
                        CssClass="" Text=""/>
                </div>
                <div style="float:right; margin-right:60px;">
                    <asp:UpdatePanel ID="upnSaveRecipe" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlSaveRecipe" runat="server" CssClass="pnlSaveRecipe" 
                                meta:resourcekey="pnlSaveRecipeResource1">
                                <asp:Button ID="btnSaveRecipe" runat="server" Text="Salva Ricetta" CssClass="MyButton btnSaveRecipe"
                                    ClientIDMode="Static" OnClick="btnSaveRecipe_Click" OnClientClick="GetStepOrder()"
                                    meta:resourcekey="btnSaveRecipeResource1" />
                                <asp:UpdateProgress ID="upSavingRecipe" runat="server" 
                                    AssociatedUpdatePanelID="upnSaveRecipe">
                                    <ProgressTemplate>
                                        <asp:Label ID="lblSaving" runat="server" Text="Sto salvando la tua ricetta" 
                                            CssClass="btnSaveRecipe" meta:resourcekey="lblSavingResource1"></asp:Label>
                                        <br /><asp:Image ID="imgLoading" runat="server" ImageUrl="/Images/loadingLineOrange.gif"
                                            Height="20px" Width="220px" CssClass="btnSaveRecipe" 
                                            meta:resourcekey="imgLoadingResource1"/>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
