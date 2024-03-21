using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileAppTest.Models
{
    public partial class EquipmentModelView : ObservableObject
    {
        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _url;

        [ObservableProperty]
        private string _fechaEntrenamiento;

        [ObservableProperty]
        private bool _isChecked;
        public EquipmentModelView()
        {
            FechaEntrenamiento = DateTime.Now.ToString("dd/MM/yyyy");
            string[] dateArray = FechaEntrenamiento.Split(' ');
            FechaEntrenamiento = dateArray[0];
        }
    }

}
