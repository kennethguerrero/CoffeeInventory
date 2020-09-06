using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeInventory.Models
{
    public class Coffee
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Type { get; set; }

        [MaxLength(255)]
        public int Density { get; set; }

        public bool IsStock { get; set; }
    }

    public class CoffeeType
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}
