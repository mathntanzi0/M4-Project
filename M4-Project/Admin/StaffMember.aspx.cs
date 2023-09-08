using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin
{
    public partial class StaffMember : System.Web.UI.Page
    {
        protected Models.StaffMember staffMember;
        protected void Page_Load(object sender, EventArgs e)
        {
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
}