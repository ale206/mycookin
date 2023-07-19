<%@ Page Title="Manage Ingredients" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master" AutoEventWireup="true" CodeBehind="MngIngredients.aspx.cs" Inherits="MyCookinWeb.IngredientWeb.MngIngredients" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register TagPrefix="MyCtrl" TagName="ImageEdit" Src="~/CustomControls/AddRemoveImage.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="AutoComplete" Src="~/CustomControls/AutoComplete.ascx" %>
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

    function HidePanel(ddlId) {
        var ControlName = document.getElementById(ddlId.id);

        if (ddlId.value == '')  //it depends on which value Selection do u want to hide or show your textbox 
        {
            document.getElementById('pnlPreparationRecipeInfo').style.display = 'none';
            document.getElementById('pnlIngrNutrictionalInfo').style.display = '';
        }
        else {
            document.getElementById('pnlPreparationRecipeInfo').style.display = '';
            document.getElementById('pnlIngrNutrictionalInfo').style.display = 'none';
        }
    }

    function ShowLoader() {
        document.getElementById('divPreparationRecipe').style.display = '';
    }

    function HideLoader() {
        document.getElementById('divPreparationRecipe').style.display = 'none';
    }

    $(document).ready(function () {
        $("#<%=ddlIngredientPreparationRecipe.ClientID%>").change(function () {
            ShowLoader();
            var ddlPreprecipe = $("#<%=ddlIngredientPreparationRecipe.ClientID%>");
            var msgbox = $("#status");
            if (ddlPreprecipe.val() != '') {
                $.ajax({
                    type: "GET",
                    url: "http://" + WebServicesPath + "/Recipe/GetRecipeInfo.asmx/GetRecipeFullInfoByID?IDRecipe=" + ddlPreprecipe.val(),
                    crossDomain: true,
                    //data: "{Message:" + "\"" + Message + "\"" + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //async: false,
                    success: function (result) {
                        //alert("ciao " + result.d);
                        $('#txtPrepRecipeInfo').val(result.d);
                        HideLoader();
                    },
                    error: function (result) {
                        //alert(result.status + ' ' + result.statusText);
                        $('#txtPrepRecipeInfo').val("Errore, non posso visualizzarti la ricetta di preparazione." );
                        HideLoader();
                    }
                });
            }
        });
    });

    function CheckAllYearIngr() {
        document.getElementById('<%=chkJanuary.ClientID%>').checked = true;
        document.getElementById('<%=chkFebruary.ClientID%>').checked = true;
        document.getElementById('<%=chkMarch.ClientID%>').checked = true;
        document.getElementById('<%=chkApril.ClientID%>').checked = true;
        document.getElementById('<%=chkMay.ClientID%>').checked = true;
        document.getElementById('<%=chkJune.ClientID%>').checked = true;
        document.getElementById('<%=chkJuly.ClientID%>').checked = true;
        document.getElementById('<%=chkAugust.ClientID%>').checked = true;
        document.getElementById('<%=chkSeptember.ClientID%>').checked = true;
        document.getElementById('<%=chkOctober.ClientID%>').checked = true;
        document.getElementById('<%=chkNovember.ClientID%>').checked = true;
        document.getElementById('<%=chkDecember.ClientID%>').checked = true;
    }
    function UnCheckAllYearIngr() {
        document.getElementById('<%=chkJanuary.ClientID%>').checked = false;
        document.getElementById('<%=chkFebruary.ClientID%>').checked = false;
        document.getElementById('<%=chkMarch.ClientID%>').checked = false;
        document.getElementById('<%=chkApril.ClientID%>').checked = false;
        document.getElementById('<%=chkMay.ClientID%>').checked = false;
        document.getElementById('<%=chkJune.ClientID%>').checked = false;
        document.getElementById('<%=chkJuly.ClientID%>').checked = false;
        document.getElementById('<%=chkAugust.ClientID%>').checked = false;
        document.getElementById('<%=chkSeptember.ClientID%>').checked = false;
        document.getElementById('<%=chkOctober.ClientID%>').checked = false;
        document.getElementById('<%=chkNovember.ClientID%>').checked = false;
        document.getElementById('<%=chkDecember.ClientID%>').checked = false;
    }
</script>
<asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server" 
        meta:resourcekey="pnlResultResource1">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server" 
            meta:resourcekey="lblResultResource1"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlMainTab" runat="server" ClientIDMode="Static" 
        meta:resourcekey="pnlMainTabResource1">
    
    


    <ul>
        <li><asp:HyperLink ID="lnkIngrMain" runat="server" Text="ManageIngredient" 
                NavigateUrl="#pnlIngredientAjax" meta:resourcekey="lnkIngrMainResource1"></asp:HyperLink></li>
    </ul>

                <asp:UpdatePanel ID="pnlIngredientAjax" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <asp:Panel ID="pnlIngredient" ClientIDMode="Static" runat="server" 
                        meta:resourcekey="pnlIngredientResource1">

                <p>
                <asp:HyperLink ID="lnkDashboard" NavigateUrl="IngredientDashBoard.aspx" 
                        runat="server" CssClass="linkStandard" 
                        meta:resourcekey="lnkDashboardResource1"> < < Clicca QUI per Tornare Indietro - 
                        Ricordati di cliccare sul pulsante SALVA in fondo alla pagina</asp:HyperLink></p>

                <p>&nbsp;</p>
                <p>
                    <asp:HyperLink ID="hlFoodInfo" NavigateUrl="http://www.valori-alimenti.com" 
                        Target="_blank" runat="server" CssClass="linkStandard" 
                        meta:resourcekey="hlFoodInfoResource1">Clicca qui per visualizzare un sito con i Valori Alimentari di migliaia di ingredienti ;)</asp:HyperLink>
                </p>
                <p>&nbsp;</p>
                <p>
                    <asp:Label ID="lblIngredientTitle" runat="server" 
                        CssClass="IngredientInfoFieldTitle" Text="Stai modificando:" 
                        meta:resourcekey="lblIngredientTitleResource1"></asp:Label>
                    <asp:Label ID="lblIngredientTitleValue" runat="server" 
                        CssClass="lblIngredientTitle" 
                        meta:resourcekey="lblIngredientTitleValueResource1"></asp:Label>
                </p>

                 <p>&nbsp;</p>

                <p class="CommentoDiAiuto">Inizia scegliendo una bella immagine per questo ingrediente. Cercala su Google oppure 
                    scegliene una delle tue. </p>

                <p class="CommentoDiAiuto">
                    Potrai ritagliarla e prenderne la parte migliore.</p>

                <MyCtrl:ImageEdit ID="upshImgIngredient" runat="server" />
                  <p>&nbsp;</p>
                  <p class="CommentoDiAiuto">
                    Se ritieni che questo non sia un ingrediente (Se ad esempio trovi "bicchiere" o "lasagne") deseleziona questa casella.</p>
                <p>&nbsp;</p>
                <p><asp:Label ID="lblIngrEnabled" runat="server" 
                        CssClass="IngredientInfoFieldTitle" Text="Ingrediente valido?" 
                        meta:resourcekey="lblIngrEnabledResource1"></asp:Label>
                    <asp:CheckBox ID="chkIngrEnabled" runat="server" 
                        meta:resourcekey="chkIngrEnabledResource1" /></p><br />
                <p>
                
                <p class="CommentoDiAiuto">
                        Un Ingrediente potrebbe essere preparato attraverso una ricetta. Ad esempio 
                        l&#39;ingrediente &quot;Maionese&quot; ha una sua propria ricetta di preparazione che 
                        comparirà in questo elenco. Invece l'ingrediente "LEGUMI" no.</p>
                    <p class="CommentoDiAiuto">
                        Se è presente una ricetta VALIDA per questo ingrediente in questo menù, 
                        selezionala.</p>
                <p>&nbsp;</p>

                    <asp:Label ID="lblIngredientPreparationRecipe" runat="server" 
                        CssClass="IngredientInfoFieldTitle" 
                        Text="Ricetta di Preparazione (se presente):" 
                        meta:resourcekey="lblIngredientPreparationRecipeResource1"></asp:Label>
                    <asp:DropDownList ID="ddlIngredientPreparationRecipe" ClientIDMode="Static" 
                        onchange="HidePanel(this);" runat="server" 
                        meta:resourcekey="ddlIngredientPreparationRecipeResource1">
                    </asp:DropDownList>
                    
                    <p>&nbsp;</p>
                <p class="CommentoDiAiuto">
                        Compila i campi sotto aiutandoti col sito di riferimento per i valori alimentari. Trovi il link in alto. </p>
                <p>&nbsp;</p>

                    <asp:Panel ID="pnlIngrNutrictionalInfo" runat="server" ClientIDMode="Static" 
                        meta:resourcekey="pnlIngrNutrictionalInfoResource1">
                        <p>
                            <asp:Label ID="lblAverageWeightOfOnePiece" runat="server" 
                                CssClass="IngredientInfoFieldTitle" Text="Peso Medio di 1 pezzo" 
                                meta:resourcekey="lblAverageWeightOfOnePieceResource1"></asp:Label>
                            <asp:TextBox ID="txtAverageWeightOfOnePiece" runat="server" MaxLength="5" onpaste="return false"
                                onkeypress="return isNumberKey(event)" 
                                meta:resourcekey="txtAverageWeightOfOnePieceResource1"></asp:TextBox>
                        </p>
                        <p>
                            <asp:Label ID="lblKcal100gr" runat="server" CssClass="IngredientInfoFieldTitle" 
                                Text="Kcal per 100gr" meta:resourcekey="lblKcal100grResource1"></asp:Label>
                            <asp:TextBox ID="txtKcal100gr" runat="server" MaxLength="5" onpaste="return false"
                                onkeypress="return isNumberKey(event)" 
                                meta:resourcekey="txtKcal100grResource1"></asp:TextBox>
                        </p>
                        <p>
                            <asp:Label ID="lblPercKcalProteins" runat="server" 
                                CssClass="IngredientInfoFieldTitle" Text="Quantità di Proteine (g)" 
                                meta:resourcekey="lblPercKcalProteinsResource1"></asp:Label>
                            <asp:TextBox ID="txtPercKcalProteins" runat="server" MaxLength="5" onpaste="return false"
                                onkeypress="return isNumberKey(event)" 
                                meta:resourcekey="txtPercKcalProteinsResource1"></asp:TextBox>
                        </p>
                        <p>
                            <asp:Label ID="lblPercKcalFats" runat="server" 
                                CssClass="IngredientInfoFieldTitle" Text="Quantità di Grassi (g)" 
                                meta:resourcekey="lblPercKcalFatsResource1"></asp:Label>
                            <asp:TextBox ID="txtPercKcalFats" runat="server" MaxLength="5" onpaste="return false"
                                onkeypress="return isNumberKey(event)" 
                                meta:resourcekey="txtPercKcalFatsResource1"></asp:TextBox>
                        </p>
                        <p>
                            <asp:Label ID="lblPerKcalCarbohydrates" runat="server" 
                                CssClass="IngredientInfoFieldTitle" Text="Quantità di Carboidrati (g)" 
                                meta:resourcekey="lblPerKcalCarbohydratesResource1"></asp:Label>
                            <asp:TextBox ID="txtPerKcalCarbohydrates" runat="server" MaxLength="5" onpaste="return false"
                                onkeypress="return isNumberKey(event)" 
                                meta:resourcekey="txtPerKcalCarbohydratesResource1"></asp:TextBox>
                        </p>
                        <p>
                            <asp:Label ID="lblPercKcalAlcohol" runat="server" 
                                CssClass="IngredientInfoFieldTitle" Text="Quantità di Alcool (g)" 
                                meta:resourcekey="lblPercKcalAlcoholResource1"></asp:Label>
                            <asp:TextBox ID="txtPercKcalAlcohol" runat="server" MaxLength="5" onpaste="return false"
                                onkeypress="return isNumberKey(event)" 
                                meta:resourcekey="txtPercKcalAlcoholResource1"></asp:TextBox>
                        </p>
                        <p>
                            <asp:Label ID="lblIsVegetarian" runat="server" 
                                CssClass="IngredientInfoFieldTitle" Text="Alimento Vegetariano" 
                                meta:resourcekey="lblIsVegetarianResource1"></asp:Label>
                            <asp:CheckBox ID="chkIsVegetarian" runat="server" 
                                meta:resourcekey="chkIsVegetarianResource1" />
                        </p>
                        <p>
                            <asp:Label ID="lblIsVegan" runat="server" CssClass="IngredientInfoFieldTitle" 
                                Text="Alimento Vegano" meta:resourcekey="lblIsVeganResource1"></asp:Label>
                            <asp:CheckBox ID="chkIsVegan" runat="server" 
                                meta:resourcekey="chkIsVeganResource1" />
                        </p>
                        <p>
                            <asp:Label ID="lblIsGlutenFree" runat="server" 
                                CssClass="IngredientInfoFieldTitle" Text="Senza Glutine" 
                                meta:resourcekey="lblIsGlutenFreeResource1"></asp:Label>
                            <asp:CheckBox ID="chkIsGlutenFree" runat="server" 
                                meta:resourcekey="chkIsGlutenFreeResource1" />
                        </p>
                        <p>
                            <asp:Label ID="lblIsHotSpicy" runat="server" 
                                CssClass="IngredientInfoFieldTitle" Text="Piccante" 
                                meta:resourcekey="lblIsHotSpicyResource1"></asp:Label>
                            <asp:CheckBox ID="chkIsHotSpicy" runat="server" 
                                meta:resourcekey="chkIsHotSpicyResource1" />
                        </p>
                        <br />
                        <p class="CommentoDiAiuto">
                        Mesi in cui l'ingrediente è "di stagione"
                        </p>
                        <input id="btnAllYearIngredient" type="button" value="Tutto l'anno" onclick="CheckAllYearIngr()" />
                        <input id="btnDeselectIngredient" type="button" value="Mai" onclick="UnCheckAllYearIngr()" />
                        <p>
                            <asp:Label ID="lblJanuary" runat="server" CssClass="IngredientInfoFieldTitle" 
                                Text="Gennaio" meta:resourcekey="lblJanuaryResource1"></asp:Label>
                            <asp:CheckBox ID="chkJanuary" runat="server" 
                                meta:resourcekey="chkJanuaryResource1" />
                        </p>
                        <p>
                            <asp:Label ID="lblFebruary" runat="server" CssClass="IngredientInfoFieldTitle" 
                                Text="Febbraio" meta:resourcekey="lblFebruaryResource1"></asp:Label>
                            <asp:CheckBox ID="chkFebruary" runat="server" 
                                meta:resourcekey="chkFebruaryResource1" />
                        </p>
                        <p>
                            <asp:Label ID="lblMarch" runat="server" CssClass="IngredientInfoFieldTitle" 
                                Text="Marzo" meta:resourcekey="lblMarchResource1"></asp:Label>
                            <asp:CheckBox ID="chkMarch" runat="server" 
                                meta:resourcekey="chkMarchResource1" />
                        </p>
                        <p>
                            <asp:Label ID="lblApril" runat="server" CssClass="IngredientInfoFieldTitle" 
                                Text="Aprile" meta:resourcekey="lblAprilResource1"></asp:Label>
                            <asp:CheckBox ID="chkApril" runat="server" 
                                meta:resourcekey="chkAprilResource1" />
                        </p>
                        <p>
                            <asp:Label ID="lblMay" runat="server" CssClass="IngredientInfoFieldTitle" 
                                Text="Maggio" meta:resourcekey="lblMayResource1"></asp:Label>
                            <asp:CheckBox ID="chkMay" runat="server" meta:resourcekey="chkMayResource1" />
                        </p>
                        <p>
                            <asp:Label ID="lblJune" runat="server" CssClass="IngredientInfoFieldTitle" 
                                Text="Giugno" meta:resourcekey="lblJuneResource1"></asp:Label>
                            <asp:CheckBox ID="chkJune" runat="server" meta:resourcekey="chkJuneResource1" />
                        </p>
                        <p>
                            <asp:Label ID="lblJuly" runat="server" CssClass="IngredientInfoFieldTitle" 
                                Text="Luglio" meta:resourcekey="lblJulyResource1"></asp:Label>
                            <asp:CheckBox ID="chkJuly" runat="server" meta:resourcekey="chkJulyResource1" />
                        </p>
                        <p>
                            <asp:Label ID="lblAugust" runat="server" CssClass="IngredientInfoFieldTitle" 
                                Text="Agosto" meta:resourcekey="lblAugustResource1"></asp:Label>
                            <asp:CheckBox ID="chkAugust" runat="server" 
                                meta:resourcekey="chkAugustResource1" />
                        </p>
                        <p>
                            <asp:Label ID="lblSeptember" runat="server" CssClass="IngredientInfoFieldTitle" 
                                Text="Settembre" meta:resourcekey="lblSeptemberResource1"></asp:Label>
                            <asp:CheckBox ID="chkSeptember" runat="server" 
                                meta:resourcekey="chkSeptemberResource1" />
                        </p>
                        <p>
                            <asp:Label ID="lblOctober" runat="server" CssClass="IngredientInfoFieldTitle" 
                                Text="Ottobre" meta:resourcekey="lblOctoberResource1"></asp:Label>
                            <asp:CheckBox ID="chkOctober" runat="server" 
                                meta:resourcekey="chkOctoberResource1" />
                        </p>
                        <p>
                            <asp:Label ID="lblNovember" runat="server" CssClass="IngredientInfoFieldTitle" 
                                Text="Novembre" meta:resourcekey="lblNovemberResource1"></asp:Label>
                            <asp:CheckBox ID="chkNovember" runat="server" 
                                meta:resourcekey="chkNovemberResource1" />
                        </p>
                        <p>
                            <asp:Label ID="lblDecember" runat="server" CssClass="IngredientInfoFieldTitle" 
                                Text="Dicembre" meta:resourcekey="lblDecemberResource1"></asp:Label>
                            <asp:CheckBox ID="chkDecember" runat="server" 
                                meta:resourcekey="chkDecemberResource1" />
                        </p>
                    </asp:Panel>
                    <asp:Panel ID="pnlPreparationRecipeInfo" runat="server" ClientIDMode="Static" 
                        style=" display:none;" 
                        meta:resourcekey="pnlPreparationRecipeInfoResource1">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTitleInfoRecipe" runat="server" 
                                        CssClass="lblIngredientTitle2" Text="Ricetta di Preparazione" 
                                        meta:resourcekey="lblTitleInfoRecipeResource1"></asp:Label>
                                </td>
                                <td>
                                    <div ID="divPreparationRecipe" style=" display:none;">
                                        <img src="/Images/Loader/ajax-loader_blu01.gif" width="32" height="32" alt="Loading data..." />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:TextBox ID="txtPrepRecipeInfo" runat="server" ClientIDMode="Static" 
                            Height="100px" ReadOnly="True" TextMode="MultiLine" Width="600px" 
                            meta:resourcekey="txtPrepRecipeInfoResource1"></asp:TextBox>
                        <br />
                    </asp:Panel>
                    <br />
                        <p class="CommentoDiAiuto">
                        Alcune volte un ingrediente può essere sostituito con uno simile.
                        </p>
                    <asp:UpdatePanel ID="upnAlternativeIngredient" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlIngrAlternative" runat="server" 
                                meta:resourcekey="pnlIngrAlternativeResource1">
                            </asp:Panel>
                            <MyCtrl:AutoComplete ID="acIngredient" runat="server" />
                            <br /><br />
                            <asp:Button ID="btnAddAltIngr" runat="server" Text="Aggiungi Alternativa" 
                                OnClick="btnAddAltIngr_Click" meta:resourcekey="btnAddAltIngrResource1" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <p>&nbsp;</p>
                    <p class="CommentoDiAiuto">
                        Controlla ed eventualmente correggi il nome dell'ingrediente al singolare e al plurale.</p>
                    <p>&nbsp;</p>


                    <p>
                        <asp:Label ID="lblIngredientSingular" runat="server" 
                            CssClass="IngredientInfoFieldTitle" Text="Nome Ingrediente al Singolare" 
                            meta:resourcekey="lblIngredientSingularResource1"></asp:Label>
                        <asp:TextBox ID="txtIngredientSingular" Width="400px" runat="server" 
                            MaxLength="130" meta:resourcekey="txtIngredientSingularResource1"></asp:TextBox>
                    </p>
                    <p>
                        <asp:Label ID="lblIngredientPlural" runat="server" 
                            CssClass="IngredientInfoFieldTitle" Text="Nome Ingrediente al Plurale" 
                            meta:resourcekey="lblIngredientPluralResource1"></asp:Label>
                        <asp:TextBox ID="txtIngredientPlural" Width="400px" runat="server" 
                            MaxLength="130" meta:resourcekey="txtIngredientPluralResource1"></asp:TextBox>
                    </p>

                    <p>&nbsp;</p>
                    <p class="CommentoDiAiuto">
                        Se ti va scrivi una descrizione di questo ingrediente. Potresti prendere spunto da Wikipedia.</p>
                    <p>&nbsp;</p>


                    <p>
                        <asp:Label ID="lblIngredientDescription" runat="server" 
                            CssClass="IngredientInfoFieldTitle" Text="Descrizione" 
                            meta:resourcekey="lblIngredientDescriptionResource1"></asp:Label>
                        <asp:TextBox ID="txtIngredientDescription" runat="server" Width="400px" 
                            Rows="5" MaxLength="200" 
                            TextMode="MultiLine" meta:resourcekey="txtIngredientDescriptionResource1"></asp:TextBox>
                    </p>

                    <p>&nbsp;</p>
                    <p class="CommentoDiAiuto">
                        Seleziona la categoria che rispecchia meglio questo ingrediente.</p>
                    <p>&nbsp;</p>

                    <p>
                        <asp:Label ID="lblIngredientCategory" runat="server" 
                            CssClass="IngredientInfoFieldTitle" Text="Categoria dell'Ingrediente" 
                            meta:resourcekey="lblIngredientCategoryResource1"></asp:Label>
                        <asp:DropDownList ID="ddlIngredientCategory" runat="server" 
                            meta:resourcekey="ddlIngredientCategoryResource1">
                        </asp:DropDownList>
                        <asp:Label ID="lblIDIngredient" runat="server" Visible="False" 
                            meta:resourcekey="lblIDIngredientResource1"></asp:Label>
                        <asp:Label ID="lblIDLanguage" runat="server" Visible="False" 
                            meta:resourcekey="lblIDLanguageResource1"></asp:Label>
                    </p>
                    
        </asp:Panel>
        <br />
        
        <asp:UpdatePanel ID="pnlIngredientQta" ClientIDMode="Static" runat="server">
        <ContentTemplate>

        



            <p>
                <asp:Label ID="lblIngredientTitle2" runat="server" 
                    CssClass="IngredientInfoFieldTitle" 
                    Text="Unità di misura utilizzabili per questo ingrediente" 
                    meta:resourcekey="lblIngredientTitle2Resource1"></asp:Label>
                <asp:Label ID="lblIngredientTitleValue2" runat="server" 
                    CssClass="lblIngredientTitle" Visible="False" 
                    meta:resourcekey="lblIngredientTitleValue2Resource1"></asp:Label><br />

                    <p>&nbsp;</p>

                    <p class="CommentoDiAiuto">
                    Come si misura questo ingrediente? <br />
                    <asp:Label ID="lblQuantityTypeDescription" runat="server" 
                Text=" Se ad esempio l'ingrediente è un liquido sarà misurabile in Cl (centilitri), in bicchieri o tazze; 
                una salsa, come la maionese, sarà misurabile in Cl, in g (grammi) o in cucchiai; 
                un pollo sarà misurabile in grammi o in pezzi e così via. " 
                            meta:resourcekey="lblQuantityTypeDescriptionResource1"></asp:Label>
                <br />
                Scegli tutte le unità di misura che possono servire a misurare questo ingrediente.

                    </p>
                    <p>&nbsp;</p>


            </p>

            <p>
            <asp:DropDownList ID="ddlIngredientQuantityType" runat="server" 
                    meta:resourcekey="ddlIngredientQuantityTypeResource1">
            </asp:DropDownList>
            <asp:Button ID="btnAddIngredientAllowedQuantityType" runat="server" 
                Text="Aggiungi tipo di quantità di misura" 
                onclick="btnAddIngredientAllowedQuantityType_Click" 
                    meta:resourcekey="btnAddIngredientAllowedQuantityTypeResource1" />

</p>

            <asp:GridView ID="gvIngredientAllowedQuantityType" AutoGenerateColumns="False" 
                runat="server" CssClass="mGrid" 
                meta:resourcekey="gvIngredientAllowedQuantityTypeResource1">
            <Columns>
                <asp:TemplateField HeaderText="IDIngredientQuantityType" Visible="False" 
                    meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                    <asp:Label ID="lblIDIngredientQuantityType" runat="server" 
                            Text='<%# Eval("IDIngredientQuantityType") %>' 
                            meta:resourcekey="lblIDIngredientQuantityTypeResource1"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IngredientQuantityType" HeaderText="Tipo Quantità" 
                   SortExpression="IngredientQuantityType" 
                    meta:resourcekey="BoundFieldResource1" />
                <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                    <asp:Button ID="btnDeleteQta" ClientIDMode="Static" 
                            CommandArgument='<%# Eval("IDIngredientQuantityType") %>' 
                            ToolTip='<%# "Cancella "+ Eval("IngredientQuantityType") %>' runat="server" 
                            Text="Elimina" OnClick="btnDelete_Click" 
                            
                            OnClientClick="return JCOnfirm(this,'Cancellare Elemento','Sei sicuro di voler cancellare questo elemento?','Si','No');" 
                            meta:resourcekey="btnDeleteQtaResource1"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
            <asp:Label ID="lblNoDataQta" runat="server" 
                    Text="Tipi di quantità non ancora definiti" 
                    meta:resourcekey="lblNoDataQtaResource1"></asp:Label>
            </EmptyDataTemplate>
            </asp:GridView>
            <br />
        </ContentTemplate>
        </asp:UpdatePanel>
       <br />
       <br />
                <asp:ImageButton ID="btnEditIngredient" ClientIDMode="Static" 
                        CssClass="btnEditIngredient" runat="server" ImageUrl="~/Images/iconSave.png" 
                        onclick="btnEditIngredient_Click" 
                        meta:resourcekey="btnEditIngredientResource1" />
        </ContentTemplate>
        </asp:UpdatePanel>

     </asp:Panel>
    <asp:Panel ID="pnlNoAuth" Visible="False" ClientIDMode="Static" runat="server" 
        meta:resourcekey="pnlNoAuthResource1">
        <br />
        <p><asp:Label ID="lblNoAuth" runat="server" CssClass="lblIngredientTitle" 
                Text="Non sei autorizzato a visualizzare questa pagina." 
                meta:resourcekey="lblNoAuthResource1"></asp:Label></p><br />
        <p><asp:HyperLink ID="lnkBackToHome" CssClass="linkStandard" 
                NavigateUrl="~/Default.aspx" runat="server" 
                meta:resourcekey="lnkBackToHomeResource1">Torna a MyCookin</asp:HyperLink></p>
    </asp:Panel>

</asp:Content>
