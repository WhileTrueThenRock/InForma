using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    [QueryProperty("ExerciseFinishedList", "ExerciseFinishedList")]
    [QueryProperty("Fecha", "Fecha")]
    [QueryProperty("UniquePrimaryMuscles", "UniquePrimaryMuscles")]
    internal partial class FinishedWorkoutViewModel : ObservableObject
    {

        [ObservableProperty]
        public ObservableCollection<ExerciseModelView> _exerciseFinishedList;
        [ObservableProperty]
        public ObservableCollection<ExerciseModelView> _exerciseFinishedLists;

        [ObservableProperty]
        public ObservableCollection<ExerciseModelView> _uniquePrimaryMuscles;

        [ObservableProperty]
        private string _fecha;

        [ObservableProperty]
        private SaveWorkoutPopup _saveWorkoutPopup;

        [ObservableProperty]
        private string _workoutTitle;

        public FinishedWorkoutViewModel()
        {
            ExerciseFinishedLists = new ObservableCollection<ExerciseModelView>();
        }

        [RelayCommand]
        private async Task SaveWorkout()
        {
            // Crear una referencia al documento del usuario
            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document("123456@gmail.com"); // Cambiar por el email del usuario

            // Iterar sobre cada ejercicio terminado y guardarlos directamente bajo la colección con el nombre proporcionado por el usuario
            foreach (var exercise in ExerciseFinishedList)
            {
                // Obtener una referencia al documento del entrenamiento con el nombre proporcionado por el usuario
                var workoutDocument = userDocument
                    .Collection("Sesiones") // Utilizar el nombre proporcionado por el usuario como nombre de la colección
                    .Document("Saved_Workouts")
                    .Collection("Saved_Workouts")
                    .Document(exercise.Exercise.Id);// Utilizar el ID del ejercicio como nombre del documento

                // Guardar el ejercicio en Firestore
                await workoutDocument.SetAsync(exercise.Exercise);
            }

            // Limpiar el título del entrenamiento
            WorkoutTitle = "";

            // Cerrar el popup de guardado de entrenamiento
            CloseSaveWorkoutPopup();
        }



        [RelayCommand]
        public async Task LoadMainPage()
        {
            ExerciseFinishedLists = new ObservableCollection<ExerciseModelView>(ExerciseFinishedList);


            await Shell.Current.GoToAsync("//MainPage", new Dictionary<string, object>()
            {
                ["ExerciseFinishedLists"] = ExerciseFinishedLists

            });

            ExerciseFinishedList.Clear();
            UniquePrimaryMuscles.Clear();
        }


        [RelayCommand]
        public async Task OpenSaveWorkoutPopup()
        {

            SaveWorkoutPopup = new SaveWorkoutPopup();
            await App.Current.MainPage.ShowPopupAsync(SaveWorkoutPopup);
        }

        [RelayCommand]
        public async Task CloseSaveWorkoutPopup()
        {
            SaveWorkoutPopup.Close();

        }


    }
}
