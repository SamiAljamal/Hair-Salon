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


    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
