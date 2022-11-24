using Barcoder.Ean;
using Barcoder.WPF.Base;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Barcoder.WPF
{
    [TemplatePart(Name = CanvasElementName, Type = typeof(Canvas))]
    [TemplatePart(Name = ErrorTextBlock, Type = typeof(TextBlock))]
    public class Ean8 : Base1DCodeControl
    {
        static Ean8()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Ean8), new FrameworkPropertyMetadata(typeof(Ean8)));
        }

        protected override IBarcode GetBarcode()
        {
            var value = Value;
            if (string.IsNullOrEmpty(value))
                return null;
            if (value.Length < 7 || value.Length > 8)
                return null;
            try
            {
                return EanEncoder.Encode(value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override IBarcode GetErrorBarcode()
        {
            return EanEncoder.Encode("1234567");
        }
    }
}