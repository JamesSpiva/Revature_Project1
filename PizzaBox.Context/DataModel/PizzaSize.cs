﻿using System;
using System.Collections.Generic;

namespace PizzaBox.Context.DataModel
{
    public partial class PizzaSize
    {
        public PizzaSize()
        {
            Pizza = new HashSet<Pizza>();
        }

        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }

        public virtual Store Store { get; set; }
        public virtual ICollection<Pizza> Pizza { get; set; }
    }
}
