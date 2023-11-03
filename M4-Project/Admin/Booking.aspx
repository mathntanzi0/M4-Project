<%@ Page Title="Event" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="M4_Project.Admin.Booking" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Admin/Content/sale_view_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    	<div class="secondary_header">
			<h1>Event Booking #<%= booking.BookingID %></h1>
			<div class="header_input">
				<asp:DropDownList ID="select_event_status" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectEventStatus_SelectedIndexChanged">
					<asp:ListItem Selected="False" Text="Pending" Value="Pending" DisplayText="Pending" style="display:none;" />
				</asp:DropDownList>
			</div>
		</div>
		<div class="items_container">
			<asp:Repeater ID="ItemRepeater" runat="server" EnableViewState="false">
				<ItemTemplate>
					<div class="item_wrapper">
						<div class="image_holder">
							<img src="<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("Image")) %>">
						</div>
						<div class="item_details">
							<h2><%# Eval("ItemName") %></h2>
							<h4>Price  R <%# Eval("ItemCostN2") %></h4>
							<h4><%# Eval("ItemCategory") %></h4>
							<%# !string.IsNullOrEmpty(Eval("Instructions").ToString()) ? 
							"<h4>Instructions</h4><h4>" + Eval("Instructions") + "</h4>" : 
							"" %>
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
				<p>Payment method: <%= booking.PaymentMethod %></p>
				<p>Total payment: R <%= booking.PaymentAmount.ToString("N2") %></p>
				<p>Payment date: <%= booking.PaymentDate.ToString("dd MMMM yyyy HH:mm") %></p>
			</div>
			<div class="detail_group">
				<h2>Event Details</h2>
				<p><%= booking.EventAddress %></p>
				<p>Date:  <%= booking.EventDate.ToString("dd MMMM yyyy HH:mm") %></p>
				<p>Time: <%= booking.EventDate.ToString("HH:mm") %> - <%= booking.EventDate.Add(booking.EventDuration).ToString("HH:mm") %></p>
				
			</div>
			<div class="detail_group">
				<h2>Decor Description</h2>
				<p><%= booking.EventDecorDescription %></p>
			</div>
		</div>
		<a href="/Admin/Event/Staff?Event=<%= booking.BookingID %>" class="left_button green_button a_tag_btn">Event Staff</a>
		

		<div class="update_form_div">
			<div class="input_field">
                <label>Address</label>
                <input class="field" type="text" name="txtAddress" placeholder="Enter event address..." runat="server" ID="txtAddress" />
            </div>
            <div class="input_field">
                <label>Decor Description</label>
                <input class="field" type="text" placeholder="Enter description, eg. The event will be dark theme, No hat allowed..." runat="server" ID="txtDecorDescription">
            </div>
            <div class="input_field">
                <label>Date</label>
                <input class="field" type="date" runat="server" ID="datePicker"/>
            </div>
            <div class="input_field">
                <label>Duration</label>
                <select class="field" onchange="select_change(this)" runat="server" ID="ddlDuration">
                    <option value="00:30">00:30</option>
                    <option value="01:00">01:00</option>
                    <option value="01:30">01:30</option>
                    <option value="02:00">02:00</option>
                    <option value="02:30">02:30</option>
                    <option value="03:00">03:00</option>
                    <option value="03:30">03:30</option>
                    <option value="04:00">04:00</option>
                    <option value="04:30">04:30</option>
                </select>
            </div>
            <div class="input_field">
                <label>Time</label>
                <select class="small-field" runat="server" ID="ddlTimeHour">
                    <option value="6">06</option>
                    <option value="7">07</option>
                    <option value="8">08</option>
                    <option value="9">09</option>
                    <option value="10">10</option>
                    <option value="11">11</option>
                    <option value="12">12</option>
                    <option value="13">13</option>
                    <option value="14">14</option>
                    <option value="15">15</option>
                    <option value="16">16</option>
                    <option value="17">17</option>
                    <option value="18">18</option>
                    <option value="19">19</option>
                </select>
                <select class="small-field" runat="server" ID="ddlTimeMin">
                    <option value="0">00</option>
                    <option value="5">05</option>
                    <option value="10">10</option>
                    <option value="15">15</option>
                    <option value="20">20</option>
                    <option value="25">25</option>
                    <option value="30">30</option>
                    <option value="35">35</option>
                    <option value="40">40</option>
                    <option value="45">45</option>
                    <option value="50">50</option>
                    <option value="55">55</option>
                </select>
            </div>
			<asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="btnEdit_Click" />

		</div>
		<div class="overlay"></div>

        <div class="right_button_wrapper">
			<button id="showUpdateBoxBtn" class="green_button">Update Event Details</button>
		</div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	<script>
        $(".update_form_div, .overlay").hide();

        $(document).ready(function () {
            function showDivs() {
                $(".update_form_div, .overlay").show();
            }

            
            function hideDivs() {
                $(".update_form_div, .overlay").hide();
            }

            
            $("#showUpdateBoxBtn").click(function () {
                event.preventDefault();
                showDivs();
            });

            $(".overlay").click(function () {
                hideDivs();
            });
        });
    </script>

</asp:Content>