using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace BestRestaurants.Models
{
  public class Cuisine
  {
    private int _id;
    private string _name;
    private string _region;

    public Cuisine(string name, string region, int id = 0)
    {
      _name = name;
      _region = region;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetRegion()
    {
      return _region;
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool idEquality = (this.GetId()) == newCuisine.GetId();
        bool nameEquality = (this.GetName()) == newCuisine.GetName();
        bool regionEquality = (this.GetRegion()) == newCuisine.GetRegion();

        return (idEquality && nameEquality && regionEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine>{};

      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM Cuisine ORDER BY name;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int cuisineId = rdr.GetInt32(0);
        string cuisineName = rdr.GetString(1);
        string cuisineRegion = rdr.GetString(2);
        Cuisine newCuisine = new Cuisine(cuisineName, cuisineRegion, cuisineId);
        allCuisines.Add(newCuisine);
      }
      conn.Close();
      return allCuisines;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO Cuisine (name, region) VALUES (@name, @region);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter region = new MySqlParameter();
      region.ParameterName = "@region";
      region.Value = this._region;
      cmd.Parameters.Add(region);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
    }

    public static Cuisine Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM Cuisine WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int cuisineId = 0;
      string name = "";
      string region = "";

      while (rdr.Read())
      {
        cuisineId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        region = rdr.GetString(2);
      }
      Cuisine newCuisine = new Cuisine(name, region, cuisineId);
      conn.Close();
      return newCuisine;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM Cuisine;";
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    // public List<Restaurant> GetRestaurants()
    // {
    //
    // }

  }
}
