using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace FinanceCounter.Models
{
  public class Account
  {
    private int _id;
    private string _name;
    private double _balance;

    public Account(string name, double balance = 0, int id = 0)
    {
      _id = id;
      _name = name;
      _balance = balance;
    }

    public string GetName(){return _name;}
    public int GetId(){return _id;}
    public void SetName(string newName){_name = newName;}

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO accounts (name) VALUES (@name);";
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

    public static List<Account> GetAll()
    {
      List<Account> allAccount = new List<Account>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM accounts;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int accountId = rdr.GetInt32(0);
        string accountName = rdr.GetString(1);
        double accountBalance = rdr.GerDouble(2);
        Account newAccount = new Account(accountName, accountBalance, accountId);
        allAccount.Add(newAccount);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allAccount;
    }

    public static Account Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM accounts WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int accountId = 0;
      string accountName = "";
      double accountBalance = 0;
      while(rdr.Read())
      {
        accountId = rdr.GetInt32(0);
        accountName = rdr.GetString(1);
        accountBalance = rdr.GerDouble(2);
      }
      Account newAccount = new Account(accountName, accountBalance, accountId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newAccount;
    }

    public List<IncomeCategory> GetIncomeCategories()
    {
      List<IncomeCategory> allIncomeCategory = new List<IncomeCategory>{};
      MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = "SELECT * FROM incomeCategory WHERE account_id = @account_id;";
     MySqlParameter accountId = new MySqlParameter();
      accountId.ParameterName = "@account_id";
      accountId.Value = this._id;
      cmd.Parameters.Add(accountId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int categpryId = rdr.GetInt32(0);
        string categoryName = rdr.GetString(1);
        double categoyTotal = rdr.GetDouble(2);
        int categoryAccountId = rdr.GetInt32(3);
        IncomeCategory newIncomeCategory = new IncomeCategory(categoryAccountId, categoryName, categoyTotal, categpryId);
        allIncomeCategory.Add(newIncomeCategory);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allIncomeCategory;
    }

    public List<ExpenseCategory> GetExpenseCategories()
    {
      List<ExpenseCategory> allExpenseCategory = new List<ExpenseCategory>{};
      MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = "SELECT * FROM expenseCategory WHERE account_id = @account_id;";
     MySqlParameter accountId = new MySqlParameter();
      accountId.ParameterName = "@account_id";
      accountId.Value = this._id;
      cmd.Parameters.Add(accountId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int categpryId = rdr.GetInt32(0);
        string categoryName = rdr.GetString(1);
        double categoyTotal = rdr.GetDouble(2);
        int categoryAccountId = rdr.GetInt32(3);
        ExpenseCategory newExpenseCategory = new ExpenseCategory(categoryAccountId, categoryName, categoyTotal, categpryId);
        allExpenseCategory.Add(newExpenseCategory);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allExpenseCategory;
    }

    public double GetTotalIncome()
    {
      double totalIncome = 0;
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT total FROM accounts;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        double AccountTotal = rdr.GetDouble(0);
        totalIncome += AccountTotal;
      }
      return totalIncome;
    }

    public double GetTotalExpense()
    {
      double totalExpense = 0;
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT total FROM expenseCategories;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        double expenseCategoryTotal = rdr.GetDouble(0);
        totalExpense += expenseCategoryTotal;
      }
      return totalExpense;
    }

    public double GetBalance()
    {
      double balance =  GetTotalIncome() - GetTotalExpense();
      _balance = balance;
      return balance;
    }

    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE accounts SET name = @newName WHERE id = @searchId;";
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

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM accounts WHERE id = @searchId; DELETE FROM expenseCategories WHERE account_id = @searchId; DELETE FROM incomeCategories WHERE account_id = @searchId; DELETE FROM incomeItems WHERE incomeCategory_id IN (SELECT id FROM incomeCategories WHERE account_id = @searchId; DELETE FROM expenseItems WHERE expenseCategory_id IN (SELECT id FROM expenseCategories WHERE account_id = @searchId;)";
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

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE  FROM accounts; DELETE FROM incomeCategories; DELETE FROM expenseCategories; DELETE  FROM incomeItems; DELETE  FROM expenseItems;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }



// ==========================TESTS =========================================
    public override bool Equals(System.Object otherAccount)
    {
      if (!(otherAccount is Account))
      {
        return false;
      }
      else
      {
        Account newAccount = (Account) otherAccount;
        bool idEquality = this.GetId().Equals(newAccount.GetId());
        bool nameEquality = this.GetName().Equals(newAccount.GetName());
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
      cmd.CommandText = @"DELETE FROM accounts;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
