<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master" AutoEventWireup="true" CodeBehind="MyRecipesBook.aspx.cs" Inherits="MyCookinWeb.RecipeWeb.MyRecipesBook" culture="auto" meta:resourcekey="PageResource2" uiculture="auto" %>
<%@ Register TagName="RecipeBlock" TagPrefix="MyCtrl" Src="~/CustomControls/ctrlMyRecipesBookBlock.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/FindRecipes.min.css" />
    <link rel="Stylesheet" href="/Styles/PageStyle/MyRecipesBook.min.css" />
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
</asp:Content>
<asp:Content ID="cntMain" ContentPlaceHolderID="cphMain" runat="server">
    <asp:HiddenField ID="hfOgpTitle" runat="server" />
    <asp:HiddenField ID="hfOgpDescription" runat="server" />
    <asp:HiddenField ID="hfOgpUrl" runat="server" />
    <asp:HiddenField ID="hfOgpImage" runat="server" />
    <asp:HiddenField ID="hfOgpFbAppID" runat="server" />
<script language="javascript" type="text/javascript">
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
</script>
    <asp:Panel ID="pnlMain" runat="server" CssClass="pnlMain" 
        meta:resourcekey="pnlMainResource1">
        <asp:UpdatePanel ID="upnMain" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfRowOffSet" runat="server" />
                <asp:HiddenField ID="hfIDUser" runat="server" />
                <asp:HiddenField ID="hfLoadRecipeError" runat="server" />
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
                    <asp:Panel ID="pnlSearchResults" runat="server" CssClass="pnlSearchResults" 
                        meta:resourcekey="pnlSearchResultsResource1">
                        <asp:Image ID="imgPostit" runat="server" ImageUrl="/Images/post-it_2.png" 
                            CssClass="imgPostit" Width="352px" Height="287px" 
                            meta:resourcekey="imgPostitResource1" />
                        <asp:Panel ID="pnlUserInfo" runat="server" CssClass="pnlUserInfo" 
                            meta:resourcekey="pnlUserInfoResource1">
                            <asp:Image ID="imgUserImage" runat="server" CssClass="imgUserImage" 
                                Width="80px" Height="80px" ImageUrl="/Images/icon-userNoPic.png" 
                                meta:resourcekey="imgUserImageResource1" />
                            <asp:Label ID="lblUserName" runat="server" Text="Il ricettario di Saverio Cammarata"
                                CssClass="lblUserName" Width="280px" 
                                meta:resourcekey="lblUserNameResource1"></asp:Label>
                            <asp:Label ID="lblUserRecipesDetails" runat="server" Text="Label" 
                                CssClass="lblUserRecipesDetails" 
                                meta:resourcekey="lblUserRecipesDetailsResource1"></asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="pnlFilterRecipes" runat="server" CssClass="pnlFilterRecipes" 
                            meta:resourcekey="pnlFilterRecipesResource1">
                            <asp:TextBox ID="txtRecipeFilter" CssClass="txtRecipeFilter padding8" runat="server"
                                MaxLength="40" Width="350px" placeholder="Cerca ricetta" 
                                meta:resourcekey="txtRecipeFilterResource1"></asp:TextBox>
                            <asp:Label ID="lblRecipeSource" runat="server" Text="Ricette da mostrare" 
                                CssClass="FilterLabel" meta:resourcekey="lblRecipeSourceResource1"></asp:Label>
                            <asp:DropDownList ID="ddlRecipeSource" runat="server" 
                                meta:resourcekey="ddlRecipeSourceResource1">
                                <asp:ListItem Value="0" Text="Tutte le ricette" 
                                    meta:resourcekey="ListItemResource1"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Le mie ricette" 
                                    meta:resourcekey="ListItemResource2"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Ricette aggiunte al ricettario" 
                                    meta:resourcekey="ListItemResource3"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblRecipeType" runat="server" Text="Tipi di piatto" 
                                CssClass="FilterLabel" meta:resourcekey="lblRecipeTypeResource1"></asp:Label>
                            <asp:DropDownList ID="ddlRecipeType" runat="server" 
                                meta:resourcekey="ddlRecipeTypeResource1">
                                <asp:ListItem Value="0" Text="Tutti" meta:resourcekey="ListItemResource4"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:LinkButton ID="lnkFilter" runat="server" CssClass="lnkFilter" onclick="lnkSearch_Click"
                                OnClientClick="ResetRowOffSet()" meta:resourcekey="lnkFilterResource1">Trova</asp:LinkButton>
                        </asp:Panel>
                        <asp:Panel ID="pnlNoSearchResult" runat="server" Visible="False" 
                            CssClass="pnlNoSearchResult" meta:resourcekey="pnlNoSearchResultResource1">
                            <asp:Label ID="lblNoSearchResult" runat="server" Text="Nessuna ricetta trovata" CssClass="lblNoSearchResult"
                                Width="700px" meta:resourcekey="lblNoSearchResultResource1"></asp:Label>
                            <asp:HyperLink ID="lnkAddNewRecipe" runat="server" CssClass="lnkAddNewRecipe" 
                                NavigateUrl="/RecipeMng/CreateRecipe.aspx" 
                                meta:resourcekey="lnkAddNewRecipeResource1"><asp:Image ID="imgAddRecipe" 
                                ImageUrl="/Images/icon-AddRecipe-color2.png" runat="server"
                                    Width="40px" Height="40px" meta:resourcekey="imgAddRecipeResource1" />
Inserisci ora la tua ricetta!</asp:HyperLink>
                        </asp:Panel>
                        <asp:Panel ID="pnlShowRecipeInRecipeBook" runat="server" 
                            CssClass="pnlRecipeFound" meta:resourcekey="pnlShowRecipeInRecipeBookResource1">
                            <asp:Panel ID="pnlRecipe1" runat="server" CssClass="pnlRecipeBlockExt" 
                                Visible="False" meta:resourcekey="pnlRecipe1Resource1">
                                <MyCtrl:RecipeBlock ID="ShowRecipe1" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlRecipe2" runat="server" CssClass="pnlRecipeBlockExt" 
                                Visible="False" meta:resourcekey="pnlRecipe2Resource1">
                                <MyCtrl:RecipeBlock ID="ShowRecipe2" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlRecipe3" runat="server" CssClass="pnlRecipeBlockExt" 
                                Visible="False" meta:resourcekey="pnlRecipe3Resource1">
                                <MyCtrl:RecipeBlock ID="ShowRecipe3" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlRecipe4" runat="server" CssClass="pnlRecipeBlockExt" 
                                Visible="False" meta:resourcekey="pnlRecipe4Resource1">
                                <MyCtrl:RecipeBlock ID="ShowRecipe4" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlRecipe5" runat="server" CssClass="pnlRecipeBlockExt" 
                                Visible="False" meta:resourcekey="pnlRecipe5Resource1">
                                <MyCtrl:RecipeBlock ID="ShowRecipe5" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlRecipe6" runat="server" CssClass="pnlRecipeBlockExt" 
                                Visible="False" meta:resourcekey="pnlRecipe6Resource1">
                                <MyCtrl:RecipeBlock ID="ShowRecipe6" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlRecipe7" runat="server" CssClass="pnlRecipeBlockExt" 
                                Visible="False" meta:resourcekey="pnlRecipe7Resource1">
                                <MyCtrl:RecipeBlock ID="ShowRecipe7" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlRecipe8" runat="server" CssClass="pnlRecipeBlockExt" 
                                Visible="False" meta:resourcekey="pnlRecipe8Resource1">
                                <MyCtrl:RecipeBlock ID="ShowRecipe8" runat="server" />
                            </asp:Panel>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
