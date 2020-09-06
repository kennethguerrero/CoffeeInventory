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
    public partial class GroundsPage : ContentPage
    {
        public GroundsPage()
        {
            var coffeeStore = new SQLiteCoffeeStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            ViewModel = new GroundsPageViewModel(coffeeStore, pageService);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            ViewModel.LoadStockGroundsCommand.Execute(null);
            ViewModel.LoadSaleGroundsCommand.Execute(null);
            base.OnAppearing();
        }

        public GroundsPageViewModel ViewModel
        {
            get { return BindingContext as GroundsPageViewModel; }
            set { BindingContext = value; }
        }
    }
}