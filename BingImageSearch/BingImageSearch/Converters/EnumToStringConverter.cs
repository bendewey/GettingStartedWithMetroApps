using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingImageSearch.Model;
using BingImageSearch.ViewModels;
using Windows.UI.Xaml.Data;

namespace BingImageSearch.Converters
{
    public class ResultSizeEnumToStringConverter : EnumToStringConverter
    {
        public ResultSizeEnumToStringConverter()
        {
            EnumType = typeof(ResultSize);
        }
    }

    public class ThumbnailSizeEnumToStringConverter : EnumToStringConverter
    {
        public ThumbnailSizeEnumToStringConverter()
        {
            EnumType = typeof(ThumbnailSize);
        }
    }

    public class RatingEnumToStringConverter : EnumToStringConverter
    {
        public RatingEnumToStringConverter()
        {
            EnumType = typeof(Rating);
        }
    }

    public abstract class EnumToStringConverter : IValueConverter
    {
        protected Type EnumType;
        
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var selected = (int)value;
            var current = (int)Enum.Parse(EnumType, (string)parameter);

            return selected == current;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var selected = (bool)value;
            
            return selected ? (int)Enum.Parse(EnumType, (string)parameter) : 0;
       } 
    }
}
