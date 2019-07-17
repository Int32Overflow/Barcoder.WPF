using System.Windows;
using System.Windows.Controls;

namespace Barcoder.WPF.Base
{
    [TemplatePart(Name = CanvasElementName, Type = typeof(Canvas))]
    public abstract class Base2DCodeControl : BaseBarcodeControl
    {
        public static readonly DependencyProperty ModuleSizeProperty = DependencyProperty.Register(nameof(ModuleSize), typeof(double), typeof(Base2DCodeControl), new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(string), typeof(Base2DCodeControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        public double ModuleSize
        {
            get => (double)GetValue(ModuleSizeProperty);
            set => SetValue(ModuleSizeProperty, value);
        }

        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public override void Redraw()
        {
            if (_canvas == null)
                return;

            _canvas.Children.Clear();

            var barcode = GetBarcode();

            base.Width = base.Height = barcode.Bounds.X * ModuleSize;

            for (var y = 0; y < barcode.Bounds.Y; y++)
            {
                for (var x = 0; x < barcode.Bounds.X; x++)
                {
                    if (barcode.At(x, y))
                    {
                        AddRectangle(x * ModuleSize, y * ModuleSize, ModuleSize, ModuleSize);
                    }
                }
            }
        }
    }
}