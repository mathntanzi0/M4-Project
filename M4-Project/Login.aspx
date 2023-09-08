<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="M4_System.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log in</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link href="Content/LoginStyle.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"/>
    <script src="Scripts/LoginJavaScript.js" ></script>
</head>
<body>
    <div class="loginbox">
        <img src="Assets\Logo.png" alt="Log in image" class="user" /><br />
        <form runat="server">
            <asp:Label Text="Username" CssClass="lblusername" runat="server" /><br />
            <asp:TextBox runat="server" CssClass="txtusername" placeholder="Enter Username..." /><br />
            <asp:Label Text="Password" CssClass="lblpass" runat="server" /><br />
            <div class="password-container">
            <asp:TextBox runat="server" CssClass="txtpass" placeholder="Enter password..." /><br />
            <i class="toggle-password" onclick="togglePasswordVisibility()"></i>
            </div>
            <asp:Button Text="LOGIN" CssClass="btnsubmit" runat="server" /><br />
            <asp:Button Text="SIGN UP" CssClass="btnsignup" runat="server" OnClick="go_to_signUp" /><br />
            <asp:LinkButton Text="Forgot Password?" CssClass="btnforget" runat="server" /><br />
        </form>
    </div>
</body>
</html>


