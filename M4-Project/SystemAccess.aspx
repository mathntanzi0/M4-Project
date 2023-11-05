<%@ Page Title="Account Deactivated" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemAccess.aspx.cs" Inherits="M4_Project.SystemAccess" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" type="text/css" href="/Content/login.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <div>
        <section>
            <asp:PlaceHolder runat="server" ID="changePasswordHolder">
                <div class="form-horizontal">
                    <h4>Account Inaccessible</h4>
                    <hr />
                    <br />
                    <p>This account has been deactivated and is not accessible.</p>
                    <p>If you have not been informed about this action, please contact us.</p>
                    <br /><br />
                    <a href="/Contact" class="a_tag_btn" style="background-color: var(--secondary-primary)">Contact</a>
                    <br /><br />
                </div>
            </asp:PlaceHolder>
        </section>
    </div>
</asp:Content>