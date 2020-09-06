using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CoffeeInventory.iOS.Persistence;
using CoffeeInventory.Persistence;
using Foundation;
using SQLite;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteDb))]

namespace CoffeeInventory.iOS.Persistence
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLiteDb.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}