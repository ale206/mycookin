<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master" AutoEventWireup="true" CodeBehind="mycooking.aspx.cs" Inherits="MyCookinWeb.mycooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/MyCooking.css" />
    <meta name="title" content="MyCooking"/>
    <meta name="description" content="MyCooking"/>
    <meta name="keywords" content="MyCooking"/>
    <meta name="author" content="MyCookin"/>
    <meta name="copyright" content="MyCookin"/>
    <meta http-equiv="Reply-to" content="alessio@mycookin.com;saverio@mycookin.com"/>
    <meta http-equiv="Content-Type" content="text/html; iso-8859-1"/>
    <meta name="ROBOTS" content="INDEX,FOLLOW"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <asp:Panel ID="pnlMyCooking" CssClass="pnlMyCooking" ToolTip="MyCooking"
        ClientIDMode="Static" runat="server">
        <h1>MyCooking</h1>
        <br />
        <table style="width: 100%;">
            
            <tr>
                <td>
                    <img src="Images/flag-ita.png" width="15" alt="ita" /><br />
                    <h2>Stavi cercando <a href="/Default.aspx">MyCookin</a>?</h2>
                    <br />

                    Abbiamo scelto questo nome, senza la G, perchè ci piaceva l'idea dell'apostrofo alla fine e volevamo creare qualcosa di unico anche nel nostro nome.
                      <br />
                    <br />
                    <a href="/Default.aspx">Clicca qui</a> per collegarti al sito di <a href="/Default.aspx">MyCookin.com</a>, scoprire nuove ricette e conoscere persone dai tuoi stessi gusti.
                    E' gratis! </td>

            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <img src="Images/flag-uk.png" width="15" alt="uk" /><br />
                    <h2>Are you looking for <a href="/Default.aspx">MyCookin</a>? </h2>
                    <br />

                    We chosed this name, without the G, because we liked the idea of the apostrophe at the end and we would create something unique in our name as well.
                      <br />
                    <br />
                    <a href="/Default.aspx">Click here</a> to go to <a href="/Default.aspx">MyCookin.com</a> website, discover new recipes and meet new people with your same tastes.
                    It is Free! </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <img src="Images/flag-es.png" width="15" alt="es" /><br />
                    <h2>¿Estabas buscando <a href="/Default.aspx">MyCookin</a>? </h2>
                    <br />

                    Elegimos este nombre, sin la G, porque nos gustaba la idea del apóstrofe al final y queríamos crear algo único también en nuestro nombre.
                      <br />
                    <br />
                    <a href="/Default.aspx">Haga clic aquí</a> para conectarse al sitio de <a href="/Default.aspx">MyCookin.com</a>, descubrir nuevas recetas e información sobre la gente de tus mismos gustos.
                    Es gratis! </td>
            </tr>
        </table>

    </asp:Panel>
</asp:Content>
