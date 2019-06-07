using System;
using System.Collections.Generic;

namespace PizzaBox.Context.DataModel
{
    public partial class PizzaCrustInv
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int CrustId { get; set; }
        public int Amount { get; set; }

        public virtual PizzaCrust Crust { get; set; }
        public virtual Location Location { get; set; }
    }
}
