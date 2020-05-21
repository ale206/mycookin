<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlUserBoardLikes.ascx.cs"
    Inherits="MyCookinWeb.CustomControls.ctrlUserBoardLikes" %>
<link href="/Styles/jQueryUiCss/PopBox/popbox.css" rel="stylesheet" type="text/css" />
<%--<script type="text/javascript" language="javascript" src="/Js/PopBox/popbox.min.js"></script>--%>
<script type="text/javascript" language="javascript" src="/Js/CustomControls/ctrlUserBoardLikes.js"></script>
<asp:Panel ID="pnlLikes" runat="server" meta:resourcekey="pnlLikesResource1">
    <asp:UpdatePanel ID="upnlActionLikes" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style=" width:20px;">
                <div style="">
                    <asp:ImageButton ID="ibtnLike" CssClass="ibtnLike" runat="server" ImageUrl="~/Images/icon-like-off.png"
                        OnClick="ibtnLike_Click" Visible="False"  Width="25px" Height="25px" 
                        meta:resourcekey="ibtnLikeResource1" />
                    <asp:ImageButton ID="ibtnUnlike" CssClass="ibtnUnlike" runat="server" ImageUrl="~/Images/icon-like-on.png"
                        Visible="False" OnClick="ibtnUnlike_Click"  Width="25px" Height="25px" 
                        meta:resourcekey="ibtnUnlikeResource1" />
                </div>
                <div style="float:left; margin-top:-10px; margin-left:20px;">
                    <asp:Panel ID="pnlUserLikes" runat="server" CssClass="popbox" 
                        meta:resourcekey="pnlUserLikesResource1">
                        <asp:HyperLink ID="hlCountLikes" runat="server" 
                            CssClass="linkPopBox hlCountLikes" NavigateUrl="#" 
                            meta:resourcekey="hlCountLikesResource1"></asp:HyperLink>
                        <div class="collapse">
                            <asp:Panel ID="pnlBoxInternal" CssClass="box" runat="server" 
                                style="padding:5px;" meta:resourcekey="pnlBoxInternalResource1">
                                <asp:Panel ID="pnlContainerUserList" runat="server" 
                                    meta:resourcekey="pnlContainerUserListResource1">
                                    <asp:Image ID="imgLoader" ImageUrl="~/Images/loadingLineOrange.gif" 
                                        AlternateText="Loading" ToolTip="Loading" runat="server" Width="180px" 
                                        Height="12px" meta:resourcekey="imgLoaderResource1" />
                                </asp:Panel>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <asp:HiddenField ID="hfIDUserActionFather" runat="server" />
            <asp:HiddenField ID="hfTypeOfLike" runat="server" />
            <asp:HiddenField ID="hfControlLikesLoaded" Value="false" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
