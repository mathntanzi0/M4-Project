<%@ Page Title="Order" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="M4_Project.Admin.Order" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/sale_view_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    	<div class="secondary_header">
			<h1>Order #323</h1>
			<div class="header_input">
				<select id="select_order_type">
					<option>Pending</option>
					<option>Preparing</option>
					<option>Prepared</option>
					<option>Collected</option>
					<option>Rejected</option>
					<option hidden>Delivered</option>
					<option hidden>Unsuccessful</option>
					<option hidden>On the way</option>
				</select>
			</div>
		</div>
		<div class="items_container">
			<div class="item_wrapper">
				<div class="image_holder">
					<img src="assets/temp/eggs.jpg">
				</div>
				<div class="item_details">
					<h2>Name</h2>
					<h4>Price R 50.00</h4>
					<h4>Dessert</h4>
				</div>
				<div class="item_line_details">
					<h4>QTY 2</h4>
					<h4>Cost R100.00</h4>
				</div>
			</div>

			<div class="item_wrapper">
				<div class="image_holder">
					<img src="assets/temp/appletiser.jpg">
				</div>
				<div class="item_details">
					<h2>Appletiser</h2>
					<h4>Price R 50.00</h4>
					<h4>Dessert</h4>
				</div>
				<div class="item_line_details">
					<h4>QTY 2</h4>
					<h4>Cost R100.00</h4>
				</div>
			</div>
			<div class="item_wrapper">
				<div class="image_holder">
					<img src="assets/temp/oreo.jpg">
				</div>
				<div class="item_details">
					<h2>Oreo Milkshake</h2>
					<h4>Price R 50.00</h4>
					<h4>Dessert</h4>
				</div>
				<div class="item_line_details">
					<h4>QTY 2</h4>
					<h4>Cost R100.00</h4>
				</div>
			</div>
		</div>
		<div class="sale_summary_wrapper">
			<div class="detail_group">
				<h2>Payment Details</h2>
				<p>Payment method: Card</p>
				<p>Tip: R 68.50</p>
				<p>Total payment: R 373.50</p>
				<p>Payment date: 12 June 2023 10:24</p>
			</div>
			<div class="detail_group">
				<h2>Staff Responsible</h2>
				<p>Name: Thivar Kushor</p>
				<p>Staff No: #52</p>
				<p>Role: Manager</p>
				<p>Phone Number: +27 56 456 6464</p>
				
			</div>
			<div class="detail_group">
				<h2>Customer</h2>
				<p>Name: Grace Walker</p>
				<p>Email: grace@email.com</p>
				<p>Phone Number: +27 56 456 6464</p>
			</div>
		</div>
		<div style="padding-bottom: 1.175rem;" class="right_button_wrapper">
			<button class="red_button">Delete</button>
		</div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	
</asp:Content>