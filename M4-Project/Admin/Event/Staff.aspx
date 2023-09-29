<%@ Page Title="Staff" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="M4_Project.Admin.Event.Staff" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" type="text/css" href="/Admin/Content/staff_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    <div class="secondary_header">
			<h1>Event Staff #<%= bookingID %></h1>
		</div>

		<% if (staff != null && staff.Count > 0)
            { %>
		<div>
			<table>
			  <tr>
			    <th>Name</th>
			    <th>Staff No.</th>
			    <th>Email</th>
			    <th>Phone number</th>
			    <th>Role</th>
			    <th></th>
			  </tr>
				<asp:Repeater ID="StaffRepeater" runat="server" OnItemCommand="StaffRepeater_ItemCommand">
				  <ItemTemplate>
					<tr>
						<td class="column_with_image">
							<img class="column_left_image" src="<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("StaffImage")) %>" alt="<%# Eval("FullName") %>">
							<%# Eval("FirstName") %>
						</td>
						<td>#<%# Eval("StaffID") %></td>
						<td><a href="mailto:<%# Eval("EmailAddress") %>"><%# Eval("EmailAddress") %></a></td>
						<td><%# Eval("PhoneNumber") %></td>
						<td><%# Eval("Role") %></td>
						<td>
							<asp:Button runat="server" Text="Remove" CommandName="Remove" OnClientClick="return confirmDelete();" CommandArgument='<%# Eval("StaffID") %>' CssClass="reject_btn" />
						</td>
					</tr>
				</ItemTemplate>
				</asp:Repeater>
			</table>
		</div>
	<% }
        else
        { %>

		<div id="empty_box">
			<h3>No Staff Member Found</h3>
			<p>Assign staff members to this booking</p>
			<a href="/Admin/Event/AssignStaff?Event=<%= bookingID %>"><div>Assign</div></a>
		</div>

	<% } %>
	<a href="/Admin/Event/AssignStaff?Event=<%= bookingID %>" class="primary_button a_tag_btn">Assign Staff</a>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
		<script type="text/javascript">
		function confirmDelete() {
			return confirm("Are you sure you want to remove this staff member?");
		}
        </script>
</asp:Content>