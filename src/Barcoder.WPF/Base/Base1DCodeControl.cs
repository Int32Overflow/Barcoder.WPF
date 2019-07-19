using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Barcoder.WPF.Base
{
    [TemplatePart(Name = CanvasElementName, Type = typeof(Canvas))]
    [TemplatePart(Name = ErrorTextBlock, Type = typeof(TextBlock))]
    public abstract class Base1DCodeControl : BaseBarcodeControl
    {
        public static readonly DependencyProperty ModuleHeightProperty = DependencyProperty.Register(nameof(ModuleHeight), typeof(double), typeof(Base1DCodeControl), new FrameworkPropertyMetadata(10d, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty ModuleWidthProperty = DependencyProperty.Register(nameof(ModuleWidth), typeof(double), typeof(Base1DCodeControl), new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(string), typeof(Base1DCodeControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
        protected const string INVALID_NUMBER_STRING = "1234";
        protected const string INVALID_TEXT_STRING = "Invalid";

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

            if (barcode == null)
            {
                DrawBarcode(posX, posY, GetErrorBarcode());
                _errorTextBlock.Visibility = Visibility.Visible;
                _errorTextBlock.Text = "Invalid";
                switch (Rotation)
                {
                    case Rotation.Rotate0:
                        _errorTextBlock.LayoutTransform = null;
                        break;

                    case Rotation.Rotate90:
                        _errorTextBlock.LayoutTransform = new RotateTransform(90);
                        break;

                    case Rotation.Rotate180:
                        _errorTextBlock.LayoutTransform = new RotateTransform(180);
                        break;

                    case Rotation.Rotate270:
                        _errorTextBlock.LayoutTransform = new RotateTransform(270);
                        break;
                }
            }
            else
            {
                _errorTextBlock.Visibility = Visibility.Collapsed;
                DrawBarcode(posX, posY, barcode);
            }
        }

        protected virtual double CalculateBarWidth(bool isBlack, int modulesCount)
        {
            return modulesCount * ModuleWidth;
        }

        private void CalculateNextBarPosition(ref double posX, ref double posY, Rotation rotation, double width)
        {
            switch (rotation)
            {
                case Rotation.Rotate0:
                case Rotation.Rotate180:
                    posX += width;
                    break;

                case Rotation.Rotate90:
                case Rotation.Rotate270:
                    posY += width;
                    break;
            }
        }

        private void DrawBar(double posX, double posY, Rotation rotation, double width, double height)
        {
            switch (rotation)
            {
                case Rotation.Rotate0:
                case Rotation.Rotate180:
                    AddRectangle(posX, posY, width, height);
                    break;

                case Rotation.Rotate90:
                case Rotation.Rotate270:
                    AddRectangle(posX, posY, height, width);
                    break;
            }
        }

        private void DrawBar(ref double posX, ref double posY, Rotation rotation, double moduleHeight, bool isBlack, int barCounts)
        {
            if (barCounts > 0)
            {
                var barWidth = CalculateBarWidth(isBlack, barCounts);
                if (isBlack)
                {
                    DrawBar(posX, posY, rotation, barWidth, moduleHeight);
                }
                else
                {
                    //White bar
                }
                CalculateNextBarPosition(ref posX, ref posY, rotation, barWidth);
            }
        }

        private void DrawBarcode(double posX, double posY, IBarcode barcode)
        {
            var orgPosX = posX;
            var orgPosY = posY;

            var rotation = Rotation;
            var moduleHeight = ModuleHeight;

            var iMax = barcode.Bounds.X;

            var binaryData = new bool[iMax];
            switch (rotation)
            {
                case Rotation.Rotate0:
                case Rotation.Rotate90:
                    {
                        for (int i = 0; i < iMax; i++)
                            binaryData[i] = barcode.At(i, 0);
                    }
                    break;

                case Rotation.Rotate180:
                case Rotation.Rotate270:
                    {
                        for (int i = 0; i < iMax; i++)
                            binaryData[i] = barcode.At(iMax - i - 1, 0);
                    }
                    break;
            }

            var lastValue = false;
            var lastI = 0;
            for (int i = lastI; i < iMax; i++)
            {
                var value = binaryData[i];
                if (value != lastValue)
                {
                    DrawBar(ref posX, ref posY, rotation, moduleHeight, lastValue, i - lastI);
                    lastValue = value;
                    lastI = i;
                }
            }
            if (lastI != iMax)
            {
                DrawBar(ref posX, ref posY, rotation, moduleHeight, lastValue, iMax - lastI);
            }

            switch (rotation)
            {
                case Rotation.Rotate0:
                case Rotation.Rotate180:
                    Width = posX - orgPosX;
                    Height = moduleHeight;
                    break;

                case Rotation.Rotate90:
                case Rotation.Rotate270:
                    Width = moduleHeight;
                    Height = posY - orgPosY;
                    break;
            }
        }
    }
}