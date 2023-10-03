<%@ Page Title="Live Delivery Orders" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DeliverOrders.aspx.cs" Inherits="M4_Project.Admin.Delivery.DeliverOrders" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/deliver_orders_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    	<div class="secondary_header">
			<h1>Delivery Orders</h1>
		</div>
		
		<% if (liveOrders != null && liveOrders.Count > 0)
            { %>
			<asp:Repeater ID="OrderRepeater" runat="server" OnItemCommand="OrderRepeater_ItemCommand">
				<ItemTemplate>
					<div class="order_wrapper">
						<h2>Order: #<%# Eval("OrderID") %></h2>
						<div>
							<p>Customer: <%# Eval("Customer.FullName") %></p>
							<p>Phone Number: <%# Eval("Customer.PhoneNumber") %> </p>
							<p>Location: <%# Eval("Delivery.DeliveryAddress.AddressName_Short") %></p>
						</div>
						<asp:Button runat="server" Text="Deliver" CommandName="Deliver" CommandArgument='<%# Eval("OrderID") %>' CssClass="accept_btn" />
					</div>
				</ItemTemplate>
			</asp:Repeater>
						
		<% } %>
		<% if (liveOrders == null || liveOrders.Count < 1) { %>
		<div id="empty_box">
			<h3>No Live Order Found</h3>
			<p>There is currently no live delivery orders.</p>
			<a href="/Admin/POS"><div>Point of Sale</div></a>
		</div>
		<% } %>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	
</asp:Content>