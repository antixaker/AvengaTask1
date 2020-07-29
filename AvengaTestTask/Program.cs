using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AvengaTestTask.Models;
using Newtonsoft.Json;

namespace AvengaTestTask
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var pizzas = GetData();

            var mostPopularPizzas = GetPopularPizzas(pizzas);

            PrintPizzaInfo(mostPopularPizzas);

            Console.ReadKey();
        }

        private static IList<Pizza> GetData()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var jsonResource = assembly.GetManifestResourceNames()?[0];

            using (Stream stream = assembly.GetManifestResourceStream(jsonResource))
            using (StreamReader reader = new StreamReader(stream))
            using (JsonReader jsonReader = new JsonTextReader(reader))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<IList<Pizza>>(jsonReader);
            }
        }

        private static Dictionary<Pizza, int> GetPopularPizzas(IList<Pizza> pizzas)
        {
            var pizzaConfigs = new Dictionary<Pizza, int>();

            foreach (var pizza in pizzas)
            {
                var exists = pizzaConfigs.Keys.Where(up => up.Equals(pizza))?.FirstOrDefault();
                if (exists != null)
                {
                    pizzaConfigs[exists] += 1;
                }
                else
                {
                    pizzaConfigs.Add(pizza, 1);
                }
            }

            var sortedDictionary = (from entry in pizzaConfigs orderby entry.Value descending select entry)
                .Take(20)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
            return sortedDictionary;
        }

        private static void PrintPizzaInfo(Dictionary<Pizza, int> mostPopularPizzas)
        {
            var i = 1;
            foreach (var pizzaConfig in mostPopularPizzas)
            {

                Console.WriteLine($"- Pizza configuration {i}, was ordered {pizzaConfig.Value} times -");
                Console.WriteLine("Toppings:");
                foreach (var topping in pizzaConfig.Key.Toppings)
                {
                    Console.WriteLine($"{topping}");
                }
                Console.WriteLine("\n");
                i++;
            }
        }
    }
}
