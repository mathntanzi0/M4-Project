<%@ Page Title="About - Family & Friends" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="M4_Project.About" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
      <link rel="stylesheet" type="text/css" href="Content/About_us_style.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.1/css/all.min.css" integrity="sha512-KfkfwYDsLkIlwQp6LFnl8zNdLGxu9YAA1QvwINks4PhcElQSvqcyVLLD9aMhXd13uQjoXtEKNosOWaZqXgel0g==" crossorigin="anonymous" referrerpolicy="no-referrer" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="team">
        <h1>Our<span>Team</span></h1>

        <div class="team_box">
            <div class="profile">
                <img src="Assets/chef1.png">
                <div class="info">
                    <h2 class="name">Manager</h2>
                    <p class="bio">Thapelo Molefe</p>
                </div>
            </div>

            <div class="profile">
                <img src="Assets/chef2.png">
                <div class="info">
                    <h2 class="name">Chef</h2>
                    <p class="bio">Leroy Smith</p>
                </div>
            </div>

            <div class="profile">
                <img src="Assets/chef3.jpg">
                <div class="info">
                    <h2 class="name">Chef</h2>
                    <p class="bio">Tyra Harris</p>
                </div>
            </div>

            <div class="profile">
                <img src="Assets/chef4.jpg">
                <div class="info">
                    <h2 class="name">Cashier</h2>
                    <p class="bio">Sandile Zungu</p>
                </div>
            </div>
        </div>
    </div>
    <div class="Why_Us">
        <h1>About<span>Us</span></h1>
        <h3>Why Choose us?</h3>
    </div>
    <div class="text">
        <p>Friends and family provide a healthy fast food alternative to students busy with their day-to-day lives at an affordable price.</p>
    </div>
</asp:Content>