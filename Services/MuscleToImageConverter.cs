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
                        return "Images/Muscles/pecho.png";
                    case "Hombro":
                        return "Images/Muscles/hombro.png";
                    case "Cuello":
                        return "Images/Muscles/cuello.png";
                    case "Bíceps":
                        return "Images/Muscles/biceps.png";
                    case "Abdomen":
                        return "Images/Muscles/abdomen.png";
                    case "Antebrazo":
                        return "Images/Muscles/aductores.png";
                    case "Aductores":
                        return "Images/Muscles/abductores.png";
                    case "Abductores":
                        return "Images/Muscles/antebrazo.png";
                    case "Cuádriceps":
                        return "Images/Muscles/cuadriceps.png";
                    case "Trapecio":
                        return "Images/Muscles/trapecio.png";
                    case "Espalda":
                        return "Images/Muscles/espalda.png";
                    case "Tríceps":
                        return "Images/Muscles/triceps.png";
                    case "Glúteos":
                        return "Images/Muscles/gluteos.png";
                    case "Femoral":
                        return "Images/Muscles/femoral.png";
                    case "Gemelos":
                        return "Images/Muscles/gemelos.png";
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
