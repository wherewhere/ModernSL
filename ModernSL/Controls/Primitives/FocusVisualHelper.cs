// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Windows;
using System.Windows.Media;

namespace ModernSL.Controls.Primitives
{
    public static class FocusVisualHelper
    {
        #region FocusVisualPrimaryBrush

        /// <summary>
        /// Gets the brush used to draw the outer border of a HighVisibility focus
        /// visual for a FrameworkElement.
        /// </summary>
        /// <param name="element">The element from which to read the property value.</param>
        /// <returns>The brush used to draw the outer border of a HighVisibility focus visual.</returns>
        public static Brush GetFocusVisualPrimaryBrush(FrameworkElement element)
        {
            return (Brush)element.GetValue(FocusVisualPrimaryBrushProperty);
        }

        /// <summary>
        /// Sets the brush used to draw the outer border of a HighVisibility focus
        /// visual for a FrameworkElement.
        /// </summary>
        /// <param name="element">The element on which to set the attached property.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetFocusVisualPrimaryBrush(FrameworkElement element, Brush value)
        {
            element.SetValue(FocusVisualPrimaryBrushProperty, value);
        }

        /// <summary>
        /// Identifies the FocusVisualPrimaryBrush dependency property.
        /// </summary>
        public static readonly DependencyProperty FocusVisualPrimaryBrushProperty =
            DependencyProperty.RegisterAttached(
                "FocusVisualPrimaryBrush",
                typeof(Brush),
                typeof(FocusVisualHelper),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(228, 0, 0, 0))));

        #endregion

        #region FocusVisualPrimaryThickness

        /// <summary>
        /// Gets the thickness of the outer border of a HighVisibility focus visual
        /// for a FrameworkElement.
        /// </summary>
        /// <param name="element">The element from which to read the property value.</param>
        /// <returns>
        /// The thickness of the outer border of a HighVisibility focus visual. The default
        /// value is 2.
        /// </returns>
        public static Thickness GetFocusVisualPrimaryThickness(FrameworkElement element)
        {
            return (Thickness)element.GetValue(FocusVisualPrimaryThicknessProperty);
        }

        /// <summary>
        /// Sets the thickness of the outer border of a HighVisibility focus visual
        /// for a FrameworkElement.
        /// </summary>
        /// <param name="element">The element on which to set the attached property.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetFocusVisualPrimaryThickness(FrameworkElement element, Thickness value)
        {
            element.SetValue(FocusVisualPrimaryThicknessProperty, value);
        }

        /// <summary>
        /// Identifies the FocusVisualPrimaryThickness dependency property.
        /// </summary>
        public static readonly DependencyProperty FocusVisualPrimaryThicknessProperty =
            DependencyProperty.RegisterAttached(
                "FocusVisualPrimaryThickness",
                typeof(Thickness),
                typeof(FocusVisualHelper),
                new PropertyMetadata(new Thickness(1)));

        #endregion

        #region FocusVisualMargin

        /// <summary>
        /// Gets the outer margin of the focus visual for a FrameworkElement.
        /// </summary>
        /// <param name="element">The element from which to read the property value.</param>
        /// <returns>
        /// Provides margin values for the focus visual. The default value is a default Thickness
        /// with all properties (dimensions) equal to 0.
        /// </returns>
        public static Thickness GetFocusVisualMargin(FrameworkElement element)
        {
            return (Thickness)element.GetValue(FocusVisualMarginProperty);
        }

        /// <summary>
        /// Sets the outer margin of the focus visual for a FrameworkElement.
        /// </summary>
        /// <param name="element">The element on which to set the attached property.</param>
        /// <param name="value">The property value to set.</param>
        public static void SetFocusVisualMargin(FrameworkElement element, Thickness value)
        {
            element.SetValue(FocusVisualMarginProperty, value);
        }

        /// <summary>
        /// Identifies the FocusVisualMargin dependency property.
        /// </summary>
        public static readonly DependencyProperty FocusVisualMarginProperty =
            DependencyProperty.RegisterAttached(
                "FocusVisualMargin",
                typeof(Thickness),
                typeof(FocusVisualHelper),
                new PropertyMetadata(new Thickness(0)));

        #endregion
    }
}
