<%@ Page Title="Error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="M4_Project.Error" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" type="text/css" href="/Content/login.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <div>
        <section>
            <asp:PlaceHolder runat="server" ID="changePasswordHolder">
                <div class="form-horizontal">
                    <h4>Error Occurred</h4>
                    <hr />
                    <br />
                    <p>An error occurred while processing your request.</p>
                    <p>Please contact us and provide the details that led to the error, and we will conduct an investigation.</p>
                    <br /><br />
                    <a href="/Contact" class="a_tag_btn" style="background-color: var(--secondary-primary)">Contact</a>
                    <br /><br />
                </div>
            </asp:PlaceHolder>

        </section>
    </div>
</asp:Content>