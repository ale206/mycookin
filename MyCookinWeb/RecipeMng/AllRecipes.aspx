<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master" AutoEventWireup="true" CodeBehind="AllRecipes.aspx.cs" Inherits="MyCookinWeb.RecipeMng.AllRecipes" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/AllRecipes.min.css" />
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
    <meta name="revisit-after" content="2 days"/>
</asp:Content>
<asp:Content ID="cntMain" ContentPlaceHolderID="cphMain" runat="server">
    <script type="text/javascript" language="javascript" src="/Js/Pages/AllRecipes.min.js"></script>
    <script type="text/javascript" language="javascript" src="/Js/ddSlick/jquery.ddslick.min.js"></script>
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            $('#boxLoading').css({ 'visibility': 'visible' });
            $('#ddlRecipeType').ddslick({
                onSelected: function (data) {
                    ResetAndReloadRecipes();
                },
                height: 465,
                width: 300
            });
            $('#<%=pnlBackground.ClientID%>').height($('#<%=pnlContent.ClientID%>').height());
        } 
    </script>
    <asp:HiddenField ID="hfRowOffSet" ClientIDMode="Static" runat="server" Value="0" />
    <asp:HiddenField ID="hfOgpTitle" runat="server" />
    <asp:HiddenField ID="hfKeywords" runat="server" />
    <asp:HiddenField ID="hfLanguageCode" runat="server" />
    <asp:HiddenField ID="hfCreationDate" runat="server" />
    <asp:HiddenField ID="hfOgpDescription" runat="server" />
    <asp:HiddenField ID="hfOgpUrl" runat="server" />
    <asp:HiddenField ID="hfOgpImage" runat="server" />
    <asp:HiddenField ID="hfOgpFbAppID" runat="server" />
    <asp:HiddenField ID="hfReferrerURL" runat="server" />
    <asp:HiddenField ID="hfIDLanguage" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfRecipeOf" ClientIDMode="Static" runat="server" Value=""/>
    <asp:HiddenField ID="hfRecipeOf2" ClientIDMode="Static" runat="server" Value=""/>
    <asp:HiddenField ID="hfLightRecipeThreshold" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfQuickRecipeThreshold" ClientIDMode="Static" runat="server" />
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
                            ClientIDMode="Static" meta:resourcekey="btnQuickResource1" />
                        <div id="boxDishType">
                            <asp:DropDownList ID="ddlRecipeType" runat="server" ClientIDMode="Static" meta:resourcekey="ddlRecipeTypeResource1">
                                <asp:ListItem Value="0" Text="Tutte le ricette" meta:resourcekey="ListItemResource1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                </div>
            </div>
            <div id="RecipesListContainer">
                
            </div>
            <div id="boxLoadMoreRecipe">
                <div id="boxLoadButton">
                    <asp:ImageButton ID="imgLoadMoreRecipe" runat="server" OnClientClick="LoadMoreRecipe();return false;"
                                    ImageUrl="/Images/icon-LoadMore_2.png" ToolTip="Load More" 
                                    AlternateText="Load More" Width="50px" Height="50px" meta:resourcekey="imgLoadMoreRecipeResource1" />
                </div>
                <div id="boxLoading">
                    <asp:Image ID="imgLoading" runat="server" ImageUrl="/Images/loadingLineOrange.gif"
                         Height="20px" Width="220px" meta:resourcekey="imgLoadingResource1" />
                </div>
            </div>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
