using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using G7CP.Models;
using G7CP.Utils;
using ModernWpf.Controls;
using G7CP.Properties;

namespace G7CP.ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
        Window curWindow;
        private string art;
        public string Art
        {
            get { return art; }
            set { art = value; OnPropertyChanged(); }
        }
        public Action CloseAction { get; set; }
        enum LGender
        {
            Other = 0,
            Female = 1,
            Male = 2
        };
        enum LTypeU
        {
            Admin = 1,
            Seller = 2,
            Customer = 6
        }
        private string _UserName;
        public string UserName
        {
            get => _UserName;
            set
            {
                _UserName = value;
                OnPropertyChanged();
            }
        }
        private string _Password;
        public string Password
        {
            get => _Password;
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }
        private string _FirstName;
        public string FirstName
        {
            get => _FirstName;
            set
            {
                _FirstName = value;
                OnPropertyChanged();
            }
        }
        private string _LastName;
        public string LastName
        {
            get => _LastName;
            set
            {
                _LastName = value;
                OnPropertyChanged();
            }
        }
        private ComboBoxItem _Gender;
        public ComboBoxItem Gender
        {
            get => _Gender;
            set
            {
                _Gender = value;
                OnPropertyChanged();
            }
        }
        private DateTime _DoB = DateTime.Now;
        public DateTime DoB
        {
            get => _DoB;
            set
            {
                _DoB = value;
                OnPropertyChanged();
            }
        }
        private ComboBoxItem _TypeUser;
        public ComboBoxItem TypeUser
        {
            get => _TypeUser;
            set
            {
                _TypeUser = value;
                OnPropertyChanged();
            }
        }
        private string _Email;
        public string Email
        {
            get => _Email;
            set
            {
                _Email = value;
                OnPropertyChanged();
            }
        }
        private string _PhoneNumber;
        public string PhoneNumber
        {
            get => _PhoneNumber;
            set
            {
                _PhoneNumber = value;
                OnPropertyChanged();
            }
        }
        public bool CanRegister => !string.IsNullOrEmpty(Email) &&
            !string.IsNullOrEmpty(UserName) &&
            !string.IsNullOrEmpty(FirstName) &&
            !string.IsNullOrEmpty(LastName) &&
            !string.IsNullOrEmpty(PhoneNumber) &&
            !string.IsNullOrEmpty(Password);

        public ICommand RegisterCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public RegisterViewModel(Window p)
        {
            art = "/GoninDigital;component/Resources/Images/LoginImage.jpg";
            curWindow = p;
            RegisterCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { RegisterExecute(); });
            CancelCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { CancelExecute(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }
        void RegisterExecute()
        {
            if (!CanRegister)
                MessageBox.Show("Chưa nhập đủ thông tin");
            else
            {
                ContentDialog content = new()
                {
                    MessageBox.Show("Password va Confirm Password không khớp");
                }
                else
                {
                    int checkUsername = DataProvider.Instance.Db.Users.Where(x => x.UserName == UserName).Count();
                    int checkEmail = DataProvider.Instance.Db.Users.Where(x => x.Email == Email).Count();
                    if (checkUsername > 0 || checkEmail > 0)
                    {
                        _ = MessageBox.Show("Tên tài khoản hoặc email đã tồn tại");
                    }
                    else
                    {
                        try
                        {
                            _ = Enum.TryParse(TypeUser.Content.ToString(), out LTypeU _Usertype);
                            _ = Enum.TryParse(Gender.Content.ToString(), out LGender _Gendertype);

                        User new_user = new()
                        {
                            UserName = UserName,
                            Password = Cryptography.MD5Hash(Cryptography.Base64Encode(Password)),
                            TypeId = (int)_Usertype,
                            FirstName = FirstName,
                            LastName = LastName,
                            PhoneNumber = PhoneNumber,
                            Email = Email,
                            Gender = (byte)_Gendertype,
                            DateOfBirth = DoB
                        };

                        AccountManager.RegisterAccount(new_user);

                            _ = MessageBox.Show("Đăng kí thành công");
                        }
                        catch
                        {
                            _ = MessageBox.Show("Đăng kí không thành công!");
                        }
                    }
                }
            }
        }
        void CancelExecute()
        {
            curWindow.Close();
        }
    }
}
