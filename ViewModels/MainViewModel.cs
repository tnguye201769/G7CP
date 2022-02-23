using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using G7CP.Models;
using G7CP.Views;
using System.Windows.Input;
using G7CP.Utils;
using G7CP.Properties;
using G7CP.SharedControl;
using System.Windows.Media;

namespace G7CP.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand LoadedWidnowCommand { get; set; }
        public MainViewModel()
        {
            if(Settings.Default.accentColor != "")
            {
                ThemeManagerProxy.Current.AccentColor = (Color)ColorConverter.ConvertFromString(Settings.Default.accentColor);
            }
            if (!Settings.Default.systemTheme)
            {
                if (Settings.Default.theme)
                {
                    ThemeManagerProxy.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Light;
                }
                else
                {
                    ThemeManagerProxy.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Dark;
                }
            }
            LoadedWidnowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                //initialize the splash screen and set it as the application main window
                WindowManager.ChangeWindowContent(p, "", "G7CP.Views.SplashScreenView");

                //in order to ensure the UI stays responsive, we need to
                //do the work on a different thread
                Task.Factory.StartNew(() =>
                {
                    //we need to do the work in batches so that we can report progress
                    G7CPDBContext db = new();
                    db.Database.EnsureCreated();
                    db.Products.ToList();
                    db.Brands.ToList();
                    db.AdDetails.ToList();
                    db.Ads.ToList();
                    db.Brands.ToList();
                    db.Favorites.ToList();
                    db.InvoiceDetails.ToList();
                    db.Invoices.ToList();
                    db.InvoiceStatuses.ToList();
                    db.ProductCategories.ToList();
                    db.ProductImages.ToList();
                    db.ProductImages.ToList();
                    db.ProductSpecDetails.ToList();
                    db.ProductSpecs.ToList();
                    db.Ratings.ToList();
                    db.Vendors.ToList();

                    //once we're done we need to use the Dispatcher
                    //to create and show the main window
                    p.Dispatcher.Invoke(() =>
                    {
                        //initialize the main window, set it as the application main window
                        //and close the splash screen
                        if (Settings.Default.usrname != "")
                        {
                            var dashboardWindow = new DashBoard();
                            WindowManager.ChangeWindowContent(p, dashboardWindow, Resources.HomepageWindowTitle, Resources.HomepageControlPath);
                        }
                        else //login
                        {
                            //var loginWindow = new LoginViewModel(p);
                            WindowManager.ChangeWindowContent(p, Resources.LoginWindowTitle, Resources.LoginControlPath);
                        }
                    });
                });
            }); 
        }
    }
}
