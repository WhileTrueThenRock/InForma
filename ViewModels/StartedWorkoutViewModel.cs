using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using mobileAppTest.Mappers;
using mobileAppTest.Models;
using mobileAppTest.Views.Popups;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileAppTest.ViewModels
{
    [QueryProperty("ExerciseList", "ExerciseList")]
    [QueryProperty("ExerciseFinishedList", "ExerciseFinishedList")]
    [QueryProperty("Fecha", "Fecha")]
    [QueryProperty("Email", "Email")]
    [QueryProperty("UniquePrimaryMuscles", "UniquePrimaryMuscles")]
    [QueryProperty("SelectedExercises", "SelectedExercises")]
    // [QueryProperty("EquipmentList", "EquipmentList")]
    internal partial class StartedWorkoutViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _exerciseList;

        [ObservableProperty]
        public ObservableCollection<ExerciseModelView> _exerciseFinishedList;

        [ObservableProperty]
        public ObservableCollection<ExerciseModelView> _uniquePrimaryMuscles;

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _selectedExercises;

        [ObservableProperty]
        private string _fecha;

        [ObservableProperty]
        private UserModel _user;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _equipmentList;

        [ObservableProperty]
        private bool _isVisible;

        [ObservableProperty]
        private int _exercisesLeft;

        [ObservableProperty]
        private string _exercisesLeftLabel;

        public StartedWorkoutViewModel()
        {
            ExerciseFinishedList = new ObservableCollection<ExerciseModelView>();
            UniquePrimaryMuscles = new ObservableCollection<ExerciseModelView>();
            SelectedExercises = new ObservableCollection<ExerciseModelView>();
            IsVisible = false;

        }

        [RelayCommand]
        public async Task GetExerciseCount()
        {
        var nr = ExerciseList.Count;
            ExercisesLeft = nr;
            if(ExercisesLeft > 1)
            {
                ExercisesLeftLabel = "Te quedan " + ExercisesLeft + " ejercicios";
                nr--;

            }
            else
            {
                ExercisesLeftLabel = "Te queda " + ExercisesLeft + " ejercicio";
                nr--;
            }

        }



        [RelayCommand]
        public async Task GetUserInfo()
        {

            var avatar = await CrossCloudFirestore.Current //Test video
                            .Instance
                            .Collection("Users")
                            .Document(Email)
                            .GetAsync();
            User = avatar.ToObject<UserModel>();

        }


        [RelayCommand]
        public async Task NavegarMainPage()
        {

            if(null == ExerciseFinishedList)
            {
                await Shell.Current.GoToAsync("//MainPage", new Dictionary<string, object>()
                {
                    ["Email"] = Email

                });
                return;
            }

            if (ExerciseFinishedList.Count==0) 
            {
                await Shell.Current.GoToAsync("//MainPage", new Dictionary<string, object>()
                {
                    ["Email"] = Email

                });
            }


            if (ExerciseFinishedList.Count>0) //Si el usuario ha registrado al menos una serie que siga 
            {
                await Shell.Current.GoToAsync("//FinishedWorkoutPage", new Dictionary<string, object>()
                {
                    ["ExerciseFinishedList"] = ExerciseFinishedList,
                    ["Fecha"] = Fecha,
                    ["Email"] = Email,
                    ["UniquePrimaryMuscles"] = UniquePrimaryMuscles

                });
            }
         
        }

        [RelayCommand]
        public async Task LoadTappedExercise(ExerciseModel selectedExercise)
        {


            IsVisible = true;
            await Shell.Current.GoToAsync("//ExerciseTappedPage", new Dictionary<string, object>()
            {

                ["MyExercise"] = selectedExercise,
                ["IsVisible"] = IsVisible,
                ["Email"] = Email,
                ["ExerciseList"] = ExerciseList

            });
        }

    }
}
