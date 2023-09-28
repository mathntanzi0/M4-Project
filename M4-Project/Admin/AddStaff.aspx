<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStaff.aspx.cs" Inherits="M4_Project.Admin.AddStaff" %>
<%@ Page Title="Add Item" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddStaff.aspx.cs" Inherits="M4_Project.Admin.AddStaff" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/add_item_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
		<div class="secondary_header">
            <% if (Request.QueryString["Item"] == null || !int.TryParse(Request.QueryString["Item"], out int itemID0))
                { %>
			<h1>Add Item</h1>
            <% }
                else
                { %>
			<h1>Edit Item</h1>
            <% } %>
		</div>

		<div class="item_image_wrapper">
			<div id="preview" onclick="clickFileControl()">
				<asp:Image ID="imgImage" runat="server" ImageUrl="/Assets/imagesmode.svg" />
			</div>
			<label>Click Image</label>
		</div>
		<asp:FileUpload style="display:none" ID="fileUploadControl" runat="server" accept=".jpg, .jpeg, .png, .gif" onchange="uploadFile()"/>

		<div class="inline_input_fields">
            <div class="input_field input_item_left">
                <label>Menu Item Name:</label>
                <asp:TextBox ID="txtItemName" runat="server" placeholder="Enter item name..."></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ControlToValidate="txtItemName" InitialValue=""
                    ErrorMessage="Item Name is required." Display="Dynamic" ForeColor="Red" />
            </div>

            <div class="input_field input_item_right">
                <label>Item Price:</label>
                <asp:TextBox ID="txtItemPrice" runat="server" placeholder="Enter item price..."></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvItemPrice" runat="server" ControlToValidate="txtItemPrice" InitialValue=""
                    ErrorMessage="Item Price is required." Display="Dynamic" ForeColor="Red" />
                <asp:RegularExpressionValidator ID="revItemPrice" runat="server" ControlToValidate="txtItemPrice"
                    ValidationExpression="^\d+(\.\d{1,2})?$" ErrorMessage="Invalid item price format. Use numbers with up to 2 decimal places."
                    Display="Dynamic" ForeColor="Red" />
            </div>
        </div>
        <div class="inline_input_fields">
            <div class="input_field input_item_left">
                <label>Item Description:</label>
                <asp:TextBox ID="txtItemDescription" runat="server" placeholder="Enter item description..." MaxLength="128"></asp:TextBox>
            </div>

            <div class="input_item_category input_field input_item_right">
                <label>Item Category:</label>
                <asp:DropDownList ID="selectCategory" runat="server" onchange="select_change(this)">
                </asp:DropDownList>
                <asp:TextBox ID="txtNewCategory" runat="server" placeholder="Enter item category..."></asp:TextBox>
            </div>
        </div>
                <% if (Request.QueryString["Item"] == null || !int.TryParse(Request.QueryString["Item"], out int itemID1))
                { %>
			<button class="primary_button" runat="server" onserverclick="btnAddItem_Click">Add Item</button>
            <% }
                else
                { %>
			<button class="primary_button" runat="server" onserverclick="btnAddItem_Click">Edit</button>
            <% } %>
        
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


		var input_new_category = document.getElementById('<%= txtNewCategory.ClientID %>');
        function select_change(select) {
            console.log("Here");
            console.log(select.value);
			if (select.value === "newOption")
				input_new_category.style.visibility = "visible";
			else
				input_new_category.style.visibility = "hidden";
		}
    </script>
</asp:Content>