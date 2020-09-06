using CoffeeInventory.Models;
using CoffeeInventory.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoffeeInventory.ViewModels
{
    public class CoffeesDetailViewModel
    {
        private readonly ICoffeeStore _coffeeStore;
        private readonly IPageService _pageService;
        public Coffee Coffee { get; private set; }
        public ICommand SaveCommand { get; private set; }

        //public List<CoffeeType> ListCoffeeTypes { get; set; }

        public CoffeesDetailViewModel(CoffeeViewModel viewModel, ICoffeeStore coffeeStore, IPageService pageService)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            _pageService = pageService;
            _coffeeStore = coffeeStore;

            SaveCommand = new Command(async () => await Save());

            Coffee = new Coffee
            {
                Id = viewModel.Id,
                Type = viewModel.Type,
                Density = viewModel.Density,
                IsStock = viewModel.IsStock,
            };

            //ListCoffeeTypes = PickerService.GetCoffeeTypes();
        }

        //private CoffeeType selectedCoffeeType;
        //public CoffeeType SelectedCoffeeType
        //{
        //    get { return selectedCoffeeType; }
        //    set
        //    {
        //        SetValue(ref selectedCoffeeType, value);
        //        CoffeeTypeText = selectedCoffeeType.Value;

        //    }
        //}

        //private string coffeeTypeText;
        //public string CoffeeTypeText
        //{
        //    get { return coffeeTypeText; }
        //    set
        //    {
        //        SetValue(ref coffeeTypeText, value);
        //    }
        //}

        async Task Save()
        {
            if (String.IsNullOrWhiteSpace(Coffee.Type))
            {
                await _pageService.DisplayAlert("Error", "Please enter the coffee.", "OK");
                return;
            }

            if (Coffee.Id == 0)
            {
                await _coffeeStore.AddCoffee(Coffee);
                MessagingCenter.Send(this, Events.CoffeeAdded, Coffee);
            }
            else
            {
                await _coffeeStore.UpdateCoffee(Coffee);
                MessagingCenter.Send(this, Events.CoffeeUpdated, Coffee);
            }
            await _pageService.PopAsync();
        }
    }
}
