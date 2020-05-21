<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master" AutoEventWireup="true" CodeBehind="RecipesBooks.aspx.cs" Inherits="MyCookinWeb.RecipeMng.RecipesBooks" culture="auto" meta:resourcekey="PageResource2" uiculture="auto" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/RecipesBooks.min.css" />
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
    <asp:HiddenField ID="hfOgpTitle" runat="server" />
    <asp:HiddenField ID="hfOgpDescription" runat="server" />
    <asp:HiddenField ID="hfOgpUrl" runat="server" />
    <asp:HiddenField ID="hfOgpImage" runat="server" />
    <asp:HiddenField ID="hfOgpFbAppID" runat="server" />
    <asp:HiddenField ID="hfIDUser" runat="server"  ClientIDMode="Static"/>
    <asp:HiddenField ID="hfIDRequester" runat="server"  ClientIDMode="Static"/>
    <asp:HiddenField ID="hfIDLanguage" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfRecipeOf" ClientIDMode="Static" runat="server"/>
    <asp:HiddenField ID="hfRecipeOf2" ClientIDMode="Static" runat="server"/>
    <asp:HiddenField ID="hfEditRecipeText" ClientIDMode="Static" runat="server"/>
    <asp:HiddenField ID="hfRowOffSet" ClientIDMode="Static" runat="server" Value="0" />
    <asp:HiddenField ID="hfLightRecipeThreshold" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfQuickRecipeThreshold" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfKeywords" runat="server" />
    <asp:HiddenField ID="hfLanguageCode" runat="server" />
    <asp:HiddenField ID="hfCreationDate" runat="server" />
    <script type="text/javascript" language="javascript" src="/Js/ddSlick/jquery.ddslick.min.js"></script>
    <script type="text/javascript" language="javascript" src="/Js/Pages/RecipesBooks.min.js"></script>
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            $('#boxLoading').css({ 'visibility': 'visible' });
            $('#ddlRecipeType').ddslick({
                onSelected: function (data) {
                    ResetAndReloadRecipes();
                },
                height: 465,
                width:355
            });
            $('#ddlRecipeSource').ddslick({
                onSelected: function (data) {
                    ResetAndReloadRecipes();
                },
                width: 355
            });
            $('#<%=pnlBackground.ClientID%>').height($('#<%=pnlContent.ClientID%>').height());
        }
    </script>
    <asp:Panel ID="pnlMain" runat="server" CssClass="pnlMain" meta:resourcekey="pnlMainResource1">
        <asp:Panel ID="pnlBackground" runat="server" CssClass="pnlBackground" meta:resourcekey="pnlBackgroundResource1">
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
        <asp:Panel ID="pnlContent" runat="server" CssClass="pnlContent" meta:resourcekey="pnlContentResource1">
            <asp:Panel ID="pnlSearchResults" runat="server" CssClass="pnlSearchResults" meta:resourcekey="pnlSearchResultsResource1">
                <asp:Image ID="imgPostit" runat="server" ImageUrl="/Images/post-it_2.png" 
                                CssClass="imgPostit" Width="352px" Height="287px" meta:resourcekey="imgPostitResource1"/>
                    <asp:Panel ID="pnlUserInfo" runat="server" CssClass="pnlUserInfo" meta:resourcekey="pnlUserInfoResource1">
                        <asp:Image ID="imgUserImage" runat="server" CssClass="imgUserImage" 
                            Width="80px" Height="80px" ImageUrl="/Images/icon-userNoPic.png" meta:resourcekey="imgUserImageResource1"/>
                        <asp:Label ID="lblUserName" runat="server" Text="Il ricettario di Saverio Cammarata"
                            CssClass="lblUserName" Width="280px" meta:resourcekey="lblUserNameResource1"></asp:Label>
                        <asp:Label ID="lblUserRecipesDetails" runat="server" Text="Label" 
                            CssClass="lblUserRecipesDetails" meta:resourcekey="lblUserRecipesDetailsResource1"></asp:Label>
                    </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="pnlFilterRecipes" runat="server" CssClass="pnlFilterRecipes" meta:resourcekey="pnlFilterRecipesResource1">
                <div style="display:none;">
                    <asp:CheckBox ID="chkVegan" runat="server" ClientIDMode="Static" meta:resourcekey="chkVeganResource1"/>
                    <asp:CheckBox ID="chkVegetarian" runat="server" ClientIDMode="Static" meta:resourcekey="chkVegetarianResource1"/>
                    <asp:CheckBox ID="chkGlutenFree" runat="server" ClientIDMode="Static" meta:resourcekey="chkGlutenFreeResource1"/>
                    <asp:CheckBox ID="chkLight" runat="server" ClientIDMode="Static" meta:resourcekey="chkLightResource1"/>
                    <asp:CheckBox ID="chkQuick" runat="server" ClientIDMode="Static" meta:resourcekey="chkQuickResource1"/>
                    <img src="/Images/vegan-on.png" alt="PreLoad"/><img src="/Images/vegetarian-on.png"  alt="PreLoad"/>
                    <img src="/Images/gluten-free-on.png"  alt="PreLoad"/><img src="/Images/ico-bilancia-on.png"  alt="PreLoad"/>
                    <img src="/Images/icon-quickRecipe-on.png"  alt="PreLoad"/>
                </div>
                <div id="boxFilterOptions">
                <div id="boxOptionIcon">
                        <asp:ImageButton ID="btnVegan" runat="server" CssClass="OptionIcon MyTooltip" ToolTip="solo ricette vegane"
                            Width="50px" Height="50px" ImageUrl="/Images/vegan-off.png" onmouseover="changeImage('chkVegan', 'btnVegan', 'over')"
                            onmouseout="changeImage('chkVegan', 'btnVegan', 'out')" OnClientClick="checkboxChange('chkVegan', 'btnVegan'); return false;"
                            ClientIDMode="Static" meta:resourcekey="btnVeganResource1"/>
                        <asp:ImageButton ID="btnVegetarian" runat="server" CssClass="OptionIcon MyTooltip"
                            ToolTip="solo ricette vegetariane" Width="50px" Height="50px" ImageUrl="/Images/vegetarian-off.png"
                            onmouseover="changeImage('chkVegetarian', 'btnVegetarian', 'over')" onmouseout="changeImage('chkVegetarian', 'btnVegetarian', 'out')"
                            OnClientClick="checkboxChange('chkVegetarian', 'btnVegetarian'); return false;"
                            ClientIDMode="Static" meta:resourcekey="btnVegetarianResource1"/>
                        <asp:ImageButton ID="btnGlutenFree" runat="server" CssClass="OptionIcon MyTooltip"
                            ToolTip="solo ricette senza Glutine" Width="50px" Height="50px" ImageUrl="/Images/gluten-free-off.png"
                            onmouseover="changeImage('chkGlutenFree', 'btnGlutenFree', 'over')" onmouseout="changeImage('chkGlutenFree', 'btnGlutenFree', 'out')"
                            OnClientClick="checkboxChange('chkGlutenFree', 'btnGlutenFree'); return false;"
                            ClientIDMode="Static" meta:resourcekey="btnGlutenFreeResource1"/>
                        <asp:ImageButton ID="btnLight" runat="server" CssClass="OptionIcon MyTooltip" ToolTip="solo ricette leggere"
                            Width="50px" Height="50px" ImageUrl="/Images/ico-bilancia-off.png" onmouseover="changeImage('chkLight', 'btnLight', 'over')"
                            onmouseout="changeImage('chkLight', 'btnLight', 'out')" OnClientClick="checkboxChange('chkLight', 'btnLight'); return false;"
                            ClientIDMode="Static" meta:resourcekey="btnLightResource1"/>
                        <asp:ImageButton ID="btnQuick" runat="server" CssClass="OptionIcon MyTooltip" ToolTip="solo ricette veloci"
                            Width="50px" Height="50px" ImageUrl="/Images/icon-quickRecipe-off.png" onmouseover="changeImage('chkQuick', 'btnQuick', 'over')"
                            onmouseout="changeImage('chkQuick', 'btnQuick', 'out')" OnClientClick="checkboxChange('chkQuick', 'btnQuick'); return false;"
                            ClientIDMode="Static" meta:resourcekey="btnQuickResource1"/>
                        <asp:TextBox ID="txtRecipeFilter" CssClass="txtRecipeFilter padding8" runat="server" ClientIDMode="Static"
                            MaxLength="40" Width="350px" placeholder="Cerca ricetta" meta:resourcekey="txtRecipeFilterResource1"></asp:TextBox>
                        <asp:DropDownList ID="ddlRecipeSource" runat="server" ClientIDMode="Static" meta:resourcekey="ddlRecipeSourceResource1">
                            <asp:ListItem Value="0" Text="" meta:resourcekey="ListItemResource1"></asp:ListItem>
                            <asp:ListItem Value="1" Text="" meta:resourcekey="ListItemResource2" data-imagesrc="/Images/IconRecipeProperty/50x50/MyRecipes.png"></asp:ListItem>
                            <asp:ListItem Value="2" Text="" meta:resourcekey="ListItemResource3" data-imagesrc="/Images/IconRecipeProperty/50x50/comunityRecipes.png"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlRecipeType" runat="server"  ClientIDMode="Static" meta:resourcekey="ddlRecipeTypeResource1">
                            <asp:ListItem Value="0" Text="Tutti" meta:resourcekey="ListItemResource4"></asp:ListItem>
                        </asp:DropDownList>
                </div>
            </div>
                <asp:LinkButton ID="lnkFilter" runat="server" CssClass="lnkFilter" OnClientClick="ResetAndReloadRecipes(); return false;" meta:resourcekey="lnkFilterResource1">Trova</asp:LinkButton>
            </asp:Panel>
                <div id="pnlNoSearchResult">
                <asp:Label ID="lblNoSearchResult" runat="server" Text="Nessuna ricetta trovata" CssClass="lblNoSearchResult"
                    Width="700px" meta:resourcekey="lblNoSearchResultResource1"></asp:Label>
                <asp:HyperLink ID="lnkAddNewRecipe" runat="server" CssClass="lnkAddNewRecipe" 
                    NavigateUrl="/RecipeMng/CreateRecipe.aspx" meta:resourcekey="lnkAddNewRecipeResource1"><asp:Image ID="imgAddRecipe" 
                    ImageUrl="/Images/icon-AddRecipe-color2.png" runat="server"
                        Width="40px" Height="40px" meta:resourcekey="imgAddRecipeResource1"/>
Inserisci ora la tua ricetta!</asp:HyperLink>
                </div>
            <asp:Panel ID="pnlShowRecipeInRecipeBook" runat="server" CssClass="pnlRecipeFound" ClientIDMode="Static" meta:resourcekey="pnlShowRecipeInRecipeBookResource1">
                <div id="RecipesListContainer">
                
                </div>
            </asp:Panel>
            <div id="boxLoadMoreRecipe">
                <div id="pnlPrevPage">
                    <asp:ImageButton ID="btnPrevPage" runat="server" ImageUrl="/Images/icon-back.png"
                        ToolTip="Torna indietro" CssClass="MyTooltip" Width="50px" Height="50px" OnClientClick="Prev();return false;" meta:resourcekey="btnPrevPageResource1"/>
                </div>
                <div id="pnlNextPage">
                    <asp:ImageButton ID="btnNextPage" runat="server" ImageUrl="/Images/icon-forward-gray.png"
                        ToolTip="Scopri altre ricette" CssClass="MyTooltip" Width="50px" 
                        Height="50px" OnClientClick="Next();return false;" meta:resourcekey="btnNextPageResource1"/>
                </div>
                <div id="boxLoading">
                    <asp:Image ID="imgLoading" runat="server" ImageUrl="/Images/loadingLineOrange.gif"
                         Height="20px" Width="220px" meta:resourcekey="imgLoadingResource1"/>
                </div>
            </div>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
