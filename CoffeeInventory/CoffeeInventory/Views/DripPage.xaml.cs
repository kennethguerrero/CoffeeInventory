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
    public partial class DripPage : ContentPage
    {
        public DripPage()
        {
            var coffeeStore = new SQLiteCoffeeStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            ViewModel = new DripPageViewModel(coffeeStore, pageService);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            ViewModel.LoadStockDripsCommand.Execute(null);
            ViewModel.LoadSaleDripsCommand.Execute(null);
            base.OnAppearing();
        }

        public DripPageViewModel ViewModel
        {
            get { return BindingContext as DripPageViewModel; }
            set { BindingContext = value; }
        }
    }
}