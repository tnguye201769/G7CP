﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using G7CP.Models;
using G7CP.Properties;

namespace G7CP.ViewModels
{
    class MyShopViewModel:BaseViewModel
    {
        private string nameShop;
        public string NameShop
        {
            get { return nameShop; }
            set { nameShop = value; OnPropertyChanged(); }
        }
        private string avatar;
        public string Avatar
        {
            get { return avatar; }
            set { avatar = value; OnPropertyChanged(); }
        }
        private string cover;
        public string Cover
        {
            get { return cover; }
            set { cover = value; OnPropertyChanged(); }
        }
        private string mail;
        public string Mail
        {
            get { return mail; }
            set { mail = value; OnPropertyChanged(); }
        }
        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged(); }
        }
        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(); }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }
        private List<Product> productList;
        public List<Product> ProductList
        {
            get { return productList; }
            set { productList = value; OnPropertyChanged(); }
        }


        
        public MyShopViewModel()
        {

            GoninDigitalDBContext db = DataProvider.Instance.Db;
            var usrname = Settings.Default.usrname;
            
            var usr = db.Users.Single(p => p.UserName == usrname);

            var vendor = db.Vendors.Single(p => p.OwnerId == usr.Id);
            var product = db.Products.Where(p => p.VendorId == vendor.Id).ToList();
            productList = product;

            cover = vendor.Cover;
            nameShop = vendor.Name;
            avatar = vendor.Avatar;
            address = vendor.Address;
            phone = vendor.Phone;
            
            description = vendor.Description;
        }

    }
}
