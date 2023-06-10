using System.Windows;

namespace Windows.Base
{
    public static class FrameworkElementExtends
    {
        /// <summary>
        /// 获取元素的ViewModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T ViewModel<T>(this FrameworkElement element) where T : class, new()
        {
            return element.DataContext as T;
        }

        /// <summary>
        /// 注册DataContext的ViewModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="viewModel"></param>
        public static void RegisterViewModel<T>(this FrameworkElement element, T viewModel) where T : class, new()
        {
            element.DataContext = viewModel;
        }
    }
}