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
			<p class="section_description">Embark on a seamless event planning journey with us. From conceptualization to execution, we're here to turn your vision into a memorable reality. Let's make your event extraordinary together!</p>
			<a href="/MakeBooking"><div class="section_action">Make Booking</div></a>
		</div>
	</div>

	<div class="section">
		<div class="image_holder">
			<img src="assets/event_catering_2.jpg">
		</div>

		<div class="section_content">
			<h2 class="section_header">What we offer?</h2>
			<p class="section_description">Experience a seamless blend of culinary excellence and impeccable service with our offering of catering staff and delectable food. From expertly crafted dishes to a professional team dedicated to ensuring your event's success, we bring a harmonious balance that elevates every occasion.</p>
			<a href="/MakeBooking" class="a_tag_btn section_action">Make Booking</a>

		</div>
	</div>

	<div class="section right_section">
		<div class="image_holder">
			<img src="assets/event_catering_1.jpg">
		</div>

		<div class="section_content">
			<h2 class="section_header">Have any questions?</h2>
			<p class="section_description">Curious minds, we're here for you! Whether you have inquiries about our services, event planning, or anything in between, feel free to ask on the <a style="color:teal;" href="/Contact">Contact us</a> page or view the FAQs by clicking the button below</p>
			<a href="/FAQ" class="a_tag_btn section_action">FAQs</a>
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
