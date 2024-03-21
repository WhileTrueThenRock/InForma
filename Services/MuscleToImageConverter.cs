using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileAppTest.Services
{
    public class MuscleToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string muscleName)
            {
                System.Diagnostics.Debug.WriteLine($"MuscleToImageConverter: Converting {muscleName}");

                switch (muscleName)
                {
                    case "Pecho":
                        return "Images/trapecio.png";
                    case "Hombro":
                        return "Images/banco.jpg";
                    case "Tríceps":
                        return "Images/pecho.png";
                    // Add more cases for other equipment names
                    default:

                        return null; // Return null for unknown equipment names
                }
            }
            System.Diagnostics.Debug.WriteLine("MuscleToImageConverter: Value is not a string");

            return null; // Return null if the value is not a string
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
