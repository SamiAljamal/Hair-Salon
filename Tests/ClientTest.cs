using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Salon
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog= hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
   public void Test_Equal_ReturnsTrueForSameNameClient()
   {
     Client firstClient = new Client("John", 1);
     Client secondClient = new Client("John", 1);

     Assert.Equal(firstClient,secondClient);
   }

   [Fact]
    public void Test_GetAll_ClientsEmptyAtFirst()
    {
      //Arrange, Act
      int result = Client.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SaveClientstoDB()
    {
      Client testClient = new Client("john", 1);
      testClient.Save();

      List<Client> clients = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      Assert.Equal(testList,clients);

    }



   public void Dispose()
   {
     Client.DeleteAll();
   }
  }
}
