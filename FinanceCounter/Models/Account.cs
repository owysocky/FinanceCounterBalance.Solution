using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace FinanceCounter.Models
{
  public class Account
  {
    private int _id;
    private string _name;
    private double _balance;

    public Account(int id = 0, string name, double balance = 0)
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
