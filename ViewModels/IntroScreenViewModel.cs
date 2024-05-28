using CommunityToolkit.Mvvm.ComponentModel;
using mobileAppTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileAppTest.ViewModels
{
    public partial class IntroScreenViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<IntroScreenModel> _introScreenModels;

        public IntroScreenViewModel()
        {
            IntroScreenModels = new ObservableCollection<IntroScreenModel>();

            IntroScreenModels.Add(new IntroScreenModel()
            {
                Title = "Bienvenido a mi app"
             
            });

            IntroScreenModels.Add(new IntroScreenModel()
            {
                Title = "Credenciales",
                User = new UserModel
                {
                    Name = "Test",
                    Email = "Test"
                },
            });

            IntroScreenModels.Add(new IntroScreenModel()
            {

                EquipmentList = new ObservableCollection<EquipmentModel>
                {
                    new EquipmentModel { name = "Banco", disponible = true, url = "Images/Equipment/bancohorizontal.jpg" },
                    new EquipmentModel { name = "Banco Declinado", disponible = true, url = "Images/Equipment/bancodeclinado.jpg" },
                    
                }
            });
        }



    }
}
