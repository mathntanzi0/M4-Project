﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace M4_Project
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected bool liveOrder = false;
        protected Models.Customer currentCustomer;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["sale"] == null)
            {
                if (HttpContext.Current.Request.Cookies[Models.Sales.CartItem.OrderCart] != null)
                    Models.Sales.Order.SyncOrderCartWithCookieCart();
                
                else if (HttpContext.Current.Request.Cookies[Models.Sales.CartItem.BookingCart] != null)
                    Models.Sales.Booking.SyncSessionWithCookies();
            }

            currentCustomer = Session["Customer"] as Models.Customer;
            if (currentCustomer == null && Page.Title != "Profile" && Page.Title != "Checkout" && Context.User.Identity.IsAuthenticated)
            {
                if (Request.QueryString["ReturnUrl"] != null)
                    Session["Customer"] = Models.Customer.SetSession(Request.QueryString["ReturnUrl"]);
                else
                    Session["Customer"] = Models.Customer.SetSession();
                currentCustomer = Session["Customer"] as Models.Customer;
            }

            if (currentCustomer != null)
            {
                if (Session["liveOrder"] == null)
                {
                    int orderID = Models.Sales.Order.GetLiveOrder(currentCustomer.CustomerID);
                    if (orderID > 0)
                    {
                        Session["liveOrder"] = orderID;
                        liveOrder = true;
                    }
                } else
                    liveOrder = true;
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }

}