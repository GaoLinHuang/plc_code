using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PipettingCode.Common;

namespace PipettingCode.Views
{
    /// <summary>
    /// ProcessExecuteView.xaml 的交互逻辑
    /// </summary>
    public partial class ProcessExecuteView : UserControl
    {
        public ProcessExecuteView()
        {
            InitializeComponent();
            Loaded += ProcessExecuteView_Loaded;
        }

        private void ProcessExecuteView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= ProcessExecuteView_Loaded;
        }
    }
    public class RichTextBoxHelper : DependencyObject
    {
        public static string GetRichText(DependencyObject obj)
        {
            return (string)obj.GetValue(RichTextProperty);
        }

        public static void SetRichText(DependencyObject obj, string value)
        {
            obj.SetValue(RichTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RichTextProperty =
            DependencyProperty.RegisterAttached("RichText", typeof(string), typeof(RichTextBoxHelper), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
                PropertyChangedCallback = (obj, e) =>
                {
                    var richTextBox = (RichTextBox)obj;
                    var text = GetRichText(richTextBox);
                    richTextBox.AppendText(text);
                    richTextBox.AppendText(Environment.NewLine);
                    richTextBox.ScrollToEnd();
                }

            });
    }

}
