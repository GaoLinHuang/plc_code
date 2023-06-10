using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Windows.Base
{
    // Token: 0x02000060 RID: 96
    internal class SizeToClipRectConverter : IMultiValueConverter
    {
        // Token: 0x0600017A RID: 378 RVA: 0x00004E38 File Offset: 0x00003038
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double num = (double)values[0];
            double num2 = (double)values[1];
            return (num > 0.0 && num2 > 0.0) ? new Rect(0.0, 0.0, num, num2) : Rect.Empty;
        }

        // Token: 0x0600017B RID: 379 RVA: 0x00004E99 File Offset: 0x00003099
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}