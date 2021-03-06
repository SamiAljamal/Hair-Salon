using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Salon
{
  public class Stylist
  {
    private int _id;
    private string _name;

    public Stylist(string Name,  int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist ))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        //bool idEquality = this.GetId()  == newStylist.GetId();
        bool nameEquality = this.GetName() == newStylist.GetName();
        return nameEquality;
      }
    }

    public void SetId(int ID)
    {
      _id = ID;
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
    public static List<Stylist> GetAll()
    {
      List<Stylist> cuisines =  new List<Stylist>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        string cuisineName = rdr.GetString(0);
        int cuisineId = rdr.GetInt32(1);
        Stylist cuisine = new Stylist(cuisineName, cuisineId);
        cuisines.Add(cuisine);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return cuisines;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name) OUTPUT INSERTED.id VALUES (@StylistName);", conn);

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

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE stylists SET name = @NewName OUTPUT INSERTED.name WHERE id = @CategoryId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);


      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@CategoryId";
      categoryIdParameter.Value = this.GetId();
      cmd.Parameters.Add(categoryIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM stylists WHERE id = @id;", conn);

      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@id";
      categoryIdParameter.Value = this.GetId();

      cmd.Parameters.Add(categoryIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
      cmd.ExecuteNonQuery();
    }

    public static Stylist Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists  WHERE id = @StylistId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@StylistId";
      idParameter.Value = id.ToString();
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();

      string foundStylistName = null;
      int foundStylistId = 0;


      while(rdr.Read())
      {
        foundStylistName = rdr.GetString(0);
        foundStylistId = rdr.GetInt32(1);
      }
      Stylist foundStylist = new Stylist(
        foundStylistName,
        foundStylistId
      );

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundStylist;
    }

    public static List<Client> GetClients(int StylistID)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylist_id = @CategoryId;", conn);
      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@CategoryId";
      categoryIdParameter.Value = StylistID;
      cmd.Parameters.Add(categoryIdParameter);
      rdr = cmd.ExecuteReader();

      List<Client> clients = new List<Client> {};
      while(rdr.Read())
      {
        string clientName= rdr.GetString(0);
        int clientStylistId = rdr.GetInt32(1);
        int id = rdr.GetInt32(2);
        Client newClient = new Client(clientName, clientStylistId, id);
        clients.Add(newClient);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return clients;
    }
  }
}
