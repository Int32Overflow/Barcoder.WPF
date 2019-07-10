using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Barcoder.WPF
{
    [TemplatePart(Name = CanvasElementName, Type = typeof(Canvas))]
    public abstract class Base1DCode : BaseBarcode
    {
        public static readonly DependencyProperty ModuleHeightProperty = DependencyProperty.Register(nameof(ModuleHeight), typeof(double), typeof(Base1DCode), new FrameworkPropertyMetadata(10d, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty ModuleWidthProperty = DependencyProperty.Register(nameof(ModuleWidth), typeof(double), typeof(Base1DCode), new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty RotationProperty = DependencyProperty.Register(nameof(Rotation), typeof(Rotation), typeof(Base1DCode), new FrameworkPropertyMetadata(Rotation.Rotate0, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(string), typeof(Base1DCode), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        public double ModuleHeight
        {
            get => GetValue<double>(ModuleHeightProperty);
            set => SetValue(ModuleHeightProperty, value);
        }

        public double ModuleWidth
        {
            get => GetValue<double>(ModuleWidthProperty);
            set => SetValue(ModuleWidthProperty, value);
        }

        public Rotation Rotation
        {
            get => GetValue<Rotation>(RotationProperty);
            set => SetValue(RotationProperty, value);
        }

        public string Value
        {
            get => GetValue<string>(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public override void Redraw()
        {
            const double posX = 0;
            const double posY = 0;

            if (_canvas == null)
                return;

            _canvas.Children.Clear();

            var barcode = GetBarcode();

            var rotation = Rotation;
            var moduleWidth = ModuleWidth;
            var moduleHeight = ModuleHeight;

            //Calculate and set dynamic size of canvas
            var size = CalculateCanvasSize(barcode, rotation, moduleHeight, moduleWidth);
            Width = size.Width;
            Height = size.Height;

            var xMax = barcode.Bounds.X;

            double startX = -1;
            for (int x = 0; x < xMax; x++)
            {
                if (barcode.At(x, 0))
                {
                    if (startX < 0)
                        startX = x;
                }
                else
                {
                    if (startX >= 0)
                    {
                        var width = (x - startX) * moduleWidth;

                        switch (rotation)
                        {
                            case Rotation.Rotate0:
                                AddRectangle(posX + (startX * moduleWidth), posY, width, moduleHeight);
                                break;

                            case Rotation.Rotate90:
                                AddRectangle(posX, posY + (startX * moduleWidth), moduleHeight, width);
                                break;

                            case Rotation.Rotate180:
                                AddRectangle(posX + ((xMax - x - 1) * moduleWidth), posY, width, moduleHeight);
                                break;

                            case Rotation.Rotate270:
                                AddRectangle(posX, posY + ((xMax - x - 1) * moduleWidth), moduleHeight, width);
                                break;
                        }
                        startX = -1;
                    }
                }
            }
            if (startX >= 0)
            {
                var x = xMax;
                var width = (x - startX) * moduleWidth;

                switch (rotation)
                {
                    case Rotation.Rotate0:
                        AddRectangle(posX + (startX * moduleWidth), posY, width, moduleHeight);
                        break;

                    case Rotation.Rotate90:
                        AddRectangle(posX, posY + (startX * moduleWidth), moduleHeight, width);
                        break;

                    case Rotation.Rotate180:
                        AddRectangle(posX + ((xMax - x - 1) * moduleWidth), posY, width, moduleHeight);
                        break;

                    case Rotation.Rotate270:
                        AddRectangle(posX, posY + ((xMax - x - 1) * moduleWidth), moduleHeight, width);
                        break;
                }
            }
        }

        private static Size CalculateCanvasSize(IBarcode barcode, Rotation rotation, double barcodeHeight, double moduleWidth)
        {
            switch (rotation)
            {
                case Rotation.Rotate0:
                case Rotation.Rotate180:
                    return new Size(moduleWidth * barcode.Bounds.X, barcodeHeight);

                case Rotation.Rotate90:
                case Rotation.Rotate270:
                    return new Size(barcodeHeight, moduleWidth * barcode.Bounds.X);

                default:
                    throw new NotSupportedException($"Rotation '{rotation}' is not supported!");
            }
        }
    }
}