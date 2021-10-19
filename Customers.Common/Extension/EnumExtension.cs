using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Customers.Common.Extension
{
    public static class EnumExtension
    {
        public static string GetEnumDescription(this Enum enumeration)
        {
            FieldInfo fi = enumeration.GetType().GetField(enumeration.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0 && !string.IsNullOrEmpty(attributes[0].Description))
            {
                return attributes[0].Description;
            }
            return enumeration.ToString();
        }
    }
}
