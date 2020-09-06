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
    public partial class BeansPage : ContentPage
    {
        public BeansPage()
        {
            var coffeeStore = new SQLiteCoffeeStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            ViewModel = new BeansPageViewModel(coffeeStore, pageService);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            ViewModel.LoadSalesCommand.Execute(null);
            ViewModel.LoadCoffeeBeansCommand.Execute(null);
            base.OnAppearing();
        }

        public BeansPageViewModel ViewModel
        {
            get { return BindingContext as BeansPageViewModel; }
            set { BindingContext = value; }
        }
    }
}