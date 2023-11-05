<%@ Page Title="Not Found" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NotFound.aspx.cs" Inherits="M4_Project.NotFound" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/login.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <div>
        <section>
            <asp:PlaceHolder runat="server" ID="notFoundHolder">
                <div class="form-horizontal">
                    <h4>Resource Not Found (404)</h4>
                    <hr />
                    <br />
                    <p>The requested resource could not be found on the server.</p>
                    <p>Please verify the URL or navigate to our <a href="/" style="color: teal">home page</a>.</p>
                    <br /><br />
                    <a href="/Contact" class="a_tag_btn" style="background-color: var(--secondary-primary)">Contact</a>
                    <br /><br />
                </div>
            </asp:PlaceHolder>
        </section>
    </div>
</asp:Content>
