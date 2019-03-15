using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace FinanceCounter.Models
{
  public class ExpenseCategory
  {
    private int _id;
    private int _account_id;
    private string _name;
    private double _total;

    public ExpenseCategory(int account_id, string name, double total = 0.00, int id = 0)
    {
      _account_id = account_id;
      _name = name;
      _id = id;
      _total = total;

    }

    public string GetName(){return _name;}
    public int GetId(){return _id;}

    public void SetName(string newName){_name = newName;}

    public static List<ExpenseCategory> GetAll()
    {
      List<ExpenseCategory> allExpenseCategories = new List<ExpenseCategory> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM expenseCategories;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int expenseCategoryId = rdr.GetInt32(0);
        string expenseCategoryName = rdr.GetString(1);
        double expenseCategoryTotal = rdr.GetDouble(2);
        int expenseAccountId = rdr.GetInt32(3);
        ExpenseCategory newExpenseCategory = new ExpenseCategory(expenseAccountId, expenseCategoryName, expenseCategoryTotal, expenseCategoryId);
        allExpenseCategories.Add(newExpenseCategory);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allExpenseCategories;
    }

    public static ExpenseCategory Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM expenseCategories WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int expenseCategoryId = 0;
      string expenseCategoryName = "";
      double expenseCategoryTotal = 0;
      int expenseAccountId = 0;
      while(rdr.Read())
      {
        expenseCategoryId = rdr.GetInt32(0);
        expenseCategoryName = rdr.GetString(1);
        expenseCategoryTotal = rdr.GetDouble(2);
        expenseAccountId = rdr.GetInt32(3);
      }
      ExpenseCategory newExpenseCategory = new ExpenseCategory(expenseAccountId, expenseCategoryName, expenseCategoryTotal, expenseCategoryId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newExpenseCategory;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO expenseCategories (name, total, account_id) VALUES (@name, @total, @accountId);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);
      MySqlParameter accountId = new MySqlParameter();
      accountId.ParameterName = "@accountId";
      accountId.Value = this._account_id;
      cmd.Parameters.Add(accountId);
      MySqlParameter total = new MySqlParameter();
      total.ParameterName = "@total";
      total.Value = this.GetTotal();
      cmd.Parameters.Add(total);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<ExpenseItem> GetExpenseItems()
    {
      List<ExpenseItem> allExpenseCategoryItems = new List<ExpenseItem>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM expenseItems WHERE expenseCategory_id = @expenseCategory_id;";
      MySqlParameter expenseCategoryId = new MySqlParameter();
      expenseCategoryId.ParameterName = "@expenseCategory_id";
      expenseCategoryId.Value = this._id;
      cmd.Parameters.Add(expenseCategoryId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int expenseItemId = rdr.GetInt32(0);
        int categpryId = rdr.GetInt32(3);
        string expenseItemName = rdr.GetString(1);
        double expenseItemPrice = rdr.GetDouble(2);
        ExpenseItem newExpenseItem= new ExpenseItem(expenseItemName, expenseItemPrice, categpryId, expenseItemId);
        allExpenseCategoryItems.Add(newExpenseItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allExpenseCategoryItems;
    }

    public double GetTotal()
    {

      double totalExpense = 0;
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT price FROM expenseItems;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        double expenseCategoryTotal = rdr.GetDouble(0);
        totalExpense += expenseCategoryTotal;
      }
      return totalExpense;
    }

    // public double GetTotal()
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = "SELECT price FROM expenseItems WHERE expenseCategory_id = @searchId;";
    //   MySqlParameter searchId = new MySqlParameter();
    //   searchId.ParameterName = "@searchId";
    //   searchId.Value = this._id;
    //   cmd.Parameters.Add(searchId);
    //   var rdr = cmd.ExecuteReader() as MySqlDataReader;
    //   double totalExpense = 0;
    //   while(rdr.Read())
    //   {
    //     double expenseItemPrice = rdr.GetDouble(0);
    //     totalExpense += expenseItemPrice;
    //   }
    //   this._total = totalExpense;
    //   return totalExpense;
    // }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM expenseCategories WHERE id = @searchId; DELETE FROM expenseItems WHERE expenseCategory_id = @searchId;";
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
      cmd.CommandText = @"UPDATE expenseCategories SET name = @newName WHERE id = @searchId;";
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

    public void DeleteAllExpenseItems()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM expenseItems WHERE expenseCategories_id = @searchId;";
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

    public static void DeleteAllExpenseCategories()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE  FROM expenseCategories; DELETE FROM expenseItems;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }




// ==========================TESTS =========================================
    public override bool Equals(System.Object otherExpenseCategory)
    {
      if (!(otherExpenseCategory is ExpenseCategory))
      {
        return false;
      }
      else
      {
        ExpenseCategory newExpenseCategory = (ExpenseCategory) otherExpenseCategory;
        bool idEquality = this.GetId().Equals(newExpenseCategory.GetId());
        bool nameEquality = this.GetName().Equals(newExpenseCategory.GetName());
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
      cmd.CommandText = @"DELETE FROM expenseCategories;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
