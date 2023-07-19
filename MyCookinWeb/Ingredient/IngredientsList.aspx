<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master" AutoEventWireup="true" CodeBehind="IngredientsList.aspx.cs" Inherits="MyCookinWeb.IngredientWeb.IngredientsList" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/IngredientsList.min.css" />
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
    <script type="text/javascript" language="javascript" src="/Js/Pages/IngredientsList.min.js"></script>
    <script type="text/javascript" language="javascript" src="/Js/ddSlick/jquery.ddslick.min.js"></script>
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            $('#boxLoading').css({ 'visibility': 'visible' });
            $('#ddlIngrStartWith').ddslick({
                onSelected: function (data) {
                    ResetAndReloadIngredients();
                }
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
                  <asp:CheckBox ID="chkHotSpicy" runat="server" ClientIDMode="Static" meta:resourcekey="chkHotSpicyResource1"/>
                    <img src="/Images/vegan-on.png" alt="PreLoad"/><img src="/Images/vegetarian-on.png"  alt="PreLoad"/>
                    <img src="/Images/gluten-free-on.png"  alt="PreLoad"/><img src="/Images/icon-HotSpicy-on.png"  alt="PreLoad"/>
            </div>
            <div id="boxFilterOptions">
                <div id="boxOptionIcon">
                        <asp:ImageButton ID="btnVegan" runat="server" CssClass="OptionIcon MyTooltip" ToolTip="solo ingredienti vegani"
                            Width="50px" Height="50px" ImageUrl="/Images/vegan-off.png" onmouseover="changeImage('chkVegan', 'btnVegan', 'over')"
                            onmouseout="changeImage('chkVegan', 'btnVegan', 'out')" OnClientClick="checkboxChange('chkVegan', 'btnVegan'); return false;"
                            ClientIDMode="Static" meta:resourcekey="btnVeganResource1"/>
                        <asp:ImageButton ID="btnVegetarian" runat="server" CssClass="OptionIcon MyTooltip"
                            ToolTip="solo ingredienti vegetariani" Width="50px" Height="50px" ImageUrl="/Images/vegetarian-off.png"
                            onmouseover="changeImage('chkVegetarian', 'btnVegetarian', 'over')" onmouseout="changeImage('chkVegetarian', 'btnVegetarian', 'out')"
                            OnClientClick="checkboxChange('chkVegetarian', 'btnVegetarian'); return false;"
                            ClientIDMode="Static" meta:resourcekey="btnVegetarianResource1"/>
                        <asp:ImageButton ID="btnGlutenFree" runat="server" CssClass="OptionIcon MyTooltip"
                            ToolTip="solo ingredienti senza Glutine" Width="50px" Height="50px" ImageUrl="/Images/gluten-free-off.png"
                            onmouseover="changeImage('chkGlutenFree', 'btnGlutenFree', 'over')" onmouseout="changeImage('chkGlutenFree', 'btnGlutenFree', 'out')"
                            OnClientClick="checkboxChange('chkGlutenFree', 'btnGlutenFree'); return false;"
                            ClientIDMode="Static" meta:resourcekey="btnGlutenFreeResource1"/>
                        <asp:ImageButton ID="btnHotSpicy" runat="server" CssClass="OptionIcon MyTooltip" ToolTip="solo ingredienti piccanti"
                            Width="50px" Height="50px" ImageUrl="/Images/icon-HotSpicy-off.png" onmouseover="changeImage('chkHotSpicy', 'btnHotSpicy', 'over')"
                            onmouseout="changeImage('chkHotSpicy', 'btnHotSpicy', 'out')" OnClientClick="checkboxChange('chkHotSpicy', 'btnHotSpicy'); return false;"
                            ClientIDMode="Static" meta:resourcekey="btnHotSpicyResource1"/>
                        <div id="boxDishType">
                            <asp:DropDownList ID="ddlIngrStartWith" runat="server" ClientIDMode="Static" meta:resourcekey="ddlIngrStartWithResource1">
                                <asp:ListItem Value="0" Text="Tutti gli Ingredienti" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                <asp:ListItem Value="a" Text="a" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                <asp:ListItem Value="b" Text="b" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                <asp:ListItem Value="c" Text="c" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                <asp:ListItem Value="d" Text="d" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                <asp:ListItem Value="e" Text="e" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                <asp:ListItem Value="f" Text="f" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                <asp:ListItem Value="g" Text="g" meta:resourcekey="ListItemResource8"></asp:ListItem>
                                <asp:ListItem Value="h" Text="h" meta:resourcekey="ListItemResource9"></asp:ListItem>
                                <asp:ListItem Value="i" Text="i" meta:resourcekey="ListItemResource10"></asp:ListItem>
                                <asp:ListItem Value="j" Text="j" meta:resourcekey="ListItemResource11"></asp:ListItem>
                                <asp:ListItem Value="k" Text="k" meta:resourcekey="ListItemResource12"></asp:ListItem>
                                <asp:ListItem Value="l" Text="l" meta:resourcekey="ListItemResource13"></asp:ListItem>
                                <asp:ListItem Value="m" Text="m" meta:resourcekey="ListItemResource14"></asp:ListItem>
                                <asp:ListItem Value="n" Text="n" meta:resourcekey="ListItemResource15"></asp:ListItem>
                                <asp:ListItem Value="o" Text="o" meta:resourcekey="ListItemResource16"></asp:ListItem>
                                <asp:ListItem Value="p" Text="p" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                <asp:ListItem Value="q" Text="q" meta:resourcekey="ListItemResource18"></asp:ListItem>
                                <asp:ListItem Value="r" Text="r" meta:resourcekey="ListItemResource19"></asp:ListItem>
                                <asp:ListItem Value="s" Text="s" meta:resourcekey="ListItemResource20"></asp:ListItem>
                                <asp:ListItem Value="t" Text="t" meta:resourcekey="ListItemResource21"></asp:ListItem>
                                <asp:ListItem Value="u" Text="u" meta:resourcekey="ListItemResource22"></asp:ListItem>
                                <asp:ListItem Value="v" Text="v" meta:resourcekey="ListItemResource23"></asp:ListItem>
                                <asp:ListItem Value="w" Text="w" meta:resourcekey="ListItemResource24"></asp:ListItem>
                                <asp:ListItem Value="x" Text="x" meta:resourcekey="ListItemResource25"></asp:ListItem>
                                <asp:ListItem Value="y" Text="y" meta:resourcekey="ListItemResource26"></asp:ListItem>
                                <asp:ListItem Value="z" Text="z" meta:resourcekey="ListItemResource27"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                </div>
                </div>
                <div id="IngredientsListContainer">
                
            </div>
            <div id="boxLoadMoreIngredients">
                <div id="boxLoadButton">
                    <asp:ImageButton ID="imgLoadMoreIngredient" runat="server" OnClientClick="LoadMoreIngredients();return false;"
                                    ImageUrl="/Images/icon-LoadMore_2.png" ToolTip="Load More" 
                                    AlternateText="Load More" Width="50px" Height="50px" meta:resourcekey="imgLoadMoreIngredientResource1"/>
                </div>
                <div id="boxLoading">
                    <asp:Image ID="imgLoading" runat="server" ImageUrl="/Images/loadingLineOrange.gif"
                         Height="20px" Width="220px" meta:resourcekey="imgLoadingResource1"/>
                </div>
            </div>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
