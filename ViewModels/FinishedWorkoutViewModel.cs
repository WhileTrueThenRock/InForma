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
using System.Windows.Input;
using System.Xml.Linq;

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
        private string _workoutTitle;



        public FinishedWorkoutViewModel()
        {
            ExerciseFinishedLists = new ObservableCollection<ExerciseModelView>();
        }

        [RelayCommand]
        private async Task SaveWorkout()
        {
            if (WorkoutTitle=="" || WorkoutTitle == null || WorkoutTitle.Any(Char.IsWhiteSpace))
            {
                return;
            }
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
                    .Collection(WorkoutTitle)
                    .Document(exercise.Exercise.Id);// Utilizar el ID del ejercicio como nombre del documento

                // Guardar el ejercicio en Firestore
                await workoutDocument.SetAsync(exercise.Exercise);
            }

            // Limpiar el título del entrenamiento
            await SaveWorkoutToUserDocument();
            WorkoutTitle = "";

            // Cerrar el popup de guardado de entrenamiento
        }
        private async Task SaveWorkoutToUserDocument()
        {
            // Crear una referencia al documento del usuario
            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document("123456@gmail.com"); // Cambiar por el email del usuario

            // Crear un objeto que represente el workout a guardar
            var workout = new
            {
                Name = WorkoutTitle,
                Exercises = ExerciseFinishedList.Select(exercise => exercise.Exercise.Id).ToList()
            };

            // Agregar el nombre del workout al campo Colecciones del documento del usuario
            await userDocument.UpdateDataAsync(new { Colecciones = FieldValue.ArrayUnion(WorkoutTitle) });

            // Guardar el workout como un documento en Firestore                                //BETA
            //await userDocument.Collection("Saved_Workouts").Document(WorkoutTitle).SetAsync(workout);
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
            if(null == UniquePrimaryMuscles)
            {
                return;
            }
            else
            {
                UniquePrimaryMuscles.Clear();
            }
        }


    }
}
