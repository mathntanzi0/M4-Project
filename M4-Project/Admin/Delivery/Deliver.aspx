<%@ Page Title="Live Delivery Orders" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Deliver.aspx.cs" Inherits="M4_Project.Admin.Delivery.Deliver" %>

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
  			<h4>Name: Sam Surname</h4>
  			<h4>phone number: +0 00 000 0000</h4>
  			<h4>Email: email@example.com</h4>
  			<h4>Location: No street, City, Postal code, County</h4>
  		</div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
	  <script type="text/javascript">
          let map;
          let marker;
          let dirver;

          let storeAddress = { lat: -29.62131879702845, lng: 30.394956658547798 };
          let selectedLocation = { lat: -29.62136476715044, lng: 30.395960753346802 };
          async function initMap() {
              const { Map } = await google.maps.importLibrary("maps");
              const initialLatLng = selectedLocation;
              map = new Map(document.getElementById("map"), {
                  center: storeAddress,
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
                  position: storeAddress,
                  map: map,
                  title: 'Dirver',
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
      </script>

    <script async
    	src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCRXvY67uo2SVzezWtxsQeqmgh4IRkA7Ag&callback=initMap">
	</script>
</asp:Content>