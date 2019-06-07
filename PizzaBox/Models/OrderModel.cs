using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PizzaBox.Models
{
    public class OrderModel
    {
        public int ID { get; set; }

        [Display(Name = "User")]
        public (int ID, string Name) User { get; set; }
        [Display(Name = "Restaurant")]
        public (int? ID, string Name, string Address) Restaurant { get; set; }
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Pizzas")]
        public IList<PizzaModel> Pizzas { get; set; }

        [Display(Name = "Price")]
        public double Price { get; set; }
        [Display(Name = "Number of Pizzas")]
        public int PizzaCount { get; set; }

        public OrderModel ()
        {
            Restaurant = (null, null, null);
            Pizzas = new List<PizzaModel>();
        }

        public void AddPizza (PizzaModel pizza)
        {
            Pizzas.Add(pizza);
            PizzaCount += pizza.Amount;
            Price += pizza.Price * pizza.Amount;
        }
        public void RemovePizza(int index)
        {
            PizzaModel pizza = Pizzas[index];
            Pizzas.RemoveAt(index);
            PizzaCount -= pizza.Amount;
            Price -= pizza.Price * pizza.Amount;
        }
    }
}
