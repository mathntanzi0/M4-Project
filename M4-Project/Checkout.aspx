<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="M4_Project.Checkout" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/Components/promo_items_wrapper.css">
	<link rel="stylesheet" type="text/css" href="Content/checkout_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="secondary_header">
		<% if (sale != null && sale.SaleType == M4_Project.Models.Sales.SaleType.Order)
            { %>
			<h1>Order Summary</h1>
		<% }
            else
            { %>
			<h1>Event Line</h1>
		<% } %>
		</div>

		<div class="items_container">
			<asp:Repeater ID="RepeaterItemLine" runat="server">
				<ItemTemplate>
					<div class="item_wrapper">
						<div class="image_holder">
							<img src="<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("Image")) %>">
						</div>
						<div class="item_details">
							<h2><%# Eval("ItemName") %></h2>
							<h4>Price  R <%# Eval("ItemCostN2") %></h4>
							<h4><%# Eval("ItemCategory") %></h4>
						</div>
						<div class="item_line_details">
							<h4>QTY <%# Eval("ItemQuantity") %></h4>
							<h4>Cost <%# Eval("TotalSubCostN2") %></h4>
						</div>
					</div>
				</ItemTemplate>
			</asp:Repeater>	
		</div>

		<div class="order_summary_wrapper">
			<% if (sale.SaleType == M4_Project.Models.Sales.SaleType.Order)
                {%>
			<div class="summary_line_wrapper">
				<label>TIP: (R) </label>
				<input onchange="tip_changed()" id="tip_input" type="number" name="" placeholder="0.00">
			</div>
			<% } %>

			<br>
			<div class="summary_line_wrapper">
				<label>Number of Items: <%= sale.Cart.Count %></label>
			</div>
			<% if (sale.SaleType == M4_Project.Models.Sales.SaleType.Order && (sale as M4_Project.Models.Sales.Order).OrderType == M4_Project.Models.Sales.OrderType.Delivery)
                {%>
			<div class="summary_line_wrapper">
				<label>Sub Total: R <%= (TotalCost - M4_Project.Models.BusinessRules.Delivery.DeliveryFee).ToString("N2")%></label>
			</div>
			<div class="summary_line_wrapper">
				<label>Delivery Fee: R <%= M4_Project.Models.BusinessRules.Delivery.DeliveryFee.ToString("N2")%></label>
			</div>
			<div class="summary_line_wrapper">
				<label><a href="/SelectAddress">Update address</a></label>
			</div>
			<% }
                else if (sale.SaleType == M4_Project.Models.Sales.SaleType.EventBooking)
                { %>
				<div class="summary_line_wrapper">
					<label>Address: <%= ((M4_Project.Models.Sales.Booking)sale).EventAddress %></label>
				</div>
				<div class="summary_line_wrapper">
					<label>Decor: <%= ((M4_Project.Models.Sales.Booking)sale).EventDecorDescription%></label>
				</div>
				<div class="summary_line_wrapper">
					<label>Date: <%= ((M4_Project.Models.Sales.Booking)sale).EventDate.ToString("dd MMMM yyyy")%></label>
				</div>
				<div class="summary_line_wrapper">
					<label>Decor: <%= ((M4_Project.Models.Sales.Booking)sale).EventDuration.ToString(@"hh\:mm")%></label>
				</div>
				<div class="summary_line_wrapper">
					<label><a href="/MakeBooking">Update booking details</a></label>
				</div>
			<% } %>
			<br>
			<div class="summary_footer_wrapper">
				<h1>Total Cost: R <span id="cost_holder"><%= TotalCost %></span></h1>
			</div>
		</div>
		<button class="primary_button_unfixed">Pay</button>
</asp:Content>

<asp:Content ID="ScriptHolder" ContentPlaceHolderID="ContentScripts" runat="server">
	<script type="text/javascript">
        const cost = <%= TotalCost %>;
        let amount = <%= TotalCost %>;
		function tip_changed() {
			const tip_input_field = document.getElementById("tip_input");
			const cost_holder = document.getElementById("cost_holder");
			const tip = tip_input_field.value;
			amount = formatToN2(cost + parseFloat(tip));
			cost_holder.innerHTML = amount;
		}

		function formatToN2(number) {
		  const roundedNumber = Number(number.toFixed(2));
		  return roundedNumber.toFixed(2).padStart(5, "0");
		}
    </script>
</asp:Content>
