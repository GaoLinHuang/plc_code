using System.Windows;

namespace PipettingCode.Views.ExperimentError
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