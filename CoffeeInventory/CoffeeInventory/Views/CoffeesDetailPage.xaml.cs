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
    public partial class CoffeesDetailPage : ContentPage
    {
        public CoffeesDetailPage(CoffeeViewModel viewModel)
        {
            InitializeComponent();
            var coffeeStore = new SQLiteCoffeeStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            Title = (viewModel.Type == null) ? "New Coffee" : "Edit Coffee";
            BindingContext = new CoffeesDetailViewModel(viewModel ?? new CoffeeViewModel(), coffeeStore, pageService);
        }
    }
}