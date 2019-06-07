using System;
using System.Collections.Generic;

namespace PizzaBox.Context.DataModel
{
    public partial class PizzaTopping1
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int ToppingId { get; set; }

        public virtual Pizza Pizza { get; set; }
        public virtual PizzaTopping Topping { get; set; }
    }
}
