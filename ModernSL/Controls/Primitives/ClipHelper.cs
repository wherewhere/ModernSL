using System.Windows;
using System.Windows.Media;

namespace ModernSL.Controls.Primitives
{
    public sealed class ClipHelper
    {
        #region ClipToBounds

        /// <summary>
        /// Gets a value indicating whether to clip the content of this element (or content coming from the child elements of this element) to fit into the size of the containing element. This is a dependency property.
        /// </summary>
        /// <param name="control">The element from which to read the property value.</param>
        /// <returns>
        /// <see langword="true"/> if the content should be clipped; otherwise, <see langword="false"/>. The default value is <see langword="false"/>.
        /// </returns>
        public static bool GetClipToBounds(FrameworkElement control)
        {
            return (bool)control.GetValue(ClipToBoundsProperty);
        }

        /// <summary>
        /// Sets a value indicating whether to clip the content of this element (or content coming from the child elements of this element) to fit into the size of the containing element. This is a dependency property.
        /// </summary>
        /// <param name="control">The element on which to set the attached property.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetClipToBounds(FrameworkElement control, bool value)
        {
            control.SetValue(ClipToBoundsProperty, value);
        }

        /// <summary>
        /// Identifies the ClipToBounds dependency property.
        /// </summary>
        public static readonly DependencyProperty ClipToBoundsProperty =
            DependencyProperty.RegisterAttached(
                "ClipToBounds",
                typeof(bool),
                typeof(ClipHelper),
                new PropertyMetadata(false, OnClipToBoundsChanged));

        private static void OnClipToBoundsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                ClipToBounds(element);

                // whenever the element which this property is attached to is loaded
                // or re-sizes, we need to update its clipping geometry
                element.Loaded += On_Loaded;
                element.SizeChanged += On_SizeChanged;
            }
        }

        private static void On_Loaded(object sender, RoutedEventArgs e)
        {
            ClipToBounds(sender as FrameworkElement);
        }

        private static void On_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ClipToBounds(sender as FrameworkElement);
        }

        #endregion

        /// <summary>
        /// Creates a rectangular clipping geometry which matches the geometry of the
        /// passed element
        /// </summary>
        private static void ClipToBounds(FrameworkElement element)
        {
            element.Clip = GetClipToBounds(element)
                ? new RectangleGeometry
                {
                    Rect = new Rect(0, 0, element.ActualWidth, element.ActualHeight)
                }
                : (Geometry)null;
        }

    }
}
