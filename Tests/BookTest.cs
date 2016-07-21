using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using Library.Objects;

namespace Library.Tests
{
  public class BookTest : IDisposable
  {
    public BookTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Author.DeleteAll();
      Patron.DeleteAll();
      Book.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Book.GetAll().Count;
      //Assert
      Assert.Equal(0,result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfNamesAndEnrollmentDatesAreTheSame()
    {
      //Arrange, Act
      Book firstBook = new Book("History");
      Book secondBook = new Book("History");

      //Assert
      Assert.Equal(firstBook, secondBook);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Book testBook = new Book("History");

      //Act
      testBook.Save();
      List<Book> result = Book.GetAll();
      List<Book> testList = new List<Book>{testBook};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Book testBook = new Book("History");

      //Act
      testBook.Save();
      Book savedBook = Book.GetAll()[0];

      int result = savedBook.GetId();
      int testId = testBook.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsBookInDatabase()
    {
      //Arrange
      Book testBook = new Book("History");
      testBook.Save();

      //Act
      Book foundBook = Book.Find(testBook.GetId());

      //Assert
      Assert.Equal(testBook, foundBook);
    }
  }
}
