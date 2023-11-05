<%@ Page Title="Error Logs" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ErrorLog.aspx.cs" Inherits="M4_Project.Admin.Reports.ErrorLog" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/message_view_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    <div class="secondary_header">
			<h1>Error Logs</h1>
			<div>
				<a class="a_tag_btn" href="/Admin/Reports/CustomerQueries">Customer Queries</a>
				<asp:Button OnClientClick="return DeleteConfirmation();" runat="server" ID="btnDelete" Text="Clear All" style="background-color:var(--faint-red)" OnClick="btnDelete_Click" CssClass="delete_btn"/>
			</div>
		</div>
		<div>
			
			<asp:Repeater ID="ErrorLogRepeater" runat="server">
				<ItemTemplate>
					<div class="query_wrapper">
						<div class="query_header">
							<h2><%# Eval("ErrorMessage") %></h2>
							<h1><%# Eval("LogTime", "{0:yyyy-MM-dd HH:mm:ss}") %></h1>
						</div>
						<p><%# Eval("StackTrace") %></p>
					</div>
				</ItemTemplate>
			</asp:Repeater>


			<% if (errorLogCount < 1) { %>
				<div id="empty_box">
					<h3>No Error Logs Found</h3>
					<p>No error logs found on page <%= page %></p>
					<a href="/Admin/Reports/CustomerQueries"><div>View Customer Queries</div></a>
				</div>
			<% } %>
		</div>

		<div class="page_nav_container">
			<asp:Button ID="btnPreviousPage" runat="server" Text="<<" OnClick="btnPreviousPage_Click" CssClass="page_box page_index_box" />
				<div class="page_box"><%= page %></div>
			<asp:Button ID="btnNextPage" runat="server" Text=">>" OnClick="btnNextPage_Click" CssClass="page_box page_index_box" />
		</div>

		<script>

        function DeleteConfirmation() {
            return confirm("Are you sure you want to delete? All queries will be permanently lost");
        }
        </script>
</asp:Content>