using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Salon
{
  public class Client
  {
    private int _id;
    private string _name;
    private int _stylist_id;

    public Client(string Name, int stylistId, int Id= 0)
    {
      _name = Name;
      _stylist_id = stylistId;
        _id = Id;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public int GetStylistId()
    {
      return _stylist_id;
    }

    public void SetStylistID(int stylistID)
    {
      _stylist_id = stylistID;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client ))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = this.GetId()  == newClient.GetId();
        bool nameEquality = this.GetName() == newClient.GetName();
        return (idEquality && nameEquality);
      }
    }

    public static List<Client> GetAll()
   {
     List<Client> clients =  new List<Client>{};

     SqlConnection conn = DB.Connection();
     SqlDataReader rdr = null;
     conn.Open();

     SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
     rdr = cmd.ExecuteReader();

     while(rdr.Read())
     {
       string clientName = rdr.GetString(0);
       int stylistId = rdr.GetInt32(1);
       int clientID = rdr.GetInt32(2);
       Client client = new Client(clientName, stylistId, clientID);
       clients.Add(client);
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

   public static void DeleteAll()
   {
     SqlConnection conn = DB.Connection();
     conn.Open();
     SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
     cmd.ExecuteNonQuery();
   }

   public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @id;", conn);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@id";
    stylistIdParameter.Value = this.GetId();

      cmd.Parameters.Add(stylistIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

   public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, stylist_id) OUTPUT INSERTED.id VALUES (@clientName, @stylistId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@clientName";
      nameParameter.Value = this.GetName();

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@stylistId";
      stylistIdParameter.Value = this.GetStylistId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(stylistIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
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

    public static Client Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients  WHERE id = @stylistId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@stylistId";
      idParameter.Value = id.ToString();
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();

      string foundClientName = null;
      int foundClientStylistId = 0;
      int foundClientId = 0;


      while(rdr.Read())
      {
        foundClientName = rdr.GetString(0);
        foundClientStylistId = rdr.GetInt32(1);
        foundClientId = rdr.GetInt32(2);
      }
      Client foundClient = new Client(
        foundClientName,
        foundClientStylistId,
        foundClientId
      );

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundClient;
    }
    public void Update(string newName, int newStylist)
   {
     _name = newName;
     _stylist_id = newStylist;
     SqlConnection conn = DB.Connection();
     SqlDataReader rdr = null;
     conn.Open();

     SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @clientName, stylist_id = @stylistId where id = @id;", conn);

     SqlParameter nameParameter = new SqlParameter();
     nameParameter.ParameterName = "@clientName";
     nameParameter.Value = newName;

     SqlParameter stylistIdParameter = new SqlParameter();
     stylistIdParameter.ParameterName = "@stylistId";
     stylistIdParameter.Value = this.GetStylistId();

     SqlParameter idParameter = new SqlParameter();
     idParameter.ParameterName = "@id";
     idParameter.Value = this.GetId();

     cmd.Parameters.Add(nameParameter);
     cmd.Parameters.Add(idParameter);
     cmd.Parameters.Add(stylistIdParameter);
     rdr = cmd.ExecuteReader();
     if (rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }
   }





  }
}
