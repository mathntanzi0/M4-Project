<%@ Page Title="Add Staff Member" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"  CodeBehind="AddStaff.aspx.cs" Inherits="M4_Project.Admin.AddStaff" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/add_item_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    <div class="secondary_header">
            <% if (Request.QueryString["Member"] == null || !int.TryParse(Request.QueryString["Member"], out int itemID0))
                { %>
			<h1>Add Staff Member</h1>
            <% }
                else
                { %>
			<h1>Edit Staff Member</h1>
            <% } %>
			
		</div>

		<div class="item_image_wrapper">
			<div id="preview" onclick="clickFileControl()">
				<asp:Image ID="imgImage" runat="server" ImageUrl="/Assets/imagesmode.svg" />
			</div>
			<label>Click Image</label>
		</div>
    <asp:FileUpload ID="fileUploadControl" runat="server" style="display: none" Accept=".jpg, .jpeg, .png, .gif" onchange="uploadFile()" />

    <div class="inline_input_fields">
        <div class="input_field input_item_left">
            <label>First Name</label>
            <asp:TextBox ID="txtFirstName" runat="server" placeholder="Enter first name..."></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" InitialValue="" ErrorMessage="First Name is required." Display="Dynamic" ForeColor="Red" />
        </div>

        <div class="input_field input_item_right">
            <label>Last Name</label>
            <asp:TextBox ID="txtLastName" runat="server" placeholder="Enter last name..."></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" InitialValue="" ErrorMessage="Last Name is required." Display="Dynamic" ForeColor="Red" />
        </div>
    </div>

    <div class="inline_input_fields">
        <div class="input_field input_item_left">
            <label>Email</label>
            <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter email..."></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" InitialValue="" ErrorMessage="Email is required." Display="Dynamic" ForeColor="Red" />
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid email format." Display="Dynamic" ForeColor="Red" />
        </div>

        <div class="input_field input_item_right">
            <label>Phone Number</label>
            <asp:TextBox ID="txtPhoneNumber" runat="server" placeholder="Enter phone number..."></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" ControlToValidate="txtPhoneNumber" InitialValue="" ErrorMessage="Phone Number is required." Display="Dynamic" ForeColor="Red" />
        </div>
    </div>

    <div class="inline_input_fields">
        <div class="input_field input_item_left">
            <label>Pay Rate</label>
            <asp:TextBox ID="txtPayRate" runat="server" placeholder="Enter pay rate per hour..."></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPayRate" runat="server" ControlToValidate="txtPayRate" InitialValue="" ErrorMessage="Pay Rate is required." Display="Dynamic" ForeColor="Red" />
            <asp:RegularExpressionValidator ID="revPayRate" runat="server" ControlToValidate="txtPayRate" ValidationExpression="^\d{1,5}(\.\d{1,2})?$" ErrorMessage="Invalid pay rate format (e.g., 10.50)." Display="Dynamic" ForeColor="Red" />
        </div>

        <div class="input_item_category input_field input_item_right">
            <label>Staff Role</label>
            <asp:DropDownList ID="ddlStaffRole" runat="server" onchange="role_select_change(this)">
                <asp:ListItem Text="Manager" Value="Manager"></asp:ListItem>
                <asp:ListItem Text="Casheir" Value="Casheir"></asp:ListItem>
                <asp:ListItem Text="Cater" Value="Cater"></asp:ListItem>
                <asp:ListItem Text="New Role" Value="newOption"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtNewStaffRole" runat="server" placeholder="Enter new staff role..."></asp:TextBox>
        </div>
    </div>

    <div class="inline_input_fields">
        <div class="input_item_category input_field input_item_left">
            <label>Gender</label>
            <asp:DropDownList ID="ddlGender" runat="server" onchange="gender_select_change(this)">
                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                <asp:ListItem Text="Prefer not to say" Value="Prefer not to say"></asp:ListItem>
                <asp:ListItem Text="Custom" Value="newOption"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtGender" runat="server" placeholder="Enter gender..."></asp:TextBox>
        </div>
    </div><br /><br />
    <asp:Button ID="btnAddStaff" runat="server" Text="Add Staff" OnClick="btnAddStaff_Click" CssClass="primary_button" />
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	<script type="text/javascript">
		function clickFileControl() {
			const fileInput = document.getElementById("<%=fileUploadControl.ClientID%>");
			fileInput.click();
        }
        function uploadFile() {
            const fileInput = document.getElementById("<%=fileUploadControl.ClientID%>");
            const preview = document.getElementById("preview");


			if (fileInput.files.length === 0) {
                console.log("Here");

                alert("Please select a file to upload.");
                return;
			}

			console.log("Here");

            const file = fileInput.files[0];
            const reader = new FileReader();

            reader.onload = function (e) {
                const img = new Image();
                img.src = e.target.result;
                img.alt = "Uploaded Image";
                preview.innerHTML = "";
                preview.appendChild(img);
            };

            reader.readAsDataURL(file);
		}

        function role_select_change(select) {
            var input_field = document.getElementById('<%= txtNewStaffRole.ClientID %>');
            select_input_field_change(select, input_field);
        }

        function gender_select_change(select) {
            var input_field = document.getElementById('<%= txtGender.ClientID %>');
            select_input_field_change(select, input_field);
        }

        function select_input_field_change(select, input_field) {
            if (select.value === "newOption")
                input_field.style.visibility = "visible";
            else
                input_field.style.visibility = "hidden";
        }
    </script>
</asp:Content>