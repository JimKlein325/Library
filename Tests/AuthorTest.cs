using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using Library.Objects;

namespace Library.Tests
{
  public class AuthorTest : IDisposable
  {
    public AuthorTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Author.DeleteAll();
      // Course.DeleteAll();
      // Student.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Author.GetAll().Count;
      //Assert
      Assert.Equal(0,result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfNamesAndIdsAreTheSame()
    {
      //Arrange, Act
      Author firstAuthor = new Author("J.R.R. Tolkein");
      Author secondAuthor = new Author("J.R.R. Tolkein");

      //Assert
      Assert.Equal(firstAuthor, secondAuthor);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Author testAuthor = new Author("History");

      //Act
      testAuthor.Save();
      List<Author> result = Author.GetAll();
      List<Author> testList = new List<Author>{testAuthor};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Author testAuthor = new Author("History");

      //Act
      testAuthor.Save();
      Author savedAuthor = Author.GetAll()[0];

      int result = savedAuthor.GetId();
      int testId = testAuthor.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsAuthorInDatabase()
    {
      //Arrange
      Author testAuthor = new Author("History");
      testAuthor.Save();

      //Act
      Author foundAuthor = Author.Find(testAuthor.GetId());

      //Assert
      Assert.Equal(testAuthor, foundAuthor);
    }
  }
}
