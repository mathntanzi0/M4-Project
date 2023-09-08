<%@ Page Title="Your Orders" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="M4_Project.Customer.Orders" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" type="text/css" href="Content/sells_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
		<div class="secondary_header">
			<h1>Orders</h1>
		</div>

		<p class="page_message">Your most recent orders</p>
		<div class="sell_card_container">
			<div class="sell_card">
				<div class="date_container">
					<h2>June <br> 21 <br> 2023 </h2>
				</div>
				<div class="sell_details">
					<h2 class="header_detail">Delivery:  4 Alexandra Rd, Pietermaritzburg, 3201 </h2>
					<h2 class="sell_status">Status: <span style="color:green;">Delivered</span></h2>
				</div>
				<div class="right_sell_details">
					<h2 class="sell_payment"><br></h2>

					<svg class="common_icon" xmlns="http://www.w3.org/2000/svg" height="48" viewBox="0 -960 960 960" width="48"><path d="m560-242-43-42 168-168H160v-60h525L516-681l43-42 241 241-240 240Z"/></svg>
					<h2 class="sell_payment">R 2 150.00</h2>
				</div>
			</div>
			<div class="sell_card">
				<div class="date_container">
					<h2>MAY <br> 05 <br> 2023 </h2>
				</div>
				<div class="sell_details">
					<h2 class="header_detail">Collection: No street, City, Postal code, County</h2>
					<h2 class="sell_status">Status: <span style="color:red;">Canceled</span></h2>
				</div>
				<div class="right_sell_details">
					<h2 class="sell_payment"><br></h2>

					<svg class="common_icon" xmlns="http://www.w3.org/2000/svg" height="48" viewBox="0 -960 960 960" width="48"><path d="m560-242-43-42 168-168H160v-60h525L516-681l43-42 241 241-240 240Z"/></svg>
					<h2 class="sell_payment">R 2 150.00</h2>
				</div>
			</div>
			<div class="sell_card">
				<div class="date_container">
					<h2>January <br> 30 <br> 2023 </h2>
				</div>
				<div class="sell_details">
					<h2 class="header_detail">Collection: No street, City, Postal code, County</h2>
					<h2 class="sell_status">Status: <span style="color:#F0E500;">Pending</span></h2>
				</div>
				<div class="right_sell_details">
					<h2 class="sell_payment"><br></h2>

					<svg class="common_icon" xmlns="http://www.w3.org/2000/svg" height="48" viewBox="0 -960 960 960" width="48"><path d="m560-242-43-42 168-168H160v-60h525L516-681l43-42 241 241-240 240Z"/></svg>
					<h2 class="sell_payment">R 2 150.00</h2>
				</div>
			</div>
		</div>
</asp:Content>
