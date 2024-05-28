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
                    case "Banco":
                        return "Images/Equipment/bancohorizontal.jpg";
                    case "Banco Declinado":
                        return "Images/Equipment/bancodeclinado.jpg";
                    case "Banco Inclinado":
                        return "Images/Equipment/bancoinclinado.jpg";
                    case "Banco Romano":
                        return "Images/Equipment/bancoromano.jpg";
                    case "Banco Scott":
                        return "Images/Equipment/bancoscott.jpg";
                    case "Barras":
                        return "Images/Equipment/barras.jpg";
                    case "Barra T":
                        return "Images/Equipment/landmine.jpg";
                    case "Barra Z":
                        return "Images/Equipment/barraz.jpg";
                    case "Rueda Abdominal":
                        return "Images/Equipment/ruedaabd.jpg";
                    case "Sin Equipamiento":
                        return "Images/Equipment/sinequipamiento.png";
                    case "Soga":
                        return "Images/Equipment/soga.jpg";
                    case "Soporte Dominadas":
                        return "Images/Equipment/dominadas.jpg";
                    case "Mancuernas":
                        return "Images/Equipment/dumbbells.jpg";
                    case "Máquinas Convergentes":
                        return "Images/Equipment/maquinaconvergente.jpg";
                    case "Máquina de Poleas":
                        return "Images/Equipment/maquinapoleas.jpg";
                    case "Máquina Scott":
                        return "Images/Equipment/maquinascottplacas.jpg";
                    case "Máquina Patada Glúteos":
                        return "Images/Equipment/maquinapatada.jpg";
                    case "Máquina Abductores y Aductores":
                        return "Images/Equipment/maquinaaductores.jpg";
                    case "Máquina Trapecio":
                        return "Images/Equipment/maquinatrapecio.jpg";
                    case "Máquina Hip Thrust":
                        return "Images/Equipment/maquinahip.jpg";
                    case "Máquina Hombros Press":
                        return "Images/Equipment/maquinahombrospress.jpg";
                    case "Máquina Hombros Laterales":
                        return "Images/Equipment/maquinahombroslat.jpg";
                    case "Máquina para gemelos de pie":
                        return "Images/Equipment/maquinagemelospie.jpg";
                    case "Máquina para gemelos Tipo Burro":
                        return "Images/Equipment/maquinaburro.jpg";
                    case "Máquina Multipower":
                        return "Images/Equipment/maquinamulti.jpg";
                    case "Máquina Aperturas":
                        return "Images/Equipment/maquinaaperturas.jpg";
                    case "Máquina Remo Alto":
                        return "Images/Equipment/maquinaremoalto.jpg";
                    case "Máquina Remo":
                        return "Images/Equipment/maquinaremo.jpg";
                    case "Máquina Remo Bajo":
                        return "Images/Equipment/maquinaremobajo.jpg";
                    case "Máquina Fondos":
                        return "Images/Equipment/maquinafondos.jpg";
                    case "Máquina Asistida":
                        return "Images/Equipment/maquinaasistida.jpg";
                    case "Máquina Sentadillas":
                        return "Images/Equipment/maquinasentadillas.jpg";
                    case "Máquina Femoral Tumbado":
                        return "Images/Equipment/maquinafemoraltumbado.jpg";
                    case "Máquina Femoral Sentado":
                        return "Images/Equipment/maquinafemoralsentado.jpg";
                    case "Máquina Femoral de Pie":
                        return "Images/Equipment/maquinafemoraldepie.jpg";
                    case "Máquina Extensiones Cuádriceps":
                        return "Images/Equipment/maquinaquads.jpg";
                    case "Máquina Press Pierna":
                        return "Images/Equipment/maquinaprensa.jpg";
                    case "Máquina Press Pierna Horizontal":
                        return "Images/Equipment/maquinapresshoriz.jpg";
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
