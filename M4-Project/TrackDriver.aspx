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
  		<h4>Name: Sam Surname</h4>
  		<h4>Phone number: +0 00 000 0000</h4>
  		<h4>WhatsApp: +0 00 000 0000</h4>
  		<h4>Email: email@example.com</h4>
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
			title: 'You'
			});
			const image = {
				url: "Assets/logo.png",
				scaledSize: new google.maps.Size(50, 50)
			};
			dirver = new google.maps.Marker({
			position: storeAddress,
			map: map,
			title: 'Dirver',
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
			getDriverLocation();
		}
		repeatedGreetings();

		function updateMarkerPosition(newLat, newLng) {
			const newPosition = new google.maps.LatLng(newLat, newLng);
			dirver.setPosition(newPosition);
			map.setCenter(newPosition);
			selectedLocation = newPosition;
		}
		let orderID = 200;
		function getDriverLocation() {
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

                        updateMarkerPosition(driverLatitude, driverLongitude);
					},
					error: function (xhr, ajaxOptions, thrownError) {
						console.log(xhr.status + ": " + thrownError);
					}
				});
            });
        }
      </script>
		<script async
    	src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCRXvY67uo2SVzezWtxsQeqmgh4IRkA7Ag&callback=initMap">
	</script>
</asp:Content>