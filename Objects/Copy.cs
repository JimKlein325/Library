using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Library.Objects
{
  public class Copy
  {
    private int _id;
    private int _bookId;
    private DateTime? _dueDate;

    public Copy(int bookId, DateTime? dueDate, int Id = 0)
    {
      _id = Id;
      _bookId = bookId;
      _dueDate = dueDate;
    }

    public override bool Equals(System.Object otherCopy)
    {
      if (!(otherCopy is Copy))
      {
        return false;
      }
      else
      {
        Copy newCopy = (Copy) otherCopy;
        bool idEquality = (this.GetId() == newCopy.GetId());
        bool bookIdEquality = (this.GetBookId() == newCopy.GetBookId());
        return (idEquality && bookIdEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public int GetBookId()
    {
      return _bookId;
    }
    public void SetBookID(int bookId)
    {
      _bookId = bookId;
    }
    public DateTime? GetDueDate()
    {
      return _dueDate;
    }
    // public void SetDueDate(DateTime? dueDate)
    // {
    //   _dueDate = dueDate;
    // }

    public static List<Copy> GetAll()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM copies;", conn);
      rdr = cmd.ExecuteReader();

      List<Copy> allCopys = new List<Copy> {};
      while(rdr.Read())
      {
        int bookId = rdr.GetInt32(0);

        DateTime? dueDate = rdr.GetDateTime(1);
        int copyId = rdr.GetInt32(2);
        Copy newCopy = new Copy(bookId, dueDate, copyId);
        allCopys.Add(newCopy);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allCopys;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO copies (book_id, due_date) OUTPUT INSERTED.id VALUES (@bookIdParameter, @DueDate);", conn);

      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@bookIdParameter";
      bookIdParameter.Value = this.GetBookId();

      cmd.Parameters.Add(bookIdParameter);

      SqlParameter dueDateParameter = new SqlParameter();
      dueDateParameter.ParameterName = "@DueDate";
      dueDateParameter.Value = this.GetDueDate();

      cmd.Parameters.Add(dueDateParameter);

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

    public static Copy Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM copies WHERE id = @CopyId;", conn);
      SqlParameter courseIdParameter = new SqlParameter();
      courseIdParameter.ParameterName = "@CopyId";
      courseIdParameter.Value = id.ToString();
      cmd.Parameters.Add(courseIdParameter);
      rdr = cmd.ExecuteReader();


      int bookId = 0;
      int copyId = 0;
      DateTime? foundDueDate = null;

      while(rdr.Read())
      {
        bookId = rdr.GetInt32(0);
        foundDueDate = rdr.GetDateTime(1);
        copyId = rdr.GetInt32(2);
      }
      Copy foundCopy = new Copy(bookId, foundDueDate, copyId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCopy;
    }
    public string GetTitle()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();


      SqlCommand cmd = new SqlCommand("SELECT books.title FROM copies JOIN books ON (copies.book_id = books.id) WHERE copies.id = @CopyId;", conn);
      SqlParameter courseIdParameter = new SqlParameter();
      courseIdParameter.ParameterName = "@CopyId";
      courseIdParameter.Value = GetId();
      cmd.Parameters.Add(courseIdParameter);
      rdr = cmd.ExecuteReader();

      string bookTitle = "";

      while(rdr.Read())
      {
        bookTitle = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return bookTitle;
    }
    public static List<Copy> GetCheckedOutCopies(Patron patron)
    {
      // SELECT copies.* FROM patrons JOIN checkouts ON (patrons.id = checkouts.patron_id) 	JOIN copies ON (checkouts.copy_id = copies.id) WHERE patrons.id = @CopyId
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT copies.* FROM patrons JOIN checkouts ON (patrons.id = checkouts.patron_id) 	JOIN copies ON (checkouts.copy_id = copies.id) WHERE patrons.id = @PatronId;", conn);
      SqlParameter courseIdParameter = new SqlParameter();
      courseIdParameter.ParameterName = "@PatronId";
      courseIdParameter.Value = patron.GetId();

      cmd.Parameters.Add(courseIdParameter);

      rdr = cmd.ExecuteReader();

      List<Copy> allCopys = new List<Copy> {};
      while(rdr.Read())
      {
        int bookId = rdr.GetInt32(0);
        Console.WriteLine("Inside loop");
        DateTime? dueDate = rdr.GetDateTime(1);
        int copyId = rdr.GetInt32(2);
        Copy newCopy = new Copy(bookId, dueDate, copyId);
        allCopys.Add(newCopy);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCopys;
    }
    public string GetAuthors()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();


      SqlCommand cmd = new SqlCommand("SELECT authors.* FROM books JOIN books_authors ON (books.id = books_authors.book_id) JOIN authors ON (books_authors.author_id = authors.id) WHERE books.id = @BookId;", conn);
      SqlParameter courseIdParameter = new SqlParameter();
      courseIdParameter.ParameterName = "@BookId";
      courseIdParameter.Value = this._bookId;
      cmd.Parameters.Add(courseIdParameter);
      rdr = cmd.ExecuteReader();

      string authorsString = "";

      while(rdr.Read())
      {
        string author = rdr.GetString(0);
        authorsString += author + " ";
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return authorsString;

    }

    public void Renew()
    {
      SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("UPDATE copies SET due_date = '2016-08-15' WHERE id = @CopyId;", conn);

     SqlParameter idParameter = new SqlParameter();
     idParameter.ParameterName = "@CopyId";
     idParameter.Value = this.GetId();

     cmd.Parameters.Add(idParameter);

     cmd.ExecuteNonQuery();

     if (conn != null)
     {
       conn.Close();
     }
    }

    public string GetDueDateString()
    {
      DateTime dt =  DateTime.Now;
      if (_dueDate.HasValue) dt = _dueDate.Value;

      //dt = _dueDate;
      return dt.ToString("d");
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM copies; DELETE FROM patrons; DELETE FROM books;", conn);
      cmd.ExecuteNonQuery();
    }

  }
}
