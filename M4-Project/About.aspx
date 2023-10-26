<%@ Page Title="About - Family & Friends" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="M4_Project.About" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
      <link rel="stylesheet" type="text/css" href="Content/About_us_style.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.1/css/all.min.css" integrity="sha512-KfkfwYDsLkIlwQp6LFnl8zNdLGxu9YAA1QvwINks4PhcElQSvqcyVLLD9aMhXd13uQjoXtEKNosOWaZqXgel0g==" crossorigin="anonymous" referrerpolicy="no-referrer" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="Why_Us">
        <h1>About<span>Us</span></h1>
    </div>
    <div class="text">
        <h3>Why Choose us?</h3>
        <p>Friends and Family stands out as the optimal choice for those seeking a wholesome fast food alternative. In the midst of hectic student life, our offerings not only provide a convenient solution but also a nourishing one. We understand the challenges of day-to-day schedules and aim to cater to the needs of students by offering a diverse and affordable menu. Our commitment to quality ingredients and a balanced approach to fast food sets us apart, ensuring that every bite is not only delicious but also contributes to the well-being of our valued customers. With Friends and Family, savoring a quick and healthy meal becomes an accessible and delightful part of your daily routine. Choose us for a satisfying blend of convenience, affordability, and nutritional goodness.</p>
    </div>

    <div class="team">
        <h1>Meet Our<span>Team</span></h1>

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
    <div class="text">
        <h3>Mission and Values: Fueling the Essence of Friends and Family</h3>
        <p>At Friends and Family, our mission is simple yet powerful: to provide a haven for those seeking a wholesome and delightful fast food experience. Our values are the compass guiding every decision, action, and flavor we craft. We believe in transparency, quality, and community engagement, striving to redefine the fast food landscape.

        Our commitment to sourcing fresh, locally sourced ingredients reflects not only our dedication to flavor but also our responsibility to the environment. Every meal we serve is a testament to our mission of offering a healthier alternative without compromising on taste.

        Our values extend beyond our kitchen to the heart of the community. We actively engage in initiatives that support local causes, ensuring that every bite at Friends and Family contributes to the well-being of both our patrons and our neighborhoods.

        Join us on this culinary journey where our mission and values intertwine to create a space where fast food isn't just a convenience—it's an experience, a choice, and a celebration of good food shared among friends and family.</p>
    </div>
</asp:Content>