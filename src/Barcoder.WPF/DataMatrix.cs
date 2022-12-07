using Barcoder.DataMatrix;
using Barcoder.WPF.Base;
using System.Windows;
using System.Windows.Controls;

namespace Barcoder.WPF
{
    [TemplatePart(Name = CanvasElementName, Type = typeof(Canvas))]
    public class DataMatrix : Base2DCodeControl
    {
        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(nameof(Size), typeof(DataMatrixSize), typeof(DataMatrix), new FrameworkPropertyMetadata(DataMatrixSize.Auto, FrameworkPropertyMetadataOptions.None));

        static DataMatrix()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataMatrix), new FrameworkPropertyMetadata(typeof(DataMatrix)));
        }

        public DataMatrixSize Size
        {
            get => GetValue<DataMatrixSize>(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        protected override IBarcode GetBarcode()
        {
            int? fixedRowCount = null, fixedColumnCount = null;
            if (Size != DataMatrixSize.Auto)
            {
                Size.ToRowsAndColumns(out var rows, out var columns);
                fixedRowCount = rows;
                fixedColumnCount = columns;
            }

            return DataMatrixEncoder.Encode(Value ?? "", fixedRowCount, fixedColumnCount);
        }

        protected override IBarcode GetErrorBarcode()
        {
            return DataMatrixEncoder.Encode("Invalid");
        }
    }
}