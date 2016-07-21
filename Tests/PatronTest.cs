using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using Library.Objects;

namespace Library.Tests
{
  public class PatronTest : IDisposable
  {
    public PatronTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Patron.DeleteAll();
      Author.DeleteAll();
      // Student.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Patron.GetAll().Count;
      //Assert
      Assert.Equal(0,result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfNamesAndIdsAreTheSame()
    {
      //Arrange, Act
      Patron firstPatron = new Patron("Adam");
      Patron secondPatron = new Patron("Adam");

      //Assert
      Assert.Equal(firstPatron, secondPatron);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Patron testPatron = new Patron("Adam");

      //Act
      testPatron.Save();
      List<Patron> result = Patron.GetAll();
      List<Patron> testList = new List<Patron>{testPatron};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Patron testPatron = new Patron("History");

      //Act
      testPatron.Save();
      Patron savedPatron = Patron.GetAll()[0];

      int result = savedPatron.GetId();
      int testId = testPatron.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsPatronInDatabase()
    {
      //Arrange
      Patron testPatron = new Patron("History");
      testPatron.Save();

      //Act
      Patron foundPatron = Patron.Find(testPatron.GetId());

      //Assert
      Assert.Equal(testPatron, foundPatron);
    }
  }
}
