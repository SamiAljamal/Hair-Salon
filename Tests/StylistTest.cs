using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Salon
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameNameForStylist()
    {
      Stylist firstStylist = new Stylist("lardo");
      Stylist secondStylist = new Stylist("lardo");

      Assert.Equal(firstStylist,secondStylist);
    }

    [Fact]
    public void Test_GetAll_StylistsEmptyAtFirst()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SaveStyliststoDB()
    {
      Stylist testStylist = new Stylist("Bavarian");
      testStylist.Save();

      List<Stylist> stylists = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      Assert.Equal(testList,stylists);

    }
    [Fact]
    public void Test_DeleteAll_DeletesStylistsFromDB()
    {
      //Arrange
      Stylist firstStylist = new Stylist("Greek");
      Stylist secondStylist = new Stylist("TexMex");
      firstStylist.Save();
      secondStylist.Save();

      //Act
      Stylist.DeleteAll();
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.Equal(0,result);
    }

    [Fact]
    public void Test_Find_FindsStylistAdded()
    {
      //Arrange
      Stylist firstStylist = new Stylist("Greek");
      Stylist secondStylist = new Stylist("TexMex");
      firstStylist.Save();
      secondStylist.Save();

      //Act
      Stylist result = Stylist.Find(secondStylist.GetId());

      string nameTest = result.GetName();
      Console.WriteLine("From Find:  " + nameTest);

      string gaName = Stylist.GetAll()[1].GetName();
      Console.WriteLine("From GetAll:  " + gaName);

      //Assert
      Assert.Equal("TexMex", nameTest);
    }

    [Fact]
    public void Test_Delete_DeletesStylistFromDB()
    {
      //Arrange
      //Arrange
      Stylist firstStylist = new Stylist("lardo");
      Stylist secondStylist = new Stylist("Chaba Thai");
      firstStylist.Save();
      secondStylist.Save();
      //Act
      firstStylist.Delete();
      List<Stylist> resultREstaurants = Stylist.GetAll();
      List<Stylist> testStylistList = new List<Stylist> {secondStylist};



      //Assert
      Assert.Equal(testStylistList, resultREstaurants);
    }

    [Fact]
      public void Test_Update_UpdatesStylistInDatabase()
      {
        //Arrange
        //Arrange
        Stylist firstStylist = new Stylist("lardo");
        Stylist secondStylist = new Stylist("Chaba Thai");
        firstStylist.Save();
        secondStylist.Save();

        Stylist result = Stylist.Find(firstStylist.GetId());

        //Act
        result.Update(secondStylist.GetName());

        Stylist updatedResult = Stylist.Find(firstStylist.GetId());

        //Assert
        Assert.Equal(secondStylist.GetName(), updatedResult.GetName());

      }
      [Fact]
      public void Test_GetClients_RetrievesAllClientssWithStylist()
      {
        //Arrange
        Stylist testStylist = new Stylist("texmex");
        testStylist.Save();

        //Act
        Client firstClient = new Client("lardo", testStylist.GetId());
        firstClient.Save();


        List<Client> testClientList = new List<Client> {firstClient};
        List<Client> resultClientList = testStylist.GetClients();

        //Assert
        Assert.Equal(testClientList[0].GetStylistId(), resultClientList[0].GetStylistId());
      }


    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
