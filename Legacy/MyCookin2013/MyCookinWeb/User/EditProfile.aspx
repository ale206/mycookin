<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master"
    AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="MyCookinWeb.User.ProfileProperties" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register TagPrefix="MyCtrlCity" TagName="AutoComplete" Src="~/CustomControls/AutoComplete.ascx" %>
<%@ Register TagName="EditImage" TagPrefix="MyCtrl" Src="~/CustomControls/ctrlEditImage.ascx" %>
<%@ Register TagName="multiUp" TagPrefix="MyCtrl" Src="~/CustomControls/ctrlMultiUpload.ascx" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/jQueryUiCss/UserControl/ctrlEditImage.css" />
    <link href="/Styles/jQueryUiCss/JCrop/jquery.Jcrop.css" rel="stylesheet" type="text/css" />
    <%--Adult DatePicker--%>
    <link href="/Styles/Bootstraps/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/Bootstraps/datepicker.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <%--DatePicker. English Language is the default--%>
    <script src="/Js/BootstrapDatePicker/bootstrap-datepicker.js" type="text/javascript"></script>
    <script src="/Js/BootstrapDatePicker/locales/bootstrap-datepicker.it.js" type="text/javascript"></script>
    <script src="/Js/BootstrapDatePicker/locales/bootstrap-datepicker.es.js" type="text/javascript"></script>
    <script src="/Js/BootstrapDatePicker/locales/bootstrap-datepicker.de.js" type="text/javascript"></script>
    <script src="/Js/BootstrapDatePicker/locales/bootstrap-datepicker.fr.js" type="text/javascript"></script>
    <script src="/Js/BootstrapDatePicker/jsDate.js" type="text/javascript"></script>
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Js/JCrop/jquery.color.js" />
            <asp:ScriptReference Path="~/Js/JCrop/jquery.Jcrop.min.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <!-- Panel used to show lbResult by JQuery Box Dialog -->
    <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server" 
        meta:resourcekey="pnlResultResource1">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server" 
            meta:resourcekey="lblResultResource1"></asp:Label>
        <!-- Validation Summary -->
        <asp:ValidationSummary ID="vsEditProfile" runat="server" ClientIDMode="Static"
            ValidationGroup="EditProfileValidator" 
            meta:resourcekey="vsEditProfileResource1" />
        <asp:ValidationSummary ID="vsChangePassword" runat="server" 
            ClientIDMode="Static" ValidationGroup="ChangePasswordValidator" 
            meta:resourcekey="vsChangePasswordResource1" />
    </asp:Panel>
    <!-- To show Panel As JQueryUITabs use this Panel with ID="pnlMainTab" and <ul> for Tabs Menu -->
    <asp:Panel ID="pnlMainTab" runat="server" ClientIDMode="Static" 
        meta:resourcekey="pnlMainTabResource1">
        <!-- Tabs Menu -->
        <ul>
            <li>
                <asp:HyperLink ID="hlMyAccount" Text="MyAccount" NavigateUrl="#upnMyAccount" 
                    runat="server" meta:resourcekey="hlMyAccountResource1"></asp:HyperLink>
            </li>
            <li>
                <asp:HyperLink ID="hlPersonalInfo" Text="Personal Info" NavigateUrl="#upnPersonalInfo"
                    runat="server" meta:resourcekey="hlPersonalInfoResource1"></asp:HyperLink>
            </li>
            <li>
                <asp:HyperLink ID="hlNotificationSettings" runat="server" 
                    meta:resourcekey="hlNotificationSettingsResource1" 
                    NavigateUrl="#upnNotificationSettings" Text="Notifiche"></asp:HyperLink>
            </li>
            <li>
                <asp:HyperLink ID="hlChangePassword" runat="server" 
                    meta:resourcekey="hlChangePasswordResource1" NavigateUrl="#upnChangePassword" 
                    Text="Change Password"></asp:HyperLink>
            </li>
            <li>
                <asp:HyperLink ID="hlSocialIntegrations" runat="server" 
                    meta:resourcekey="hlSocialIntegrationsResource1" 
                    NavigateUrl="#upnSocialIntegrations" Text="Social Integrations"></asp:HyperLink>
            </li>
        </ul>
        <!--
        ****************************************
        Dynamic Panel
        ****************************************
        PersonalInfo
        ****************************************
        ****************************************
        -->
        <asp:UpdatePanel ID="upnPersonalInfo" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
            <ContentTemplate>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--
        ****************************************
        Dynamic Panel
        ****************************************
        OtherInformations
        ****************************************
        ****************************************
        -->
        <%--        <asp:UpdatePanel ID="upnOtherInformations" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
            <ContentTemplate>
            </ContentTemplate>
        </asp:UpdatePanel>
        --%>
        <asp:UpdatePanel ID="upnSocialIntegrations" runat="server" 
            ClientIDMode="Static" UpdateMode="Conditional">
            <ContentTemplate>
                <p>
                    &nbsp;</p>
                <asp:Panel ID="pnlSocialConnect" runat="server" ClientIDMode="Static" 
                    meta:resourcekey="pnlSocialConnectResource1">
                    <asp:Label ID="lblSocialConnect" runat="server" 
                        meta:resourcekey="lblSocialConnectResource1" Text="Connect Your Social!"></asp:Label>
                    <br />
                    <br />
                    <!-- Facebook Login -->
                    <asp:Panel ID="facebook_login" runat="server" ClientIDMode="Static" 
                        meta:resourcekey="facebook_loginResource1">
                        <asp:ImageButton ID="btnFacebook" runat="server" CausesValidation="False" 
                            ImageUrl="~/Images/login-facebook-button.png" 
                            meta:resourcekey="btnFacebookResource1" OnClick="btnFacebook_Click" />
                        <br />
                        <br />
                    </asp:Panel>
                    <!-- End Facebook Login -->
                    <!-- Google Login -->
                    <asp:Panel ID="google_login" runat="server" ClientIDMode="Static" 
                        meta:resourcekey="google_loginResource1">
                        <asp:ImageButton ID="btnGoogle" runat="server" CausesValidation="False" 
                            ImageUrl="/Images/google-signin.png" meta:resourcekey="btnGoogleResource1" 
                            PostBackUrl="/auth/auth.aspx?googleauth=true" />
                        <br />
                        <br />
                    </asp:Panel>
                    <!-- Twitter Login -->
                    <asp:Panel ID="twitter_login" runat="server" ClientIDMode="Static" 
                        meta:resourcekey="twitter_loginResource1">
                        <asp:ImageButton ID="btnTwitter" runat="server" CausesValidation="False" 
                            ImageUrl="/Images/twitter_signin.png" meta:resourcekey="btnTwitterResource1" 
                            PostBackUrl="/auth/auth.aspx?twitterauth=true" />
                        <br />
                        <br />
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--
        ****************************************
        Dynamic Panel
        ****************************************
        SocialIntegrations
        ****************************************
        ****************************************
        -->
        <asp:UpdatePanel ID="upnMyAccount" runat="server" ClientIDMode="Static"
            UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlPicture" runat="server" CssClass="UserInfoFirstColumn" 
                    meta:resourcekey="pnlPictureResource1">
                    <asp:UpdatePanel ID="upnUserPhoto" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="lblPhoto" runat="server" CssClass="UserInfoFieldTitle" 
                                meta:resourcekey="lblPhotoResource1" Text="Photo"></asp:Label>
                            <MyCtrl:EditImage ID="imgProfile" runat="server" />
                            <MyCtrl:multiUp ID="multiup" runat="server" OnFilesUploaded="FileUploaded" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Label ID="lblGeneralUploadError" runat="server" CssClass="lblError" 
                        meta:resourcekey="lblGeneralUploadErrorResource1" Text="Foto troppo grande" 
                        Visible="False" Width="150px"></asp:Label>
                </asp:Panel>
                <p>
                    <asp:Label ID="lblName" runat="server" CssClass="UserInfoFieldTitle" 
                        meta:resourcekey="lblNameResource1" Text="Name"></asp:Label>
                    <asp:TextBox ID="txtName" runat="server" meta:resourcekey="txtNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" 
                        ControlToValidate="txtName" Display="None" ErrorMessage="* Write your Name" 
                        meta:resourcekey="rfvNameResource1" ValidationGroup="EditProfileValidator"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revSpecialCharsName" runat="server" 
                        ControlToValidate="txtName" Display="None" 
                        ErrorMessage="Do Not Write Special Chars in Name" 
                        meta:resourcekey="revSpecialCharsNameResource1" 
                        ValidationExpression="^[\wÀ-Öäëïöüâêîôûáàéèíìóòúù.' -]*$"></asp:RegularExpressionValidator>
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:Label ID="lblSurname" runat="server" CssClass="UserInfoFieldTitle" 
                        meta:resourcekey="lblSurnameResource1" Text="Surname"></asp:Label>
                    <asp:TextBox ID="txtSurname" runat="server" 
                        meta:resourcekey="txtSurnameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSurname" runat="server" 
                        ControlToValidate="txtSurname" Display="None" 
                        ErrorMessage="* Write your Surname" meta:resourcekey="rfvSurnameResource1" 
                        ValidationGroup="EditProfileValidator"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revSpecialCharsSurname" runat="server" 
                        ControlToValidate="txtSurname" Display="None" 
                        ErrorMessage="Do Not Write Special Chars in Surname" 
                        meta:resourcekey="revSpecialCharsSurnameResource1" 
                        ValidationExpression="^[\wÀ-Öäëïöüâêîôûáàéèíìóòúù.' -]*$"></asp:RegularExpressionValidator>
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:Label ID="lblUserName" runat="server" CssClass="UserInfoFieldTitle" 
                        meta:resourcekey="lblUserNameResource1" Text="Username"></asp:Label>
                    <asp:TextBox ID="txtUserName" runat="server" ClientIDMode="Static" 
                        MaxLength="25" meta:resourcekey="txtUserNameResource1"></asp:TextBox>
                    <span ID="status"></span>
                    <!-- Call WebService to verify Username Existence -->
                    <script type="text/javascript">

                            //<![CDATA[
                        $(document).ready(function () {
                            $("#").keyup(function () {
                                var uname = $("#");
                                var currentUsername = $("#");
                                var msgbox = $("#status");
                                if (uname.val().length > 0) {
                                    $.ajax({
                                        type: "GET",
                                        url: "http://" + WebServicesPath + "/User/CheckUser.asmx/CheckUsername?username=" + uname.val() + "&currentUsername=" + currentUsername.val(),
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
                                                $('#txtUserName').removeClass("myTextBoxInvalid").addClass("myTextBoxValid");
                                                $('#lblUsernameNotAvailable').text("");
                                            }
                                            else {
                                                $('#txtUserName').removeClass("myTextBoxValid").addClass("myTextBoxInvalid");
                                                $('#lblUsernameNotAvailable').text(" * Username Not Available");
                                            }
                                        },
                                        error: function (result) {
                                            console.log(result.status + ' ' + result.statusText);
                                        }
                                    });
                                }
                            });
                        });
                            //]]>
                    </script>
                    <asp:CompareValidator ID="cvCheckUsername" runat="server" 
                        ControlToValidate="txtCheckUsername" Display="None" 
                        ErrorMessage="Username not available" 
                        meta:resourcekey="cvCheckUsernameResource1" 
                        ValidationGroup="EditProfileValidator" ValueToCompare="available"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
                        ControlToValidate="txtUserName" Display="Dynamic" 
                        ErrorMessage="Write your Username" meta:resourcekey="rfvUsernameResource1" 
                        ValidationGroup="EditProfileValidator">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revSpecialcharsUsername" runat="server" 
                        ControlToValidate="txtUsername" Display="None" 
                        ErrorMessage="Do Not Write Special Chars in Username" 
                        meta:resourcekey="revSpecialcharsUsernameResource1" 
                        ValidationExpression="^[a-zA-Z0-9_.-]*$"></asp:RegularExpressionValidator>
                    <asp:Label ID="lblUsernameNotAvailable" runat="server" ClientIDMode="Static" 
                        meta:resourcekey="lblUsernameNotAvailableResource1"></asp:Label>
                </p>
                <p>
                    &nbsp;
                </p>
                <p>
                    <asp:Label ID="lblEmpty" runat="server" CssClass="UserInfoFieldTitle" 
                        meta:resourcekey="lblEmptyResource1" Text="Change Security Question And Answer"></asp:Label>
                    <asp:ImageButton ID="btnChangeAnswer" runat="server" 
                        AlternateText="Change Security Question and Answer" CausesValidation="False" 
                        meta:resourcekey="btnChangeAnswerResource1" OnClick="btnChangeAnswer_Click" />
                </p>
                <p>
                    &nbsp;</p>
                <asp:Panel ID="pnlSecurityQuestionAndAnswer" runat="server" 
                    meta:resourcekey="pnlSecurityQuestionAndAnswerResource1">
                    <p>
                        &nbsp;</p>
                    <p>
                        <asp:Label ID="lblSecurityQuestion" runat="server" 
                            CssClass="UserInfoFieldTitle" meta:resourcekey="lblSecurityQuestionResource1" 
                            Text="Question"></asp:Label>
                        <asp:DropDownList ID="ddlSecurityQuestion" runat="server" 
                            meta:resourcekey="ddlSecurityQuestionResource1">
                        </asp:DropDownList>
                    </p>
                    <p>
                        &nbsp;</p>
                    <p>
                        <asp:Label ID="lblSecurityAnswer" runat="server" CssClass="UserInfoFieldTitle" 
                            meta:resourcekey="lblSecurityAnswerResource1" Text="Answer"></asp:Label>
                        <asp:TextBox ID="txtSecurityAnswer" runat="server" 
                            meta:resourcekey="txtSecurityAnswerResource1" TextMode="Password"></asp:TextBox>
                    </p>
                    <p>
                        &nbsp;</p>
                </asp:Panel>
                <p>
                    <asp:Label ID="lblMobile" runat="server" CssClass="UserInfoFieldTitle" 
                        meta:resourcekey="lblMobileResource1" Text="Mobile"></asp:Label>
                    <asp:TextBox ID="txtMobile" runat="server" 
                        meta:resourcekey="txtMobileResource1"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revMobile" runat="server" 
                        ControlToValidate="txtUsername" Display="None" 
                        ErrorMessage="Only Write Numbers in Mobile" 
                        meta:resourcekey="revMobileResource1" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:Label ID="lblBirthDate" runat="server" CssClass="UserInfoFieldTitle" 
                        meta:resourcekey="lblBirthDateResource1" Text="Birth Date"></asp:Label>
                    <asp:TextBox ID="txtBirthDate" runat="server" ClientIDMode="Static" 
                        meta:resourcekey="txtBirthDateResource1" onkeydown="return noWrite(event)" 
                        onkeypress="return noWrite(event)" onPaste="return noWrite(event)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvBirthDate" runat="server" 
                        ControlToValidate="txtBirthDate" Display="None" 
                        ErrorMessage="* Write your BirthDate" meta:resourcekey="rfvBirthDateResource1" 
                        ValidationGroup="EditProfileValidator"></asp:RequiredFieldValidator>
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:Label ID="lblGender" runat="server" CssClass="UserInfoFieldTitle" 
                        meta:resourcekey="lblGenderResource1" Text="Gender"></asp:Label>
                    <asp:DropDownList ID="ddlGender" runat="server" 
                        meta:resourcekey="ddlGenderResource1">
                    </asp:DropDownList>
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:Label ID="lblLanguage" runat="server" CssClass="UserInfoFieldTitle" 
                        meta:resourcekey="lblLanguageResource1" Text="Language"></asp:Label>
                    <asp:DropDownList ID="ddlLanguage" runat="server" 
                        meta:resourcekey="ddlLanguageResource1">
                    </asp:DropDownList>
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:Label ID="lblCity" runat="server" CssClass="UserInfoFieldTitle" 
                        meta:resourcekey="lblCityResource1" Text="City"></asp:Label>
                    <MyCtrlCity:AutoComplete ID="cacCity" runat="server" />
                    <p>
                        &nbsp;</p>
                    <!-- Button -->
                    <asp:Button ID="btnMyAccountUpdate" runat="server" CssClass="MyButton" 
                        meta:resourcekey="btnMyAccountUpdateResource1" 
                        OnClick="btnMyAccountUpdate_Click" Text="Salva" 
                        ValidationGroup="EditProfileValidator" />
                    <div style="visibility: hidden; height: 0px;">
                        <asp:TextBox ID="txtCityID" runat="server" ClientIDMode="Static" 
                            meta:resourcekey="txtCityIDResource1"></asp:TextBox>
                        <asp:TextBox ID="txtCurrentUsername" runat="server" ClientIDMode="Static" 
                            meta:resourcekey="txtCurrentUsernameResource1"></asp:TextBox>
                    </div>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                </p>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--
        ****************************************
        Static Panel
        ****************************************
        Complete Personal UserInfo of USER TABLE
        ****************************************
        ****************************************
        -->
        <!--
        ****************************************
        Static Panel - DISABLED FOR NOW
        ****************************************
        Become Cook
        ****************************************
        ****************************************
    -->
        <asp:UpdatePanel ID="upnNotificationSettings" runat="server" 
            ClientIDMode="Static" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlNotifications" runat="server" ClientIDMode="Static" 
                    meta:resourcekey="pnlNotificationsResource1">
                </asp:Panel>
                <asp:Button ID="btnUpdateNotificationSetting" runat="server" 
                    CssClass="MyButton" meta:resourcekey="btnUpdateNotificationSettingResource1" 
                    OnClick="btnUpdateNotificationSetting_Click" Text="Salva" 
                    ValidationGroup="UpdateNotificationSettingValidation" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--<asp:UpdatePanel ID="upnBecomeCook" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlBecomeCookDescription" runat="server" ClientIDMode="Static">
                    <p>
                        <asp:Label ID="Label1" runat="server" Text="Became a Cooker now! Just click Here and Enjoy"
                            CssClass="UserInfoFieldTitle"></asp:Label>
                        <br />
                        <asp:ImageButton ID="btnBecomeCook" CssClass="btnBecomeCook" runat="server" OnClick="btnBecomeCook_Click"
                            ImageUrl="~/Images/iconSave.png" />
                    </p>
                </asp:Panel>
                <asp:Panel ID="pnlCookInformations" runat="server" ClientIDMode="Static">
                    <p>
                        <asp:Label ID="lblProfessionalCook" runat="server" Text="Are you a Professional Cook?"
                            CssClass="UserInfoFieldTitle"></asp:Label>
                        <asp:CheckBox ID="chkProfessionalCook" runat="server" />
                    </p>
                    <p>
                        <asp:Label ID="lblCookInRestaurant" runat="server" Text="Do you cook in a Restaurant?"
                            CssClass="UserInfoFieldTitle"></asp:Label>
                        <asp:CheckBox ID="chkCookInRestaurant" runat="server" />
                    </p>
                    <p>
                        <asp:Label ID="lblCookAtHome" runat="server" Text="Do you cook at home?" CssClass="UserInfoFieldTitle"></asp:Label>
                        <asp:CheckBox ID="chkCookAtHome" runat="server" />
                    </p>
                    <p>
                        <asp:TextBox ID="txtCookDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </p>
                    <p>
                        <asp:ImageButton ID="btnUpdateCookerInformations" CssClass="btnUpdateCookerInformations"
                            runat="server" OnClick="btnUpdateCookerInformations_Click" ImageUrl="~/Images/iconSave.png" />
                    </p>
                    <p>
                        <asp:Label ID="lblNoMoreCook" runat="server" Text="Are you not more a cooker? Click to change it."
                            CssClass="UserInfoFieldTitle"></asp:Label>
                        <br />
                        <asp:ImageButton ID="btnNoMoreCook" CssClass="btnNoMoreCook" runat="server" OnClick="btnNoMoreCook_Click"
                            ImageUrl="~/Images/iconSave.png" />
                    </p>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
        <asp:UpdatePanel ID="upnChangePassword" runat="server" ClientIDMode="Static"
            UpdateMode="Conditional">
            <ContentTemplate>
                <p>
                    <asp:Label ID="lblChangePswText" runat="server" 
                        meta:resourcekey="lblChangePswTextResource1" 
                        Text="After changing your password you will redirect to login page"></asp:Label>
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:Label ID="lblOldPassword" runat="server" CssClass="UserInfoFieldTitle" 
                        meta:resourcekey="lblOldPasswordResource1" Text="Old Password"></asp:Label>
                    <asp:TextBox ID="txtOldPassword" runat="server" 
                        meta:resourcekey="txtOldPasswordResource1" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" 
                        ControlToValidate="txtOldPassword" Display="None" 
                        ErrorMessage="* Insert Old Password" meta:resourcekey="rfvOldPasswordResource1" 
                        ValidationGroup="ChangePasswordValidator"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="lblPassword1" runat="server" CssClass="UserInfoFieldTitle" 
                        meta:resourcekey="lblPassword1Resource1" Text="New Password"></asp:Label>
                    <asp:TextBox ID="txtPassword1" runat="server" 
                        meta:resourcekey="txtPassword1Resource1" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword1" runat="server" 
                        ControlToValidate="txtPassword1" Display="None" 
                        ErrorMessage="* Insert New Password" meta:resourcekey="rfvPassword1Resource1" 
                        ValidationGroup="ChangePasswordValidator"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="lblPassword2" runat="server" CssClass="UserInfoFieldTitle" 
                        meta:resourcekey="lblPassword2Resource1" Text="Confirm New Password"></asp:Label>
                    <asp:TextBox ID="txtPassword2" runat="server" ClientIDMode="Static" 
                        MaxLength="50" meta:resourcekey="txtPassword2Resource1" TextMode="Password"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revMinLengthPsw" runat="server" 
                        ControlToValidate="txtPassword2" 
                        ErrorMessage="Inserisci almeno 6 caratteri per la Password." 
                        meta:resourcekey="revMinLengthPswResource1" ValidationExpression="^.{6,25}$">*</asp:RegularExpressionValidator>
                    <!-- Call WebService to verify psw -->
                    <asp:RequiredFieldValidator ID="rfvPassword2" runat="server" 
                        ControlToValidate="txtPassword2" Display="None" 
                        ErrorMessage="* Insert Again New Password" 
                        meta:resourcekey="rfvPassword2Resource1" 
                        ValidationGroup="ChangePasswordValidator"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvPassword1Password2" runat="server" 
                        ControlToCompare="txtPassword1" ControlToValidate="txtPassword2" Display="None" 
                        ErrorMessage="* Passwords must be the same" 
                        meta:resourcekey="cvPassword1Password2Resource1" 
                        ValidationGroup="ChangePasswordValidator"></asp:CompareValidator>
                </p>
                <div style="visibility: hidden; height: 0;">
                    <asp:TextBox ID="txtPasswordStrenght" runat="server" ClientIDMode="Static" 
                        meta:resourcekey="txtPasswordStrenghtResource1"></asp:TextBox>
                </div>
                <!-- Button -->
                <asp:Button ID="btnChangePassword" CssClass="MyButton" runat="server"
                    Text="Salva" OnClick="btnChangePassword_Click" 
                    ValidationGroup="ChangePasswordValidator" 
                    meta:resourcekey="btnChangePasswordResource1" />
                <p>
                    &nbsp;</p>
                <p>
                    <hr />
                    Delete Account
                    <br />
                    <asp:Button ID="btnDeleteAccount" runat="server" CausesValidation="False" 
                        ClientIDMode="Static" CssClass="MyButton" 
                        meta:resourcekey="btnDeleteAccountResource1" OnClick="btnDeleteAccount_Click" 
                        OnClientClick="return JCOnfirm(this, 'Remove', 'Sicuro', 'SI', 'NO');" 
                        Text="Cancella" />
                </p>
                <div style="visibility: hidden; height: 0;">
                    <asp:TextBox ID="txtCheckUsername" runat="server" ClientIDMode="Static" 
                        meta:resourcekey="txtCheckUsernameResource1"></asp:TextBox>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
