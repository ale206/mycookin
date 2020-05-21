<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MyCookinWeb.UserInfo.Login"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/Bootstraps/bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <script type="text/javascript" src="../Js/Pages/Login.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //Start Tooltip
            $(document).tooltip();

            try {
                var d = new Date();
                $("#hfOffset").val(d.getTimezoneOffset());
            }
            catch (err) {
            }
        });
    </script>
    <script type="text/javascript">
        //Hide scroll bars
        HideScrollbars();
        //===
    </script>
    <%--Offset--%>
    <asp:HiddenField ID="hfOffset" ClientIDMode="Static" runat="server" />
    <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server" meta:resourcekey="pnlResultResource1">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server" meta:resourcekey="lblResultResource1"></asp:Label>
    </asp:Panel>
    <div id="content">
        <div id="boxLoginBackGround">
        </div>
        <div id="boxLogin">
            <p>
                <asp:Label ID="lblSocialLogin" runat="server" Text="Fai il login o registrati in un click con"
                    meta:resourcekey="lblSocialLoginResource1"></asp:Label>
                <asp:ImageButton ID="loginFacebook" class="loginbox" ImageUrl="/Images/fb_icon_64.png"
                    Height="28px" runat="server" CausesValidation="False" OnClick="btnFacebook_Click"
                    meta:resourcekey="loginFacebookResource1"></asp:ImageButton>
                <asp:ImageButton ID="loginGoogle" class="loginbox" ImageUrl="/Images/google_icon_64.png"
                    Height="28px" runat="server" CausesValidation="False" OnClick="btnGoogle_Click"
                    meta:resourcekey="loginGoogleResource1"></asp:ImageButton>
            </p>
            <p>
                &nbsp;</p>
            <p>
                <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" MaxLength="150" Placeholder="Email"
                    CssClass="tipsyTooltip Padding8" onkeypress="return clickButton(event,'lbtnUserLogin')"
                    meta:resourcekey="txtEmailResource1"></asp:TextBox>
                <asp:Label ID="lblMandatoryEmail" ClientIDMode="Static" runat="server" ToolTip="Inserisci la tua email"
                    Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblMandatoryEmailResource1"></asp:Label>
                <asp:Label ID="lblCorrectEmail" ClientIDMode="Static" runat="server" ToolTip="Controlla la tua email"
                    Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblCorrectEmailResource1"></asp:Label>
            </p>
            <p>
                &nbsp;</p>
            <p>
                <asp:TextBox ID="txtPsw" ClientIDMode="Static" runat="server" TextMode="Password"
                    MaxLength="50" Placeholder="Password" CssClass="tipsyTooltip Padding8" onkeypress="return clickButton(event,'lbtnUserLogin')"
                    meta:resourcekey="txtPswResource1"></asp:TextBox>
                <asp:Label ID="lblMandatoryPassword" ClientIDMode="Static" runat="server" ToolTip="Inserisci la tua password"
                    Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblMandatoryPasswordResource1"></asp:Label>
            </p>
            <p>
                &nbsp;
            </p>
            <p>
                <asp:CheckBox ID="chkRemember" runat="server" Text="Ricordami" meta:resourcekey="chkRememberResource1" />
            </p>
            <div id="boxLoginButton">
                <asp:LinkButton ID="lbtnUserLogin" ClientIDMode="Static" runat="server" OnClick="lbtnUserLogin_Click"
                    meta:resourcekey="lbtnUserLoginResource1">
                            
                        
                </asp:LinkButton>
            </div>
            <p>
                <asp:Label ID="lblWrongUsnOrPsw" runat="server" Visible="False" CssClass="ErrorMessage"
                    meta:resourcekey="lblWrongUsnOrPswResource1"></asp:Label>&nbsp;</p>
            <p>
                <asp:HyperLink ID="hlForgotPassword" Text="Dimenticato la Password?" NavigateUrl="~/User/ForgotPassword.aspx"
                    runat="server" meta:resourcekey="hlForgotPasswordResource1"></asp:HyperLink>
            </p>
        </div>
    </div>
</asp:Content>
