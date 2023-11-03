<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="MenuItem.aspx.cs" Inherits="M4_Project.Admin.MenuItem" %>
<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/menu_item_style.css">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    	<div class="item_details_wrapper">
			<div class="item_image_wrapper">
				<asp:Image ID="Image1" runat="server" EnableViewState="false"/>
			</div>
			<div class="item_details">
				<h1><%: menuItem.ItemName %></h1>
				<div class="middle_details_wrapper">
					<p class="item_description"><%: menuItem.ItemDescription %></p>
					<p class="item_price">R <%: menuItem.ItemPriceN2 %></p>
				</div>

				<div class="bottom_details_wrapper">
					<h2>Category: <%: menuItem.ItemCategory %></h2>
				</div>
			</div>

		</div>
		<asp:DropDownList ID="category_list" runat="server">
			<asp:ListItem Text="Available" Value="Available" />
			<asp:ListItem Text="Unavailable" Value="Unavailable" />
		</asp:DropDownList>
		<div class="analytic_data_wrapper">
			<h1>Summary</h1>
			<div class="text_details_wrapper">
				<p>For This Month (<%= DateTime.Now.ToString("MMMM yyyy") %>):</p>
				<p>Number of orders the item is involved in: <%= orderItemSummary.RowsCount %></p>
				<p>Total units sold (for orders): <%= orderItemSummary.TotalQty %></p>
				<p>Total revenue (for orders): R<%= orderItemSummary.TotalAmount.ToString("N2") %></p>
				<br />
				<p>Number of bookings the item is involved in: <%= bookingItemSummary.RowsCount %></p>
				<p>Total units sold (for bookings): <%= bookingItemSummary.TotalQty %></p>
				<p>Total revenue (for bookings): R<%= bookingItemSummary.TotalAmount.ToString("N2") %></p>

				<br />
				<p>Total units sold (for both): <%= bookingItemSummary.TotalQty + orderItemSummary.TotalQty %></p>
				<p>Total revenue (for both): R<%= (bookingItemSummary.TotalAmount + orderItemSummary.TotalAmount).ToString("N2") %></p>
			</div>
			<br />
			<br />
		</div>

		<div class="inline_buttons">
			<asp:Button ID="EditButton" runat="server" Text="Edit" OnClick="EditButton_Click" CommandArgument="<%# menuItem.ItemID %>" CssClass="primary_button"/>
			<asp:Button ID="DeleteButton" runat="server" Text="Delete" OnClientClick="return confirmDelete();" onClick="DeleteButton_Click" CommandArgument="<%# menuItem.ItemID %>" CssClass="delete_button"/>
		</div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	<script type="text/javascript">
		function confirmDelete() {
			return confirm("Are you sure you want to delete this item?");
		}
    </script>
</asp:Content>