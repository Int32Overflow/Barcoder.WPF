using System.Windows;
using System.Windows.Media;

namespace Barcoder.WPF.Base
{
    public abstract class BaseEanCodeControl: Base1DCodeControl
    {
        public static readonly DependencyProperty MarkerLineLengthProperty = DependencyProperty.Register(nameof(MarkerLineLength), typeof(double), typeof(BaseEanCodeControl), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.None));

        public double MarkerLineLength
        {
            get => GetValue<double>(MarkerLineLengthProperty);
            set => SetValue(MarkerLineLengthProperty, value);
        }

        protected override void DrawBar(ref double posX, double posY, double moduleHeight, bool isBlack, int startIndex, int barCounts, int totalLength, Brush foreground)
        {
            var markerLineLength = MarkerLineLength;
            if(markerLineLength > 0 && barCounts > 0)
            {
                var halfLength = (totalLength - 1) / 2;
                if(startIndex < 3) // 2 black and 1 white bar
                {
                    // Start marker
                }
                else if(totalLength - 3 <= startIndex) // 2 black and 1 white bar
                {
                    // End marker
                }
                else if (halfLength - 1 <= startIndex && halfLength + 1 >= startIndex) // Left and right bar from the center
                {
                    // Center marker
                }
                else
                {
                    moduleHeight -= markerLineLength;
                }

            }
            base.DrawBar(ref posX, posY, moduleHeight, isBlack, startIndex, barCounts, totalLength, foreground);
        }
    }
}
