<%@ Page Title="Orders" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Bookings.aspx.cs" Inherits="M4_Project.Admin.Bookings" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/sales_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    	<div class="secondary_header">
			<h1>Bookings</h1>
			<div>
				<asp:TextBox ID="search_bar" runat="server" CssClass="search-input" placeholder="Search by customer or order number..." OnTextChanged="SearchTextBox_TextChanged"></asp:TextBox>
				<svg class="search_icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960">
					<path d="M796-121 533-384q-30 26-69.959 40.5T378-329q-108.162 0-183.081-75Q120-479 120-585t75-181q75-75 181.5-75t181 75Q632-691 632-584.85 632-542 618-502q-14 40-42 75l264 262-44 44ZM377-389q81.25 0 138.125-57.5T572-585q0-81-56.875-138.5T377-781q-82.083 0-139.542 57.5Q180-666 180-585t57.458 138.5Q294.917-389 377-389Z"/>
				</svg>
			</div>
			<asp:DropDownList ID="select_order_type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectOrderType_Changed">
				<asp:ListItem Text="Order Type" Value="" />
				<asp:ListItem Text="In-Store" Value="In-Store" />
				<asp:ListItem Text="Delivery" Value="Delivery" />
				<asp:ListItem Text="Collection" Value="Collection" />
			</asp:DropDownList>
		</div>
		
		<div class="secondary_table">
			<h2 class="role_label">Orders</h2>
			<table>
				<tr>
					<th>Customer</th>
					<th>Order Number</th>
					<th>Order Type</th>
					<th>Order Status</th>
					<th>Date</th>
					<th>Payment</th>
					<th></th>
				</tr>
				<asp:Repeater ID="OrderRepeater" runat="server" OnItemCommand="OrderRepeater_ItemCommand">
					<ItemTemplate>
						<tr>
							<td><%# Eval("CustomerName") %></td>
							<td><%# Eval("OrderID") %></td>
							<td><%# Eval("OrderType") %></td>
							<td><%# Eval("OrderStatus") %></td>
							<td><%# Eval("PaymentDate") %></td>
							<td><%# Eval("PaymentAmount", "R {0:F2}") %></td>
							<td>
								<asp:Button runat="server" Text="View Details" CommandName="ViewDetails" CommandArgument='<%# Eval("OrderID") %>' CssClass="accept_btn" />
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>

			</table>
		</div>
		
		<div id="empty_box">
			<h3>No Booking Found</h3>
			<p>Make sure your search parameters are correct</p>
			<a href="/Admin/POS"><div>Point of Sale</div></a>
		</div>

	<div class="page_nav_container">
		<asp:Button ID="btnPreviousPage" runat="server" Text="<<" OnClick="btnPreviousPage_Click" CssClass="page_box page_index_box" />
			<div class="page_box"><%= 1 %></div>
		<asp:Button ID="btnNextPage" runat="server" Text=">>" OnClick="btnNextPage_Click" CssClass="page_box page_index_box" />
	</div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	
</asp:Content>