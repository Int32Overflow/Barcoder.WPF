using Barcoder.WPF.Base;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Barcoder.WPF.Demo
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string TextInvalid { get; set; } = BaseBarcodeControl.INVALID_TEXT_STRING;
        public string Value { get; set; }
    }
}