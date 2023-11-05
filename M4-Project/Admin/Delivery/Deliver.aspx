<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Deliver.aspx.cs" Inherits="M4_Project.Admin.Delivery.Deliver" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/address_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
	<div class="secondary_header">
		<h1 class="inner_body_title">Track</h1>
	</div>
  	<div id="map"></div>
    <div class="address_details_wrapper">
  		<h3>Customer Details</h3>
  		<h4>Name: <%= order.Customer.FirstName %></h4>
  		<h4>Phone number: <%= order.Customer.PhoneNumber %></h4>
  		<h4>Email: <%= order.Customer.EmailAddress %></h4>
  		<h4>Location: <%= order.Delivery.DeliveryAddress.AddressName %></h4>
  	</div>

    <asp:Button ID="btnDelivered" runat="server" Text="Delivered" OnClick="btnDelivered_Click" CssClass="mainBtn" />
    <asp:Button ID="btnUnsuccessful" runat="server" Text="Unsuccessful" OnClick="btnUnsuccessful_Click" CssClass="mainBtn" style="background-color:var(--faint-red)"/>
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
                  <% if (Session["DarkModeEnabled"] != null && Convert.ToBoolean(Session["DarkModeEnabled"])) { %>
                  styles: darkModeStyles
                  <% } %>
              });

              marker = new google.maps.Marker({
                  position: initialLatLng,
                  map: map,
                  title: 'Customer'
              });
              const image = {
                  url: "/Assets/logo.png",
                  scaledSize: new google.maps.Size(50, 50)
              };
              dirver = new google.maps.Marker({
                  position: driverLocation,
                  map: map,
                  title: 'Driver',
                  icon: image,
              });
          }



          let id;
          let target;
          let options;

          function success(pos) {
              const crd = pos.coords;
              updateMarkerPosition(crd.latitude, crd.longitude);
          }

          function error(err) {
              console.error(`ERROR(${err.code}): ${err.message}`);
          }

          target = {
              latitude: 0,
              longitude: 0,
          };

          options = {
              enableHighAccuracy: false,
              timeout: 5000,
              maximumAge: 0,
          };

          id = navigator.geolocation.watchPosition(success, error, options);




          function updateMarkerPosition(newLat, newLng) {
              console.log(newLat + " " + newLng)
              const newPosition = new google.maps.LatLng(newLat, newLng);
              dirver.setPosition(newPosition);
              map.setCenter(newPosition);
              selectedLocation = newPosition;
              driverLocation.lat = newLat;
              driverLocation.lng = newLng;
          }

          const darkModeStyles = [
              { elementType: "geometry", stylers: [{ color: "#131e2d" }] },
              { elementType: "labels.text.stroke", stylers: [{ color: "#242f3e" }] },
              { elementType: "labels.text.fill", stylers: [{ color: "#746855" }] },
              {
                  featureType: "administrative.locality",
                  elementType: "labels.text.fill",
                  stylers: [{ color: "#d59563" }],
              },
              {
                  featureType: "poi",
                  elementType: "labels.text.fill",
                  stylers: [{ color: "#d59563" }],
              },
              {
                  featureType: "poi.park",
                  elementType: "geometry",
                  stylers: [{ color: "#263c3f" }],
              },
              {
                  featureType: "poi.park",
                  elementType: "labels.text.fill",
                  stylers: [{ color: "#6b9a76" }],
              },
              {
                  featureType: "road",
                  elementType: "geometry",
                  stylers: [{ color: "#38414e" }],
              },
              {
                  featureType: "road",
                  elementType: "geometry.stroke",
                  stylers: [{ color: "#212a37" }],
              },
              {
                  featureType: "road",
                  elementType: "labels.text.fill",
                  stylers: [{ color: "#9ca5b3" }],
              },
              {
                  featureType: "road.highway",
                  elementType: "geometry",
                  stylers: [{ color: "#746855" }],
              },
              {
                  featureType: "road.highway",
                  elementType: "geometry.stroke",
                  stylers: [{ color: "#1f2835" }],
              },
              {
                  featureType: "road.highway",
                  elementType: "labels.text.fill",
                  stylers: [{ color: "#f3d19c" }],
              },
              {
                  featureType: "transit",
                  elementType: "geometry",
                  stylers: [{ color: "#2f3948" }],
              },
              {
                  featureType: "transit.station",
                  elementType: "labels.text.fill",
                  stylers: [{ color: "#d59563" }],
              },
              {
                  featureType: "water",
                  elementType: "geometry",
                  stylers: [{ color: "#17263c" }],
              },
              {
                  featureType: "water",
                  elementType: "labels.text.fill",
                  stylers: [{ color: "#515c6d" }],
              },
              {
                  featureType: "water",
                  elementType: "labels.text.stroke",
                  stylers: [{ color: "#17263c" }],
              },
          ];

          function updateDriverLocation() {
              $.ajax({
                  type: "POST",
                  url: "Deliver.aspx/UpdateDriverLocation",
                  data: JSON.stringify({ latitude: driverLocation.lat, longitude: driverLocation.lng }),
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  success: function (data) {

                  },
                  error: function (error) {
                      console.error("An error occurred: " + error.responseText);
                  }
              });
          }
          setInterval(updateDriverLocation, 10000);
      </script>

    <script async
    	src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCRXvY67uo2SVzezWtxsQeqmgh4IRkA7Ag&callback=initMap">
	</script>
</asp:Content>