using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Salon
{
  public class CustomerTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=salon_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Customer.DeleteAll();
    }
  }
}
