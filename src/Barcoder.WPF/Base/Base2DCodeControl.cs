using System.Dynamic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

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
        protected override void DrawCode(double posX, double posY, IBarcode barcode, Brush foreground)
        {
            base.Width = base.Height = barcode.Bounds.X * ModuleSize;

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

                        switch (Rotation)
                        {
                            case Rotation.Rotate0:
                                break;

                            case Rotation.Rotate90:
                                newX = xMax - y - 1;
                                newY = x;
                                break;

                            case Rotation.Rotate180:
                                newX = xMax - x - 1;
                                newY = yMax - y - 1;
                                break;

                            case Rotation.Rotate270:
                                newX = y;
                                newY = yMax - x - 1;
                                break;
                        }
                        AddRectangle(newX * ModuleSize, newY * ModuleSize, ModuleSize, ModuleSize, foreground);
                    }
                }
            }
        }       
    }
}