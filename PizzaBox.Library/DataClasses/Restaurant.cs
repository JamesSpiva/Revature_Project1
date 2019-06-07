using System;
using System.Collections.Generic;
using System.Text;
using PizzaBox.Library.Repositories;

namespace PizzaBox.Library.DataClasses
{
    public class Restaurant
    {
        public int ID { get; set; }
        public (int ID, string Name) Store { get; set; }
        public (int? MaxPrice, int? MaxPizza, int? MaxToppings, int MinHours) Constraints { get; set; }
        public (int Open, int Close) Hours { get; set; }
        public Address Address { get; set; }

        public IList<(int ID, string Name)> Sizes { get; set; }
        public IList<(int ID, string Name, int invAmount)> Crusts { get; set; }
        public IList<(int ID, string Name, IList<(int ID, string Name)> Toppings)> Presets { get; set; }
        public IList<(int ID, string Name, int invAmount)> Toppings { get; set; }
    }
}
