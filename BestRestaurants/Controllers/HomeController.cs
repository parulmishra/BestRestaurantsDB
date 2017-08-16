using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BestRestaurants.Models;

namespace BestRestaurants.Controllers
{
	public class HomeController : Controller
	{
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
    [HttpGet("/cuisines")]
    public ActionResult Cuisines()
    {
      List<Cuisine> allCuisines = Cuisine.GetAll();
			return View(allCuisines);
    }
		[HttpGet("/restaurants")]
    public ActionResult Restaurants()
    {
      Dictionary<string,object> model = new Dictionary<string,object>();
			model.Add("cuisines",Cuisine.GetAll());
			model.Add("restaurants",Restaurant.GetAll());
			return View(model);
    }
		[HttpGet("/cuisines/add")]
		public ActionResult CuisineForm()
		{
			return View();
		}
		[HttpPost("/cuisines/add")]
		public ActionResult CuisineFormInput()
		{
			string name = Request.Form["cuisine_name"];
			string region = Request.Form["cuisine_region"];
			Cuisine newCuisine = new Cuisine(name,region);
			newCuisine.Save();
			return View("Cuisines", Cuisine.GetAll());
		}
		[HttpGet("/restaurants/cuisines/{id}")]
		public ActionResult RestaurantByCuisine(int id)
		{
			Dictionary<string,object> model = new Dictionary<string,object>();
			Cuisine myCuisine = Cuisine.Find(id);
			//model["cuisines"] = mycuisine;
			model.Add("cuisines",myCuisine);
			model.Add("restaurants",Restaurant.GetAll());
			return View(model);
		}
		[HttpGet("/restaurantform/add/{id}")]
		public ActionResult RestaurantForm(int id)
		{
			return View(Cuisine.Find(id));
		}
		[HttpPost("/restaurants/cuisines/{id}")]
		public ActionResult RestaurantFormInput(int id)
		{
			string name = Request.Form["restaurant_name"];
			string address = Request.Form["restaurant_address"];
			string pricey = Request.Form["restaurant_pricey"];
			Restaurant newRestaurant = new Restaurant(name,address,pricey,id);
			newRestaurant.Save();
			Dictionary<string,object> model = new Dictionary<string,object>();
			model.Add("restaurants",Restaurant.GetAll());
			model.Add("cuisines",Cuisine.Find(id));

			return View("RestaurantByCuisine",model);
		}
		[HttpGet("/restaurants/{id}/cuisines/{id2}/")]
		public ActionResult RestaurantDetails(int id, int id2)
		{
			Dictionary<string,object> details = new Dictionary<string,object>();
			Restaurant newRestaurant = Restaurant.Find(id);
			Cuisine newCuisine = Cuisine.Find(id2);
			details.Add("restaurants",newRestaurant);
			details.Add("cuisines",newCuisine);
			return View(details);
		}

	}
}
