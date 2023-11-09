<%@ Page Title="Cart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="M4_Project.Cart" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/Components/promo_items_wrapper.css">
	<link rel="stylesheet" type="text/css" href="Content/cart_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
			<% if (sale == null || sale.SaleType == M4_Project.Models.Sales.SaleType.EventBooking)
                { %>
			<button ID="btnOrderCart" runat="server" OnServerClick="btnOrderCart_Click">Switch to Order Cart</button>
			<% }
                else
                { %>
			<button ID="btnBookingCart" runat="server" OnServerClick="btnBookingCart_Click">Switch to Booking Cart</button>
			<% } %>
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
							<a href="/MenuItem?item=<%# Eval("ItemID") %>">EDIT</a>
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
					<p id="deliveryFeeLabel">Delivery Fee</p>
					<p id="deliveryFee" class="right_text">R <%= M4_Project.Models.BusinessRules.Delivery.DeliveryFee.ToString("N2") %></p>
					<select runat="server" style="display:block; font-size: 1rem; text-transform: uppercase; border: 1px solid #ddd;" id="select_category">
						<option value="D">Delivery</option>
						<option value="C">Collection</option>
					</select>
					<br>
					<br>
					<h2>Order Total</h2>
					<h2 id="totalCostHolder" class="right_text">R <%= (TotalCost + M4_Project.Models.BusinessRules.Delivery.DeliveryFee) %></h2>
				</div>
			<% } %>
			<button id="checkoutBtn" runat="server" OnServerClick="btnCheckout_Click">CHECKOUT</button>
		</div>

		<div class="promo_items_wrapper">
		<header>You May Like</header>
		<div class="promo_items">
			<asp:Repeater runat="server" ID="PromoRepeater" EnableViewState="false">
				<ItemTemplate>
					<a href="MenuItem?Item=<%# Eval("ItemID") %>">
						<div class="item">
							<div class="image_holder"><img src="<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("ItemImage")) %>"></div>
							<div class="promo_item_detail">
								<h2><%# Eval("ItemName") %></h2>
								<h3> R <%# Eval("ItemPriceN2") %></h3>
							</div>
						</div>
					</a>
				</ItemTemplate>
			</asp:Repeater>
		</div>
	</div>

	<script>
        function removeItemInCart(ItemID) {
            event.preventDefault();
            var itemID = ItemID;
            $.ajax({
                type: "POST",
                url: "/MenuItem.aspx/RemoveItem",
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
        var selectCategory = document.getElementById('<%= select_category.ClientID%>');
        var deliveryFee = document.getElementById('deliveryFee');
		var deliveryFeeLabel = document.getElementById('deliveryFeeLabel');
        let totalCost = <%= (TotalCost + 50) %>;

        selectCategory.addEventListener('change', function () {
			var selectedOption = selectCategory.value;
			if (selectedOption === 'D') {
                deliveryFee.style.display = 'inline-block';
				deliveryFeeLabel.style.display = 'inline-block';
				totalCost += 50;
			} else {
                deliveryFee.style.display = 'none';
				deliveryFeeLabel.style.display = 'none';
                totalCost -= 50;
			}
            // Assuming 'totalCost' is a numeric value representing the total cost
            var formattedTotalCost = 'R ' + totalCost.toFixed(2);

            document.getElementById('totalCostHolder').innerHTML = formattedTotalCost;

		});
    </script>
</asp:Content>
