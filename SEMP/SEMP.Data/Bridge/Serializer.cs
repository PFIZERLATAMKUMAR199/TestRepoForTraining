using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace SEMP.Data
{
    class Serializer
    {
        public static String Serialize<T>(IList<T> list)
        {
            var sw = new StringWriter();
            Serialize(list, new XmlTextWriter(sw));
            return sw.ToString();
        }
        public static void Serialize<T>(IList<T> list, XmlTextWriter xml)
        {
            var t = typeof(T);
            xml.WriteStartElement("List");
            xml.WriteString("\n");
            foreach (var o in list) Serialize(o, xml);
            xml.WriteFullEndElement();
            xml.WriteString("\n");
        }
        public static void Serialize(Object o, XmlTextWriter xml)
        {
            Object value;
            var t = o.GetType();
            var props = t.GetProperties();
            xml.WriteString("\t");
            xml.WriteStartElement(t.Name);
            foreach (var p in props)
            {
                if (p.CanRead && null != (value = p.GetValue(o, null))
                && (value is String || !(value is IEnumerable)))
                {
                    xml.WriteAttributeString(p.Name,
                        (p.PropertyType == typeof(Boolean) ||
                        p.PropertyType == typeof(Boolean?) ||
                        p.PropertyType == typeof(Nullable<Boolean>)
                        ? (value.Equals(true) ? 1 : 0)
                        : value).ToString()
                    );
                }
            }
            xml.WriteEndElement();
            xml.WriteString("\n");
        }
    }
}
