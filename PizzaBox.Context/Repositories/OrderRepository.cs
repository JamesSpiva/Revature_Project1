using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PizzaBox.Context.DataModel;
using PizzaBox.Library.DataClasses;
using PizzaBox.Library.Repositories;

namespace PizzaBox.Context.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private void CreateOrder(Order order)
        {
            Purchase p = Mapper.Map(order);
            Database.GetContext().Purchase.Add(p);
            foreach (var pizza in p.Pizza)
            {
                pizza.OrderId = p.Id;
                Database.GetContext().Pizza.Add(pizza);
                Database.GetContext().PizzaCrustInv.Where(x => x.LocationId == order.Restaurant.ID).First().Amount-=pizza.Amount;
                foreach (var t in pizza.PizzaTopping1)
                {
                    t.PizzaId = pizza.Id;
                    Database.GetContext().PizzaTopping1.Add(t);
                    var pt = Database.GetContext().PizzaToppingInv.Where(x => x.ToppingId == t.ToppingId && x.LocationId == order.Restaurant.ID).First();
                    pt.Amount -= pizza.Amount;
                }
            }
            Database.SaveContext();
        }
        public string Create(Order order, Restaurant restaurant)
        {
            string valid = Library.BusinessLogic.ValidateOrder(order, ReadByUser(order.User.ID), restaurant);
            if (valid != null) return valid;

            CreateOrder(order);
            return null;
        }
        
        public Order Read(int id)
        {
            return Mapper.Map(Database.GetContext().Purchase.Find(id));
        }

        public IEnumerable<Order> ReadByRestaurant(int restaurantID)
        {
            return Mapper.Map(Database.GetContext().Purchase.Where(x => x.LocationId == restaurantID));
        }

        public IEnumerable<Order> ReadByUser(int userID)
        {
            return Mapper.Map(Database.GetContext().Purchase.Where(x => x.UserId == userID));
        }
        public IEnumerable<Order> ReadByUser(string username)
        {
            Account user = Database.GetContext().Account.Where(x => x.Username == username).FirstOrDefault();
            if (user == null) return null;
            return ReadByUser(user.Id);
        }
    }
}
