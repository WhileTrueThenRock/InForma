using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileAppTest.ViewModels
{
    public partial class InfoMuscleMopupViewModel : ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<string> _imagesPathList;

        public InfoMuscleMopupViewModel() 
        {
            ImagesPathList = new ObservableCollection<string>();
            ImagesPathList = ["body.png","biceps.png","cuadriceps.png","pecho.png","trapecio.png"];

        }


        public void SetMuscleImagePathList(ObservableCollection<string> imagesPathList)
        {
            ImagesPathList = imagesPathList;
        }

        [RelayCommand]
        public async Task CloseInfoMopup()
        {
            await MopupService.Instance.PopAllAsync();
        }

    }
}
