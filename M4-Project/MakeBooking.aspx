<%@ Page Title="Make Booking" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MakeBooking.aspx.cs" Inherits="M4_System.MakeBooking" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" type="text/css" href="Content/make_booking_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    	<div class="secondary_header">
			<h1>Event Details</h1>
		</div>
		<div class="input_field_wrapper">
            <div class="input_field">
                <label>Address</label>
                <input class="field" type="text" name="txtAddress" placeholder="Enter event address..." runat="server" ID="txtAddress" />
            </div>
            <div class="input_field">
                <label>Decor Description</label>
                <textarea class="field" placeholder="Enter description, eg. The event will be dark theme, No hat allowed..." runat="server" ID="txtDecorDescription"></textarea>
            </div>
            <div class="input_field">
                <label>Date</label>
                <input class="field" type="date" min="2023-08-01" max="2023-08-31" runat="server" ID="datePicker" />
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
		</div>
		<button class="next_btn" id="btnAddToCart" runat="server" type="submit" OnServerClick="btnNext_Click">Next</button>
</asp:Content>
