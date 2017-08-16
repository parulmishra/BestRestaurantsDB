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
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
