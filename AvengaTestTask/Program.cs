using System;
using System.Collections.Generic;
using System.IO;
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

            Console.WriteLine("Hello World!");
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
    }
}
