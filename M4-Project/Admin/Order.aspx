<%@ Page Title="Order" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="M4_Project.Admin.Order" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/sale_view_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    	<div class="secondary_header">
			<h1>Order <%=order.OrderID %></h1>
			<div class="header_input">
				<asp:DropDownList ID="select_order_type" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectOrderType_SelectedIndexChanged">
					<asp:ListItem Enabled="false" Text="Pending" Value="Pending" DisplayText="Pending" />
					<asp:ListItem Enabled="false" Text="On the way" Value="On the way" DisplayText="On the way" />
				</asp:DropDownList>
			</div>
		</div>
		<div class="items_container">
			<asp:Repeater ID="ItemRepeater" runat="server">
				<ItemTemplate>
					<div class="item_wrapper">
						<div class="image_holder">
							<img src="<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("Image")) %>">
						</div>
						<div class="item_details">
							<h2><%# Eval("ItemName") %></h2>
							<h4>Price  R <%# Eval("ItemCostN2") %></h4>
							<h4><%# Eval("ItemCategory") %></h4>
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
				<p>Payment method: <%= order.PaymentMethod %></p>
				<p>Tip: R <%= order.TipN2 %></p>
				<p>Total payment: R <%= order.TotalAmountDueN2 %></p>
				<p>Payment date: <%= order.PaymentDate.ToString("dd MMMM yyyy HH:mm") %></p>
				<p>Order Type: <%= order.OrderType %></p>
			</div>
			<div class="detail_group">
				<h2>Staff Responsible</h2>
				<% if (staffMember == null)
                    {%>
				<p>None</p>
				<% }
                    else
                    { %>
				<p>Name: <%= staffMember.FirstName + " " + staffMember.LastName %></p>
				<p>Staff No: #<%= staffMember.StaffID  %></p>
				<p>Role: <%= staffMember.Role %></p>
				<p>Phone Number: <%= staffMember.PhoneNumber %></p>
				<p>Email: <%= staffMember.EmailAddress %></p>
				<% } %>
			</div>
			<div class="detail_group">
				<h2>Customer</h2>
				<% if (customer == null)
                    {%>
				<p>None</p>
				<% }
                    else
                    { %>
				<p>Name: <%= customer.FullName %></p>
				<p>Email: <%= customer.EmailAddress %></p>
				<p>Phone Number: <%= customer.PhoneNumber %></p>
				<% } %>
			</div>
		</div>
		<div style="padding-bottom: 1.175rem;" class="right_button_wrapper">
			<button class="red_button">Delete</button>
		</div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	
</asp:Content>