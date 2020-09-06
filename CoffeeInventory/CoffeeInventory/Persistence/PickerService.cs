using CoffeeInventory.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeInventory.Persistence
{
    public class PickerService
    {
        public static List<CoffeeType> GetCoffeeTypes()
        {
            var coffeeTypes = new List<CoffeeType>()
            {
                new CoffeeType() { Key = 1, Value = "Beans"},
                new CoffeeType() { Key = 2, Value = "Ground"},
                new CoffeeType() { Key = 3, Value = "Drip"},
                new CoffeeType() { Key = 4, Value = "Tablea"},
                new CoffeeType() { Key = 5, Value = "Pepper"}
            };
            return coffeeTypes;
        }
    }
}
