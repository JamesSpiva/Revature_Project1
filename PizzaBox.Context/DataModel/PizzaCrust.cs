using System;
using System.Collections.Generic;

namespace PizzaBox.Context.DataModel
{
    public partial class PizzaCrust
    {
        public PizzaCrust()
        {
            Pizza = new HashSet<Pizza>();
            PizzaCrustInv = new HashSet<PizzaCrustInv>();
        }

        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }

        public virtual Store Store { get; set; }
        public virtual ICollection<Pizza> Pizza { get; set; }
        public virtual ICollection<PizzaCrustInv> PizzaCrustInv { get; set; }
    }
}
