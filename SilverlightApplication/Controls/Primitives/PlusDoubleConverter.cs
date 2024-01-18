using System;
using System.Globalization;
using System.Windows.Data;

namespace SilverlightApplication.Controls.Primitives
{
    /// <summary>
    /// Plus two <see cref="double"/> value to a <see cref="double"/> value.
    /// </summary>
    public class PlusDoubleConverter : IValueConverter
    {
        /// <summary>
        /// Plus two <see cref="double"/> value.
        /// </summary>
        /// <param name="value">The <see cref="bool"/> value to negate.</param>
        /// <param name="targetType">The type of the target property, as a type reference.</param>
        /// <param name="parameter">Optional parameter. Not used.</param>
        /// <param name="culture">The language of the conversion. Not used</param>
        /// <returns>The value to be passed to the target dependency property.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double left = System.Convert.ToDouble(value);
            double right = System.Convert.ToDouble(parameter);
            return left + right;
        }

        /// <summary>
        /// Subtract two <see cref="double"/> value.
        /// </summary>
        /// <param name="value">The <see cref="bool"/> value to negate.</param>
        /// <param name="targetType">The type of the target property, as a type reference.</param>
        /// <param name="parameter">Optional parameter. Not used.</param>
        /// <param name="culture">The language of the conversion. Not used</param>
        /// <returns>The value to be passed to the target dependency property.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double left = System.Convert.ToDouble(value);
            double right = System.Convert.ToDouble(parameter);
            return left - right;
        }
    }
}
