using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileAppTest.Services
{
    public class EquipmentToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string equipmentName)
            {
                switch (equipmentName)
                {
                    case "Mancuernas":
                        return "Images/anillas.jpg";
                    case "Banco":
                        return "Images/banco.jpg";
                    case "Barras":
                        return "Images/pecho.png";
                    // Add more cases for other equipment names
                    default:
                        return null; // Return null for unknown equipment names
                }
            }

            return null; // Return null if the value is not a string
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
