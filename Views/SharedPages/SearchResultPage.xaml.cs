﻿using G7CP.Models;
using G7CP.Properties;
using G7CP.Utils;
using G7CP.ViewModels;
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
    public partial class SearchResultPage : Page
    {
        public static Stack<string> OldQuery { get; set; } = new Stack<string>();

        public List<Product> Products { get; set; }
        public List<Vendor> Vendors { get; set; }
        public string Query { get; set; }

        public SearchResultPage(string query)
        {
            Query = query;
            using (var context = new GoninDigitalDBContext())
            {
                var productResult = context.Products.Where(
                            product => product.StatusId == (int)Constants.ProductStatus.ACCEPTED
                            && product.Name.Contains(query)
                        ).ToList();
                if (productResult.Count > 20)
                    productResult = productResult.GetRange(0, 30).ToList();
                Products = productResult;

                var vendorResult = context.Vendors.Where(
                        vendor => vendor.Name.Contains(query)
                        && vendor.ApprovalStatus == (int)Constants.ApprovalStatus.APPROVED)
                        .ToList();
                if (vendorResult.Count > 10)
                    vendorResult = vendorResult.GetRange(0, 10).ToList();
                Vendors = vendorResult;
            }

            InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode != NavigationMode.Back)
                OldQuery.Push(Query);
        }

        public SearchResultPage() : this(OldQuery.Pop()) { }
    }
}