<%@ Page Title="Live Orders" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="LiveOrders.aspx.cs" Inherits="M4_Project.Admin.LiveOrders" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/orders_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    	<div class="secondary_header">
			<h1>Live Orders</h1>
			<div class="header_input">
				<div>
					<input type="text" id="staff_name" placeholder="Search by customer name...">
					<svg class="search_icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960">
						<path d="M796-121 533-384q-30 26-69.959 40.5T378-329q-108.162 0-183.081-75Q120-479 120-585t75-181q75-75 181.5-75t181 75Q632-691 632-584.85 632-542 618-502q-14 40-42 75l264 262-44 44ZM377-389q81.25 0 138.125-57.5T572-585q0-81-56.875-138.5T377-781q-82.083 0-139.542 57.5Q180-666 180-585t57.458 138.5Q294.917-389 377-389Z"/>
					</svg>
				</div>
				<select id="select_order_type">
					<option>Order Type</option>
					<option>In-Store</option>
					<option>Delivery</option>
					<option>Collection</option>
				</select>
			</div>
		</div>
		
		<div>
			<% if (newOrders != null && newOrders.Count > 0)
            { %>
			<h2 class="role_label">New Online Orders</h2>
			<table>
			  <tr>
			    <th>Customer</th>
			    <th>Order Number</th>
			    <th>Order Type</th>
			    <th>Payment</th>
			    <th></th>
			  </tr>
			  <asp:Repeater ID="OrderRepeater" runat="server">
				<ItemTemplate>
					<tr>
						<td><%# Eval("CustomerName") %></td>
						<td><%# Eval("OrderID") %></td>
						<td><%# Eval("OrderType") %></td>
						<td><%# Eval("PaymentAmount", "R {0:F2}") %></td>
						<td>
							<button class="accept_btn">Accept</button>
							<button class="reject_btn">Reject</button>
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>

			</table>
			<% } %>
		</div>

		<br>
		
		<div class="secondary_table">
			<h2 class="role_label">Orders</h2>
			<table>
				<tr>
					<th>Order Number</th>
					<th>Order Type</th>
					<th>Order Status</th>
					<th>Date</th>
					<th>Payment</th>
					<th></th>
				</tr>
				<asp:Repeater ID="LiveOrderRepeater" runat="server">
					<ItemTemplate>
						<tr>
							<td><%# Eval("OrderID") %></td>
							<td><%# Eval("OrderType") %></td>
							<td><%# Eval("OrderStatus") %></td>
							<td><%# Eval("PaymentDate") %></td>
							<td><%# Eval("PaymentAmount", "R {0:F2}") %></td>
							<td>
								<button class="accept_btn">View Details</button>
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
			</table>
		</div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	
</asp:Content>