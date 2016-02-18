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
    public class DictionaryConverter : IValueConverter
    {
        public static DictionaryConverter Current = new DictionaryConverter();
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            try { return ((IDictionary)value)[parameter]; }
            catch { }
            return null;
        }
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            try { ((IDictionary)value)[parameter] = value; }
            catch { }
            return null;
        }
    }
}
