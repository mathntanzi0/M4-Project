<%@ Page Title="Customers" Language="C#" MasterPageFile="~/Admin/Admin.Master" CodeBehind="Customers.aspx.cs" Inherits="M4_Project.Admin.POS.Customers" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/table_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    <div class="secondary_header">
			<h1>Customer</h1>
			<div class="header_input">
				<div>
					<asp:TextBox ID="search_bar" runat="server" CssClass="search-input" placeholder="Search by customer name..." OnTextChanged="SearchTextBox_TextChanged"></asp:TextBox>
					<svg class="search_icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960">
						<path d="M796-121 533-384q-30 26-69.959 40.5T378-329q-108.162 0-183.081-75Q120-479 120-585t75-181q75-75 181.5-75t181 75Q632-691 632-584.85 632-542 618-502q-14 40-42 75l264 262-44 44ZM377-389q81.25 0 138.125-57.5T572-585q0-81-56.875-138.5T377-781q-82.083 0-139.542 57.5Q180-666 180-585t57.458 138.5Q294.917-389 377-389Z"/>
					</svg>
				</div>
			</div>
		</div>
		<% if (customers.Count > 0)
            { %>
		<div class="secondary_table">
			<table>
				<tr>
				    <th>First Name</th>
				    <th>Last Name</th>
				    <th>Email Address</th>
				    <th>Phone Number</th>
				    <th>Loyalty points</th>
				    <th></th>
				</tr>

				<asp:Repeater ID="CustomerRepeater" runat="server" OnItemCommand="CustomerRepeater_ItemCommand">
					<ItemTemplate>
						<tr>
							<td><%# Eval("FirstName") %></td>
							<td><%# Eval("LastName") %></td>
							<td><%# Eval("EmailAddress") %></td>
							<td><%# Eval("PhoneNumber") %></td>
							<td><%# Eval("LoyaltyPoints") %></td>
							<td>
							<asp:Button runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%# Eval("CustomerID") %>' CssClass="accept_btn" />
							<asp:Button runat="server" Text="Make Booking" CommandName="MakeBooking" CommandArgument='<%# Eval("CustomerID") %>' CssClass="accept_btn" />
						</td>
						</tr>
					</ItemTemplate>
				  </asp:Repeater>

			</table>
		</div>
		<% }
            else
            { %>
			<div id="empty_box">
				<h3>No Staff Member Found</h3>
				<p><%= p_NotFound %></p>
				<a href="/Admin/POS"><div>Point of Sale</div></a>
			</div>
		
		<% } %>
		<div class="page_nav_container">
		<% if (page > 1)
            { %>
		<asp:Button ID="btnPreviousPage" runat="server" Text="<<" OnClick="btnPreviousPage_Click" CssClass="page_box page_index_box" />
		<% } %>
			<div class="page_box"><%= page %></div>
		<% if (page < maxPage)
            { %>
		<asp:Button ID="btnNextPage" runat="server" Text=">>" OnClick="btnNextPage_Click" CssClass="page_box page_index_box" />
		<% } %>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	
</asp:Content>