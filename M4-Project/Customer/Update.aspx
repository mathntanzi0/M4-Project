<%@ Page Title="Update Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="M4_Project.Customer.Update" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
	<link rel="stylesheet" type="text/css" href="/Content/login.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <div>
        <section>
            <asp:PlaceHolder runat="server" ID="changePasswordHolder">
                <div class="form-horizontal">
                    <h4>Update Personal Details Form</h4>
                    <hr />
                    <div class="form-group">
                        <asp:Label runat="server" ID="FirstNameLabel" AssociatedControlID="FirstName" CssClass="col-md-2 control-label">First Name</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="FirstName" CssClass="form-control" PlaceHolder="Enter first name..." /><br />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstName"
                                CssClass="text-danger" ErrorMessage="First name is required"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="LastNameLabel" AssociatedControlID="LastName" CssClass="col-md-2 control-label">Last Name</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="LastName" CssClass="form-control" PlaceHolder="Enter last name..." /><br />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName"
                                CssClass="text-danger" ErrorMessage="Last name is required"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="PhoneNumberLabel" AssociatedControlID="PhoneNumber" CssClass="col-md-2 control-label">Phone Number</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="PhoneNumber" CssClass="form-control" PlaceHolder="Enter phone number (e.g 0123456789)" /><br />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="PhoneNumber"
                                CssClass="text-danger" ErrorMessage="Phone number is required"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="AddressLabel" AssociatedControlID="Address" CssClass="col-md-2 control-label">Physical Address</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Address" CssClass="form-control" PlaceHolder="Type Address..."/><br />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" Text="Update" CssClass="btn btn-default" OnClick="UpdateButton_Click" />
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>
        </section>
    </div>
    <script>
        let initialLatLng = { lat: <%= M4_Project.Models.BusinessRules.Address.centerLatitude %>, lng: <%= M4_Project.Models.BusinessRules.Address.centerLongitude %> };

    let autocomplete;

    function autocompleteAddress() {
        const input = document.getElementById('<%= Address.ClientID %>');
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
