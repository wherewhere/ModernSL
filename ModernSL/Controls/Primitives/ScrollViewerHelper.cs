﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ModernSL.Controls.Primitives
{
    public static class ScrollViewerHelper
    {
        #region IsEnabled

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(ScrollViewerHelper),
                new PropertyMetadata(false, OnIsEnabledChanged));

        public static bool GetIsEnabled(ScrollViewer scrollViewer)
        {
            return (bool)scrollViewer.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(ScrollViewer scrollViewer, bool value)
        {
            scrollViewer.SetValue(IsEnabledProperty, value);
        }

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScrollViewer sv = (ScrollViewer)d;
            if ((bool)e.NewValue)
            {
                sv.Loaded += OnLoaded;
            }
            else
            {
                sv.Loaded -= OnLoaded;
            }
        }

        #endregion

        #region AutoHideScrollBars

        public static readonly DependencyProperty AutoHideScrollBarsProperty =
            DependencyProperty.RegisterAttached(
                "AutoHideScrollBars",
                typeof(bool),
                typeof(ScrollViewerHelper),
                new PropertyMetadata(false, OnAutoHideScrollBarsChanged));

        public static bool GetAutoHideScrollBars(DependencyObject element)
        {
            return (bool)element.GetValue(AutoHideScrollBarsProperty);
        }

        public static void SetAutoHideScrollBars(DependencyObject element, bool value)
        {
            element.SetValue(AutoHideScrollBarsProperty, value);
        }

        private static void OnAutoHideScrollBarsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollViewer sv)
            {
                UpdateVisualState(sv);
            }
        }

        #endregion

        private static void OnLoaded(object sender, RoutedEventArgs e)
        {
            ScrollViewer sv = (ScrollViewer)sender;
            sv.ApplyTemplate();
            UpdateVisualState(sv, false);
        }

        private static void UpdateVisualState(ScrollViewer sv, bool useTransitions = true)
        {
            string stateName = GetAutoHideScrollBars(sv) ? "NoIndicator" : "MouseIndicator";
            VisualStateManager.GoToState(sv, stateName, useTransitions);
        }
    }

    public class IndicatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && (bool)value ? ScrollingIndicatorMode.None : ScrollingIndicatorMode.MouseIndicator;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is ScrollingIndicatorMode && (ScrollingIndicatorMode)value != ScrollingIndicatorMode.MouseIndicator;
        }
    }
}
