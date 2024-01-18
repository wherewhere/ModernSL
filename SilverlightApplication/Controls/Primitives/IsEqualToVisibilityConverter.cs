using System.Windows;

namespace SilverlightApplication.Controls.Primitives
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
