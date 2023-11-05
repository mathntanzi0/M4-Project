using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin
{
    public partial class Deactivate : System.Web.UI.Page
    {
        protected Models.StaffMember staffMember;
        protected void Page_Load(object sender, EventArgs e)
        {
            int staffID = 0;
            if (Request.QueryString["Member"] == null || !int.TryParse(Request.QueryString["Member"], out staffID))
                Response.Redirect("/Admin/Staff");
            staffMember = Models.StaffMember.GetStaffMember(staffID);
            if (staffMember == null)
                Response.Redirect("/Admin/Staff");

            string base64String = Convert.ToBase64String(staffMember.StaffImage);
            string imageUrl = "data:image/jpeg;base64," + base64String;
            StaffImage.ImageUrl = imageUrl;
            Title = staffMember.FullName;
        }
        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            int staffID = 0;
            if (Request.QueryString["Member"] == null || !int.TryParse(Request.QueryString["Member"], out staffID))
                Response.Redirect("/Admin/Staff");
            staffMember = Models.StaffMember.GetStaffMember_Short(staffID);
            if (staffMember == null)
                Response.Redirect("/Admin/Staff");
            Models.StaffMember.UpdateStaffStatus(staffID, Models.StaffMemberState.Deactivated);
            Response.Redirect("/Admin/StaffMember?Member="+staffMember.StaffID);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int staffID = 0;
            if (Request.QueryString["Member"] == null || !int.TryParse(Request.QueryString["Member"], out staffID))
                Response.Redirect("/Admin/Staff");
            staffMember = Models.StaffMember.GetStaffMember_Short(staffID);
            if (staffMember == null)
                Response.Redirect("/Admin/Staff");

            if (!staffMember.DeleteStaff())
            {
                string script = "alert('Failed to delete staff member.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            Response.Redirect("/Admin/Staff");
        }

    }
}