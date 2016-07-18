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

    public Client(string Name,  int stylistID, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _stylist_id = stylistID;
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
    public string GetStylist()
    {
      return Stylist.Find(_stylist_id).GetName();
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
        int clientId = rdr.GetInt32(2);
        int stylistID = rdr.GetInt32(1);
        Client client = new Client(clientName, stylistID, clientId);
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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (name,stylist_id) OUTPUT INSERTED.id VALUES (@ClientName, @StylistID);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@ClientName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistID";
      stylistIdParameter.Value = this.GetStylistId();
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
      if(conn != null)
      {
        conn.Close();
      }
    }

    public void Update(string newName, int stylist_id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @NewName,  stylist_id = @StylistID OUTPUT INSERTED.name, INSERTED.stylist_id WHERE id = @CategoryId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistID";
      stylistIdParameter.Value = stylist_id;
      cmd.Parameters.Add(stylistIdParameter);

      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@CategoryId";
      categoryIdParameter.Value = this.GetId();
      cmd.Parameters.Add(categoryIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
        this._stylist_id = rdr.GetInt32(1);

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

      SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @id;", conn);

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
      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
    }

    public static Client Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients  WHERE id = @ClientId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@ClientId";
      idParameter.Value = id.ToString();
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();

      string foundClientName = null;
      int foundClientStylistId = 0;
      int foundClientId = 0;

      while(rdr.Read())
      {
        foundClientName = rdr.GetString(0);
        Console.WriteLine("hello");
        foundClientStylistId = rdr.GetInt32(1);
        foundClientId = rdr.GetInt32(2);
        Console.WriteLine(foundClientId.ToString());
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
  }
}
