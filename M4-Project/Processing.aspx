<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Processing.aspx.cs" Inherits="M4_Project.Processing" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Processing Order</title>

    <style>
        body {
            background-color: ghostwhite;
            margin: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            flex-direction: column;
            font-family: Arial, sans-serif;
            text-align: center;
        }

        .spinner {
            position: relative;
            width: 80px;
            height: 80px;
            border-radius: 50%;
            border: 8px solid transparent;
            border-top-color: #2ecc71;
            animation: spin 2s linear infinite;
        }

        .text {
            font-size: 24px;
            letter-spacing: 2px;
            color: #2ecc71;
            margin-top: 20px;
            animation: fadeInOut 3s infinite, changeColor 6s infinite;
        }

        @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }

        @keyframes fadeInOut {
            0%, 100% { opacity: 0; }
            50% { opacity: 1; }
        }

        @keyframes changeColor {
            0% { color: #2ecc71; }
            25% { color: #27ae60; }
            50% { color: #218c74; }
            75% { color: #1e8449; }
            100% { color: #2ecc71; }
        }
    </style>
    <script type="text/javascript" src="Scripts/jquery-3.4.1.js"> </script>
</head>

<body>
    <div class="spinner"></div>
    <div class="text">Processing...</div>
    <br/><br/>
    <p>Your order is being processed. <br/> Just a few minute.</p>
</body>

</html>

<script>

    function isPending() {
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "Processing.aspx/IsPending",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var value = response.d;
                    if (value == 0)
                        window.location.replace("TrackOrder");
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    console.log(xhr.status + ": " + thrownError);
                }
            });
        });
    }
    setInterval(isPending, 5000);

</script>