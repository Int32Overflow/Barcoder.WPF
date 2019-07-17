using System;
using System.Windows;
using System.Windows.Controls;
using Barcoder.Code39;
using Barcoder.WPF.Base;

namespace Barcoder.WPF
{
    [TemplatePart(Name = CanvasElementName, Type = typeof(Canvas))]
    [TemplatePart(Name = ErrorTextBlock, Type = typeof(TextBlock))]
    public class Code39 : Base1DCodeControl
    {
        public static readonly DependencyProperty IncludeChecksumProperty = DependencyProperty.Register(nameof(IncludeChecksum), typeof(bool), typeof(Code39), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty WideToNarrowBarWidthRatioProperty = DependencyProperty.Register(nameof(WideToNarrowBarWidthRatio), typeof(float), typeof(Code39), new FrameworkPropertyMetadata(3.0f, FrameworkPropertyMetadataOptions.None), WideToNarrowBarWidthRatioProperty_Validate);

        static Code39()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Code39), new FrameworkPropertyMetadata(typeof(Code39)));
        }

        public bool IncludeChecksum
        {
            get => GetValue<bool>(IncludeChecksumProperty);
            set => SetValue(IncludeChecksumProperty, value);
        }

        public float WideToNarrowBarWidthRatio
        {
            get => GetValue<float>(WideToNarrowBarWidthRatioProperty);
            set
            {
                if (!WideToNarrowBarWidthRatioProperty_Validate(value))
                    throw new ArgumentOutOfRangeException(nameof(WideToNarrowBarWidthRatio), "The value must be between 2 and 3 inclusive.");
                SetValue(WideToNarrowBarWidthRatioProperty, value);
            }
        }

        protected override double CalculateBarWidth(bool isBlack, int modulesCount)
        {
            if (modulesCount == 1)
                return modulesCount * ModuleWidth;
            else if (modulesCount == 2)
                return WideToNarrowBarWidthRatio * ModuleWidth;
            else
                throw new ArgumentOutOfRangeException(nameof(modulesCount));
        }

        protected override IBarcode GetBarcode()
        {
            var value = Value;
            if (string.IsNullOrEmpty(value))
                return null;
            return Code39Encoder.Encode(value, IncludeChecksum, false);
        }

        protected override IBarcode GetErrorBarcode()
        {
            return Code39Encoder.Encode(INVALID_NUMBER_STRING, false, false);
        }

        private static bool WideToNarrowBarWidthRatioProperty_Validate(object value)
        {
            if (value is float f)
                return WideToNarrowBarWidthRatioProperty_Validate(f);
            return false;
        }

        private static bool WideToNarrowBarWidthRatioProperty_Validate(float value)
        {
            return value >= 2.0 && value <= 3.0;
        }
    }
}