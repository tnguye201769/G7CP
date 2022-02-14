using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using G7CP.Models;
using G7CP.Utils;
using G7CP.Views;
using ModernWpf.Controls;
using G7CP.Properties;

namespace G7CP.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        #region Properties
        Window curWindow;
        private string art;
        public string Art
        {
            get { return art; }
            set { art = value; OnPropertyChanged(); }
        }
        public Action CloseAction { get; set; }
        private string _Usrname;
        public string UserName
        {
            get { return _Usrname; }
            set
            {
                _Usrname = value;
                OnPropertyChanged(UserName);
            }
        }
        private string _Passwrd;
        public string Password
        {
            get { return _Passwrd; }
            set
            {
                _Passwrd = value;
                OnPropertyChanged(Password);
            }
        }
        public ICommand LoginCommand { get; set; }
        #endregion

        #region Constructor
        public LoginViewModel(Window window)
        {
            art = "/GoninDigital;component/Resources/Images/LoginImage.jpg";
            curWindow = window;
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { LoginCommandExecute(); });
            Passwordchangedcommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { _passwrd = p.Password; });
        }
        #endregion

        #region Private Methods
        
        private void LoginCommandExecute()
        {
            if (UserName == null || Password == null)
            {
                var content = new ContentDialog();
                content.Title = "Warning";
                content.Content = "Both username and password should be filled in.";
                content.PrimaryButtonText = "Ok";
                content.ShowAsync();
                return;
            }
            int accCount = DataProvider.Instance.Db.Users.Where(x => x.UserName == _usrname && x.Password == _passwrd).Count();
            if (accCount > 0)
            {
                this.window = new Window();
                window.Show();
            }
            else
            {
                //MessageBox.Show("Invalid credentials.");
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}