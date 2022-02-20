﻿using System;
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
using G7CP.ViewModels;
using ModernWpf.Demo.ThreadedUI;

namespace G7CP.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        public LoginViewModel ViewModel { get; } = new(Application.Current.MainWindow);

        private void ProgressControlHost_ChildChanged(object sender, EventArgs e)
        {
            var host = (ThreadedVisualHost)sender;
            if (host.Child is ThreadedProgressBar progressBar)
            {
                progressBar.SetBinding(ThreadedProgressBar.IsIndeterminateProperty, new Binding(nameof(ViewModel.IsBusy)) { Source = ViewModel });
            }
            else if (host.Child is ThreadedProgressRing progressRing)
            {
                progressRing.SetBinding(ThreadedProgressRing.IsActiveProperty, new Binding(nameof(ViewModel.IsBusy)) { Source = ViewModel });
            }
        }
    }
}
