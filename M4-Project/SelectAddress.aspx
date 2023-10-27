<%@ Page Title="Select Address" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectAddress.aspx.cs" Inherits="M4_Project.SelectAddress" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/address_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="secondary_header">
        <h1 class="inner_body_title">Address</h1>
        <div class="header_input">
            <input id="autocomplete_address" type="text" size="50" placeholder="Type your nearest address and then select your position...">
        </div>
    </div>
    <div id="map"></div>
    <button class="primary_button_unfixed" onclick="pickAddress_Click()">Pick Address</button>
</asp:Content>

<asp:Content ID="ScriptHolder" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript">
        let map;
        let marker;

        let selectedLocation = { lat: <%= latitude %>, lng: <%= longitude %> };

        async function initMap() {
            const { Map } = await google.maps.importLibrary("maps");
            const initialLatLng = selectedLocation;
            map = new Map(document.getElementById("map"), {
                center: initialLatLng,
                zoom: 18,
                streetViewControl: false,
                mapTypeControl: false,
            });

            marker = new google.maps.Marker({
                position: initialLatLng,
                map: map,
                title: 'Your location'
            });
            autocompleteAddress(initialLatLng);

            google.maps.event.addListener(map, 'center_changed', function () {
                const newCenter = map.getCenter();
                updateMarkerPositionCenter(newCenter.lat(), newCenter.lng());
            });
        }

        let autocomplete;

        function autocompleteAddress(initialLatLng) {
            const center = initialLatLng;
            const defaultBounds = {
                north: center.lat + 0.1,
                south: center.lat - 0.1,
                east: center.lng + 0.1,
                west: center.lng - 0.1,
            };
            const input = document.getElementById("autocomplete_address");
            const options = {
                bounds: defaultBounds,
                componentRestrictions: { country: "ZA" },
                fields: ["address_components", "geometry", "icon", "name"],
                strictBounds: false,
            };
            autocomplete = new google.maps.places.Autocomplete(input, options);
            autocomplete.addListener('place_changed', onPlaceChanged);
        }

        function onPlaceChanged() {
            const place = autocomplete.getPlace();
            if (!place.geometry)
                console.log("Does not exist");
            else {
                updateMarkerPosition(place.geometry.location.lat(), place.geometry.location.lng());
            }
        }

        function updateMarkerPosition(newLat, newLng) {
            const newPosition = new google.maps.LatLng(newLat, newLng);
            marker.setPosition(newPosition);
            map.setCenter(newPosition);
            updateSelectedLocation(newLat, newLng);
        }

        function updateMarkerPositionCenter(newLat, newLng) {
            const newPosition = new google.maps.LatLng(newLat, newLng);
            marker.setPosition(newPosition);
            updateSelectedLocation(newLat, newLng);
        }

        function updateSelectedLocation(newLat, newLng) {
            selectedLocation.lat = newLat;
            selectedLocation.lng = newLng;
        }

        function pickAddress_Click() {
            event.preventDefault();
            let addressName = document.getElementById("autocomplete_address").value;
            console.log(selectedLocation.lat + " " + selectedLocation.lng);
            $.ajax({
                type: "POST",
                url: "/SelectAddress.aspx/SaveSelectedAddress",
                data: JSON.stringify({ addressName: addressName, latitude: selectedLocation.lat, longitude: selectedLocation.lng }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    window.location.href = "/Checkout";
                },
                error: function (error) {
                    console.error("An error occurred: " + error.responseText);
                }
            });
        }
    </script>
      <script async src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCRXvY67uo2SVzezWtxsQeqmgh4IRkA7Ag&libraries=places&callback=initMap"></script>
</asp:Content>
