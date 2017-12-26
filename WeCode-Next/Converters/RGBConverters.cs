using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace WeCode_Next.Converters
{
    public class RGBRedConverters : IValueConverter
    {
        // Define the Convert method to convert a DateTime value to 
        // a month string.
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return "255";
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
