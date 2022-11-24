using Barcoder.Code128;
using Barcoder.WPF.Base;
using System.Windows;
using System.Windows.Controls;

namespace Barcoder.WPF
{
    [TemplatePart(Name = CanvasElementName, Type = typeof(Canvas))]
    [TemplatePart(Name = ErrorTextBlock, Type = typeof(TextBlock))]
    public class Code128 : Base1DCodeControl
    {
        public static readonly DependencyProperty IncludeChecksumProperty = DependencyProperty.Register(nameof(IncludeChecksum), typeof(bool), typeof(Code128), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None));

        static Code128()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Code128), new FrameworkPropertyMetadata(typeof(Code128)));
        }

        public bool IncludeChecksum
        {
            get => GetValue<bool>(IncludeChecksumProperty);
            set => SetValue(IncludeChecksumProperty, value);
        }

        protected override IBarcode GetBarcode()
        {
            var value = Value;
            if (string.IsNullOrEmpty(value))
                return null;
            return Code128Encoder.Encode(value, IncludeChecksum);
        }

        protected override IBarcode GetErrorBarcode()
        {
            return Code128Encoder.Encode(INVALID_TEXT_STRING, IncludeChecksum);
        }
    }
}