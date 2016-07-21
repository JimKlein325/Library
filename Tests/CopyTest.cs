using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using Library.Objects;

namespace Library.Tests
{
  public class CopyTest : IDisposable
  {
    private DateTime? dueDate = new DateTime(2016, 7, 12);
    public CopyTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Copy.DeleteAll();
      Patron.DeleteAll();
      // Student.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Copy.GetAll().Count;
      //Assert
      Assert.Equal(0,result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfNamesAndIdsAreTheSame()
    {
      //Arrange, Act
      Book testBook = new Book("J.R.R. Tolkein: A Life");
      testBook.Save();

      Copy firstCopy = new Copy(testBook.GetId(), dueDate);
      Copy secondCopy = new Copy(testBook.GetId(), dueDate);

      //Assert
      Assert.Equal(firstCopy, secondCopy);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Book testBook = new Book("J.R.R. Tolkein: A Life");
      testBook.Save();
      Copy testCopy = new Copy(testBook.GetId(), dueDate);

      //Act
      testCopy.Save();
      List<Copy> result = Copy.GetAll();
      List<Copy> testList = new List<Copy>{testCopy};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Book testBook = new Book("J.R.R. Tolkein: A Life");
      testBook.Save();
      Copy testCopy = new Copy(testBook.GetId(), dueDate);

      //Act
      testCopy.Save();
      Copy savedCopy = Copy.GetAll()[0];

      int result = savedCopy.GetId();
      int testId = testCopy.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsCopyInDatabase()
    {
      //Arrange
      Book testBook = new Book("J.R.R. Tolkein: A Life");
      testBook.Save();
      Copy testCopy = new Copy(testBook.GetId(), dueDate);
      testCopy.Save();

      //Act
      Copy foundCopy = Copy.Find(testCopy.GetId());

      //Assert
      Assert.Equal(testCopy, foundCopy);
    }
    [Fact]
    public void Test_GetTitle_ForCopyInDatabase()
    {
      //Arrange
      string title = "J.R.R. Tolkein: A Life";
      Book testBook = new Book(title);
      testBook.Save();
      Copy testCopy = new Copy(testBook.GetId(), dueDate);
      testCopy.Save();

      //Actf
      string result = testCopy.GetTitle();

      //Assert
      Assert.Equal(title, result);
    }
  }
}
