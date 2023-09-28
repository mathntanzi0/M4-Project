using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_Project.Admin
{
    public partial class AddItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                selectCategory.DataSource = Models.MenuItem.menuCategories;
                selectCategory.DataBind();

                ListItem item = new ListItem("New Option", "newOption");
                selectCategory.Items.Add(item);


                if (Request.QueryString["Item"] != null)
                {
                    if (int.TryParse(Request.QueryString["Item"], out int itemID))
                    {
                        Models.MenuItem menuItem = Models.MenuItem.GetMenuItem(itemID);
                        if (menuItem != null){
                            txtItemName.Text = menuItem.ItemName;
                            txtItemPrice.Text = menuItem.ItemPrice.ToString();
                            txtItemDescription.Text = menuItem.ItemDescription;
                            selectCategory.SelectedValue = menuItem.ItemCategory;

                            string base64String = Convert.ToBase64String(menuItem.ItemImage);
                            string imageUrl = "data:image/jpeg;base64," + base64String;
                            imgImage.ImageUrl = imageUrl;
                        }
                    }
                }
            }
        }
        protected void btnAddItem_Click(object sender, EventArgs e)
        {

            if (!Page.IsValid)
                return;

            string itemName = txtItemName.Text;
            decimal itemPrice = decimal.Parse(txtItemPrice.Text);
            string itemDescription = txtItemDescription.Text;
            string itemCategory = selectCategory.SelectedValue;


            if (itemPrice < 1)
            {
                string script = "alert('Please enter a valid amount');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }
            if (itemName.Length > Models.BusinessRules.MenuItem.ItemNameCharLimit)
            {
                string script = "alert('Please enter a item name with characters less than " + Models.BusinessRules.MenuItem.ItemNameCharLimit + " amount');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }
            bool newOption = false;
            if (itemCategory == "newOption") {
                itemCategory = Utilities.TextManager.CapitalizeString(txtNewCategory.Text);
                newOption = true;
            }
                

            byte[] image;
            bool hasFile = fileUploadControl.HasFile;
            if (hasFile)
            {
                HttpPostedFile uploadedFile = fileUploadControl.PostedFile;
                string fileName = Path.GetFileName(uploadedFile.FileName);
                string fileExtension = Path.GetExtension(fileName);

                if (!(fileExtension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                    fileExtension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                    fileExtension.Equals(".png", StringComparison.OrdinalIgnoreCase) ||
                    fileExtension.Equals(".gif", StringComparison.OrdinalIgnoreCase)))
                {

                    string script = "alert('Please upload a valid image file (jpg, jpeg, png, gif)');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    return;
                }

                using (Stream stream = uploadedFile.InputStream)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        image = ms.ToArray();
                    }
                }
            } else
            {
                string defaultImagePath = Server.MapPath("~/Assets/default_item.png");
                image = File.ReadAllBytes(defaultImagePath);
            }


            if (Request.QueryString["Item"] != null && int.TryParse(Request.QueryString["Item"], out int itemID))
            {
                if (hasFile)
                {
                    Models.MenuItem item = new Models.MenuItem()
                    {
                        ItemID = itemID,
                        ItemName = itemName,
                        ItemDescription = itemDescription,
                        ItemPrice = itemPrice,
                        ItemCategory = itemCategory,
                        ItemImage = image
                    };
                    item.UpdateMenuItemWithImage();
                } 
                else
                {
                    Models.MenuItem item = new Models.MenuItem()
                    {
                        ItemID = itemID,
                        ItemName = itemName,
                        ItemDescription = itemDescription,
                        ItemPrice = itemPrice,
                        ItemCategory = itemCategory
                    };
                    item.UpdateMenuItem();
                }
                if (newOption)
                    M4_Project.Models.MenuItem.SetMenuCategories();
                Response.Redirect("/Admin/Menu");
                return;
            }
            Models.MenuItem menuItem = new Models.MenuItem()
            {
                ItemName = itemName,
                ItemDescription = itemDescription,
                ItemPrice = itemPrice,
                ItemCategory = itemCategory,
                ItemImage = image,
                Status = Models.MenuItemStatus.Available
            };
            menuItem.AddMenuItem();

            if (newOption)
                M4_Project.Models.MenuItem.SetMenuCategories();
            Response.Redirect("/Admin/Menu");
        }
    }
}