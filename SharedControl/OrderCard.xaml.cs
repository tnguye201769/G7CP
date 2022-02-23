﻿using G7CP.Models;
using G7CP.Properties;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
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

namespace G7CP.SharedControl
{
    /// <summary>
    /// Interaction logic for OrderCard.xaml
    /// </summary>
    public partial class OrderCard : UserControl
    {
        public object Image
        {
            get => (object)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }
        public object VendorName
        {
            get => (object)GetValue(VendorNameProperty);
            set => SetValue(VendorNameProperty, value);
        }
        public object ProductName
        {
            get => (object)GetValue(ProductNameProperty);
            set => SetValue(ProductNameProperty, value);
        }
        public object BrandName
        {
            get => (object)GetValue(BrandNameProperty);
            set => SetValue(BrandNameProperty, value);
        }
        public object Quantity
        {
            get => (object)GetValue(QuantityProperty);
            set => SetValue(QuantityProperty, value);
        }
        public object PriceDisc
        {
            get => (object)GetValue(PriceDiscProperty);
            set => SetValue(PriceDiscProperty, value);
        }
        public object PriceOrg
        {
            get => (object)GetValue(PriceOrgProperty);
            set => SetValue(PriceOrgProperty, value);
        }
        public object TotalPrice
        {
            get => (object)GetValue(TotalPriceProperty);
            set => SetValue(TotalPriceProperty, value);
        }
        public object Status
        {
            get => (object)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        public object Date
        {
            get => (object)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(object), typeof(OrderCard),
                new PropertyMetadata("/Resources/Images/BlankImage.jpg"));
        public static readonly DependencyProperty VendorNameProperty =
            DependencyProperty.Register("VendorName", typeof(object), typeof(OrderCard), new PropertyMetadata("Unknown"));
        public static readonly DependencyProperty ProductNameProperty =
            DependencyProperty.Register("ProductName", typeof(object), typeof(OrderCard), new PropertyMetadata("Unknown"));
        public static readonly DependencyProperty BrandNameProperty =
            DependencyProperty.Register("BrandName", typeof(object), typeof(OrderCard), new PropertyMetadata("Unknown"));
        public static readonly DependencyProperty QuantityProperty =
            DependencyProperty.Register("Quantity", typeof(object), typeof(OrderCard), new PropertyMetadata(0));
        public static readonly DependencyProperty PriceDiscProperty =
            DependencyProperty.Register("PriceDisc", typeof(object), typeof(OrderCard), new PropertyMetadata(0));
        public static readonly DependencyProperty PriceOrgProperty =
            DependencyProperty.Register("PriceOrg", typeof(object), typeof(OrderCard), new PropertyMetadata(0));
        public static readonly DependencyProperty TotalPriceProperty =
            DependencyProperty.Register("TotalPrice", typeof(object), typeof(OrderCard), new PropertyMetadata(0));
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(object), typeof(OrderCard), new PropertyMetadata("Unknown"));
        public static readonly DependencyProperty DateProperty =
           DependencyProperty.Register("Date", typeof(object), typeof(OrderCard), new PropertyMetadata(0));
        public OrderCard()
        {
            InitializeComponent();
        }

        private void addCart_Click(object sender, RoutedEventArgs e)
        {
            G7CPDBContext db = DataProvider.Instance.Db;
            int userID = db.Users.Where(x => x.UserName == Settings.Default.usrname).First().Id;
            int productID = db.Products.Where(x => x.Name == ProductName).First().Id;
            if (db.Carts.Where(x => x.UserId == userID & x.ProductId == productID).Count() == 0)
            {
                Cart cart = new Cart();
                cart.UserId = userID;
                cart.ProductId = productID;
                cart.Quantity = 1;
                db.Carts.Add(cart);
                db.SaveChanges();
            }
            else
            {
                db.Carts.Where(x => x.UserId == userID & x.ProductId == productID).First().Quantity += 1;
                db.SaveChanges();
            }
            ContentDialog content = new()
            {
                Title = "Nortification",

                Content = "Added this item to your cart successfully!",
                PrimaryButtonText = "Ok"
            };
            content.ShowAsync();
        }
    }
}