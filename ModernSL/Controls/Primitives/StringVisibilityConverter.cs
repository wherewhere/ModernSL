using System.Windows;

namespace ModernSL.Controls.Primitives
{
    /// <summary>
    /// This class converts a string value into a Visibility value (if the value is null or empty returns a collapsed value).
    /// </summary>
    public class StringVisibilityConverter : EmptyStringToObjectConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringVisibilityConverter"/> class.
        /// </summary>
        public StringVisibilityConverter()
        {
            NotEmptyValue = Visibility.Visible;
            EmptyValue = Visibility.Collapsed;
        }
    }
}
