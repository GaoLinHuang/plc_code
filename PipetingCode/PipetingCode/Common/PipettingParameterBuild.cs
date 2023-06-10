using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipetitngCode.Common
{
    public class PipettingParameterBuild
    {
        // 针管数
        public static int TxtNumberOfStitches { get; set; }
        // 气压阈值
        public static int[] ThresholdPress { get; set; }
        // 吸液量
        public static int TxtSuctionVolume { get; set; }
        // 吸液回吐量
        public static int TxtRegurgitation { get; set; }
        // 液面探测最低高度
        public static int MtxtHeightLimited { get; set; }
        // 10混吸液高度
        public static int TxtImmersionDepth10 { get; set; }
        // 20混吸液高度
        public static int TxtImmersionDepth20 { get; set; }
        // 回吐高度
        public static int TxtRetrusionHeight { get; set; }
        // 浸没深度
        public static int MtxtImmersionDepth { get; set; }
        // 吸液延迟
        public static int TxtDelay { get; set; }
        // 针尖间隙
        public static int TxtTipClearance { get; set; }
        // 容器底面积
        public static int MtxtContainerArea { get; set; }
        // 吸液速度
        public static int MtxtVolumeSpeed { get; set; }
        // 针尾间隙
        public static int TxtNeedleTailGap { get; set; }
        // 返回探测高度
        public static int MtxtFhtcgd { get; set; }
        // 吸液加速度
        public static int MtxtVolumeAccSpeed { get; set; }
        // 液面探测速度
        public static int TextBox3 { get; set; }
        // 戳到硬物失步阈值
        public static int TextBox1 { get; set; }
        // 回吸量
        public static int TxtDesorptionL { get; set; }
        // 10混、20混液面探测高度
        public static int LiquildDectectionHeight10 { get; set; }
        // 单混液面探测高度
        public static int LiquildDectectionHeight01 { get; set; }
        // 注液速度
        public static int MInjectionPumpSpeed { get; set; }
        // 注液量
        public static int MInjectionPumpSpace { get; set; }
        // 注液高度
        public static int MInjectionHeight { get; set; }
        // 注液次数
        public static int TextBox5 { get; set; }
        // 注液加速度
        public static int MInjectionPumpAccSpeed { get; set; }
        // 注液延迟
        public static int TextBox2 { get; set; }
        // 注液完毕Z轴提起高度
        public static int MInjectionZSpace { get; set; }
        // 是否开启液面探测
        public static bool ChkLiquidLevelDetection { get; set; }
        // 是否不等分吸液位置
        public static bool CheckBox7 { get; set; }
    }
}
