﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using G7CP.Models;
using G7CP.Views;
using G7CP.Views.SharedPages;
using Microsoft.EntityFrameworkCore;
using G7CP.Utils;

namespace G7CP.ViewModels
{
    class HomePageViewModel : BaseViewModel
    {
        private string art;
        public string Art
        {
            get { return art; }
            set { art = value; OnPropertyChanged(); }
        }

        List<Ad> ads = null;
        public List<Ad> Ads
        {
            get { return ads; }
            set { ads = value; OnPropertyChanged(); }
        }

        private List<List<Product>> adProducts = null;
        public List<List<Product>> AdProducts
        {
            get { return adProducts; }
            set { adProducts = value; OnPropertyChanged(); }
        }

        private List<Product> topProducts = null;
        public List<Product> TopProducts
        {
            get { return topProducts; }
            set { topProducts = value; OnPropertyChanged(); }
        }

        private List<Product> recommendedProducts = null;
        public List<Product> RecommendedProducts
        {
            get { return recommendedProducts; }
            set { recommendedProducts = value; OnPropertyChanged(); }
        }

        private List<Product> discountProducts = null;
        public List<Product> DiscountProducts
        {
            get { return discountProducts; }
            set { discountProducts = value; OnPropertyChanged(); }
        }

        private async void InitAds()
        {
            using (var db = new GoninDigitalDBContext())
            {
                Ads = db.Ads.OrderBy(o => Guid.NewGuid()).Take(3).ToList();
                List<List<Product>> _adProducts = new List<List<Product>>(3);
                for (int i = 0; i < 3; i++)
                {
                    _adProducts.Add(await db.AdDetails.Where(o => o.AdId == Ads[i].Id)
                                                .Include(x => x.Product.Vendor)
                                                .Include(x => x.Product.Brand)
                                                .Select(o => o.Product)
                                                .ToListAsync());

                }
                AdProducts = _adProducts;
            }
        }

        private async void InitProducts()
        {
            using (var db = new GoninDigitalDBContext())
            {
                List<Product> randomProducts = await db.Products
                    .Include(x => x.Vendor)
                    .Include(x => x.Brand)
                    .Where(o => o.Status.Id == (int)Constants.ProductStatus.ACCEPTED)
                    .OrderBy(o => Guid.NewGuid()).Take(20).ToListAsync();

                // Top products
                var topInvoiceDetails = db.InvoiceDetails
                    .Include(x => x.Invoice)
                    .Include(x => x.Product)
                    .Where(o => o.Invoice.CreatedAt > DateTime.Now.AddDays(-7))
                    .AsEnumerable()
                    .GroupBy(x => x.ProductId)
                    .OrderByDescending(x => x.Count())
                    .ToList();

                var tmp = topInvoiceDetails.Select(o => o.Key);

                var fetchedProducts = await db.Products
                    .Where(o => tmp.Contains(o.Id) && o.StatusId == (int)Constants.ProductStatus.ACCEPTED)
                    .ToListAsync();
                if (fetchedProducts.Count < 20)
                    fetchedProducts.AddRange(randomProducts.Take(20 - fetchedProducts.Count));
                else
                    fetchedProducts = fetchedProducts.GetRange(0, 20);

                TopProducts = fetchedProducts;
                
                // Recommended Products
                RecommendedProducts = await db.Products
                    .Include(x => x.Vendor)
                    .Include(x => x.Brand)
                    .Where(o => o.StatusId == (int)Constants.ProductStatus.ACCEPTED)
                    .OrderBy(o => Guid.NewGuid()).Take(20).ToListAsync();

                // Discount Products
                fetchedProducts = await db.Products
                    .Include(x => x.Vendor)
                    .Include(x => x.Brand)
                    .Where(o => o.Price != o.OriginPrice && o.StatusId == (int)Constants.ProductStatus.ACCEPTED)
                    .OrderByDescending(o => o.UpdatedAt).ToListAsync();

                if (fetchedProducts.Count < 20)
                    fetchedProducts.AddRange(randomProducts.Take(20 - fetchedProducts.Count));
                else
                    fetchedProducts = fetchedProducts.GetRange(0, 20);

                DiscountProducts = fetchedProducts;

            }
        }

        public HomePageViewModel()
        {
            art = "/Resources/Images/HomeBanner.jpg";

            ads = new List<Ad>(3);
            adProducts = new List<List<Product>>(3);

            InitAds();
            InitProducts();
        }
    }
}