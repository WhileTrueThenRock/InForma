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
    [QueryProperty("UniquePrimaryMuscles", "UniquePrimaryMuscles")]
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
        private string _fecha;

        [ObservableProperty]
        private UserModel _user;

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _equipmentList;

        [ObservableProperty]
        private bool _isVisible;

        private DateTime time;

        [ObservableProperty]
        private bool _isRunning;

        [ObservableProperty]
        private string _timeLabel;

        [ObservableProperty]
        private int _exercisesLeft;

        [ObservableProperty]
        private string _exercisesLeftLabel;



        public StartedWorkoutViewModel()
        {
            ExerciseFinishedList = new ObservableCollection<ExerciseModelView>();
            UniquePrimaryMuscles = new ObservableCollection<ExerciseModelView>();
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
                            .Document("123456@gmail.com")
                            .GetAsync();
            User = avatar.ToObject<UserModel>();
            if (User.ResetTimeLabel)
            {
                IsRunning = false;
            }

        }


        [RelayCommand]
        public void StartStopwatch()
        {
            IsVisible = true;
            if (IsRunning == false)
            {
                time = new();
                TimeLabel = "00:00:00";
                IsRunning = true;

                Task.Run(async () =>
                {
                    while (IsRunning)
                    {
                        // Esperar 1 segundo
                        await Task.Delay(1000);

                        // Actualizar el tiempo y la etiqueta de tiempo en el hilo de la interfaz de usuario
                        UpdateUI();
                    }
                });
            }
        }

        [RelayCommand]
        public void StopStopwatch()
        {
            time = new();
            TimeLabel = "00:00:00";
            IsRunning = false;

        }


        private void UpdateUI()
        {
            time = time.Add(TimeSpan.FromSeconds(1));
            TimeLabel = time.TimeOfDay.ToString();
        }


        [RelayCommand]
        public async Task NavegarMainPage()
        {

            time = new();
                    TimeLabel = "00:00:00";
            IsRunning = false;
            if(null == ExerciseFinishedList)
            {
                await Shell.Current.GoToAsync("//MainPage");
                return;
            }

            if (ExerciseFinishedList.Count==0) 
            {
                TimeLabel = "00:00:00";
                await Shell.Current.GoToAsync("//MainPage");
            }


            if (ExerciseFinishedList.Count>0) //Si el usuario ha registrado al menos una serie que siga 
            {
                TimeLabel = "00:00:00";
                await Shell.Current.GoToAsync("//FinishedWorkoutPage", new Dictionary<string, object>()
                {
                    ["ExerciseFinishedList"] = ExerciseFinishedList,
                    ["Fecha"] = Fecha,
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
                ["ExerciseList"] = ExerciseList

            });
        }

    }
}
