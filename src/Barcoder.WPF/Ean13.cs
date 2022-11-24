using Barcoder.Ean;
using Barcoder.WPF.Base;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Barcoder.WPF
{
    [TemplatePart(Name = CanvasElementName, Type = typeof(Canvas))]
    [TemplatePart(Name = ErrorTextBlock, Type = typeof(TextBlock))]
    public class Ean13 : Base1DCodeControl
    {
        static Ean13()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Ean13), new FrameworkPropertyMetadata(typeof(Ean13)));
        }

        protected override IBarcode GetBarcode()
        {
            var value = Value;
            if (string.IsNullOrEmpty(value))
                return null;
            if (value.Length < 12 || value.Length > 13)
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
            return EanEncoder.Encode("123456789012");
        }
    }
}