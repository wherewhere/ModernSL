﻿using System.Windows;
using System.Windows.Controls;

namespace ModernSL.Controls.Primitives
{
    public static class TextBoxHelper
    {
        private const string ButtonStatesGroup = "ButtonStates";
        private const string ButtonVisibleState = "ButtonVisible";
        private const string ButtonCollapsedState = "ButtonCollapsed";

        #region IsEnabled

        public static bool GetIsEnabled(TextBox textBox)
        {
            return (bool)textBox.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(TextBox textBox, bool value)
        {
            textBox.SetValue(IsEnabledProperty, value);
        }

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(TextBoxHelper),
                new PropertyMetadata(OnIsEnabledChanged));

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = (TextBox)d;

            if ((bool)e.NewValue)
            {
                textBox.Loaded += OnLoaded;
                textBox.TextChanged += OnTextChanged;
                UpdateHasText(textBox);

            }
            else
            {
                textBox.Loaded -= OnLoaded;
                textBox.TextChanged -= OnTextChanged;
                textBox.ClearValue(HasTextProperty);
            }
        }

        #endregion

        #region HasText

        private static readonly DependencyProperty HasTextProperty =
            DependencyProperty.RegisterAttached(
                "HasText",
                typeof(bool),
                typeof(TextBoxHelper),
                null);

        public static bool GetHasText(TextBox textBox)
        {
            return (bool)textBox.GetValue(HasTextProperty);
        }

        private static void SetHasText(TextBox textBox, bool value)
        {
            textBox.SetValue(HasTextProperty, value);
        }

        private static void UpdateHasText(TextBox textBox)
        {
            SetHasText(textBox, !string.IsNullOrEmpty(textBox.Text));
        }

        #endregion

        #region IsDeleteButton

        public static bool GetIsDeleteButton(Button button)
        {
            return (bool)button.GetValue(IsDeleteButtonProperty);
        }

        public static void SetIsDeleteButton(Button button, bool value)
        {
            button.SetValue(IsDeleteButtonProperty, value);
        }

        public static readonly DependencyProperty IsDeleteButtonProperty =
            DependencyProperty.RegisterAttached(
                "IsDeleteButton",
                typeof(bool),
                typeof(TextBoxHelper),
                new PropertyMetadata(OnIsDeleteButtonChanged));

        private static void OnIsDeleteButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Button button = (Button)d;

            if ((bool)e.OldValue)
            {
                button.Click -= OnDeleteButtonClick;
            }

            if ((bool)e.NewValue)
            {
                button.Click += OnDeleteButtonClick;
            }
        }

        #endregion

        #region IsDeleteButtonVisible

        public static readonly DependencyProperty IsDeleteButtonVisibleProperty =
            DependencyProperty.RegisterAttached(
                "IsDeleteButtonVisible",
                typeof(bool),
                typeof(TextBoxHelper),
                new PropertyMetadata(OnIsDeleteButtonVisibleChanged));

        public static bool GetIsDeleteButtonVisible(TextBox textBox)
        {
            return (bool)textBox.GetValue(IsDeleteButtonVisibleProperty);
        }

        public static void SetIsDeleteButtonVisible(TextBox textBox, bool value)
        {
            textBox.SetValue(IsDeleteButtonVisibleProperty, value);
        }

        private static void OnIsDeleteButtonVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateVisualStates((TextBox)d, (bool)e.NewValue);
        }

        #endregion

        private static void OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            UpdateVisualStates(textBox, GetIsDeleteButtonVisible(textBox));
        }

        private static void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            UpdateHasText(textBox);
        }

        private static void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            TextBox textBox = button.FindAscendant<TextBox>();
            textBox?.SetValue(TextBox.TextProperty, null);
        }

        private static void UpdateVisualStates(TextBox textBox, bool isDeleteButtonVisible)
        {
            VisualStateManager.GoToState(textBox, isDeleteButtonVisible ? ButtonVisibleState : ButtonCollapsedState, true);
        }
    }
}
