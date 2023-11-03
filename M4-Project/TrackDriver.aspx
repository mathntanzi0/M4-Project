<%@ Page Title="Tracker" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrackDriver.aspx.cs" Inherits="M4_Project.TrackDriver" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/address_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="secondary_header">
		<h1 class="inner_body_title">Track</h1>
	</div>
  	<div class="address_details_wrapper">
  		<h3>Dirver Details</h3>
  		<h4>Name: <%= driver.FullName %></h4>
  		<h4>Phone number & WhatsApp: <%= driver.PhoneNumber %></h4>
  		<h4>Email: <%= driver.EmailAddress %></h4>
  	</div>
  	<div id="map"></div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	  <script type="text/javascript">

		let map;
		let marker;
		let dirver;

        let driverLocation = { lat: <%= M4_Project.Models.BusinessRules.Address.centerLatitude %>, lng: <%= M4_Project.Models.BusinessRules.Address.centerLongitude %> };
        let selectedLocation = { lat: <%= order.Delivery.DeliveryAddress.Latitude %>, lng: <%= order.Delivery.DeliveryAddress.Longitude %> };
		async function initMap() {
			const { Map } = await google.maps.importLibrary("maps");
			const initialLatLng = selectedLocation;
			map = new Map(document.getElementById("map"), {
            center: driverLocation,
			zoom: 18,
			streetViewControl: false,
			mapTypeControl: false,
			});

			marker = new google.maps.Marker({
			position: initialLatLng,
			map: map,
			title: 'You'
			});
			const image = {
				url: "Assets/logo.png",
				scaledSize: new google.maps.Size(50, 50)
			};
			dirver = new google.maps.Marker({
            position: driverLocation,
			map: map,
			title: 'Driver',
			icon: image,
			});
		}

		function updateMarkerPosition(newLat, newLng) {
			const newPosition = new google.maps.LatLng(newLat, newLng);
			dirver.setPosition(newPosition);
			map.setCenter(newPosition);
			selectedLocation = newPosition;
		}
		let orderID = <%= orderID  %>;
		function getDriverLocation() {
			console.log(orderID);
            $(document).ready(function () {  
				$.ajax({
					type: "POST",
					url: "TrackDriver.aspx/GetDriverLocation",
					data: JSON.stringify({ orderID: orderID }),
					contentType: "application/json; charset=utf-8",
					dataType: "json",
					success: function (response) {
						var driverLocation = response.d;

                        var locationParts = driverLocation.split(', ');
                        var driverLatitude = parseFloat(locationParts[0]);
                        var driverLongitude = parseFloat(locationParts[1]);

						if (driverLatitude == 1000)
						{
							location.replace("/TrackOrder");
						}
                        updateMarkerPosition(driverLatitude, driverLongitude);
					},
					error: function (xhr, ajaxOptions, thrownError) {
						console.log(xhr.status + ": " + thrownError);
					}
				});
            });
		  }


		  <% if (!M4_Project.Models.Sales.OrderState.IsFinalState(order.OrderStatus))
          { %>
		  setInterval(getDriverLocation, 5000);
		  <% } %>
      </script>
		<script async
    	src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCRXvY67uo2SVzezWtxsQeqmgh4IRkA7Ag&callback=initMap">
	</script>
</asp:Content>