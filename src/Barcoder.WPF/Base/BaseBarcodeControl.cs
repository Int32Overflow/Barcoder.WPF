using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Barcoder.WPF.Base
{
    [TemplatePart(Name = CanvasElementName, Type = typeof(Canvas))]
    [TemplatePart(Name = ErrorTextBlock, Type = typeof(TextBlock))]
    public abstract class BaseBarcodeControl : Control
    {
        public const string INVALID_NUMBER_STRING = "1234";
        public const string INVALID_TEXT_STRING = "INVALID";
        public static readonly DependencyProperty ForegroundInvalidProperty = DependencyProperty.Register(nameof(ForegroundInvalid), typeof(Brush), typeof(BaseBarcodeControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty RotationProperty = DependencyProperty.Register(nameof(Rotation), typeof(Rotation), typeof(BaseBarcodeControl), new FrameworkPropertyMetadata(Rotation.Rotate0, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty TextInvalidProperty = DependencyProperty.Register(nameof(TextInvalid), typeof(string), typeof(BaseBarcodeControl), new FrameworkPropertyMetadata(INVALID_TEXT_STRING, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(string), typeof(BaseBarcodeControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
        internal const string CanvasElementName = "PART_Canvas";
        internal const string ErrorTextBlock = "PART_Error_Text";
        protected Canvas canvas;
        protected TextBlock errorTextBlock;

        public Brush ForegroundInvalid
        {
            get => GetValue<Brush>(ForegroundInvalidProperty);
            set => SetValue(ForegroundInvalidProperty, value);
        }

        public new double Height
        {
            get => base.Height;
            protected set => base.Height = value;
        }

        public string TextInvalid
        {
            get => GetValue<string>(TextInvalidProperty);
            set => SetValue(TextInvalidProperty, value);
        }

        public string Value
        {
            get => GetValue<string>(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public new double Width
        {
            get => base.Width;
            protected set => base.Width = value;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            canvas = GetTemplateChild(CanvasElementName) as Canvas;
            errorTextBlock = GetTemplateChild(ErrorTextBlock) as TextBlock;

            Redraw();
        }

        public void Redraw()
        {
            const double posX = 0;
            const double posY = 0;

            if (canvas == null)
                return;

            canvas.Children.Clear();

            IBarcode barcode = default;
            try
            {
                barcode = GetBarcode();
            }
            catch { }

            errorTextBlock.Visibility = Visibility.Collapsed;
            if (barcode == null)
            {
                DrawCode(posX, posY, GetErrorBarcode(), ForegroundInvalid ?? Foreground);

                var textInvalid = TextInvalid;
                if (!string.IsNullOrEmpty(textInvalid))
                {
                    errorTextBlock.Text = textInvalid;
                    errorTextBlock.Visibility = Visibility.Visible;
                    errorTextBlock.Foreground = ForegroundInvalid ?? Foreground;
                }
            }
            else
            {
                DrawCode(posX, posY, barcode, Foreground);
            }
        }

        protected void AddControl(UIElement uIElement, double x, double y)
        {
            canvas.Children.Add(uIElement);
            Canvas.SetLeft(uIElement, x);
            Canvas.SetTop(uIElement, y);
        }

        protected void AddRectangle(double x, double y, double width, double height, Brush color)
        {
            var rect = new Rectangle()
            {
                Fill = color,
                Stroke = color,
                Width = width,
                Height = height,
                StrokeThickness = 0,
                IsHitTestVisible = IsHitTestVisible,
            };
            AddControl(rect, x, y);
            RenderOptions.SetEdgeMode(rect, EdgeMode.Aliased);
        }

        protected abstract void DrawCode(double posX, double posY, IBarcode barcode, Brush foreground);

        protected abstract IBarcode GetBarcode();

        protected abstract IBarcode GetErrorBarcode();

        protected T GetValue<T>(DependencyProperty dependencyProperty)
        {
            return (T)GetValue(dependencyProperty);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            Redraw();
        }
    }
}