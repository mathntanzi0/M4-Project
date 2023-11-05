<%@ Page Title="Live Orders" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="LiveOrders.aspx.cs" Inherits="M4_Project.Admin.LiveOrders" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/sales_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    	<div class="secondary_header">
			<h1>Live Orders</h1>
			<asp:DropDownList ID="select_order_type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectOrderType_Changed">
				<asp:ListItem Text="Order Type" Value="" />
				<asp:ListItem Text="In-Store" Value="In-Store" />
				<asp:ListItem Text="Delivery" Value="Delivery" />
				<asp:ListItem Text="Collection" Value="Collection" />
			</asp:DropDownList>
		</div>

	<br />
	<label style="display:block">Allow online orders</label>
	<label class="switch">
	  <asp:CheckBox ID="myCheckBox" runat="server" OnCheckedChanged="MyCheckBox_CheckedChanged" AutoPostBack="true" />
	  <span class="slider round"></span>
	</label>
	<br />
		
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
			    <th></th>
			  </tr>
			  <asp:Repeater ID="OrderRepeater" runat="server" OnItemCommand="NewOrderRepeater_ItemCommand">
				<ItemTemplate>
					<tr>
						<td><%# Eval("CustomerName") %></td>
						<td><%# Eval("OrderID") %></td>
						<td><%# Eval("OrderType") %></td>
						<td><%# Eval("PaymentAmount", "R {0:F2}") %></td>
						<td>
							<asp:Button runat="server" Text="Accept" CommandName="Accept" CommandArgument='<%# Eval("OrderID") %>' CssClass="accept_btn" />
							<asp:Button runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("OrderID") %>' CssClass="reject_btn" />
						</td>
						<td>
							<asp:Button runat="server" Text="View" CommandName="View" CommandArgument='<%# Eval("OrderID") %>' CssClass="accept_btn" />
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>

			</table>
			<% } %>
		</div>

		<br>

		
		<% if (liveOrders != null && liveOrders.Count > 0)
            { %>
			<script>
                playDefaultNotificationSound();
            </script>
		<div class="secondary_table">
			<% if (newOrders != null && newOrders.Count > 0)
                { %>
			<h2 class="role_label">Orders</h2>
			<% } %>
			<table>
				<tr>
					<th>Order Number</th>
					<th>Order Type</th>
					<th>Order Status</th>
					<th>Date</th>
					<th>Payment</th>
					<th></th>
				</tr>
				<asp:Repeater ID="LiveOrderRepeater" runat="server" OnItemCommand="LiveOrderRepeater_ItemCommand">
					<ItemTemplate>
						<tr>
							<td><%# Eval("OrderID") %></td>
							<td><%# Eval("OrderType") %></td>
							<td style="color: <%# M4_Project.Models.Sales.OrderState.GetStatusColor(Eval("OrderStatus").ToString()) %> "><%# Eval("OrderStatus") %></td>
							<td><%# Eval("Str_PaymentDate") %></td>
							<td><%# Eval("PaymentAmount", "R {0:F2}") %></td>
							<td>
								<asp:Button runat="server" Text="View Details" CommandName="ViewDetails" CommandArgument='<%# Eval("OrderID") %>' CssClass="accept_btn" />
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>

			</table>
		</div>
		<% } %>
		<% if ((newOrders == null || newOrders.Count < 1) && (liveOrders == null || liveOrders.Count < 1)) { %>
		<div id="empty_box">
			<h3>No Live Order Found</h3>
			<p>There is currently no live orders.</p>
			<a href="/Admin/POS"><div>Point of Sale</div></a>
		</div>
		<% } %>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	<script>
        setInterval(function () {
            location.reload();
		}, 10000);
        function playDefaultNotificationSound() {
            var sound = new Audio('/Assets/mixkit-magic-marimba-2820.wav');
            sound.play();
		}
    </script>
</asp:Content>