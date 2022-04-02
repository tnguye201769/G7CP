using G7CP.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace G7CP.Converters
{
    class BrandIdToName :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var id = (int)value;
            string name;
            using (var db = new G7CPDBContext())
            {
                try
                {
                    name = db.Brands.Single(o => o.Id == id).Name;
                }
                catch
                {
                    return null;
                }
            }
            return name;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = (string)value;
            int id;
            using (var db = new G7CPDBContext())
            {
                try
                {
                    id = db.Brands.Single(o => o.Name == name).Id;
                }
                catch
                {
                    return null;
                }
            }
            return id;
        }
    }
}
