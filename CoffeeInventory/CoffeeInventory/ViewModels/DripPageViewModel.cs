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
    public class DripPageViewModel : BaseViewModel
    {
        private ICoffeeStore _coffeeStore;
        private IPageService _pageService;

        private bool _isDataLoaded;
        private int TotalStockDrips;
        private int TotalSaleDrips;
        private int RemainingStock;

        public ObservableCollection<CoffeeViewModel> CoffeeDrips { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();
        public ObservableCollection<CoffeeViewModel> StockDrips { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();
        public ObservableCollection<CoffeeViewModel> SaleDrips { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();

        public ICommand LoadDataCommand { get; private set; }
        public ICommand LoadStockDripsCommand { get; private set; }
        public ICommand LoadSaleDripsCommand { get; private set; }
        public ICommand CheckRemainingStockCommand { get; private set; }

        public DripPageViewModel(ICoffeeStore coffeeStore, IPageService pageService)
        {
            _coffeeStore = coffeeStore;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            LoadStockDripsCommand = new Command(async () => await LoadStockDrips());
            LoadSaleDripsCommand = new Command(async () => await LoadSaleDrips());
            CheckRemainingStockCommand = new Command(async () => await CheckRemainingStock());
        }

        private async Task LoadSaleDrips()
        {
            _isDataLoaded = true;

            var coffees = await _coffeeStore.GetSaleDripsAsync();
            foreach (var coffee in coffees)
            {
                SaleDrips.Add(new CoffeeViewModel(coffee));
            }
        }

        private async Task LoadStockDrips()
        {
            _isDataLoaded = true;

            var coffees = await _coffeeStore.GetStockDripsAsync();
            foreach (var coffee in coffees)
            {
                StockDrips.Add(new CoffeeViewModel(coffee));
            }
        }

        private async Task CheckRemainingStock()
        {
            TotalStockDrips = StockDrips.Sum(d => d.Density);
            TotalSaleDrips = SaleDrips.Sum(d => d.Density);
            RemainingStock = TotalStockDrips - TotalSaleDrips;

            await _pageService.DisplayAlert("Stock Drips", "Remaining: " + RemainingStock.ToString() + "g", "OK");
        }

        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;

            var coffees = await _coffeeStore.GetCoffeeDripsAsync();
            foreach (var coffee in coffees)
            {
                CoffeeDrips.Add(new CoffeeViewModel(coffee));
            }
        }
    }
}
