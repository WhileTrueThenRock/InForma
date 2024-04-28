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
                    case "Banco ":
                        return "Images/bancohorizontal.jpg";
                    case "Banco Declinado":
                        return "Images/bancodeclinado.jpg";
                    case "Banco Inclinado":
                        return "Images/bancoinclinado.jpg";
                    case "Banco Romano":
                        return "Images/bancoromano.jpg";
                    case "Banco Scott":
                        return "Images/bancoscott.jpg";
                    case "Barras":
                        return "Images/landmine.jpg";
                    case "Barra T":
                        return "Images/barras.jpg";
                    case "Barra Z":
                        return "Images/barraz.jpg";
                    case "Rueda Abdominal":
                        return "Images/ruedaabd.jpg";
                    case "Sin Equipamiento":
                        return "Images/sinequipamiento.png";
                    case "Soga":
                        return "Images/soga.jpg";
                    case "Soporte Dominadas":
                        return "Images/dominadas.jpg";
                    case "Mancuernas":
                        return "Images/dumbbells.jpg";
                    case "Máquinas Convergentes":
                        return "Images/maquinaconvergente.jpg";
                    case "Máquina de Poleas":
                        return "Images/maquinapoleas.jpg";
                    case "Máquina Scott":
                        return "Images/maquinascottplacas.jpg";
                    case "Máquina Patada Glúteos":
                        return "Images/maquinapatada.jpg";
                    case "Máquina Abductores y Aductores":
                        return "Images/maquinaaductores.jpg";
                    case "Máquina Trapecio":
                        return "Images/maquinatrapecio.jpg";
                    case "Máquina Hip Thrust":
                        return "Images/maquinahip.jpg";
                    case "Máquina Hombros Press":
                        return "Images/maquinahombrospress.jpg";
                    case "Máquina Hombros Laterales":
                        return "Images/maquinahombroslat.jpg";
                    case "Máquina para gemelos de pie":
                        return "Images/maquinagemelospie.jpg";
                    case "Máquina para gemelos Tipo Burro":
                        return "Images/maquinaburro.jpg";
                    case "Máquina Multipower":
                        return "Images/maquinamulti.jpg";
                    case "Máquina Aperturas":
                        return "Images/maquinaaperturas.jpg";
                    case "Máquina Remo Alto":
                        return "Images/maquinaremoalto.jpg";
                    case "Máquina Remo":
                        return "Images/maquinaremo.jpg";
                    case "Máquina Remo Bajo":
                        return "Images/maquinaremobajo.jpg";
                    case "Máquina Fondos":
                        return "Images/maquinafondos.jpg";
                    case "Máquina Asistida":
                        return "Images/maquinaasistida.jpg";
                    case "Máquina Sentadillas":
                        return "Images/maquinasentadillas.jpg";
                    case "Máquina Femoral Tumbado":
                        return "Images/maquinafemoraltumbado.jpg";
                    case "Máquina Femoral Sentado":
                        return "Images/maquinafemoralsentado.jpg";
                    case "Máquina Femoral de Pie":
                        return "Images/maquinafemoraldepie.jpg";
                    case "Máquina Extensiones Cuádriceps":
                        return "Images/maquinaquads.jpg";
                    case "Máquina Press Pierna":
                        return "Images/maquinaprensa.jpg";
                    case "Máquina Press Pierna Horizontal":
                        return "Images/maquinapresshoriz.jpg";
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
