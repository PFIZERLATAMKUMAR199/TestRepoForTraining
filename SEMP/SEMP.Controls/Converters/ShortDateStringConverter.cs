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

// FRL - 2011-12-06
namespace SEMP.Controls
{
    public class ShortDateStringConverter : IValueConverter
    {
        public static ShortDateStringConverter Current = new ShortDateStringConverter();
        public String Convert(Object value)
        {
            try { return System.Convert.ToDateTime(value).ToShortDateString(); }
            catch { return null; }
        }
        public String Convert(Object value, Object parameter)
        {
            return Convert(value);
        }
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return Convert(value, parameter);
        }
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
