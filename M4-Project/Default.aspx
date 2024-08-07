﻿<%@ Page Title="Home - Friends & Family" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="M4_Project._Default" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/home_style.css">
    <link rel="stylesheet" type="text/css" href="Content/Components/promo_items_wrapper.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="header_section">
		<div class="image_holder">
			<img src="assets/home_header_image.jpg">
		</div>

		<div class="section_content">
			<h2 class="section_header">Welcome to Friends & Family</h2>
			<p class="section_description">Friends and family provide a healthy fast food alternative to students at an affordable price.</p>
			<a href="Menu"><div class="section_action">Order Now</div></a>
		</div>
	</div>

	<div class="section">
		<div class="image_holder">
			<img src="assets/temp/chicken_pie.jpg">
		</div>

		<div class="section_content">
			<h2 class="section_header">Chicken Pie</h2>
			<p class="section_description">Indulge in the savory delight of our mouthwatering Chicken Pie! Made with tender, succulent chicken, and a rich, flavorful gravy, our Chicken Pie is the ultimate comfort food. Baked to golden perfection with a flaky, buttery crust, every bite is a warm and satisfying experience.</p>
			<a href="/Menu" class="a_tag_btn section_action">Order Now</a>
		</div>
	</div>

	<div class="section right_section">
		<div class="image_holder">
			<img src="assets/event_catering.jpg">
		</div>

		<div class="section_content">
			<h2 class="section_header">Are you having a wedding?</h2>
			<p class="section_description">Let our service transform your special day into an unforgettable wedding experience. 
				Book our catering services to add a touch of culinary elegance and create cherished memories for you and your guests.</p>
			<a href="/MakeBooking" class="a_tag_btn section_action">Make Booking</a>
		</div>
	</div>

	<div class="section">
		<div class="image_holder">
			<img src="assets/temp/oreo.jpg">
		</div>

		<div class="section_content">
			<h2 class="section_header">Oreo Shake</h2>
			<p class="section_description">Indulge in the creamy delight of our Oreo Shake – a rich blend of velvety ice cream and the irresistible crunch of Oreo cookies.</p>
			<a href="/Menu" class="a_tag_btn section_action">Order Now</a>
		</div>
	</div>

	

	<br /><br />

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

</asp:Content>
