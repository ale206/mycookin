<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PleaseUpdateYourBrowser.aspx.cs"
    Inherits="MyCookinWeb.PleaseUpdateYourBrowser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MyCookin - Please update your browser</title>
    <link rel="Stylesheet" href="/Styles/SiteStyle/style.css" />
</head>
<body>
    <img src="/Images/Backgrounds/vino2.jpg" alt="Background Image" id="backgroundImage" />
    <form id="form1" runat="server">
    <asp:Panel ID="pnlErrorMessage" ClientIDMode="Static" runat="server">
        <p>
            <asp:HyperLink ID="lnkLogo" runat="server" ImageUrl="/Images/MyCookinLogo-105x105.png"
                NavigateUrl="/Default.aspx"></asp:HyperLink></p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;We are sorry, this browser is not supported <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ci dispiace, questo browser non è supportato <br />
            &nbsp;&nbsp;&nbsp;&nbsp;Lo sentimos, este navegador no es compatible <br />
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;To use MyCookin please update your browser or install one of the list below <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Per utilizzare MyCookin aggiorna il tuo browser o installane uno della lista sottostante <br />
            &nbsp;&nbsp;&nbsp;&nbsp;Para utilizar MyCookin por favor actualiza tu navegador o instala uno de la lista abajo<br />
            
        </p>
        <p>
            &nbsp;</p>
        <p>
           <a href="http://www.google.it/chrome">Download Google Chrome</a> <br />
        </p>
            <p>
           <a href="http://www.mozilla.org/en-US/firefox/all/">Download Mozilla Firefox</a> <br />
        </p>
    </asp:Panel>
    </form>

    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-39281629-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>
</body>
</html>
