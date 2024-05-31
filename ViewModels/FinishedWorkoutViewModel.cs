using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using mobileAppTest.Models;
using mobileAppTest.Views.Popups;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace mobileAppTest.ViewModels
{
    [QueryProperty("ExerciseFinishedList", "ExerciseFinishedList")]
    [QueryProperty("Fecha", "Fecha")]
    [QueryProperty("Email", "Email")]
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
        private string _email;

        [ObservableProperty]
        private string _fecha;

        [ObservableProperty]
        private string _workoutTitle;

        [ObservableProperty]
        private string _headerTitle;

        [ObservableProperty]
        private Color _passwordTextColor;

        [ObservableProperty]
        private bool _isSaveButtonVisible;

        [ObservableProperty]
        private bool _fireworksVisible;



        public FinishedWorkoutViewModel()
        {
            ExerciseFinishedLists = new ObservableCollection<ExerciseModelView>();
            PasswordTextColor = Colors.Black;
            IsSaveButtonVisible = true;
            HeaderTitle = "Dale un título a tu entrenamiento";
        }


        [RelayCommand]
        private async void DeleteSwipedRow(ExerciseModelView rowToDelete)
        {
            ExerciseFinishedList.Remove(rowToDelete);
            if(ExerciseFinishedList.Count == 0 )
            {
                IsSaveButtonVisible = false;
            }
          
            var snackbarOptions = new SnackbarOptions
            {
                CornerRadius = 10,
                BackgroundColor = Colors.LightGray
            };
            var snackbar = Snackbar.Make($"{rowToDelete.Exercise.Name} borrado",
                () =>
                {
                    ExerciseFinishedList.Add(rowToDelete);
                    if (ExerciseFinishedList.Count > 0)
                    {
                        IsSaveButtonVisible = true;
                    }
                }, "Rehacer", TimeSpan.FromSeconds(5), snackbarOptions);

            await snackbar.Show();

         
        }


        [RelayCommand]
        private async Task SaveWorkout()
        {
            if (string.IsNullOrEmpty(WorkoutTitle) || WorkoutTitle.Trim().Length == 0 || WorkoutTitle.Trim().Length > 30)
            {
                HeaderTitle = "Título incorrecto o demasiado largo";
                await App.Current.MainPage.DisplayAlert("Error", "Título incorrecto o demasiado largo", "Aceptar");
                PasswordTextColor = Colors.Red;
                return;
            }

            HeaderTitle = "Dale un título a tu entrenamiento";


            PasswordTextColor = Colors.Black;


            // Crear una referencia al documento del usuario
            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document(Email); 
                

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
            await App.Current.MainPage.DisplayAlert("Info", "Entrenamiento guardado correctamente", "Aceptar");

            // Cerrar el popup de guardado de entrenamiento
        }
        private async Task SaveWorkoutToUserDocument()
        {
            // Crear una referencia al documento del usuario
            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document(Email);

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
                ["Email"] = Email,
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

        [RelayCommand]
        public async Task LoadSavedWorkouts()
        {
            await Shell.Current.GoToAsync("//SavedWorkoutsPage");

            ExerciseFinishedList.Clear();
            if (null == UniquePrimaryMuscles)
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
