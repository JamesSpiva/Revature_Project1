using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PizzaBox.Context.DataModel;
using PizzaBox.Library.DataClasses;
using PizzaBox.Library.Repositories;

namespace PizzaBox.Context.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        public IList<Restaurant> Read()
        {
            return Mapper.Map(Database.GetContext().Location);
        }

        public Restaurant Read(int id)
        {
            return Mapper.Map(Database.GetContext().Location.Find(id));
        }
    }
}
