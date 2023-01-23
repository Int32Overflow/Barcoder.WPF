using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Barcoder.WPF.Base
{
    [TemplatePart(Name = CanvasElementName, Type = typeof(Canvas))]
    [TemplatePart(Name = ErrorTextBlock, Type = typeof(TextBlock))]
    public abstract class Base1DCodeControl : BaseBarcodeControl
    {
        public static readonly DependencyProperty ModuleHeightProperty = DependencyProperty.Register(nameof(ModuleHeight), typeof(double), typeof(Base1DCodeControl), new FrameworkPropertyMetadata(10d, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty ModuleWidthProperty = DependencyProperty.Register(nameof(ModuleWidth), typeof(double), typeof(Base1DCodeControl), new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.None));

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

        protected virtual double CalculateBarWidth(int modulesCount)
        {
            return modulesCount * ModuleWidth;
        }

        protected virtual void DrawBar(ref double posX, double posY, double moduleHeight, bool isBlack, int startIndex, int barCounts, int totalLength, Brush foreground)
        {
            if (barCounts > 0)
            {
                var barWidth = CalculateBarWidth(barCounts);
                if (isBlack)
                {
                    DrawBar(posX, posY, barWidth, moduleHeight, foreground);
                }
                else
                {
                    //White bar
                }
                posX += barWidth;
            }
        }

        protected override void DrawCode(double posX, double posY, IBarcode barcode, Brush foreground)
        {
            var orgPosX = posX;
            var moduleHeight = ModuleHeight;

            var iMax = barcode.Bounds.X;

            var lastValue = false;
            var lastDrawedIndex = 0;
            for (int i = lastDrawedIndex; i < iMax; i++)
            {
                var value = barcode.At(i, 0);
                if (value != lastValue)
                {
                    DrawBar(ref posX, posY, moduleHeight, lastValue, lastDrawedIndex, i - lastDrawedIndex, iMax, foreground);
                    lastValue = value;
                    lastDrawedIndex = i;
                }
            }
            if (lastDrawedIndex != iMax)
            {
                DrawBar(ref posX, posY, moduleHeight, lastValue, lastDrawedIndex, iMax - lastDrawedIndex, iMax, foreground);
            }

            Width = posX - orgPosX;
            Height = moduleHeight;
        }

        private void DrawBar(double posX, double posY, double width, double height, Brush color)
        {
            AddRectangle(posX, posY, width, height, color);
        }
    }
}