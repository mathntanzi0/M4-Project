<%@ Page Title="Live Delivery Orders" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DeliverOrders.aspx.cs" Inherits="M4_Project.Admin.Delivery.DeliverOrders" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/sales_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    	<div class="secondary_header">
			<h1>Live Orders</h1>
		</div>
		
		<% if (liveOrders != null && liveOrders.Count > 0)
            { %>
		<div class="secondary_table">
			<table>
			<tr>
			    <th>Customer</th>
			    <th>Phone Number</th>
			    <th>Location</th>
			    <th>Order Number</th>
			    <th></th>
			  </tr>
				<asp:Repeater ID="OrderRepeater" runat="server" OnItemCommand="OrderRepeater_ItemCommand">
					<ItemTemplate>
						<tr>
							<td><%# Eval("Customer.FullName") %></td>
							<td><%# Eval("Customer.PhoneNumber") %></td>
							<td><%# Eval("Delivery.DeliveryAddress.AddressName_Short") %></td>
							<td><%# Eval("OrderID") %></td>
							<td>
								<asp:Button runat="server" Text="Deliver" CommandName="Deliver" CommandArgument='<%# Eval("OrderID") %>' CssClass="accept_btn" />
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
			</table>
		</div>
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