﻿using Common.Log;
using System.Windows;
using Windows.Base;

namespace PipettingCode
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var mainWindowViewModel = this.ViewModel<MainWindowViewModel>();
            mainWindowViewModel.Init();
            ILogger log = new Log4netAdapter(nameof(PipettingCode));
            log.Info("test");
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ILogger log = new Log4netAdapter(nameof(PipettingCode));
            log.Info("test");
        }
    }
}