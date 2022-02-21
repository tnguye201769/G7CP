using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using G7CP.Models;
using G7CP.Properties;
using G7CP.SharedControl;
using G7CP.Views;

namespace G7CP.ViewModels
{
    class CartPageViewModel : BaseViewModel
    {
        

        private List<Product> recommnededByEditor;
        public List<Product> RecommendedByEditor
        {
            get { return recommnededByEditor; }
            set { recommnededByEditor = value; OnPropertyChanged(); }
        }
        public List<Product> RecommendedByEditor3
        {
            get { return recommnededByEditor.GetRange(0, 3); }
        }
        public ICommand PurchaseCommand { get; set; }

        public CartPageViewModel()
        {
  
            GoninDigitalDBContext db = DataProvider.Instance.Db;
            recommnededByEditor = db.Products.ToList();

            PurchaseCommand = new RelayCommand<object>((p) => { return true; }, (p) => { DashBoard.RootFrame.Navigate(new CartPage_Purchase()); });
        }

    }
}
