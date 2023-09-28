<%@ Page Title="Staff" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="M4_Project.Admin.Staff" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" type="text/css" href="/Admin/Content/staff_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    <div class="secondary_header">
			<h1>Staff</h1>
			<div class="header_input">
				<div>
					<asp:TextBox ID="search_bar" runat="server" CssClass="search-input" placeholder="Search by name..." OnTextChanged="SearchTextBox_TextChanged"></asp:TextBox>
					<svg class="search_icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960">
						<path d="M796-121 533-384q-30 26-69.959 40.5T378-329q-108.162 0-183.081-75Q120-479 120-585t75-181q75-75 181.5-75t181 75Q632-691 632-584.85 632-542 618-502q-14 40-42 75l264 262-44 44ZM377-389q81.25 0 138.125-57.5T572-585q0-81-56.875-138.5T377-781q-82.083 0-139.542 57.5Q180-666 180-585t57.458 138.5Q294.917-389 377-389Z"/>
					</svg>
				</div>
				<asp:DropDownList ID="select_role" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectRole_Changed">
				</asp:DropDownList>
			</div>
		</div>

		<% if (staff.Count > 0)
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
				<asp:Repeater ID="StaffRepeater" runat="server" OnItemCommand="StaffRepeater_ItemCommand" EnableViewState="false">
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
							<asp:Button runat="server" Text="View Details" CommandName="ViewDetails" CommandArgument='<%# Eval("StaffID") %>' CssClass="accept_btn" />
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