using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PizzaBox.Models
{
    public class PizzaModel
    {
        [Required]
        [Display(Name = "Size")]
        public int SizeID { get; set; }
        public string Size { get; set; }
        [Required]
        [Display(Name = "Crust")]
        public int CrustID { get; set; }
        public string Crust { get; set; }
        [Display(Name = "Preset Toppings")]
        [PresetValidation]
        public int? PresetID { get; set; }
        public string Preset { get; set; }

        [Display(Name = "Toppings")]
        [ToppingCountValidation]
        public IList<int> ToppingIDs { get; set; }
        public IList<string> Toppings { get; set; }

        [Required(ErrorMessage = "Must be an integer")]
        [Display(Name = "Count")]
        [Range(1, int.MaxValue, ErrorMessage = "Must be at least 1")]
        public int Amount { get; set; }
        [Display(Name = "Price (each)")]
        public double Price { get; set; }

        public int? MaxToppings { get; set; }

        public void InitFromRestaurant (RestaurantModel restaurant)
        {
            Size = restaurant.Sizes.Where(x => x.ID == SizeID).Select(y => y.Name).First();
            Crust = restaurant.Crusts.Where(x => x.ID == CrustID).Select(y => y.Name).First();
            if (PresetID != null)
            {
                Preset = restaurant.Presets.Where(x => x.ID == PresetID).Select(y => y.Name).First();
                ToppingIDs = restaurant.Presets.Where(x => x.ID == PresetID).First().Toppings.Select(y => y.ID).ToList();
            }
            if (ToppingIDs == null) ToppingIDs = new List<int>();
            Toppings = restaurant.Toppings.Join(ToppingIDs, x => x.ID, y => y, (x, y) => new { X = x, Y = y }).Select(z => z.X.Name).ToList();

            Price = Library.BusinessLogic.CalcCost(ModelMapper.Map(this));
        }

        public class PresetValidation : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext context)
            {
                PizzaModel pizza = (PizzaModel)context.ObjectInstance;
                if (pizza.PresetID == null || pizza.ToppingIDs == null)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Cannot have both a Preset and a list of Toppings");
                }
            }
        }
        public class ToppingCountValidation : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext context)
            {
                PizzaModel pizza = (PizzaModel)context.ObjectInstance;
                if (pizza.ToppingIDs == null || pizza.MaxToppings == null || pizza.ToppingIDs.Count() <= pizza.MaxToppings)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"{pizza.MaxToppings} Toppings max");
                }
            }
        }
    }
}
