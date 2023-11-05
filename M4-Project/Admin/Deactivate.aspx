<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Deactivate.aspx.cs" Inherits="M4_Project.Admin.Deactivate" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/staff_deactivate_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
        <div class="secondary_header">
			<h1>Staff Deactivation</h1>
		</div>
		<div class="deactivation_panel">
			<div class="staff_detail_wrpper">
				<asp:Image runat="server" ID="StaffImage" />
				<h2><%= staffMember.FullName %></h2>
				<h4>#<%= staffMember.StaffID %></h4>
			</div>

			<div class="deactivation_action_wrapper">
                <div class="deactivation_action">
                    <div class="action_description">
                        <h3>Temporary Deactivation</h3>
                        <p>Staff member will not be able to access the system.</p>
                    </div>
                    <asp:Button ID="btnDeactivate" runat="server" CssClass="action_control action_control_minor" Text="Deactivate" OnClientClick="return confirm('Are you sure you want to deactivate this staff member?');" OnClick="btnDeactivate_Click" />
                </div>
                <div class="deactivation_action">
                    <div class="action_description">
                        <h3>Permanent Deletion</h3>
                        <p>Staff member's personal information will be deleted and associated information.</p>
                    </div>
                    <asp:Button ID="btnDelete" runat="server" CssClass="action_control" Text="Delete" OnClientClick="return confirmDelete();" OnClick="btnDelete_Click" />
                </div>
            </div>

		</div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript">
        function confirmDelete() {
            return confirm('Are you sure you want to permanently delete this staff member?') && confirm('All information associated to the staff member will be PERMANENTLY lost');
        }
    </script>
</asp:Content>