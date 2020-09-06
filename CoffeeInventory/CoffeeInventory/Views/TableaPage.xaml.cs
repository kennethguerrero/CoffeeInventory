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
    public partial class TableaPage : ContentPage
    {
        public TableaPage()
        {
            var coffeeStore = new SQLiteCoffeeStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            ViewModel = new TableaPageViewModel(coffeeStore, pageService);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            ViewModel.LoadStockTableasCommand.Execute(null);
            ViewModel.LoadSaleTableasCommand.Execute(null);
            base.OnAppearing();
        }

        public TableaPageViewModel ViewModel
        {
            get { return BindingContext as TableaPageViewModel; }
            set { BindingContext = value; }
        }
    }
}