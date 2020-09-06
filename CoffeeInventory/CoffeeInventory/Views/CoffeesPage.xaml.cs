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
    public partial class CoffeesPage : ContentPage
    {
        public CoffeesPage()
        {
            var coffeeStore = new SQLiteCoffeeStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            ViewModel = new CoffeesPageViewModel(coffeeStore, pageService);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }

        void OnCoffeeSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectCoffeeCommand.Execute(e.SelectedItem);
        }

        public CoffeesPageViewModel ViewModel 
        { 
            get { return BindingContext as CoffeesPageViewModel; }
            set { BindingContext = value; }
        }
    }
}