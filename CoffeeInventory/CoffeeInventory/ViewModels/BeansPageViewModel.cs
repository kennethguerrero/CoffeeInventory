using CoffeeInventory.Models;
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
    public class BeansPageViewModel : BaseViewModel
    {
        private ICoffeeStore _coffeeStore;
        private IPageService _pageService;

        private bool _isDataLoaded;
        private int TotalStockBeans;
        private int TotalSaleBeans;
        private int RemainingStock;

        public ObservableCollection<CoffeeViewModel> CoffeeBeans { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();

        public ObservableCollection<CoffeeViewModel> SaleBeans { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();

        public ObservableCollection<CoffeeViewModel> StockBeans { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();     
        
        public ICommand LoadCoffeeBeansCommand { get; private set; }
        public ICommand LoadDataCommand { get; private set; }
        public ICommand LoadSalesCommand { get; private set; }

        public ICommand CheckStockBeansCommand { get; private set; }

        public BeansPageViewModel(ICoffeeStore coffeeStore, IPageService pageService)
        {
            _coffeeStore = coffeeStore;
            _pageService = pageService;

            LoadCoffeeBeansCommand = new Command(async () => await LoadCoffeeBeans());
            LoadDataCommand = new Command(async () => await LoadData());
            LoadSalesCommand = new Command(async () => await LoadSales());

            CheckStockBeansCommand = new Command(async () => await CheckStockBeans());
        }

        private async Task LoadCoffeeBeans()
        {
            _isDataLoaded = true;

            var coffees = await _coffeeStore.GetCoffeeBeansAsync();
            foreach (var coffee in coffees)
            {
                CoffeeBeans.Add(new CoffeeViewModel(coffee));
            }
        }

        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;

            var coffees = await _coffeeStore.GetStockBeansAsync();
            foreach (var coffee in coffees)
            {
                StockBeans.Add(new CoffeeViewModel(coffee));
            }
        }

        private async Task LoadSales()
        {
            //if (_isDataLoaded)
            //    return;

            _isDataLoaded = true;

            var coffeeSales = await _coffeeStore.GetSaleBeansAsync();
            foreach (var coffeeSale in coffeeSales)
            {
                SaleBeans.Add(new CoffeeViewModel(coffeeSale));
            }
        }

        private async Task CheckStockBeans()
        {
            TotalStockBeans = StockBeans.Sum(b => b.Density);
            TotalSaleBeans = SaleBeans.Sum(b => b.Density);

            RemainingStock = TotalStockBeans - TotalSaleBeans;

            await _pageService.DisplayAlert("Stock Beans", "Remaining: " + RemainingStock.ToString() + "g" , "OK");
        }
    }
}
