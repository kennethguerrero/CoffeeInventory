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
    public class TableaPageViewModel : BaseViewModel
    {
        private ICoffeeStore _coffeeStore;
        private IPageService _pageService;

        private bool _isDataLoaded;
        private int TotalStockTableas;
        private int TotalSaleTableas;
        private int RemainingStock;

        public ObservableCollection<CoffeeViewModel> Tableas { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();

        public ObservableCollection<CoffeeViewModel> StockTableas { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();

        public ObservableCollection<CoffeeViewModel> SaleTableas { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();

        public ICommand LoadDataCommand { get; private set; }
        public ICommand LoadStockTableasCommand { get; private set; }
        public ICommand LoadSaleTableasCommand { get; private set; }
        public ICommand CheckRemainingStockCommand { get; private set; }

        public TableaPageViewModel(ICoffeeStore coffeeStore, IPageService pageService)
        {
            _coffeeStore = coffeeStore;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            LoadStockTableasCommand = new Command(async () => await LoadStockTableas());
            LoadSaleTableasCommand = new Command(async () => await LoadSaleTablea());
            CheckRemainingStockCommand = new Command(async () => await CheckRemainingStock());
        }

        private async Task LoadSaleTablea()
        {
            _isDataLoaded = true;

            var coffees = await _coffeeStore.GetSaleTableasAsync();
            foreach (var coffee in coffees)
            {
                SaleTableas.Add(new CoffeeViewModel(coffee));
            }
        }

        private async Task LoadStockTableas()
        {
            _isDataLoaded = true;

            var coffees = await _coffeeStore.GetStockTableasAsync();
            foreach (var coffee in coffees)
            {
                StockTableas.Add(new CoffeeViewModel(coffee));
            }
        }

        private async Task CheckRemainingStock()
        {
            TotalStockTableas = StockTableas.Sum(t => t.Density);
            TotalSaleTableas = SaleTableas.Sum(t => t.Density);
            RemainingStock = TotalStockTableas - TotalSaleTableas;

            await _pageService.DisplayAlert("Stock Tableas", "Remaining: " + RemainingStock.ToString() + "g", "OK");
        }

        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;

            var coffees = await _coffeeStore.GetTableasAsync();
            foreach (var coffee in coffees)
            {
                Tableas.Add(new CoffeeViewModel(coffee));
            }
        }
    }
}
