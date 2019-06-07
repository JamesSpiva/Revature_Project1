using System;
using System.Collections.Generic;
using System.Text;
using PizzaBox.Library.DataClasses;

namespace PizzaBox.Library.Repositories
{
    public interface IRestaurantRepository
    {
        //void Create();
        IList<Restaurant> Read();
        Restaurant Read(int id);
        //void Update();
        //void Delete();
    }
}
