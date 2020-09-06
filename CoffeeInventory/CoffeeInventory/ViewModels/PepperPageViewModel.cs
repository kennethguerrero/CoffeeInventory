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
    public class PepperPageViewModel : BaseViewModel
    {
        private ICoffeeStore _coffeeStore;
        private IPageService _pageService;

        private bool _isDataLoaded;
        private int TotalStockPeppers;
        private int TotalSalePeppers;
        private int RemainingStock;

        public ObservableCollection<CoffeeViewModel> Peppers { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();

        public ObservableCollection<CoffeeViewModel> StockPeppers { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();

        public ObservableCollection<CoffeeViewModel> SalePeppers { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();

        public ICommand LoadDataCommand { get; private set; }
        public ICommand LoadStockPeppersCommand { get; private set; }
        public ICommand LoadSalePeppersCommand { get; private set; }
        public ICommand CheckRemainingStockCommand { get; private set; }

        public PepperPageViewModel(ICoffeeStore coffeeStore, IPageService pageService)
        {
            _coffeeStore = coffeeStore;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            LoadStockPeppersCommand = new Command(async () => await LoadStockPeppers());
            LoadSalePeppersCommand = new Command(async () => await LoadSalePeppers());
            CheckRemainingStockCommand = new Command(async () => await CheckRemainingStock());
        }

        private async Task LoadSalePeppers()
        {
            _isDataLoaded = true;

            var coffees = await _coffeeStore.GetSalePeppersAsync();
            foreach (var coffee in coffees)
            {
                SalePeppers.Add(new CoffeeViewModel(coffee));
            }
        }

        private async Task LoadStockPeppers()
        {
            _isDataLoaded = true;

            var coffees = await _coffeeStore.GetStockPeppersAsync();
            foreach (var coffee in coffees)
            {
                StockPeppers.Add(new CoffeeViewModel(coffee));
            }
        }

        private async Task CheckRemainingStock()
        {
            TotalStockPeppers = StockPeppers.Sum(p => p.Density);
            TotalSalePeppers = SalePeppers.Sum(p => p.Density);
            RemainingStock = TotalStockPeppers - TotalSalePeppers;

            await _pageService.DisplayAlert("Stock Peppers", "Remaining: " + RemainingStock.ToString() + "g", "OK");
        }

        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;

            var coffees = await _coffeeStore.GetPeppersAsync();
            foreach (var coffee in coffees)
            {
                Peppers.Add(new CoffeeViewModel(coffee));
            }
        }
    }
}
