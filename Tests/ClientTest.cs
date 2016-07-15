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

    [Fact]
    public void Test_DeleteAll_DeletesClientsFromDB()
    {
      //Arrange
      Client firstClient = new Client("john", 1);
      Client secondClient = new Client("kerry", 1);
      firstClient.Save();
      secondClient.Save();

      //Act
      Client.DeleteAll();
      int result = Client.GetAll().Count;

      //Assert
      Assert.Equal(0,result);
    }

    [Fact]
   public void Test_Find_FindsClientAdded()
   {
     //Arrange
     Client firstClient = new Client("john", 0);
     Client secondClient = new Client("kerry", 1);
     firstClient.Save();
     secondClient.Save();

     //Act
     Client result = Client.Find(secondClient.GetId());

     string nameTest = result.GetName();

     string gaName = Client.GetAll()[1].GetName();

     //Assert
     Assert.Equal("kerry", nameTest);
   }
   [Fact]
   public void Test_Delete_DeletesClientFromDB()
   {
     //Arrange
     //Arrange
     Client firstClient = new Client("joh ", 0);
     Client secondClient = new Client("jerry", 1);
     firstClient.Save();
     secondClient.Save();
     //Act
     firstClient.Delete();
     List<Client> resultClients = Client.GetAll();
     List<Client> testClientList = new List<Client> {secondClient};



     //Assert
     Assert.Equal(testClientList, resultClients);
   }

   [Fact]
    public void Test_Update_UpdatesNameInDatabase()
    {
      //Arrange
      Client firstClient = new Client("john", 1);
      firstClient.Save();

      //Act
      firstClient.Update("kerry", 3);
      Client resultClient = Client.Find(firstClient.GetId());

      //Assert
      Assert.Equal("kerry", firstClient.GetName());
      Assert.Equal("kerry", resultClient.GetName());
      Assert.Equal(3 , firstClient.GetStylistId());
      Assert.Equal(3 , resultClient.GetStylistId());
    }






   public void Dispose()
   {
     Client.DeleteAll();
   }
  }
}
