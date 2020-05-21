<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master"
    AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="MyCookinWeb.UserInfo.ForgotPassword"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <script type="text/javascript" src="../Js/Pages/ForgotPassword.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).tooltip();
        });
    </script>
    <script type="text/javascript">
        //Hide scroll bars
        HideScrollbars();
        //===
    </script>
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
    <!-- Panel used to show lbResult by JQuery Box Dialog -->
    <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server" meta:resourcekey="pnlResultResource1">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server" meta:resourcekey="lblResultResource1"></asp:Label>
    </asp:Panel>
    <div id="content">
        <div id="boxForgotPswBackGround">
        </div>
        <div id="boxForgotPsw">
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
            <asp:Panel ID="pnlForgotPswForm1" ClientIDMode="Static" runat="server" meta:resourcekey="pnlForgotPswForm1Resource1">
                <p>
                    <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" MaxLength="150" Placeholder="Email"
                        CssClass="tipsyTooltip Padding8" onkeypress="return clickButton(event,'lbtnForgotPsw1')"
                        meta:resourcekey="txtEmailResource1"></asp:TextBox>
                    <asp:Label ID="lblMandatoryEmail" ClientIDMode="Static" runat="server" ToolTip="Inserisci la tua email"
                        Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblMandatoryEmailResource1"></asp:Label>
                    <asp:Label ID="lblCorrectEmail" ClientIDMode="Static" runat="server" ToolTip="Controlla la tua email"
                        Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblCorrectEmailResource1"></asp:Label>
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:Label ID="lblWrongEmail" runat="server" Visible="False" CssClass="ErrorMessage"
                        meta:resourcekey="lblWrongEmailResource1"></asp:Label>&nbsp;</p>
                <div id="boxForgotPsw1Button" class="ForgotPswButton">
                    <asp:LinkButton ID="lbtnForgotPsw1" runat="server" ClientIDMode="Static" OnClick="lbtnForgotPsw_Click"
                        meta:resourcekey="lbtnForgotPsw1Resource1">
                    </asp:LinkButton>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlForgotPswForm2" ClientIDMode="Static" runat="server" meta:resourcekey="pnlForgotPswForm2Resource1">
                <p>
                    <asp:Label ID="lblSecurityQuestion" CssClass="fieldtitle" runat="server" meta:resourcekey="lblSecurityQuestionResource1"></asp:Label>
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:TextBox ID="txtSecurityAnswer" ClientIDMode="Static" runat="server" MaxLength="150"
                        CssClass="tipsyTooltip" TextMode="Password" onkeypress="return clickButton(event,'lbtnForgotPsw2')"
                        meta:resourcekey="txtSecurityAnswerResource1"></asp:TextBox>
                    <asp:Label ID="lblMandatorySecurityAnswer" ClientIDMode="Static" runat="server" ToolTip="Inserisci la tua risposta segreta"
                        Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblMandatorySecurityAnswerResource1"></asp:Label>
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:Label ID="lblWrongAnswer" runat="server" Visible="False" CssClass="ErrorMessage"
                        meta:resourcekey="lblWrongAnswerResource1"></asp:Label>&nbsp;</p>
                <div id="boxForgotPsw2Button" class="ForgotPswButton">
                    <asp:LinkButton ID="lbtnForgotPsw2" ClientIDMode="Static" runat="server" OnClick="lbtnForgotPsw2_Click"
                        meta:resourcekey="lbtnForgotPsw2Resource1">
                    </asp:LinkButton>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
