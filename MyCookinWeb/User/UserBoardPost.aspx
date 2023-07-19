<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master"
    AutoEventWireup="true" CodeBehind="UserBoardPost.aspx.cs" Inherits="MyCookinWeb.User.UserBoardPost" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register TagPrefix="MyCtrl" TagName="UserBoardBlockControl" Src="~/CustomControls/ctrlUserBoardBlock.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/UserBoard.min.css" />
    <link rel="Stylesheet" href="/Styles/PageStyle/UserBoardBlock.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <!-- Panel used to show lbResult by JQuery Box Dialog -->
    <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server" 
        meta:resourcekey="pnlResultResource1">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server" 
            meta:resourcekey="lblResultResource1"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlWrapperMainContent" ClientIDMode="Static" runat="server" 
        meta:resourcekey="pnlWrapperMainContentResource1">
        <asp:Panel ID="pnlMainContent" ClientIDMode="Static" runat="server" 
            meta:resourcekey="pnlMainContentResource1">
            <asp:Panel ID="firstColumn" ClientIDMode="Static" runat="server" 
                meta:resourcekey="firstColumnResource1">
            </asp:Panel>
            <asp:Panel ID="pnlUserBoard" ClientIDMode="Static" runat="server" 
                CssClass="pnlUserBoard" meta:resourcekey="pnlUserBoardResource1">
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <asp:UpdatePanel ID="upnlUserBoard" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Repeater ID="rptUserBoardControl" runat="server">
                            <ItemTemplate>
                                <asp:UpdatePanel ID="upnlUserBoardBlock" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <MyCtrl:UserBoardBlockControl ID="ctrlUserBoardBlock" runat="server" TypeOfLike='<%# Eval("TypeOfLike") %>'
                                            IDUserAction='<%# Eval("IDUserAction") %>' />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="secondColumn" ClientIDMode="Static" runat="server" 
            meta:resourcekey="secondColumnResource1">
            <asp:Panel ID="pnlProfileComplete" runat="server" 
                meta:resourcekey="pnlProfileCompleteResource1">
                <asp:Label ID="lblProfileCompleteTitle" runat="server" Visible="False" 
                    meta:resourcekey="lblProfileCompleteTitleResource1"></asp:Label><br />
                <asp:Label ID="lblProfileComplete" runat="server" Visible="False" 
                    meta:resourcekey="lblProfileCompleteResource1"></asp:Label>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
