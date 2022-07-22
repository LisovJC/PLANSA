using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PLANSA.Converter
{
    public class BackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime date)
            {
                int daysLeft = (date - DateTime.Today).Days;

                //return new SolidColorBrush(daysLeft switch
                //{
                //    <= 2 and > 0 => Colors.LightCoral,
                //    <= 4 and > 2 => Colors.Khaki,
                //    > 4 => Colors.LightGreen,
                //    _ => Colors.Red
                //});
                return new SolidColorBrush(Colors.Red);
            }
            return new ArgumentException(null, nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}