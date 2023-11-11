<%@ Page Title="Frequently Asked Questions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="M4_Project.FAQpageaspx" %>

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
                    To change event booking details, please contact our business directly. You can reach out to our customer support or the designated contact person to assist you with the necessary changes to your event booking. They will provide you with guidance on the process and ensure that any modifications are made according to your preferences. If you have any specific questions or requirements, feel free to let them know, and they'll be happy to help.
                </p>
            </div>
        </div>

        <div class="faq">
            <div class="accordion">
                How to cancel event booking?
                <i class="fa-solid fa-chevron-down"></i>
            </div>
            <div class="pannel">
                <p>
                    Contact Us: Reach out to our customer support or the designated contact person for event cancellations. You can find our contact information on our website or in the confirmation email you received when you made the booking.

                    Provide Booking Details: When contacting us, be ready to provide details about your event booking, such as the booking ID, your name, and the event details. This information will help us locate your booking in our system.

                    Confirmation of Cancellation: Once you've contacted us and the cancellation process is complete, you should receive a confirmation of the cancellation. This may be sent to you via email or another communication method.
                </p>
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
                   Once an order is placed, it cannot be altered or canceled. We strive to process orders efficiently to ensure timely delivery and service. If you have any concerns or specific issues with your order, please reach out to our customer support team for assistance. They will be happy to help resolve any issues within their capabilities. Thank you for your understanding.
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
                    Certainly! If you're having trouble canceling your order or need further assistance, please reach out to our customer support team. Keep in mind that while we'll do our best to accommodate your request, a refund is not guaranteed and will be subject to our cancellation policy. Our dedicated support staff will guide you through the process and provide any necessary information. Thank you for your understanding.
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