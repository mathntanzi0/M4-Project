<%@ Page Title="Your Orders" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="M4_Project.Customer.Orders" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" type="text/css" href="Content/sells_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
		<div class="secondary_header">
			<h1>Orders</h1>
		</div>

		<p class="page_message">Your most recent orders</p>
		<div class="sell_card_container">
			<% if (orders.Count < 1) { %>
			<div id="empty_box">
				<h3>No Orders Found</h3>
				<p>Once you have made an order - it will appear here.</p>
				<a href="/Menu"><div>Menu</div></a>
			</div>
			<% } %>
			<asp:Repeater ID="orderRepeater" runat="server">
				<ItemTemplate>
					<div class="sell_card">
						<div class="date_container">
							<h2>
								<%# Eval("PaymentDate", "{0:MMMM}") %> <br>
								<%# Eval("PaymentDate", "{0:dd}") %> <br>
								<%# Eval("PaymentDate", "{0:yyyy}") %>
							</h2>
						</div>
						<div class="sell_details">
							<h2 class="header_detail">#<%# Eval("OrderID") %> <%# Eval("OrderType") %></h2>
							<h2 class="sell_status">Status: <span style='color:<%# GetStatusColor(Eval("OrderStatus")) %>'><%# Eval("OrderStatus") %></span></h2>
						</div>
						<div class="right_sell_details">
							<h2 class="sell_payment"><br /></h2>
							<svg class="common_icon" xmlns="http://www.w3.org/2000/svg" height="48" viewBox="0 -960 960 960" width="48">
								<path d="m560-242-43-42 168-168H160v-60h525L516-681l43-42 241 241-240 240Z" />
							</svg>
							<h2 class="sell_payment"><%# Eval("PaymentAmount", "{0:C}") %></h2>
						</div>
					</div>
				</ItemTemplate>
			</asp:Repeater>
		</div>
</asp:Content>
