using System;
using System.Collections.Generic;
using System.Text;
using PizzaBox.Context.DataModel;

namespace PizzaBox.Context
{
    class Database
    {
        private static PizzaDBContext dBContext;
        private Database () { }
        public static PizzaDBContext GetContext ()
        {
            if (dBContext == null) { dBContext = new PizzaDBContext(); }
            return dBContext;
        }
        public static void SaveContext ()
        {
            GetContext().SaveChanges();
        }
    }
}
