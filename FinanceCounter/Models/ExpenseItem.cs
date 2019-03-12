using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace FinanceCounter.Models
{
  public class ExpenseItem
  {
    private string _name;
    private int _id;
    private double _price;
    private int _expenseCategoryId;

    public ExpenseItem (string name, double price, int expenseCategoryId, int id = 0)
    {
      _name = name;
      _expenseCategoryId = expenseCategoryId;
      _id = id;
      _price = price;
    }

    public string GetName(){return _name;}
    public double GetPrice(){return _price;}
    public int GetId(){ return _id;}
    public int GetExpenseCategoryId(){ return _expenseCategoryId;}

    public void SetName(string newName){ _name = newName;}
    public void SetPrice(double newPrice){ _price = newPrice;}

    public static List<ExpenseItem> GetAll()
    {
      List<ExpenseItem> allExpenseItems = new List<ExpenseItem> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM expenseItems;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int expenseItemId = rdr.GetInt32(0);
        string expenseItemName = rdr.GetString(1);
        double expenseItemPrice = rdr.GetDouble(2);
        int expenseItemCategoryId = rdr.GetInt32(3);
        ExpenseItem newExpenseItem = new ExpenseItem(expenseItemName, expenseItemPrice, expenseItemCategoryId, expenseItemId);
        allExpenseItems.Add(newExpenseItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allExpenseItems;
    }

    public static ExpenseItem Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM expenseItems WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int expenseItemId = 0;
      string expenseItemName = "";
      int expenseItemCategoryId = 0;
      double expenseItemPrice = 0;
      while(rdr.Read())
      {
        expenseItemId = rdr.GetInt32(0);
        expenseItemName = rdr.GetString(1);
        expenseItemPrice = rdr.GetDouble(2);
        expenseItemCategoryId = rdr.GetInt32(3);
      }
      ExpenseItem newExpenseItem = new ExpenseItem(expenseItemName, expenseItemPrice, expenseItemCategoryId, expenseItemId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newExpenseItem;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO expenseItems (name, price, expenseCategory_id) VALUES (@name, @price, @expenseCategory_id);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);
      MySqlParameter expenseCategoryId = new MySqlParameter();
      expenseCategoryId.ParameterName = "@expenseCategory_id";
      expenseCategoryId.Value = this._expenseCategoryId;
      cmd.Parameters.Add(expenseCategoryId);
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
      cmd.CommandText = @"UPDATE expenseItems SET name = @newName WHERE id = @searchId;";
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
      cmd.CommandText = @"UPDATE expenseItems SET name = @newName, price = newPrice WHERE id = @searchId;";
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
      cmd.CommandText = @"DELETE FROM expenseItems WHERE id = @searchId;";
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

    public override bool Equals(System.Object otherExpenseItem)
    {
      if (!(otherExpenseItem is ExpenseItem))
      {
        return false;
      }
      else
      {
         ExpenseItem newExpenseItem = (ExpenseItem) otherExpenseItem;
         bool idEquality = this.GetId() == newExpenseItem.GetId();
         bool nameEquality = this.GetName() == newExpenseItem.GetName();
         bool expenseCategoryEquality = this.GetExpenseCategoryId() == newExpenseItem.GetExpenseCategoryId();
         return (idEquality && nameEquality && expenseCategoryEquality);
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
      cmd.CommandText = @"DELETE FROM expenseItems;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
       conn.Dispose();
      }
    }
  }
}
