<%@ Page Title="Manage Recipe" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master" AutoEventWireup="true" CodeBehind="MngRecipes.aspx.cs" Inherits="MyCookinWeb.RecipeWeb.MngRecipes" %>
<%@ Register TagPrefix="MyCtrl" TagName="ImageEdit" Src="~/CustomControls/AddRemoveImage.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="AddRecipeIngr" Src="~/CustomControls/ctrlAddRecipeIngredient.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="ShowRecipeIngr" Src="~/CustomControls/ctrlShowRecipeIngredient.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="AutoSuggest" Src="~/CustomControls/AutoSuggestMultiValue.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="RecipeBeverage" Src="~/CustomControls/ctrlBeverageRecipe.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="AutoComplete" Src="~/CustomControls/AutoComplete.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="Rating" Src="~/CustomControls/ctrlRating.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
<link rel="Stylesheet" href="\Styles\SiteStyle\_OLD_\Recipe.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Js/JCrop/jquery.color.js" />
            <asp:ScriptReference Path="~/Js/JCrop/jquery.Jcrop.js" />
            <asp:ScriptReference Path="~/Js/JCrop/jquery.Jcrop.min.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <script language="javascript" type="text/javascript">
    $(function () {
        $("#pnlMainTab").tabs();
    });
    function pageLoad() {
        AddRecipeIngredientStartAutoComplete();
    }
    </script>
    <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server"></asp:Label>
    </asp:Panel>
    <div style="width: 90%; min-width: 1024px">
        <asp:Panel ID="pnlMainTab" runat="server" ClientIDMode="Static">
            <ul>
                <li>
                    <asp:HyperLink ID="lnkRecipeMain" runat="server" Text="ManageRecipe" NavigateUrl="#pnlRecipeAjax"></asp:HyperLink></li>
            </ul>
            <asp:UpdatePanel ID="pnlRecipeAjax" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="hfIDRecipe" runat="server" />
                    <p>
                        <asp:HyperLink ID="hlReturnToList" runat="server" NavigateUrl="/RecipeMng/RecipeDashBoard.aspx">Torna alla lista delle Ricette</asp:HyperLink><br /><br />
                        <asp:Label ID="lblRecipeTitle" runat="server" CssClass="IngredientInfoFieldTitle"
                            Text="Stai modificando:"></asp:Label>
                        <asp:Label ID="lblRecipeTitleValue" runat="server" CssClass="lblIngredientTitle"></asp:Label>
                    </p>
                    <p>
                        &nbsp;</p>
                    <div class="pnlTable">
                        <div class="pnlTableCaption">
                            Inizia scegliendo una bella immagine per questa Ricetta. Cercala su Google oppure
                            scegliene una delle tue. Potrai ritagliarla e prenderne la parte migliore.</div>
                        <div class="pnlTableRow">
                            <div class="pnlTableCol">
                                <asp:UpdatePanel ID="upnRecipePhoto" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <MyCtrl:ImageEdit ID="upshImgRecipe" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="pnlTable">
                        <div class="pnlTableCaption">
                            Se ritieni che questa non sia una buona ricetta o sia troppo simile ad un'altra
                            togli il flag sotto.
                        </div>
                        <div class="pnlTableRow">
                            <div class="pnlTableCol">
                                <asp:Label ID="lblRecipeEnabled" runat="server" CssClass="IngredientInfoFieldTitle"
                                    Text="Ricetta valida?"></asp:Label>
                            </div>
                            <div class="pnlTableCol">
                                <asp:CheckBox ID="chkRecipeEnabled" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="pnlTable">
                        <div class="pnlTableCaption">
                            Informazioni generali sulla ricetta
                        </div>
                        <div class="pnlTableRow">
                            <div class="pnlTableCol">
                                <asp:Label ID="lblRecipeName" runat="server" CssClass="IngredientInfoFieldTitle"
                                    Text="Nome Ricetta"></asp:Label>
                            </div>
                            <div class="pnlTableCol">
                                <asp:TextBox ID="txtRecipeName" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="pnlTableRow">
                            <div class="pnlTableCol">
                                <asp:Label ID="lblNumberOfPerson" runat="server" CssClass="IngredientInfoFieldTitle"
                                    Text="Numero di persone"></asp:Label>
                            </div>
                            <div class="pnlTableCol">
                                <asp:TextBox ID="txtNumberOfPerson" runat="server" MaxLength="2" onpaste="return false"
                                    onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="pnlTableRow">
                            <div class="pnlTableCol">
                                <asp:Label ID="lblPreparationTime" runat="server" CssClass="IngredientInfoFieldTitle"
                                    Text="Tempo di preparazione (minuti)"></asp:Label>
                            </div>
                            <div class="pnlTableCol">
                                <asp:TextBox ID="txtPreparationTime" runat="server" MaxLength="5" onpaste="return false"
                                    onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="pnlTableRow">
                            <div class="pnlTableCol">
                                <asp:Label ID="lblCookingTime" runat="server" CssClass="IngredientInfoFieldTitle"
                                    Text="Tempo di cottura (minuti)"></asp:Label>
                            </div>
                            <div class="pnlTableCol">
                                <asp:TextBox ID="txtCookingTime" runat="server" MaxLength="3" onpaste="return false"
                                    onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="pnlTableRow">
                            <div class="pnlTableCol">
                                <asp:Label ID="lblRecipeDifficulties" runat="server" CssClass="IngredientInfoFieldTitle"
                                    Text="Difficoltà ricetta"></asp:Label>
                            </div>
                            <div class="pnlTableCol">
                                <asp:DropDownList ID="ddlRecipeDifficulties" runat="server">
                                    <asp:ListItem Value="1">Facile</asp:ListItem>
                                    <asp:ListItem Value="2">Media</asp:ListItem>
                                    <asp:ListItem Value="3">Difficile</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="pnlTableRow">
                            <div class="pnlTableCol">
                                <asp:Label ID="lblRecipeOrigin" runat="server" CssClass="IngredientInfoFieldTitle"
                                    Text="Nazione, Regione o Città di origine"></asp:Label>
                            </div>
                            <div class="pnlTableCol">
                                <asp:TextBox ID="txtRecipeOrigin" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="pnlTable">
                        <div class="pnlTableCaption">
                            Una ricetta di base ad esempio l'impasto per la frolla, la base per pizza, la crema
                            pasticcera, ecc.
                        </div>
                        <div class="pnlTableRow">
                            <div class="pnlTableCol">
                                <asp:Label ID="lblBaseRecipe" runat="server" CssClass="IngredientInfoFieldTitle" Text="Ricetta di base"></asp:Label>
                            </div>
                            <div class="pnlTableCol">
                                <asp:CheckBox ID="chkBaseRecipe" runat="server" />
                            </div>
                        </div>
                        <div class="pnlTableRow">
                            <div class="pnlTableCol">
                                <asp:Label ID="lblHotSpicy" runat="server" CssClass="IngredientInfoFieldTitle" Text="Ricetta Piccante"></asp:Label>
                            </div>
                            <div class="pnlTableCol">
                                <asp:CheckBox ID="chkHotSpicy" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="pnlTable">
                        <div class="pnlTableCaption">
                            Altre informazioni sulla ricetta
                        </div>
                        <div class="pnlTableRow">
                            <div class="pnlTableCol">
                                <asp:Label ID="lblRecipeHistory" runat="server" CssClass="IngredientInfoFieldTitle"
                                    Text="Storia della ricetta"></asp:Label>
                            </div>
                            <div class="pnlTableCol">
                                <asp:TextBox ID="txtRecipeHistory" runat="server" TextMode="MultiLine" Rows="4" Columns="60"></asp:TextBox>
                            </div>
                        </div>
                        <div class="pnlTableRow">
                            <div class="pnlTableCol">
                                <asp:Label ID="lblRecipeNote" runat="server" CssClass="IngredientInfoFieldTitle"
                                    Text="Note particolari"></asp:Label>
                            </div>
                            <div class="pnlTableCol">
                                <asp:TextBox ID="txtRecipeNote" runat="server" TextMode="MultiLine" Rows="4" Columns="60"></asp:TextBox>
                            </div>
                        </div>
                        <div class="pnlTableRow">
                            <div class="pnlTableCol">
                                <asp:Label ID="lblRecipeSuggestion" runat="server" CssClass="IngredientInfoFieldTitle"
                                    Text="Suggerimenti"></asp:Label>
                            </div>
                            <div class="pnlTableCol">
                                <asp:TextBox ID="txtRecipeSuggestion" runat="server" TextMode="MultiLine" Rows="4"
                                    Columns="60"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <%-- <p class="CommentoDiAiuto">
                        Una Ricetta potrebbe avere una ricetta di rifferimento. Ad esempio 
                        di lasagne al forno con rag&ugrave; possono essercene molte varianti, ma tutte facenti
                        parte delle lasagne al forno con rag&ugrave;</p>
                    <p class="CommentoDiAiuto">
                        Se è presente una ricetta VALIDA per essere il "padre" di questa in questo menù, 
                        selezionala.</p>
                <p>&nbsp;</p>
                <p>--%>
                    <%--<asp:Label ID="lblRecipeFather" runat="server" 
                        CssClass="IngredientInfoFieldTitle" 
                        Text="Ricetta padre (se presente):"></asp:Label>
                    <asp:DropDownList ID="ddlRecipeFather" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="pnlTable">
                <div class="pnlTableCaption">
                    Ingredienti della ricetta
                </div>
                <div class="pnlTableRow">
                    <div class="pnlTableCol">
                        <asp:UpdatePanel ID="upnlRecipeIngredient" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Repeater ID="repRecipeIngredient" runat="server">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="upnRecipeIngredient" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <MyCtrl:ShowRecipeIngr ID="sriRecipeIngr" runat="server" IDIngredient='<%# Eval("Ingredient.IDIngredient") %>'
                                                    IDLanguage="2" IDRecipe='<%# Eval("Recipe.IDRecipe") %>' IDRecipeIngredient='<%# Eval("IDRecipeIngredient") %>'
                                                    IsPrincipal='<%# Eval("IsPrincipalIngredient") %>' Quantity='<%# Eval("Quantity") %>'
                                                    QuantityNotSpecified='<%# Eval("QuantityNotSpecified") %>' QuantityNotStd='<%# Eval("QuantityNotStd") %>'
                                                    QuantityNotStdType='<%# Eval("QuantityNotStdType.IDQuantityNotStd") %>' QuantityType='<%# Eval("QuantityType.IDIngredientQuantityType") %>'
                                                    ShowInvalidIngr="true" ShowPrincipalIngr="false" RecipeIngredientGroupNumber='<%# Eval("RecipeIngredientGroupNumber") %>'
                                                    RecipeIngredientGroupNumberChange='<%# Eval("RecipeIngredientGroupNumberChange") %>' EditIngredientRelevance="true"
                                                    ShowEditButton="true" DeleteButtonToolTip="Cancella Ingrediente da questa Ricetta"
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
                                <br />
                                <MyCtrl:AddRecipeIngr ID="ariIngredient" runat="server" Visible="true" OnIngredientAdded="ariIngredient_IngredientAdded" />
                                <asp:Panel ID="pnlAdd" CssClass="pnlAddIngrRecipeButton" runat="server">
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="pnlTable">
                <div class="pnlTableCaption">
                    Informazioni aggiuntive sulla ricetta
                </div>
                <asp:UpdatePanel ID="upnlDynamicProperty" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="pnlTable800px">
                <div class="pnlTableCaption">
                    Controlla che il procedimento delle ricetta sia corretto ed abbia un senso...
                </div>
                <div class="pnlTableRow">
<%--                    <div class="pnlTableCol">
                        <asp:Label ID="lblRecipeStep" runat="server" CssClass="IngredientInfoFieldTitle"
                            Text="Procedimento"></asp:Label>
                    </div>--%>
                    <div class="pnlTableCol">
                        <asp:TextBox ID="txtRecipeStep" runat="server" TextMode="MultiLine" Rows="6" Columns="100"> </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="pnlTable400px">
                <div class="pnlTableCaption">
                    Bevande consigliate, scegli una o più bevande da abbinare a questa ricetta in base
                    al tuo gusto.
                </div>
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
                                                    ShowInfoPanel="true" />
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
                            <div class="pnlTableCol">
                                <MyCtrl:AutoComplete runat="server" ID="acBeverage" />
                            </div>
                            <div class="pnlTableCol">
                                <asp:Button ID="btnAddBeverageRecipe" runat="server" Text="Aggiungi" OnClick="btnAddBeverageRecipe_Click" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
    
    <div class="pnlTable400px">
        <div class="pnlTableCaption">
            Prima di salvare dai un voto a questa ricetta
        </div>
        <div class="pnlTableRow">
         <div class="pnlTableCol">
              <MyCtrl:Rating runat="server" ID="rtgRecipe1" />
         </div>
        </div>
    </div>
    <div style="display: block; float: left; padding: 30 0 50 0; width: 600px; height: 100px">
        <asp:UpdatePanel ID="upnBtnSave" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnSave" runat="server" Text="Salva Ricetta" OnClick="btnSave_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </asp:Panel>
    </div>
    <asp:Panel ID="pnlNoAuth" Visible="false" ClientIDMode="Static" runat="server">
        <br />
        <p>
            <asp:Label ID="lblNoAuth" runat="server" CssClass="lblIngredientTitle" Text="Non sei autorizzato a visualizzare questa pagina."></asp:Label></p>
        <br />
        <p>
            <asp:HyperLink ID="lnkBackToHome" CssClass="linkStandard" NavigateUrl="~/Default.aspx"
                runat="server">Torna a MyCookin</asp:HyperLink></p>
    </asp:Panel>
</asp:Content>
