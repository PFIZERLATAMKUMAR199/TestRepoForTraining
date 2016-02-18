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

//luis - 2012-07-04
namespace SEMP.Controls
{
    public class MoneyStringConverter : IValueConverter
    {
        public static MoneyStringConverter Current = new MoneyStringConverter();
        public String Convert(Object value)
        {
            try { return "$ " + value.ToString(); }
            catch { return null; }
        }
        public String Convert(Object value, Object parameter)
        {
            return Convert(value);
        }
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return Convert(value);
        }
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
