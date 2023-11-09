﻿<%@ Page Title="Frequently Asked Questions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="M4_Project.FAQpageaspx" %>

    <asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Content/FAQStyle.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" integrity="sha512-z3gLpd7yknf1YoNbCzqRKc4qyor8gaKU1qmn+CShxbuBusANI9QpRohGBreCFkKxLhei6S9CQXFEbbKuqLg0DA==" crossorigin="anonymous" referrerpolicy="no-referrer" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="wrapper">
        <p>Need help?</p>
        <h1>Frequently Asked Questions</h1>
        <div class="name">
            Booking
        </div>
        <div class="faq">
            <div class="accordion">
                How to change the event booking details?
                <i class="fa-solid fa-chevron-down"></i>
            </div>
            <div class="pannel">
                <p>
                    You can chage your booking details on your event line click on <b>"Update booking details".</b>
                </p>
                <p><b>Please Note:</b> You can not change your details once you have paid.</p>
            </div>
        </div>

        <div class="faq">
            <div class="accordion">
                Where can I see the event booking details?
                <i class="fa-solid fa-chevron-down"></i>
            </div>
            <div class="pannel">
                <p>
                    Navigate to the top right corner and click your surname,then click <b>"Number of bookings"</b>, then click the booking you want to view.
                </p>
                <p>The details of your booking will be presented to you.</p>
            </div>
        </div>

        <div class="name">
            Order
        </div>

        <div class="faq">
            <div class="accordion">
                How to change the order?
                <i class="fa-solid fa-chevron-down"></i>
            </div>
            <div class="pannel">
                <p>
                    Its is what and what even
                    that comes in the middle of that.
                </p>
            </div>
        </div>

        <div class="faq">
            <div class="accordion">
                How to cancel an order?
                <i class="fa-solid fa-chevron-down"></i>
            </div>
            <div class="pannel">
                <p>
                    Its is what and what even
                    that comes in the middle of that.
                </p>
            </div>
        </div>

        <div class="faq">
            <div class="accordion">
                I cant find my adress for delivery?
                <i class="fa-solid fa-chevron-down"></i>
            </div>
            <div class="pannel">
                <p>
                    If you search for your place and can't find it.
                </p>
                <p>You must select the the adress manually from the provided map.</p>
            </div>
        </div>

        <div class="name">
            Account
        </div>

        <div class="faq">
            <div class="accordion">
                How to delete my account?
                <i class="fa-solid fa-chevron-down"></i>
            </div>
            <div class="pannel">
                <p>
                    Go to the top right corner of your screen and click on your name
                </p>
                <p>Then Navigate down to your account details and click <b>"Delete Account"</b></p>
            </div>
        </div>

        <div class="faq">
            <div class="accordion">
                What happens to my personal information?
                <i class="fa-solid fa-chevron-down"></i>
            </div>
            <div class="pannel">
                <p>
                    Your personal information will be removed from the database when you <b>"delete"</b> your account.
                </p>
            </div>
        </div>

        <div class="faq">
            <div class="accordion">
                Can I update my details?
                <i class="fa-solid fa-chevron-down"></i>
            </div>
            <div class="pannel">
                <p>
                    Yes you can update your details. To do so <b>follow the steps:</b>
                </p>
                <p>1. Click on your surname on the top rigth corner of the wesite.</p>
                <p>2. Scroll down and you will see your details listed.</p>
                <p>3. Click on the pen icon to update.</p>
            </div>
            <hr />
        </div>
    </div>



    <script>
        var acc = document.getElementsByClassName
            ("accordion");

        for (var i = 0; i < acc.length; i++) {
            acc[i].addEventListener("click", function () {
                this.classList.toggle("active");
                this.parentElement.classList.toggle("active");

                var pannel = this.nextElementSibling;

                if (pannel.style.display === "block") {
                    pannel.style.display = "none";
                } else {
                    pannel.style.display = "block";
                }
            });
        }
    </script>
</asp:Content>

