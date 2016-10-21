using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XOCV.Converters
{
    public class CaptureNavigationBoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = (bool)value;
            if (temp == true)
            {
                return "Submit";
            }
            else
            {
                return "Next";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = (bool)value;
            if (temp)
            {
                return "Next";
            }
            else
            {
                return "Submit";
            }
        }
    }
}
