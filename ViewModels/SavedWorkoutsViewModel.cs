using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using mobileAppTest.Mappers;
using mobileAppTest.Models;
using mobileAppTest.Views.Popups;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileAppTest.ViewModels
{
    [QueryProperty("Email", "Email")]

    internal partial class SavedWorkoutsViewModel : ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty]
        private DateGroup _currentDateGroup; 

        [ObservableProperty]
        private UserModel _user;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private InfoExercisePopup _infoExercisePopup;

        [ObservableProperty]
        private ObservableCollection<DateGroup> _exerciseGrouplist;

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _exerciseSessionlist;

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _exerciseList;

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _infoEventsList;

        [ObservableProperty]
        private List<ExerciseModelView> _exercisesToDelete;

        [ObservableProperty]
        private int _miCustomContador;

        [ObservableProperty]
        private int _exerciseCount;

        [ObservableProperty]
        private bool _isDeleteCustomExerciseButtonVisible;

        [ObservableProperty]
        private bool _radialOpen;

        [ObservableProperty]
        private Color _radialColor;



        public SavedWorkoutsViewModel() 
        {
            User = new UserModel();
            ExerciseGrouplist = new ObservableCollection<DateGroup>();
            ExerciseSessionlist = new ObservableCollection<ExerciseModelView>();
            ExerciseList = new ObservableCollection<ExerciseModelView>();
            AllExercises = new ObservableCollection<ExerciseModelView>();
            InfoEventsList = new ObservableCollection<ExerciseModelView>();
            ExercisesToDelete = new List<ExerciseModelView>();
            MiCustomContador = 0;

        }



        [RelayCommand]
        private async Task test()
        {
            //Agregar series y guardar los cambios solo en favoritos o en los 2 ? 
            //los otros 3 módulos funcionan bien
        }


        [RelayCommand]
        public async Task GetUserCredentials()
        {
            var avatar = await CrossCloudFirestore.Current
                            .Instance
                            .Collection("Users")
                            .Document(Email) 
                            .GetAsync();
            User = avatar.ToObject<UserModel>();

            // Reps = User.Reps;
            // Weight = User.Weight;

        }

        [RelayCommand]
        public void AddSeriesRow(ExerciseModelView exercise)
        {
            if (exercise.Reps != null && exercise.Weight != null)
            {
                List<double> updatedRepsList = exercise.Reps.ToList();
                List<double> updatedWeightList = exercise.Weight.ToList();

                updatedRepsList.Add(10); // Valor predeterminado o entrada del usuario
                updatedWeightList.Add(12.5); // Valor predeterminado o entrada del usuario

                exercise.Reps = updatedRepsList.ToArray();
                exercise.Weight = updatedWeightList.ToArray();
            }
        }

        [RelayCommand]
        public void RemoveSeriesRow(ExerciseModelView exercise)
        {
            if (exercise.Reps != null && exercise.Weight != null && exercise.Reps.Length > 0 && exercise.Weight.Length > 0)
            {
                List<double> updatedRepsList = exercise.Reps.ToList();
                List<double> updatedWeightList = exercise.Weight.ToList();

                updatedRepsList.RemoveAt(updatedRepsList.Count - 1);
                updatedWeightList.RemoveAt(updatedWeightList.Count - 1);

                exercise.Reps = updatedRepsList.ToArray();
                exercise.Weight = updatedWeightList.ToArray();
            }
        }






        [RelayCommand]
        public async Task TestDarkMode()
        {


            if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                Application.Current.UserAppTheme = AppTheme.Light;

            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Dark;

            }

        }

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _allExercises;

        public async Task LoadCustomWorkouts()
        {
            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document("123456@gmail.com"); // Cambiar por el email del usuario

            var userSnapshot = await userDocument.GetDocumentAsync();

            if (userSnapshot.Exists)
            {
                var userData = userSnapshot.ToObject<Dictionary<string, object>>();
                if (userData.ContainsKey("Colecciones"))
                {
                    var colecciones = userData["Colecciones"] as List<object>;

                    // Diccionario para almacenar los ejercicios agrupados por título de entrenamiento
                    Dictionary<string, List<ExerciseModelView>> exercisesByWorkoutTitle = new Dictionary<string, List<ExerciseModelView>>();

                    foreach (string workoutTitle in colecciones)
                    {
                        var workoutDoc = CrossCloudFirestore.Current
                            .Instance
                            .Collection("Users")
                            .Document("123456@gmail.com")
                            .Collection("Sesiones")
                            .Document("Saved_Workouts")
                            .Collection(workoutTitle);

                        var workoutQuerySnapshot = await workoutDoc.GetDocumentsAsync();
                        foreach (var exerciseDocument in workoutQuerySnapshot.Documents)
                        {
                            var exerciseView = exerciseDocument.ToObject<ExerciseModelView>();
                            if (exerciseView.Exercise == null)
                            {
                                exerciseView.Exercise = ModelMapper.ExerciseModelViewToExerciseModel(exerciseView);
                            }


                            // Agregar el ejercicio a la lista correspondiente en el diccionario
                            if (!exercisesByWorkoutTitle.ContainsKey(workoutTitle))
                            {
                                exercisesByWorkoutTitle[workoutTitle] = new List<ExerciseModelView>();
                            }
                            exercisesByWorkoutTitle[workoutTitle].Add(exerciseView);
                            AllExercises.Add(exerciseView);

                        }
                    }


                    // Crear una lista de DateGroup para cada título de entrenamiento
                    var dateGroups = exercisesByWorkoutTitle.Select(kvp => new DateGroup
                    {
                        FechaEntrenamiento = kvp.Key,
                        Exercises = kvp.Value,
                    }).ToList();

                    // Asignar los grupos a ExerciseSessionlist
                    ExerciseGrouplist = new ObservableCollection<DateGroup>(dateGroups);
               
                }
            }

        }


        [RelayCommand]
        private async Task RepeatWorkout(DateGroup dateGroup)
        {
            ExerciseList.Clear();
            foreach (var ex in dateGroup.Exercises)
            {
                    ExerciseList.Add(ex);
            }

            await Shell.Current.GoToAsync("//StartedWorkoutPage", new Dictionary<string, object>()
            {
                ["ExerciseList"] = ExerciseList
            });

            RadialOpen = false;
            if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                RadialColor = Colors.Black;


            }
            else
            {
                RadialColor = Colors.White;

            }
        }


        [RelayCommand]
        public async void DeleteWorkout(DateGroup dateGroup)
        {
            if (dateGroup == null || string.IsNullOrEmpty(dateGroup.FechaEntrenamiento))
                return;

            bool DeleteUser = await App.Current.MainPage.DisplayAlert("Confirmación",
              "¿Estás seguro de que quieres eliminar\n '"+dateGroup.FechaEntrenamiento +"' ?", "Sí", "No");

            if (!DeleteUser)
            {
                RadialOpen = false;
                return;
            }

            var query = CrossCloudFirestore.Current
                 .Instance
                 .Collection("Users")
                 .Document("123456@gmail.com")
                 .Collection("Sesiones")
                 .Document("Saved_Workouts")
                 .Collection(dateGroup.FechaEntrenamiento);

            var querySnapshot = await query.GetDocumentsAsync();

            // Iterate through the result documents and delete them
            foreach (var document in querySnapshot.Documents)
            {
                await document.Reference.DeleteAsync();
            }

            // Remove the exercise from the local list
            ExerciseGrouplist.Remove(dateGroup);

            await DeleteNameWorkoutFromUserCollection(dateGroup);
            RadialOpen = false;
            if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                RadialColor = Colors.Black;


            }
            else
            {
                RadialColor = Colors.White;

            }


        }

        [RelayCommand]
        private async Task DeleteNameWorkoutFromUserCollection(DateGroup dateGroup)
        {
            if (dateGroup == null || string.IsNullOrEmpty(dateGroup.FechaEntrenamiento))
                return;

            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document("123456@gmail.com"); // Cambiar por el email del usuario

            // Eliminar el nombre del workout del campo Colecciones del documento del usuario
            await userDocument.UpdateDataAsync(new { Colecciones = FieldValue.ArrayRemove(dateGroup.FechaEntrenamiento) });
        }



        [RelayCommand]
        public async void DeleteCustomTrainingExercise(ExerciseModelView exercise)
        {
            ExercisesToDelete.Add(exercise);

            var query = CrossCloudFirestore.Current
                 .Instance
                 .Collection("Users")
                 .Document("123456@gmail.com")
                 .Collection("Sesiones")
                 .WhereEqualsTo("Id", exercise.Id); // Replace with the actual property and condition

            var querySnapshot = await query.GetDocumentsAsync();

            // Iterate through the result documents and delete them
            foreach (var document in querySnapshot.Documents)
            {
                await document.Reference.DeleteAsync();
            }

            // Remove the exercise from the local list
            ExerciseSessionlist.Remove(exercise);

        }

      

        [RelayCommand]
        public async Task OpenInfoExercisePopup(DateGroup dateGroup)
        {
            InfoEventsList.Clear();
            if (dateGroup == null || string.IsNullOrEmpty(dateGroup.FechaEntrenamiento))
                return;

            foreach (var ex in dateGroup.Exercises)
            {
                InfoEventsList.Add(ex);
            }

            InfoExercisePopup = new InfoExercisePopup();
            await App.Current.MainPage.ShowPopupAsync(InfoExercisePopup);

            RadialOpen = false;
            if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                RadialColor = Colors.Black;


            }
            else
            {
                RadialColor = Colors.White;

            }
        }


        [RelayCommand]
        public async Task CloseInfoExercisePopup()
        {
            InfoExercisePopup.Close();
            RadialOpen = false;
            if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                RadialColor = Colors.Black;


            }
            else
            {
                RadialColor = Colors.White;

            }

        }



        [RelayCommand]
        public async Task NavegarMainPage()
        {
           await Shell.Current.GoToAsync("//MainPage");
        }

    }
}
