using System;
using System.Globalization;
using Xamarin.Forms;

namespace XOCV.Converters
{
    public class CommentButtonTextStringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = (bool)value;
            if (temp == true)
            {
                return "Hide Comment";
            }
            else
            {
                return "Add comment";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = (bool)value;
            if (temp)
            {
                return "Add comment";
            }
            else
            {
                return "Hide Comment";
            }
        }
    }
}