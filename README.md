# Barcoder.WPF
WPF Component for Library [Barcoder](https://github.com/huysentruitw/barcoder) [![NuGet Status](http://img.shields.io/nuget/v/Barcoder.svg?style=flat&max-age=86400)](https://www.nuget.org/packages/Barcoder/)


## Install

Install NuGet-Package [Barcoder.WPF](https://www.nuget.org/packages/Barcoder.WPF/) 
[![NuGet Status](http://img.shields.io/nuget/v/Barcoder.WPF.svg?style=flat&max-age=86400)](https://www.nuget.org/packages/Barcoder.WPF/)

```
Install-Package Barcoder.WPF
```

## Supported Barcode Types:
- [x] Code128
- [x] Code 39
- [x] DataMatrix (ECC 200)
- [x] QR Code
- [ ] 2 of 5
- [ ] Codabar
- [ ] Code 93
- [ ] EAN 8
- [ ] EAN 13

## Usage - WPF Control

**Add resource to the file App.xaml:**
```
<ResourceDictionary Source="pack://application:,,,/Barcoder.WPF;component/Theme.xaml" />
```

**Example MainWindow:**
```
<Window x:Class="Barcoder.WPF.Demo.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:Barcoder.WPF.Demo"
        xmlns:c="clr-namespace:Barcoder.WPF;assembly=Barcoder.WPF"
        mc:Ignorable="d" Title="MainWindow" Height="250" Width="500">
    <StackPanel Orientation="Horizontal">
        <c:Code128 Value="" ModuleWidth="1" ModuleHeight="50" Foreground="Red" IncludeChecksum="True" HorizontalAlignment="Center" Rotation="Rotate90" Margin="10" />
        <c:Code128 Value="1234" ModuleWidth="1" ModuleHeight="50" Foreground="Red" IncludeChecksum="True" HorizontalAlignment="Center" Rotation="Rotate0" Margin="10" />
        <c:Code39 Value="1234" ModuleWidth="2" ModuleHeight="50" Foreground="Black" Rotation="Rotate90" WideToNarrowBarWidthRatio="3" IncludeChecksum="False" Margin="10" />
        <c:DataMatrix Value="1234599" ModuleSize="8" Foreground="Green" FixedRowCount="18" Margin="10" />
        <c:QRCode Value="123Abc" ModuleSize="5"  Margin="10" />
    </StackPanel>
</Window>
```
