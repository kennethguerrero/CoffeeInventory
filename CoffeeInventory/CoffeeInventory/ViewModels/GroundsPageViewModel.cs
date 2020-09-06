using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoffeeInventory.ViewModels
{
    public class GroundsPageViewModel : BaseViewModel
    {
        private ICoffeeStore _coffeeStore;
        private IPageService _pageService;

        private bool _isDataLoaded;
        private int TotalStockGrounds;
        private int TotalSaleGrounds;
        private int RemainingStock;

        public ObservableCollection<CoffeeViewModel> CoffeeGrounds { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();

        public ObservableCollection<CoffeeViewModel> StockGrounds { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();

        public ObservableCollection<CoffeeViewModel> SaleGrounds { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();

        public ICommand LoadDataCommand { get; private set; }
        public ICommand LoadStockGroundsCommand { get; private set; }
        public ICommand LoadSaleGroundsCommand { get; private set; }
        public ICommand CheckRemainingStockCommand { get; private set; }


        public GroundsPageViewModel(ICoffeeStore coffeeStore, IPageService pageService)
        {
            _coffeeStore = coffeeStore;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            LoadStockGroundsCommand = new Command(async () => await LoadStockGrounds());
            LoadSaleGroundsCommand = new Command(async () => await LoadSaleGrounds());
            CheckRemainingStockCommand = new Command(async () => await CheckRemainingStock());
        }

        private async Task LoadSaleGrounds()
        {
            _isDataLoaded = true;

            var coffees = await _coffeeStore.GetSaleGroundsAsync();
            foreach (var coffee in coffees)
            {
                SaleGrounds.Add(new CoffeeViewModel(coffee));
            }
        }

        private async Task LoadStockGrounds()
        {
            _isDataLoaded = true;

            var coffees = await _coffeeStore.GetStockGroundsAsync();
            foreach (var coffee in coffees)
            {
                StockGrounds.Add(new CoffeeViewModel(coffee));
            }
        }

        private async Task CheckRemainingStock()
        {
            TotalStockGrounds = StockGrounds.Sum(g => g.Density);
            TotalSaleGrounds = SaleGrounds.Sum(g => g.Density);

            RemainingStock = TotalStockGrounds - TotalSaleGrounds;

            await _pageService.DisplayAlert("Stock Grounds", "Remaining: " + RemainingStock.ToString() + "g", "OK");
        }

        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;

            var coffees = await _coffeeStore.GetCoffeeGroundsAsync();
            foreach (var coffee in coffees)
            {
                CoffeeGrounds.Add(new CoffeeViewModel(coffee));
            }
        }
    }
}