<%@ Page Title="Customer Queries" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CustomerQueries.aspx.cs" Inherits="M4_Project.Admin.Reports.CustomerQueries" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/message_view_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
		<div class="secondary_header">
			<h1>Customer Queries</h1>
			<div>
				<a class="a_tag_btn" href="/Admin/Reports/ErrorLog">Error Log</a>
				<a class="a_tag_btn" href="https://app.powerbi.com/links/zqxX64CQtz?ctid=226827d6-a9d0-470d-8c15-b146b0192d51&pbi_source=linkShare">Reports</a>
			</div>
			<asp:Button OnClientClick="return DeleteConfirmation();" runat="server" ID="btnDelete" Text="Clear All" style="background-color:var(--faint-red)" OnClick="btnDelete_Click" CssClass="delete_btn"/>
		</div>
		<div>
			<asp:Repeater ID="QueryRepeater" runat="server">
				<ItemTemplate>
					<div class="query_wrapper">

						<div class="query_header">
							<h2><%# Eval("Name") %> <span>~ <%# Eval("Email") %></span></h2>
							<h1><%# Eval("Date", "{0:dd MMMM yyyy}") %></h1>
						</div>
						<p><%# Eval("Query") %></p>
					</div>
				</ItemTemplate>
			</asp:Repeater>

			<% if (queryCount < 1) { %>
				<div id="empty_box">
					<h3>No Queries Found</h3>
					<p>No queries found on page <%= page %></p>
					<a href="/Admin/Reports/ErrorLog"><div>View Error Log</div></a>
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