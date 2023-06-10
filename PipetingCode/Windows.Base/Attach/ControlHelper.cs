using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Windows.Base
{
    /// <summary>
    /// 通用控件的附加属性类
    /// <para>注：控件的一般附加属性，基本都在ControlHelper里了。如果是特殊的需求，可以额外添加附加属性类</para>
    /// </summary>
    // Token: 0x0200005A RID: 90
    public class ControlHelper
    {
        /// <summary>
        /// 设置普通前景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x060000E3 RID: 227 RVA: 0x00003280 File Offset: 0x00001480
        public static void SetForegroundNormal(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.ForegroundNormalProperty, value);
        }

        /// <summary>
        /// 获取普通前景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x060000E4 RID: 228 RVA: 0x00003290 File Offset: 0x00001490
        public static Brush GetForegroundNormal(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.ForegroundNormalProperty);
        }

        /// <summary>
        /// 设置鼠标、手势悬停前景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x060000E5 RID: 229 RVA: 0x000032B2 File Offset: 0x000014B2
        public static void SetForegroundHover(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.ForegroundHoverProperty, value);
        }

        /// <summary>
        /// 获取鼠标、手势悬停前景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x060000E6 RID: 230 RVA: 0x000032C4 File Offset: 0x000014C4
        public static Brush GetForegroundHover(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.ForegroundHoverProperty);
        }

        /// <summary>
        /// 设置鼠标、手势按压前景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x060000E7 RID: 231 RVA: 0x000032E6 File Offset: 0x000014E6
        public static void SetForegroundPressed(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.ForegroundPressedProperty, value);
        }

        /// <summary>
        /// 获取鼠标、手势按压前景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x060000E8 RID: 232 RVA: 0x000032F8 File Offset: 0x000014F8
        public static Brush GetForegroundPressed(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.ForegroundPressedProperty);
        }

        /// <summary>
        /// 设置选中状态前景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x060000E9 RID: 233 RVA: 0x0000331A File Offset: 0x0000151A
        public static void SetForegroundChecked(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.ForegroundCheckedProperty, value);
        }

        /// <summary>
        /// 获取选中状态前景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x060000EA RID: 234 RVA: 0x0000332C File Offset: 0x0000152C
        public static Brush GetForegroundChecked(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.ForegroundCheckedProperty);
        }

        // Token: 0x060000EB RID: 235 RVA: 0x0000334E File Offset: 0x0000154E
        public static void SetForegroundDisabled(DependencyObject element, SolidColorBrush value)
        {
            element.SetValue(ControlHelper.ForegroundDisabledProperty, value);
        }

        // Token: 0x060000EC RID: 236 RVA: 0x00003360 File Offset: 0x00001560
        public static SolidColorBrush GetForegroundDisabled(DependencyObject element)
        {
            return (SolidColorBrush)element.GetValue(ControlHelper.ForegroundDisabledProperty);
        }

        /// <summary>
        /// 设置普通填充色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x060000ED RID: 237 RVA: 0x00003382 File Offset: 0x00001582
        public static void SetFillNormal(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.FillNormalProperty, value);
        }

        /// <summary>
        /// 获取普通填充色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x060000EE RID: 238 RVA: 0x00003394 File Offset: 0x00001594
        public static Brush GetFillNormal(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.FillNormalProperty);
        }

        /// <summary>
        /// 设置鼠标、手势悬停填充色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x060000EF RID: 239 RVA: 0x000033B6 File Offset: 0x000015B6
        public static void SetFillHover(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.FillHoverProperty, value);
        }

        /// <summary>
        /// 获取鼠标、手势悬停填充色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x060000F0 RID: 240 RVA: 0x000033C8 File Offset: 0x000015C8
        public static Brush GetFillHover(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.FillHoverProperty);
        }

        /// <summary>
        /// 设置鼠标、手势按压填充色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x060000F1 RID: 241 RVA: 0x000033EA File Offset: 0x000015EA
        public static void SetFillPressed(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.FillPressedProperty, value);
        }

        /// <summary>
        /// 获取鼠标、手势按压填充色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x060000F2 RID: 242 RVA: 0x000033FC File Offset: 0x000015FC
        public static Brush GetFillPressed(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.FillPressedProperty);
        }

        /// <summary>
        /// 设置选中状态填充色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x060000F3 RID: 243 RVA: 0x0000341E File Offset: 0x0000161E
        public static void SetFillChecked(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.FillCheckedProperty, value);
        }

        /// <summary>
        /// 获取选中状态填充色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x060000F4 RID: 244 RVA: 0x00003430 File Offset: 0x00001630
        public static Brush GetFillChecked(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.FillCheckedProperty);
        }

        /// <summary>
        /// 设置未选中状态填充色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x060000F5 RID: 245 RVA: 0x00003452 File Offset: 0x00001652
        public static void SetFillUnchecked(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.FillUncheckedProperty, value);
        }

        /// <summary>
        /// 获取未选中状态填充色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x060000F6 RID: 246 RVA: 0x00003464 File Offset: 0x00001664
        public static Brush GetFillUnchecked(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.FillUncheckedProperty);
        }

        /// <summary>
        ///  设置普通背景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x060000F7 RID: 247 RVA: 0x00003486 File Offset: 0x00001686
        public static void SetBackgroundNormal(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.BackgroundNormalProperty, value);
        }

        /// <summary>
        ///  获取普通背景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x060000F8 RID: 248 RVA: 0x00003498 File Offset: 0x00001698
        public static Brush GetBackgroundNormal(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.BackgroundNormalProperty);
        }

        /// <summary>
        /// 设置鼠标、手势悬停背景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x060000F9 RID: 249 RVA: 0x000034BA File Offset: 0x000016BA
        public static void SetBackgroundHover(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.BackgroundHoverProperty, value);
        }

        /// <summary>
        /// 获取鼠标、手势悬停背景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x060000FA RID: 250 RVA: 0x000034CC File Offset: 0x000016CC
        public static Brush GetBackgroundHover(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.BackgroundHoverProperty);
        }

        /// <summary>
        /// 设置鼠标、手势按压背景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x060000FB RID: 251 RVA: 0x000034EE File Offset: 0x000016EE
        public static void SetBackgroundPressed(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.BackgroundPressedProperty, value);
        }

        /// <summary>
        /// 获取鼠标、手势按压背景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x060000FC RID: 252 RVA: 0x00003500 File Offset: 0x00001700
        public static Brush GetBackgroundPressed(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.BackgroundPressedProperty);
        }

        /// <summary>
        /// 设置选中状态背景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x060000FD RID: 253 RVA: 0x00003522 File Offset: 0x00001722
        public static void SetBackgroundChecked(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.BackgroundCheckedProperty, value);
        }

        /// <summary>
        /// 获取选中状态背景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x060000FE RID: 254 RVA: 0x00003534 File Offset: 0x00001734
        public static Brush GetBackgroundChecked(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.BackgroundCheckedProperty);
        }

        /// <summary>
        ///  设置不可用背景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x060000FF RID: 255 RVA: 0x00003556 File Offset: 0x00001756
        public static void SetBackgroundDisabled(DependencyObject element, SolidColorBrush value)
        {
            element.SetValue(ControlHelper.BackgroundDisabledProperty, value);
        }

        /// <summary>
        ///  获取不可用背景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x06000100 RID: 256 RVA: 0x00003568 File Offset: 0x00001768
        public static SolidColorBrush GetBackgroundDisabled(DependencyObject element)
        {
            return (SolidColorBrush)element.GetValue(ControlHelper.BackgroundDisabledProperty);
        }

        /// <summary>
        /// 设置普通边框颜色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x06000101 RID: 257 RVA: 0x0000358A File Offset: 0x0000178A
        public static void SetBorderBrushNormal(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.BorderBrushNormalProperty, value);
        }

        /// <summary>
        /// 获取普通边框颜色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x06000102 RID: 258 RVA: 0x0000359C File Offset: 0x0000179C
        public static Brush GetBorderBrushNormal(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.BorderBrushNormalProperty);
        }

        /// <summary>
        /// 设置鼠标、手势悬停边框颜色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x06000103 RID: 259 RVA: 0x000035BE File Offset: 0x000017BE
        public static void SetBorderBrushHover(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.BorderBrushHoverProperty, value);
        }

        /// <summary>
        /// 获取鼠标、手势悬停边框颜色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x06000104 RID: 260 RVA: 0x000035D0 File Offset: 0x000017D0
        public static Brush GetBorderBrushHover(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.BorderBrushHoverProperty);
        }

        /// <summary>
        /// 设置鼠标、手势按压边框颜色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x06000105 RID: 261 RVA: 0x000035F2 File Offset: 0x000017F2
        public static void SetBorderBrushPressed(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.BorderBrushPressedProperty, value);
        }

        /// <summary>
        /// 获取鼠标、手势按压边框颜色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x06000106 RID: 262 RVA: 0x00003604 File Offset: 0x00001804
        public static Brush GetBorderBrushPressed(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.BorderBrushPressedProperty);
        }

        /// <summary>
        /// 设置选中状态下边框颜色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x06000107 RID: 263 RVA: 0x00003626 File Offset: 0x00001826
        public static void SetBorderBrushChecked(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.BorderBrushCheckedProperty, value);
        }

        /// <summary>
        /// 获取选中状态下边框颜色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x06000108 RID: 264 RVA: 0x00003638 File Offset: 0x00001838
        public static Brush GetBorderBrushChecked(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.BorderBrushCheckedProperty);
        }

        /// <summary>
        ///  设置普通透明度蒙层的填充色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x06000109 RID: 265 RVA: 0x0000365A File Offset: 0x0000185A
        public static void SetCoverFillNormal(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.CoverFillNormalProperty, value);
        }

        /// <summary>
        ///  获取普通透明度蒙层的填充色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x0600010A RID: 266 RVA: 0x0000366C File Offset: 0x0000186C
        public static Brush GetCoverFillNormal(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.CoverFillNormalProperty);
        }

        /// <summary>
        /// 设置鼠标、手势悬停透明度蒙层的填充色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x0600010B RID: 267 RVA: 0x0000368E File Offset: 0x0000188E
        public static void SetCoverFillHover(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.CoverFillHoverProperty, value);
        }

        /// <summary>
        /// 获取鼠标、手势悬停透明度蒙层的填充色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x0600010C RID: 268 RVA: 0x000036A0 File Offset: 0x000018A0
        public static Brush GetCoverFillHover(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.CoverFillHoverProperty);
        }

        /// <summary>
        /// 设置鼠标、手势按压透明度蒙层的填充色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x0600010D RID: 269 RVA: 0x000036C2 File Offset: 0x000018C2
        public static void SetCoverFillPressed(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.CoverFillPressedProperty, value);
        }

        /// <summary>
        /// 获取鼠标、手势按压透明度蒙层的填充色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x0600010E RID: 270 RVA: 0x000036D4 File Offset: 0x000018D4
        public static Brush GetCoverFillPressed(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.CoverFillPressedProperty);
        }

        /// <summary>
        /// 设置选中状态下透明度蒙层的填充色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x0600010F RID: 271 RVA: 0x000036F6 File Offset: 0x000018F6
        public static void SetCoverFillChecked(DependencyObject element, Brush value)
        {
            element.SetValue(ControlHelper.CoverFillCheckedProperty, value);
        }

        /// <summary>
        /// 获取选中状态下透明度蒙层的填充色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x06000110 RID: 272 RVA: 0x00003708 File Offset: 0x00001908
        public static Brush GetCoverFillChecked(DependencyObject element)
        {
            return (Brush)element.GetValue(ControlHelper.CoverFillCheckedProperty);
        }

        // Token: 0x06000111 RID: 273 RVA: 0x0000372C File Offset: 0x0000192C
        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(ControlHelper.CornerRadiusProperty);
        }

        // Token: 0x06000112 RID: 274 RVA: 0x0000374E File Offset: 0x0000194E
        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(ControlHelper.CornerRadiusProperty, value);
        }

        // Token: 0x06000113 RID: 275 RVA: 0x00003763 File Offset: 0x00001963
        public static void SetClipCornerRadius(UIElement element, double value)
        {
            element.SetValue(ControlHelper.ClipCornerRadiusProperty, value);
        }

        // Token: 0x06000114 RID: 276 RVA: 0x00003778 File Offset: 0x00001978
        public static double GetClipCornerRadius(UIElement element)
        {
            return (double)element.GetValue(ControlHelper.ClipCornerRadiusProperty);
        }

        // Token: 0x06000115 RID: 277 RVA: 0x0000379C File Offset: 0x0000199C
        private static void ClipCornerRadiusPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement uielement = (UIElement)d;
            double num = (double)e.NewValue;
            RectangleGeometry rectangleGeometry = uielement.Clip as RectangleGeometry;
            bool flag = uielement.Clip != null && rectangleGeometry == null;
            if (flag)
            {
                throw new InvalidOperationException(string.Concat(new string[]
                {
                    typeof(ControlHelper).FullName,
                    ".",
                    ControlHelper.ClipCornerRadiusProperty.Name,
                    " 属性需要使用到 ",
                    uielement.GetType().FullName,
                    ".",
                    UIElement.ClipProperty.Name,
                    " 属性，请不要在设置此属性的同时设置它。"
                }));
            }
            rectangleGeometry = (rectangleGeometry ?? new RectangleGeometry());
            rectangleGeometry.RadiusX = num;
            rectangleGeometry.RadiusY = num;
            uielement.Clip = rectangleGeometry;
            MultiBinding multiBinding = BindingOperations.GetMultiBinding(rectangleGeometry, RectangleGeometry.RectProperty);

            if (multiBinding == null)
            {
                multiBinding = new MultiBinding
                {
                    Converter = new SizeToClipRectConverter()
                };
                multiBinding.Bindings.Add(new Binding(FrameworkElement.ActualWidthProperty.Name)
                {
                    Source = uielement,
                    Mode = BindingMode.OneWay
                });
                multiBinding.Bindings.Add(new Binding(FrameworkElement.ActualHeightProperty.Name)
                {
                    Source = uielement,
                    Mode = BindingMode.OneWay
                });
                BindingOperations.SetBinding(rectangleGeometry, RectangleGeometry.RectProperty, multiBinding);
            }
        }

        /// <summary>
        /// 设置-是否启用
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        // Token: 0x06000116 RID: 278 RVA: 0x000038FB File Offset: 0x00001AFB
        public static void SetIsEnabled(DependencyObject element, bool value)
        {
            element.SetValue(ControlHelper.IsEnabledProperty, value);
        }

        /// <summary>
        /// 获取-是否启用
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        // Token: 0x06000117 RID: 279 RVA: 0x00003910 File Offset: 0x00001B10
        public static bool GetIsEnabled(DependencyObject element)
        {
            return (bool)element.GetValue(ControlHelper.IsEnabledProperty);
        }

        // Token: 0x06000118 RID: 280 RVA: 0x00003932 File Offset: 0x00001B32
        public static void SetVisibility(DependencyObject element, Visibility value)
        {
            element.SetValue(ControlHelper.VisibilityProperty, value);
        }

        // Token: 0x06000119 RID: 281 RVA: 0x00003948 File Offset: 0x00001B48
        public static Visibility GetVisibility(DependencyObject element)
        {
            return ((Visibility?)element.GetValue(ControlHelper.VisibilityProperty)).GetValueOrDefault();
        }

        // Token: 0x0600011A RID: 282 RVA: 0x00003972 File Offset: 0x00001B72
        public static void SetSelected(DependencyObject element, bool value)
        {
            element.SetValue(ControlHelper.SelectedProperty, value);
        }

        // Token: 0x0600011B RID: 283 RVA: 0x00003988 File Offset: 0x00001B88
        public static bool GetSelected(DependencyObject element)
        {
            return (bool)element.GetValue(ControlHelper.SelectedProperty);
        }

        // Token: 0x0600011C RID: 284 RVA: 0x000039AA File Offset: 0x00001BAA
        public static void SetContent(DependencyObject element, object value)
        {
            element.SetValue(ControlHelper.ContentProperty, value);
        }

        // Token: 0x0600011D RID: 285 RVA: 0x000039BC File Offset: 0x00001BBC
        public static object GetContent(DependencyObject element)
        {
            return element.GetValue(ControlHelper.ContentProperty);
        }

        // Token: 0x0600011E RID: 286 RVA: 0x000039D9 File Offset: 0x00001BD9
        public static void SetContent2(DependencyObject element, object value)
        {
            element.SetValue(ControlHelper.Content2Property, value);
        }

        // Token: 0x0600011F RID: 287 RVA: 0x000039EC File Offset: 0x00001BEC
        public static object GetContent2(DependencyObject element)
        {
            return element.GetValue(ControlHelper.Content2Property);
        }

        // Token: 0x06000120 RID: 288 RVA: 0x00003A09 File Offset: 0x00001C09
        public static void SetDisabledOpacity(DependencyObject element, double value)
        {
            element.SetValue(ControlHelper.DisabledOpacityProperty, value);
        }

        // Token: 0x06000121 RID: 289 RVA: 0x00003A20 File Offset: 0x00001C20
        public static double GetDisabledOpacity(DependencyObject element)
        {
            return (double)element.GetValue(ControlHelper.DisabledOpacityProperty);
        }

        // Token: 0x06000122 RID: 290 RVA: 0x00003A42 File Offset: 0x00001C42
        public static void SetImageMargin(DependencyObject element, Thickness value)
        {
            element.SetValue(ControlHelper.ImageMarginProperty, value);
        }

        // Token: 0x06000123 RID: 291 RVA: 0x00003A58 File Offset: 0x00001C58
        public static Thickness GetImageMargin(DependencyObject element)
        {
            return (Thickness)element.GetValue(ControlHelper.ImageMarginProperty);
        }

        // Token: 0x06000124 RID: 292 RVA: 0x00003A7A File Offset: 0x00001C7A
        public static void SetTextMargin(DependencyObject element, Thickness value)
        {
            element.SetValue(ControlHelper.TextMarginProperty, value);
        }

        // Token: 0x06000125 RID: 293 RVA: 0x00003A90 File Offset: 0x00001C90
        public static Thickness GetTextMargin(DependencyObject element)
        {
            return (Thickness)element.GetValue(ControlHelper.TextMarginProperty);
        }

        // Token: 0x06000126 RID: 294 RVA: 0x00003AB2 File Offset: 0x00001CB2
        public static void SetIsShowHandWhenMouseOverText(DependencyObject element, bool value)
        {
            element.SetValue(ControlHelper.IsShowHandWhenMouseOverTextProperty, value);
        }

        // Token: 0x06000127 RID: 295 RVA: 0x00003AC8 File Offset: 0x00001CC8
        public static bool GetIsShowHandWhenMouseOverText(DependencyObject element)
        {
            return (bool)element.GetValue(ControlHelper.IsShowHandWhenMouseOverTextProperty);
        }

        // Token: 0x06000128 RID: 296 RVA: 0x00003AEA File Offset: 0x00001CEA
        public static void SetTextDecorations(DependencyObject element, TextDecorationCollection value)
        {
            element.SetValue(ControlHelper.TextDecorationsProperty, value);
        }

        // Token: 0x06000129 RID: 297 RVA: 0x00003AFC File Offset: 0x00001CFC
        public static TextDecorationCollection GetTextDecorations(DependencyObject element)
        {
            return (TextDecorationCollection)element.GetValue(ControlHelper.TextDecorationsProperty);
        }

        // Token: 0x0600012A RID: 298 RVA: 0x00003B1E File Offset: 0x00001D1E
        public static void SetTextTrimming(DependencyObject element, TextTrimming value)
        {
            element.SetValue(ControlHelper.TextTrimmingProperty, value);
        }

        // Token: 0x0600012B RID: 299 RVA: 0x00003B34 File Offset: 0x00001D34
        public static TextTrimming GetTextTrimming(DependencyObject element)
        {
            return (TextTrimming)element.GetValue(ControlHelper.TextTrimmingProperty);
        }

        // Token: 0x0600012C RID: 300 RVA: 0x00003B56 File Offset: 0x00001D56
        public static void SetContentOrientation(DependencyObject element, Orientation value)
        {
            element.SetValue(ControlHelper.ContentOrientationProperty, value);
        }

        // Token: 0x0600012D RID: 301 RVA: 0x00003B6C File Offset: 0x00001D6C
        public static Orientation GetContentOrientation(DependencyObject element)
        {
            return (Orientation)element.GetValue(ControlHelper.ContentOrientationProperty);
        }

        // Token: 0x0600012E RID: 302 RVA: 0x00003B8E File Offset: 0x00001D8E
        public static void SetWatermarkText(DependencyObject element, string value)
        {
            element.SetValue(ControlHelper.WatermarkTextProperty, value);
        }

        // Token: 0x0600012F RID: 303 RVA: 0x00003BA0 File Offset: 0x00001DA0
        public static string GetWatermarkText(DependencyObject element)
        {
            return (string)element.GetValue(ControlHelper.WatermarkTextProperty);
        }

        // Token: 0x06000130 RID: 304 RVA: 0x00003BC2 File Offset: 0x00001DC2
        public static void SetContentPadding(DependencyObject element, Thickness value)
        {
            element.SetValue(ControlHelper.ContentPaddingProperty, value);
        }

        // Token: 0x06000131 RID: 305 RVA: 0x00003BD8 File Offset: 0x00001DD8
        public static Thickness GetContentPadding(DependencyObject element)
        {
            return (Thickness)element.GetValue(ControlHelper.ContentPaddingProperty);
        }

        // Token: 0x06000132 RID: 306 RVA: 0x00003BFA File Offset: 0x00001DFA
        public static void SetMarinNormal(DependencyObject element, Thickness value)
        {
            element.SetValue(ControlHelper.MarinNormalProperty, value);
        }

        // Token: 0x06000133 RID: 307 RVA: 0x00003C10 File Offset: 0x00001E10
        public static Thickness GetMarinNormal(DependencyObject element)
        {
            return (Thickness)element.GetValue(ControlHelper.MarinNormalProperty);
        }

        // Token: 0x06000134 RID: 308 RVA: 0x00003C32 File Offset: 0x00001E32
        public static void SetMarginHover(DependencyObject element, Thickness value)
        {
            element.SetValue(ControlHelper.MarginHoverProperty, value);
        }

        // Token: 0x06000135 RID: 309 RVA: 0x00003C48 File Offset: 0x00001E48
        public static Thickness GetMarginHover(DependencyObject element)
        {
            return (Thickness)element.GetValue(ControlHelper.MarginHoverProperty);
        }

        // Token: 0x06000136 RID: 310 RVA: 0x00003C6A File Offset: 0x00001E6A
        public static void SetMarginPressed(DependencyObject element, Thickness value)
        {
            element.SetValue(ControlHelper.MarginPressedProperty, value);
        }

        // Token: 0x06000137 RID: 311 RVA: 0x00003C80 File Offset: 0x00001E80
        public static Thickness GetMarginPressed(DependencyObject element)
        {
            return (Thickness)element.GetValue(ControlHelper.MarginPressedProperty);
        }

        // Token: 0x06000138 RID: 312 RVA: 0x00003CA2 File Offset: 0x00001EA2
        public static void SetMarginChecked(DependencyObject element, Thickness value)
        {
            element.SetValue(ControlHelper.MarginCheckedProperty, value);
        }

        // Token: 0x06000139 RID: 313 RVA: 0x00003CB8 File Offset: 0x00001EB8
        public static Thickness GetMarginChecked(DependencyObject element)
        {
            return (Thickness)element.GetValue(ControlHelper.MarginCheckedProperty);
        }

        // Token: 0x0600013A RID: 314 RVA: 0x00003CDA File Offset: 0x00001EDA
        public static void SetEffectBlurRadius(DependencyObject element, double value)
        {
            element.SetValue(ControlHelper.EffectBlurRadiusProperty, value);
        }

        // Token: 0x0600013B RID: 315 RVA: 0x00003CF0 File Offset: 0x00001EF0
        public static double GetEffectBlurRadius(DependencyObject element)
        {
            return (double)element.GetValue(ControlHelper.EffectBlurRadiusProperty);
        }

        // Token: 0x0600013C RID: 316 RVA: 0x00003D12 File Offset: 0x00001F12
        public static void SetEffectShadowDepth(DependencyObject element, double value)
        {
            element.SetValue(ControlHelper.EffectShadowDepthProperty, value);
        }

        // Token: 0x0600013D RID: 317 RVA: 0x00003D28 File Offset: 0x00001F28
        public static double GetEffectShadowDepth(DependencyObject element)
        {
            return (double)element.GetValue(ControlHelper.EffectShadowDepthProperty);
        }

        // Token: 0x0600013E RID: 318 RVA: 0x00003D4A File Offset: 0x00001F4A
        public static void SetEffectColor(DependencyObject element, Color value)
        {
            element.SetValue(ControlHelper.EffectColorProperty, value);
        }

        // Token: 0x0600013F RID: 319 RVA: 0x00003D60 File Offset: 0x00001F60
        public static Color GetEffectColor(DependencyObject element)
        {
            return (Color)element.GetValue(ControlHelper.EffectColorProperty);
        }

        // Token: 0x06000140 RID: 320 RVA: 0x00003D82 File Offset: 0x00001F82
        public static void SetEffectOpacity(DependencyObject element, double value)
        {
            element.SetValue(ControlHelper.EffectOpacityProperty, value);
        }

        // Token: 0x06000141 RID: 321 RVA: 0x00003D98 File Offset: 0x00001F98
        public static double GetEffectOpacity(DependencyObject element)
        {
            return (double)element.GetValue(ControlHelper.EffectOpacityProperty);
        }

        // Token: 0x06000142 RID: 322 RVA: 0x00003DBA File Offset: 0x00001FBA
        public static void SetImage(DependencyObject element, ImageSource value)
        {
            element.SetValue(ControlHelper.ImageProperty, value);
        }

        // Token: 0x06000143 RID: 323 RVA: 0x00003DCC File Offset: 0x00001FCC
        public static ImageSource GetImage(DependencyObject element)
        {
            return (ImageSource)element.GetValue(ControlHelper.ImageProperty);
        }

        // Token: 0x06000144 RID: 324 RVA: 0x00003DEE File Offset: 0x00001FEE
        public static void SetImageHover(DependencyObject element, ImageSource value)
        {
            element.SetValue(ControlHelper.ImageHoverProperty, value);
        }

        // Token: 0x06000145 RID: 325 RVA: 0x00003E00 File Offset: 0x00002000
        public static ImageSource GetImageHover(DependencyObject element)
        {
            return (ImageSource)element.GetValue(ControlHelper.ImageHoverProperty);
        }

        // Token: 0x06000146 RID: 326 RVA: 0x00003E22 File Offset: 0x00002022
        public static void SetImagePressed(DependencyObject element, ImageSource value)
        {
            element.SetValue(ControlHelper.ImagePressedProperty, value);
        }

        // Token: 0x06000147 RID: 327 RVA: 0x00003E34 File Offset: 0x00002034
        public static ImageSource GetImagePressed(DependencyObject element)
        {
            return (ImageSource)element.GetValue(ControlHelper.ImagePressedProperty);
        }

        // Token: 0x06000148 RID: 328 RVA: 0x00003E56 File Offset: 0x00002056
        public static void SetImageDisabled(DependencyObject element, ImageSource value)
        {
            element.SetValue(ControlHelper.ImageDisabledProperty, value);
        }

        // Token: 0x06000149 RID: 329 RVA: 0x00003E68 File Offset: 0x00002068
        public static ImageSource GetImageDisabled(DependencyObject element)
        {
            return (ImageSource)element.GetValue(ControlHelper.ImageDisabledProperty);
        }

        // Token: 0x0600014A RID: 330 RVA: 0x00003E8A File Offset: 0x0000208A
        public static void SetImageSize(DependencyObject element, double value)
        {
            element.SetValue(ControlHelper.ImageSizeProperty, value);
        }

        // Token: 0x0600014B RID: 331 RVA: 0x00003EA0 File Offset: 0x000020A0
        public static double GetImageSize(DependencyObject element)
        {
            return (double)element.GetValue(ControlHelper.ImageSizeProperty);
        }

        // Token: 0x0600014C RID: 332 RVA: 0x00003EC2 File Offset: 0x000020C2
        public static void SetImageHeight(DependencyObject element, double value)
        {
            element.SetValue(ControlHelper.ImageHeightProperty, value);
        }

        // Token: 0x0600014D RID: 333 RVA: 0x00003ED8 File Offset: 0x000020D8
        public static double GetImageHeight(DependencyObject element)
        {
            return (double)element.GetValue(ControlHelper.ImageHeightProperty);
        }

        // Token: 0x0600014E RID: 334 RVA: 0x00003EFA File Offset: 0x000020FA
        public static void SetImageWidth(DependencyObject element, double value)
        {
            element.SetValue(ControlHelper.ImageWidthProperty, value);
        }

        // Token: 0x0600014F RID: 335 RVA: 0x00003F10 File Offset: 0x00002110
        public static double GetImageWidth(DependencyObject element)
        {
            return (double)element.GetValue(ControlHelper.ImageWidthProperty);
        }

        // Token: 0x06000150 RID: 336 RVA: 0x00003F32 File Offset: 0x00002132
        public static void SetGeometry(DependencyObject element, Geometry value)
        {
            element.SetValue(ControlHelper.GeometryProperty, value);
        }

        // Token: 0x06000151 RID: 337 RVA: 0x00003F44 File Offset: 0x00002144
        public static Geometry GetGeometry(DependencyObject element)
        {
            return (Geometry)element.GetValue(ControlHelper.GeometryProperty);
        }

        // Token: 0x06000152 RID: 338 RVA: 0x00003F66 File Offset: 0x00002166
        public static void SetGeometry2(DependencyObject element, Geometry value)
        {
            element.SetValue(ControlHelper.Geometry2Property, value);
        }

        // Token: 0x06000153 RID: 339 RVA: 0x00003F78 File Offset: 0x00002178
        public static Geometry GetGeometry2(DependencyObject element)
        {
            return (Geometry)element.GetValue(ControlHelper.Geometry2Property);
        }

        // Token: 0x06000154 RID: 340 RVA: 0x00003F9A File Offset: 0x0000219A
        public static void SetSignImageHorizontalAlignment(DependencyObject element, HorizontalAlignment value)
        {
            element.SetValue(ControlHelper.SignImageHorizontalAlignmentProperty, value);
        }

        // Token: 0x06000155 RID: 341 RVA: 0x00003FB0 File Offset: 0x000021B0
        public static HorizontalAlignment GetSignImageHorizontalAlignment(DependencyObject element)
        {
            return (HorizontalAlignment)element.GetValue(ControlHelper.SignImageHorizontalAlignmentProperty);
        }

        // Token: 0x06000156 RID: 342 RVA: 0x00003FD2 File Offset: 0x000021D2
        public static void SetSignImageVerticalAlignment(DependencyObject element, VerticalAlignment value)
        {
            element.SetValue(ControlHelper.SignImageVerticalAlignmentProperty, value);
        }

        // Token: 0x06000157 RID: 343 RVA: 0x00003FE8 File Offset: 0x000021E8
        public static VerticalAlignment GetSignImageVerticalAlignment(DependencyObject element)
        {
            return (VerticalAlignment)element.GetValue(ControlHelper.SignImageVerticalAlignmentProperty);
        }

        // Token: 0x06000158 RID: 344 RVA: 0x0000400A File Offset: 0x0000220A
        public static void SetSignImageMargin(DependencyObject element, Thickness value)
        {
            element.SetValue(ControlHelper.SignImageMarginProperty, value);
        }

        // Token: 0x06000159 RID: 345 RVA: 0x00004020 File Offset: 0x00002220
        public static Thickness GetSignImageMargin(DependencyObject element)
        {
            return (Thickness)element.GetValue(ControlHelper.SignImageMarginProperty);
        }

        // Token: 0x0600015A RID: 346 RVA: 0x00004042 File Offset: 0x00002242
        public static void SetSignImageSize(DependencyObject element, double value)
        {
            element.SetValue(ControlHelper.SignImageSizeProperty, value);
        }

        // Token: 0x0600015B RID: 347 RVA: 0x00004058 File Offset: 0x00002258
        public static double GetSignImageSize(DependencyObject element)
        {
            return (double)element.GetValue(ControlHelper.SignImageSizeProperty);
        }

        // Token: 0x0600015C RID: 348 RVA: 0x0000407A File Offset: 0x0000227A
        public static void SetImageStretch(DependencyObject element, Stretch value)
        {
            element.SetValue(ControlHelper.ImageStretchProperty, value);
        }

        // Token: 0x0600015D RID: 349 RVA: 0x00004090 File Offset: 0x00002290
        public static Stretch GetImageStretch(DependencyObject element)
        {
            return (Stretch)element.GetValue(ControlHelper.ImageStretchProperty);
        }

        /// <summary>
        /// 普通前景色
        /// </summary>
        // Token: 0x04000063 RID: 99
        public static readonly DependencyProperty ForegroundNormalProperty = DependencyProperty.RegisterAttached("ForegroundNormal", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// 鼠标、手势悬停前景色
        /// </summary>
        // Token: 0x04000064 RID: 100
        public static readonly DependencyProperty ForegroundHoverProperty = DependencyProperty.RegisterAttached("ForegroundHover", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// 鼠标、手势按压前景色
        /// </summary>
        // Token: 0x04000065 RID: 101
        public static readonly DependencyProperty ForegroundPressedProperty = DependencyProperty.RegisterAttached("ForegroundPressed", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// 选中状态前景色
        /// </summary>
        // Token: 0x04000066 RID: 102
        public static readonly DependencyProperty ForegroundCheckedProperty = DependencyProperty.RegisterAttached("ForegroundChecked", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// 不可用
        /// </summary>
        // Token: 0x04000067 RID: 103
        public static readonly DependencyProperty ForegroundDisabledProperty = DependencyProperty.RegisterAttached("ForegroundDisabled", typeof(SolidColorBrush), typeof(ControlHelper), new PropertyMetadata(null));

        /// <summary>
        /// 普通填充色
        /// </summary>
        // Token: 0x04000068 RID: 104
        public static readonly DependencyProperty FillNormalProperty = DependencyProperty.RegisterAttached("FillNormal", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(null));

        /// <summary>
        /// 鼠标、手势悬停填充色
        /// </summary>
        // Token: 0x04000069 RID: 105
        public static readonly DependencyProperty FillHoverProperty = DependencyProperty.RegisterAttached("FillHover", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(null));

        /// <summary>
        /// 鼠标、手势按压填充色
        /// </summary>
        // Token: 0x0400006A RID: 106
        public static readonly DependencyProperty FillPressedProperty = DependencyProperty.RegisterAttached("FillPressed", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(null));

        /// <summary>
        /// 选中状态填充色
        /// </summary>
        // Token: 0x0400006B RID: 107
        public static readonly DependencyProperty FillCheckedProperty = DependencyProperty.RegisterAttached("FillChecked", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(null));

        /// <summary>
        /// 未选中状态填充色
        /// </summary>
        // Token: 0x0400006C RID: 108
        public static readonly DependencyProperty FillUncheckedProperty = DependencyProperty.RegisterAttached("FillUnchecked", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(null));

        /// <summary>
        /// 普通背景色
        /// </summary>
        // Token: 0x0400006D RID: 109
        public static readonly DependencyProperty BackgroundNormalProperty = DependencyProperty.RegisterAttached("BackgroundNormal", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// 鼠标、手势悬停背景色
        /// </summary>
        // Token: 0x0400006E RID: 110
        public static readonly DependencyProperty BackgroundHoverProperty = DependencyProperty.RegisterAttached("BackgroundHover", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// 鼠标、手势按压背景色
        /// </summary>
        // Token: 0x0400006F RID: 111
        public static readonly DependencyProperty BackgroundPressedProperty = DependencyProperty.RegisterAttached("BackgroundPressed", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// 选中状态背景色
        /// </summary>
        // Token: 0x04000070 RID: 112
        public static readonly DependencyProperty BackgroundCheckedProperty = DependencyProperty.RegisterAttached("BackgroundChecked", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// 不可用背景色
        /// </summary>
        // Token: 0x04000071 RID: 113
        public static readonly DependencyProperty BackgroundDisabledProperty = DependencyProperty.RegisterAttached("BackgroundDisabled", typeof(SolidColorBrush), typeof(ControlHelper), new PropertyMetadata(null));

        /// <summary>
        /// 普通边框颜色
        /// </summary>
        // Token: 0x04000072 RID: 114
        public static readonly DependencyProperty BorderBrushNormalProperty = DependencyProperty.RegisterAttached("BorderBrushNormal", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// 鼠标、手势悬停边框颜色
        /// </summary>
        // Token: 0x04000073 RID: 115
        public static readonly DependencyProperty BorderBrushHoverProperty = DependencyProperty.RegisterAttached("BorderBrushHover", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// 鼠标、手势按压边框颜色
        /// </summary>
        // Token: 0x04000074 RID: 116
        public static readonly DependencyProperty BorderBrushPressedProperty = DependencyProperty.RegisterAttached("BorderBrushPressed", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// 选中状态下边框颜色
        /// </summary>
        // Token: 0x04000075 RID: 117
        public static readonly DependencyProperty BorderBrushCheckedProperty = DependencyProperty.RegisterAttached("BorderBrushChecked", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// 普通透明度蒙层的填充色
        /// </summary>
        // Token: 0x04000076 RID: 118
        public static readonly DependencyProperty CoverFillNormalProperty = DependencyProperty.RegisterAttached("CoverFillNormal", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        ///  鼠标、手势悬停透明度蒙层的填充色
        /// </summary>
        // Token: 0x04000077 RID: 119
        public static readonly DependencyProperty CoverFillHoverProperty = DependencyProperty.RegisterAttached("CoverFillHover", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// 鼠标、手势按压透明度蒙层的填充色
        /// </summary>
        // Token: 0x04000078 RID: 120
        public static readonly DependencyProperty CoverFillPressedProperty = DependencyProperty.RegisterAttached("CoverFillPressed", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// 选中状态下透明度蒙层的填充色
        /// </summary>
        // Token: 0x04000079 RID: 121
        public static readonly DependencyProperty CoverFillCheckedProperty = DependencyProperty.RegisterAttached("CoverFillChecked", typeof(Brush), typeof(ControlHelper), new PropertyMetadata(Brushes.Transparent));

        // Token: 0x0400007A RID: 122
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ControlHelper), new PropertyMetadata(default(CornerRadius)));

        // Token: 0x0400007B RID: 123
        public static readonly DependencyProperty ClipCornerRadiusProperty = DependencyProperty.RegisterAttached("ClipCornerRadius", typeof(double), typeof(ControlHelper), new PropertyMetadata(0.0, new PropertyChangedCallback(ControlHelper.ClipCornerRadiusPropertyChangedCallback)));

        /// <summary>
        /// 是否启用
        /// <para>用于设置控件是否可使用的状态、是否可操作的状态</para>
        /// </summary>
        // Token: 0x0400007C RID: 124
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(ControlHelper), new PropertyMetadata(true));

        // Token: 0x0400007D RID: 125
        public static readonly DependencyProperty VisibilityProperty = DependencyProperty.RegisterAttached("Visibility", typeof(Visibility), typeof(ControlHelper), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// 是否被选中附加属性
        /// </summary>
        // Token: 0x0400007E RID: 126
        public static readonly DependencyProperty SelectedProperty = DependencyProperty.RegisterAttached("Selected", typeof(bool), typeof(ControlHelper), new PropertyMetadata(false));

        /// <summary>
        /// 为没有 Content 属性的控件标识附加属性。
        /// </summary>
        // Token: 0x0400007F RID: 127
        public static readonly DependencyProperty ContentProperty = DependencyProperty.RegisterAttached("Content", typeof(object), typeof(ControlHelper), new PropertyMetadata(null));

        /// <summary>
        /// Content2 属性的控件标识附加属性。
        /// </summary>
        // Token: 0x04000080 RID: 128
        public static readonly DependencyProperty Content2Property = DependencyProperty.RegisterAttached("Content2", typeof(object), typeof(ControlHelper), new PropertyMetadata(null));

        // Token: 0x04000081 RID: 129
        public static readonly DependencyProperty DisabledOpacityProperty = DependencyProperty.RegisterAttached("DisabledOpacity", typeof(double), typeof(ControlHelper), new PropertyMetadata(0.4));

        // Token: 0x04000082 RID: 130
        public static readonly DependencyProperty ImageMarginProperty = DependencyProperty.RegisterAttached("ImageMargin", typeof(Thickness), typeof(ControlHelper), new PropertyMetadata(default(Thickness)));

        // Token: 0x04000083 RID: 131
        public static readonly DependencyProperty TextMarginProperty = DependencyProperty.RegisterAttached("TextMargin", typeof(Thickness), typeof(ControlHelper), new PropertyMetadata(default(Thickness)));

        // Token: 0x04000084 RID: 132
        public static readonly DependencyProperty IsShowHandWhenMouseOverTextProperty = DependencyProperty.RegisterAttached("IsShowHandWhenMouseOverText", typeof(bool), typeof(ControlHelper), new PropertyMetadata(false));

        // Token: 0x04000085 RID: 133
        public static readonly DependencyProperty TextDecorationsProperty = DependencyProperty.RegisterAttached("TextDecorations", typeof(TextDecorationCollection), typeof(ControlHelper), new PropertyMetadata(null));

        // Token: 0x04000086 RID: 134
        public static readonly DependencyProperty TextTrimmingProperty = DependencyProperty.RegisterAttached("TextTrimming", typeof(TextTrimming), typeof(ControlHelper), new PropertyMetadata(TextTrimming.None));

        // Token: 0x04000087 RID: 135
        public static readonly DependencyProperty ContentOrientationProperty = DependencyProperty.RegisterAttached("ContentOrientation", typeof(Orientation), typeof(ControlHelper), new PropertyMetadata(Orientation.Horizontal));

        // Token: 0x04000088 RID: 136
        public static readonly DependencyProperty WatermarkTextProperty = DependencyProperty.RegisterAttached("WatermarkText", typeof(string), typeof(ControlHelper), new PropertyMetadata(null));

        // Token: 0x04000089 RID: 137
        public static readonly DependencyProperty ContentPaddingProperty = DependencyProperty.RegisterAttached("ContentPadding", typeof(Thickness), typeof(ControlHelper), new PropertyMetadata(default(Thickness)));

        // Token: 0x0400008A RID: 138
        public static readonly DependencyProperty MarinNormalProperty = DependencyProperty.RegisterAttached("MarinNormal", typeof(Thickness), typeof(ControlHelper), new PropertyMetadata(default(Thickness)));

        // Token: 0x0400008B RID: 139
        public static readonly DependencyProperty MarginHoverProperty = DependencyProperty.RegisterAttached("MarginHover", typeof(Thickness), typeof(ControlHelper), new PropertyMetadata(default(Thickness)));

        // Token: 0x0400008C RID: 140
        public static readonly DependencyProperty MarginPressedProperty = DependencyProperty.RegisterAttached("MarginPressed", typeof(Thickness), typeof(ControlHelper), new PropertyMetadata(default(Thickness)));

        // Token: 0x0400008D RID: 141
        public static readonly DependencyProperty MarginCheckedProperty = DependencyProperty.RegisterAttached("MarginChecked", typeof(Thickness), typeof(ControlHelper), new PropertyMetadata(default(Thickness)));

        // Token: 0x0400008E RID: 142
        public static readonly DependencyProperty EffectBlurRadiusProperty = DependencyProperty.RegisterAttached("EffectBlurRadius", typeof(double), typeof(ControlHelper), new PropertyMetadata(0.0));

        // Token: 0x0400008F RID: 143
        public static readonly DependencyProperty EffectShadowDepthProperty = DependencyProperty.RegisterAttached("EffectShadowDepth", typeof(double), typeof(ControlHelper), new PropertyMetadata(0.0));

        // Token: 0x04000090 RID: 144
        public static readonly DependencyProperty EffectColorProperty = DependencyProperty.RegisterAttached("EffectColor", typeof(Color), typeof(ControlHelper), new PropertyMetadata(Colors.Transparent));

        // Token: 0x04000091 RID: 145
        public static readonly DependencyProperty EffectOpacityProperty = DependencyProperty.RegisterAttached("EffectOpacity", typeof(double), typeof(ControlHelper), new PropertyMetadata(0.0));

        // Token: 0x04000092 RID: 146
        public static readonly DependencyProperty ImageProperty = DependencyProperty.RegisterAttached("Image", typeof(ImageSource), typeof(ControlHelper), new PropertyMetadata(null));

        // Token: 0x04000093 RID: 147
        public static readonly DependencyProperty ImageHoverProperty = DependencyProperty.RegisterAttached("ImageHover", typeof(ImageSource), typeof(ControlHelper), new PropertyMetadata(null));

        // Token: 0x04000094 RID: 148
        public static readonly DependencyProperty ImagePressedProperty = DependencyProperty.RegisterAttached("ImagePressed", typeof(ImageSource), typeof(ControlHelper), new PropertyMetadata(null));

        // Token: 0x04000095 RID: 149
        public static readonly DependencyProperty ImageDisabledProperty = DependencyProperty.RegisterAttached("ImageDisabled", typeof(ImageSource), typeof(ControlHelper), new PropertyMetadata(null));

        // Token: 0x04000096 RID: 150
        public static readonly DependencyProperty ImageSizeProperty = DependencyProperty.RegisterAttached("ImageSize", typeof(double), typeof(ControlHelper), new PropertyMetadata(0.0));

        // Token: 0x04000097 RID: 151
        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.RegisterAttached("ImageHeight", typeof(double), typeof(ControlHelper), new PropertyMetadata(0.0));

        // Token: 0x04000098 RID: 152
        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.RegisterAttached("ImageWidth", typeof(double), typeof(ControlHelper), new PropertyMetadata(0.0));

        /// <summary>
        /// 图形
        /// </summary>
        // Token: 0x04000099 RID: 153
        public static readonly DependencyProperty GeometryProperty = DependencyProperty.RegisterAttached("Geometry", typeof(Geometry), typeof(ControlHelper), new PropertyMetadata(null));

        /// <summary>
        /// 图形
        /// </summary>
        // Token: 0x0400009A RID: 154
        public static readonly DependencyProperty Geometry2Property = DependencyProperty.RegisterAttached("Geometry2", typeof(Geometry), typeof(ControlHelper), new PropertyMetadata(null));

        // Token: 0x0400009B RID: 155
        public static readonly DependencyProperty SignImageHorizontalAlignmentProperty = DependencyProperty.RegisterAttached("SignImageHorizontalAlignment", typeof(HorizontalAlignment), typeof(ControlHelper), new PropertyMetadata(HorizontalAlignment.Left));

        // Token: 0x0400009C RID: 156
        public static readonly DependencyProperty SignImageVerticalAlignmentProperty = DependencyProperty.RegisterAttached("SignImageVerticalAlignment", typeof(VerticalAlignment), typeof(ControlHelper), new PropertyMetadata(VerticalAlignment.Top));

        // Token: 0x0400009D RID: 157
        public static readonly DependencyProperty SignImageMarginProperty = DependencyProperty.RegisterAttached("SignImageMargin", typeof(Thickness), typeof(ControlHelper), new PropertyMetadata(default(Thickness)));

        // Token: 0x0400009E RID: 158
        public static readonly DependencyProperty SignImageSizeProperty = DependencyProperty.RegisterAttached("SignImageSize", typeof(double), typeof(ControlHelper), new PropertyMetadata(0.0));

        // Token: 0x0400009F RID: 159
        public static readonly DependencyProperty ImageStretchProperty = DependencyProperty.RegisterAttached("ImageStretch", typeof(Stretch), typeof(ControlHelper), new PropertyMetadata(Stretch.None));
    }
}