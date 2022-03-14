using G7CP.Models;
using G7CP.Properties;
using G7CP.Utils;
using G7CP.ViewModels;
using G7CP.Views.DashBoardPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Page = ModernWpf.Controls.Page;

namespace G7CP.Views.DashBoardPages
{
    /// <summary>
    /// Interaction logic for MyShopPage.xaml
    /// </summary>
    public partial class MyShopPage : Page
    {
        private Dictionary<string, Page> pages;
        
        public MyShopPage()
        {
            this.DataContext = new MyShopViewModel();
            InitializeComponent();
            pages = new Dictionary<string, Page>();
        }

    }
}
