using CoffeeInventory.Models;
using CoffeeInventory.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CoffeeInventory.ViewModels
{
    public class CoffeeViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public CoffeeViewModel() { }

        public CoffeeViewModel(Coffee coffee)
        {
            Id = coffee.Id;
            _type = coffee.Type;
            _density = coffee.Density;
            IsStock = coffee.IsStock;
        }

        private string _typeValue;
        public string TypeValue
        {
            get { return _typeValue; }
            set
            {
                SetValue(ref _typeValue, value);
                OnPropertyChanged();
            }
        }

        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                SetValue(ref _type, value);
                OnPropertyChanged(nameof(DensityAndType));
            }
        }

        private int _density;
        public int Density
        {
            get { return _density; }
            set
            {
                SetValue(ref _density, value);
                OnPropertyChanged(nameof(DensityAndType));
            }
        }

        private bool _isStock;
        public bool IsStock
        {
            get { return _isStock; }
            set
            {
                SetValue(ref _isStock, value);
                OnPropertyChanged(nameof(CoffeeImage));
            }
        }

        public string DensityAndType
        {
            get { return $"{DensityInGrams} {Type}"; }
        }
        public string DensityInGrams
        {
            get { return $"{Density}g"; }
        }

        public ImageSource CoffeeImage
        {
            get
            {
                if (IsStock)
                {
                    return ImageSource.FromResource("CoffeeInventory.Assets.Images.StockImage.png");
                }
                return ImageSource.FromResource("CoffeeInventory.Assets.Images.SaleImage.png");
            }
        }
    }
}
