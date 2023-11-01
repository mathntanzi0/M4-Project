<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="StaffMember.aspx.cs" Inherits="M4_Project.Admin.StaffMember"%>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/staff_member_style.css">
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
				<p> <%= staffMember.Status %></p>
			</div>
		</div>
		<% if (loginStaff.StaffID != staffMember.StaffID)
            {%>
		<div class="right_top_button_wrapper">
			<% if (staffMember.Status != M4_Project.Models.StaffMemberState.Deactivated)
                { %>
			<a href="/Admin/Deactivate?Member=<%= staffMember.StaffID %>" class="delete_button a_tag_btn">Deactivate</a>
			<% }
                else
                { %>
				<asp:Button ID="btnActivate" runat="server" CssClass="delete_button" Text="Activate" OnClick="btnActivate_Click" style="background-color: var(--secondary-primary); color:var(--text-color)"/>
			<% } %>
		</div>
		<% } %>

	</div>

	<% if (!loginStaff.IsDriver()){ %>
	<div class="activities_detail_wrapper">
		<h2>Activities</h2>
		<p>Number of Orders managed: <%= staffMember.GetNumberOfOrders() %></p>
		<p>Number of events assigned to: <%= staffMember.GetNumberOfBookings() %></p>
		<div class="action_control_wrapper">
			<p>View activity details</p>
			<div class="action_controls">
				<a href="Orders?Staff=<%= staffMember.StaffID %>">Orders</a>
				<a href="Bookings?Staff=<%= staffMember.StaffID %>">Bookings</a>
			</div>
		</div>
	</div>
	<% } %>

	<div style="padding-bottom: 1.175rem;" class="right_button_wrapper">
		<asp:Button ID="EditButton" runat="server" Text="Edit" OnClick="EditButton_Click" CommandArgument="<%# staffMember.StaffID %>" CssClass="green_button"/>
		<asp:Button ID="LogoutButton" runat="server" Text="Logout" OnClick="LogoutButton_Click" CssClass="red_button"/>
	</div>
</asp:Content>
