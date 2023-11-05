using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace M4_Project.Models
{
    /// <summary>
    ///     Represents a menu item.
    /// </summary>
    public class MenuItem
    {
        private int itemID;
        private string itemName;
        private string itemDescription;
        private decimal itemPrice;
        private string itemCategory;
        private byte[] itemImage;
        private string status;
        private static List<MenuItem> menuItems;
        public static List<string> menuCategories;




        ///
        /// <summary>
        ///     Initializes a new instance of the M4_System.Models.MenuItem class.
        /// </summary>
        public MenuItem()
        {
            this.itemID = -1;
        }
        ///
        /// <summary>
        ///     Initializes a new instance of the M4_System.Models.MenuItem class.
        /// </summary>
        public MenuItem(int itemID, string itemName, string itemDescription, decimal itemPrice, string itemCategory, byte[] itemImage, string status)
        {
            this.itemID = itemID;
            this.itemName = itemName;
            this.itemDescription = itemDescription;
            this.itemPrice = itemPrice;
            this.itemCategory = itemCategory;
            this.itemImage = itemImage;
            this.status = status;
        }
        ///
        /// <summary>
        ///     Returns a MenuItem object with data retrieved from the database using the items identification.
        /// </summary>
        public static MenuItem GetMenuItem(int itemID)
        {
            try
            {
                string query = "SELECT item_id, item_name, item_description, item_price, item_type, item_image, [availability] " +
                    "FROM dbo.[Menu Item] " +
                    "WHERE item_id = @itemID;";

                using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@itemID", itemID);
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count < 1)
                        return null;
                    DataRow row = dt.Rows[0];
                    return new MenuItem(itemID, (string)row["item_name"], (string)row["item_description"], (decimal)row["item_price"], (string)row["item_type"], (byte[])row["item_image"], (string)row["availability"]);
                }
            }
            catch (Exception ex) {
                SystemUtilities.LogError(ex);
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        ///
        /// <summary>
        ///     Returns a list of MenuItem objects with data retrieved from the database.
        /// </summary>
        public static List<MenuItem> GetMenuItems(int page, int maxListSize)
        {
            try {
                if (page < 1)
                    page = 1;

                menuItems = new List<MenuItem>();
                string query = "SELECT item_id, item_name, item_description, item_price, item_type, item_image, [availability] " +
                                "FROM dbo.[Menu Item] " +
                                "ORDER BY item_id DESC " +
                                "OFFSET @page ROWS " +
                                "FETCH NEXT @itemCount ROWS ONLY; ";

                using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@page", (page - 1) * maxListSize);
                    command.Parameters.AddWithValue("@itemCount", maxListSize);
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        menuItems.Add(new MenuItem((int)row["item_id"], (string)row["item_name"], (string)row["item_description"], (decimal)row["item_price"], (string)row["item_type"], (byte[])row["item_image"], (string)row["availability"]));
                    }
                    return menuItems;
                }
            }
            catch (Exception ex) {
                SystemUtilities.LogError(ex);
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        ///
        /// <summary>
        ///     Returns a list of MenuItem objects with similar names retrieved from the database.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="maxListSize"></param>
        /// <param name="searchValue"></param>
        /// <returns>A list of menu items that have similar names</returns>
        public static List<MenuItem> GetMenuItems(int maxListSize, MenuSearch menuSearch)
        {
            try {
                menuItems = new List<MenuItem>();
                string query = "SELECT item_id, item_name, item_description, item_price, item_type, item_image, [availability] " +
                                "FROM dbo.[Menu Item] " +
                                menuSearch.SearchString +
                                "ORDER BY item_id DESC " +
                                "OFFSET @page ROWS " +
                                "FETCH NEXT @itemCount ROWS ONLY; ";

                using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@page", (menuSearch.Page - 1) * maxListSize);
                    command.Parameters.AddWithValue("@itemCount", maxListSize);
                    menuSearch.SetParameters(ref command);
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        menuItems.Add(new MenuItem((int)row["item_id"], (string)row["item_name"], (string)row["item_description"], (decimal)row["item_price"], (string)row["item_type"], (byte[])row["item_image"], (string)row["availability"]));
                    }
                    return menuItems;
                }
            }
            catch (Exception ex) {
                SystemUtilities.LogError(ex);
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        ///
        /// <summary>
        ///     Saves the attributes' values of the MenuItem instance into the database.
        /// </summary>
        public void AddMenuItem()
        {
            try {
                string query = "INSERT INTO [Menu Item] ([item_name], [item_description], [item_price], [item_type], [item_image], [availability]) VALUES (@item_name, @item_description, @item_price, @item_type, @item_image, @availability); ";

                using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@item_name", itemName);
                    command.Parameters.AddWithValue("@item_description", itemDescription);
                    command.Parameters.AddWithValue("@item_price", itemPrice);
                    command.Parameters.AddWithValue("@item_type", itemCategory);
                    command.Parameters.AddWithValue("@item_image", itemImage);
                    command.Parameters.AddWithValue("@availability", status);
                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex) {
                SystemUtilities.LogError(ex);
            }
        }
        ///
        /// <summary>
        ///     Delete a menu item using a specific menu item identification number.
        /// </summary>
        public static void DeleteMenuItem(int itemID)
        {
            try {
                string query = "DELETE [Order Line] WHERE [Order Line].item_id = @itemID; " +
                    "DELETE [Event Line] WHERE [Event Line].item_id = @itemID; " +
                    "DELETE [Menu Item] WHERE [Menu Item].item_id = @itemID;";

                using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@itemID", itemID);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex) {
                SystemUtilities.LogError(ex);
            }
        }
        /// <summary>
        /// Gets a list of random menu items from the database.
        /// </summary>
        /// <returns>A list of random menu items.</returns>
        public static List<MenuItem> GetRandomItems(int max)
        {
            try {
                List<MenuItem> menuItems = new List<MenuItem>();
                using (SqlConnection connection = new SqlConnection(Database.ConnectionString))
                {
                    string query = "SELECT TOP(@max) [item_id], [item_name], [item_description], [item_image], [item_price], [item_type] " +
                                   "FROM [GroupPmb6].[dbo].[Menu Item] " +
                                   "WHERE [availability] = @availability ORDER BY CHECKSUM(NEWID());";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@max", max);
                        command.Parameters.AddWithValue("@availability", Models.MenuItemStatus.Available.ToString());

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MenuItem menuItem = new MenuItem
                                {
                                    ItemID = (int)reader["item_id"],
                                    ItemName = reader["item_name"].ToString(),
                                    ItemPrice = (decimal)reader["item_price"],
                                    ItemCategory = reader["item_type"].ToString()
                                };
                                menuItem.ItemDescription = (reader.IsDBNull(reader.GetOrdinal("item_description"))) ? "none" : reader["item_description"].ToString();
                                menuItem.ItemImage = (byte[])reader["item_image"];
                                menuItems.Add(menuItem);
                            }
                        }
                    }
                }
                return menuItems;
            }
            catch (Exception ex) {
                SystemUtilities.LogError(ex);
                return null;
            }
        }
        /// <summary>
        /// Updates a menu item with an image.
        /// </summary>
        public void UpdateMenuItemWithImage()
        {
            try {
                string query = @"
            UPDATE [Menu Item]
            SET [item_name] = @item_name,
                [item_description] = @item_description,
                [item_price] = @item_price,
                [item_type] = @item_type,
                [item_image] = @item_image
            WHERE [item_id] = @item_id;";

                using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@item_id", itemID);
                    command.Parameters.AddWithValue("@item_name", itemName);
                    command.Parameters.AddWithValue("@item_description", itemDescription);
                    command.Parameters.AddWithValue("@item_price", itemPrice);
                    command.Parameters.AddWithValue("@item_type", itemCategory);
                    command.Parameters.AddWithValue("@item_image", itemImage);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex) {
                SystemUtilities.LogError(ex);
            } }
        /// <summary>
        ///     Updates a menu item without an image.
        /// </summary>
        public void UpdateMenuItem()
        {
            try {
                string query = @"
            UPDATE [Menu Item]
            SET [item_name] = @item_name,
                [item_description] = @item_description,
                [item_price] = @item_price,
                [item_type] = @item_type
            WHERE [item_id] = @item_id;";

                using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@item_id", itemID);
                    command.Parameters.AddWithValue("@item_name", itemName);
                    command.Parameters.AddWithValue("@item_description", itemDescription);
                    command.Parameters.AddWithValue("@item_price", itemPrice);
                    command.Parameters.AddWithValue("@item_type", itemCategory);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            } catch (Exception ex) {
                SystemUtilities.LogError(ex);
            } }
        /// <summary>
        ///     Sets the available menu categories.
        /// </summary>
        public static void SetMenuCategories()
        {
            try {
                string query = "SELECT DISTINCT item_type FROM [Menu Item]";
                menuCategories = new List<string>();

                using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string itemType = reader["item_type"].ToString();
                            menuCategories.Add(itemType);
                        }
                    }
                }
            }
            catch (Exception ex) {
                SystemUtilities.LogError(ex);
            }
        }
        /// <summary>
        ///     Changes the availability state of a menu item in the database.
        /// </summary>
        /// <param name="itemID">The unique identifier of the menu item.</param>
        /// <param name="state">The new availability state to be set.</param>
        public static void ChangeState(int itemID, string state)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Models.Database.ConnectionString))
                {
                    connection.Open();

                    string updateQuery = "UPDATE [Menu Item] SET [availability] = @State WHERE item_id = @ItemID";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@State", state);
                        command.Parameters.AddWithValue("@ItemID", itemID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Models.SystemUtilities.LogError(ex);
            }
        }


        public int ItemID { get => itemID; set => itemID = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public string ItemDescription { get => itemDescription; set => itemDescription = value; }
        public decimal ItemPrice { get => itemPrice; set => itemPrice = value; }
        public string ItemPriceN2 { get => itemPrice.ToString("N2"); }
        public string ItemCategory { get => itemCategory; set => itemCategory = value; }
        public string Status { get => status; set => status = value; }
        public byte[] ItemImage { get => itemImage; set => itemImage = value; }
    }
    public class MenuSearch
    {
        private string searchString;
        private bool itemNameIsSet;
        private bool itemTypeIsSet;
        private int page;

        private string itemName;
        private string itemType;

        public MenuSearch(string itemName, string itemType, bool hideUnavailable)
        {
            page = 1;
            if (!string.IsNullOrEmpty(itemName))
            {
                searchString = "WHERE item_name LIKE '%' + @searchValue + '%' ";
                if (hideUnavailable)
                    searchString += $"AND [availability] = '{MenuItemStatus.Available}' ";
                
                itemNameIsSet = true;
                if (!string.IsNullOrEmpty(itemType))
                {
                    searchString += "AND item_type = @itemType ";
                    itemTypeIsSet = true;
                }
            }
            else if (!string.IsNullOrEmpty(itemType))
            {
                searchString += "WHERE item_type = @itemType ";
                if (hideUnavailable)
                    searchString += $"AND [availability] = '{MenuItemStatus.Available}' ";
                itemTypeIsSet = true;
            } else
            {
                if (hideUnavailable)
                    searchString += $"WHERE [availability] = '{MenuItemStatus.Available}' ";
            }
            this.itemName = itemName;
            this.itemType = itemType;
        }
        public void SetParameters(ref SqlCommand command)
        {
            if (itemNameIsSet)
                command.Parameters.AddWithValue("@searchValue", itemName);
            if (itemTypeIsSet)
                command.Parameters.AddWithValue("@itemType", itemType);
        }
        public void NextPage()
        {
            page++;
        }
        public void PreviousPage()
        {
            if (page < 2)
                return;

            page--;
        }
        public void SetPage(int page)
        {
            if (page < 1)
                return;
            this.page = page;
        }
        public string SearchString { get => searchString; }
        public int Page { get => page; }
        public string ItemName { get => itemName; set => itemName = value; }
        public string ItemType { get => itemType; set => itemType = value; }
    }
    public static class MenuItemStatus
    {
        public readonly static string Available = "Available";
        public readonly static string Unavailable = "Unavailable";

        public static bool IsValidState(string state)
        {
            return Available == state || Unavailable == state;
        }
    }
}