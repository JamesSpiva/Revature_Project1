using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Library.DataClasses
{
    public class Pizza
    {
        public (int ID, string Name) Size { get; set; }
        public (int ID, string Name) Crust { get; set; }
        public (int? ID, string Name) Preset { get; set; }

        public IEnumerable<(int ID, string Name)> Toppings { get; set; }

        public int Amount { get; set; }
        public double Price { get; set; }
    }
}
