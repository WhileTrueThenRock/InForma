using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using mobileAppTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileAppTest.ViewModels
{
    [QueryProperty("ExerciseFinishedList", "ExerciseFinishedList")]
    [QueryProperty("Fecha", "Fecha")]
    [QueryProperty("UniquePrimaryMuscles", "UniquePrimaryMuscles")]
    internal partial class FinishedWorkoutViewModel : ObservableObject
    {

        [ObservableProperty]
        public ObservableCollection<ExerciseModelView> _exerciseFinishedList;

        [ObservableProperty]
        public ObservableCollection<ExerciseModelView> _uniquePrimaryMuscles;

        [ObservableProperty]
        private string _fecha;

        public FinishedWorkoutViewModel()
        {
            
        }

        [RelayCommand]
        public async Task LoadMainPage()
        {
            ExerciseFinishedList.Clear();
            UniquePrimaryMuscles.Clear();
            await Shell.Current.GoToAsync("//MainPage");
        }


    }
}
