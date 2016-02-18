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
    public static class Clipboard
    {
        private class ClipboardTextBox : TextBox
        {
            public ClipboardTextBox()
            {
                AcceptsReturn = true;
            }
            protected override void OnKeyDown(KeyEventArgs e)
            {
                base.OnKeyDown(e);
            }
            public void ProcessKeyDown(KeyEventArgs e)
            {
                OnKeyDown(e);
            }
        }
        private static ClipboardTextBox _helper = new ClipboardTextBox();
        public static void Copy(KeyEventArgs e, String Text)
        {
            _helper.Text = Text;
            _helper.SelectAll();
            _helper.ProcessKeyDown(e);
        }
        public static String Paste(KeyEventArgs e)
        {
            _helper.Text = String.Empty;
            _helper.ProcessKeyDown(e);
            return _helper.Text;
        }
    }
}