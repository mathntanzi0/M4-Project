<%@ Page Title="Order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="M4_Project.Customer.Order" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/sale_view_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    	<div class="secondary_header" style="display:block">
			<h1 style="padding: 0.5rem 0">Order #<%=order.OrderID %></h1>
			
			<h1 style="padding: 0.5rem 0">Status: <span style="color:<%= M4_Project.Models.Sales.OrderState.GetStatusColor(order.OrderStatus) %>"> <%= order.OrderStatus %></span></h1>
		</div>
		<div class="items_container">
			<asp:Repeater ID="ItemRepeater" runat="server">
				<ItemTemplate>
					<div class="item_wrapper">
						<div class="image_holder">
							<img src="<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("Image")) %>">
						</div>
						<div class="item_details">
							<h2><%# Eval("ItemName") %></h2>
							<h4>Price  R <%# Eval("ItemCostN2") %></h4>
							<h4><%# Eval("ItemCategory") %></h4>
							<%# !string.IsNullOrEmpty(Eval("Instructions").ToString()) ? 
							"<h4>Instructions</h4><h4>" + Eval("Instructions") + "</h4>" : 
							"" %>
						</div>
						<div class="item_line_details">
							<h4>QTY <%# Eval("ItemQuantity") %></h4>
							<h4>Cost <%# Eval("TotalSubCostN2") %></h4>
						</div>
					</div>
				</ItemTemplate>
			</asp:Repeater>
		</div>
		<div class="sale_summary_wrapper">
			<div class="detail_group">
				<h2>Payment Details</h2>
				<p>Payment method: <%= order.PaymentMethod %></p>
				<p>Tip: R <%= order.TipN2 %></p>
				<p>Total payment: R <%= order.TotalAmountDueN2 %></p>
				<p>Payment date: <%= order.PaymentDate.ToString("dd MMMM yyyy HH:mm") %></p>
				<p>Order Type: <%= order.OrderType %></p>
			</div>
			<div class="detail_group"></div>
		</div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	
</asp:Content>