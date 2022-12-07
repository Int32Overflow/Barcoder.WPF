using System.ComponentModel;

namespace Barcoder.WPF
{
    public enum DataMatrixSize : ushort
    {
        [Description("Auto")]
        Auto,

        [Description("10x10")]
        Size_10x10 = 0x0a0a,

        [Description("12x12")]
        Size_12x12 = 0x0c0c,

        [Description("14x14")]
        Size_14x14 = 0x0e0e,

        [Description("16x16")]
        Size_16x16 = 0x1010,

        [Description("18x18")]
        Size_18x18 = 0x1212,

        [Description("20x20")]
        Size_20x20 = 0x1414,

        [Description("22x22")]
        Size_22x22 = 0x1616,

        [Description("24x24")]
        Size_24x24 = 0x1818,

        [Description("26x26")]
        Size_26x26 = 0x1a1a,

        [Description("32x32")]
        Size_32x32 = 0x2020,

        [Description("36x36")]
        Size_36x36 = 0x2424,

        [Description("40x40")]
        Size_40x40 = 0x2828,

        [Description("44x44")]
        Size_44x44 = 0x2c2c,

        [Description("48x48")]
        Size_48x48 = 0x3030,

        [Description("52x52")]
        Size_52x52 = 0x3434,

        [Description("64x64")]
        Size_64x64 = 0x4040,

        [Description("72x72")]
        Size_72x72 = 0x4848,

        [Description("80x80")]
        Size_80x80 = 0x5050,

        [Description("88x88")]
        Size_88x88 = 0x5858,

        [Description("96x96")]
        Size_96x96 = 0x6060,

        [Description("104x104")]
        Size_104x104 = 0x6868,

        [Description("120x120")]
        Size_120x120 = 0x7878,

        [Description("132x132")]
        Size_132x132 = 0x8484,

        [Description("144x144")]
        Size_144x144 = 0x9090,

        [Description("8x18")]
        Size_8x18 = 0x0812,

        [Description("8x32")]
        Size_8x32 = 0x0820,

        [Description("12x26")]
        Size_12x26 = 0x0c1a,

        [Description("12x36")]
        Size_12x36 = 0x0c24,

        [Description("16x36")]
        Size_16x36 = 0x1024,

        [Description("16x48")]
        Size_16x48 = 0x1030,
    }
}