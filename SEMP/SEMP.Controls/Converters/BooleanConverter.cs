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
    public class BooleanConverter : IValueConverter
    {
        public static BooleanConverter Current = new BooleanConverter();
        public Boolean Convert(Object value)
        {
            try { return System.Convert.ToBoolean(value); }
            catch { return value is ICollection ? ((ICollection)value).Count != 0 : value != null; }
        }
        public Boolean Convert(Object value, Object parameter)
        { // FRL - 2012-01-16
            //    return Convert(value).Equals(parameter == null || System.Convert.ToBoolean(parameter));
            bool comparedValue = true;
            if (parameter is Predicate<Object>) { return ((Predicate<Object>)parameter).Invoke(value); }
            if (parameter is IDictionary)
            {
                var dic = (IDictionary)parameter;
                if (((bool)dic["ZeroIsTrue"]) && value is int)
                    value = (value == null ? false : true);
                if (dic.Contains("ConversionEqualsTo"))
                    comparedValue = (bool)dic["ConversionEqualsTo"];
            }
            else if (parameter is bool || parameter is Boolean || parameter is String)
            {
                comparedValue = System.Convert.ToBoolean(parameter);
            }
            return Convert(value).Equals(comparedValue);
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
