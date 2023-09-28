<%@ Page Title="Customer" Language="C#" MasterPageFile="~/Admin/Admin.Master" CodeBehind="Customer.aspx.cs" Inherits="M4_Project.Admin.POS.Customer" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/add_item_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    	<div class="secondary_header">
			<h1>Add Customer</h1>
		</div>

        <div class="inline_input_fields">
            <div class="input_field input_item_left">
                <label>First Name</label>
                <asp:TextBox ID="txtFirstName" runat="server" placeholder="Enter customer's first name..."></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                    InitialValue="" Display="Dynamic" ErrorMessage="First Name is required." ForeColor="Red">*</asp:RequiredFieldValidator>
            </div>
            <div class="input_field input_item_right">
                <label>Last Name</label>
                <asp:TextBox ID="txtLastName" runat="server" placeholder="Enter customer's last name..."></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                    InitialValue="" Display="Dynamic" ErrorMessage="Last Name is required." ForeColor="Red">*</asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="inline_input_fields">
            <div class="input_field input_item_left">
                <label>Address</label>
                <asp:TextBox ID="txtAddress" runat="server" placeholder="Enter customer's address..."></asp:TextBox>
            </div>
            <div class="input_field input_item_right">
                <label>Email</label>
                <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter customer's email..."></asp:TextBox>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                    ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" 
                    ErrorMessage="Invalid email format." ForeColor="Red" Display="Dynamic">*</asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="inline_input_fields">
            <div class="input_field input_item_left">
                <label>Number</label>
                <asp:TextBox ID="txtNumber" runat="server" placeholder="Enter customer's number..."></asp:TextBox>
                <asp:RegularExpressionValidator ID="revNumber" runat="server" ControlToValidate="txtNumber"
                    ValidationExpression="^\d{10}$" ErrorMessage="Invalid phone number format (e.g., 1234567890)." 
                    ForeColor="Red" Display="Dynamic">*</asp:RegularExpressionValidator>
            </div>
        </div>
		

		<button ID="btnAddCustomer" runat="server" class="primary_button">Add Customer</button>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	
</asp:Content>