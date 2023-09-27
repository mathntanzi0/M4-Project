<%@ Page Title="Events" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Bookings.aspx.cs" Inherits="M4_Project.Admin.Bookings" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/sales_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    	<div class="secondary_header">
			<h1>Events</h1>
			<div>
				<asp:TextBox ID="search_bar" runat="server" CssClass="search-input" placeholder="Search by customer or order number..." OnTextChanged="SearchTextBox_TextChanged"></asp:TextBox>
				<svg class="search_icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960">
					<path d="M796-121 533-384q-30 26-69.959 40.5T378-329q-108.162 0-183.081-75Q120-479 120-585t75-181q75-75 181.5-75t181 75Q632-691 632-584.85 632-542 618-502q-14 40-42 75l264 262-44 44ZM377-389q81.25 0 138.125-57.5T572-585q0-81-56.875-138.5T377-781q-82.083 0-139.542 57.5Q180-666 180-585t57.458 138.5Q294.917-389 377-389Z"/>
				</svg>
			</div>
			<asp:DropDownList ID="select_event_status" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectEventStatus_Changed">
				<asp:ListItem Text="Event Status" Value="" />
				<asp:ListItem Text="Pending" Value="Pending" />
				<asp:ListItem Text="Up coming" Value="Up coming" />
				<asp:ListItem Text="In progress" Value="In progress" />
				<asp:ListItem Text="Completed" Value="Completed" />
				<asp:ListItem Text="Canceled" Value="Canceled" />
				<asp:ListItem Text="Rejected" Value="Rejected" />
			</asp:DropDownList>
		</div>
		
		<% if (events.Count > 0)
            { %>
		<div class="secondary_table">
			<h2 class="role_label">Most Recent Events</h2>
			<table>
				<tr>
					<th>Customer</th>
				    <th>Booking Number</th>
				    <th>Event Date</th>
				    <th>Event Status</th>
				    <th>Cost</th>
					<th></th>
				</tr>
				<asp:Repeater ID="EventRepeater" runat="server" OnItemCommand="EventRepeater_ItemCommand">
					<ItemTemplate>
						<tr>
							<td><%# Eval("CustomerName") %></td>
							<td><%# Eval("BookingID") %></td>
							<td><%# Eval("Str_EventDate") %></td>
							<td><%# Eval("BookingStatus") %></td>
							<td><%# Eval("PaymentAmount", "R {0:F2}") %></td>
							<td>
								<asp:Button runat="server" Text="View Details" CommandName="ViewDetails" CommandArgument='<%# Eval("BookingID") %>' CssClass="accept_btn" />
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
			<h3>No Event Booking Found</h3>
			<p><%= p_NotFound %></p>
			<a href="/Admin/POS"><div>Point of Sale</div></a>
		</div>
		<% } %>

	<div class="page_nav_container">
		<% if (page > 1)
            { %>
		<asp:Button ID="btnPreviousPage" runat="server" Text="<<" OnClick="btnPreviousPage_Click" CssClass="page_box page_index_box" />
		<% } %>
			<div class="page_box"><%= page %></div>
		<% if (page < maxPage)
            { %>
		<asp:Button ID="btnNextPage" runat="server" Text=">>" OnClick="btnNextPage_Click" CssClass="page_box page_index_box" />
		<% } %>
	</div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	
</asp:Content>