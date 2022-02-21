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
using Microsoft.EntityFrameworkCore;
using ModernWpf.Controls;

namespace G7CP.ViewModels
{
    class CartPageViewModel : BaseViewModel
    {
        private ObservableCollection<Cart> products;
        public ObservableCollection<Cart> Products
        {
            get { return products; }
            set { products = value; OnPropertyChanged(); }
        }

        private void Init()
        {
            using (var db = new GoninDigitalDBContext())
            {
                Products = new ObservableCollection<Cart>(db.Carts.Include(x => x.User)
                                .Include(x => x.Product)
                                .Include(x => x.Product.Vendor)
                                .Where(o => o.User.UserName == Settings.Default.usrname)
                                .ToList());
            }
        }

        public void OnNavigatedTo()
        {
            Thread thread = new Thread(Init);
            thread.Start();
        }

        public CartPageViewModel()
        {

        }
    }
}
