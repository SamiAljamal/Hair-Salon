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
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog= hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
   public void Test_Equal_ReturnsTrueForSameNameForStylist()
   {
     Stylist firstStylist = new Stylist("jill");
     Stylist secondStylist = new Stylist("jill");

     Assert.Equal(firstStylist,secondStylist);
   }

   [Fact]
   public void Test_Save_SaveClientstoDB()
   {
     Stylist testStylist = new Stylist("jill");
     testStylist.Save();

     List<Stylist> stylists = Stylist.GetAll();
     List<Stylist> testList = new List<Stylist>{testStylist};

     Assert.Equal(testList,stylists);
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



    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
