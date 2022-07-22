using PLANSA.ViewModel.Windows;
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
                double hoursLeft = (date - DateTime.Today).TotalHours;

                if (hoursLeft >= 96)
                    return (Brush)new BrushConverter().ConvertFrom(PlansaViewModel.normalColor);

                if ((hoursLeft < 96) && (hoursLeft >= 72))
                    return (Brush)new BrushConverter().ConvertFrom(PlansaViewModel.lessNormalColor);

                if ((hoursLeft < 72) && (hoursLeft >= 48))
                    return (Brush)new BrushConverter().ConvertFrom(PlansaViewModel.warningColor);

                if ((hoursLeft < 48) && (hoursLeft >= 32))
                    return (Brush)new BrushConverter().ConvertFrom(PlansaViewModel.lessWarningColor);

                if (hoursLeft < 32)
                    return (Brush)new BrushConverter().ConvertFrom(PlansaViewModel.criticalColor);
            }
            return new ArgumentException(null, nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}