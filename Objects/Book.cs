using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Library.Objects
{
  public class Book
  {
    private int _id;
    private string _title;

    public Book(string title, int Id = 0)
    {
      _id = Id;
      _title = title;
    }

    public override bool Equals(System.Object otherBook)
    {
      if (!(otherBook is Book))
      {
        return false;
      }
      else
      {
        Book newBook = (Book) otherBook;
        bool idEquality = (this.GetId() == newBook.GetId());
        bool titleEquality = (this.GetTitle() == newBook.GetTitle());
        return (idEquality && titleEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetTitle()
    {
      return _title;
    }

    public static List<Book> GetAll()
    {
      List<Book> allBooks = new List<Book> {};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM books;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        string courseBook = rdr.GetString(0);
        int bookId = rdr.GetInt32(1);
        Book newBook = new Book(courseBook, bookId);
        allBooks.Add(newBook);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allBooks;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO books (title) OUTPUT INSERTED.id VALUES (@BookTitle);", conn);

      SqlParameter titleParameter = new SqlParameter();
      titleParameter.ParameterName = "@BookTitle";
      titleParameter.Value = this.GetTitle();

      cmd.Parameters.Add(titleParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Book Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM books WHERE id = @BookId;", conn);
      SqlParameter courseIdParameter = new SqlParameter();
      courseIdParameter.ParameterName = "@BookId";
      courseIdParameter.Value = id.ToString();
      cmd.Parameters.Add(courseIdParameter);
      rdr = cmd.ExecuteReader();


      int foundBookId = 0;
      string foundBookTitle = null;

      while(rdr.Read())
      {
        foundBookTitle = rdr.GetString(0);
        foundBookId = rdr.GetInt32(1);
      }
      Book foundBook = new Book(foundBookTitle, foundBookId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundBook;
    }

    public static void DeleteAll()
     {
       SqlConnection conn = DB.Connection();
       conn.Open();
       SqlCommand cmd = new SqlCommand("DELETE FROM authors; DELETE FROM patrons; DELETE FROM books;", conn);
       cmd.ExecuteNonQuery();
     }

  }
}
