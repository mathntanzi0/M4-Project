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

            if (!IsPostBack)
            {
                if (Request.QueryString["Member"] != null)
                {
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

            Response.Redirect("/Admin/AddStaff?Member=" + staffID);
        }
    }
}