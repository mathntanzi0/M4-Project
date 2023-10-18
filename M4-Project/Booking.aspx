<%@ Page Title="Event Booking" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="M4_Project.Booking" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/Components/promo_items_wrapper.css">
	<link rel="stylesheet" type="text/css" href="Content/booking_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="header_section">
		<div class="image_holder">
			<img src="assets/event_catering_3.jpg">
		</div>

		<div class="section_content">
			<h2 class="section_header">Having an event?</h2>
			<p class="section_description">Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
			<a href="MakeBooking"><div class="section_action">Make Booking</div></a>
		</div>
	</div>

	<div class="section">
		<div class="image_holder">
			<img src="assets/event_catering_2.jpg">
		</div>

		<div class="section_content">
			<h2 class="section_header">Lorem Ipsum</h2>
			<p class="section_description">Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
			<button class="section_action">Make Booking</button>
		</div>
	</div>

	<div class="section">
		<div class="image_holder">
			<img src="assets/event_catering_1.jpg">
		</div>

		<div class="section_content">
			<h2 class="section_header">Lorem Ipsum</h2>
			<p class="section_description">Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
			<button class="section_action">Make Booking</button>
		</div>
	</div>

	<div class="promo_items_wrapper">
		<header>You May Like</header>
		<div class="promo_items">
			<asp:Repeater runat="server" ID="PromoRepeater">
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
</asp:Content>
