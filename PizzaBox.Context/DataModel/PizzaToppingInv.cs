using System;
using System.Collections.Generic;

namespace PizzaBox.Context.DataModel
{
    public partial class PizzaToppingInv
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int ToppingId { get; set; }
        public int Amount { get; set; }

        public virtual Location Location { get; set; }
        public virtual PizzaTopping Topping { get; set; }
    }
}
