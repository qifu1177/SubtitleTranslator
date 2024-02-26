using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.UI.Infrastructure.Converters
{
    public class InvertBoolConverter : IValueConverter
    {

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isVisible)
            {
                var isInvert = parameter as bool? ?? false;

                return isVisible && !isInvert ? false : true;
            }

            throw new ArgumentException();
        }        

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
