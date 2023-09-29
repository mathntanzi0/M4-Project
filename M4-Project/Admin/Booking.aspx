<%@ Page Title="Event" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="M4_Project.Admin.Booking" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/sale_view_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    	<div class="secondary_header">
			<h1>Event Booking #<%= booking.BookingID %></h1>
			<div class="header_input">
				<asp:DropDownList ID="select_event_status" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectEventStatus_SelectedIndexChanged">
					<asp:ListItem Selected="False" Text="Pending" Value="Pending" DisplayText="Pending" style="display:none;" />
				</asp:DropDownList>
			</div>
		</div>
		<div class="items_container">
			<asp:Repeater ID="ItemRepeater" runat="server" EnableViewState="false">
				<ItemTemplate>
					<div class="item_wrapper">
						<div class="image_holder">
							<img src="<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("Image")) %>">
						</div>
						<div class="item_details">
							<h2><%# Eval("ItemName") %></h2>
							<h4>Price  R <%# Eval("ItemCostN2") %></h4>
							<h4><%# Eval("ItemCategory") %></h4>
							<%# !string.IsNullOrEmpty(Eval("Instructions").ToString()) ? 
							"<h4>Instructions</h4><h4>" + Eval("Instructions") + "</h4>" : 
							"" %>
						</div>
						<div class="item_line_details">
							<h4>QTY <%# Eval("ItemQuantity") %></h4>
							<h4>Cost <%# Eval("TotalSubCostN2") %></h4>
						</div>
					</div>
				</ItemTemplate>
			</asp:Repeater>
		</div>
	<div class="sale_summary_wrapper">
			<div class="detail_group">
				<h2>Payment Details</h2>
				<p>Payment method: <%= booking.PaymentMethod %></p>
				<p>Total payment: R <%= booking.PaymentAmount.ToString("N2") %></p>
				<p>Payment date: <%= booking.PaymentDate.ToString("dd MMMM yyyy HH:mm") %></p>
			</div>
			<div class="detail_group">
				<h2>Event Details</h2>
				<p><%= booking.EventAddress %></p>
				<p>Date:  <%= booking.EventDate.ToString("dd MMMM yyyy HH:mm") %></p>
				<p>Time: <%= booking.EventDate.ToString("HH:mm") %> - <%= booking.EventDate.Add(booking.EventDuration).ToString("HH:mm") %></p>
				
			</div>
			<div class="detail_group">
				<h2>Decor Description</h2>
				<p><%= booking.EventDecorDescription %></p>
			</div>
		</div>
		<a href="/Admin/Event/Staff?Event=<%= booking.BookingID %>" class="left_button green_button a_tag_btn">Event Staff</a>
		<div class="right_button_wrapper">
			<button class="green_button">Edit</button>
			<button class="red_button">Delete</button>
		</div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	
</asp:Content>