using System;
using System.Collections.Generic;
using System.Linq;

namespace AvengaTestTask.Models
{
    public class Pizza
    {
        public IList<string> Toppings { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Pizza pizza)
            {
                var equals = (Toppings.Count == pizza.Toppings.Count) && !Toppings.Except(pizza.Toppings).Any();
                return equals;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return 1910748205 + EqualityComparer<IList<string>>.Default.GetHashCode(Toppings);
        }
    }
}
