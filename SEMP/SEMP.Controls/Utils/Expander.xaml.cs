using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace SEMP.Controls
{
    public partial class Expander : ContentControl
    {
        public Expander()
        {
            InitializeComponent();
        }
        private void tglButton_Click(Object Sender, RoutedEventArgs Args)
        {
            IsExpanded = ((ToggleButton)this.GetTemplateChild("tglButton")).IsChecked.Value;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ((ToggleButton)this.GetTemplateChild("tglButton"))
                .SetBinding(ToggleButton.ContentProperty, new Binding("Title") { Source = this });
            IsExpanded = IsExpanded;
        }
        #region TitleDependencyProperty
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(UIElement), typeof(Expander),
            new PropertyMetadata(new PropertyChangedCallback(TitlePropertyChanged)));
        private static void TitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue) ((Expander)d).Title = (UIElement)e.NewValue;
        }
        public UIElement Title
        {
            get { return (UIElement)GetValue(Expander.TitleProperty); }
            set { SetValue(Expander.TitleProperty, value); }
        }
        #endregion TitleDependencyProperty
        #region IsExpandedDependencyProperty
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(Boolean), typeof(Expander),
            new PropertyMetadata(true, new PropertyChangedCallback(IsExpandedPropertyChanged)));
        private static void IsExpandedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue) ((Expander)d).IsExpanded = (Boolean)e.NewValue;
        }
        public Boolean IsExpanded
        {
            get { return (Boolean)GetValue(Expander.IsExpandedProperty); }
            set
            {
                SetValue(Expander.IsExpandedProperty, value);
                if (null != this.GetTemplateChild("tglButton"))
                {
                    ((ToggleButton)this.GetTemplateChild("tglButton")).IsChecked = value;
                    ((UIElement)this.GetTemplateChild("ButtonContent")).Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }
        #endregion IsExpandedDependencyProperty
    }
}
