using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Barcoder.WPF.Base
{
    [TemplatePart(Name = CanvasElementName, Type = typeof(Canvas))]
    [TemplatePart(Name = ErrorTextBlock, Type = typeof(TextBlock))]
    public abstract class BaseBarcodeControl : Control
    {
        internal const string CanvasElementName = "PART_Canvas";
        internal const string ErrorTextBlock = "PART_Error_Text";
        protected Canvas _canvas;
        protected TextBlock _errorTextBlock;

        public new double Height
        {
            get => base.Height;
            protected set => base.Height = value;
        }

        public new double Width
        {
            get => base.Width;
            protected set => base.Width = value;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _canvas = GetTemplateChild(CanvasElementName) as Canvas;
            _errorTextBlock = GetTemplateChild(ErrorTextBlock) as TextBlock;

            Redraw();
        }

        public abstract void Redraw();

        protected void AddControl(UIElement uIElement, double x, double y)
        {
            _canvas.Children.Add(uIElement);
            Canvas.SetLeft(uIElement, x);
            Canvas.SetTop(uIElement, y);
        }

        protected void AddRectangle(double x, double y, double width, double height)
        {
            var rect = new Rectangle()
            {
                Fill = Foreground,
                Stroke = Foreground,
                Width = width,
                Height = height,
                StrokeThickness = 0,
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