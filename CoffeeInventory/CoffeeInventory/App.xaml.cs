using CoffeeInventory.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeInventory
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new CoffeesPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
