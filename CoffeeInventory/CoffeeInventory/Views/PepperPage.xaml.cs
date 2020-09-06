using CoffeeInventory.Persistence;
using CoffeeInventory.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeInventory.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PepperPage : ContentPage
    {
        public PepperPage()
        {
            var coffeeStore = new SQLiteCoffeeStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            ViewModel = new PepperPageViewModel(coffeeStore, pageService);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            ViewModel.LoadStockPeppersCommand.Execute(null);
            ViewModel.LoadSalePeppersCommand.Execute(null);
            base.OnAppearing();
        }

        public PepperPageViewModel ViewModel
        {
            get { return BindingContext as PepperPageViewModel; }
            set { BindingContext = value; }
        }
    }
}