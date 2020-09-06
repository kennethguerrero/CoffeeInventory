using CoffeeInventory.Models;
using CoffeeInventory.Views;
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
    public class CoffeesPageViewModel : BaseViewModel
    {
        private CoffeeViewModel _selectedCoffee;
        private ICoffeeStore _coffeeStore;
        private IPageService _pageService;

        private bool _isDataLoaded;

        public ObservableCollection<CoffeeViewModel> Coffees { get; private set; }
            = new ObservableCollection<CoffeeViewModel>();

        public CoffeeViewModel SelectedCoffee
        {
            get { return _selectedCoffee; }
            set { SetValue(ref _selectedCoffee, value); }
        }

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddCoffeeCommand { get; private set; }
        public ICommand SelectCoffeeCommand { get; private set; }
        public ICommand DeleteCoffeeCommand { get; private set; }
        public ICommand CheckStockCommand { get; private set; }

        public CoffeesPageViewModel(ICoffeeStore coffeeStore, IPageService pageService)
        {
            _coffeeStore = coffeeStore;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            AddCoffeeCommand = new Command(async () => await AddCoffee());
            SelectCoffeeCommand = new Command<CoffeeViewModel>(async c => await SelectCoffee(c));
            DeleteCoffeeCommand = new Command<CoffeeViewModel>(async c => await DeleteCoffee(c));

            CheckStockCommand = new Command(async c => await CheckStock());

            MessagingCenter.Subscribe<CoffeesDetailViewModel, Coffee>
                (this, Events.CoffeeAdded, OnCoffeeAdded);

            MessagingCenter.Subscribe<CoffeesDetailViewModel, Coffee>
                (this, Events.CoffeeUpdated, OnCoffeeUpdated);
        }


        private async Task CheckStock()
        {
            await _pageService.PushAsync(new MainStockPage());
        }

        private void OnCoffeeAdded(CoffeesDetailViewModel source, Coffee coffee)
        {
            Coffees.Add(new CoffeeViewModel(coffee));
        }

        private void OnCoffeeUpdated(CoffeesDetailViewModel source, Coffee coffee)
        {
            var coffeeInList = Coffees.Single(c => c.Id == coffee.Id);

            coffeeInList.Id = coffee.Id;
            coffeeInList.Type = coffee.Type;
            coffeeInList.Density = coffee.Density;
            coffeeInList.IsStock = coffee.IsStock;
        }

        public async Task LoadData()
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;

            var coffees = await _coffeeStore.GetCoffeeAsync();
            foreach (var coffee in coffees)
            {
                Coffees.Add(new CoffeeViewModel(coffee));
            }
        }

        private async Task AddCoffee()
        {
            await _pageService.PushAsync(new CoffeesDetailPage(new CoffeeViewModel()));
        }

        private async Task SelectCoffee(CoffeeViewModel coffee)
        {
            if (coffee == null)
                return;

            SelectedCoffee = null;
            await _pageService.PushAsync(new CoffeesDetailPage(coffee));
        }

        private async Task DeleteCoffee(CoffeeViewModel coffeeViewModel)
        {
            if (await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {coffeeViewModel.DensityAndType}", "Yes", "No"))
            {
                Coffees.Remove(coffeeViewModel);

                var coffee = await _coffeeStore.GetCoffee(coffeeViewModel.Id);
                await _coffeeStore.DeleteCoffee(coffee);
            }
        }
    }
}
