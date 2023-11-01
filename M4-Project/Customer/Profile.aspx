<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="M4_Project.Customer.Profile" %>


<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" type="text/css" href="/Content/login.css">
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    

    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h2 style="text-transform:uppercase"><%: Page.Title %>.</h2>
                    <h4>Use a local account to log in.</h4>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="FirstName" CssClass="col-md-2 control-label">First Name</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="FirstName" CssClass="form-control" placeholder="Enter your first name..." MaxLength="256" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstName" CssClass="text-danger" ErrorMessage="The first name field is required." />
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="LastName" CssClass="col-md-2 control-label">Last Name</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="LastName" CssClass="form-control" placeholder="Enter your last name..." MaxLength="256" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName" CssClass="text-danger" ErrorMessage="The last name field is required." />
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="PhoneNumber" CssClass="col-md-2 control-label">Phone Number</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="PhoneNumber" CssClass="form-control" placeholder="Enter your phone number..." MaxLength="15" onkeyup="validatePhoneNumber(this)" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="PhoneNumber" CssClass="text-danger" ErrorMessage="The phone number field is required." />
                        </div>
                    </div>


                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="PhysicalAddress" CssClass="col-md-2 control-label">Physical Address</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="PhysicalAddress" CssClass="form-control" placeholder="Enter your physical address..." MaxLength="256" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="PhysicalAddress" CssClass="text-danger" ErrorMessage="The physical address field is required." />
                            <asp:CustomValidator runat="server" ControlToValidate="PhysicalAddress" CssClass="text-danger" 
                                OnServerValidate="ValidatePhysicalAddress" 
                                ErrorMessage="Please enter a valid physical address." />
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" Text="Create" OnClick="CreateCustomer" CssClass="btn btn-default" />
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript">
        function validatePhoneNumber(input) {
            // Define a regular expression to allow only digits and optional hyphens or spaces
            var pattern = /^[0-9- ]+$/;

            // Get the input value
            var phoneNumber = input.value;

            // Check if the input matches the pattern
            if (!pattern.test(phoneNumber)) {
                // If the input doesn't match, remove invalid characters
                input.value = phoneNumber.replace(/[^0-9- ]/g, '');
            }
        }
    </script>
        <script>
            let initialLatLng = { lat: <%= M4_Project.Models.BusinessRules.Address.centerLatitude %>, lng: <%= M4_Project.Models.BusinessRules.Address.centerLongitude %> };

    let autocomplete;

    function autocompleteAddress() {
        const input = document.getElementById('<%= PhysicalAddress.ClientID %>');
                const options = {
                    componentRestrictions: { country: "ZA" },
                    fields: ["address_components", "geometry", "icon", "name"],
                    strictBounds: false,
                };
                autocomplete = new google.maps.places.Autocomplete(input, options);

                autocomplete.addListener('place_changed', function () {
                    const place = autocomplete.getPlace();
                    console.log(place);
                });
            }
        </script>
    <script async src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCRXvY67uo2SVzezWtxsQeqmgh4IRkA7Ag&libraries=places&callback=autocompleteAddress"></script>
</asp:Content>