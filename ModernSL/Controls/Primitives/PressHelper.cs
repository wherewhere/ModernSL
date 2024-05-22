using System.Windows;
using System.Windows.Input;

namespace ModernSL.Controls.Primitives
{
    public static class PressHelper
    {
        #region IsEnabled

        public static bool GetIsEnabled(UIElement element)
        {
            return (bool)element.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(UIElement element, bool value)
        {
            element.SetValue(IsEnabledProperty, value);
        }

        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
            "IsEnabled",
            typeof(bool),
            typeof(PressHelper),
            new PropertyMetadata(OnIsEnabledChanged));

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = (UIElement)d;
            if ((bool)e.NewValue)
            {
                element.MouseLeftButtonDown += OnMouseLeftButtonDown;
                element.MouseLeftButtonUp += OnMouseLeftButtonUp;
                element.MouseEnter += OnMouseEnter;
                element.MouseLeave += OnMouseLeave;
            }
            else
            {
                element.MouseLeftButtonDown -= OnMouseLeftButtonDown;
                element.MouseLeftButtonUp -= OnMouseLeftButtonUp;
                element.MouseEnter -= OnMouseEnter;
                element.MouseLeave -= OnMouseLeave;
                element.ClearValue(IsPressedProperty);
            }
        }

        #endregion

        #region IsPressed

        public static bool GetIsPressed(UIElement element)
        {
            return (bool)element.GetValue(IsPressedProperty);
        }

        private static void SetIsPressed(UIElement element, bool value)
        {
            element.SetValue(IsPressedProperty, value);
        }

        private static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.RegisterAttached(
                "IsPressed",
                typeof(bool),
                typeof(PressHelper),
                null);

        #endregion

        #region IsMouseOver

        public static bool GetIsMouseOver(UIElement element)
        {
            return (bool)element.GetValue(IsMouseOverProperty);
        }

        private static void SetIsMouseOver(UIElement element, bool value)
        {
            element.SetValue(IsMouseOverProperty, value);
        }

        private static readonly DependencyProperty IsMouseOverProperty =
            DependencyProperty.RegisterAttached(
                "IsMouseOver",
                typeof(bool),
                typeof(PressHelper),
                null);

        #endregion

        private static void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UpdateIsPressed((UIElement)sender, true, e);
        }

        private static void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateIsPressed((UIElement)sender, false, e);
        }

        private static void OnMouseEnter(object sender, MouseEventArgs e)
        {
            UpdateIsMouseOver((UIElement)sender, e);
        }

        private static void OnMouseLeave(object sender, MouseEventArgs e)
        {
            UpdateIsMouseOver((UIElement)sender, e);
        }

        private static void UpdateIsPressed(UIElement element, bool isdown, MouseButtonEventArgs e)
        {
            Rect itemBounds = new(new Point(), element.RenderSize);

            if (itemBounds.Contains(e.GetPosition(element)))
            {
                SetIsPressed(element, isdown);
            }
            else
            {
                element.ClearValue(IsPressedProperty);
            }
        }

        private static void UpdateIsMouseOver(UIElement element, MouseEventArgs e)
        {
            Rect itemBounds = new(new Point(), element.RenderSize);

            if (itemBounds.Contains(e.GetPosition(element)))
            {
                SetIsMouseOver(element, true);
            }
            else
            {
                element.ClearValue(IsMouseOverProperty);
            }
        }
    }
}
