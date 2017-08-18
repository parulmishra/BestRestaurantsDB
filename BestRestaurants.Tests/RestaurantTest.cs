using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BestRestaurants.Models;
using System;

namespace BestRestaurants.Tests
{
  [TestClass]
  public class RestaurantTest : IDisposable
  {
    public void Dispose()
    {
      Restaurant.DeleteAll();
    }

    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=best_restaurant_test;";
    }
    [TestMethod]
    public void GetAll_DatabaseCountsWhenTheDBIsEmpty_0()
    {
      //Arrange
      int expected = 0;
      //Act
      int actual = Restaurant.GetAll().Count;
      //Assert
      Assert.AreEqual(expected,actual);
    }
    [TestMethod]
    public void GetAll_ReturnsDatabaseCounts_2()
    {
      //Arrange
      Restaurant newRestaurant1 = new Restaurant("Chat House ","Bellevue","$$",1);
      newRestaurant1.Save();
      Restaurant newRestaurant2 = new Restaurant("Mayuri Fast Foods","Bellevue","$",2);
      newRestaurant2.Save();
      int expected = 2;
      //Act
      int actual = Restaurant.GetAll().Count;
      //Assert
      Assert.AreEqual(expected,actual);
    }
    [TestMethod]
    public void Equals_ReturnsTrueForSameObjects_True()
    {
      //Arrange
      Restaurant newRestaurant1 = new Restaurant("Chat House" , "Bellevue","$$",1);
      Restaurant newRestaurant2 = new Restaurant("Chat House" , "Bellevue","$$",1);
      Assert.AreEqual(true, Restaurant.Equals(newRestaurant1,newRestaurant2));
    }
      [TestMethod]
      public void Save_SavesToDatabase_allRestaurants()
      {
        Restaurant newRestaurant = new Restaurant("Chat House", "Bellevue","$$",1);
        newRestaurant.Save();
        List<Restaurant> actual = Restaurant.GetAll();
        List<Restaurant> expected = new List<Restaurant>();
        expected.Add(newRestaurant);
        CollectionAssert.AreEqual(expected,actual);
      }
      [TestMethod]
      public void Find_FindsRestaurantInDatabase_Restaurant()
      {
        Restaurant newRestaurant = new Restaurant("Chat House", "Bellevue","$$",1);
        newRestaurant.Save();
        Restaurant result = Restaurant.Find(newRestaurant.GetId());

        Assert.AreEqual(newRestaurant, result);
      }
      [TestMethod]
      public void Update_UpdatesTaskInDatabase_String()
      {
      //Arrange
       Restaurant testRestaurant = new Restaurant("Chat House", "Bellevue","$$",1);
       testRestaurant.Save();
       string newAddress = "Redmond";

    //Act
      testRestaurant.UpdateAddress(newAddress);

      string result = testRestaurant.GetAddress();

    //Assert
      Assert.AreEqual(newAddress, result);
    }
  }
}
