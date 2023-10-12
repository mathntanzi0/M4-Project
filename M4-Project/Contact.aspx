<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="M4_Project.Contact" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css" />
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
            </div>
            <div class="right-side">
                <div class="topic-text">Send us a message</div>
                <p>If you wish to make a booking or have any queries regarding your order, you can send us a message here. It's our pleasure to help you.</p>
                <div class="input-box">
                    <asp:TextBox runat="server" ID="txtName" placeholder="Enter your name"></asp:TextBox>
                </div>
                <div class="input-box">
                    <asp:TextBox runat="server" ID="txtEmail" placeholder="Enter your Email"></asp:TextBox>
                </div>
                <div class="input-box message-box">
                    <asp:TextBox runat="server" ID="txtQuery" TextMode="MultiLine" placeholder="Type your query here"></asp:TextBox>
                </div>
                <div class="button">
                    <asp:Button ID="SendNowButton" runat="server" Text="Send Now" OnClientClick="return validateForm();" OnClick="SendNowButton_Click" CssClass="custom-button" />
                </div>
            </div>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script type="text/javascript">
        function validateForm() {
            var name = document.getElementById('<%= txtName.ClientID %>').value;
    var email = document.getElementById('<%= txtEmail.ClientID %>').value;
            var query = document.getElementById('<%= txtQuery.ClientID %>').value;

            if (name !== "" && email !== "" && query !== "") {
                // Display SweetAlert success message
                Swal.fire({
                    icon: 'success',
                    title: 'Thank You!',
                    text: 'Your form has been submitted successfully.',
                    showConfirmButton: true
                }).then((result) => {
                    // If the user clicks "OK," submit the form
                    if (result.isConfirmed) {
                        __doPostBack('<%= SendNowButton.UniqueID %>', ''); // Replace 'SendNowButton' with the actual ID of your button
             }
                });

                return false; // Prevent the form from being submitted immediately
            } else {
                // Display SweetAlert error message
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Please fill in all the required fields!',
                    showConfirmButton: true
                });

                return false;
            }
        }

    </script>
</asp:Content>
