using G7CP.Models;
using G7CP.Views;
using G7CP.Views.SharedPages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace G7CP.SharedControl
{
    /// <summary>
    /// Interaction logic for ProductCard.xaml
    /// </summary>
    public partial class ProductCard : UserControl
    {
        public Product ProductInfo
        {
            get => (Product)GetValue(ProductProperty);
            set => SetValue(ProductProperty, value);
        }
        public bool Clickable
        {
            get => (bool)GetValue(ClickableProperty);
            set => SetValue(ClickableProperty, value);
        }

        public static readonly DependencyProperty ProductProperty =
            DependencyProperty.Register("ProductInfo", typeof(Product), typeof(ProductCard), new PropertyMetadata(new Product()));
        public static readonly DependencyProperty ClickableProperty =
            DependencyProperty.Register("Clickable", typeof(bool), typeof(ProductCard), new PropertyMetadata(true));

        public ProductCard()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductInfo != null && ProductInfo.Id != 0 && Clickable)
            {
                DashBoard.RootFrame.Navigate(new ProductPage(ProductInfo));
            }
        }
    }
}
