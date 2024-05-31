using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using mobileAppTest.Mappers;
using mobileAppTest.Models;
using mobileAppTest.Views.Popups;
using Mopups.Services;
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

    public partial class SavedWorkoutsViewModel : ObservableObject, INotifyPropertyChanged
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
        private ObservableCollection<ExerciseModelView> _exerciseList;

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _infoEventsList;

        [ObservableProperty]
        private int _miCustomContador;

        [ObservableProperty]
        private int _exerciseCount;

        [ObservableProperty]
        private bool _isDeleteCustomExerciseButtonVisible;

        [ObservableProperty]
        private bool _radialOpen;

        [ObservableProperty]
        private bool _isShimmerPlaying;



        public SavedWorkoutsViewModel() 
        {
            User = new UserModel();
            ExerciseGrouplist = new ObservableCollection<DateGroup>();
            ExerciseList = new ObservableCollection<ExerciseModelView>();
            InfoEventsList = new ObservableCollection<ExerciseModelView>();
            MiCustomContador = 0;

        }





        public async Task StartShimmerAndWait()
        {
            IsShimmerPlaying = true;


            try
            {
                // Realiza todas las operaciones asincrónicas
                //await GetUserCredentials();
                await LoadCustomWorkouts();

            }
            finally
            {
                // Independientemente de si las operaciones son exitosas o no, detén el shimmer
                IsShimmerPlaying = false;
            }
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

        }


        public async Task LoadCustomWorkouts()
        {
            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document(Email); 

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
                            .Document(Email)
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
                ["Email"] = Email,
                ["ExerciseList"] = ExerciseList
            });

            RadialOpen = false;
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
                 .Document(Email)
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
        }


        [RelayCommand]
        private async Task DeleteNameWorkoutFromUserCollection(DateGroup dateGroup)
        {
            if (dateGroup == null || string.IsNullOrEmpty(dateGroup.FechaEntrenamiento))
                return;

            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document(Email); 

            // Eliminar el nombre del workout del campo Colecciones del documento del usuario
            await userDocument.UpdateDataAsync(new { Colecciones = FieldValue.ArrayRemove(dateGroup.FechaEntrenamiento) });
        }



        [RelayCommand]
        public async Task OpenInfoExercisePopup(DateGroup dateGroup)
        {
            RadialOpen = false;


            InfoEventsList.Clear();
            if (dateGroup == null || string.IsNullOrEmpty(dateGroup.FechaEntrenamiento))
                return;

            foreach (var ex in dateGroup.Exercises)
            {
                InfoEventsList.Add(ex);
            }

            InfoExercisePopup = new InfoExercisePopup();
            await App.Current.MainPage.ShowPopupAsync(InfoExercisePopup);

        }


        [RelayCommand]
        public async Task CloseInfoExercisePopup()
        {
            InfoExercisePopup.Close();
            RadialOpen = false;
        }


        [RelayCommand]
        public async Task NavegarMainPage()
        {
           await Shell.Current.GoToAsync("//MainPage");
        }


    }
}
