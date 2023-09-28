<%@ Page Title="Make Booking" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="MakeBooking.aspx.cs" Inherits="M4_Project.Admin.POS.MakeBooking" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" type="text/css" href="/Content/make_booking_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
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
		</div>
		<button class="next_btn" id="btnAddToCart" runat="server" type="submit" OnServerClick="btnNext_Click">Next</button>

    <script type="text/javascript">
        document.getElementById('<%= datePicker.ClientID %>').addEventListener('change', function () {
            var selectedDate = new Date(this.value);
            var unavailableDates = <%= GetUnavailableDatesAsJavaScriptArray() %>;

                if (unavailableDates.includes(selectedDate.toISOString().split('T')[0])) {
                    alert('Selected date is unavailable.');
                this.value = '';
            }
        });
    </script>
</asp:Content>
