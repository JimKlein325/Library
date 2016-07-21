using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Library.Objects
{
  public class Author
  {
    private int _id;
    private string _name;

    public Author(string name, int Id = 0)
    {
      _id = Id;
      _name = name;
    }

    public override bool Equals(System.Object otherAuthor)
    {
      if (!(otherAuthor is Author))
      {
        return false;
      }
      else
      {
        Author newAuthor = (Author) otherAuthor;
        bool idEquality = (this.GetId() == newAuthor.GetId());
        bool nameEquality = (this.GetName() == newAuthor.GetName());
        return (idEquality && nameEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public static List<Author> GetAll()
    {
      List<Author> allAuthors = new List<Author> {};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM authors;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        string courseName = rdr.GetString(0);
        int courseId = rdr.GetInt32(1);
        Author newAuthor = new Author(courseName, courseId);
        allAuthors.Add(newAuthor);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allAuthors;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO authors (name) OUTPUT INSERTED.id VALUES (@AuthorName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@AuthorName";
      nameParameter.Value = this.GetName();

      cmd.Parameters.Add(nameParameter);

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

    public static Author Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM authors WHERE id = @AuthorId;", conn);
      SqlParameter courseIdParameter = new SqlParameter();
      courseIdParameter.ParameterName = "@AuthorId";
      courseIdParameter.Value = id.ToString();
      cmd.Parameters.Add(courseIdParameter);
      rdr = cmd.ExecuteReader();


      int foundAuthorId = 0;
      string foundAuthorName = null;

      while(rdr.Read())
      {
        foundAuthorName = rdr.GetString(0);
        foundAuthorId = rdr.GetInt32(1);
      }
      Author foundAuthor = new Author(foundAuthorName, foundAuthorId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundAuthor;
    }

    public static void DeleteAll()
     {
       SqlConnection conn = DB.Connection();
       conn.Open();
       SqlCommand cmd = new SqlCommand("DELETE FROM authors;", conn);
       cmd.ExecuteNonQuery();
     }

  }
}
