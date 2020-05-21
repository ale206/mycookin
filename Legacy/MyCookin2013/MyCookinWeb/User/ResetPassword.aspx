<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master"
    AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="MyCookinWeb.UserInfo.ResetPassword"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <script type="text/javascript" src="../Js/Pages/ResetPassword.js"></script>
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
        <div id="boxResetPswBackGround">
        </div>
        <div id="boxResetPsw">
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
                <asp:TextBox ID="txtPassword" ClientIDMode="Static" runat="server" TextMode="Password"
                    MaxLength="25" Placeholder="Nuova Password" CssClass="tipsyTooltip" autocomplete="off"
                    onkeypress="return clickButton(event,'lbtnResetPsw')" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                <asp:Label ID="lblMandatoryPassword" ClientIDMode="Static" runat="server" ToolTip="Inserisci una password"
                    Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblMandatoryPasswordResource1"></asp:Label>
                <asp:Label ID="lblValidPassword" ClientIDMode="Static" runat="server" ToolTip="Scegli una password di almeno 6 caratteri"
                    Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblValidPasswordResource1"></asp:Label>
                <!-- Call WebService to verify Username Existence -->
                <%--<script type="text/javascript">
                            //<![CDATA[
                        $(document).ready(function () {
                            $("#<%=txtPassword.ClientID%>").keyup(function () {
                                var password = $("#<%=txtPassword.ClientID%>");
                                var usn = $("#<%=txtUserName.ClientID%>");
                                var msgbox = $("#status");
                                if (password.val().length > 0) {
                                    $.ajax({
                                        type: "GET",
                                        url: "http://" + WebServicesPath + "/User/CheckUser.asmx/CheckPasswordStrenght?password=" + password.val() + "&usn=" + usn.val(),
                                        crossDomain: true,
                                        //data: "{Message:" + "\"" + Message + "\"" + "}",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        //async: false,
                                        success: function (result) {
                                            $('#txtPasswordStrenght').val(result.d);

                                            //Change Color of TextBox if secure or not
                                            if (result.d == "secure") {
                                                $('#txtPassword').removeClass("myTextBoxInvalid").addClass("myTextBoxValid");
                                                $('#lblPasswordStrenght').text("Your password is a good password");
                                            }
                                            else {
                                                $('#txtPassword').removeClass("myTextBoxValid").addClass("myTextBoxInvalid");

                                                $('#lblPasswordStrenght').text("La sicurezza del potere si fonda sull'insicurezza dei cittadini (Leonardo Sciascia). Devi scegliere una password migliore.");
                                            }
                                        },
                                        error: function (result) {
                                            alert(result.status + ' ' + result.statusText);
                                        }
                                    });
                                }
                            });
                        });
                            //]]>
                    </script>--%>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                    Display="Dynamic" ErrorMessage="Insert your new Password" meta:resourcekey="rfvPasswordResource1">*</asp:RequiredFieldValidator>
                <%--                    <asp:CompareValidator ID="cvPasswordStrenght" runat="server" ErrorMessage="Choose a better Password"
                        ControlToValidate="txtPasswordStrenght" Display="None" ValueToCompare="secure"></asp:CompareValidator>
                --%>
            </p>
            <p>
                &nbsp;</p>
            <p>
                <asp:Label ID="lblNote" runat="server" meta:resourcekey="lblNoteResource1"></asp:Label>
            </p>
            <p>
                &nbsp;</p>
            <p>
                <asp:Label ID="lblPasswordStrenght" ClientIDMode="Static" runat="server" meta:resourcekey="lblPasswordStrenghtResource1"></asp:Label>
            </p>
            <div id="boxResetPasswordButton" class="ResetPswButton">
                <asp:LinkButton ID="lbtnResetPsw" runat="server" ClientIDMode="Static" OnClick="lbtnResetPsw_Click"
                    meta:resourcekey="lbtnResetPswResource1">
                    
                
                </asp:LinkButton>
            </div>
            <div style="visibility: hidden; height: 0;">
                <asp:TextBox ID="txtUserName" ClientIDMode="Static" runat="server" meta:resourcekey="txtUserNameResource1"></asp:TextBox>
                <asp:TextBox ID="txtPasswordStrenght" ClientIDMode="Static" runat="server" meta:resourcekey="txtPasswordStrenghtResource1"></asp:TextBox>
            </div>
        </div>
    </div>
</asp:Content>
