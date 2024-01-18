using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace SilverlightApplication.Controls
{
    /// <summary>
    /// Represents the base class for an icon UI element.
    /// </summary>
    [TypeConverter(typeof(IconElementConverter))]
    public abstract class IconElement : Panel
    {
        private protected IconElement()
        {
        }

        #region Foreground

        /// <summary>
        /// Identifies the Foreground dependency property.
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register(
                nameof(Foreground),
                typeof(Brush),
                typeof(IconElement),
                new PropertyMetadata(OnForegroundPropertyChanged));

        private static void OnForegroundPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((IconElement)sender).OnForegroundPropertyChanged(args);
        }

        protected virtual void OnForegroundPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            _isForegroundDefaultOrInherited = Foreground == null;
            UpdateShouldInheritForegroundFromVisualParent();
        }

        /// <summary>
        /// Gets or sets a brush that describes the foreground color.
        /// </summary>
        /// <value>The brush that paints the foreground of the control.</value>
        [Bindable(true), Category("Appearance")]
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        #endregion

        #region VisualParentForeground

        private static readonly DependencyProperty VisualParentForegroundProperty =
            DependencyProperty.Register(
                nameof(VisualParentForeground),
                typeof(Brush),
                typeof(IconElement),
                new PropertyMetadata(null, OnVisualParentForegroundPropertyChanged));

        private protected Brush VisualParentForeground
        {
            get => (Brush)GetValue(VisualParentForegroundProperty);
            set => SetValue(VisualParentForegroundProperty, value);
        }

        private static void OnVisualParentForegroundPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((IconElement)sender).OnVisualParentForegroundPropertyChanged(args);
        }

        private protected virtual void OnVisualParentForegroundPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        #endregion

        private protected bool ShouldInheritForegroundFromVisualParent
        {
            get => _shouldInheritForegroundFromVisualParent;
            private set
            {
                if (_shouldInheritForegroundFromVisualParent != value)
                {
                    _shouldInheritForegroundFromVisualParent = value;

                    if (_shouldInheritForegroundFromVisualParent)
                    {
                        SetBinding(VisualParentForegroundProperty,
                            new Binding
                            {
                                Path = new PropertyPath(nameof(Foreground)),
                                Source = VisualParent
                            });
                    }
                    else
                    {
                        ClearValue(VisualParentForegroundProperty);
                    }

                    OnShouldInheritForegroundFromVisualParentChanged();
                }
            }
        }

        private protected virtual void OnShouldInheritForegroundFromVisualParentChanged()
        {
        }

        private void UpdateShouldInheritForegroundFromVisualParent()
        {
            ShouldInheritForegroundFromVisualParent =
                _isForegroundDefaultOrInherited &&
                Parent != null &&
                VisualParent != null &&
                Parent != VisualParent;
        }

        protected DependencyObject VisualParent => GetVisualParent();

        private protected abstract void InitializeChildren();

        protected DependencyObject GetVisualParent()
        {
            var parent = VisualTreeHelper.GetParent(this);

            while (parent != null)
            {
                if (parent.GetType().GetProperty(nameof(ForegroundProperty)) != null) { return parent; }
                parent = VisualTreeHelper.GetParent(this);
            }

            return null;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            EnsureLayoutRoot();

            Size stackDesiredSize = new Size();
            UIElementCollection children = Children;
            Size layoutSlotSize = availableSize;
            bool hasVisibleChild = false;

            for (int i = 0, count = children.Count; i < count; ++i)
            {
                UIElement child = children[i];

                if (child == null) { continue; }

                bool isVisible = child.Visibility != Visibility.Collapsed;

                if (isVisible && !hasVisibleChild)
                {
                    hasVisibleChild = true;
                }

                child.Measure(layoutSlotSize);
                Size childDesiredSize = child.DesiredSize;

                stackDesiredSize.Width = Math.Max(stackDesiredSize.Width, childDesiredSize.Width);
                stackDesiredSize.Height = Math.Max(stackDesiredSize.Height, childDesiredSize.Height);
            }

            return stackDesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Rect GetRect(Size size) => new Rect(0, 0, size.Width, size.Height);

            UIElementCollection children = Children;
            Rect rcChild = GetRect(finalSize);

            for (int i = 0, count = children.Count; i < count; ++i)
            {
                UIElement child = children[i];

                if (child == null) { continue; }

                rcChild.Width = Math.Max(finalSize.Width, child.DesiredSize.Width);
                rcChild.Height = Math.Max(finalSize.Height, child.DesiredSize.Height);

                child.Arrange(rcChild);
            }
            return finalSize;
        }

        private void EnsureLayoutRoot()
        {
            InitializeChildren();
        }

        private bool _isForegroundDefaultOrInherited = true;
        private bool _shouldInheritForegroundFromVisualParent;
    }
}
