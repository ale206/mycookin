<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyCookinWeb.UserInfo.Default" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
    <asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
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
    <meta name="revisit-after" content="1 days"/>
    </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:HiddenField ID="hfOgpTitle" runat="server" />
    <asp:HiddenField ID="hfKeywords" runat="server" />
    <asp:HiddenField ID="hfLanguageCode" runat="server" />
    <asp:HiddenField ID="hfCreationDate" runat="server" />
    <asp:HiddenField ID="hfOgpDescription" runat="server" Value="Find recipes from all over the world, share yours and enjoy meeting people with your same tastes." />
    <asp:HiddenField ID="hfOgpUrl" runat="server" />
    <asp:HiddenField ID="hfOgpImage" runat="server" />
    <asp:HiddenField ID="hfOgpFbAppID" runat="server" />
    <asp:HiddenField ID="hfIDLanguage" runat="server" Value="1" />
       <script type="text/javascript" language="javascript" src="/Js/Pages/Default.js"></script>
    <script type="text/javascript">
        //For Checkboxes and Filter Icons
        var boxFrigoInfo;

        function checkboxChange(chekboxId, imgId) {
            if ($("#" + chekboxId).attr('checked')) {
                $("#" + chekboxId).attr('checked', false);

                $("#" + imgId).attr('src', $("#" + imgId).attr('src').replace("-on", "-off"));

                if (chekboxId == 'chkFrigo') {
                    //                    boxFrigoInfo.close();
                    $('#lblFrigoMixNote').hide();
                    $('#lblFrigoMixNote').tipsy('hide');
                    $("#txtSearchString").attr("placeholder", GetBaseSearchPlaceHolder('<%=hfIDLanguage.Value %>'));
                }
            }
            else {
                $("#" + chekboxId).attr('checked', true);
                $("#" + imgId).attr('src', $("#" + imgId).attr('src').replace("-off", "-on"));

                if (chekboxId == 'chkFrigo') {
//                    boxFrigoInfo = noty({
//                        text: '<%=hfFrigoInfo.Value %>',
//                        layout: 'bottomCenter',
//                        timeout: false
                    //                    });
                    $('#lblFrigoMixNote').show();
                    $('#lblFrigoMixNote').tipsy('show');
                    $("#txtSearchString").attr("placeholder", GetFreeFridgeSearchPlaceHolder('<%=hfIDLanguage.Value %>'));
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
        function pageLoad() {
            $("#txtSearchString").attr("placeholder", GetBaseSearchPlaceHolder('<%=hfIDLanguage.Value %>'));
        }
    </script>

  <div style="display:none;">
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
      <%--Nota: Tag HTML nella stringa sotto generano errore. In futuro prendere i valori dal db--%>
      <asp:HiddenField ID="hfFrigoInfo" runat="server" Value="SVUOTA FRIGO:  Inserisci almeno tre ingredienti separati da virgola per trovare le ricette che puoi preparare. Prova: \'pasta, tonno, pomodoro\' " />
  </div>

    <script type="text/javascript">
        //Hide scroll bars
        HideScrollbars();
        //===
    </script>
    <div id="content">
        <div id="boxMainSearchBackGround">
        </div>
        <div id="boxMainSearch">
            <div id="boxMainSearchInternal">
                <div id="boxSearchOptionIcon">
                    <asp:ImageButton ID="btnVegan" runat="server" CssClass="OptionIcon MyTooltip" ToolTip="solo ricette vegane"
                        Width="50" Height="50" ImageUrl="/Images/vegan-off.png" onmouseover="changeImage('chkVegan', 'btnVegan', 'over')"
                        onmouseout="changeImage('chkVegan', 'btnVegan', 'out')" OnClientClick="checkboxChange('chkVegan', 'btnVegan'); return false;"
                        ClientIDMode="Static" meta:resourcekey="btnVeganResource"/>
                    <asp:ImageButton ID="btnVegetarian" runat="server" CssClass="OptionIcon MyTooltip"
                        ToolTip="solo ricette vegetariane" Width="50" Height="50" ImageUrl="/Images/vegetarian-off.png"
                        onmouseover="changeImage('chkVegetarian', 'btnVegetarian', 'over')" onmouseout="changeImage('chkVegetarian', 'btnVegetarian', 'out')"
                        OnClientClick="checkboxChange('chkVegetarian', 'btnVegetarian'); return false;"
                        ClientIDMode="Static" meta:resourcekey="btnVegetarianResource" />
                    <asp:ImageButton ID="btnGlutenFree" runat="server" CssClass="OptionIcon MyTooltip"
                        ToolTip="solo ricette senza Glutine" Width="50" Height="50" ImageUrl="/Images/gluten-free-off.png"
                        onmouseover="changeImage('chkGlutenFree', 'btnGlutenFree', 'over')" onmouseout="changeImage('chkGlutenFree', 'btnGlutenFree', 'out')"
                        OnClientClick="checkboxChange('chkGlutenFree', 'btnGlutenFree'); return false;"
                        ClientIDMode="Static" meta:resourcekey="btnGlutenFreeResource" />
                    <asp:ImageButton ID="btnLight" runat="server" CssClass="OptionIcon MyTooltip" ToolTip="solo ricette leggere"
                        Width="50" Height="50" ImageUrl="/Images/ico-bilancia-off.png" onmouseover="changeImage('chkLight', 'btnLight', 'over')"
                        onmouseout="changeImage('chkLight', 'btnLight', 'out')" OnClientClick="checkboxChange('chkLight', 'btnLight'); return false;"
                        ClientIDMode="Static" meta:resourcekey="btnLightResource" />
                    <asp:ImageButton ID="btnQuick" runat="server" CssClass="OptionIcon MyTooltip" ToolTip="solo ricette veloci"
                        Width="50" Height="50" ImageUrl="/Images/icon-quickRecipe-off.png" onmouseover="changeImage('chkQuick', 'btnQuick', 'over')"
                        onmouseout="changeImage('chkQuick', 'btnQuick', 'out')" OnClientClick="checkboxChange('chkQuick', 'btnQuick'); return false;"
                        ClientIDMode="Static" meta:resourcekey="btnQuickResource" />
                    <asp:ImageButton ID="btnEmptyFridge" runat="server" CssClass="OptionIcon MyTooltip"
                        ToolTip="svuota frigo" Width="50" Height="50" ImageUrl="/Images/mymix-off.png"
                        onmouseover="changeImage('chkFrigo', 'btnEmptyFridge', 'over')" onmouseout="changeImage('chkFrigo', 'btnEmptyFridge', 'out')"
                        OnClientClick="checkboxChange('chkFrigo', 'btnEmptyFridge'); return false;" ClientIDMode="Static" meta:resourcekey="btnEmptyFridgeResource" />
                </div>
            </div>
            <div id="boxSearch">
                <div id="boxSearchInput">
                    <div id="boxSearchField">
                        <asp:TextBox ID="txtSearchString" CssClass="Padding8" runat="server" 
                            placeholder="Ho voglia di qualcosa di dolce..." ClientIDMode="Static" 
                            onkeypress="return isSpecialHTMLChar(event)"
                            meta:resourcekey="txtSearchStringResource1"></asp:TextBox></div>
                    <div id="boxSearchButton">
                        <asp:LinkButton ID="lnkSearch" ClientIDMode="Static" runat="server" 
                            onclick="lnkSearch_Click" meta:resourcekey="lnkSearchResource1">trova</asp:LinkButton></div>
                </div>
                <asp:Panel ID="pnlAdvancedSearch" runat="server" CssClass="boxSearchAdv" 
                    meta:resourcekey="pnlAdvancedSearchResource1">
                    <asp:LinkButton Visible="false" ID="lnkAdvancedSearch" runat="server" CssClass="boldLink"
                        meta:resourcekey="lnkAdvancedSearchResource1">ricerca avanzata</asp:LinkButton>
                    <h2><asp:LinkButton Visible="true" ID="lnkAllRecipes" runat="server" CssClass="boldLink"
                        meta:resourcekey="lnkAllRecipesResource1">tutte le ricette</asp:LinkButton></h2>
                </asp:Panel>
            </div>
            <div style=" display:block; margin-top:-40px; margin-left:70px;">
            <asp:Label ID="lblFrigoMixNote" ClientIDMode="Static" runat="server" 
                    ToolTip="SVUOTA FRIGO:<br/>Inserisci almeno tre ingredienti separati da virgola<br/>per trovare le ricette che puoi preparare.<br/>Prova: 'pasta, tonno, pomodoro' " 
                    CssClass="tipsyInfoNoteW" meta:resourcekey="lblFrigoMixNoteResource1"></asp:Label></div>
        </div>
    </div>
    <asp:Panel ID="pnlSelectLanguage" runat="server" CssClass="pnlSelectLanguage" 
        meta:resourcekey="pnlSelectLanguageResource1">
        <asp:ImageButton ID="btnLangEn" ImageUrl="/Images/flag-uk.png" runat="server" 
            CssClass="btnFlag" Width="25px" Height="19px" OnClick="btnLang_Click" 
            CommandArgument="1" meta:resourcekey="btnLangEnResource1" />
        <asp:ImageButton ID="btnLangIt" ImageUrl="/Images/flag-ita.png" runat="server" 
            CssClass="btnFlag" Width="25px" Height="19px" OnClick="btnLang_Click" 
            CommandArgument="2" meta:resourcekey="btnLangItResource1" />
        <asp:ImageButton ID="btnLangEs" ImageUrl="/Images/flag-es.png" runat="server" 
            CssClass="btnFlag" Width="25px" Height="19px" OnClick="btnLang_Click" 
            CommandArgument="3" meta:resourcekey="btnLangEsResource1" />
        <asp:ImageButton ID="btnLangFr" ImageUrl="/Images/flag-fr.png" runat="server" 
            CssClass="btnFlag" Width="25px" Height="19px" Visible="False" 
            OnClick="btnLang_Click" CommandArgument="4" 
            meta:resourcekey="btnLangFrResource1" />
        <asp:ImageButton ID="btnLangDe" ImageUrl="/Images/flag-de.png" runat="server" 
            CssClass="btnFlag" Width="25px" Height="19px" Visible="False" 
            OnClick="btnLang_Click" CommandArgument="5" 
            meta:resourcekey="btnLangDeResource1" />
    </asp:Panel>
    <asp:Panel ID="pnlBanner" runat="server" CssClass="pnlBanner" Visible="False" 
        meta:resourcekey="pnlBannerResource1">
        <asp:Image ID="imgBanner" runat="server" Width="40px" Height="40px" 
            ImageUrl="/Images/mymix-off.png" CssClass="imgBanner" 
            meta:resourcekey="imgBannerResource1" />
        <asp:HyperLink ID="hlBanner" runat="server" CssClass="hlBanner" 
            meta:resourcekey="hlBannerResource1">Link verso qualcosa</asp:HyperLink><br />
        <asp:Label ID="lblBanner" runat="server" 
            Text="Descrizione banner bla bla bla bla bla bla" CssClass="lblBanner" 
            Height="100px" meta:resourcekey="lblBannerResource1" ></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlTopHomeRecipes" runat="server" CssClass="pnlTopHomeRecipes" 
        meta:resourcekey="pnlTopHomeRecipesResource1">
    </asp:Panel><div class="bg"></div>
</asp:Content>
