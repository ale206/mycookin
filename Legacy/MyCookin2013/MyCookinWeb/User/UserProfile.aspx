<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master"
    AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="MyCookinWeb.User.UserProfile" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register TagPrefix="MyCtrl" TagName="UserBoardBlockControl" Src="~/CustomControls/ctrlUserBoardBlock.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="UserBoardStatusControl" Src="~/CustomControls/ctrlUserBoardStatus.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="UserBoardPostOnFriendBoard" Src="~/CustomControls/ctrlUserBoardPostOnFriendBoard.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/UserBoard.min.css" />
    <link rel="Stylesheet" href="/Styles/PageStyle/UserBoardBlock.min.css" />
    <link rel="Stylesheet" href="/Styles/PageStyle/UserProfile.min.css" />
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
    <meta name="revisit-after" content="5 days"/>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:HiddenField ID="hfOgpTitle" runat="server" />
    <asp:HiddenField ID="hfKeywords" runat="server" />
    <asp:HiddenField ID="hfLanguageCode" runat="server" />
    <asp:HiddenField ID="hfCreationDate" runat="server" />
    <asp:HiddenField ID="hfOgpDescription" runat="server" />
    <asp:HiddenField ID="hfOgpUrl" runat="server" />
    <asp:HiddenField ID="hfOgpImage" runat="server" />
    <asp:HiddenField ID="hfOgpFbAppID" runat="server" />
<script language="javascript" type="text/javascript">
    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height() - 1000 || $(window).scrollTop() == $(document).height() - $(window).height() - 100 || $(window).scrollTop() == $(document).height() - $(window).height()) {
            if ($('#<%=ibtnUserBoardLoadNext.ClientID %>').length > 0) {
                __doPostBack("<%=ibtnUserBoardLoadNext.UniqueID%>", "");
            }
        }
    });
</script>
    <br />
    <asp:HiddenField ID="hfUserName" runat="server" />
    <asp:HiddenField ID="hfIDUserRequested" runat="server" />
    <!-- Panel used to show lbResult by JQuery Box Dialog -->
    <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server" 
        meta:resourcekey="pnlResultResource1">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server" 
            meta:resourcekey="lblResultResource1"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlInfo" runat="server" CssClass="pnlInfo" 
        meta:resourcekey="pnlInfoResource1">
        <asp:Panel ID="pnlUserInformations" ClientIDMode="Static" runat="server" 
            meta:resourcekey="pnlUserInformationsResource1">
            <asp:UpdatePanel ID="upnUserInformationsLeft" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlUserInformationsLeft" ClientIDMode="Static" runat="server" 
                        meta:resourcekey="pnlUserInformationsLeftResource1">
                        <asp:Image ID="ProfileImage" ClientIDMode="Static" ImageAlign="Left" 
                            runat="server" meta:resourcekey="ProfileImageResource1" />
                        <asp:Panel ID="pnlUserTextInfo" runat="server" CssClass="pnlUserTextInfo" 
                            meta:resourcekey="pnlUserTextInfoResource1">
                            <asp:Label ID="lblName" runat="server" CssClass="lblUserTextInfo" 
                                meta:resourcekey="lblNameResource1"></asp:Label>
                            <div style="height:20px;">
                                <asp:Image ID="imgOnline" CssClass="AlignMiddle" 
                                    ImageUrl="/Images/icon-green-circle.png" Width="16px" Height="16px" 
                                    runat="server" meta:resourcekey="imgOnlineResource1" />
                                <asp:Image ID="imgOffline" CssClass="AlignMiddle" 
                                    ImageUrl="/Images/icon-empty-circle.png" Width="16px" Height="16px" 
                                    runat="server" meta:resourcekey="imgOfflineResource1" />
                                <asp:Label ID="lblLastTimeOnline" runat="server" 
                                    CssClass="lblDatePublish AlignMiddle" 
                                    meta:resourcekey="lblLastTimeOnlineResource1"></asp:Label>
                            </div>
                            <asp:Label ID="lblFollowingYou" runat="server" 
                                Text="Segue i tuoi aggiornamenti" CssClass="lblUserTextInfo" 
                                meta:resourcekey="lblFollowingYouResource1"></asp:Label>
                            <asp:Label ID="lblNumberOfFollowers" runat="server" 
                                CssClass="lblUserTextInfoNum" meta:resourcekey="lblNumberOfFollowersResource1"></asp:Label>
                            <asp:LinkButton ID="lbtnShowFollowers" runat="server" ToolTip="Followers" 
                                OnClick="lbtnShowFollowers_Click" CssClass="lblUserTextInfoLink" 
                                meta:resourcekey="lbtnShowFollowersResource1">Followers</asp:LinkButton>
                            <asp:Label ID="lblNumberOfFollowing" runat="server" 
                                CssClass="lblUserTextInfoNum" meta:resourcekey="lblNumberOfFollowingResource1"></asp:Label>
                            <asp:LinkButton ID="lbtnShowFollowing" runat="server" ToolTip="Following" 
                                OnClick="lbtnShowFollowing_Click" CssClass="lblUserTextInfoLink" 
                                meta:resourcekey="lbtnShowFollowingResource1">Following</asp:LinkButton>
                            <div style="display:block; margin-top:5px; margin-bottom:15px;">
                                <asp:Image ID="imgRecipeBook" CssClass="imgRecipeBook" runat="server" ImageUrl="/Images/icon-recipeBook-color.png"
                                    Width="30px" Height="30px" meta:resourcekey="imgRecipeBookResource1" />
                                <asp:HyperLink ID="lnkRecipeBook" runat="server" CssClass="lnkRecipeBook" meta:resourcekey="lnkRecipeBookResource1"
                                    Text="Go to the Recipe Book">
                                </asp:HyperLink></div>
                            <asp:HyperLink ID="lnkPersonalSite" runat="server" Visible="false" CssClass="lnkPersonalSite" Target="_blank"></asp:HyperLink>
                        </asp:Panel>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Panel ID="pnlUserInformationsRight" ClientIDMode="Static" runat="server" 
                meta:resourcekey="pnlUserInformationsRightResource1">
                <asp:UpdatePanel ID="pnlManageFriendship" runat="server">
                    <ContentTemplate>
                        <asp:Image ID="imgFriendshipActionLoading" 
                            ImageUrl="~/Images/icon-followActionLoading.gif" Width="50px" Height="50px"
                            Visible="False" runat="server" 
                            meta:resourcekey="imgFriendshipActionLoadingResource1" />
                        <asp:ImageButton ID="btnRequestFriendship" CssClass="btnRequestFriendship" ClientIDMode="Static"
                            ToolTip="Segui i suoi aggiornamenti" ImageUrl="~/Images/icon-follow.png" 
                            runat="server" Width="50px" Height="50px"
                            OnClick="btnRequestFriendship_Click" 
                            meta:resourcekey="btnRequestFriendshipResource1" />
                        <asp:ImageButton ID="btnRemoveFriendship" CssClass="btnRemoveFriendship" 
                            ClientIDMode="Static" Width="50px" Height="50px"
                            ToolTip="NON SEGUIRE PIU'" ImageUrl="~/Images/icon-dontfollow.png" runat="server"
                            OnClick="btnRemoveFriendship_Click" 
                            meta:resourcekey="btnRemoveFriendshipResource1" />

                        <asp:Panel ID="pnlReportUserSpam" CssClass="pnlReportUserSpam" runat="server">
                            <asp:ImageButton ID="btnReportUserSpam" runat="server" CssClass="btnReportUserSpam"
                                ImageUrl="~/Images/icon-spam.png" Width="16px" Height="16px" 
                                OnClick="btnReportUserSpam_Click" ToolTip="Report User Spam" 
                                meta:resourcekey="btnReportUserSpamResource1" />
                        </asp:Panel>

                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="upWaitForFollow" runat="server" AssociatedUpdatePanelID="pnlManageFriendship">
                    <ProgressTemplate>
                        <asp:Image ID="imgFollowLoading" runat="server" 
                            ImageUrl="/Images/icon_loader.gif" Width="16px" Height="16px" 
                            meta:resourcekey="imgFollowLoadingResource1" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:Panel ID="pnlSpotify" runat="server" 
                    meta:resourcekey="pnlSpotifyResource1">
                    <iframe runat="server" id="ifrSpotify" width="300" height="80" frameborder="0" allowtransparency="true">
                    </iframe>
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    <%--    <asp:Panel ID="pnlActionButtons" ClientIDMode="Static" runat="server">
        <%-- Action Buttons (New Recipe, New Event, ...)
    </asp:Panel>
    --%>
    <%--Update own status--%>
    <asp:Panel ID="pnlStatusBar" ClientIDMode="Static" runat="server">
        <asp:UpdatePanel ID="upnlStatusBar" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <%--Status Bar--%>
                <MyCtrl:UserBoardStatusControl ID="ctrlUserBoard" runat="server" OnStatusAdded="ReloadBoard" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <%--For post on Friend's UserBoard--%>
    <asp:Panel ID="pnlPostOnFriendUserBoard" ClientIDMode="Static" runat="server">
        <asp:UpdatePanel ID="upnlPostOnFriendUserBoard" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <%--Post Bar--%>
               
                <MyCtrl:UserBoardPostOnFriendBoard ID="ctrlUserBoardPostOnFriendBoard" runat="server"
                        OnStatusAdded="ReloadBoard" />

            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="pnlLoading" CssClass="pnlLoading" ClientIDMode="Static" runat="server">
        <asp:UpdateProgress ID="upStatusBarInsert" runat="server" AssociatedUpdatePanelID="upnlStatusBar">
            <ProgressTemplate>
                <asp:Image ID="imgLoading" runat="server" ImageUrl="~/Images/loadingLineOrange.gif"
                     Width="180" Height="12"></asp:Image>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <asp:Panel ID="pnlWrapperMainContent" ClientIDMode="Static" runat="server">
        <asp:Panel ID="pnlMainContent" ClientIDMode="Static" runat="server">
            <asp:Panel ID="firstColumn" ClientIDMode="Static" runat="server">
                <%--FIRST COLUMN--%>
            </asp:Panel>
            <asp:Panel ID="pnlUserBoard" ClientIDMode="Static" runat="server" CssClass="pnlUserBoard">
                <%--BOARD--%>
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
                        <%--Number of Pages for Paging--%>
                        <asp:HiddenField ID="hfPageCount" runat="server" />
                        <%--Number Of Current Paging--%>
                        <%--<asp:HiddenField ID="hfPageIndex" runat="server" Value="1" />--%>
                        <%--Button to Load Previous UserBoard Blocks--%>
                        <%--<asp:ImageButton ID="ibtnUserBoardLoadPrevious" runat="server" OnClick="ibtnUserBoardLoadPrevious_Click"
                            ImageUrl="~/Images/Left.png" />--%>
                        <%--Button to Load Next UserBoard Blocks--%>
                        <%--<asp:ImageButton ID="ibtnUserBoardLoadNext" runat="server" OnClick="ibtnUserBoardLoadNext_Click"
                            ImageUrl="~/Images/Right.png" />--%>
                        <asp:Panel ID="pnlLoadMore" ClientIDMode="Static" runat="server" CssClass="pnlLoadMore">
                            <asp:ImageButton ID="ibtnUserBoardLoadNext" runat="server" OnClick="ibtnUserBoardLoadNext_Click"
                                ImageUrl="/Images/icon-LoadMore_2.png" ToolTip="Load More" AlternateText="Load More" Width="50" Height="50" />
                            <br />
                            <asp:UpdateProgress ID="upLoadMoreItemBoard" runat="server" AssociatedUpdatePanelID="upnlUserBoard">
                                <ProgressTemplate>
                                    <asp:Image runat="server" ImageUrl="/Images/loadingLineOrange.gif" Height="20" Width="220">
                                    </asp:Image>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ctrlUserBoard" EventName="StatusAdded" />
                        <asp:AsyncPostBackTrigger ControlID="ctrlUserBoardPostOnFriendBoard" EventName="StatusAdded" />                        
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="secondColumn" ClientIDMode="Static" runat="server">
            <%--SECOND COLUMN--%>
            <asp:Panel ID="pnlProfileComplete" runat="server">
                <asp:Label ID="lblProfileCompleteTitle" runat="server" Text="" Visible="false"></asp:Label><br />
                <asp:Label ID="lblProfileComplete" runat="server" Text="" Visible="false"></asp:Label>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
