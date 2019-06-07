using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PizzaBox.Library.DataClasses;

namespace PizzaBox.Library
{
    public static class BusinessLogic
    {
        public static double CalcCost(Pizza pizza)
        {
            return 10;
        }

        //return null if order is valid
        public static string ValidateOrder (Order order, IEnumerable<Order> userOrders, Restaurant restaurant)
        {
            Order recentOrder = userOrders.OrderByDescending(x => x.Date).FirstOrDefault();
            DateTime recentDate = DateTime.MinValue;
            if (recentOrder != null) recentDate = recentOrder.Date;
            int minHours = restaurant.Constraints.MinHours;
            var offset = order.Date.Subtract(recentDate);

            if (recentDate.Date.Equals(order.Date.Date) && recentOrder.Restaurant.ID != order.Restaurant.ID)
            {
                return "Unable to create order. You already ordered from a different location today.";
            }
            else if (offset.Hours < minHours)
            {
                return "Unable to create order. You already ordered recently.";
            }
            else if (order.Date.Hour < restaurant.Hours.Open || order.Date.Hour >= restaurant.Hours.Close)
            {
                return "Unable to create order. You can only order while the store is open.";
            }
            else if (order.PizzaCount < 1)
            {
                return "Unable to create order. You must order at least one pizza.";
            }
            else if (restaurant.Constraints.MaxPizza != null && order.PizzaCount > restaurant.Constraints.MaxPizza)
            {
                return "Unable to create order. You cannot order that many pizzas.";
            }
            else if (restaurant.Constraints.MaxPrice != null && order.Price > restaurant.Constraints.MaxPrice)
            {
                return "Unable to create order. Order exceeds maximum allowed price.";
            }
            else if (CheckInventory(order, restaurant) != null)
            {
                return CheckInventory(order, restaurant);
            }
            else
            {
                return null;
            }
        }

        private static string CheckInventory(Order order, Restaurant restaurant)
        {
            Dictionary<int, int> crusts = new Dictionary<int, int>();
            Dictionary<int, int> toppings = new Dictionary<int, int>();

            foreach (var p in order.pizzas)
            {
                if (!crusts.Keys.Contains(p.Crust.ID)) crusts[p.Crust.ID] = 0;
                crusts[p.Crust.ID] += p.Amount;

                foreach (var t in p.Toppings)
                {
                    if (!toppings.Keys.Contains(t.ID)) toppings[t.ID] = 0;
                    toppings[t.ID] += p.Amount;
                }
            }

            foreach (var c in crusts)
            {
                var rc = restaurant.Crusts.Where(x => x.ID == c.Key).FirstOrDefault();
                if (c.Value > rc.invAmount)
                {
                    return $"Unable to create order. Location does not have enough {rc.Name} crust";
                }
            }
            foreach (var t in toppings)
            {
                var rt = restaurant.Toppings.Where(x => x.ID == t.Key).FirstOrDefault();
                if (t.Value > rt.invAmount)
                {
                    return $"Unable to create order. Location does not have enough {rt.Name}";
                }
            }

            return null;
        }

    }
}
