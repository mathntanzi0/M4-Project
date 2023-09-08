<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="M4_Project.Menu" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/menu_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
		<div class="secondary_header">
			<h1>Menu</h1>
			<div class="header_input">
				<div>
					<asp:TextBox ID="itemName" runat="server" Placeholder="Search by item name..."></asp:TextBox>
					<svg class="search_icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960">
						<path d="M796-121 533-384q-30 26-69.959 40.5T378-329q-108.162 0-183.081-75Q120-479 120-585t75-181q75-75 181.5-75t181 75Q632-691 632-584.85 632-542 618-502q-14 40-42 75l264 262-44 44ZM377-389q81.25 0 138.125-57.5T572-585q0-81-56.875-138.5T377-781q-82.083 0-139.542 57.5Q180-666 180-585t57.458 138.5Q294.917-389 377-389Z"/>
					</svg>
				</div>
				<asp:DropDownList ID="category_list" runat="server">
					<asp:ListItem Text="Category" Value="" />
				</asp:DropDownList>
				<div>
					<asp:Button ID="btnSubmit" runat="server" Text="Apply" OnClick="btnSubmit_Click" CssClass="apply_button" />
				</div>
			</div>
		</div>
		<% if (menuItems.Count < 1) { %>
		<div id="empty_box">
			<h3>No Items Found</h3>
			<p>If you're looking for a specific item, please feel free to contact us and let us know. We'll be happy to assist you!</p>
			<a href="/Contact"><div>Contact Us</div></a>
		</div>
		<% } %>
		<div class="items_container">
			<asp:Repeater ID="RepeaterMenuItem" runat="server">
				<ItemTemplate>
					<a href="MenuItem?Item=<%# Eval("ItemID") %>">
						<div class="item_wrapper">
							<img src="<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("ItemImage")) %>">

							<h2><%# Eval("ItemName") %></h2>
							<h3> R <%# Eval("ItemPriceN2") %></h3>
						</div>
					</a>
				</ItemTemplate>
			</asp:Repeater>
		</div>

		<div class="page_nav_container">
			<asp:Button ID="btnPreviousPage" runat="server" Text="<<" OnClick="btnPreviousPage_Click" CssClass="page_box page_index_box" />
				<div class="page_box"><%= SearchPage %></div>
			<asp:Button ID="btnNextPage" runat="server" Text=">>" OnClick="btnNextPage_Click" CssClass="page_box page_index_box" />
		</div>

	<script type="text/javascript">
		
    </script>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
</asp:Content>