using System;
using System.Windows.Data;

namespace IQMan.Helpers
{
    public class BoolToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return false;
            else
            {
                if (value.ToString() == "Y")
                {
                    return true;
                }
                else if (value.ToString() == "N")
                {
                    return false;
                }
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return "N";
            }
            else
            {
                if ((bool)value == true)
                {
                    return "Y";
                }
                else
                {
                    return "N";
                }
            }
        }
    }
}
