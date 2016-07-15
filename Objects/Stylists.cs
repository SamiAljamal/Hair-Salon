using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Salon
{
  public class Stylist
  {
    private int _id;
    private string _name;

    public Stylist(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool nameEquality = this.GetName() == newStylist.GetName();
        return nameEquality;
      }
    }
    public static void DeleteAll()
   {
     SqlConnection conn = DB.Connection();
     conn.Open();
     SqlCommand cmd = new SqlCommand("DELETE FROM stylist;", conn);
     cmd.ExecuteNonQuery();
   }
   
   public static List<Stylist> GetAll()
   {
     List<Stylist> stylists =  new List<Stylist>{};

     SqlConnection conn = DB.Connection();
     SqlDataReader rdr = null;
     conn.Open();

     SqlCommand cmd = new SqlCommand("SELECT * FROM stylist;", conn);
     rdr = cmd.ExecuteReader();

     while(rdr.Read())
     {
       string stylistName = rdr.GetString(0);
       int stylistId = rdr.GetInt32(1);
       Stylist stylist = new Stylist(stylistName, stylistId);
       stylists.Add(stylist);
     }

     if (rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }

     return stylists;
   }

   public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO stylist (name) OUTPUT INSERTED.id VALUES (@StylistName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@StylistName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }




  }
}
