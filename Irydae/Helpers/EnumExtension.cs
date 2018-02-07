using System;
using System.ComponentModel;
using System.Reflection;

namespace Irydae.Helpers
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[]) fi.GetCustomAttributes(
                    typeof (DescriptionAttribute),
                    false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return value.ToString();
        }

        public static string GetAmbientValue(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            AmbientValueAttribute[] attributes =
                (AmbientValueAttribute[])fi.GetCustomAttributes(
                    typeof(AmbientValueAttribute),
                    false);

            if (attributes != null && attributes.Length > 0)
            {
                return (string)attributes[0].Value;
            }
            return value.ToString();
        }
    }
}