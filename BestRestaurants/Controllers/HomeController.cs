using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BestRestaurants.Models;

namespace BestRestaurants.Controllers
{
	public class HomeController : Controller
	{

		//BASIC MENU PAGES
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

		//CUISINE MANAGEMENT PAGES
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

		//RESTAURANT MANAGEMENT PAGES

		//list of rests
		[HttpGet("/restaurants/cuisines/{id}")]
		public ActionResult RestaurantByCuisine(int id)
		{
			Dictionary<string,object> model = new Dictionary<string,object>();
			Cuisine myCuisine = Cuisine.Find(id);
			//model["cuisines"] = mycuisine;
			model.Add("cuisines",myCuisine);
			model.Add("restaurants",myCuisine.GetRestaurants());
			return View(model);
		}

		//form for new rest
		[HttpGet("/restaurantform/add/{id}")]
		public ActionResult RestaurantForm(int id)
		{
			return View(Cuisine.Find(id));
		}

		//submit new rest
		[HttpPost("/restaurants/cuisines/{id}")]
		public ActionResult RestaurantFormInput(int id)
		{
			string name = Request.Form["restaurant_name"];
			string address = Request.Form["restaurant_address"];
			string pricey = Request.Form["restaurant_pricey"];
			Restaurant newRestaurant = new Restaurant(name,address,pricey,id);
			newRestaurant.Save();
			Dictionary<string,object> model = new Dictionary<string,object>();
			model.Add("restaurants",Cuisine.Find(id).GetRestaurants());
			model.Add("cuisines",Cuisine.Find(id));

			return View("RestaurantByCuisine",model);
		}

		//restaurant specific details
		[HttpGet("/restaurants/{id}/cuisines/{id2}/")]
		public ActionResult RestaurantDetails(int id, int id2)
		{
			Dictionary<string,object> details = new Dictionary<string,object>();
			Restaurant newRestaurant = Restaurant.Find(id);
			Cuisine newCuisine = Cuisine.Find(id2);
			details.Add("reviews", newRestaurant.GetReviewSpecificToRestaurant());
			details.Add("restaurants",newRestaurant);
			details.Add("cuisines",newCuisine);
			return View(details);
		}

		[HttpPost("/restaurants/{id}/cuisines/{id2}/")]
		public ActionResult RestaurantDetailsReviewInput(int id, int id2)
		{
			string name = Request.Form["review_name"];
			int rating = int.Parse(Request.Form["review_rating"]);
			string description = Request.Form["review_description"];
			Review myReview = new Review(name, description, rating, id);
			myReview.Save();
			Dictionary<string,object> details = new Dictionary<string,object>();
			Restaurant newRestaurant = Restaurant.Find(id);
			Cuisine newCuisine = Cuisine.Find(id2);
			details.Add("reviews", newRestaurant.GetReviewSpecificToRestaurant());
			details.Add("restaurants",newRestaurant);
			details.Add("cuisines",newCuisine);
			return View("RestaurantDetails",details);
		}

		//Delete a restaurant form
		[HttpGet("/cuisines/delete")]
		public ActionResult DeleteAllCuisines()
		{
			Restaurant.DeleteAll();
			Cuisine.DeleteAll();
			return View("Cuisines", Cuisine.GetAll());
		}
		[HttpGet("/restaurants/delete")]
		public ActionResult DeleteAllRestaurants()
		{
			Restaurant.DeleteAll();
			return View("Index");
		}

		//DELETE A SPECIFIC RESTAURANT
		[HttpGet("/restaurants/{id}/cuisines/{id2}/delete")]
		public ActionResult DeleteSpecificRestaurant(int id, int id2)
		{
		  (Restaurant.Find(id)).DeleteThis();
			Dictionary<string, object> model = new Dictionary<string, object>();

			model.Add("cuisines", Cuisine.Find(id2));
			model.Add("restaurants", Restaurant.GetAll());

			return View("RestaurantByCuisine", model);
		}

		[HttpGet("/cuisines/{id}/delete")]
		public ActionResult DeleteSpecificCuisine(int id)
		{
			Cuisine.Find(id).DeleteRestaurants();
			(Cuisine.Find(id)).DeleteThis();
			return View("Cuisines", Cuisine.GetAll());
		}

		[HttpGet("/cuisines/{id}/deleteAll")]
		public ActionResult DeleteSpecificCuisineRestaurants(int id)
		{
			Cuisine.Find(id).DeleteRestaurants();
			return View("Cuisines", Cuisine.GetAll());
		}
		[HttpGet("/cuisine/{id}/restaurant/{id2}/reviews/add")]
		public ActionResult ReviewForm(int id, int id2)
		{
			Dictionary<string, object> model = new Dictionary<string, object>();

			model.Add("cuisines", Cuisine.Find(id));
			model.Add("restaurants", Restaurant.Find(id2));

			return View(model);
		}
	}
}
