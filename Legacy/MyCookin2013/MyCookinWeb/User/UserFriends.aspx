<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master"
    AutoEventWireup="true" CodeBehind="UserFriends.aspx.cs" Inherits="MyCookinWeb.UserInfo.UserFriends" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/UserBoard.min.css" />
    <link rel="Stylesheet" href="/Styles/PageStyle/UserBoardBlock.min.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <br />
    <!-- Panel used to show lbResult by JQuery Box Dialog -->
    <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlUserInformations" ClientIDMode="Static" runat="server" CssClass="pnlUserInformationsFollow">
        <asp:Panel ID="pnlUserInformationsLeft" ClientIDMode="Static" runat="server">
            <p>
                <asp:Image ID="ProfileImage" ClientIDMode="Static" ImageAlign="Left" runat="server" />
                <asp:Label ID="lblName" runat="server" Text="" CssClass="lblName"></asp:Label>
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlUserInformationsRight" ClientIDMode="Static" runat="server">
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnlFriendsContainer" ClientIDMode="Static" runat="server" CssClass="pnlFriendsContainer">
        <asp:UpdatePanel ID="upnlFriendsList" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <p style="display: block; margin: 10px;">
                    <asp:Label ID="lblFriendsList" CssClass="sectionTitle" runat="server" Text="Amici"></asp:Label>
                    <asp:Label ID="lblNumberOfFriends" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    &nbsp;<asp:Repeater ID="rptFriendsList" runat="server">
                        <ItemTemplate>
                            <div class="ContainerFriendPic">
                                <asp:ImageButton ID="btnIDFriendsListUserProfile" runat="server" CommandArgument='<%# Eval("Friend") %>'
                                    CssClass="imgFollow" ImageUrl="" OnClick="btnShowFriendProfile_Click" Width="80"
                                    Height="80" OnDataBinding="btnIDFriendsList_DataBinding" ToolTip="Show Profile" />
                                <br />
                                <asp:Label ID="lblIDFriendsList" runat="server" OnDataBinding="WriteNameOfUser_DataBinding"
                                    Text='<%# Eval("Friend") %>'></asp:Label>
                                <%-- 
                            Activate this, and the relative button on codeBehind to show delete button near the name                            
                            <asp:ImageButton ID="btnIDFriendsListDelete" CssClass="ButtonFriendsList" ToolTip="Delete Friendship" ImageUrl="~/Images/removeFriendship.png" runat="server" ClientIDMode="Static" CommandArgument='<%# Eval("Friend") %>' OnClick="btnRemoveFriendship_Click" OnClientClick="return JCOnfirm(this, 'Remove', 'Sicuro', 'SI', 'NO');" />
                                --%>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <p>
                    </p>
                </p>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upnlFollowersList" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <p style="display: block; margin: 10px;">
                    <asp:Label ID="lblFollowersList" CssClass="sectionTitle" runat="server" Text="Persone che mi seguono"></asp:Label>
                    <asp:Label ID="lblNumberOfFollowers" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    &nbsp;<asp:Repeater ID="rptFollowersList" runat="server">
                        <ItemTemplate>
                            <div class="ContainerFriendPic">
                                <asp:ImageButton ID="btnIDFollowersListUserProfile" runat="server" CommandArgument='<%# Eval("Follower") %>'
                                    CssClass="imgFollow" ImageUrl="" OnClick="btnShowFriendProfile_Click" Width="80"
                                    Height="80" OnDataBinding="btnIDFriendsList_DataBinding" ToolTip="Show Profile" />
                                <br />
                                <asp:Label ID="lblIDollowerList" runat="server" OnDataBinding="WriteNameOfUser_DataBinding"
                                    Text='<%# Eval("Follower") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <p>
                    </p>
                </p>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upnlFollowingList" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <p style="display: block; margin: 10px;">
                    <asp:Label ID="lblFollowingList" CssClass="sectionTitle" runat="server" Text="Persone che seguo"></asp:Label>
                    <asp:Label ID="lblNumberOfFollowing" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    &nbsp;<asp:Repeater ID="rptFollowingList" runat="server">
                        <ItemTemplate>
                            <div class="ContainerFriendPic">
                                <asp:ImageButton ID="btnIDFollowingListUserProfile" runat="server" CommandArgument='<%# Eval("Following") %>'
                                    CssClass="imgFollow" ImageUrl="" OnClick="btnShowFriendProfile_Click" Width="80"
                                    Height="80" OnDataBinding="btnIDFriendsList_DataBinding" ToolTip="Show Profile" />
                                <br />
                                <asp:Label ID="lblIDollowingList" runat="server" OnDataBinding="WriteNameOfUser_DataBinding"
                                    Text='<%# Eval("Following") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <p>
                    </p>
                </p>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--COMMON FRIENDS - DISABLED--%>
        <%--<asp:UpdatePanel ID="upnlCommonFriends" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <p>
                    <asp:Label ID="lblCommonFriendsList" CssClass="sectionTitle" runat="server" Text="Common Friends"></asp:Label>
                    <asp:Label ID="lblNumberOfCommonFriends" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    &nbsp;<asp:Repeater ID="rptCommonFriendsList" runat="server">
                        <ItemTemplate>
                            <div class="ContainerFriendPic">
                                <asp:ImageButton ID="btnIDCommonFriendsListUserProfile" runat="server" CommandArgument='<%# Eval("IDUser1") %>'
                                    CssClass="imgFollow" ImageUrl="" OnClick="btnShowFriendProfile_Click" OnDataBinding="btnIDFriendsList_DataBinding"
                                    ToolTip="Show Profile" />
                                <br />
                                <asp:Label ID="lblIDCommonFriendsList" runat="server" OnDataBinding="WriteNameOfUser_DataBinding"
                                    Text='<%# Eval("IDUser1") %>'></asp:Label>
                                <%-- 
                            Activate this, and the relative button on codeBehind to show delete button near the name                            
                            <asp:ImageButton ID="btnIDFriendsListDelete" CssClass="ButtonFriendsList" ToolTip="Delete Friendship" ImageUrl="~/Images/removeFriendship.png" runat="server" ClientIDMode="Static" CommandArgument='<%# Eval("Friend") %>' OnClick="btnRemoveFriendship_Click" OnClientClick="return JCOnfirm(this, 'Remove', 'Sicuro', 'SI', 'NO');" />
        --%>
        <%--  </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <p>
                    </p>
                </p>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
        <%--BLOCKED FRIENDS - DISABLED--%>
        <%--<asp:UpdatePanel ID="upnlBlockedFriendsList" ClientIDMode="Static" runat="server"
            UpdateMode="Conditional">
            <ContentTemplate>
                <p>
                    <asp:Label ID="lblBlockedFriendsList" CssClass="sectionTitle" runat="server" Text="Blocked Friends"></asp:Label>
                </p>
                <p>
                    &nbsp;
                    <asp:Repeater ID="rptBlockedFriendsList" runat="server">
                        <ItemTemplate>
                            <div class="ContainerFriendPic">
                                <asp:ImageButton ID="btnIDBlockedFriendsListUserProfile" CssClass="imgFollow" ToolTip="Show Profile"
                                    OnDataBinding="btnIDFriendsList_DataBinding" ImageUrl="~/Images/showFriendProfile.png"
                                    runat="server" CommandArgument='<%# Eval("Friend") %>' OnClick="btnShowFriendProfile_Click" />
                                <br />
                                <asp:Label ID="lblBlockFriendsList" runat="server" Text='<%# Eval("Friend") %>' OnDataBinding="WriteNameOfUser_DataBinding"></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </p>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </asp:Panel>
</asp:Content>
