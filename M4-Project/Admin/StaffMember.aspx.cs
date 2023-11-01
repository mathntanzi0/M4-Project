using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin
{
    public partial class StaffMember : System.Web.UI.Page
    {
        protected Models.StaffMember staffMember;
        protected Models.StaffLoginSession loginStaff;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                loginStaff = Session["LoginStaff"] as Models.StaffLoginSession;

                if (loginStaff == null)
                    loginStaff = Models.StaffLoginSession.SetSession();
                if (Request.QueryString["Member"] != null)
                {
                    if (!loginStaff.IsManagerOrSupervisor())
                        Response.Redirect("/Admin");

                    if (!int.TryParse(Request.QueryString["Member"], out int staffID))
                        Response.Redirect("/");

                    staffMember = Models.StaffMember.GetStaffMember(staffID);
                    if (staffMember == null)
                        Response.Redirect("/");
                    else
                    {
                        string base64String = Convert.ToBase64String(staffMember.StaffImage);
                        string imageUrl = "data:image/jpeg;base64," + base64String;
                        Image1.ImageUrl = imageUrl;
                        Page.Title = staffMember.FirstName + " " + staffMember.LastName;
                    }
                    EditButton.CommandArgument = staffMember.StaffID.ToString();
                    return;
                }

                staffMember = Models.StaffMember.GetStaffMember(Context.User.Identity.Name);
                if (staffMember == null)
                    Response.Redirect("/");
                else
                {
                    string base64String = Convert.ToBase64String(staffMember.StaffImage);
                    string imageUrl = "data:image/jpeg;base64," + base64String;
                    Image1.ImageUrl = imageUrl;
                    Page.Title = staffMember.FirstName + " " + staffMember.LastName;
                }
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            Button editButton = (Button)sender;
            string staffID = editButton.CommandArgument;

            Models.StaffLoginSession loginStaff = Session["LoginStaff"] as Models.StaffLoginSession;

            if (string.IsNullOrEmpty(staffID))
                staffID = loginStaff.StaffID.ToString();

            Response.Redirect("/Admin/AddStaff?Member=" + staffID);
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            
            if (int.TryParse(Request.QueryString["Member"], out int staffID))
            {
                try
                {
                    Models.StaffMember.UpdateStaffStatus(staffID, Models.StaffMemberState.Active);
                }
                catch
                {

                }
                Response.Redirect("/Admin/StaffMember?Member="+staffID);
            }
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
            Response.Redirect("/Account/Login");
        }
    }
}