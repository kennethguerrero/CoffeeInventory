using CoffeeInventory.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoffeeInventory.ViewModels
{
    public class MainStockPageViewModel
    {
        public ICommand NavigateBeansPageCommand { get; private set; }
        public ICommand NavigateGroundsPageCommand { get; private set; }
        public ICommand NavigateDripPageCommand { get; private set; }
        public ICommand NavigateTableaPageCommand { get; private set; }
        public ICommand NavigatePepperPageCommand { get; private set; }

        public MainStockPageViewModel()
        {
            NavigateBeansPageCommand = new Command(async () => await NavigateBeansPage());
            NavigateGroundsPageCommand = new Command(async () => await NavigateGroundsPage());
            NavigateDripPageCommand = new Command(async () => await NavigateDripPage());
            NavigateTableaPageCommand = new Command(async () => await NavigateTableaPage());
            NavigatePepperPageCommand = new Command(async () => await NavigatePepperPage());
        }

        private async Task NavigatePepperPage()
        {
            await MainPage.Navigation.PushAsync(new PepperPage());
        }

        private async Task NavigateTableaPage()
        {
            await MainPage.Navigation.PushAsync(new TableaPage());
        }

        private async Task NavigateDripPage()
        {
            await MainPage.Navigation.PushAsync(new DripPage());
        }

        private async Task NavigateGroundsPage()
        {
            await MainPage.Navigation.PushAsync(new GroundsPage());
        }

        private async Task NavigateBeansPage()
        {
            await MainPage.Navigation.PushAsync(new BeansPage());
        }

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }
    }
}
