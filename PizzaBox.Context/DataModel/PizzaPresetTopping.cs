using System;
using System.Collections.Generic;

namespace PizzaBox.Context.DataModel
{
    public partial class PizzaPresetTopping
    {
        public int Id { get; set; }
        public int PresetId { get; set; }
        public int ToppingId { get; set; }

        public virtual PizzaPreset Preset { get; set; }
        public virtual PizzaTopping Topping { get; set; }
    }
}
