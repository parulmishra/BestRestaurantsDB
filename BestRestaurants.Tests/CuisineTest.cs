using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BestRestaurants.Models;

namespace BestRestaurants.Tests
{
  [TestClass]
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=best_restaurant_test;";
    }

    [TestMethod]
    public void GetAll_CuisineDBEmptyAtFirst_0()
    {
      int result = Cuisine.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetAll_ReturnsCusinesAfterTheyHaveBeenAddedOrderedAlphabeticallyByName_CuisineList()
    {
      Cuisine newCuisine = new Cuisine("mexican", "mexico", 1);
      newCuisine.Save();
      Cuisine secondCuisine = new Cuisine("indian", "india", 2);
      secondCuisine.Save();
      List<Cuisine> allCuisines = new List<Cuisine>{secondCuisine, newCuisine};

      CollectionAssert.AreEqual(allCuisines, Cuisine.GetAll());
    }

    [TestMethod]
    public void Equals_TestSameForSameProperties_True()
    {
      Cuisine newCuisine = new Cuisine("mexican", "mexico", 1);
      Cuisine secondCuisine = new Cuisine("mexican", "mexico", 1);

      bool result = newCuisine.Equals(secondCuisine);
      Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToCuisine_Id()
    {
      Cuisine newCuisine = new Cuisine("mexican", "mexico", 1);
      newCuisine.Save();

      int result = newCuisine.GetId();
      int id = Cuisine.GetAll()[0].GetId();

      Assert.AreEqual(result, id);
    }

    [TestMethod]
    public void Find_FindsCuisineInDatabase_Cuisine()
    {
      Cuisine newCuisine = new Cuisine("mexican", "mexico", 1);
      newCuisine.Save();

      Cuisine result = Cuisine.Find(newCuisine.GetId());

      Assert.AreEqual(newCuisine, result);
    }

    public void Dispose()
    {
      //Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }
  }

}
