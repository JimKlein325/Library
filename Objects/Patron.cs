using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Library.Objects
{
  public class Patron
  {
    private int _id;
    private string _name;

    public Patron(string name, int Id = 0)
    {
      _id = Id;
      _name = name;
    }

    public override bool Equals(System.Object otherPatron)
    {
      if (!(otherPatron is Patron))
      {
        return false;
      }
      else
      {
        Patron newPatron = (Patron) otherPatron;
        bool idEquality = (this.GetId() == newPatron.GetId());
        bool nameEquality = (this.GetName() == newPatron.GetName());
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

    public static List<Patron> GetAll()
    {
      List<Patron> allPatrons = new List<Patron> {};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patrons;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        string courseName = rdr.GetString(0);
        int courseId = rdr.GetInt32(1);
        Patron newPatron = new Patron(courseName, courseId);
        allPatrons.Add(newPatron);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allPatrons;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO patrons (name) OUTPUT INSERTED.id VALUES (@PatronName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@PatronName";
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

    public static Patron Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patrons WHERE id = @PatronId;", conn);
      SqlParameter courseIdParameter = new SqlParameter();
      courseIdParameter.ParameterName = "@PatronId";
      courseIdParameter.Value = id.ToString();
      cmd.Parameters.Add(courseIdParameter);
      rdr = cmd.ExecuteReader();


      int foundPatronId = 0;
      string foundPatronName = null;

      while(rdr.Read())
      {
        foundPatronName = rdr.GetString(0);
        foundPatronId = rdr.GetInt32(1);
      }
      Patron foundPatron = new Patron(foundPatronName, foundPatronId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundPatron;
    }

    public static void DeleteAll()
     {
       SqlConnection conn = DB.Connection();
       conn.Open();
       SqlCommand cmd = new SqlCommand("DELETE FROM patrons; DELETE FROM authors; DELETE FROM books;", conn);
       cmd.ExecuteNonQuery();
     }

  }
}
