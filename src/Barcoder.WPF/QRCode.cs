using System.Windows;
using System.Windows.Controls;
using Barcoder.Qr;
using Barcoder.WPF.Base;

namespace Barcoder.WPF
{
    [TemplatePart(Name = CanvasElementName, Type = typeof(Canvas))]
    public class QRCode : Base2DCodeControl
    {
        public static readonly DependencyProperty EncodingProperty = DependencyProperty.Register(nameof(Encoding), typeof(Encoding), typeof(QRCode), new FrameworkPropertyMetadata(Encoding.Auto, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty ErrorCorrectionLevelProperty = DependencyProperty.Register(nameof(ErrorCorrectionLevel), typeof(ErrorCorrectionLevel), typeof(QRCode), new FrameworkPropertyMetadata(ErrorCorrectionLevel.M, FrameworkPropertyMetadataOptions.None));

        static QRCode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QRCode), new FrameworkPropertyMetadata(typeof(DataMatrix)));
        }

        public Encoding Encoding
        {
            get => GetValue<Encoding>(EncodingProperty);
            set => SetValue(EncodingProperty, value);
        }

        public ErrorCorrectionLevel ErrorCorrectionLevel
        {
            get => GetValue<ErrorCorrectionLevel>(ErrorCorrectionLevelProperty);
            set => SetValue(ErrorCorrectionLevelProperty, value);
        }

        protected override IBarcode GetBarcode()
        {
            return QrEncoder.Encode(Value ?? "", ErrorCorrectionLevel, Encoding);
        }

        protected override IBarcode GetErrorBarcode()
        {
            return QrEncoder.Encode("Invalid", ErrorCorrectionLevel, Encoding);
        }
    }
}