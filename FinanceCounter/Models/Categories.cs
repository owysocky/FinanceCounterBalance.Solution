using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Stylist
  {
    private int _id;
    private string _name;
    private double _total;

    public Stylist(string name, double total; int id = 0)
    {
      _name = name;
      _id = id;
      _total = total;

    }

    public string GetName(){return _name;}
    public int GetId(){return _id;}
    public double GetTotal(){return _total;}

    public void SetName(string newName){_name = newName;}
    public void SetTotal(double newTotal){_total = newTotal;}



    // public override bool Equals(System.Object otherStylist)
    // {
    //     if (!(otherStylist is Stylist))
    //     {
    //       return false;
    //     }
    //     else
    //     {
    //       Stylist newStylist = (Stylist) otherStylist;
    //       bool idEquality = this.GetId().Equals(newStylist.GetId());
    //       bool nameEquality = this.GetName().Equals(newStylist.GetName());
    //       return (idEquality && nameEquality);
    //     }
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
    //   cmd.CommandText = @"DELETE  FROM stylists;";
    //   cmd.ExecuteNonQuery();
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    // }
  }
}
