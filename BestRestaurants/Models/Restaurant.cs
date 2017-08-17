using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace BestRestaurants.Models
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private string _address;
    private string _pricey;
    private int _cuisineId;

    public Restaurant(string name, string address, string pricey, int cuisineId, int id = 0)
    {
      _name = name;
      _address = address;
      _cuisineId = cuisineId;
      _pricey = pricey;
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
    public string GetAddress()
    {
      return _address;
    }
    public string GetPricey()
    {
      return _pricey;
    }
    public int GetCuisineId()
    {
      return _cuisineId;
    }
    public override bool Equals(System.Object otherRestaurant)
    {
      if(!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = this.GetId() == newRestaurant.GetId();
        bool nameEquality = (this.GetName() == newRestaurant.GetName());
        bool addressEquality = (this.GetAddress() == newRestaurant.GetAddress());
        bool priceyEquality = (this.GetPricey() == newRestaurant.GetPricey());
        bool cuisineIdEquality = (this.GetCuisineId() == newRestaurant.GetCuisineId());
        return (idEquality && nameEquality && addressEquality && priceyEquality && cuisineIdEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> restaurantList = new List<Restaurant>();
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM Restaurant ORDER BY name;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string address = rdr.GetString(2);
        string pricey = rdr.GetString(3);
        int cuisineId = rdr.GetInt32(4);
        Restaurant newRestaurant = new Restaurant(name,address,pricey,cuisineId,id);
        restaurantList.Add(newRestaurant);
      }
      conn.Close();
      return restaurantList;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO Restaurant(name, address,pricey,cuisineId) VALUES(@name, @address, @pricey,@cuisineId);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter address = new MySqlParameter();
      address.ParameterName = "@address";
      address.Value = this._address;
      cmd.Parameters.Add(address);

      MySqlParameter pricey = new MySqlParameter();
      pricey.ParameterName = "@pricey";
      pricey.Value = this._pricey;
      cmd.Parameters.Add(pricey);

      MySqlParameter cuisineId = new MySqlParameter();
      cuisineId.ParameterName = "@cuisineId";
      cuisineId.Value = this._cuisineId;
      cmd.Parameters.Add(cuisineId);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
    }

    public static Restaurant Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM Restaurant WHERE id = @thisId ORDER BY name ASC;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int restaurantId = 0;
      string name = "";
      string address = "";
      string pricey = "";
      int cuisineId = 0;

      while (rdr.Read())
      {
        restaurantId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        address = rdr.GetString(2);
        pricey = rdr.GetString(3);
        cuisineId = rdr.GetInt32(4);
      }
      Restaurant newRestaurant = new Restaurant(name,address,pricey,cuisineId,restaurantId);
      conn.Close();
      return newRestaurant;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM Restaurant;";
      cmd.ExecuteNonQuery();
      conn.Close();
    }
    public void UpdateAddress(string newAddress)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE Restaurant SET address = @newAddress WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter address = new MySqlParameter();
      address.ParameterName = "@newAddress";
      address.Value = newAddress;
      cmd.Parameters.Add(address);

      cmd.ExecuteNonQuery();
      _address = newAddress;
      conn.Close();
    }
    public List<Review> GetReviewSpecificToRestaurant()
    {
      List<Review> reviewList = new List<Review>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM Review WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = _id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int restaurantId = 0;
      string username = "";
      string description = "";
      string rating = "";
      int id=0;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        username = rdr.GetString(1);
        description = rdr.GetInt32(2);
        rating = rdr.GetString(3);
        restaurantId = rdr.GetInt32(4);
        Review newReview = new Review(username, description, rating, restaurantId,id);
        reviewList.Add(newReview);
      }
      conn.Close();
      return reviewList;
    }
    public void DeleteThis()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM Restaurant WHERE id = @thisId;";

      MySqlParameter restaurant = new MySqlParameter();
      restaurant.ParameterName = "@thisId";
      restaurant.Value = this._id;
      cmd.Parameters.Add(restaurant);

      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
