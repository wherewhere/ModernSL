using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SilverlightApplication.Controls.Primitives
{
    public static class ControlHelper
    {
        #region CornerRadius

        /// <summary>
        /// Gets the radius for the corners of the control's border.
        /// </summary>
        /// <param name="control">The element from which to read the property value.</param>
        /// <returns>
        /// The degree to which the corners are rounded, expressed as values of the CornerRadius
        /// structure.
        /// </returns>
        public static CornerRadius GetCornerRadius(Control control)
        {
            return (CornerRadius)control.GetValue(CornerRadiusProperty);
        }

        /// <summary>
        /// Sets the radius for the corners of the control's border.
        /// </summary>
        /// <param name="control">The element on which to set the attached property.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetCornerRadius(Control control, CornerRadius value)
        {
            control.SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Identifies the CornerRadius dependency property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadius",
                typeof(CornerRadius),
                typeof(ControlHelper),
                null);

        #endregion

        #region Header

        /// <summary>
        /// Identifies the Header dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.RegisterAttached(
                "Header",
                typeof(object),
                typeof(ControlHelper),
                new PropertyMetadata(OnHeaderChanged));

        /// <summary>
        /// Gets the content for the control's header.
        /// </summary>
        /// <param name="control">The element from which to read the property value.</param>
        /// <returns>The content of the control's header. The default is **null**.</returns>
        public static object GetHeader(Control control)
        {
            return control.GetValue(HeaderProperty);
        }

        /// <summary>
        /// Sets the content for the control's header.
        /// </summary>
        /// <param name="control">The element on which to set the attached property.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetHeader(Control control, object value)
        {
            control.SetValue(HeaderProperty, value);
        }

        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateHeaderVisibility((Control)d);
        }

        #endregion

        #region HeaderTemplate

        /// <summary>
        /// Identifies the HeaderTemplate dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.RegisterAttached(
                "HeaderTemplate",
                typeof(DataTemplate),
                typeof(ControlHelper),
                new PropertyMetadata(OnHeaderTemplateChanged));

        /// <summary>
        /// Gets the DataTemplate used to display the content of the control's header.
        /// </summary>
        /// <param name="control">The element from which to read the property value.</param>
        /// <returns>
        /// The template that specifies the visualization of the header object. The default
        /// is **null**.
        /// </returns>
        public static DataTemplate GetHeaderTemplate(Control control)
        {
            return (DataTemplate)control.GetValue(HeaderTemplateProperty);
        }

        /// <summary>
        /// Sets the DataTemplate used to display the content of the control's header.
        /// </summary>
        /// <param name="control">The element on which to set the attached property.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetHeaderTemplate(Control control, DataTemplate value)
        {
            control.SetValue(HeaderTemplateProperty, value);
        }

        private static void OnHeaderTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateHeaderVisibility((Control)d);
        }

        #endregion

        #region HeaderVisibility

        public static readonly DependencyProperty HeaderVisibilityProperty =
            DependencyProperty.RegisterAttached(
                "HeaderVisibility",
                typeof(Visibility),
                typeof(ControlHelper),
                new PropertyMetadata(Visibility.Collapsed));

        public static Visibility GetHeaderVisibility(Control control)
        {
            return (Visibility)control.GetValue(HeaderVisibilityProperty);
        }

        private static void SetHeaderVisibility(Control control, Visibility value)
        {
            control.SetValue(HeaderVisibilityProperty, value);
        }

        private static void UpdateHeaderVisibility(Control control)
        {
            Visibility visibility = GetHeaderTemplate(control) != null
                ? Visibility.Visible
                : IsNullOrEmptyString(GetHeader(control)) ? Visibility.Collapsed : Visibility.Visible;
            SetHeaderVisibility(control, visibility);
        }

        #endregion

        #region PlaceholderText

        /// <summary>
        /// Gets the text that is displayed in the control until the value is changed
        /// by a user action or some other operation.
        /// </summary>
        /// <param name="control">The element from which to read the property value.</param>
        /// <returns>
        /// The text that is displayed in the control when no value is entered. The default
        /// is an empty string ("").
        /// </returns>
        public static string GetPlaceholderText(Control control)
        {
            return (string)control.GetValue(PlaceholderTextProperty);
        }

        /// <summary>
        /// Sets the text that is displayed in the control until the value is changed
        /// by a user action or some other operation.
        /// </summary>
        /// <param name="control">The element on which to set the attached property.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetPlaceholderText(Control control, string value)
        {
            control.SetValue(PlaceholderTextProperty, value);
        }

        /// <summary>
        /// Identifies the PlaceholderText dependency property.
        /// </summary>
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.RegisterAttached(
                "PlaceholderText",
                typeof(string),
                typeof(ControlHelper),
                new PropertyMetadata(string.Empty, OnPlaceholderTextChanged));

        private static void OnPlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdatePlaceholderTextVisibility((Control)d);
        }

        #endregion

        #region PlaceholderTextVisibility

        public static Visibility GetPlaceholderTextVisibility(Control control)
        {
            return (Visibility)control.GetValue(PlaceholderTextVisibilityProperty);
        }

        private static void SetPlaceholderTextVisibility(Control control, Visibility value)
        {
            control.SetValue(PlaceholderTextVisibilityProperty, value);
        }

        private static readonly DependencyProperty PlaceholderTextVisibilityProperty =
            DependencyProperty.RegisterAttached(
                "PlaceholderTextVisibility",
                typeof(Visibility),
                typeof(ControlHelper),
                new PropertyMetadata(Visibility.Collapsed));

        private static void UpdatePlaceholderTextVisibility(Control control)
        {
            SetPlaceholderTextVisibility(control, string.IsNullOrEmpty(GetPlaceholderText(control)) ? Visibility.Collapsed : Visibility.Visible);
        }

        #endregion

        #region PlaceholderForeground

        /// <summary>
        /// Gets a brush that describes the color of placeholder text.
        /// </summary>
        /// <param name="control">The element from which to read the property value.</param>
        /// <returns>The brush that describes the color of placeholder text.</returns>
        public static Brush GetPlaceholderForeground(Control control)
        {
            return (Brush)control.GetValue(PlaceholderForegroundProperty);
        }

        /// <summary>
        /// Sets a brush that describes the color of placeholder text.
        /// </summary>
        /// <param name="control">The element on which to set the attached property.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetPlaceholderForeground(Control control, Brush value)
        {
            control.SetValue(PlaceholderForegroundProperty, value);
        }

        /// <summary>
        /// Identifies the PlaceholderForeground dependency property.
        /// </summary>
        public static readonly DependencyProperty PlaceholderForegroundProperty =
            DependencyProperty.RegisterAttached(
                "PlaceholderForeground",
                typeof(Brush),
                typeof(ControlHelper),
                null);

        #endregion

        #region Description

        /// <summary>
        /// Gets content that is shown below the control. The content should provide
        /// guidance about the input expected by the control.
        /// </summary>
        /// <param name="control">The element from which to read the property value.</param>
        /// <returns>The content to be displayed below the control. The default is **null**.</returns>
        public static object GetDescription(Control control)
        {
            return control.GetValue(DescriptionProperty);
        }

        /// <summary>
        /// Sets content that is shown below the control. The content should provide
        /// guidance about the input expected by the control.
        /// </summary>
        /// <param name="control">The element on which to set the attached property.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetDescription(Control control, object value)
        {
            control.SetValue(DescriptionProperty, value);
        }

        /// <summary>
        /// Identifies the Description dependency property.
        /// </summary>
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.RegisterAttached(
                "Description",
                typeof(object),
                typeof(ControlHelper),
                new PropertyMetadata(OnDescriptionChanged));

        private static void OnDescriptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateDescriptionVisibility((Control)d);
        }

        #endregion

        #region DescriptionVisibility

        private static readonly DependencyProperty DescriptionVisibilityProperty =
            DependencyProperty.RegisterAttached(
                "DescriptionVisibility",
                typeof(Visibility),
                typeof(ControlHelper),
                new PropertyMetadata(Visibility.Collapsed));

        public static Visibility GetDescriptionVisibility(Control control)
        {
            return (Visibility)control.GetValue(DescriptionVisibilityProperty);
        }

        private static void SetDescriptionVisibility(Control control, Visibility value)
        {
            control.SetValue(DescriptionVisibilityProperty, value);
        }

        private static void UpdateDescriptionVisibility(Control control)
        {
            SetDescriptionVisibility(control, IsNullOrEmptyString(GetDescription(control)) ? Visibility.Collapsed : Visibility.Visible);
        }

        #endregion

        #region VisualState

        /// <summary>
        /// Identifies the VisualState dependency property.
        /// </summary>
        public static readonly DependencyProperty VisualStateProperty =
            DependencyProperty.RegisterAttached(
                "VisualState",
                typeof(string),
                typeof(ControlHelper),
                new PropertyMetadata(OnVisualStateChanged));

        /// <summary>
        /// Gets the visual state for the control.
        /// </summary>
        /// <param name="control">The element on which to set the attached property.</param>
        /// <returns>The visual state for the control.</returns>
        public static string GetVisualState(Control control)
        {
            return (string)control.GetValue(VisualStateProperty);
        }

        /// <summary>
        /// Sets the visual state for the control.
        /// </summary>
        /// <param name="control">The element on which to set the attached property.</param>
        /// <param name="value">The visual state for the control.</param>
        public static void SetVisualState(Control control, string value)
        {
            control.SetValue(VisualStateProperty, value);
        }

        private static void OnVisualStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateVisualState((Control)d);
        }

        private static void UpdateVisualState(Control control)
        {
            string State = GetVisualState(control);
            if (!string.IsNullOrEmpty(State))
            {
                VisualStateManager.GoToState(control, State, true);
                control.Loaded += (sender, e) => VisualStateManager.GoToState(control, State, false);
            }
        }

        #endregion

        internal static bool IsNullOrEmptyString(object obj)
        {
            return obj == null || (obj is string s && string.IsNullOrEmpty(s));
        }
    }
}
