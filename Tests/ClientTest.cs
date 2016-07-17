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
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameNameIDPhoneNumber()
    {
      Client firstClient = new Client("lardo", 1);
      Client secondClient = new Client("lardo", 1);

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
      Client testClient = new Client("lardo", 1);
      testClient.Save();

      List<Client> restaurants = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      Assert.Equal(testList,restaurants);

    }
    [Fact]
    public void Test_DeleteAll_DeletesClientsFromDB()
    {
      //Arrange
      Client firstClient = new Client("lardo", 1);
      Client secondClient = new Client("Chaba Thai", 1);
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
      Client firstClient = new Client("lardo", 0);
      Client secondClient = new Client("Chaba Thai", 1);
      firstClient.Save();
      secondClient.Save();

      //Act
      Client result = Client.Find(secondClient.GetId());

      string nameTest = result.GetName();
      Console.WriteLine("From Find:  " + nameTest);

      string gaName = Client.GetAll()[1].GetName();
      Console.WriteLine("From GetAll:  " + gaName);

      //Assert
      Assert.Equal("Chaba Thai", nameTest);
    }
    [Fact]
    public void Test_Delete_DeletesClientFromDB()
    {
      //Arrange
      //Arrange
      Client firstClient = new Client("lardo", 0);
      Client secondClient = new Client("Chaba Thai", 1);
      firstClient.Save();
      secondClient.Save();
      //Act
      firstClient.Delete();
      List<Client> resultREstaurants = Client.GetAll();
      List<Client> testClientList = new List<Client> {secondClient};



      //Assert
      Assert.Equal(testClientList, resultREstaurants);
    }

    [Fact]
      public void Test_Update_UpdatesClientInDatabase()
      {
        //Arrange
        //Arrange
        Client firstClient = new Client("lardo", 0);
        Client secondClient = new Client("Chaba Thai", 1);
        firstClient.Save();
        secondClient.Save();

        Client result = Client.Find(firstClient.GetId());

        //Act
        result.Update(secondClient.GetName(), secondClient.GetStylistId());

        Client updatedResult = Client.Find(firstClient.GetId());

        //Assert
        Assert.Equal(secondClient.GetName(), updatedResult.GetName());
        Assert.Equal(secondClient.GetStylistId() , updatedResult.GetStylistId());
      }

      [Fact]
      public void Test_GetStylist_ReturnsNameForInputID()
      {

        //Arrange
        Stylist firstStylist = new Stylist("Thai");
        firstStylist.Save();

        Client firstClient = new Client("lardo", firstStylist.GetId());

        //Act
        string result = firstClient.GetStylist();

        Assert.Equal("Thai",result);
      }

    //
    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
