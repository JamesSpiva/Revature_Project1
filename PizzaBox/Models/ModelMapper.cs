using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaBox.Library.DataClasses;

namespace PizzaBox.Models
{
    public class ModelMapper
    {
        public static RestaurantModel Map (Restaurant restaurant)
        {
            if (restaurant == null) return null;
            RestaurantModel r = new RestaurantModel();

            r.Id = restaurant.ID;
            r.Name = restaurant.Store.Name;
            r.Address = restaurant.Address.AsString();
            r.Open = restaurant.Hours.Open;
            r.Close = restaurant.Hours.Close;

            r.Constraints = restaurant.Constraints;

            r.CrustInventory = restaurant.Crusts.Select(x => (x.ID, x.Name, x.invAmount));
            r.ToppingInventory = restaurant.Toppings.Select(x => (x.ID, x.Name, x.invAmount));

            r.Crusts = restaurant.Crusts.Select(x => (x.ID, x.Name));
            r.Sizes = restaurant.Sizes.Select(x => (x.ID, x.Name));
            r.Presets = restaurant.Presets.Select(x => (x.ID, x.Name, x.Toppings));
            r.Toppings = restaurant.Toppings.Select(x => (x.ID, x.Name));

            return r;
        }
        public static IEnumerable<RestaurantModel> Map (IEnumerable<Restaurant> restaurants)
        {
            return restaurants.Select(x => Map(x));
        }

        public static OrderModel Map (Order order)
        {
            if (order == null) return null;
            OrderModel om = new OrderModel();

            om.ID = order.ID;
            om.User = order.User;
            om.Restaurant = order.Restaurant;
            om.Date = order.Date;
            om.Price = order.Price;
            om.PizzaCount = order.PizzaCount;

            om.Pizzas = Map(order.pizzas).ToList();

            return om;
        }
        public static IEnumerable<OrderModel> Map (IEnumerable<Order> orders)
        {
            return orders.Select(x => Map(x));
        }

        public static Order Map(OrderModel order)
        {
            if (order == null) return null;
            Order o = new Order();

            o.User = order.User;
            o.Restaurant = ((int)order.Restaurant.ID, order.Restaurant.Name, order.Restaurant.Address);
            o.Date = order.Date;
            o.Price = order.Price;
            o.PizzaCount = order.PizzaCount;

            o.pizzas = Map(order.Pizzas).ToList();

            return o;
        }

        public static PizzaModel Map (Pizza pizza)
        {
            if (pizza == null) return null;
            PizzaModel p = new PizzaModel();

            p.SizeID = pizza.Size.ID;
            p.Size = pizza.Size.Name;
            p.CrustID = pizza.Crust.ID;
            p.Crust = pizza.Crust.Name;
            p.PresetID = pizza.Preset.ID;
            p.Preset = pizza.Preset.Name;
            p.ToppingIDs = pizza.Toppings.Select(x => x.ID).ToList();
            p.Toppings = pizza.Toppings.Select(x => x.Name).ToList();
            p.Price = pizza.Price;
            p.Amount = pizza.Amount;

            return p;
        }
        public static IEnumerable<PizzaModel> Map (IEnumerable<Pizza> pizzas)
        {
            return pizzas.Select(x => Map(x));
        }

        public static Pizza Map(PizzaModel pizza)
        {
            if (pizza == null) return null;
            Pizza p = new Pizza();

            p.Size = (pizza.SizeID, pizza.Size);
            p.Crust = (pizza.CrustID, pizza.Crust);
            p.Preset = (pizza.PresetID, pizza.Preset);
            var temp = new List<(int, string)>();
            for (int i = 0; i < pizza.ToppingIDs.Count; i++)
            {
                temp.Add((pizza.ToppingIDs[i], pizza.Toppings[i]));
            }
            p.Toppings = temp;
            p.Price = pizza.Price;
            p.Amount = pizza.Amount;

            return p;
        }
        public static IEnumerable<Pizza> Map(IEnumerable<PizzaModel> pizzas)
        {
            return pizzas.Select(x => Map(x));
        }

        public static UserModel Map (User user)
        {
            if (user == null) return null;
            UserModel u = new UserModel();

            u.ID = user.ID;
            u.Username = user.Username;
            u.Password = user.Password;

            return u;
        }
        public static User Map(UserModel user)
        {
            if (user == null) return null;
            User u = new User();

            u.ID = user.ID;
            u.Username = user.Username;
            u.Password = user.Password;

            return u;
        }
    }
}
