<%@ Page Title="Ordering Currently Unavailable" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderUnavailable.aspx.cs" Inherits="M4_Project.OrderUnavailable" %>


<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" type="text/css" href="/Content/login.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <div>
        <section>
            <asp:PlaceHolder runat="server" ID="changePasswordHolder">
                <div class="form-horizontal">
                    <h4>The system is currently not accepting any orders</h4>
                    <hr />
                    <br />
                    <p>Online orders are not accepted at the moment. Please check back later.</p>
                    <br /><br />
                    <a href="/Menu" class="a_tag_btn" style="background-color: var(--secondary-primary)">Explore Menu</a>
                    <br /><br />
                </div>
            </asp:PlaceHolder>

        </section>
    </div>
</asp:Content>