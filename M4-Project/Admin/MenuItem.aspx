<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="MenuItem.aspx.cs" Inherits="M4_Project.Admin.MenuItem" %>
<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/menu_item_style.css">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">

      google.charts.load('current', {'packages':['corechart']});
      google.charts.setOnLoadCallback(drawChart);
      var data;
      function drawChart() {
        data = google.visualization.arrayToDataTable([
          ['Mouth', 'Sales'],
          ['Jan',  1000],
          ['Feb',  1170],
          ['Mar',  660],
          ['May',  1030],
          ['May1',  1030],
          ['May2',  1030]
        ]);

        var options = {
          title: 'Monthly sales',
          hAxis: {title: 'Mouth',  titleTextStyle: {color: '#333', italic: false}},
          vAxis: {title: 'Amount (Rands)', titleTextStyle: {color: '#333', italic: false}, minValue: 0},
          series: {
    		0: { color: '#2E77A0' },
		  }
        };
        
        var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));
        chart.draw(data, options);
      }
    </script>
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="AdminMainContent" runat="server">
    		<div class="item_details_wrapper">
			<div class="item_image_wrapper">
				<asp:Image ID="Image1" runat="server"/>
			</div>
			<div class="item_details">
				<h1><%: menuItem.ItemName %></h1>
				<div class="middle_details_wrapper">
					<p class="item_description"><%: menuItem.ItemDescription %></p>
					<p class="item_price">R <%: menuItem.ItemPriceN2 %></p>
				</div>

				<div class="bottom_details_wrapper">
					<h2>Category: <%: menuItem.ItemCategory %></h2>
				</div>
			</div>
		</div>

		<div class="analytic_data_wrapper">
			<h1>Analysis</h1>
			<div class="time_frame_wrapper">
				<p>For This Time Frame:</p>
				<div class="inline_input_fields">
					<div class="inline_label_input">
						<label>From:</label>
						<input type="date" id="start_date">
					</div>
					<div class="inline_label_input">
						<label>To:</label>
						<input type="date" id="end_date">
					</div>
				</div>
			</div>
			<div class="text_details_wrapper">
				<p>Number of orders the item is involved in: 10</p>
				<p>Number of bookings the item is involved in: 3</p>
				<p>Total units sold (for orders): 14</p>
				<p>Total units sold (for booking): 211</p>
				<p>Total units sold: 225</p>
			</div>

			<div>
				<div id="chart_div" style="width: 80%; height: 450px; margin-left: -48px; transition: 0.5s ease;"></div>
			</div>
		</div>

		<div class="inline_buttons">
			<button class="primary_button">Edit</button>
			<button class="delete_button">Delete</button>
		</div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
		<script>
            theme_button.onclick = function () {
                if (document.body.classList.contains("dark-mode")) {
                    console.log("Here");
                    var options = {
                        title: 'Monthly sales',
                        hAxis: { title: 'Mouth', titleTextStyle: { color: '#333', italic: false } },
                        vAxis: { title: 'Amount (Rands)', titleTextStyle: { color: '#333', italic: false }, minValue: 0 },
                        backgroundColor: '#101010',
                        series: {
                            0: { color: '#ff0000' },
                        }
                    };
                }
                else {

                }
                var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));
                chart.draw(data, options);
            };
        </script>
	<script type="text/javascript">
        var sun = '<svg xmlns="http://www.w3.org/2000/svg" enable-background="new 0 0 24 24" viewBox="0 0 24 24" fill="#000000"><rect fill="none" height="24" width="24"/><path d="M12,7c-2.76,0-5,2.24-5,5s2.24,5,5,5s5-2.24,5-5S14.76,7,12,7L12,7z M2,13l2,0c0.55,0,1-0.45,1-1s-0.45-1-1-1l-2,0 c-0.55,0-1,0.45-1,1S1.45,13,2,13z M20,13l2,0c0.55,0,1-0.45,1-1s-0.45-1-1-1l-2,0c-0.55,0-1,0.45-1,1S19.45,13,20,13z M11,2v2 c0,0.55,0.45,1,1,1s1-0.45,1-1V2c0-0.55-0.45-1-1-1S11,1.45,11,2z M11,20v2c0,0.55,0.45,1,1,1s1-0.45,1-1v-2c0-0.55-0.45-1-1-1 C11.45,19,11,19.45,11,20z M5.99,4.58c-0.39-0.39-1.03-0.39-1.41,0c-0.39,0.39-0.39,1.03,0,1.41l1.06,1.06 c0.39,0.39,1.03,0.39,1.41,0s0.39-1.03,0-1.41L5.99,4.58z M18.36,16.95c-0.39-0.39-1.03-0.39-1.41,0c-0.39,0.39-0.39,1.03,0,1.41 l1.06,1.06c0.39,0.39,1.03,0.39,1.41,0c0.39-0.39,0.39-1.03,0-1.41L18.36,16.95z M19.42,5.99c0.39-0.39,0.39-1.03,0-1.41 c-0.39-0.39-1.03-0.39-1.41,0l-1.06,1.06c-0.39,0.39-0.39,1.03,0,1.41s1.03,0.39,1.41,0L19.42,5.99z M7.05,18.36 c0.39-0.39,0.39-1.03,0-1.41c-0.39-0.39-1.03-0.39-1.41,0l-1.06,1.06c-0.39,0.39-0.39,1.03,0,1.41s1.03,0.39,1.41,0L7.05,18.36z"/></svg>';

        var moon = '<svg xmlns="http://www.w3.org/2000/svg" enable-background="new 0 0 24 24" viewBox="0 0 24 24" fill="#000000"><rect fill="none" height="24" width="24"/><path d="M12,3c-4.97,0-9,4.03-9,9s4.03,9,9,9s9-4.03,9-9c0-0.46-0.04-0.92-0.1-1.36c-0.98,1.37-2.58,2.26-4.4,2.26 c-2.98,0-5.4-2.42-5.4-5.4c0-1.81,0.89-3.42,2.26-4.4C12.92,3.04,12.46,3,12,3L12,3z"/></svg>';

        var theme_button = document.getElementById('theme_button');
        theme_button.onclick = function () {
            document.body.classList.toggle("dark-mode");
            if (document.body.classList.contains("dark-mode")) {
                theme_button.innerHTML = sun;
                redraw_chart(true);
            }
            else {
                theme_button.innerHTML = moon;
                redraw_chart(false);
            }

        };

        function redraw_chart(isDarkMode) {
            if (isDarkMode) {
                var options = {
                    title: 'Monthly sales',
                    titleTextStyle: {
                        color: '#dfdfdf'
                    },
                    hAxis: { title: 'Mouth', titleTextStyle: { color: '#dfdfdf', italic: false }, textStyle: { color: '#dfdfdf' } },
                    vAxis: { title: 'Amount (Rands)', titleTextStyle: { color: '#dfdfdf', italic: false }, minValue: 0, textStyle: { color: '#dfdfdf' } },
                    series: {
                        0: { color: '#2E77A0' },
                    },
                    legend: {
                        textStyle: {
                            color: 'dfdfdf'
                        }
                    },
                    backgroundColor: '#151515'
                };
            } else {
                var options = {
                    title: 'Monthly sales',
                    titleTextStyle: {
                        color: '#303030'
                    },
                    hAxis: { title: 'Mouth', titleTextStyle: { color: '#303030', italic: false }, textStyle: { color: '#303030' } },
                    vAxis: { title: 'Amount (Rands)', titleTextStyle: { color: '#303030', italic: false }, minValue: 0, textStyle: { color: '#303030' } },
                    series: {
                        0: { color: '#2E77A0' },
                    },
                    legend: {
                        textStyle: {
                            color: '303030'
                        }
                    },
                    backgroundColor: '#fff'
                };
            }

            var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));
            chart.draw(data, options);
        }
    </script>
</asp:Content>