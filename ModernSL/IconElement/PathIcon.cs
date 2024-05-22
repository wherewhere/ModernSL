using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ModernSL.Controls
{
    /// <summary>
    /// Represents an icon that uses a vector path as its content.
    /// </summary>
    public class PathIcon : IconElement
    {
        static PathIcon()
        {
        }

        /// <summary>
        /// Initializes a new instance of the PathIcon class.
        /// </summary>
        public PathIcon()
        {
        }

        #region Data

        /// <summary>
        /// Identifies the Data dependency property.
        /// </summary>
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(
                nameof(Data),
                typeof(Geometry),
                typeof(PathIcon),
                new PropertyMetadata(OnDataChanged));

        /// <summary>
        /// Gets or sets a Geometry that specifies the shape to be drawn. In XAML. this can
        /// also be set using a string that describes Move and draw commands syntax.
        /// </summary>
        /// <value>A description of the shape to be drawn.</value>
        public Geometry Data
        {
            get => (Geometry)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PathIcon)d).ApplyData();
        }

        #endregion

        private protected override void InitializeChildren()
        {
            _path = new Path
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Stretch = Stretch.Uniform
            };

            ApplyForeground();
            ApplyData();

            Children.Add(_path);
        }

        private protected override void OnShouldInheritForegroundFromVisualParentChanged()
        {
            ApplyForeground();
        }

        private protected override void OnVisualParentForegroundPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            if (ShouldInheritForegroundFromVisualParent)
            {
                ApplyForeground();
            }
        }

        protected override void OnForegroundPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            ApplyForeground();
        }

        private void ApplyForeground()
        {
            if (_path != null)
            {
                _path.Fill = ShouldInheritForegroundFromVisualParent ? VisualParentForeground : Foreground ?? new SolidColorBrush(SystemColors.ControlTextColor);
            }
        }

        private void ApplyData()
        {
            if (_path != null)
            {
                _path.Data = Data;
            }
        }

        private Path _path;
    }
}
