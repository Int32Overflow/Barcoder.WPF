# Barcoder.WPF
WPF Component for Library [Barcoder](https://github.com/huysentruitw/barcoder) [![NuGet Status](http://img.shields.io/nuget/v/Barcoder.svg?style=flat&max-age=86400)](https://www.nuget.org/packages/Barcoder/)


## Install

Install NuGet-Package [Barcoder.WPF](https://www.nuget.org/packages/Barcoder.WPF/) 
[![NuGet Status](http://img.shields.io/nuget/v/Barcoder.WPF.svg?style=flat&max-age=86400)](https://www.nuget.org/packages/Barcoder.WPF/)

```
Install-Package Barcoder.WPF
```

## Supported Barcode Types:
- [x] DataMatrix (ECC 200)
- [x] Code128
- [ ] 2 of 5
- [ ] Codabar
- [ ] Code 39
- [ ] Code 93
- [ ] EAN 8
- [ ] EAN 13
- [ ] QR Code

## Usage - WPF Control

**Add resource to the file App.xaml:**
```
<ResourceDictionary Source="pack://application:,,,/Barcoder.WPF;component/Theme.xaml" />
```

**Example MainWindow:**
```
<Window x:Class="Barcode.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:c="clr-namespace:Barcoder.WPF;assembly=Barcoder.WPF"
        mc:Ignorable="d" Title="MainWindow" Height="250" Width="500">
    <StackPanel Orientation="Horizontal">
        <c:Code128 Value="123599" ModuleWidth="1" Height="50" Foreground="Red" IncludeChecksum="True" />
        <c:DataMatrix Value="1234599" ModuleSize="8" Foreground="Green" />
    </StackPanel>
</Window>
```
