

using System.Globalization;

namespace SubtitleTranslator.Converters
{
    public class BoolColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Color color=Color.FromRgb(0, 0, 0);
            if (value is bool b)
            {
                if (b)
                {
                    if (App.Current.Resources.TryGetValue("Primary", out var colorvalue))
                        color = (Color)colorvalue;
                }
                else
                {
                    if (App.Current.Resources.TryGetValue("Black", out var colorvalue))
                        color = (Color)colorvalue;
                }
                
            }
            return color;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
