using System.Collections.Generic;
using MySql.Data.MySqlItem;

namespace HairSalon.Models
{
  public class Category
  {
    private int _id;
    private string _name;
    private double _total;

    public Category(string name, double total = 0, int id = 0)
    {
      _name = name;
      _id = id;
      _total = total;

    }

    public string GetName(){return _name;}
    public int GetId(){return _id;}

    public void SetName(string newName){_name = newName;}
    public void SetTotal(double newTotal){_total = newTotal;}

    public static List<Category> GetAll()
    {
      List<Category> allCategories = new List<Category> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM categories;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int categoryId = rdr.GetInt32(0);
        string categoryName = rdr.GetString(1);
        double categoryTotal = rdr.GetDouble(2);
        Category newCategory = new Category(categoryName, categoryTotal, categoryId);
        allCategories.Add(newCategory);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCategories;
    }

    public static Category Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM categories WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int categoryId = 0;
      string categoryName = "";
      double categoryTotal = 0;
      while(rdr.Read())
      {
        categoryId = rdr.GetInt32(0);
        categoryName = rdr.GetString(1);
        categoryTotal = rdr.GetDouble(2);
      }
      Category newCategory = new Category(categoryName, categoryTotal, categoryId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newCategory;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO categories (name) VALUES (@name);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Item> GetItems()
    {
      List<Item> allCategoryItems = new List<Item>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM items WHERE category_id = @category_id;";
      MySqlParameter categoryId = new MySqlParameter();
      categoryId.ParameterName = "@category_id";
      categoryId.Value = this._id;
      cmd.Parameters.Add(categoryId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        int categpryId = rdr.GetInt32(1);
        string itemName = rdr.GetString(2);
        double itemPrice = rdr.GetDouble(3);
        Item newItem= new Item(categpryId, itemName, itemPrice, itemId);
        allCategoryItems.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCategoryItems;
    }

    public static double GetTotal()
    {
      double total = 0;
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT price FROM items;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        double itemPrice = rdr.GetDouble(3);
        total += itemPrice;
      }
      return total;
    }








// // ==========================TESTS =========================================
    // `public override bool Equals(System.Object otherCategory)
    // {
    //   if (!(otherCategory is Category))
    //   {
    //     return false;
    //   }
    //   else
    //   {
    //     Category newCategory = (Category) otherCategory;
    //     bool idEquality = this.GetId().Equals(newCategory.GetId());
    //     bool nameEquality = this.GetName().Equals(newCategory.GetName());
    //     return (idEquality && nameEquality);
    //   }
    // }
    // public override int GetHashCode()
    // {
    //   return this.GetId().GetHashCode();
    // }
    //
    // public static void ClearAll()
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"DELETE FROM categories;";
    //   cmd.ExecuteNonQuery();
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    // }
  }
}
