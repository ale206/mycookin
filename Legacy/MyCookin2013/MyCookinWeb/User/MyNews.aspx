<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master"
    AutoEventWireup="true" CodeBehind="MyNews.aspx.cs" Inherits="MyCookinWeb.User.MyNews" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register TagPrefix="MyCtrl" TagName="UserBoardBlockControl" Src="~/CustomControls/ctrlUserBoardBlock.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="UserBoardStatusControl" Src="~/CustomControls/ctrlUserBoardStatus.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
<link rel="Stylesheet" href="/Styles/PageStyle/UserBoard.min.css?ver=140618" />
<link rel="Stylesheet" href="/Styles/PageStyle/UserBoardBlock.min.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
<asp:HiddenField ID="hfIDLanguage" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hfIDUser" ClientIDMode="Static" runat="server" />
<script type="text/javascript" language="javascript">
    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height() - 1000 || $(window).scrollTop() == $(document).height() - $(window).height() - 100 || $(window).scrollTop() == $(document).height() - $(window).height()) {
            if ($('#<%=ibtnUserBoardLoadNext.ClientID %>').length > 0) {
                __doPostBack("<%=ibtnUserBoardLoadNext.UniqueID%>", "");
            }
        }
    });
    </script>
    <div style="display:none;">
        <asp:TextBox ID="txtLoadingChecker" runat="server"></asp:TextBox>
    </div>
    <br />
    <!-- Panel used to show lbResult by JQuery Box Dialog -->
    <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server" 
        meta:resourcekey="pnlResultResource1">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server" 
            meta:resourcekey="lblResultResource1"></asp:Label>
    </asp:Panel>
        <asp:Panel ID="pnlStatusBar" ClientIDMode="Static" runat="server" 
        meta:resourcekey="pnlStatusBarResource1">
            <asp:UpdatePanel ID="upnlStatusBar" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
        
                    <MyCtrl:UserBoardStatusControl ID="ctrlUserBoard" runat="server" OnStatusAdded="ReloadBoard" />

                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Panel ID="pnlLoading" CssClass="pnlLoading" ClientIDMode="Static" 
        runat="server" meta:resourcekey="pnlLoadingResource1">
            <asp:UpdateProgress ID="upStatusBarInsert" runat="server" AssociatedUpdatePanelID="upnlStatusBar">
                <ProgressTemplate>
                    <asp:Image ID="imgLoading" runat="server" 
                        ImageUrl="~/Images/loadingLineOrange.gif"  Width="180px" Height="12px" 
                        meta:resourcekey="imgLoadingResource1"></asp:Image>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </asp:Panel>

    <asp:Panel ID="pnlWrapperMainContent" ClientIDMode="Static" runat="server" 
        CssClass="pnlWrapperMainContent" 
        meta:resourcekey="pnlWrapperMainContentResource1">
        <asp:Panel ID="pnlMainContent" ClientIDMode="Static" runat="server" 
            CssClass="pnlContentMain" meta:resourcekey="pnlMainContentResource1">
            <asp:Panel ID="firstColumn" ClientIDMode="Static" runat="server" 
                meta:resourcekey="firstColumnResource1">
            </asp:Panel>
            <asp:Panel ID="pnlUserBoard" ClientIDMode="Static" runat="server" 
                CssClass="pnlUserBoard" meta:resourcekey="pnlUserBoardResource1">
                <asp:UpdatePanel ID="upnlUserBoard" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Repeater ID="rptUserBoardControl" runat="server">
                            <ItemTemplate>
                                <asp:UpdatePanel ID="upnlUserBoardBlock" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <MyCtrl:UserBoardBlockControl ID="ctrlUserBoardBlock" runat="server" TypeOfLike='<%# Eval("TypeOfLike") %>' IDUserAction='<%# Eval("IDUserAction") %>' />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:HiddenField ID="hfPageCount" runat="server" />
                        <asp:Panel ID="pnlLoadMore" ClientIDMode="Static" runat="server" 
                            CssClass="pnlLoadMore" meta:resourcekey="pnlLoadMoreResource1">
                        
                                <asp:ImageButton ID="ibtnUserBoardLoadNext" runat="server" OnClick="ibtnUserBoardLoadNext_Click"
                                    ImageUrl="/Images/icon-LoadMore_2.png" ToolTip="Load More" 
                                    AlternateText="Load More" Width="50px" Height="50px" 
                                    meta:resourcekey="ibtnUserBoardLoadNextResource1" />
                        <br />
                            <asp:UpdateProgress ID="upLoadMoreItemBoard" runat="server" AssociatedUpdatePanelID="upnlUserBoard">
                            <ProgressTemplate>
                                <asp:Image runat="server" ImageUrl="/Images/loadingLineOrange.gif" 
                                    Height="20px" Width="220px" meta:resourcekey="ImageResource1"></asp:Image>
                                </ProgressTemplate>
                            </asp:UpdateProgress>

                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ctrlUserBoard" EventName="StatusAdded" />    
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="secondColumn" ClientIDMode="Static" runat="server" 
            meta:resourcekey="secondColumnResource1">
            
        </asp:Panel>
    </asp:Panel>
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
                <asp:Image ID="imgLoadingRecipe" runat="server" ImageUrl="/Images/loadingLineOrange.gif"
                        Height="20px" Width="220px"/>
            </div>
            <div class="lblContainerTitles">
                <asp:HyperLink ID="lnkOtherRecipes" runat="server" CssClass="lnkIngredient" meta:resourcekey="lnkOtherRecipesResource1">Vedi tutto</asp:HyperLink>
            </div>
        </asp:Panel>
    </asp:Panel>
    <script type="text/javascript" language="javascript" src="/Js/Pages/MyNews.min.js"></script>
    <script language="javascript" type="text/javascript">
        $('#boxSimilarRecipes').css({ 'display': 'block' });
        $('#boxLoadingUsers').css({ 'display': 'block' });
        LoadSimilarRecipes('boxSimilarRecipes', 'boxLoadingSimilarRecipes');
        LoadUsers('boxUsers', 'boxLoadingUsers');
    </script>
</asp:Content>
