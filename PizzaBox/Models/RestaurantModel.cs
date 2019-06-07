using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PizzaBox.Models
{
    public class RestaurantModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Hours")]
        public int Open { get; set; }
        public int Close { get; set; }

        public (int? MaxPrice, int? MaxPizza, int? MaxToppings, int MinHours) Constraints { get; set; }

        [Display(Name = "Crust Inventory")]
        public IEnumerable<(int ID, string Name, int Amount)> CrustInventory { get; set; }
        [Display(Name = "Topping Inventory")]
        public IEnumerable<(int ID, string Name, int Amount)> ToppingInventory { get; set; }

        [Display(Name = "Orders")]
        public IEnumerable<OrderModel> Orders { get; set; }

        [Display(Name = "Sizes")]
        public IEnumerable<(int ID, string Name)> Sizes { get; set; }
        [Display(Name = "Crusts")]
        public IEnumerable<(int ID, string Name)> Crusts { get; set; }
        [Display(Name = "Presets")]
        public IEnumerable<(int ID, string Name, IList<(int ID, string Name)> Toppings)> Presets { get; set; }
        [Display(Name = "Toppings")]
        public IEnumerable<(int ID, string Name)> Toppings { get; set; }
    }
}
