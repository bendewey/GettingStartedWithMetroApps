using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BingImageSearch.Converters
{
    public class DateConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            DateTime date = (DateTime)value;

            if ((string)parameter == "day")
            {
                // Date "7/27/2011 9:30:59 AM" returns "27"
                return date.Day.ToString();
            }
            else if ((string)parameter == "month")
            {
                // Date "7/27/2011 9:30:59 AM" returns "JUL"
                string s = date.ToString("m");
                return s.Substring(0, 3).ToUpper();
            }
            else if ((string)parameter == "year")
            {
                // Date "7/27/2011 9:30:59 AM" returns "2011"
                return date.Year.ToString();
            }
            else
            {
                // Date "7/27/2011 9:30:59 AM" returns "7/27/2011"
                return date.ToString("d");
            }


            //switch ((string)parameter)
            //{
            //    case "day":
            //        return date.Day.ToString();
            //        break;
            //    case "month":
            //        string s = date.ToString("m");
            //        return s.Substring(0, 3).ToUpper();
            //        break;
            //    case "year":
            //        return date.Year.ToString();
            //        break;
            //    default:
            //        return date.ToString("d");
            //        break;
            //}

        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            string strValue = value as string;
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, out resultDateTime))
            {
                return resultDateTime;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
