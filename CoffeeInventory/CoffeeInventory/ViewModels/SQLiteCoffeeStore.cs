using CoffeeInventory.Models;
using CoffeeInventory.Persistence;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeInventory.ViewModels
{
    public class SQLiteCoffeeStore : ICoffeeStore
    {
        private SQLiteAsyncConnection _connection;

        public SQLiteCoffeeStore(ISQLiteDb db)
        {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<Coffee>();
        }

        public async Task<IEnumerable<Coffee>> GetCoffeeAsync()
        {
            return await _connection.Table<Coffee>().ToListAsync();
        }

        public async Task DeleteCoffee(Coffee coffee)
        {
            await _connection.DeleteAsync(coffee);
        }

        public async Task AddCoffee(Coffee coffee)
        {
            await _connection.InsertAsync(coffee);
        }

        public async Task UpdateCoffee(Coffee coffee)
        {
            await _connection.UpdateAsync(coffee);
        }

        public async Task<Coffee> GetCoffee(int id)
        {
            return await _connection.FindAsync<Coffee>(id);
        }

        public async Task<IEnumerable<Coffee>> GetCoffeeBeansAsync()
        {
            return await _connection.QueryAsync<Coffee>("SELECT * FROM [Coffee] WHERE [Type] = 'Beans'");
        }
        public async Task<IEnumerable<Coffee>> GetCoffeeGroundsAsync()
        {
            return await _connection.QueryAsync<Coffee>("SELECT * FROM [Coffee] WHERE [Type] = 'Grounds'");
        }
        public async Task<IEnumerable<Coffee>> GetCoffeeDripsAsync()
        {
            return await _connection.QueryAsync<Coffee>("SELECT * FROM [Coffee] WHERE [Type] = 'Drip'");
        }
        public async Task<IEnumerable<Coffee>> GetTableasAsync()
        {
            return await _connection.QueryAsync<Coffee>("SELECT * FROM [Coffee] WHERE [Type] = 'Tablea'");
        }
        public async Task<IEnumerable<Coffee>> GetPeppersAsync()
        {
            return await _connection.QueryAsync<Coffee>("SELECT * FROM [Coffee] WHERE [Type] = 'Pepper'");
        }

        public async Task<IEnumerable<Coffee>> GetStockBeansAsync()
        {
            return await _connection.QueryAsync<Coffee>("SELECT * FROM [Coffee] WHERE [Type] = 'Beans' AND [IsStock] = true");
        }
        public async Task<IEnumerable<Coffee>> GetSaleBeansAsync()
        {
            return await _connection.QueryAsync<Coffee>("SELECT * FROM [Coffee] WHERE [Type] = 'Beans' AND [IsStock] = false");
        }

        public async Task<IEnumerable<Coffee>> GetStockGroundsAsync()
        {
            return await _connection.QueryAsync<Coffee>("SELECT * FROM [Coffee] WHERE [Type] = 'Grounds' AND [IsStock] = true");
        }
        public async Task<IEnumerable<Coffee>> GetSaleGroundsAsync()
        {
            return await _connection.QueryAsync<Coffee>("SELECT * FROM [Coffee] WHERE [Type] = 'Grounds' AND [IsStock] = false");
        }

        public async Task<IEnumerable<Coffee>> GetStockDripsAsync()
        {
            return await _connection.QueryAsync<Coffee>("SELECT * FROM [Coffee] WHERE [Type] = 'Drip' AND [IsStock] = true");
        }
        public async Task<IEnumerable<Coffee>> GetSaleDripsAsync()
        {
            return await _connection.QueryAsync<Coffee>("SELECT * FROM [Coffee] WHERE [Type] = 'Drip' AND [IsStock] = false");
        }

        public async Task<IEnumerable<Coffee>> GetStockTableasAsync()
        {
            return await _connection.QueryAsync<Coffee>("SELECT * FROM [Coffee] WHERE [Type] = 'Tablea' AND [IsStock] = true");
        }
        public async Task<IEnumerable<Coffee>> GetSaleTableasAsync()
        {
            return await _connection.QueryAsync<Coffee>("SELECT * FROM [Coffee] WHERE [Type] = 'Tablea' AND [IsStock] = false");
        }

        public async Task<IEnumerable<Coffee>> GetStockPeppersAsync()
        {
            return await _connection.QueryAsync<Coffee>("SELECT * FROM [Coffee] WHERE [Type] = 'Pepper' AND [IsStock] = true");
        }
        public async Task<IEnumerable<Coffee>> GetSalePeppersAsync()
        {
            return await _connection.QueryAsync<Coffee>("SELECT * FROM [Coffee] WHERE [Type] = 'Pepper' AND [IsStock] = false");
        }
    }
}
