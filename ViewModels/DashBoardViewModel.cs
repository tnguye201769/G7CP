using G7CP.ViewModels.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G7CP.ViewModels
{
    internal class DashBoardViewModel: BaseViewModel
    {
        private string hasVendor;
        public string HasVendor
        {
            get { return hasVendor; }
            set { hasVendor = value; OnPropertyChanged(); }
        }
        public DashBoardViewModel()
        {
            HasVendor = "Collapsed";
        }
    }
}
