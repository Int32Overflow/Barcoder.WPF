using Barcoder.DataMatrix;
using Barcoder.WPF.Base;
using System.Windows;
using System.Windows.Controls;

namespace Barcoder.WPF
{
    [TemplatePart(Name = CanvasElementName, Type = typeof(Canvas))]
    public class DataMatrix : Base2DCodeControl
    {
        public static readonly DependencyProperty FixedColumnCountProperty = DependencyProperty.Register(nameof(FixedColumnCount), typeof(byte), typeof(DataMatrix), new FrameworkPropertyMetadata((byte)0, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty FixedRowCountProperty = DependencyProperty.Register(nameof(FixedRowCount), typeof(byte), typeof(DataMatrix), new FrameworkPropertyMetadata((byte)0, FrameworkPropertyMetadataOptions.None));

        static DataMatrix()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataMatrix), new FrameworkPropertyMetadata(typeof(DataMatrix)));
        }

        public byte FixedColumnCount
        {
            get => GetValue<byte>(FixedColumnCountProperty);
            set => SetValue(FixedColumnCountProperty, value);
        }

        public byte FixedRowCount
        {
            get => GetValue<byte>(FixedRowCountProperty);
            set => SetValue(FixedRowCountProperty, value);
        }

        protected override IBarcode GetBarcode()
        {
            int? fixedRowCount = FixedRowCount == 0 ? null : FixedRowCount;
            int? fixedColumnCount = FixedRowCount == 0 ? null : FixedRowCount;
            if (fixedColumnCount == null || fixedRowCount == null)
                fixedRowCount = fixedColumnCount = null;
            return DataMatrixEncoder.Encode(Value ?? "", fixedRowCount, fixedColumnCount);
        }

        protected override IBarcode GetErrorBarcode()
        {
            return DataMatrixEncoder.Encode("Invalid");
        }
    }
}