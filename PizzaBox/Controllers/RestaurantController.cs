using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Library.Repositories;
using PizzaBox.Models;

namespace PizzaBox.Controllers
{
    public class RestaurantController : Controller
    {
        public static IRestaurantRepository Repository { get; set; }

        public RestaurantController (IRestaurantRepository repository)
        {
            Repository = repository;
        }

        public IActionResult Index()
        {
            IEnumerable<RestaurantModel> restaurants = ModelMapper.Map(Repository.Read());
            return View(restaurants);
        }

        public IActionResult Details(int id)
        {
            RestaurantModel restaurant = ModelMapper.Map(Repository.Read(id));
            return View(restaurant);
        }

        public IActionResult SelectRestaurant()
        {
            IEnumerable<RestaurantModel> restaurants = ModelMapper.Map(Repository.Read());
            return View(restaurants);
        }
    }
}