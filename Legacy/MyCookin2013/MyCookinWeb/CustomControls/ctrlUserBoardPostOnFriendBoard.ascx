<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlUserBoardPostOnFriendBoard.ascx.cs"
    Inherits="MyCookinWeb.CustomControls.ctrlUserBoardPostOnFriendBoard" %>
<asp:Panel ID="pnlUserBoardPostOnFriendBoard" runat="server" 
    meta:resourcekey="pnlUserBoardPostOnFriendBoardResource1">

    <script type="text/javascript">

        var PostFriendPlaceHolderLang = {
            '2': 'Scrivi qualcosa',
            '3': 'Escriba algo',
            '1': 'Write something',
            '4': 'Ecrire quelque chose',
            '5': 'schreiben Sie etwas'
        };

        function GetPostOnFriendCorrectPlaceHolder(idLanguage) {
            PostFriendPlaceHolder = PostFriendPlaceHolderLang[idLanguage];
            if (PostFriendPlaceHolder == "") {
                PostFriendPlaceHolder = PostFriendPlaceHolderLang['1'];
            }
            return PostFriendPlaceHolder;
        }

        $(document).ready(function () {
            $("#<%=txtStatus.ClientID%>").attr("placeholder", GetPostOnFriendCorrectPlaceHolder('<%=hfIDLanguage.Value %>'));
        });

    </script>

    <asp:HiddenField ID="hfIDLanguage" runat="server" />

    <asp:HiddenField ID="hfIDUserFriend" runat="server" />
    <asp:Label ID="lblStatus" runat="server" meta:resourcekey="lblStatusResource1"></asp:Label>
    <asp:TextBox ID="txtStatus" CssClass="AutoSizeTextArea" runat="server" 
        TextMode="MultiLine" PlaceHolder=""
        Width="764px" Rows="1" meta:resourcekey="txtStatusResource1"></asp:TextBox>
    <asp:ImageButton ID="ibtnPostStatus" runat="server" 
        ImageUrl="/Images/icon-forward.png" OnClick="ibtnPostStatus_Click" 
        Height="34px" Width="34px" meta:resourcekey="ibtnPostStatusResource1"/>
</asp:Panel>
