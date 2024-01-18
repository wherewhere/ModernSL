using System.Windows;

namespace SilverlightApplication.Controls.Primitives
{
    /// <summary>
    /// This class converts a boolean value into a Visibility enumeration.
    /// </summary>
    public class BoolToVisibilityConverter : BoolToObjectConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoolToVisibilityConverter"/> class.
        /// </summary>
        public BoolToVisibilityConverter()
        {
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }
    }
}
