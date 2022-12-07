namespace Barcoder.WPF
{
    public static class DataMatrixSizeExtension
    {
        public static void ToRowsAndColumns(this DataMatrixSize value, out byte rows, out byte columns)
        {
            rows = (byte)((ushort)value >> 8);
            columns = (byte)value;
        }
    }
}