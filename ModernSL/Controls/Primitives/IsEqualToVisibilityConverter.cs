using System.Windows;

namespace ModernSL.Controls.Primitives
{
    public partial class IsEqualToVisibilityConverter : IsEqualToObjectConverter
    {
        public IsEqualToVisibilityConverter()
        {
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }
    }
}
