using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace FinanceCounter.Models
{
  public class IncomeItem
  {
    private string _name;
    private int _id;
    private double _price;
    private int _incomeCategoryId;

    public IncomeItem (string name, double price, int incomeCategoryId, int id = 0)
    {
      _name = name;
      _incomeCategoryId = incomeCategoryId;
      _id = id;
      _price = price;
    }

    public string GetName(){return _name;}
    public double GetPrice(){return _price;}
    public int GetId(){ return _id;}
    public int GetIncomeCategoryId(){ return _incomeCategoryId;}

    public void SetName(string newName){ _name = newName;}
    public void SetPrice(double newPrice){ _price = newPrice;}

    public static List<IncomeItem> GetAll()
    {
      List<IncomeItem> allIncomeItems = new List<IncomeItem> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM incomeItems;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int incomeItemId = rdr.GetInt32(0);
        string incomeItemName = rdr.GetString(1);
        double incomeItemPrice = rdr.GetDouble(2);
        int incomeItemCategoryId = rdr.GetInt32(3);
        IncomeItem newIncomeItem = new IncomeItem(incomeItemName, incomeItemPrice, incomeItemCategoryId, incomeItemId);
        allIncomeItems.Add(newIncomeItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allIncomeItems;
    }

    public static IncomeItem Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM incomeItems WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int incomeItemId = 0;
      string incomeItemName = "";
      int incomeItemCategoryId = 0;
      double incomeItemPrice = 0;
      while(rdr.Read())
      {
        incomeItemId = rdr.GetInt32(0);
        incomeItemName = rdr.GetString(1);
        incomeItemPrice = rdr.GetDouble(2);
        incomeItemCategoryId = rdr.GetInt32(3);
      }
      IncomeItem newIncomeItem = new IncomeItem(incomeItemName, incomeItemPrice, incomeItemCategoryId, incomeItemId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newIncomeItem;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO incomeItems (name, price, incomeCategory_id) VALUES (@name, @price, @incomeCategory_id);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);
      MySqlParameter incomeCategoryId = new MySqlParameter();
      incomeCategoryId.ParameterName = "@incomeCategory_id";
      incomeCategoryId.Value = this._incomeCategoryId;
      cmd.Parameters.Add(incomeCategoryId);
      MySqlParameter price = new MySqlParameter();
      price.ParameterName = "@price";
      price.Value = this._price;
      cmd.Parameters.Add(price);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE incomeItems SET name = @newName WHERE id = @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);
      cmd.ExecuteNonQuery();
      _name = newName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Edit(string newName, double newPrice)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE incomeItems SET name = @newName, price = newPrice WHERE id = @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);
      MySqlParameter price = new MySqlParameter();
      price.ParameterName = "@newPrice";
      price.Value = newPrice;
      cmd.Parameters.Add(price);
      cmd.ExecuteNonQuery();
      _name = newName;
      _price = newPrice;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM incomeItems WHERE id = @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

// ====================== TESTS =================================

    public override bool Equals(System.Object otherIncomeItem)
    {
      if (!(otherIncomeItem is IncomeItem))
      {
        return false;
      }
      else
      {
         IncomeItem newIncomeItem = (IncomeItem) otherIncomeItem;
         bool idEquality = this.GetId() == newIncomeItem.GetId();
         bool nameEquality = this.GetName() == newIncomeItem.GetName();
         bool incomeCategoryEquality = this.GetIncomeCategoryId() == newIncomeItem.GetIncomeCategoryId();
         return (idEquality && nameEquality && incomeCategoryEquality);
       }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM incomeItems;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
       conn.Dispose();
      }
    }
  }
}
