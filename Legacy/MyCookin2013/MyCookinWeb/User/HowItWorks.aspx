<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master" AutoEventWireup="true" CodeBehind="HowItWorks.aspx.cs" Inherits="MyCookinWeb.User.HowItWorks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/HowItWorks.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            //Start Tooltip
            $(document).tooltip();
        });
    </script>

    <asp:UpdatePanel ID="upnlHowItWorks" CssClass="pnlHowItWorks" ClientIDMode="Static" UpdateMode="Always" runat="server">
        <ContentTemplate>
           
            <p class="titleRow">
                <asp:Label ID="lblTitle" CssClass="lblTitle" runat="server" Text=""></asp:Label>
            </p>

            <asp:Panel ID="pnlMenu" runat="server" ClientIDMode="Static">
                <asp:ImageButton ID="imgbVoteRecipeMenu" runat="server" OnClick="imgbVoteRecipeMenu_Click" />
                <asp:ImageButton ID="imgbFollowPeopleMenu" runat="server" OnClick="imgbFollowPeopleMenu_Click" />
                <asp:ImageButton ID="imgbSearchEngineMenu" runat="server" OnClick="imgbSearchEngineMenu_Click" />
                <asp:ImageButton ID="imgbCommentRecipeMenu" runat="server" OnClick="imgbCommentRecipeMenu_Click" />
                <asp:ImageButton ID="imgbWriteOnBulletinBoardMenu" runat="server" OnClick="imgbWriteOnBulletinBoardMenu_Click" />
                <asp:ImageButton ID="imgbLoginFbGoogleMenu" runat="server" OnClick="imgbLoginFbGoogleMenu_Click" />
                <asp:ImageButton ID="imgbShareRecipeMenu" runat="server" OnClick="imgbShareRecipeMenu_Click" />
                <asp:ImageButton ID="imgbFoodBloggerMenu" runat="server" OnClick="imgbFoodBloggerMenu_Click" />
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlVoteRecipe" ClientIDMode="Static" CssClass="content" Visible="false">
                <h1>
                    <asp:Label ID="lblVoteRecipeTitle" runat="server" Text=""></asp:Label></h1>
                <br />
                <asp:Image ID="imgVoteRecipe" runat="server" />
            
                <asp:Label ID="lblVoteRecipe" runat="server" Text=""></asp:Label>

            </asp:Panel>

            <asp:Panel runat="server" ID="pnlFollowPeople" ClientIDMode="Static" CssClass="content" Visible="false">
                <h1>
                    <asp:Label ID="lblFollowPeopleTitle" runat="server" Text=""></asp:Label></h1>
                <br />
                <asp:Image ID="imgFollowPeople" runat="server" />
            
                <asp:Label ID="lblFollowPeople" runat="server" Text=""></asp:Label>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlSearchEngine" ClientIDMode="Static" CssClass="content" Visible="false">
                <h1>
                    <asp:Label ID="lblSearchEngineTitle" runat="server" Text=""></asp:Label></h1>
                <br />
                <asp:Image ID="imgSearchEngine" runat="server" />
            
                <asp:Label ID="lblSearchEngine" runat="server" Text=""></asp:Label>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlCommentRecipe" ClientIDMode="Static" CssClass="content" Visible="false">
                <h1>
                    <asp:Label ID="lblCommentRecipeTitle" runat="server" Text=""></asp:Label></h1>
                <br />
                <asp:Image ID="imgCommentRecipe" runat="server" />
            
                <asp:Label ID="lblCommentRecipe" runat="server" Text=""></asp:Label>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlWriteOnBulletinBoard" ClientIDMode="Static" CssClass="content" Visible="false">
                <h1>
                    <asp:Label ID="lblWriteOnBulletinBoardTitle" runat="server" Text=""></asp:Label></h1>
                <br />
                <asp:Image ID="imgWriteOnBulletinBoard" runat="server" />
            
                <asp:Label ID="lblWriteOnBulletinBoard" runat="server" Text=""></asp:Label>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlLoginFbGoogle" ClientIDMode="Static" CssClass="content" Visible="false">
                <h1>
                    <asp:Label ID="lblLoginFbGoogleTitle" runat="server" Text=""></asp:Label></h1>
                <br />
                <asp:Image ID="imgLoginFbGoogle" runat="server" />
            
                <asp:Label ID="lblLoginFbGoogle" runat="server" Text=""></asp:Label>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlShareRecipe" ClientIDMode="Static" CssClass="content" Visible="false">
                <h1>
                    <asp:Label ID="lblShareRecipeTitle" runat="server" Text=""></asp:Label></h1>
                <br />
                <asp:Image ID="imgShareRecipe" runat="server" />
            
                <asp:Label ID="lblShareRecipe" runat="server" Text=""></asp:Label>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlFoodBlogger" ClientIDMode="Static" CssClass="content" Visible="false">
                <h1>
                    <asp:Label ID="lblFoodBloggerTitle" runat="server" Text=""></asp:Label></h1>
                <br />
                <asp:Image ID="imgFoodBlogger" runat="server" />
            
                <asp:Label ID="lblFoodBlogger" runat="server" Text=""></asp:Label>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>