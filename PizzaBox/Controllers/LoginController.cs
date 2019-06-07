using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Context.Repositories;
using PizzaBox.Library.DataClasses;
using PizzaBox.Library.Repositories;
using PizzaBox.Models;

namespace PizzaBox.Controllers
{
    public class LoginController : Controller
    {
        public static UserModel user;

        public static IUserRepository Repository { get; set; }

        public LoginController(IUserRepository repository)
        {
            Repository = repository;
        }

        public IActionResult Login()
        {
            if (user != null) return RedirectToAction("ViewUserOrders", "Order");
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult Logout()
        {
            user = null;
            OrderController.order = null;
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("Username,Password")]UserModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                User u = Repository.Read(user.Username);

                if (u == null) return View();
                if (u.Password == user.Password)
                {
                    LoginController.user = ModelMapper.Map(u);
                    return RedirectToAction("ViewUserOrders", "Order");
                }
                else return View();
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signup([Bind("Username,Password")]UserModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                User u = Repository.Read(user.Username);

                if (u == null)
                {
                    Repository.Create(ModelMapper.Map(user));
                    LoginController.user = ModelMapper.Map(Repository.Read(user.Username));
                    return RedirectToAction("ViewUserOrders", "Order");
                }
                else return View();
            }
            catch
            {
                return View();
            }
        }

    }
}