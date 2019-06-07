using System;
using System.Collections.Generic;
using System.Text;
using PizzaBox.Library.Repositories;

namespace PizzaBox.Library.DataClasses
{
    public class Order
    {
        public int ID { get; set; }

        public (int ID, string Name) User { get; set; }
        public (int ID, string Name, string Address) Restaurant { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<Pizza> pizzas;

        public double Price { get; set; }
        public int PizzaCount { get; set; }
    }
}
