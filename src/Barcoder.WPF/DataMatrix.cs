using System.Windows;
using System.Windows.Controls;
using Barcoder.DataMatrix;

namespace Barcoder.WPF
{
    [TemplatePart(Name = CanvasElementName, Type = typeof(Canvas))]
    public class DataMatrix : Base2DCode
    {
        public static readonly DependencyProperty FixedRowCountProperty = DependencyProperty.Register(nameof(FixedRowCount), typeof(byte), typeof(DataMatrix), new FrameworkPropertyMetadata((byte)0, FrameworkPropertyMetadataOptions.None));

        static DataMatrix()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataMatrix), new FrameworkPropertyMetadata(typeof(DataMatrix)));
        }

        public byte FixedRowCount
        {
            get => GetValue<byte>(FixedRowCountProperty);
            set => SetValue(FixedRowCountProperty, value);
        }

        protected override IBarcode GetBarcode()
        {
            int? fixedRowCount = FixedRowCount == 0 ? (int?)null : FixedRowCount;
            return DataMatrixEncoder.Encode(Value, fixedRowCount);
        }
    }
}