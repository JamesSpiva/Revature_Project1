using System;
using System.Collections.Generic;
using System.Text;
using PizzaBox.Library.DataClasses;

namespace PizzaBox.Library.Repositories
{
    public interface IOrderRepository
    {
        string Create(Order order, Restaurant restaurant);
        Order Read(int id);
        IEnumerable<Order> ReadByUser(int userID);
        IEnumerable<Order> ReadByUser(string username);
        IEnumerable<Order> ReadByRestaurant(int restaurantID);
        //void Update();
        //void Delete();
    }
}
