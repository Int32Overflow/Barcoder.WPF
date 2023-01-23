using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Barcoder.WPF.Base
{
    [TemplatePart(Name = CanvasElementName, Type = typeof(Canvas))]
    public abstract class Base2DCodeControl : BaseBarcodeControl
    {
        public static readonly DependencyProperty ModuleSizeProperty = DependencyProperty.Register(nameof(ModuleSize), typeof(double), typeof(Base2DCodeControl), new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.None));
      
        public double ModuleSize
        {
            get => (double)GetValue(ModuleSizeProperty);
            set => SetValue(ModuleSizeProperty, value);
        }

        protected override void DrawCode(double posX, double posY, IBarcode barcode, Brush foreground)
        {
            base.Width = barcode.Bounds.X * ModuleSize;
            base.Height = barcode.Bounds.Y * ModuleSize;

            var xMax = barcode.Bounds.X;
            var yMax = barcode.Bounds.Y;

            for (var y = 0; y < yMax; y++)
            {
                for (var x = 0; x < xMax; x++)
                {
                    if (barcode.At(x, y))
                    {
                        var newX = x;
                        var newY = y;
   
                        AddRectangle(newX * ModuleSize, newY * ModuleSize, ModuleSize, ModuleSize, foreground);
                    }
                }
            }
        }       
    }
}