<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="MenuItem.aspx.cs" Inherits="M4_Project.Admin.POS.MenuItem" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/components/promo_items_wrapper.css">
	<link rel="stylesheet" type="text/css" href="/Content/menu_item_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
	<div class="item_details_wrapper">
		<div class="item_image_wrapper">
			<asp:Image ID="Image1" runat="server"/>
		</div>
		<div class="item_details">
			<h1><%: menuItem.ItemName %></h1>
			<div class="middle_details_wrapper">
				<p class="item_description"><%: menuItem.ItemDescription %></p>
				<p class="item_price">R <%: menuItem.ItemPriceN2 %></p>
				
			</div>

			<div class="bottom_details_wrapper">
				<h2><%: menuItem.ItemCategory %></h2>
				<label>Quantity</label>
				<br>
				<asp:DropDownList runat="server" ID="itemQuantity">
					<asp:ListItem Text="1" Value="1" />
					<asp:ListItem Text="2" Value="2" />
					<asp:ListItem Text="3" Value="3" />
					<asp:ListItem Text="4" Value="4" />
					<asp:ListItem Text="5" Value="5" />
					<asp:ListItem Text="6" Value="6" />
				</asp:DropDownList>
				<br>
				<% if (Session["POS"] != null && (Session["POS"] as M4_Project.Models.Sales.Sale).Cart.ContainsKey(menuItem.ItemID))
                    { %>
				<button id="btnEditItem" runat="server" type="submit" onclick="editItemInCart()">Edit</button>
				<button class="remove_button" id="btnRemoveItem" runat="server" type="submit" onclick="removeItemInCart()">Remove</button>
				<% }
                    else
                    { %>
				<button id="btnAddToCart" runat="server" type="submit" onclick="addItemToCart()">Add to Cart</button>
				<% } %>
			</div>
		</div>
	</div>

	<div class="instructions_wrapper">
		<h2>Instructions</h2>
		<p>Specify any preparation instructions you will like.</p>
		<asp:TextBox runat="server" ID="txtInstructions" TextMode="MultiLine" placeholder="Enter instructions here, eg. No tomato, No cake..."></asp:TextBox>
	</div>


	<script type="text/javascript">
		function addItemToCart() {
			event.preventDefault();
			console.log("Hallo");
			var itemID = <%= menuItem.ItemID %>;
            var quantity = document.getElementById('<%= itemQuantity.ClientID %>').value;
            var instructions = document.getElementById('<%= txtInstructions.ClientID %>').value;


            $.ajax({
                type: "POST",
                url: "/Admin/POS/MenuItem.aspx/AddItem",
                data: JSON.stringify({ itemID: itemID, qty: quantity, instructions: instructions }),
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
        function editItemInCart() {
            event.preventDefault();
            var itemID = <%= menuItem.ItemID %>;
            var quantity = $("#<%= itemQuantity.ClientID %>").val();
            var instructions = $("#<%= txtInstructions.ClientID %>").val();

            $.ajax({
                type: "POST",
                url: "/Admin/POS/MenuItem.aspx/EditItem",
                data: JSON.stringify({ itemID: itemID, qty: quantity, instructions: instructions }),
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
        function removeItemInCart() {
            event.preventDefault();
            var itemID = <%= menuItem.ItemID %>;
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
