using CoffeeInventory.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeInventory.ViewModels
{
    public interface ICoffeeStore
    {
        Task<IEnumerable<Coffee>> GetCoffeeAsync();
        Task<Coffee> GetCoffee(int id);
        Task AddCoffee(Coffee coffee);
        Task UpdateCoffee(Coffee coffee);
        Task DeleteCoffee(Coffee coffee);

        Task<IEnumerable<Coffee>> GetCoffeeBeansAsync();
        Task<IEnumerable<Coffee>> GetCoffeeGroundsAsync();
        Task<IEnumerable<Coffee>> GetCoffeeDripsAsync();
        Task<IEnumerable<Coffee>> GetTableasAsync();
        Task<IEnumerable<Coffee>> GetPeppersAsync();

        Task<IEnumerable<Coffee>> GetStockBeansAsync();
        Task<IEnumerable<Coffee>> GetSaleBeansAsync();

        Task<IEnumerable<Coffee>> GetStockGroundsAsync();
        Task<IEnumerable<Coffee>> GetSaleGroundsAsync();

        Task<IEnumerable<Coffee>> GetStockDripsAsync();
        Task<IEnumerable<Coffee>> GetSaleDripsAsync();

        Task<IEnumerable<Coffee>> GetStockTableasAsync();
        Task<IEnumerable<Coffee>> GetSaleTableasAsync();

        Task<IEnumerable<Coffee>> GetStockPeppersAsync();
        Task<IEnumerable<Coffee>> GetSalePeppersAsync();
    }
}
