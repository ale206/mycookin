<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyCookinWeb.Landing.es.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MyCookin</title>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />

    <link rel="stylesheet" href="../css/supersized.core.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="../css/landing.css" type="text/css" media="screen" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.0/jquery.min.js"></script>
    <script type="text/javascript" src="../js/supersized.core.3.2.1.min.js"></script>

    <script type="text/javascript">
        jQuery(function ($) {
            $.supersized({
                slides: [{ image: '../images/spuntino.jpg', title: 'MyCookin', url: 'http://www.mycookin.com/' }]
            });
        });
    </script>

    <!-- Facebook Conversion Code for LandingPageIt -->
    <script type="text/javascript">
        var fb_param = {};
        fb_param.pixel_id = '6011817273578';
        fb_param.value = '0.01';
        fb_param.currency = 'EUR';
        (function () {
            var fpw = document.createElement('script');
            fpw.async = true;
            fpw.src = '//connect.facebook.net/en_US/fp.js';
            var ref = document.getElementsByTagName('script')[0];
            ref.parentNode.insertBefore(fpw, ref);
        })();
    </script>
    <noscript><img height="1" width="1" alt="" style="display:none" src="https://www.facebook.com/offsite_event.php?id=6011817273578&amp;value=0.01&amp;currency=EUR" /></noscript>

</head>
<body>
    <form id="form1" runat="server">
        <div id="content">

            <h3>
                <a href="http://www.mycookin.com">
                    <img src="../images/MyCookinLogo.png" height="81" alt="MyCookinLogo" border="0" /></a>
            </h3>

            <!--<iframe width="550" height="315" src="//www.youtube.com/embed/ZNNugregdQk&autoplay=1" frameborder="0" autoplay="1" allowfullscreen></iframe>-->

            <table border="0" cellspacing="0" cellpadding="0" width="900">
                <tr>
                    <td>
                        <object height="350" width="425">
                            <param name="movie" value="http://www.youtube.com/v/ZNNugregdQk" />
                            <embed height="350" src="http://www.youtube.com/v/ZNNugregdQk&autoplay=1" type="application/x-shockwave-flash" width="550" /></embed>
                        </object>
                    </td>
                    <td>
                        <a href="http://www.mycookin.com/es/receta/sopa-de-calabaza-y-zanahoria/e690f707-3eae-4869-9ea8-ae1de868e397" target="_blank" border="0">
                            <img src="../images/vellutata-di-carote.jpg" width="50" style="float: left; margin: 0 10px 5px 10px;" />
                            <b>Receta del mes</b>
                            <br />
                            Sopa de calabaza y zanahoria
                        </a>

                        <script async="async" src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
                        <!-- MyCookin - Landing ES 1 -->
                        <ins class="adsbygoogle"
                            style="display: inline-block; width: 336px; height: 280px; margin: 0 0 0 10px;"
                            data-ad-client="ca-pub-7083575263085672"
                            data-ad-slot="2191136085"></ins>
                        <script>
                            (adsbygoogle = window.adsbygoogle || []).push({});
                        </script>
                    </td>
                </tr>

            </table>

            <table border="0" cellspacing="10" cellpadding="0" width="900">
                <tr>
                    <td width="250">
                        <%--<button type="button" id="btnRegister">Registrati Gratis</button>--%>
                        <asp:ImageButton ID="loginFacebook" class="loginbox" ImageUrl="../images/facebook-button-es.png"
                            Height="48px" runat="server" CausesValidation="False" OnClick="btnFacebook_Click"
                            meta:resourcekey="loginFacebookResource1"></asp:ImageButton>
                    </td>
                    <td>
                        <a href="https://twitter.com/mycookin" class="twitter-follow-button" data-show-count="true" data-lang="en">Follow @mycookin</a>
                        <script>					    !function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "http://platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs");</script>

                        <br />
                        <br />
                        <iframe src="//www.facebook.com/plugins/like.php?href=https%3A%2F%2Fwww.facebook.com%2Fmycookinespanol&amp;width&amp;layout=button_count&amp;action=like&amp;show_faces=false&amp;share=false&amp;height=21&amp;appId=476759249023668" scrolling="no" frameborder="0" style="border:none; overflow:hidden; height:21px;" allowTransparency="true"></iframe>
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <script async="async" src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
                        <!-- MyCookin - Landing Ita 2 -->
                        <ins class="adsbygoogle"
                            style="display: inline-block; width: 468px; height: 15px"
                            data-ad-client="ca-pub-7083575263085672"
                            data-ad-slot="9574802088"></ins>
                        <script>
                            (adsbygoogle = window.adsbygoogle || []).push({});
                        </script>
                    </td>
                </tr>


            </table>

        </div>

        <script>
            (function (i, s, o, g, r, a, m) {
                i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                    (i[r].q = i[r].q || []).push(arguments)
                }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
            })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

            ga('create', 'UA-39281629-1', 'mycookin.com');
            ga('send', 'pageview');

</script>
    </form>
</body>
</html>