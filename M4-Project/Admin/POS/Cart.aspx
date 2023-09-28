<%@ Page Title="Cart" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="M4_Project.Admin.POS.Cart" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/Components/promo_items_wrapper.css">
	<link rel="stylesheet" type="text/css" href="/Content/cart_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
<div class="secondary_header">
			<% if (sale != null)
			{ %>
				<h1><%= (sale.SaleType == M4_Project.Models.Sales.SaleType.Order) ? "Order" : "Event" %> Cart</h1>
			<% }
			else
			{ %>
				<h1>Cart</h1>
			<% } %>
		</div>

		<div class="sale_type_btns">
			<button ID="btnOrderCart" runat="server" OnServerClick="btnOrderCart_Click">Switch to Order</button>
			<button ID="btnBookingCart" runat="server" OnServerClick="btnBookingCart_Click">Switch to Booking</button>
		</div>
		<% if (TotalCost == 0) { %>
		<div id="empty_box">
			<h3>YOUR CART IS EMPTY</h3>
			<p>Once you add something in your cart - it will appear here.</p>
			<a href="/Menu"><div>Continue To Menu</div></a>
		</div>
		<% } %>
		<div class="items_container">
			<asp:Repeater ID="RepeaterItemLine" runat="server" EnableViewState="false">
				<ItemTemplate>
					<div class="item_wrapper">
						<div class="item_details">
							<img src="<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("Image")) %>">
							<div class="item_details_wrapper">
								<h2><%# Eval("ItemName") %></h2>
								<p> R <%# Eval("TotalSubCostN2") %></p>
								<p>quantity <%# Eval("ItemQuantity") %></p>
							</div>
						</div>
						<div class="item_action_wrapper">
							<a href="/Admin/POS/MenuItem?item=<%# Eval("ItemID") %>">EDIT</a>
							<a style="cursor:pointer" class="right_text" onclick="removeItemInCart(<%# Eval("ItemID") %>)">REMOVE ITEM</a>
						</div>
					</div>
				</ItemTemplate>
			</asp:Repeater>
		</div>
		<div class="cart_summary">
			<div class="cart_summary_header">
				<% if (sale != null && sale.SaleType == M4_Project.Models.Sales.SaleType.EventBooking)
                    { %>
				<h2>Event Line</h2>
				<% }
                    else
                    { %>
				<h2>Order Summary</h2>
				<% } %>
			</div>
			<% if (sale != null && sale.SaleType == M4_Project.Models.Sales.SaleType.EventBooking)
                { %>
				<div class="cart_summary_details">
					<p>SUBTOTAL</p>
					<p class="right_text">R <%= TotalCost %></p>
					<br>
					<p>Booking Fee</p>
					<p class="right_text">R <%= M4_Project.Models.BusinessRules.Booking.BookingFee.ToString("N2") %></p>
					<br>
					<br>
					<h2>Event Total Cost</h2>
					<h2 id="totalCostHolder" class="right_text">R <%= (TotalCost + M4_Project.Models.BusinessRules.Booking.BookingFee).ToString("N2") %></h2>
				</div>
			<% }
            else
            { %>
				<div class="cart_summary_details">
					<p>SUBTOTAL</p>
					<p class="right_text">R <%= TotalCost %></p>
					<br>
					<br>
					<h2>Order Total</h2>
					<h2 id="totalCostHolder" class="right_text">R <%= (TotalCost + M4_Project.Models.BusinessRules.Delivery.DeliveryFee) %></h2>
				</div>
			<% } %>
			<button id="checkoutBtn" runat="server" OnServerClick="btnCheckout_Click">CHECKOUT</button>
		</div>

	<script>
        function removeItemInCart(ItemID) {
            event.preventDefault();
            var itemID = ItemID;
            $.ajax({
                type: "POST",
                url: "/Admin/POS/MenuItem.aspx/RemoveItem",
                data: JSON.stringify({ itemID: itemID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    //alert("Item added to cart successfully.");
                    location.reload();
                },
                error: function (error) {
                    //alert("An error occurred: " + error.responseText);
                }
            });
		}
    </script>
</asp:Content>
