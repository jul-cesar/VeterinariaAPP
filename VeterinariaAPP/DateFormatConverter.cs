using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace VeterinariaAPP
{
    public class DateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string dateStr && DateTime.TryParse(dateStr, null, DateTimeStyles.RoundtripKind, out DateTime dateTime))
            {
                // Aquí puedes personalizar el formato de salida
                return dateTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string dateStr && DateTime.TryParseExact(dateStr, "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            {
                return dateTime;
            }
            return value;
        }
    }
}
