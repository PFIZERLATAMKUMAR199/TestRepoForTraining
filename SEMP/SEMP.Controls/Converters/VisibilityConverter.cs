using System;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections;
using System.Globalization;

namespace SEMP.Controls
{
    public class VisibilityConverter : IValueConverter
    {
        public static VisibilityConverter Current = new VisibilityConverter();
        public Visibility Convert(Object value, Object parameter)
        {
            return true.Equals(BooleanConverter.Current.Convert(value, parameter))
                ? Visibility.Visible : Visibility.Collapsed;
        }
        public Visibility Convert(Object value)
        {
            return Convert(value, null);
        }
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return Convert(value, parameter);
        }
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return Visibility.Visible.Equals(value)
                .Equals(parameter == null || System.Convert.ToBoolean(parameter));
        }
    }
}
