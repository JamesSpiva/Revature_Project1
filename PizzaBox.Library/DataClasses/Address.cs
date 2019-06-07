using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Library.DataClasses
{
    public class Address
    {
        public int? ID { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }

        public string AsString ()
        {
            string s = $"{Street1} ";
            if (Street2 != null) s += $"{Street2} ";
            s += $"{City}, {State} {Country} {Zipcode}";

            return s;
        }
    }
}
