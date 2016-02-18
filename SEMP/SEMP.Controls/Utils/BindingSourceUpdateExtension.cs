using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SEMP.Controls
{
    public static class BindingSourceUpdateExtension
    {//luis - 2011-10-11
        public static void UpdateBindingSource(this FrameworkElement ctl)
        {
            var el = FocusManager.GetFocusedElement();
            if (el != null)
            {
                if (el is TextBox && !(el is System.Windows.Controls.Primitives.DatePickerTextBox))
                {
                    if(((TextBox)el).GetBindingExpression(TextBox.TextProperty) != null)
                        ((TextBox)el).GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
                if (el is PasswordBox) ((PasswordBox)el).GetBindingExpression(PasswordBox.PasswordProperty).UpdateSource();
                //if (el is ComboBox) ((ComboBox)el).GetBindingExpression(ComboBox.SelectedItemProperty).UpdateSource();
            }
        }
    }
}