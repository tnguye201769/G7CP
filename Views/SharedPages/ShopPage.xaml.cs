﻿using G7CP.Models;
using G7CP.Utils;
using G7CP.ViewModels;
using Microsoft.EntityFrameworkCore;
using ModernWpf.Controls;
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

namespace G7CP.Views.SharedPages
{
    /// <summary>
    /// Interaction logic for ShopPage.xaml
    /// </summary>
    public partial class ShopPage : Page
    {
        public Vendor Vendor { get; set; }
        public ObservableCollection<Product> ProductBestSeller { get; set; }
        public ObservableCollection<Product> ProductSpecial { get; set; }

        public ShopPage(int vendorId)
        {
            using (var db = new GoninDigitalDBContext())
            {
                try
                {
                    Vendor = db.Vendors
                        .Include(o => o.Products)
                        .ThenInclude(o => o.Category)
                        .First(o => o.Id == vendorId && o.ApprovalStatus == 1);
                    var Products = new ObservableCollection<Product>(Vendor.Products.Where(o => o.StatusId == (int)Constants.ProductStatus.ACCEPTED).ToList());
                    ProductBestSeller = new ObservableCollection<Product>(Vendor.Products.Where(o => o.StatusId == (int)Constants.ProductStatus.ACCEPTED).Take(10).ToList());
                    ProductSpecial = new ObservableCollection<Product>(Vendor.Products.Where(o => o.StatusId == (int)Constants.ProductStatus.ACCEPTED).Take(5).ToList());
                }
                catch
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Something went wrong",
                        Content = "This shop is no longer available",
                        PrimaryButtonText = "Ok",
                        PrimaryButtonCommand = new RelayCommand<object>(o => true, o => { DashBoard.RootFrame.GoBack(); }) 
                    };
                    dialog.ShowAsync();
                }

            }
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            // Checkout page should not in back stack
            DashBoard.RootFrame.RemoveBackEntry();
        }
    }
}