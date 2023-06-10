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
using System.Windows.Shapes;

namespace PipetitngCode.Views.ExperimentError
{
    /// <summary>
    /// ExperimentError.xaml 的交互逻辑
    /// </summary>
    public partial class ExperimentError : Window
    {
        public ExperimentError()
        {
            InitializeComponent();
           DataContext = ExperimentErrorViewModel.GetInstance();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
           Close();
        }

        private void ReTry_Click(object sender, RoutedEventArgs e)
        {
           Close();
        }

        private void InitialReTry_Click(object sender, RoutedEventArgs e)
        {
           Close();
        }

        private void Igonre_Click(object sender, RoutedEventArgs e)
        {
           Close();
        }
    }
}
