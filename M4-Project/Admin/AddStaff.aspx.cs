using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin
{
    public partial class AddStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlStaffRole.DataSource = Models.StaffMember.GetRoles();
                ddlStaffRole.DataBind();

                ListItem item = new ListItem("New Role", "newOption");
                ddlStaffRole.Items.Add(item);

                Models.StaffLoginSession loginStaff = Session["LoginStaff"] as Models.StaffLoginSession;
                if (Request.QueryString["Member"] != null)
                {
                    if (int.TryParse(Request.QueryString["Member"], out int staffID))
                    {
                        if (!loginStaff.IsManagerOrSupervisor() && loginStaff.StaffID != staffID)
                            Response.Redirect("/Admin");
                        Models.StaffMember staffMember = Models.StaffMember.GetStaffMember(staffID);
                        if (staffMember != null)
                        {
                            txtFirstName.Text = staffMember.FirstName;
                            txtLastName.Text = staffMember.LastName;
                            txtEmail.Text = staffMember.EmailAddress;
                            txtPhoneNumber.Text = staffMember.PhoneNumber;
                            txtPayRate.Text = staffMember.PayRate.ToString();
                            ddlStaffRole.SelectedValue = staffMember.Role;
                            ddlGender.SelectedValue = staffMember.Gender;

                            byte[] staffImage = staffMember.StaffImage;
                            if (staffImage != null && staffImage.Length > 0)
                            {
                                string base64String = Convert.ToBase64String(staffImage);
                                string imageUrl = "data:image/jpeg;base64," + base64String;
                                imgImage.ImageUrl = imageUrl;
                            }
                            btnAddStaff.Text = "Edit";
                            Title = "Edit Staff Details";
                        }
                    }
                }
                else if (loginStaff == null || !loginStaff.IsManagerOrSupervisor())
                    Response.Redirect("/Admin");
            }
        }
        protected void btnAddStaff_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string staffRole = ddlStaffRole.SelectedValue;
            string gender = ddlGender.SelectedValue;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNumber))
            {
                string script = "alert('Please fill in all required fields.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            if (!decimal.TryParse(txtPayRate.Text, out decimal payRate))
            {
                string script = "alert('Invalid pay rate format. Please enter a valid number.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            if (fileUploadControl.HasFile)
            {
                string fileExtension = Path.GetExtension(fileUploadControl.FileName).ToLower();
                if (!(fileExtension.Equals(".jpg") || fileExtension.Equals(".jpeg") || fileExtension.Equals(".png") || fileExtension.Equals(".gif")))
                {
                    string script = "alert('Please upload a valid image file (jpg, jpeg, png, gif).');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    return;
                }
            }
            if (ddlStaffRole.SelectedValue == "newOption")
            {
                staffRole = Utilities.TextManager.CapitalizeString(txtNewStaffRole.Text);
                if (string.IsNullOrEmpty(staffRole))
                {
                    string script = "alert('Please fill in the new staff role.');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    return;
                }
            }
            if (ddlGender.SelectedValue == "newOption")
            {
                gender = Utilities.TextManager.CapitalizeString(txtGender.Text);
                if (string.IsNullOrEmpty(gender))
                {
                    string script = "alert('Please fill in the customer gender.');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    return;
                }
            }

            //Processing
            byte[] image;
            bool hasFile = fileUploadControl.HasFile;
            Models.StaffMember member;
            if (hasFile)
            {
                HttpPostedFile uploadedFile = fileUploadControl.PostedFile;
                string fileName = Path.GetFileName(uploadedFile.FileName);
                string fileExtension = Path.GetExtension(fileName);
                using (Stream stream = uploadedFile.InputStream)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        image = ms.ToArray();
                    }
                }
                member = new Models.StaffMember()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = email,
                    PhoneNumber = phoneNumber,
                    PayRate = payRate,
                    Role = staffRole,
                    Gender = gender,
                    Password = "M4-System",
                    StaffImage = image,
                    Status = Models.StaffMemberState.Active
                };
            }
            else
            {
                member = new Models.StaffMember()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = email,
                    PhoneNumber = phoneNumber,
                    PayRate = payRate,
                    Role = staffRole,
                    Password = "M4-System",
                    Gender = gender,
                    Status = Models.StaffMemberState.Active
                };
            }
            if (Request.QueryString["Member"] != null && int.TryParse(Request.QueryString["Member"], out int staffID))
            {
                member.StaffID = staffID;
                member.UpdateStaffMember();
                if (member.EmailAddress == HttpContext.Current.User.Identity.Name)
                    Response.Redirect("/Admin/StaffMember");
            }
            else 
                member.AddStaffMember();

            Response.Redirect("/Admin/StaffMember?Member="+member.StaffID);
        }
    }
}