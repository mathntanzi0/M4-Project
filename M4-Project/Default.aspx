<%@ Page Title="Home - Friends & Family" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="M4_Project._Default" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/home_style.css">
    <link rel="stylesheet" type="text/css" href="Content/Components/promo_items_wrapper.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="header_section">
		<div class="image_holder">
			<img src="assets/home_header_image.png">
		</div>

		<div class="section_content">
			<h2 class="section_header">Welcome to Friends & Family</h2>
			<p class="section_description">Friends and family provide a healthy fast food alternative to students at an affordable price.</p>
			<a href="Menu"><div class="section_action">Order Now</div></a>
		</div>
	</div>

	<div class="section">
		<div class="image_holder">
			<img src="assets/temp/oreo.jpg">
		</div>

		<div class="section_content">
			<h2 class="section_header">Oreo Shake</h2>
			<p class="section_description">Indulge in the creamy delight of our Oreo Shake – a rich blend of velvety ice cream and the irresistible crunch of Oreo cookies.</p>
			<button class="section_action">Order Now</button>
		</div>
	</div>

	<div class="section right_section">
		<div class="image_holder">
			<img src="assets/temp/oreo.jpg">
		</div>

		<div class="section_content">
			<h2 class="section_header">Oreo Shake</h2>
			<p class="section_description">Indulge in the creamy delight of our Oreo Shake – a rich blend of velvety ice cream and the irresistible crunch of Oreo cookies.</p>
			<button class="section_action">Order Now</button>
		</div>
	</div>

	<div class="section">
		<div class="image_holder">
			<img src="assets/event_catering.jpg">
		</div>

		<div class="section_content">
			<h2 class="section_header">Are you having a wedding?</h2>
			<p class="section_description">Let our service transform your special day into an unforgettable wedding experience. 
				Book our catering services to add a touch of culinary elegance and create cherished memories for you and your guests.</p>
			<button class="section_action">Make Booking</button>
		</div>
	</div>

	<br /><br />

	<div class="promo_items_wrapper">
		<header>You May Like</header>
		<div class="promo_items">
			<div class="item">
				<div class="image_holder"><img src="assets/temp/oreo_ice_cream.jpg"></div>
				<div class="promo_item_detail">
					<h2>Name</h2>
					<h3>R 80.50</h3>
				</div>
			</div>
			<div class="item">
				<div class="image_holder"><img src="assets/temp/eggs.jpg"></div>
				<div class="promo_item_detail">
					<h2>Name</h2>
					<h3>R 80.50</h3>
				</div>
			</div>
			<div class="item">
				<div class="image_holder"><img src="assets/temp/oreo.jpg"></div>
				<div class="promo_item_detail">
					<h2>Name</h2>
					<h3>R 80.50</h3>
				</div>
			</div>
			<div class="item">
				<div class="image_holder"><img src="assets/temp/appletiser.jpg"></div>
				<div class="promo_item_detail">
					<h2>Name</h2>
					<h3>R 80.50</h3>
				</div>
			</div>
			<div class="item">
				<div class="image_holder"><img src="assets/temp/eggs.jpg"></div>
				<div class="promo_item_detail">
					<h2>Name</h2>
					<h3>R 80.50</h3>
				</div>
			</div>
		</div>
	</div>

</asp:Content>
