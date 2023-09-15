<%@ Page Title="Track Order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrackOrder.aspx.cs" Inherits="M4_Project.TrackOrder" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/order_track_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="secondary_header">
		<h1>Order #323</h1>
	</div>
	<div class="order_progress_wrapper">
		<div class="status_bar"></div>
		<div>
			<div class="order_progress_line in_progress">
				<div class="image_wrapper">
					<svg width="96" height="96" viewBox="0 0 96 96" fill="none" xmlns="http://www.w3.org/2000/svg">
					<path d="M21.1 54C21.5667 53.2 21.9167 52.3333 22.15 51.4C22.3833 50.4667 22.5 49.3333 22.5 48C22.5 45.6667 21.8333 43.15 20.5 40.45C19.1667 37.75 18.5 35.4333 18.5 33.5C18.5 32.7667 18.5667 31.9167 18.7 30.95C18.8333 29.9833 19.2333 29 19.9 28H24.9C24.3667 28.9333 24 29.9 23.8 30.9C23.6 31.9 23.5 32.7667 23.5 33.5C23.5 35.7667 24.1667 38.1667 25.5 40.7C26.8333 43.2333 27.5 45.6667 27.5 48C27.5 49.3333 27.3833 50.4667 27.15 51.4C26.9167 52.3333 26.5667 53.2 26.1 54H21.1ZM47.1 54C47.5667 53.2 47.9167 52.3333 48.15 51.4C48.3833 50.4667 48.5 49.3333 48.5 48C48.5 45.6667 47.8333 43.15 46.5 40.45C45.1667 37.75 44.5 35.4333 44.5 33.5C44.5 32.7667 44.5667 31.9167 44.7 30.95C44.8333 29.9833 45.2333 29 45.9 28H50.9C50.3667 28.9333 50 29.9 49.8 30.9C49.6 31.9 49.5 32.7667 49.5 33.5C49.5 35.7667 50.1667 38.1667 51.5 40.7C52.8333 43.2333 53.5 45.6667 53.5 48C53.5 49.3333 53.3833 50.4667 53.15 51.4C52.9167 52.3333 52.5667 53.2 52.1 54H47.1ZM34.1 54C34.5667 53.2 34.9167 52.3333 35.15 51.4C35.3833 50.4667 35.5 49.3333 35.5 48C35.5 45.6667 34.8333 43.15 33.5 40.45C32.1667 37.75 31.5 35.4333 31.5 33.5C31.5 32.7667 31.5667 31.9167 31.7 30.95C31.8333 29.9833 32.2333 29 32.9 28H37.9C37.3667 28.9333 37 29.9 36.8 30.9C36.6 31.9 36.5 32.7667 36.5 33.5C36.5 35.7667 37.1667 38.1667 38.5 40.7C39.8333 43.2333 40.5 45.6667 40.5 48C40.5 49.3333 40.3833 50.4667 40.15 51.4C39.9167 52.3333 39.5667 53.2 39.1 54H34.1ZM39 88C32.3333 88 26.4 85.7167 21.2 81.15C16 76.5833 12.9333 71 12 64.4C11.8667 63.4667 12.0833 62.6667 12.65 62C13.2167 61.3333 14 61 15 61H60L64.5 17.7C64.8334 14.9667 66.0334 12.6667 68.1 10.8C70.1667 8.93333 72.6 8 75.4 8C78.4667 8 81.0667 9.06667 83.2 11.2C85.3334 13.3333 86.4 15.9333 86.4 19C86.4 19.6 86.3667 20.3833 86.3 21.35C86.2334 22.3167 86.1334 23.1667 86 23.9L80.1 23.1L80.25 21.4C80.35 20.2667 80.4 19.4667 80.4 19C80.4 17.6 79.9167 16.4167 78.95 15.45C77.9834 14.4833 76.8 14 75.4 14C74.0667 14 72.9333 14.4333 72 15.3C71.0667 16.1667 70.5334 17.2333 70.4 18.5L65.8 63C65.0667 70.0667 62.15 76 57.05 80.8C51.95 85.6 45.9333 88 39 88ZM39 82C43.3333 82 47.3 80.7 50.9 78.1C54.5 75.5 57.1667 71.8 58.9 67H18.8C20.5333 71.8 23.25 75.5 26.95 78.1C30.65 80.7 34.6667 82 39 82Z" fill="#404040"/>
					</svg>
				</div>
				<div class="status_details">
					<h2>Status: Preparing</h2>
					<br>
					<p>Our chefs are on it.</p>
				</div>
			</div>
		</div>
		<div>
			<div class="order_progress_line">
				<div class="image_wrapper">
					<svg width="96" height="96" viewBox="0 0 96 96" fill="none" xmlns="http://www.w3.org/2000/svg">
					<path d="M57.5 59.6C56.3 56.2 53.8333 53.4667 50.1 51.4C46.3667 49.3333 41.1333 48.3 34.4 48.3C27.6667 48.3 22.4333 49.3333 18.7 51.4C14.9667 53.4667 12.4667 56.2 11.2 59.6H57.5ZM4 65.6C4 62.4 4.58333 59.3833 5.75 56.55C6.91667 53.7167 8.75 51.25 11.25 49.15C13.75 47.05 16.9167 45.3833 20.75 44.15C24.5833 42.9167 29.15 42.3 34.45 42.3C39.75 42.3 44.3 42.9167 48.1 44.15C51.9 45.3833 55.05 47.05 57.55 49.15C60.05 51.25 61.8833 53.7167 63.05 56.55C64.2167 59.3833 64.8 62.4 64.8 65.6H4ZM4 78.4V72.4H64.8V78.4H4ZM70.8 92V86H79.4L85.1 28H45.3L44.6 22H65.1V4H71.1V22H92L85.3 86C85.1 87.7333 84.3833 89.1667 83.15 90.3C81.9167 91.4333 80.3667 92 78.5 92H70.8ZM7 92C6.2 92 5.5 91.7 4.9 91.1C4.3 90.5 4 89.8 4 89V86H64.8V89C64.8 89.8 64.5 90.5 63.9 91.1C63.3 91.7 62.6 92 61.8 92H7Z" fill="#404040"/>
					</svg>
				</div>
				<div class="status_details">
					<h2>Status: Prepared</h2>
					<br>
					<p>Order ready for collection.</p>
				</div>
			</div>
		</div>
		<div>
			<div class="order_progress_line order_progress_line_button">
				<div class="image_wrapper">
					<svg width="96" height="96" viewBox="0 0 96 96" fill="none" xmlns="http://www.w3.org/2000/svg">
					<g clip-path="url(#clip0_1313_1591)">
					<path d="M76 28C76 23.6 72.4 20 68 20H56V28H68V38.6L54.08 56H40V36H24C15.16 36 8 43.16 8 52V64H16C16 70.64 21.36 76 28 76C34.64 76 40 70.64 40 64H57.92L76 41.4V28ZM28 68C25.8 68 24 66.2 24 64H32C32 66.2 30.2 68 28 68Z" fill="#404040"/>
					<path d="M40 24H20V32H40V24Z" fill="#404040"/>
					<path d="M76 52C69.36 52 64 57.36 64 64C64 70.64 69.36 76 76 76C82.64 76 88 70.64 88 64C88 57.36 82.64 52 76 52ZM76 68C73.8 68 72 66.2 72 64C72 61.8 73.8 60 76 60C78.2 60 80 61.8 80 64C80 66.2 78.2 68 76 68Z" fill="#404040"/>
					</g>
					<defs>
					<clipPath id="clip0_1313_1591">
					<rect width="96" height="96" fill="white"/>
					</clipPath>
					</defs>
					</svg>
				</div>
				<div class="status_details">
					<h2>Status: Collected</h2>
					<br>
					<p>Driver on their way.</p>
				</div>
				<button>Track</button>
			</div>
		</div>
		<div>
			<div class="order_progress_line">
				<div class="image_wrapper">
					<svg width="96" height="96" viewBox="0 0 96 96" fill="none" xmlns="http://www.w3.org/2000/svg">
					<g clip-path="url(#clip0_1313_1604)">
					<path d="M8 76H88L80 84H16L8 76ZM20 24H24V28H20V24ZM20 16H24V20H20V16ZM36 16V20H28V16H36ZM36 28H28V24H36V28ZM24 60.92C22.56 61.36 21.24 62.04 20 62.8V32H24V60.92ZM16 66.08C14.48 67.84 13.28 69.8 12.64 72H79.92C79.96 71.36 80.04 70.68 80.04 70C80.04 57.84 70.2 48 58.04 48C48.88 48 41.04 53.6 37.72 61.6C35.36 60.6 32.76 60 30 60C29.32 60 28.68 60.08 28 60.16V32H36C40.12 32.24 43.6 28.16 44 24H84V20H44C43.6 15.8 40.12 12.12 36 12H12V16H16V20H12V24H16V28H12V32H16V66.08Z" fill="#404040"/>
					</g>
					<defs>
					<clipPath id="clip0_1313_1604">
					<rect width="96" height="96" fill="white"/>
					</clipPath>
					</defs>
					</svg>
				</div>
				<div class="status_details">
					<h2>Status: Delivered</h2>
					<br>
					<p>Order delivered, Enjoy.</p>
				</div>
			</div>
		</div>
	</div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
</asp:Content>