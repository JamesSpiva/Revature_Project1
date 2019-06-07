using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Library.DataClasses;
using PizzaBox.Library.Repositories;
using PizzaBox.Models;

namespace PizzaBox.Controllers
{
    public class OrderController : Controller
    {
        public static OrderModel order;

        public static IOrderRepository Repository { get; set; }

        public OrderController(IOrderRepository repository)
        {
            Repository = repository;
        }

        public IActionResult Index()
        {
            if (LoginController.user == null) return RedirectToAction("Login", "Login");
            else return RedirectToAction("ViewUserOrders");
        }

        public IActionResult CreateOrder()
        {
            if (LoginController.user == null) return RedirectToAction("Login", "Login");
            if (order == null) { order = new OrderModel(); }
            if (order.Restaurant.ID != null) ViewBag.Restaurant = ModelMapper.Map(RestaurantController.Repository.Read((int)order.Restaurant.ID));

            return View(order);
        }
        public IActionResult CreatePizza()
        {
            if (order == null) return RedirectToAction("CreateOrder");
            ViewBag.Restaurant = ModelMapper.Map(RestaurantController.Repository.Read((int)order.Restaurant.ID));
            return View();
        }

        public IActionResult LinkRestaurant(RestaurantModel restaurant)
        {
            if (order == null) return RedirectToAction("CreateOrder");
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("CreateOrder");
                }

                order.Restaurant = (restaurant.Id, restaurant.Name, restaurant.Address);
                return RedirectToAction("CreateOrder");
            }
            catch
            {
                return RedirectToAction("CreateOrder");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePizza([Bind("SizeID,CrustID,PresetID,ToppingIDs,MaxToppings,Amount")] PizzaModel pizza)
        {
            if (order == null) return RedirectToAction("CreateOrder");
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Restaurant = ModelMapper.Map(RestaurantController.Repository.Read((int)order.Restaurant.ID));
                    return View();
                }

                pizza.InitFromRestaurant(ModelMapper.Map(RestaurantController.Repository.Read((int)order.Restaurant.ID)));

                order.AddPizza(pizza);
                return RedirectToAction("CreateOrder");
            }
            catch (Exception e)
            {
                ViewBag.Restaurant = ModelMapper.Map(RestaurantController.Repository.Read((int)order.Restaurant.ID));
                return View();
            }
        }
        public IActionResult RemovePizza(int index)
        {
            if (order == null) return RedirectToAction("CreateOrder");
            order.RemovePizza(index);
            return RedirectToAction("CreateOrder");
        }

        public IActionResult FinishOrder()
        {
            if (order == null) return RedirectToAction("CreateOrder");
            order.User = (LoginController.user.ID, LoginController.user.Username);
            order.Date = DateTime.Now;
            string valid = Repository.Create(ModelMapper.Map(order), RestaurantController.Repository.Read((int)order.Restaurant.ID));
            TempData["ErrorMessage"] = valid;
            if (valid == null)
            {
                order = null;
                return RedirectToAction("ViewUserOrders");
            }
            else return RedirectToAction("CreateOrder");
        }
        public IActionResult CancelOrder()
        {
            order = null;
            return RedirectToAction("ViewUserOrders");
        }

        public IActionResult ViewDetails (int id)
        {
            return View(ModelMapper.Map(Repository.Read(id)));
        }
        public IActionResult ViewUserOrders()
        {
            if (LoginController.user == null) RedirectToAction("Login", "Login");
            var a = LoginController.user.ID;
            var b = Repository.ReadByUser(a);
            var c = b.OrderByDescending(x => x.Date);
            IEnumerable<OrderModel> orders = ModelMapper.Map(c);
            return View(orders);
        }
        public IActionResult ViewRestaurantOrders(int id)
        {
            IEnumerable<OrderModel> orders = ModelMapper.Map(Repository.ReadByRestaurant(id));
            return View(orders);
        }
    }
}