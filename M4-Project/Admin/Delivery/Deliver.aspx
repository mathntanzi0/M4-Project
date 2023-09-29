<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Deliver.aspx.cs" Inherits="M4_Project.Admin.Delivery.Deliver" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/address_style.css">
	<script async
    	src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCRXvY67uo2SVzezWtxsQeqmgh4IRkA7Ag&callback=initMap">
	</script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">

    <div class="secondary_header">
			<h1 class="inner_body_title">Delivery</h1>
		</div>
  		<div class="address_details_wrapper">
  			<h3>Customer Details</h3>
  			<h4>Name: Sam Surname</h4>
  			<h4>phone number: +0 00 000 0000</h4>
  			<h4>Email: email@example.com</h4>
  			<h4>Location: No street, City, Postal code, County</h4>
  		</div>
  		<div id="map"></div>

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
              });

              marker = new google.maps.Marker({
                  position: initialLatLng,
                  map: map,
                  title: 'Customer'
              });
              const image = "assets/logo.png";
              dirver = new google.maps.Marker({
                  position: storeAddress,
                  map: map,
                  title: 'You',
                  icon: image,
              });
          }


          const sleep = (delay) => new Promise((resolve) => setTimeout(resolve, delay))

          const repeatedGreetings = async () => {
              await sleep(2000)
              updateMarkerPosition(-29.62131879702845, 30.395056658547798);
              await sleep(2000)
              updateMarkerPosition(-29.62131879702845, 30.395156658547798);
              await sleep(2000)
              updateMarkerPosition(-29.62131879702845, 30.395256658547798);
              await sleep(2000)
              updateMarkerPosition(-29.62131879702845, 30.395356658547798);
              await sleep(2000)
              updateMarkerPosition(-29.62131879702845, 30.395456658547798);
          }
          repeatedGreetings();

          function updateMarkerPosition(newLat, newLng) {
              const newPosition = new google.maps.LatLng(newLat, newLng);
              dirver.setPosition(newPosition);
              map.setCenter(newPosition);
              selectedLocation = newPosition;
          }
      </script>

	<script>
        var sun = '<svg xmlns="http://www.w3.org/2000/svg" enable-background="new 0 0 24 24" viewBox="0 0 24 24" fill="#000000"><rect fill="none" height="24" width="24"/><path d="M12,7c-2.76,0-5,2.24-5,5s2.24,5,5,5s5-2.24,5-5S14.76,7,12,7L12,7z M2,13l2,0c0.55,0,1-0.45,1-1s-0.45-1-1-1l-2,0 c-0.55,0-1,0.45-1,1S1.45,13,2,13z M20,13l2,0c0.55,0,1-0.45,1-1s-0.45-1-1-1l-2,0c-0.55,0-1,0.45-1,1S19.45,13,20,13z M11,2v2 c0,0.55,0.45,1,1,1s1-0.45,1-1V2c0-0.55-0.45-1-1-1S11,1.45,11,2z M11,20v2c0,0.55,0.45,1,1,1s1-0.45,1-1v-2c0-0.55-0.45-1-1-1 C11.45,19,11,19.45,11,20z M5.99,4.58c-0.39-0.39-1.03-0.39-1.41,0c-0.39,0.39-0.39,1.03,0,1.41l1.06,1.06 c0.39,0.39,1.03,0.39,1.41,0s0.39-1.03,0-1.41L5.99,4.58z M18.36,16.95c-0.39-0.39-1.03-0.39-1.41,0c-0.39,0.39-0.39,1.03,0,1.41 l1.06,1.06c0.39,0.39,1.03,0.39,1.41,0c0.39-0.39,0.39-1.03,0-1.41L18.36,16.95z M19.42,5.99c0.39-0.39,0.39-1.03,0-1.41 c-0.39-0.39-1.03-0.39-1.41,0l-1.06,1.06c-0.39,0.39-0.39,1.03,0,1.41s1.03,0.39,1.41,0L19.42,5.99z M7.05,18.36 c0.39-0.39,0.39-1.03,0-1.41c-0.39-0.39-1.03-0.39-1.41,0l-1.06,1.06c-0.39,0.39-0.39,1.03,0,1.41s1.03,0.39,1.41,0L7.05,18.36z"/></svg>';

        var moon = '<svg xmlns="http://www.w3.org/2000/svg" enable-background="new 0 0 24 24" viewBox="0 0 24 24" fill="#000000"><rect fill="none" height="24" width="24"/><path d="M12,3c-4.97,0-9,4.03-9,9s4.03,9,9,9s9-4.03,9-9c0-0.46-0.04-0.92-0.1-1.36c-0.98,1.37-2.58,2.26-4.4,2.26 c-2.98,0-5.4-2.42-5.4-5.4c0-1.81,0.89-3.42,2.26-4.4C12.92,3.04,12.46,3,12,3L12,3z"/></svg>';

        const lightModeStyles = [
            //Default
        ];
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
        // Add more styles as needed for specific map elements
        var theme_button = document.getElementById('theme_button');
        theme_button.onclick = function () {
            document.body.classList.toggle("dark-mode");
            if (document.body.classList.contains("dark-mode")) {
                theme_button.innerHTML = sun;
                map.setOptions({ styles: darkModeStyles });
            }
            else {
                theme_button.innerHTML = moon;
                map.setOptions({ styles: lightModeStyles });
            }

        };
    </script>
</asp:Content>