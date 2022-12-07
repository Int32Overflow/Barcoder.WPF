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
        public static readonly DependencyProperty ForegroundInvalidProperty = DependencyProperty.Register(nameof(ForegroundInvalid), typeof(Brush), typeof(BaseBarcodeControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty RotationProperty = DependencyProperty.Register(nameof(Rotation), typeof(Rotation), typeof(BaseBarcodeControl), new FrameworkPropertyMetadata(Rotation.Rotate0, FrameworkPropertyMetadataOptions.None));
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

        public Rotation Rotation
        {
            get => GetValue<Rotation>(RotationProperty);
            set => SetValue(RotationProperty, value);
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

            if (barcode == null)
            {
                DrawCode(posX, posY, GetErrorBarcode(), ForegroundInvalid ?? Foreground);
                errorTextBlock.Visibility = Visibility.Visible;
                errorTextBlock.Text = "Invalid";
                errorTextBlock.Foreground = ForegroundInvalid ?? Foreground;
                switch (Rotation)
                {
                    case Rotation.Rotate0:
                        errorTextBlock.LayoutTransform = null;
                        break;

                    case Rotation.Rotate90:
                        errorTextBlock.LayoutTransform = new RotateTransform(90);
                        break;

                    case Rotation.Rotate180:
                        errorTextBlock.LayoutTransform = new RotateTransform(180);
                        break;

                    case Rotation.Rotate270:
                        errorTextBlock.LayoutTransform = new RotateTransform(270);
                        break;
                }
            }
            else
            {
                errorTextBlock.Visibility = Visibility.Collapsed;
                DrawCode(posX, posY, barcode, Foreground);
            }
        }

        protected abstract void DrawCode(double posX, double posY, IBarcode barcode, Brush foreground);

        protected void AddControl(UIElement uIElement, double x, double y)
        {
            canvas.Children.Add(uIElement);
            Canvas.SetLeft(uIElement, x);
            Canvas.SetTop(uIElement, y);
        }

        protected void AddRectangle(double x, double y, double width, double height, Brush foreground)
        {
            var rect = new Rectangle()
            {
                Fill = foreground,
                Stroke = foreground,
                Width = width,
                Height = height,
                StrokeThickness = 0,
                IsHitTestVisible = IsHitTestVisible,
            };
            AddControl(rect, x, y);
            RenderOptions.SetEdgeMode(rect, EdgeMode.Aliased);
        }

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