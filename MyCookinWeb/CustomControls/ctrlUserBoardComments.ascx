<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlUserBoardComments.ascx.cs" Inherits="MyCookinWeb.CustomControls.ctrlUserBoardComments" %>

<%@ Register TagPrefix = "MyCtrl" TagName="UserBoardLikesControl" Src="~/CustomControls/ctrlUserBoardLikes.ascx" %>

<asp:Panel ID="pnlComments" runat="server" CssClass="pnlComments" 
    meta:resourcekey="pnlCommentsResource1">

    <asp:HiddenField ID="hfIDUserActionFather" runat="server" />

    <asp:HiddenField ID="hfIDUser" runat="server" />
    <asp:HiddenField ID="hfUserActionDate" runat="server" />
    <asp:HiddenField ID="hfUserActionMessage" runat="server" />
    <asp:HiddenField ID="hfIDUserAction" runat="server" />
            <div style="display:block; margin-top:5px;">
            <div class="commentSeparator"></div>
            <asp:HyperLink ID="hlCommentOwner" runat="server" 
                    OnDataBinding="hlCommentOwner_OnDataBinding" 
                    meta:resourcekey="hlCommentOwnerResource1"></asp:HyperLink>
            <asp:Label ID="lblDate" runat="server"  OnDataBinding="lblDate_OnDataBinding" 
                    CssClass="lblDatePublish" meta:resourcekey="lblDateResource1"></asp:Label> <br /> 
            <asp:HyperLink ID="hlUser" runat="server" CssClass="lnkHlUser" 
                    meta:resourcekey="hlUserResource1"></asp:HyperLink>: 
            <asp:Label ID="lblComment" runat="server" meta:resourcekey="lblCommentResource1" ></asp:Label> 
            <asp:ImageButton ID="ibtnDelete" OnClick="ibtnDelete_Click" 
                    OnClientClick="return JCOnfirm(this, 'Remove', 'Sicuro', 'SI', 'NO');" 
                    ImageUrl="~/Images/deleteX.png" runat="server" Width="16px" Height="16px" 
                    meta:resourcekey="ibtnDeleteResource1" />

            </div>

      <div style="display:block;">
            <asp:UpdatePanel ID="upnLikes" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <MyCtrl:UserBoardLikesControl ID="ctrlUserBoardLikes" TypeOfLike="3" 
                   OnLikeChanged="LikeChangedEvent" runat="server" />
            </ContentTemplate>
            <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ctrlUserBoardLikes" EventName="LikeChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>


    <asp:HiddenField ID="hfControlCommentsLoaded" Value="false" runat="server" />

</asp:Panel>
