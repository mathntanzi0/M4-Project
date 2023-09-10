<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="M4_Project.Contact" %>



<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css"/>
    <link rel="stylesheet" href="Content/contact_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="secondary_header">
		<h1 class="inner_body_title">Contact Us</h1>
	</div>
    <div class="container">
        <div class="content">
            <div class="left-side">
            <div class="address details">
                <i class="fas fa-map-marker-alt"></i>
                <div class="topic">Address</div>
                <div class="text-one">7 Field Road</div>
                <div class="text-two">Hayfields, 3201</div>
            </div>
            <div class="phone details">
                <i class="fas fa-phone-alt"></i>
                <div class="topic">Phone</div>
                <div class="text-one">+27 81 501 9589</div>
            </div>
            <!-- <div class="email details">
                <i class="fas fa-envelope"></i>
                <div class="topic">Email</div>
                <div class="text-one">codinglab@gmail.com</div>
                <div class="text-two">info.codinglab@gmail.com</div>
            </div> -->
            </div>
            <div class="right-side">
                <div class="topic-text">Send us a message</div>
                <p>If you wish to make a booking or have any quries regarding your order, you can send us a message here. It's our pleasure to help you.</p>
                <div class="input-box">
                    <input type="text" placeholder="Enter your name">
                </div>
                <div class="input-box">
                    <input type="text" placeholder="Enter your email">
                </div>
                <div class="input-box message-box">
                    <textarea placeholder="Type your query here"></textarea>
                </div>
                <div class="button">
                    <input type="button" value="Send Now" >
                </div>
            </div>
        </div>
    </div>
</asp:Content>
