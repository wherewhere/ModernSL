using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ModernSL.Controls.Primitives
{
    public static class RichTextBoxHelper
    {
        #region IsEnabled

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(RichTextBoxHelper),
                new PropertyMetadata(false, OnIsEnabledChanged));

        public static bool GetIsEnabled(RichTextBox richTextBox)
        {
            return (bool)richTextBox.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(RichTextBox richTextBox, bool value)
        {
            richTextBox.SetValue(IsEnabledProperty, value);
        }

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RichTextBox richTextBox = (RichTextBox)d;
            bool oldValue = (bool)e.OldValue;
            bool newValue = (bool)e.NewValue;
            if (newValue)
            {
                richTextBox.ContentChanged += OnTextChanged;
                UpdateIsEmpty(richTextBox);
            }
            else
            {
                richTextBox.ContentChanged -= OnTextChanged;
                richTextBox.ClearValue(IsEmptyProperty);
            }
        }

        #endregion

        #region IsEmpty

        private static readonly DependencyProperty IsEmptyProperty =
            DependencyProperty.RegisterAttached(
                "IsEmpty",
                typeof(bool),
                typeof(RichTextBoxHelper),
                new PropertyMetadata(false));

        public static bool GetIsEmpty(RichTextBox richTextBox)
        {
            return (bool)richTextBox.GetValue(IsEmptyProperty);
        }

        private static void SetIsEmpty(RichTextBox richTextBox, bool value)
        {
            richTextBox.SetValue(IsEmptyProperty, value);
        }

        private static void UpdateIsEmpty(RichTextBox rtb)
        {
            bool isEmpty;
            if (rtb.Blocks.Count == 0)
            {
                isEmpty = true;
            }
            else
            {
                TextPointer startPointer = rtb.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward);
                TextPointer endPointer = rtb.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward);
                isEmpty = startPointer.CompareTo(endPointer) == 0;
            }

            if (GetIsEmpty(rtb) != isEmpty)
            {
                SetIsEmpty(rtb, isEmpty);
            }

            SetPlaceholderTextVisibility(rtb, isEmpty ? Visibility.Visible : Visibility.Collapsed);
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
                typeof(RichTextBoxHelper),
                new PropertyMetadata(Visibility.Visible));

        #endregion

        private static void OnTextChanged(object sender, ContentChangedEventArgs e)
        {
            UpdateIsEmpty((RichTextBox)sender);
        }
    }
}
