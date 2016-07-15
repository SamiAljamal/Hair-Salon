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
      _id = Id;
      _name = Name;
      _stylist_id = stylistId;
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

  }
}
