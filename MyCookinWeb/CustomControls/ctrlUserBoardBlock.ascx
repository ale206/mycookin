<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlUserBoardBlock.ascx.cs"
    Inherits="MyCookinWeb.CustomControls.ctrlUserBoardBlock" %>
<%@ Register TagPrefix="MyCtrl" TagName="UserBoardLikesControl" Src="~/CustomControls/ctrlUserBoardLikes.ascx" %>
<%@ Register TagPrefix="MyCtrl" TagName="UserBoardCommentsControl" Src="~/CustomControls/ctrlUserBoardComments.ascx" %>
<%--Javascript for Scrollbar--%>
<script type="text/javascript" src="/Js/Scrollbar/jquery.mCustomScrollbar.concat.min.js"></script>
<asp:Panel ID="pnlUserBoardBlock" runat="server" CssClass="pnlUserBoardBlock" 
    meta:resourcekey="pnlUserBoardBlockResource1">
   
   <script type="text/javascript">

       var CommentsPlaceHolderLang = {
           '2': 'Scrivi un commento qui',
           '3': 'Escriba un comentario aquí',
           '1': 'Write a comment here',
           '4': 'Écrire un commentaire ici',
           '5': 'Schreiben Sie einen Kommentar'
       };

       function GetCommentsCorrectPlaceHolder(idLanguage) {

           CommentsPlaceHolder = CommentsPlaceHolderLang[idLanguage];
           if (CommentsPlaceHolder == "") {
               CommentsPlaceHolder = CommentsPlaceHolderLang['1'];
           }
           return CommentsPlaceHolder;
       }

       $(document).ready(function () {
           $("#<%=txtNewComment.ClientID%>").attr("placeholder", GetCommentsCorrectPlaceHolder('<%=hfIDLanguage.Value %>'));
       });

    </script>

    <asp:HiddenField ID="hfIDLanguage" runat="server" />

   <asp:Panel ID="pnlUserInfo" CssClass="pnlUserInfo" runat="server" 
        meta:resourcekey="pnlUserInfoResource1">
        <div style="float:left; width:800px;">
        <asp:ImageButton ID="btnImgUser" runat="server" CssClass="imgUser" Width="60px" 
                Height="60px" ImageUrl="/Images/icon-user-color-Orange.png" />
        <asp:HyperLink ID="hlUser" runat="server" CssClass="lnkHlUser" 
                meta:resourcekey="hlUserResource1"></asp:HyperLink>
        <asp:Panel ID="pnlDateAndDelete" runat="server" CssClass="pnlDateAndDelete" 
                meta:resourcekey="pnlDateAndDeleteResource1">
            <asp:Label ID="lblDatePublish" runat="server" CssClass="lblDatePublish" 
                meta:resourcekey="lblDatePublishResource1"></asp:Label>
            <asp:ImageButton ID="ibtnDelete" CommandArgument='<%# Eval("IDUserAction") %>' 
                OnClick="ibtnDelete_Click" ToolTip="Elimina questo post"
                OnClientClick="return JCOnfirm(this, 'Remove', 'Sicuro', 'SI', 'NO');" ImageUrl="~/Images/deleteX.png"
                runat="server" Width="16px" Height="16px" CssClass="ibtnDelete" 
                meta:resourcekey="ibtnDeleteResource1" />
        </asp:Panel>
        <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Width="710px" 
                meta:resourcekey="lblMessageResource1"></asp:Label>
        <asp:HiddenField ID="hfIDUserAction" runat="server" />
        </div>   
        <div style="float:right; margin-right:40px;">
        <asp:Panel ID="pnlShare" CssClass="pnlShare" runat="server" Visible="True" 
                meta:resourcekey="pnlShareResource1">    
            <asp:Image ID="imgSharingLoader" 
                    AlternateText="Loading" ImageUrl="~/Images/icon_loader.gif" runat="server" 
                    meta:resourcekey="imgSenderListLoaderResource1" />  
            <asp:ImageButton ID="ibtnShareFacebook" CommandArgument='<%# Eval("IDUserAction") %>'
            OnClick="ibtnShareFacebook_Click" OnClientClick="$('#imgSharingLoader').show();" ImageUrl="~/Images/fb.png"  Width="20px" 
                Height="20px" runat="server" ToolTip="Condividi su Facebook" 
                meta:resourcekey="ibtnShareFacebookResource1" />
            <asp:ImageButton ID="ibtnShareTwitter" 
                CommandArgument='<%# Eval("IDUserAction") %>'  ToolTip="Condividi su Twitter"
            OnClick="ibtnShareTwitter_Click" OnClientClick="$('#imgSharingLoader').show();" ImageUrl="~/Images/tw.png"  Width="20px" 
                Height="20px" runat="server" meta:resourcekey="ibtnShareTwitterResource1" />
            <asp:ImageButton ID="ibtnShareUserBoard" CommandArgument='<%# Eval("IDUserAction") %>'
            OnClick="ibtnShareUserBoard_Click" OnClientClick="$('#imgSharingLoader').show();" ImageUrl="~/Images/icon-cooking.png"  
                Width="20px" Height="20px" runat="server" ToolTip="Condividi sulla tua bacheca" 
                meta:resourcekey="ibtnShareUserBoardResource1" />
            
        </asp:Panel>

        </div>
    </asp:Panel>
    <asp:Panel ID="pnlImageAndDescription" CssClass="pnlImageAndDescription" 
        runat="server" meta:resourcekey="pnlImageAndDescriptionResource1">
        
        <div style="display: inline-block;">
            <div style="float: left;">
                <asp:ImageButton ID="btnImgRelatedObject" runat="server"  CssClass="RelatedObjectImage" 
                Height="200px" Width="200px"/>
                <asp:Image ID="imgGraph" runat="server" ImageUrl="/Images/graffetta.png" 
                    Width="20px" Height="55px" CssClass="imgGraffetta" 
                    meta:resourcekey="imgGraphResource1" />
            </div>
            <div id="pnlObjDesc" class="pnlObjDesc">
                <asp:HyperLink ID="hlRelatedObject" runat="server" CssClass="hlRelatedObject" 
                    meta:resourcekey="hlRelatedObjectResource1"></asp:HyperLink><br />
                <br />
                <asp:Label ID="lblDescriptionObject" runat="server" 
                    CssClass="lblDescriptionObject" 
                    meta:resourcekey="lblDescriptionObjectResource1"></asp:Label>
            </div>
        </div>
    </asp:Panel>

      <asp:Panel ID="pnlLeft" CssClass="pnlLeft" runat="server" 
        meta:resourcekey="pnlLeftResource1">

        
    </asp:Panel>
    <asp:Panel ID="pnlRight" CssClass="pnlRight pnlCommentsAndLike" runat="server" 
        meta:resourcekey="pnlRightResource1">
        <asp:Panel ID="pnlLikes" runat="server" CssClass="pnlLikes" 
            meta:resourcekey="pnlLikesResource1">
            <asp:UpdatePanel ID="upnLikes" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <MyCtrl:UserBoardLikesControl ID="ctrlUserBoardBlockLikes" runat="server" OnLikeChanged="LikeChangedEvent" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ctrlUserBoardBlockLikes" EventName="LikeChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Panel ID="pnlCountComments" CssClass="pnlCountComments" runat="server" 
            meta:resourcekey="pnlCountCommentsResource1">
            <asp:Label ID="lblCountComments" runat="server" 
                meta:resourcekey="lblCountCommentsResource1"></asp:Label>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlCommentContainer" CssClass="pnlComments" 
        meta:resourcekey="pnlCommentContainerResource1">
        <asp:Panel runat="server" ID="pnlComments" 
            meta:resourcekey="pnlCommentsResource1">
            <asp:UpdatePanel ID="uPnlComments" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Repeater ID="rptComments" runat="server">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="uPnlCommentSing" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <MyCtrl:UserBoardCommentsControl ID="ctrlUserBoardComments" OnCommentDeleted="UpdateCommentCount"
                                        IDUserActionFather='<%# Eval("IDUserAction") %>' IDUser='<%# Eval("IDUser") %>'
                                        UserActionDate='<%# Eval("UserActionDate") %>' UserActionMessage='<%# Eval("UserActionMessage") %>'
                                        IDUserAction='<%# Eval("IDUserAction") %>' runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rptComments" EventName="DataBinding" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnlAddComment" runat="server" CssClass="pnlAddComment" 
        meta:resourcekey="pnlAddCommentResource1">
        <asp:Label ID="lblWriteComment" runat="server" 
            meta:resourcekey="lblWriteCommentResource1"></asp:Label>
        <asp:TextBox ID="txtNewComment" CssClass="AutoSizeTextArea" runat="server" TextMode="MultiLine"
            Rows="1" Width="450px" PlaceHolder="" 
            meta:resourcekey="txtNewCommentResource1"></asp:TextBox>
        <asp:ImageButton ID="ibtnComment" runat="server" OnClick="ibtnComment_Click" 
            ImageUrl="~/Images/icon-forward.png" Width="34px" Height="34px" 
            meta:resourcekey="ibtnCommentResource1" />
        <br />
    </asp:Panel>
    <asp:HiddenField ID="hfTypeOfLike" runat="server" />
    <asp:HiddenField ID="hfControlBoardLoaded" Value="false" runat="server" />
</asp:Panel>
