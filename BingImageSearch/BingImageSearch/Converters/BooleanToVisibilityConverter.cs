using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace BingImageSearch.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type typeName, object parameter, string language)
        {
            return ((value as bool?) ?? true) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type typeName, object parameter, string language)
        {
            return ((value as Visibility?) ?? Visibility.Visible) == Visibility.Visible ? true : false;
        }
    }
}
