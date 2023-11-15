<%@ Page Title="Your Bookings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bookings.aspx.cs" Inherits="M4_Project.Customer.Bookings" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" type="text/css" href="Content/sells_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
		<div class="secondary_header">
			<h1>Event Bookings</h1>
		</div>

		<p class="page_message">Your most recent event bookings</p>
		<div class="sell_card_container">
			<% if (bookings.Count < 1) { %>
			<div id="empty_box">
				<h3>No Events Found</h3>
				<p>Once you have made an event booking - it will appear here.</p>
				<a href="/MakeBooking"><div>Make Booking</div></a>
			</div>
			<% } %>
			<asp:Repeater ID="bookingRepeater" runat="server">
				<ItemTemplate>
					<a href="Booking?Event=<%# Eval("BookingID") %>">
						<div class="sell_card">
							<div class="date_container">
								<h2>
									<%# Eval("EventDate", "{0:MMMM}") %> <br>
									<%# Eval("EventDate", "{0:dd}") %> <br>
									<%# Eval("EventDate", "{0:yyyy}") %>
								</h2>
							</div>
							<div class="sell_details">
								<h2 class="header_detail">Event Address: <%# Eval("EventAddress") %></h2>
								<h2 class="sell_status">Status: <span style='color:<%# GetBookingStatusColor(Eval("BookingStatus")) %>'><%# Eval("BookingStatus") %></span></h2>
							</div>
							<div class="right_sell_details">
								<h2 class="sell_payment"><br></h2>

								<svg class="common_icon" xmlns="http://www.w3.org/2000/svg" height="48" viewBox="0 -960 960 960" width="48"><path d="m560-242-43-42 168-168H160v-60h525L516-681l43-42 241 241-240 240Z"/></svg>
								<h2 class="sell_payment">R <%# Eval("PaymentAmount", "{0:0.00}") %></h2>
							</div>
						</div>
					</a>
				</ItemTemplate>
			</asp:Repeater>
		</div>
</asp:Content>
