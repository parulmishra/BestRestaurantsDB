using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BestRestaurants.Models
{
  public class Review
  {
    private int _id;
    private string _userName;
    private string _description;
    private int _rating;
    private int _restaurantId;
    public Review(string name, string description, int rating, int restaurantId, int id = 0)
    {
      _userName = name;
      _description = description;
      _rating = rating;
      _restaurantId = restaurantId;
      _id = id;
    }
    public int GetId()
    {
      return _id;
    }

    public string GetUserName()
    {
      return _userName;
    }
    public string GetDescription()
    {
      return _description;
    }
    public int GetRating()
    {
      return _rating;
    }
    public int GetReastaurantId()
    {
      return _restaurantId;
    }
    public override bool Equals(System.Object otherReview)
    {
      if (!(otherReview is Review))
      {
        return false;
      }
      else
      {
        Review newReview = (Review) otherReview;
        bool idEquality = (this.GetId()) == newReview.GetId();
        bool userNameEquality = (this.GetUserName()) == newReview.GetUserName();
        bool descriptionEquality = (this.GetDescription()) == newReview.GetDescription();
        bool ratingEquality = (this.GetRating()) == newReview.GetRating();
        bool restaurantIdEquality = (this.GetReastaurantId()) == newReview.GetReastaurantId();
        return (idEquality && userNameEquality && descriptionEquality && ratingEquality && restaurantIdEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetUserName().GetHashCode();
    }
    public static List<Review> GetAll()
    {
      List<Review> allReviews = new List<Review>();
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd= conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM Review;";
      var rdr =  cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int reviewId = rdr.GetInt32(0);
        string userName = rdr.GetString(1);
        string description = rdr.GetString(2);
        int rating = rdr.GetInt32(3);
        int cuisineId = rdr.GetInt32(4);
        Review newReview = new Review(userName,description,rating, cuisineId, reviewId);
        allReviews.Add(newReview);
      }
      conn.Close();
      return allReviews;
    }
    public void Save()
    {
      MySqlConnection conn =DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText =@"INSERT INTO Review(username,description,rating,restaurantId) VALUES(@username, @description, @rating, @restaurantId);";

      MySqlParameter username = new MySqlParameter();
      username.ParameterName = "@username";
      username.Value = this._userName;
      cmd.Parameters.Add(username);

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@description";
      description.Value = this._description;
      cmd.Parameters.Add(description);

      MySqlParameter rating = new MySqlParameter();
      rating.ParameterName = "@rating";
      rating.Value = this._rating;
      cmd.Parameters.Add(rating);

      MySqlParameter restaurantId = new MySqlParameter();
      restaurantId.ParameterName = "@restaurantId";
      restaurantId.Value = this._restaurantId;
      cmd.Parameters.Add(restaurantId);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
    }

    public static Review Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM Review WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int reviewid =0;
      int restaurantId = 0;
      string username = "";
      string description = "";
      int rating = 0;
      while(rdr.Read())
      {
        reviewid = rdr.GetInt32(0);
        username = rdr.GetString(1);
        description = rdr.GetString(2);
        rating  = rdr.GetInt32(3);
        restaurantId = rdr.GetInt32(4);
      }
      Review newReview = new Review(username, description, rating, restaurantId, reviewid);
      conn.Close();
      return newReview;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM Review;";
      cmd.ExecuteNonQuery();
      conn.Close();
    }
    public void DeleteThis()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM Review WHERE id = @thisId;";

      MySqlParameter review = new MySqlParameter();
      review.ParameterName = "@thisId";
      review.Value = this._id;
      cmd.Parameters.Add(review);

      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
