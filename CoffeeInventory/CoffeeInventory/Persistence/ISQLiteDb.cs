using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeInventory.Persistence
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
