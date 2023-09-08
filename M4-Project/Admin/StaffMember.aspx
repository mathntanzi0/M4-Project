<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="StaffMember.aspx.cs" Inherits="M4_Project.Admin.StaffMember"%>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/staff_member_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    <div class="staff_detail_wrpper">
		<div class="staff_image_wrapper">
			<asp:Image ID="Image1" runat="server"/>			
		</div>
		<div class="staff_detail">
			<div class="upper_details">
				<h1><%= staffMember.FirstName + " " + staffMember.LastName %></h1>
				<p>#<%= staffMember.StaffID %></p>
			</div>

			<div class="lower_details">
				<a style="color: teal;"><%= staffMember.EmailAddress %></a>
				<p><%= staffMember.PhoneNumber %></p>
				<p><%= staffMember.Role %></p>
				<p>R <%= staffMember.PayRateN2 %> Hourly</p>
				<p> <%= staffMember.Gender %></p>
			</div>
		</div>
		<div class="right_button_wrapper">
			<button class="delete_button">Deactivate</button>
		</div>
	</div>

	<div class="activities_detail_wrapper">
		<h2>Activities</h2>
		<p>Number of Orders managed: <%= staffMember.GetNumberOfOrders() %></p>
		<p>Number of events assigned to: <%= staffMember.GetNumberOfBookings() %></p>
		<div class="action_control_wrapper">
			<p>View activity details</p>
			<div class="action_controls">
				<a href="#">Orders</a>
				<a href="#">Bookings</a>
			</div>
		</div>
	</div>
</asp:Content>
