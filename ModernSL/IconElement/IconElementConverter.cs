using System;
using System.ComponentModel;
using System.Globalization;

namespace ModernSL.Controls
{
    public class IconElementConverter : TypeConverter
    {
        public IconElementConverter()
        {
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value is string s && Enum.TryParse(s, true, out Symbol symbol)
                ? (object)new SymbolIcon(symbol)
                : throw new InvalidOperationException(value.ToString());
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            throw new InvalidOperationException(value.ToString());
        }
    }
}
