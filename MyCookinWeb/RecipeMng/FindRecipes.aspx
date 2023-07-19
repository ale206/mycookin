<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master" AutoEventWireup="true" CodeBehind="FindRecipes.aspx.cs" Inherits="MyCookinWeb.RecipeWeb.FindRecipes" culture="auto" meta:resourcekey="PageResource2" uiculture="auto" %>
<%@ Register TagName="ShowRecipe" TagPrefix="MyCtrl" Src="~/CustomControls/ctrlRecipePolaroid.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
<link rel="Stylesheet" href="/Styles/PageStyle/FindRecipes.min.css" />
</asp:Content>
<asp:Content ID="cntMain" ContentPlaceHolderID="cphMain" runat="server">
<script type="text/javascript">
    //For Checkboxes and Filter Icons
    var boxFrigoInfo;

    function checkboxChange(chekboxId, imgId) {
        if ($("#" + chekboxId).attr('checked')) {
            $("#" + chekboxId).attr('checked', false);

            $("#" + imgId).attr('src', $("#" + imgId).attr('src').replace("-on", "-off"));

            if (chekboxId == 'chkFrigo') {
            }
        }
        else {
            $("#" + chekboxId).attr('checked', true);
            $("#" + imgId).attr('src', $("#" + imgId).attr('src').replace("-off", "-on"));

            if (chekboxId == 'chkFrigo') {
            }
        }
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
    function goNext(numItem) {
        try {
            var current = parseInt($('#<%=hfRowOffSet.ClientID %>').val());
            current = current + numItem;
            $('#<%=hfRowOffSet.ClientID %>').val(current)
        }
        catch (er) {
        }
    }
    function goPrev(numItem) {
        try {
            var current = parseInt($('#<%=hfRowOffSet.ClientID %>').val());
            current = current - numItem;
            $('#<%=hfRowOffSet.ClientID %>').val(current)
        }
        catch (er) {
        }
    }
    function ResetRowOffSet() {
        $('#<%=hfRowOffSet.ClientID %>').val('0')
    }
    function pageLoad() {
        $('#<%=pnlBackground.ClientID%>').height($('#<%=pnlContent.ClientID%>').height());
    }
    $(document).ready(function () {
        //Start Tooltip
        $('#lblExistenceUsername').tooltip();
    }); 
    </script>
    <asp:Panel ID="pnlMain" runat="server" CssClass="pnlMain" 
        meta:resourcekey="pnlMainResource1">
        <asp:UpdatePanel ID="upnMain" runat="server">
            <ContentTemplate>
                <div style="display: none;">
                    <asp:CheckBox ID="chkVegan" runat="server" ClientIDMode="Static" 
                        meta:resourcekey="chkVeganResource1" />
                    <asp:CheckBox ID="chkVegetarian" runat="server" ClientIDMode="Static" 
                        meta:resourcekey="chkVegetarianResource1" />
                    <asp:CheckBox ID="chkGlutenFree" runat="server" ClientIDMode="Static" 
                        meta:resourcekey="chkGlutenFreeResource1" />
                    <asp:CheckBox ID="chkLight" runat="server" ClientIDMode="Static" 
                        meta:resourcekey="chkLightResource1" />
                    <asp:CheckBox ID="chkQuick" runat="server" ClientIDMode="Static" 
                        meta:resourcekey="chkQuickResource1" />
                    <asp:CheckBox ID="chkFrigo" runat="server" ClientIDMode="Static" 
                        meta:resourcekey="chkFrigoResource1" />
                    <%-- For image preload--%>
                    <img src="/Images/vegan-on.png" alt="PreLoad"/><img src="/Images/vegetarian-on.png"  alt="PreLoad"/>
                    <img src="/Images/gluten-free-on.png"  alt="PreLoad"/><img src="/Images/ico-bilancia-on.png"  alt="PreLoad"/>
                    <img src="/Images/icon-quickRecipe-on.png"  alt="PreLoad"/><img src="/Images/mymix-on.png"  alt="PreLoad"/>
                    <asp:HiddenField ID="hfFrigoInfo" runat="server" Value="SVUOTA FRIGO:  Inserisci almeno tre ingredienti separati da virgola per trovare le ricette che puoi preparare. Prova: \'pasta, tonno, pomodoro\' " />
                    <asp:HiddenField ID="hfRowOffSet" runat="server" />
                    <asp:HiddenField ID="hfLoadRecipeError" runat="server" />
                </div>
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
                    <div id="divSearchOptionIcon" class="divSearchOptionIcon">
                        <asp:ImageButton ID="btnVegan" runat="server" CssClass="OptionIcon MyTooltip" ToolTip="solo ricette vegane"
                            Width="50px" Height="50px" ImageUrl="/Images/vegan-off.png" onmouseover="changeImage('chkVegan', 'btnVegan', 'over')"
                            onmouseout="changeImage('chkVegan', 'btnVegan', 'out')" OnClientClick="checkboxChange('chkVegan', 'btnVegan'); return false;"
                            ClientIDMode="Static" meta:resourcekey="btnVeganResource1" />
                        <asp:ImageButton ID="btnVegetarian" runat="server" CssClass="OptionIcon MyTooltip"
                            ToolTip="solo ricette vegetariane" Width="50px" Height="50px" ImageUrl="/Images/vegetarian-off.png"
                            onmouseover="changeImage('chkVegetarian', 'btnVegetarian', 'over')" onmouseout="changeImage('chkVegetarian', 'btnVegetarian', 'out')"
                            OnClientClick="checkboxChange('chkVegetarian', 'btnVegetarian'); return false;"
                            ClientIDMode="Static" meta:resourcekey="btnVegetarianResource1" />
                        <asp:ImageButton ID="btnGlutenFree" runat="server" CssClass="OptionIcon MyTooltip"
                            ToolTip="solo ricette senza Glutine" Width="50px" Height="50px" ImageUrl="/Images/gluten-free-off.png"
                            onmouseover="changeImage('chkGlutenFree', 'btnGlutenFree', 'over')" onmouseout="changeImage('chkGlutenFree', 'btnGlutenFree', 'out')"
                            OnClientClick="checkboxChange('chkGlutenFree', 'btnGlutenFree'); return false;"
                            ClientIDMode="Static" meta:resourcekey="btnGlutenFreeResource1" />
                        <asp:ImageButton ID="btnLight" runat="server" CssClass="OptionIcon MyTooltip" ToolTip="solo ricette leggere"
                            Width="50px" Height="50px" ImageUrl="/Images/ico-bilancia-off.png" onmouseover="changeImage('chkLight', 'btnLight', 'over')"
                            onmouseout="changeImage('chkLight', 'btnLight', 'out')" OnClientClick="checkboxChange('chkLight', 'btnLight'); return false;"
                            ClientIDMode="Static" meta:resourcekey="btnLightResource1" />
                        <asp:ImageButton ID="btnQuick" runat="server" CssClass="OptionIcon MyTooltip" ToolTip="solo ricette veloci"
                            Width="50px" Height="50px" ImageUrl="/Images/icon-quickRecipe-off.png" onmouseover="changeImage('chkQuick', 'btnQuick', 'over')"
                            onmouseout="changeImage('chkQuick', 'btnQuick', 'out')" OnClientClick="checkboxChange('chkQuick', 'btnQuick'); return false;"
                            ClientIDMode="Static" meta:resourcekey="btnQuickResource1" />
                        <asp:ImageButton ID="btnEmptyFridge" runat="server" CssClass="OptionIcon MyTooltip"
                            ToolTip="svuota frigo" Width="50px" Height="50px" ImageUrl="/Images/mymix-off.png"
                            onmouseover="changeImage('chkFrigo', 'btnEmptyFridge', 'over')" onmouseout="changeImage('chkFrigo', 'btnEmptyFridge', 'out')"
                            
                            OnClientClick="checkboxChange('chkFrigo', 'btnEmptyFridge'); return false;" 
                            ClientIDMode="Static" meta:resourcekey="btnEmptyFridgeResource1" />
                        <div style="float: right; margin-top: -50px;">
                            <asp:Label ID="lblFrigoMixNote" ClientIDMode="Static" runat="server" 
                                ToolTip="SVUOTA FRIGO:<br/>Inserisci almeno tre ingredienti separati<br/>da virgola per trovare le ricette che<br/>puoi preparare.<br/>Prova: 'pasta, tonno, pomodoro' " 
                                CssClass="tipsyInfoNote" meta:resourcekey="lblFrigoMixNoteResource1"></asp:Label></div>
                    </div>
                    <div id="divSearch" class="divSearch">
                        <div id="boxSearchInput">
                            <div id="boxSearchField">
                                <asp:TextBox ID="txtSearchString" CssClass="padding8" runat="server" 
                                    placeholder="Ho voglia di qualcosa di dolce..." 
                                    onkeypress="return isSpecialHTMLChar(event)"
                                    meta:resourcekey="txtSearchStringResource1"></asp:TextBox></div>
                            <div id="boxSearchButton">
                                <asp:LinkButton ID="lnkSearch" runat="server" onclick="lnkSearch_Click" 
                                    OnClientClick="ResetRowOffSet()" meta:resourcekey="lnkSearchResource1">trova</asp:LinkButton></div>
                        </div>
                        <asp:Panel ID="pnlAdvancedSearch" runat="server" CssClass="boxSearchAdv" 
                            Visible="False" meta:resourcekey="pnlAdvancedSearchResource1">
                            <asp:LinkButton ID="lnkAdvancedSearch" runat="server" 
                                meta:resourcekey="lnkAdvancedSearchResource1">ricerca avanzata</asp:LinkButton>
                        </asp:Panel>
                    </div>
                    <asp:Panel ID="pnlSearchResults" runat="server" CssClass="pnlSearchResults" 
                        meta:resourcekey="pnlSearchResultsResource1">
                        <asp:Panel ID="pnlNoSearchResult" runat="server" Visible="False" 
                            CssClass="pnlNoSearchResult" meta:resourcekey="pnlNoSearchResultResource1">
                            <asp:Label ID="lblNoSearchResult" runat="server" 
                                Text="Ci dispiace ma non abbiamo trovato nessuna ricetta con i criteri selezionati." 
                                CssClass="lblNoSearchResult" Width="700px" 
                                meta:resourcekey="lblNoSearchResultResource1"></asp:Label>
                            <asp:Image ID="imgAddRecipe" 
                                ImageUrl="~/Images/icon-AddRecipe-color2.png" runat="server" Width="40px" 
                                Height="40px" meta:resourcekey="imgAddRecipeResource1" />
                            <asp:HyperLink ID="lnkAddNewRecipe" runat="server" CssClass="lnkAddNewRecipe" 
                                NavigateUrl="/RecipeMng/CreateRecipe.aspx" 
                                meta:resourcekey="lnkAddNewRecipeResource1">
                                Aggiungila TU!</asp:HyperLink>
                        </asp:Panel>
                        <asp:Panel ID="pnlShowRecipePolaroid" runat="server" CssClass="pnlRecipeFound" 
                            meta:resourcekey="pnlShowRecipePolaroidResource1">
                            <asp:Panel ID="pnlRecipe1" runat="server" CssClass="pnlRecipeLeft" 
                                Visible="False" meta:resourcekey="pnlRecipe1Resource1">
                                <MyCtrl:ShowRecipe ID="ShowRecipe1" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlRecipe2" runat="server" CssClass="pnlRecipeRight" 
                                Visible="False" meta:resourcekey="pnlRecipe2Resource1">
                                <MyCtrl:ShowRecipe ID="ShowRecipe2" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlRecipe3" runat="server" CssClass="pnlRecipeLeft" 
                                Visible="False" meta:resourcekey="pnlRecipe3Resource1">
                                <MyCtrl:ShowRecipe ID="ShowRecipe3" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlRecipe4" runat="server" CssClass="pnlRecipeRight" 
                                Visible="False" meta:resourcekey="pnlRecipe4Resource1">
                                <MyCtrl:ShowRecipe ID="ShowRecipe4" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlRecipe5" runat="server" CssClass="pnlRecipeLeft" 
                                Visible="False" meta:resourcekey="pnlRecipe5Resource1">
                                <MyCtrl:ShowRecipe ID="ShowRecipe5" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlRecipe6" runat="server" CssClass="pnlRecipeRight" 
                                Visible="False" meta:resourcekey="pnlRecipe6Resource1">
                                <MyCtrl:ShowRecipe ID="ShowRecipe6" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlRecipe7" runat="server" CssClass="pnlRecipeLeft" 
                                Visible="False" meta:resourcekey="pnlRecipe7Resource1">
                                <MyCtrl:ShowRecipe ID="ShowRecipe7" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlRecipe8" runat="server" CssClass="pnlRecipeRight" 
                                Visible="False" meta:resourcekey="pnlRecipe8Resource1">
                                <MyCtrl:ShowRecipe ID="ShowRecipe8" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlSearchNavigationButtons" runat="server" 
                                CssClass="pnlSearchNavigationButtons" 
                                meta:resourcekey="pnlSearchNavigationButtonsResource1">
                                <asp:Panel ID="pnlPrevPage" runat="server" CssClass="pnlPrevPage" 
                                    meta:resourcekey="pnlPrevPageResource1">
                                    <asp:ImageButton ID="btnPrevPage" runat="server" ImageUrl="/Images/icon-back.png"
                                        ToolTip="Torna indietro" CssClass="MyTooltip" Width="50px" Height="50px" OnClick="btnPrevPage_Click"
                                        OnClientClick="goPrev(8);" meta:resourcekey="btnPrevPageResource1" />
                                </asp:Panel>
                                <asp:Panel ID="pnlNextPage" runat="server" CssClass="pnlNextPage" 
                                    meta:resourcekey="pnlNextPageResource1">
                                    <asp:ImageButton ID="btnNextPage" runat="server" ImageUrl="/Images/icon-forward-gray.png"
                                        ToolTip="Scopri altre ricette" CssClass="MyTooltip" Width="50px" 
                                        Height="50px" OnClick="btnNextPage_Click"
                                        OnClientClick="goNext(8);" meta:resourcekey="btnNextPageResource1" />
                                </asp:Panel>
                                <asp:UpdateProgress ID="upnProgressWait" runat="server">
                                <ProgressTemplate>
                                    <asp:Image ID="imgLoading" runat="server" 
                                        ImageUrl="/Images/loadingLineOrange.gif" Height="20px" Width="220px" 
                                        meta:resourcekey="imgLoadingResource1" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            </asp:Panel>
                        </asp:Panel>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
