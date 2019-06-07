using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PizzaBox.Context.DataModel;
using PizzaBox.Library.DataClasses;

namespace PizzaBox.Context
{
    public class Mapper
    {
        public static Library.DataClasses.Address Map (Context.DataModel.Address address)
        {
            if (address == null) return null;
            Library.DataClasses.Address a = new Library.DataClasses.Address();

            a.ID = address.Id;
            a.Street1 = address.Street1;
            a.Street2 = address.Street2;
            a.City = address.City;
            a.State = address.State;
            a.Country = address.Country;
            a.Zipcode = address.Zipcode;

            return a;
        }

        public static Order Map (Purchase purchase)
        {
            if (purchase == null) return null;
            Order o = new Order();
            Database.GetContext().Entry(purchase).Reference("Location").Load();
            Database.GetContext().Entry(purchase.Location).Reference("Store").Load();
            Database.GetContext().Entry(purchase.Location).Reference("Address").Load();
            Database.GetContext().Entry(purchase).Reference("User").Load();
            Database.GetContext().Entry(purchase).Collection("Pizza").Load();

            o.ID = purchase.Id;
            o.User = (purchase.UserId, purchase.User.Username);
            o.Restaurant = (purchase.LocationId, purchase.Location.Store.Name, Map(purchase.Location.Address).AsString());
            o.Date = purchase.Date;
            o.pizzas = Map(purchase.Pizza);
            
            foreach (var p in o.pizzas)
            {
                o.PizzaCount += p.Amount;
                o.Price += p.Price;
            }

            return o;
        }
        public static List<Order> Map (IEnumerable<Purchase> purchases)
        {
            return purchases.Select(x => Map(x)).ToList();
        }
        public static Purchase Map(Order order) //does not link foreign keys
        {
            if (order == null) return null;
            Purchase purchase = new Purchase();

            purchase.LocationId = order.Restaurant.ID;
            purchase.UserId = order.User.ID;
            purchase.Date = order.Date;

            foreach (var p in order.pizzas)
            {
                purchase.Pizza.Add(Map(p));
            }

            return purchase;
        }

        public static Library.DataClasses.Pizza Map(Context.DataModel.Pizza pizza)
        {
            if (pizza == null) return null;
            Library.DataClasses.Pizza p = new Library.DataClasses.Pizza();
            Database.GetContext().Entry(pizza).Reference("Size").Load();
            Database.GetContext().Entry(pizza).Reference("Crust").Load();
            Database.GetContext().Entry(pizza).Reference("Preset").Load();
            Database.GetContext().Entry(pizza).Collection("PizzaTopping1").Load();
            
            p.Size = (pizza.SizeId, pizza.Size.Name);
            p.Crust = (pizza.CrustId, pizza.Crust.Name);
            if (pizza.PresetId != null) p.Preset = (pizza.PresetId, pizza.Preset.Name);
            p.Toppings = pizza.PizzaTopping1.Select(x => { Database.GetContext().Entry(x).Reference("Topping").Load(); return (x.ToppingId, x.Topping.Name); });
            p.Amount = pizza.Amount;
            p.Price = pizza.Price;

            return p;
        }
        public static IEnumerable<Library.DataClasses.Pizza> Map(IEnumerable<Context.DataModel.Pizza> pizzas)
        {
            return pizzas.Select(x => Map(x));
        }
        internal static Context.DataModel.Pizza Map(Library.DataClasses.Pizza pizza) //does not link foreign keys
        {
            if (pizza == null) return null;
            DataModel.Pizza p = new DataModel.Pizza();

            p.SizeId = pizza.Size.ID;
            p.CrustId = pizza.Crust.ID;
            if (pizza.Preset.ID != null) p.PresetId = pizza.Preset.ID;
            p.Amount = pizza.Amount;
            p.Price = pizza.Price;

            foreach (var t in pizza.Toppings)
            {
                PizzaTopping1 nt = new PizzaTopping1();
                p.PizzaTopping1.Add(nt);
                nt.ToppingId = t.ID;
            }

            return p;
        }

        public static Restaurant Map(Location location)
        {
            if (location == null) return null;
            Restaurant r = new Restaurant();
            Database.GetContext().Entry(location).Reference("Store").Load();
            Database.GetContext().Entry(location).Reference("Address").Load();
            Database.GetContext().Entry(location).Collection("PizzaCrustInv").Load();
            Database.GetContext().Entry(location).Collection("PizzaToppingInv").Load();
            Database.GetContext().Entry(location.Store).Collection("PizzaSize").Load();
            Database.GetContext().Entry(location.Store).Collection("PizzaCrust").Load();
            Database.GetContext().Entry(location.Store).Collection("PizzaPreset").Load();
            Database.GetContext().Entry(location.Store).Collection("PizzaTopping").Load();

            r.ID = location.Id;
            r.Store = (location.StoreId, location.Store.Name);
            r.Constraints = (location.Store.MaxPrice, location.Store.MaxPizza, location.Store.MaxToppings, location.Store.MinHours);
            r.Hours = (location.OpenTime, location.CloseTime);
            r.Address = Map(location.Address);
            r.Sizes = location.Store.PizzaSize.Select(x => (x.Id, x.Name)).ToList();
            r.Crusts = location.Store.PizzaCrust.Select(x => (x.Id, x.Name, x.PizzaCrustInv.Where(y => y.LocationId == location.Id).FirstOrDefault().Amount)).ToList();
            r.Presets = location.Store.PizzaPreset.Select(x => { Database.GetContext().Entry(x).Collection("PizzaPresetTopping").Load(); return (x.Id, x.Name, (IList<(int, string)>)(x.PizzaPresetTopping.Select(y => { Database.GetContext().Entry(y).Reference("Topping").Load(); return (y.ToppingId, y.Topping.Name); }).ToList())); }).ToList();
            r.Toppings = location.Store.PizzaTopping.Select(x => (x.Id, x.Name, location.PizzaToppingInv.Where(y => y.ToppingId == x.Id).FirstOrDefault().Amount)).ToList();

            return r;
        }
        public static List<Restaurant> Map (IEnumerable<Location> locations)
        {
            return locations.Select(x => Map(x)).ToList();
        }

        public static User Map (Account account)
        {
            if (account == null) return null;
            User u = new User();

            u.ID = account.Id;
            u.Username = account.Username;
            u.Password = account.Password;

            return u;
        }
        public static Account Map (User user)
        {
            if (user == null) return null;
            Account a = new Account();

            a.Id = user.ID;
            a.Username = user.Username;
            a.Password = user.Password;

            return a;
        }
    }
}
