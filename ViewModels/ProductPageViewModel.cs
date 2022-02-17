using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using G7CP.Models;
using G7CP.Views.SharedPages;
namespace G7CP.ViewModels
{
    class ProductPageViewModel : BaseViewModel
    {
        private string isDisc;
        public string IsDisc
        {
            get { return isDisc; }
            set { isDisc = value; OnPropertyChanged(); }
        }
        private string productImage;
        public string ProductImage
        {
            get { return productImage; }
            set { productImage = value; OnPropertyChanged(); }
        }
        private string vendorAvatar;
        public string VendorAvatar
        {
            get { return vendorAvatar; }
            set { vendorAvatar = value; OnPropertyChanged(); }
        }
        private int productId;
        public int ProductId
        {
            get => productId;
            set { productId = value; OnPropertyChanged(); }
        }
        private string productType;
        public string ProductType
        {
        }
        {
        }
        {
        }
        {
        }
        {
        }
        {
        }
        {
        }
        {
        }
        {
        }
        {
        }
        Product product = new Product();

        Product product = new Product();
        public ProductPageViewModel()
        {
            else
        }
    }
}
