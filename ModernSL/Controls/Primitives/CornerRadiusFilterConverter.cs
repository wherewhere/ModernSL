﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ModernSL.Controls.Primitives
{
    public class CornerRadiusFilterConverter : DependencyObject, IValueConverter
    {
        public CornerRadiusFilterKind Filter { get; set; }

        public double Scale { get; set; } = 1.0;

        public CornerRadius Convert(CornerRadius radius, CornerRadiusFilterKind filterKind)
        {
            CornerRadius result = radius;

            switch (filterKind)
            {
                case CornerRadiusFilterKind.Top:
                    result.BottomLeft = 0;
                    result.BottomRight = 0;
                    break;
                case CornerRadiusFilterKind.Right:
                    result.TopLeft = 0;
                    result.BottomLeft = 0;
                    break;
                case CornerRadiusFilterKind.Bottom:
                    result.TopLeft = 0;
                    result.TopRight = 0;
                    break;
                case CornerRadiusFilterKind.Left:
                    result.TopRight = 0;
                    result.BottomRight = 0;
                    break;
            }

            return result;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CornerRadius cornerRadius = (CornerRadius)value;

            double scale = Scale;
            if (!double.IsNaN(scale))
            {
                cornerRadius.TopLeft *= scale;
                cornerRadius.TopRight *= scale;
                cornerRadius.BottomRight *= scale;
                cornerRadius.BottomLeft *= scale;
            }

            CornerRadiusFilterKind filterType = Filter;
            return filterType is CornerRadiusFilterKind.TopLeftValue or
                CornerRadiusFilterKind.BottomRightValue
                ? GetDoubleValue(cornerRadius, filterType)
                : Convert(cornerRadius, filterType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private double GetDoubleValue(CornerRadius radius, CornerRadiusFilterKind filterKind)
        {
            return filterKind switch
            {
                CornerRadiusFilterKind.TopLeftValue => radius.TopLeft,
                CornerRadiusFilterKind.BottomRightValue => radius.BottomRight,
                _ => 0,
            };
        }
    }

    public enum CornerRadiusFilterKind
    {
        None,
        Top,
        Right,
        Bottom,
        Left,
        TopLeftValue,
        BottomRightValue
    }
}
