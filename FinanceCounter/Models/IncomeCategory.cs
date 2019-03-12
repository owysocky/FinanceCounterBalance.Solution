using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace FinanceCounter.Models
{
  public class IncomeCategory
  {
    private int _id;
    private int _account_id;
    private string _name;
    private double _total;

    public IncomeCategory(int account_id, string name, double total = 0, int id = 0)
    {
      _account_id = account_id;
      _name = name;
      _id = id;
      _total = total;

    }

    public string GetName(){return _name;}
    public int GetId(){return _id;}

    public void SetName(string newName){_name = newName;}

    public static List<IncomeCategory> GetAll()
    {
      List<IncomeCategory> allCategories = new List<IncomeCategory> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM incomeCategories;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int incomeCategoryId = rdr.GetInt32(0);
        string incomeCategoryName = rdr.GetString(1);
        double incomeCategoryTotal = rdr.GetDouble(2);
        int incomeAccountId = rdr.GetInt32(3);
        IncomeCategory newIncomeCategory = new IncomeCategory(incomeAccountId, incomeCategoryName, incomeCategoryTotal, incomeCategoryId);
        allCategories.Add(newIncomeCategory);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCategories;
    }

    public static IncomeCategory Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM incomeCategories WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int incomeCategoryId = 0;
      string incomeCategoryName = "";
      double incomeCategoryTotal = 0;
      int incomeAccountId = 0;
      while(rdr.Read())
      {
        incomeCategoryId = rdr.GetInt32(0);
        incomeCategoryName = rdr.GetString(1);
        incomeCategoryTotal = rdr.GetDouble(2);
        incomeAccountId = rdr.GetInt32(3);
      }
      IncomeCategory newIncomeCategory = new IncomeCategory(incomeAccountId, incomeCategoryName, incomeCategoryTotal, incomeCategoryId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newIncomeCategory;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO incomeCategories (name, account_id) VALUES (@name, @accountId);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);
      MySqlParameter accountId = new MySqlParameter();
      accountId.ParameterName = "@accountId";
      accountId.Value = this._account_id;
      cmd.Parameters.Add(accountId);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<IncomeItem> GetIncomeItems()
    {
      List<IncomeItem> allIncomeCategoryItems = new List<IncomeItem>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM incomeItems WHERE incomeCategory_id = @incomeCategory_id;";
      MySqlParameter incomeCategoryId = new MySqlParameter();
      incomeCategoryId.ParameterName = "@incomeCategory_id";
      incomeCategoryId.Value = this._id;
      cmd.Parameters.Add(incomeCategoryId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int incomeItemId = rdr.GetInt32(0);
        int categpryId = rdr.GetInt32(3);
        string incomeItemName = rdr.GetString(1);
        double incomeItemPrice = rdr.GetDouble(2);
        IncomeItem newIncomeItem= new IncomeItem(incomeItemName, incomeItemPrice, categpryId, incomeItemId);
        allIncomeCategoryItems.Add(newIncomeItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allIncomeCategoryItems;
    }

    public double GetTotal()
    {
      double total = 0;
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT price FROM incomeItems;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        double incomeItemPrice = rdr.GetDouble(0);
        total += incomeItemPrice;
      }
      // _total = total;
      return total;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM incomeCategories WHERE id = @searchId; DELETE FROM incomeItems WHERE incomeCategory_id = @searchId;";
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

    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE incomeCategories SET name = @newName WHERE id = @searchId;";
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

    public void DeleteAllIncomeItems()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM incomeItems WHERE incomeCategories_id = @searchId;";
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

    public static void DeleteAllCategories()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE  FROM incomeCategories; DELETE FROM incomeItems;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }




// ==========================TESTS =========================================
    public override bool Equals(System.Object otherIncomeCategory)
    {
      if (!(otherIncomeCategory is IncomeCategory))
      {
        return false;
      }
      else
      {
        IncomeCategory newIncomeCategory = (IncomeCategory) otherIncomeCategory;
        bool idEquality = this.GetId().Equals(newIncomeCategory.GetId());
        bool nameEquality = this.GetName().Equals(newIncomeCategory.GetName());
        return (idEquality && nameEquality);
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
      cmd.CommandText = @"DELETE FROM incomeCategories;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
