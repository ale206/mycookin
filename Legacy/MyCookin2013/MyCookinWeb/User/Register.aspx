<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master"
    AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MyCookinWeb.UserInfo.Register"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/Bootstraps/bootstrap.css" rel="stylesheet" type="text/css" />
    <%--Adult DatePicker--%>
    <link href="/Styles/Bootstraps/datepicker.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="/Js/Pages/Register.js" />
            <asp:ScriptReference Path="/Js/BootstrapDatePicker/bootstrap-datepicker.js" />
            <asp:ScriptReference Path="/Js/BootstrapDatePicker/locales/bootstrap-datepicker.it.js" />
            <asp:ScriptReference Path="/Js/BootstrapDatePicker/locales/bootstrap-datepicker.es.js" />
            <asp:ScriptReference Path="/Js/BootstrapDatePicker/locales/bootstrap-datepicker.de.js" />
            <asp:ScriptReference Path="/Js/BootstrapDatePicker/locales/bootstrap-datepicker.fr.js" />
            <asp:ScriptReference Path="/Js/BootstrapDatePicker/jsDate.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <script type="text/javascript">
        $(document).ready(function () {
            //Start Tooltip
            $(document).tooltip();

            $("#<%=txtName.ClientID%>").attr("placeholder", GetNameCorrectPlaceHolder('<%=hfIDLanguage.Value %>'));
            $("#<%=txtSurname.ClientID%>").attr("placeholder", GetSurnameCorrectPlaceHolder('<%=hfIDLanguage.Value %>'));
            $("#<%=txtBirthdate.ClientID%>").attr("placeholder", GetBirthdateCorrectPlaceHolder('<%=hfIDLanguage.Value %>'));

        });     
    </script>
    <script type="text/javascript">
        //Hide scroll bars
        HideScrollbars();
        //===
    </script>
    <script language="javascript" type="text/javascript">
        function ShowLoader() {
            document.getElementById('divLoader').style.display = '';
        }
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

    <asp:HiddenField ID="hfIDLanguage" runat="server" />


    <asp:UpdatePanel ID="upnMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- Panel used to show lbResult by JQuery Box Dialog -->
            <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server" meta:resourcekey="pnlResultResource1">
                <asp:Label ID="lblResult" ClientIDMode="Static" runat="server" meta:resourcekey="lblResultResource1"></asp:Label>
            </asp:Panel>
            <div id="content">
                <div id="boxRegisterBackGround">
                </div>
                <div id="boxRegister">
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
                        <asp:TextBox ID="txtName" ClientIDMode="Static" runat="server" MaxLength="50" Placeholder=""
                            CssClass="tipsyTooltip Padding8" autocomplete="off" onkeypress="return clickButton(event,'lbtnRegister')"
                            meta:resourcekey="txtNameResource1"></asp:TextBox>
                        <asp:Label ID="lblMandatoryName" ClientIDMode="Static" runat="server" ToolTip="Inserisci il tuo nome"
                            Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblMandatoryNameResource1"></asp:Label>
                        <asp:Label ID="lblValidName" ClientIDMode="Static" runat="server" ToolTip="Non inserire caratteri speciali nel tuo nome"
                            Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblValidNameResource1"></asp:Label>
                    </p>
                    <p>
                        <asp:TextBox ID="txtSurname" ClientIDMode="Static" runat="server" MaxLength="50"
                            Placeholder="" CssClass="tipsyTooltip Padding8" autocomplete="off" onkeypress="return clickButton(event,'lbtnRegister')"
                            meta:resourcekey="txtSurnameResource1"></asp:TextBox>
                        <asp:Label ID="lblMandatorySurname" ClientIDMode="Static" runat="server" ToolTip="Inserisci il tuo cognome"
                            Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblMandatorySurnameResource1"></asp:Label>
                        <asp:Label ID="lblValidSurname" ClientIDMode="Static" runat="server" ToolTip="Non inserire caratteri speciali nel tuo cognome"
                            Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblValidSurnameResource1"></asp:Label>
                    </p>
                    <p>
                        <asp:TextBox ID="txtBirthdate" onkeypress="return noWrite(event)" onkeydown="return noWrite(event)"
                            onPaste="return noWrite(event)" runat="server" Placeholder=""
                            ClientIDMode="Static" CssClass="tipsyTooltip Padding8" autocomplete="off" onfocus="$('#lblMandatoryBirthdate').tipsy('hide');"
                            meta:resourcekey="txtBirthdateResource1"></asp:TextBox>
                        <asp:Label ID="lblMandatoryBirthdate" ClientIDMode="Static" runat="server" ToolTip="Qual è la tua data di nascita?"
                            Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblMandatoryBirthdateResource1"></asp:Label>
                        <asp:Label ID="lblValidBirthdate" ClientIDMode="Static" runat="server" ToolTip="Controlla la tua data di nascita"
                            Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblValidBirthdateResource1"></asp:Label>
                    </p>
                    <p>
                        <asp:TextBox ID="txtUsername" ClientIDMode="Static" runat="server" MaxLength="25"
                            Placeholder="Username" CssClass="tipsyTooltip Padding8" autocomplete="off" onkeypress="return clickButton(event,'lbtnRegister')"
                            meta:resourcekey="txtUsernameResource1"></asp:TextBox>
                        <asp:Label ID="lblMandatoryUsername" ClientIDMode="Static" runat="server" ToolTip="Scegli un username"
                            Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblMandatoryUsernameResource1"></asp:Label>
                        <asp:Label ID="lblValidUsername" ClientIDMode="Static" runat="server" ToolTip="Non inserire caratteri speciali nel tuo username"
                            Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblValidUsernameResource1"></asp:Label>
                        <asp:Label ID="lblExistenceUsername" ClientIDMode="Static" runat="server" ToolTip="Username già esistente. Scegline un altro."
                            Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblExistenceUsernameResource1"></asp:Label>
                        <span id="status"></span>
                        <!-- Call WebService to verify Username Existence -->
                        <script type="text/javascript">
                            //<![CDATA[
                            $(document).ready(function () {
                                $("#<%=txtUsername.ClientID%>").keyup(function () {
                                    if (isValidUsername()) {
                                        var uname = $("#<%=txtUsername.ClientID%>");
                                        var msgbox = $("#status");
                                        if (uname.val().length > 0) {
                                            $.ajax({
                                                type: "GET",
                                                url: "http://" + WebServicesPath + "/User/CheckUser.asmx/CheckUsername?username=" + uname.val(),
                                                crossDomain: true,
                                                //data: "{Message:" + "\"" + Message + "\"" + "}",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                //async: false,
                                                success: function (result) {
                                                    //alert("ciao " + result.d);
                                                    $('#txtCheckUsername').val(result.d);

                                                    //Change Color of TextBox if available or not
                                                    if (result.d == "available") {
                                                        $('#txtUsername').removeClass("myTextBoxInvalid").addClass("myTextBoxValid");

                                                        $('#lblExistenceUsername').hide();
                                                        $('#lblExistenceUsername').tipsy('hide');
                                                    }
                                                    else {
                                                        $('#txtUsername').removeClass("myTextBoxValid").addClass("myTextBoxInvalid");

                                                        $('#lblExistenceUsername').show();
                                                        $('#lblExistenceUsername').tipsy('show');

                                                    }
                                                },
                                                error: function (result) {
                                                    console.log(result.status + ' ' + result.statusText);
                                                }
                                            });
                                        }
                                    }
                                });
                            });
                            //]]>
                        </script>
                    </p>
                    <p>
                        <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" MaxLength="70" Placeholder="Email"
                            CssClass="tipsyTooltip Padding8" autocomplete="off" onkeypress="return clickButton(event,'lbtnRegister')"
                            meta:resourcekey="txtEmailResource1"></asp:TextBox>
                        <asp:Label ID="lblMandatoryEmail" ClientIDMode="Static" runat="server" ToolTip="Inserisci la tua email"
                            Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblMandatoryEmailResource1"></asp:Label>
                        <asp:Label ID="lblValidEmail" ClientIDMode="Static" runat="server" ToolTip="Controlla la tua email"
                            Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblValidEmailResource1"></asp:Label>
                        <asp:Label ID="lblExistenceEmail" ClientIDMode="Static" runat="server" ToolTip="Questa email risulta già registrata"
                            Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblExistenceEmailResource1"></asp:Label>
                        <span id="statusEmail"></span>
                        <!-- Call WebService to verify Username Existence -->
                        <script type="text/javascript">
                            //<![CDATA[
                            $(document).ready(function () {
                                $("#<%=txtEmail.ClientID%>").keyup(function () {
                                    if (isValidEmail()) {
                                        var email = $("#<%=txtEmail.ClientID%>");
                                        var msgbox = $("#status");
                                        if (email.val().length > 5) {
                                            $.ajax({
                                                type: "GET",
                                                url: "http://" + WebServicesPath + "/User/CheckUser.asmx/CheckEmail?email=" + email.val(),
                                                crossDomain: true,
                                                //data: "{Message:" + "\"" + Message + "\"" + "}",
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                //async: false,
                                                success: function (result) {
                                                    //alert("ciao " + result.d);
                                                    $('#txtCheckEmail').val(result.d);

                                                    //Change Color of TextBox if available or not
                                                    if (result.d == "available") {
                                                        $('#txtEmail').removeClass("myTextBoxInvalid").addClass("myTextBoxValid");

                                                        $('#lblExistenceEmail').tipsy('hide');
                                                        $('#lblExistenceEmail').hide();

                                                    }
                                                    else {
                                                        $('#txtEmail').removeClass("myTextBoxValid").addClass("myTextBoxInvalid");

                                                        $('#lblExistenceEmail').show();
                                                        $('#lblExistenceEmail').tipsy('show');

                                                    }
                                                },
                                                error: function (result) {
                                                    console.log(result.status + ' ' + result.statusText);

                                                }
                                            });
                                        }
                                    }
                                });
                            });
                            //]]>
                        </script>
                    </p>
                    <p>
                        <asp:TextBox ID="txtPassword" ClientIDMode="Static" runat="server" TextMode="Password"
                            MaxLength="25" Placeholder="Password" CssClass="tipsyTooltip Padding8" autocomplete="off"
                            onkeypress="return clickButton(event,'lbtnRegister')" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                        <asp:Label ID="lblMandatoryPassword" ClientIDMode="Static" runat="server" ToolTip="Inserisci una password"
                            Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblMandatoryPasswordResource1"></asp:Label>
                        <asp:Label ID="lblValidPassword" ClientIDMode="Static" runat="server" ToolTip="Scegli una password di almeno 6 caratteri"
                            Text="*" CssClass="tipsyTooltipError" meta:resourcekey="lblValidPasswordResource1"></asp:Label>
                        <!-- Call WebService to verify Username Existence -->
                    </p>
                    <div id="boxRegisterButton">
                        <asp:Image ID="imgRegisterListLoader" ClientIDMode="Static" AlternateText="Loading"
                            ImageUrl="~/Images/icon_loader.gif" runat="server" meta:resourcekey="imgRegisterListLoaderResource1" />
                        <asp:LinkButton ID="lbtnRegister" ClientIDMode="Static" runat="server" OnClick="lbtnRegister_Click"
                            meta:resourcekey="lbtnRegisterResource1">
                        </asp:LinkButton>
                    </div>
                    <p class="conditions">
                        <asp:Label ID="lblNote" runat="server" Text="Cliccando su Registrati, accetti le nostre"
                            meta:resourcekey="lblNoteResource1"></asp:Label>
                        <asp:HyperLink ID="hlConditions" NavigateUrl="/User/TermsAndConditions.aspx" Text="Condizioni"
                            runat="server" meta:resourcekey="hlConditionsResource1"></asp:HyperLink>
                    </p>
                    <div id="divLoader" class="ButtonRow" style="display: none;">
                        <img src="/Images/Loader/ajax-loader_blu01.gif" width="32" height="32" alt="Loading data..." />
                    </div>
                    <p>
                        <asp:Label ID="lblUsernameNotAvailable" ClientIDMode="Static" runat="server" meta:resourcekey="lblUsernameNotAvailableResource1"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="lblMailNotAvailable" ClientIDMode="Static" runat="server" meta:resourcekey="lblMailNotAvailableResource1"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="lblPasswordStrenght" ClientIDMode="Static" runat="server" meta:resourcekey="lblPasswordStrenghtResource1"></asp:Label>
                    </p>
                    <div style="visibility: hidden; height: 0;">
                        <asp:TextBox ID="txtCheckUsername" ClientIDMode="Static" runat="server" meta:resourcekey="txtCheckUsernameResource1"></asp:TextBox>
                        <asp:TextBox ID="txtCheckEmail" ClientIDMode="Static" runat="server" meta:resourcekey="txtCheckEmailResource1"></asp:TextBox>
                        <asp:TextBox ID="txtPasswordStrenght" ClientIDMode="Static" runat="server" meta:resourcekey="txtPasswordStrenghtResource1"></asp:TextBox>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
