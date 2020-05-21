<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master"
    AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="MyCookinWeb.Message.Messages" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register TagPrefix="MyCtrl" TagName="AutoComplete" Src="~/CustomControls/AutoComplete.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/PageStyle/Messages.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <script type="text/javascript" src="../Js/Pages/Messages.js"></script>
    <script type="text/javascript" src="../Js/Messages/MessagesSetAsRead.js"></script>
    <script type="text/javascript" src="../Js/DateFormat.js"></script>
    <%--Offset--%>
    <asp:HiddenField ID="hfOffset" ClientIDMode="Static" runat="server" />
    <script type="text/javascript" language="javascript">
        try {
            var d = new Date();
            document.getElementById("hfOffset").value = d.getTimezoneOffset();
        }
        catch (err) {
        }
    </script>
    <script type="text/javascript" src="/Js/Scrollbar/jquery.mCustomScrollbar.concat.min.js"></script>
    <asp:HiddenField ID="hfIDUser" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfIDConversation" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfPageSize" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfDateFormat" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfCurrentPagingOffset" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfNumberOfMessages" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfPagingOffset" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfNumberOfPages" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfRecipientID" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfName" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfSurname" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfNewMessage" ClientIDMode="Static" runat="server" />
    <%--<div id="Container">
    --%>
    <asp:Panel ID="Container" ClientIDMode="Static" runat="server" 
        meta:resourcekey="ContainerResource1">
        
        <asp:Panel ID="InformationContainer" ClientIDMode="Static" runat="server" 
            meta:resourcekey="InformationContainerResource1">
            <asp:HyperLink ID="hlWriteNewMessage" ToolTip="Write new message" 
                runat="server" CssClass="hlWriteNewMessage"
                NavigateUrl="#" meta:resourcekey="hlWriteNewMessageResource1"></asp:HyperLink>
        </asp:Panel>

        <asp:Panel ID="ContainerLeft" ClientIDMode="Static" runat="server" 
            meta:resourcekey="ContainerLeftResource1">
            <asp:Panel ID="ContainerSenderList" ClientIDMode="Static" runat="server" 
                meta:resourcekey="ContainerSenderListResource1">
               <asp:Panel ID="SenderList" ClientIDMode="Static" runat="server" 
                    meta:resourcekey="SenderListResource2">
                   <asp:Image ID="imgSenderListLoader" ClientIDMode="Static" 
                       AlternateText="Loading" ImageUrl="~/Images/icon_loader.gif" runat="server" 
                       meta:resourcekey="imgSenderListLoaderResource1" />
                    <ul>
                    </ul>
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>


        <asp:Panel ID="ContainerRight" ClientIDMode="Static" runat="server" 
            meta:resourcekey="ContainerRightResource1">
        <asp:Image ID="imgMessageContainerLoader" ClientIDMode="Static" 
                AlternateText="Loading" ImageUrl="~/Images/icon_loader.gif" runat="server" 
                meta:resourcekey="imgMessageContainerLoaderResource1" />
        
            <asp:Panel ID="MessageContainer" ClientIDMode="Static" runat="server" 
                meta:resourcekey="MessageContainerResource1">          
            </asp:Panel>
            
            <asp:Panel ID="ReplyPanel" ClientIDMode="Static" runat="server" 
                meta:resourcekey="ReplyPanelResource1">
                <asp:TextBox ID="txtMessage" Width="655" CssClass="ReplyTextBox AutoSizeTextArea" ClientIDMode="Static" runat="server" TextMode="MultiLine"
                    onkeypress="return clickButton(event,'ibtnSendReplyMessage')" 
                    meta:resourcekey="txtMessageResource1"></asp:TextBox>
                
                <asp:ImageButton ID="ibtnSendReplyMessage" ClientIDMode="Static" runat="server" Text="Invia"
                    
                    OnClientClick="$('#imgSendReplyLoader').show(); SendReplyMessage(); return false;" 
                    ImageUrl="~/Images/icon-back.png" Width="30" Height="30" ToolTip="Invia" 
                    meta:resourcekey="ibtnSendReplyMessageResource1" />
                
                <asp:Image ID="imgSendReplyLoader" ClientIDMode="Static" 
                    AlternateText="Loading" ImageUrl="~/Images/icon_loader.gif" runat="server" 
                    meta:resourcekey="imgSendReplyLoaderResource1" />

                <asp:Label ID="lblMandatoryReplyField" ClientIDMode="Static" runat="server"
                    meta:resourcekey="lblMandatoryReplyFieldResource1"></asp:Label>

            </asp:Panel>
            
            <asp:Panel ID="pnlNewMessage" ClientIDMode="Static" runat="server" 
                ToolTip="Nuovo Messaggio" meta:resourcekey="pnlNewMessageResource1">
                <asp:Label ID="lblForAutocomplete" runat="server" Text="A chi vuoi scrivere?" 
                    meta:resourcekey="lblForAutocompleteResource1"></asp:Label><br /><br />
                <MyCtrl:AutoComplete ID="acRecipient" runat="server" />

                <br />

                <asp:TextBox ID="txtNewMessage" CssClass="AutoSizeTextArea" runat="server" 
                    TextMode="MultiLine" PlaceHolder="Scrivi qualcosa"
        Width="400px" Rows="1" ClientIDMode="Static" 
                    onkeypress="return clickButton(event,'btnSendNewMessage')" 
                    meta:resourcekey="txtNewMessageResource1"></asp:TextBox>
                
                <br /><br />
                <div style="text-align:right;">

                <asp:Label ID="lblMandatoryField" ClientIDMode="Static" runat="server"
                    meta:resourcekey="lblMandatoryFieldResource1"></asp:Label>

                
                    <asp:Button ID="btnSendNewMessage" CssClass="MyButton" ClientIDMode="Static" 
                        runat="server" Text="Invia"
                        OnClientClick="$('#imgSendNewMessageLoader').show(); SendNewMessage();" 
                        meta:resourcekey="btnSendNewMessageResource1" />
                    <asp:Image ID="imgSendNewMessageLoader" ClientIDMode="Static" 
                        AlternateText="Loading" ImageUrl="~/Images/icon_loader.gif" runat="server" 
                        meta:resourcekey="imgSendNewMessageLoaderResource1" />
                </div>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>

</asp:Content>
