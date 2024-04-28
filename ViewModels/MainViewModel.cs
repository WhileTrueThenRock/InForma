using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Storage;
using mobileAppTest.Mappers;
using mobileAppTest.Models;
using mobileAppTest.Services;
using mobileAppTest.Views.Popups;
using Mopups.Services;
using Plugin.CloudFirestore;
using Plugin.Maui.Calendar.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using Syncfusion.Maui.Accordion;
using CommunityToolkit.Maui.Alerts;
using System.Diagnostics;
using Firebase.Auth;

/*
 * 
SfNumericEntry for age/height/weight               
Firebase : Explore extensions :Delete User Data     
SfEffectsView                                       
SfParallaxView
SfSwitch
SfTextInputLayout
*
*/
namespace mobileAppTest.ViewModels
{
    [QueryProperty("ExerciseFinishedLists", "ExerciseFinishedLists")]
    internal partial class MainViewModel : ObservableObject
    {

        #region Popups
        [ObservableProperty]
        private CustomExercises _customExercisePopup;

        [ObservableProperty]
        private EquipmentPopup _equipmentPopup;

        [ObservableProperty]
        private MuscleSelectionPopup _musclePopup;

        [ObservableProperty]
        private MuscleReplacementPopup _muscleReplacementPopup;

        [ObservableProperty]
        private InfoExercisePopup _infoExercisePopup;
        #endregion

        [ObservableProperty]
        private UserModel _user;

        [ObservableProperty]
        private ExerciseModel _exercise;

        [ObservableProperty]
        private ExerciseModelView _exerciseView;

        [ObservableProperty]
        private CultureInfo culture;

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private int _exerciseCount;

        [ObservableProperty]
        private int _miContador;

        [ObservableProperty]
        private string _durationHeader;

        [ObservableProperty]
        private string _muscleHeader;

        [ObservableProperty]
        private bool _durationExpander;

        [ObservableProperty]
        private bool _isDurationChecked;

        [ObservableProperty]
        private bool _muscleExpander;

        [ObservableProperty]
        private bool _isVisible;

        [ObservableProperty]
        private bool _isDeleteExerciseButtonVisible;

        [ObservableProperty]
        private bool _isCustomExerciseButtonVisible;

        [ObservableProperty]
        private bool _isDetailsExerciseButtonVisible;

        [ObservableProperty]
        private bool _isBackButtonVisible;




        //Muscle  Related
        #region MuscleRegion

        [ObservableProperty]
        private bool _isFiltrarMusclesButtonVisible;

        [ObservableProperty]
        private bool _isFrontBodyVisible;

        [ObservableProperty]
        private bool _isBackBodyVisible;

        [ObservableProperty]
        private bool _isNeckSelected;

        [ObservableProperty]
        private bool _isChestSelected;

        [ObservableProperty]
        private bool _isLeftShoulderSelected;

        [ObservableProperty]
        private bool _isRightShoulderSelected;

        [ObservableProperty]
        private bool _isLeftBicepSelected;

        [ObservableProperty]
        private bool _isRightBicepSelected;

        [ObservableProperty]
        private bool _isAbsSelected;

        [ObservableProperty]
        private bool _isLeftForearmSelected;

        [ObservableProperty]
        private bool _isRightForearmSelected;

        [ObservableProperty]
        private bool _isLeftAdductorSelected;

        [ObservableProperty]
        private bool _isRightAdductorSelected;

        [ObservableProperty]
        private bool _isLeftAbductorSelected;

        [ObservableProperty]
        private bool _isRightAbductorSelected;

        [ObservableProperty]
        private bool _isLeftQuadSelected;

        [ObservableProperty]
        private bool _isRightQuadSelected;

        [ObservableProperty]
        private bool _isTrapSelected;

        [ObservableProperty]
        private bool _isLeftLatSelected;

        [ObservableProperty]
        private bool _isRightLatSelected;

        [ObservableProperty]
        private bool _isLeftTricepSelected;

        [ObservableProperty]
        private bool _isRightTricepSelected;

        [ObservableProperty]
        private bool _isGlutesSelected;

        [ObservableProperty]
        private bool _isHamstringsSelected;

        [ObservableProperty]
        private bool _isCalfsSelected;

        [ObservableProperty]
        private bool _isNeckVisible;

        [ObservableProperty]
        private bool _isChestVisible;

        [ObservableProperty]
        private bool _isLeftShoulderVisible;

        [ObservableProperty]
        private bool _isRightShoulderVisible;

        [ObservableProperty]
        private bool _isLeftBicepVisible;

        [ObservableProperty]
        private bool _isRightBicepVisible;

        [ObservableProperty]
        private bool _isAbsVisible;

        [ObservableProperty]
        private bool _isLeftForearmVisible;

        [ObservableProperty]
        private bool _isRightForearmVisible;

        [ObservableProperty]
        private bool _isLeftAdductorVisible;

        [ObservableProperty]
        private bool _isRightAdductorVisible;

        [ObservableProperty]
        private bool _isLeftAbductorVisible;

        [ObservableProperty]
        private bool _isRightAbductorVisible;

        [ObservableProperty]
        private bool _isLeftQuadVisible;

        [ObservableProperty]
        private bool _isRightQuadVisible;

        [ObservableProperty]
        private bool _isTrapVisible;

        [ObservableProperty]
        private bool _isLeftLatVisible;

        [ObservableProperty]
        private bool _isRightLatVisible;

        [ObservableProperty]
        private bool _isLeftTricepVisible;

        [ObservableProperty]
        private bool _isRightTricepVisible;

        [ObservableProperty]
        private bool _isGlutesVisible;

        [ObservableProperty]
        private bool _isHamstringsVisible;

        [ObservableProperty]
        private bool _isCalfsVisible;

        [ObservableProperty]
        private string _neckColor;

        [ObservableProperty]
        private string _chestColor;

        [ObservableProperty]
        private string _leftShoulderColor;

        [ObservableProperty]
        private string _rightShoulderColor;

        [ObservableProperty]
        private string _leftBicepColor;

        [ObservableProperty]
        private string _rightBicepColor;

        [ObservableProperty]
        private string _absColor;

        [ObservableProperty]
        private string _leftForearmColor;

        [ObservableProperty]
        private string _rightForearmColor;

        [ObservableProperty]
        private string _leftAdductorColor;

        [ObservableProperty]
        private string _rightAdductorColor;

        [ObservableProperty]
        private string _leftAbductorColor;

        [ObservableProperty]
        private string _rightAbductorColor;

        [ObservableProperty]
        private string _leftQuadColor;

        [ObservableProperty]
        private string _rightQuadColor;

        [ObservableProperty]
        private string _trapColor;

        [ObservableProperty]
        private string _leftLatColor;

        [ObservableProperty]
        private string _rightLatColor;

        [ObservableProperty]
        private string _leftTricepColor;

        [ObservableProperty]
        private string _rightTricepColor;

        [ObservableProperty]
        private string _glutesColor;

        [ObservableProperty]
        private string _hamstringsColor;

        [ObservableProperty]
        private string _calfsColor;

        #endregion

        [ObservableProperty]
        private EventCollection events;

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> eventsList;  //Para el punto rojo del calendar.

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _infoEventsList;

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> exerciseList;

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _exerciseSessionlist;

        [ObservableProperty]
        private ObservableCollection<DateGroup> _exerciseGrouplist;

        [ObservableProperty]
        public ObservableCollection<ExerciseModelView> _exerciseFinishedLists;

        [ObservableProperty]
        private List<ExerciseModelView> _exercisesToDelete;

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _muscleExerciselist;

        [ObservableProperty]
        private ObservableCollection<EquipmentModelView> equipmentList;

        [ObservableProperty]
        private ObservableCollection<EquipmentModelView> selectedEquipment;

        [ObservableProperty]
        private ObservableCollection<string> selectedMuscles;

        [ObservableProperty]
        private string _avatarImage;

        [ObservableProperty]
        private string totalWorkoutDurationLabel;

        [ObservableProperty]
        private Color _exerciseListColor;

        [ObservableProperty]
        private bool _isExerciseFinished;

        [ObservableProperty]
        private bool _isAccordionVisible;

        [ObservableProperty]
        private bool _isShimmerPlaying;

        public MainViewModel()
        {
            IsShimmerPlaying = true;
            InitComponents();
            ShowName();
            GetProfilePic();
            //ShowAllExercises();
            //GetSessionExercises();
            //RealTimeUpdateFilter();
            GetExercisesEvents();
            LoadUserEquipments();
            LoadCustomWorkouts();
            //ShowExercisesWithAvailableEquipment();
            //DurationHeader = "15 min";
            // IsDurationChecked = true;
            MuscleHeader = "Cuerpo Entero";
            // MuscleExpander = false;
            SelectDurationWorkout("15");
            //FilterBySelectedMuscle();
            //IsShimmerPlaying = true;
        }

        public async Task StartShimmer()
        {
            IsShimmerPlaying = true;
            await Task.Delay(3000);
            IsShimmerPlaying = false;
        }

        private void InitComponents()
        {
            User = new UserModel();
            Exercise = new ExerciseModel();
            ExerciseView = new ExerciseModelView();
            Events = new EventCollection();
            Culture = CultureInfo.CurrentCulture;
            EventsList = new ObservableCollection<ExerciseModelView>();
            InfoEventsList = new ObservableCollection<ExerciseModelView>();
            ExerciseSessionlist = new ObservableCollection<ExerciseModelView>();
            ExerciseGrouplist = new ObservableCollection<DateGroup>();
            ExercisesToDelete = new List<ExerciseModelView>();
            ExerciseList = new ObservableCollection<ExerciseModelView>();
            EquipmentList = new ObservableCollection<EquipmentModelView>();
            SelectedEquipment = new ObservableCollection<EquipmentModelView>();
            SelectedMuscles = new ObservableCollection<string>();
            MuscleExerciselist = new ObservableCollection<ExerciseModelView>();

            IsFiltrarMusclesButtonVisible = true;
            IsFrontBodyVisible = true;
            IsBackBodyVisible = false;
            IsNeckVisible = true;
            IsChestVisible = true;
            IsLeftShoulderVisible = true;
            IsRightShoulderVisible = true;
            IsLeftBicepVisible = true;
            IsRightBicepVisible = true;
            IsAbsVisible = true;
            IsLeftForearmVisible = true;
            IsRightForearmVisible = true;
            IsLeftAdductorVisible = true;
            IsRightAdductorVisible = true;
            IsLeftAbductorVisible = true;
            IsRightAbductorVisible = true;
            IsLeftQuadVisible = true;
            IsRightQuadVisible = true;
            IsTrapVisible = false;
            IsLeftLatVisible = false;
            IsRightLatVisible = false;
            IsLeftTricepVisible = false;
            IsRightTricepVisible = false;
            IsGlutesVisible = false;
            IsHamstringsVisible = false;
            IsCalfsVisible = false;

            IsCustomExerciseButtonVisible = false;
            IsAccordionVisible = false;
            MiContador = 0;

            ExerciseListColor = Colors.Black;
            ChestColor = "pecho2.png";
            IsChestSelected = true;
            LeftShoulderColor = "hombroizq2.png";
            RightShoulderColor = "hombroder2.png";
            IsRightShoulderSelected = true;
            SelectedMuscles.Add("Pecho");
            SelectedMuscles.Add("Hombro");
      
        }


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
                            var exercise = exerciseDocument.ToObject<ExerciseModelView>();

                            // Agregar el ejercicio a la lista correspondiente en el diccionario
                            if (!exercisesByWorkoutTitle.ContainsKey(workoutTitle))
                            {
                                exercisesByWorkoutTitle[workoutTitle] = new List<ExerciseModelView>();
                            }
                            exercisesByWorkoutTitle[workoutTitle].Add(exercise);
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

                    IsAccordionVisible = true;
                }
            }
        }

















        [RelayCommand]
        public async Task ChangeFinishedExercisesColor()
        {
            if (ExerciseFinishedLists.Count == 0)
            {
                return;
            }

            //var matchingExercises = ExerciseList.Intersect(ExerciseFinishedLists).ToList();
            ExerciseList.Clear();
            ExerciseCount = 0;

            foreach (var exercise in ExerciseFinishedLists)
            {
                if (exercise == null)
                {
                    continue;
                }
                ExerciseList.Add(exercise);

                if (exercise.Exercise.Name.All(ex => ExerciseList.Contains(exercise)))
                {
                    ExerciseList.Add(exercise);
                    ExerciseCount++;
                    //ExerciseListColor = Colors.LightGreen;

                    // Si el ejercicio está terminado, establece la propiedad IsFinished en true
                    IsExerciseFinished = true;
                }
            }
        }



        [RelayCommand]
        private async void DeleteSwipedRow(ExerciseModelView rowToDelete)
        {
            ExerciseList.Remove(rowToDelete);
            ExerciseCount--;
            var snackbarOptions = new SnackbarOptions
            {
                CornerRadius = 10,
                BackgroundColor = Colors.LightGray
            };
            var snackbar = Snackbar.Make($"'{rowToDelete.Name}' borrado",
                () =>
                {
                    ExerciseList.Add(rowToDelete);
                    ExerciseCount++;
                },"Rehacer",TimeSpan.FromSeconds(5), snackbarOptions);
            await snackbar.Show();
        }


        [RelayCommand]
        public async void DeleteCustomTrainingExercise(ExerciseModelView exercise)
        {
            ExerciseCount--;
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
        private async void ReplaceSwipedRow(ExerciseModelView exercise)
        {
            OpenMuscleReplacementPopup(exercise);
            ExerciseList.Remove(exercise);
        }

        [RelayCommand]
        public async Task ChangeMuscleImage(string muscle)
        {

            IsFiltrarMusclesButtonVisible = true;

            if (muscle.Contains("Pecho"))
            {
                IsChestSelected = !IsChestSelected;

                if (IsChestSelected)
                {
                    ChestColor = "pecho2.png";
                    SelectedMuscles.Add(muscle);
                }

                else
                {
                    ChestColor = "pecho1.png";
                    SelectedMuscles.Remove(muscle);
                }

            }

            if (muscle.Contains("Cuello"))
            {
                IsNeckSelected = !IsNeckSelected;

                if (IsNeckSelected)
                {
                    NeckColor = "cuello2.png";
                    SelectedMuscles.Add(muscle);
                }
                else
                {
                    NeckColor = "cuello1.png";
                    SelectedMuscles.Remove(muscle);
                }
            }

            if (muscle.Contains("Hombro"))
            {
                IsRightShoulderSelected = !IsRightShoulderSelected;

                if (IsRightShoulderSelected)
                {
                    LeftShoulderColor = "hombroizq2.png";
                    RightShoulderColor = "hombroder2.png";
                    SelectedMuscles.Add(muscle);
                }

                else
                {
                    LeftShoulderColor = "hombroizq1.png";
                    RightShoulderColor = "hombroder1.png";
                    SelectedMuscles.Remove(muscle);
                }

            }

            if (muscle.Contains("Bíceps"))
            {
                IsRightBicepSelected = !IsRightBicepSelected;

                if (IsRightBicepSelected)
                {
                    LeftBicepColor = "leftbicep2.png";
                    RightBicepColor = "rightbicep2.png";
                    SelectedMuscles.Add(muscle);
                }

                else
                {
                    LeftBicepColor = "leftbicep1.png";
                    RightBicepColor = "rightbicep1.png";
                    SelectedMuscles.Remove(muscle);
                }

            }

            if (muscle.Contains("Abdomen"))
            {
                IsAbsSelected = !IsAbsSelected;

                if (IsAbsSelected)
                {
                    AbsColor = "abs2.png";
                    SelectedMuscles.Add(muscle);
                }
                else
                {
                    AbsColor = "abs1.png";
                    SelectedMuscles.Remove(muscle);
                }
            }

            if (muscle.Contains("Antebrazo"))
            {
                IsRightForearmSelected = !IsRightForearmSelected;

                if (IsRightForearmSelected)
                {
                    LeftForearmColor = "leftforearm2.png";
                    RightForearmColor = "rightforearm2.png";
                    SelectedMuscles.Add(muscle);
                }

                else
                {
                    LeftForearmColor = "leftforearm1.png";
                    RightForearmColor = "rightforearm1.png";
                    SelectedMuscles.Remove(muscle);
                }

            }

            if (muscle.Contains("Aductores"))
            {
                IsRightAdductorSelected = !IsRightAdductorSelected;

                if (IsRightAdductorSelected)
                {
                    LeftAdductorColor = "leftadductor2.png";
                    RightAdductorColor = "rightadductor2.png";
                    SelectedMuscles.Add(muscle);
                }

                else
                {
                    LeftAdductorColor = "leftadductor1.png";
                    RightAdductorColor = "rightadductor1.png";
                    SelectedMuscles.Remove(muscle);
                }

            }

            if (muscle.Contains("Abductores"))
            {
                IsRightAbductorSelected = !IsRightAbductorSelected;

                if (IsRightAbductorSelected)
                {
                    LeftAbductorColor = "leftabductor2.png";
                    RightAbductorColor = "rightabductor2.png";
                    SelectedMuscles.Add(muscle);
                }

                else
                {
                    LeftAbductorColor = "leftabductor1.png";
                    RightAbductorColor = "rightabductor1.png";
                    SelectedMuscles.Remove(muscle);
                }

            }


            if (muscle.Contains("Cuádriceps"))
            {
                IsRightQuadSelected = !IsRightQuadSelected;

                if (IsRightQuadSelected)
                {
                    LeftQuadColor = "leftquad2.png";
                    RightQuadColor = "rightquad2.png";
                    SelectedMuscles.Add(muscle);
                }
                else
                {
                    LeftQuadColor = "leftquad1.png";
                    RightQuadColor = "rightquad1.png";
                    SelectedMuscles.Remove(muscle);
                }
            }

            if (muscle.Contains("Trapecio"))
            {
                IsTrapSelected = !IsTrapSelected;

                if (IsTrapSelected)
                {
                    TrapColor = "trap2.png";
                    SelectedMuscles.Add(muscle);
                }
                else
                {
                    TrapColor = "trap1.png";
                    SelectedMuscles.Remove(muscle);
                }
            }

            if (muscle.Contains("Espalda"))
            {
                IsRightLatSelected = !IsRightLatSelected;

                if (IsRightLatSelected)
                {
                    LeftLatColor = "leftlat2.png";
                    RightLatColor = "rightlat2.png";
                    SelectedMuscles.Add(muscle);
                }
                else
                {
                    LeftLatColor = "leftlat1.png";
                    RightLatColor = "rightlat1.png";
                    SelectedMuscles.Remove(muscle);
                }
            }

            if (muscle.Contains("Tríceps"))
            {
                IsRightTricepSelected = !IsRightTricepSelected;

                if (IsRightTricepSelected)
                {
                    LeftTricepColor = "lefttricep2.png";
                    RightTricepColor = "righttricep2.png";
                    SelectedMuscles.Add(muscle);
                }
                else
                {
                    LeftTricepColor = "lefttricep1.png";
                    RightTricepColor = "righttricep1.png";
                    SelectedMuscles.Remove(muscle);
                }
            }

            if (muscle.Contains("Glúteos"))
            {
                IsGlutesSelected = !IsGlutesSelected;

                if (IsGlutesSelected)
                {
                    GlutesColor = "glutes2.png";
                    SelectedMuscles.Add(muscle);
                }
                else
                {
                    GlutesColor = "glutes1.png";
                    SelectedMuscles.Remove(muscle);
                }
            }

            if (muscle.Contains("Femoral"))
            {
                IsHamstringsSelected = !IsHamstringsSelected;

                if (IsHamstringsSelected)
                {
                    HamstringsColor = "hamstrings2.png";
                    SelectedMuscles.Add(muscle);
                }
                else
                {
                    HamstringsColor = "hamstrings1.png";
                    SelectedMuscles.Remove(muscle);
                }
            }

            if (muscle.Contains("Gemelos"))
            {
                IsCalfsSelected = !IsCalfsSelected;

                if (IsCalfsSelected)
                {
                    CalfsColor = "calfs2.png";
                    SelectedMuscles.Add(muscle);
                }
                else
                {
                    CalfsColor = "calfs1.png";
                    SelectedMuscles.Remove(muscle);
                }
            }

            if (SelectedMuscles.Count == 0)
            {
                IsFiltrarMusclesButtonVisible = false;
            }

        }

        [RelayCommand]
        public async Task RotateBody()
        {
            if (!IsFrontBodyVisible)
            {
                IsNeckVisible = true;
                IsChestVisible = true;
                IsLeftShoulderVisible = true;
                IsRightShoulderVisible = true;
                IsLeftBicepVisible = true;
                IsRightBicepVisible = true;
                IsAbsVisible = true;
                IsLeftForearmVisible = true;
                IsRightForearmVisible = true;
                IsLeftAdductorVisible = true;
                IsRightAdductorVisible = true;
                IsLeftAbductorVisible = true;
                IsRightAbductorVisible = true;
                IsLeftQuadVisible = true;
                IsRightQuadVisible = true;
                IsTrapVisible = false;
                IsLeftLatVisible = false;
                IsRightLatVisible = false;
                IsLeftTricepVisible = false;
                IsRightTricepVisible = false;
                IsGlutesVisible = false;
                IsHamstringsVisible = false;
                IsCalfsVisible = false;
            }
            else
            {
                IsNeckVisible = false;
                IsChestVisible = false;
                IsLeftShoulderVisible = false;
                IsRightShoulderVisible = false;
                IsLeftBicepVisible = false;
                IsRightBicepVisible = false;
                IsAbsVisible = false;
                IsLeftForearmVisible = false;
                IsRightForearmVisible = false;
                IsLeftAdductorVisible = false;
                IsRightAdductorVisible = false;
                IsLeftAbductorVisible = false;
                IsRightAbductorVisible = false;
                IsLeftQuadVisible = false;
                IsRightQuadVisible = false;
                IsTrapVisible = true;
                IsLeftLatVisible = true;
                IsRightLatVisible = true;
                IsLeftTricepVisible = true;
                IsRightTricepVisible = true;
                IsGlutesVisible = true;
                IsHamstringsVisible = true;
                IsCalfsVisible = true;
            }

            IsFrontBodyVisible = !IsFrontBodyVisible;
            IsBackBodyVisible = !IsBackBodyVisible;
        }

        [RelayCommand]
        public async Task FilterBySelectedMuscle()
        {


            await ShowExercisesWithAvailableEquipment();

            var filteredByMuscle = ExerciseList.Where(exercise => SelectedMuscles.Any(muscle => exercise.PrimaryMuscles.Contains(muscle)) || SelectedMuscles.Contains("fullBody")).ToList();


            ExerciseList.Clear();
            ExerciseCount = 0;
            foreach (var exercise in filteredByMuscle)
            {
                ExerciseList.Add(exercise);
                ExerciseCount++;
            }
            await SelectedDurationWorkout();

            if (MusclePopup != null)
            {
                await CloseMuscleSelectionPopup();
            }

        }

        [RelayCommand]
        public async Task SelectDurationWorkout(string minutos)
        {

            if (minutos == "15")
            {
                DurationHeader = "15 min";

            }
            else if (minutos == "30")
            {
                DurationHeader = "30 min";

            }
            else if (minutos == "45")
            {
                DurationHeader = "45 min";

            }
            else if (minutos == "60")
            {
                DurationHeader = "1 hr";

            }
            else if (minutos == "90")
            {
                DurationHeader = "1:30 hr";

            }
            else if (minutos == "120")
            {
                DurationHeader = "2 hr";

            }
            else if (minutos == "150")
            {
                DurationHeader = "2:30 hr";

            }
            else if (minutos == "180")
            {
                DurationHeader = "3 hr";

            }

            if (int.TryParse(minutos, out int selectedMinutes))
            {
                await ShowExercisesWithAvailableEquipment();


                var filteredByMuscles = ExerciseList
                   .Where(exercise => SelectedMuscles.Any(muscle => exercise.PrimaryMuscles.Contains(muscle)))
                   .ToList();

                var filteredByTimeAndMuscles = filteredByMuscles
                    .Where(exercise => exercise.Duration != null && int.Parse(exercise.Duration) <= selectedMinutes)
                    .ToList();

                ExerciseList.Clear();
                int totalMinutes = 0;
                ExerciseCount = 0;
                foreach (var exercise in filteredByTimeAndMuscles)
                {
                    int exerciseMinutes = int.Parse(exercise.Duration);
                    totalMinutes += exerciseMinutes;

                    if (totalMinutes <= selectedMinutes)
                    {
                        ExerciseList.Add(exercise);
                        ExerciseCount++;
                    }
                }

            }
            DurationExpander = false;
            IsShimmerPlaying = true;
            await Task.Delay(500);
            IsShimmerPlaying = false;
        }


        [RelayCommand]
        public async Task SelectMuscleWorkout(string muscle)
        {
            if (muscle == "fullBody")
            {
                MuscleHeader = "Cuerpo Entero";

            }
            else if (muscle == "Pecho")
            {
                MuscleHeader = "Pecho";

            }
            else if (muscle == "Espalda")
            {
                MuscleHeader = "Espalda";

            }

            else if (muscle == "Dorsales")
            {
                MuscleHeader = "Dorsales";

            }

            else if (muscle == "Gemelos")
            {
                MuscleHeader = "Gemelos";

            }

            else if (muscle == "Femoral")
            {
                MuscleHeader = "Femoral";

            }

            else if (muscle == "Cuádriceps")
            {
                MuscleHeader = "Cuádriceps";

            }

            else if (muscle == "Aductores")
            {
                MuscleHeader = "Aductores";

            }

            else if (muscle == "Abductores")
            {
                MuscleHeader = "Abductores";

            }

            else if (muscle == "Hombro")
            {
                MuscleHeader = "Hombro";

            }
            else if (muscle == "Bíceps")
            {
                MuscleHeader = "Bíceps";

            }
            else if (muscle == "Tríceps")
            {
                MuscleHeader = "Tríceps";

            }
            else if (muscle == "Trapecio")
            {
                MuscleHeader = "Trapecio";

            }
            else if (muscle == "Glúteos")
            {
                MuscleHeader = "Glúteos";

            }
            else if (muscle == "Abdomen")
            {
                MuscleHeader = "Abdomen";

            }
            else if (muscle == "Antebrazo")
            {
                MuscleHeader = "Antebrazo";

            }


            await ShowExercisesWithAvailableEquipment();

            // Filtrar nuevamente por el músculo seleccionado
            var filteredByMuscle = ExerciseList.Where(exercise => exercise.PrimaryMuscles.Equals(MuscleHeader) || muscle.Equals("fullBody")).ToList();


            ExerciseList.Clear();
            ExerciseCount = 0;
            foreach (var exercise in filteredByMuscle)
            {
                ExerciseList.Add(exercise);
                ExerciseCount++;
            }

            MuscleExpander = false;


        }








        // Método que carga los ejercicios segun el equipamiento de un usuario en el
        //
        //
        // ExercisePopup
        [RelayCommand]
        public async Task ShowExercisesWithAvailableEquipment() //Usarlo en workout en vez de customExercise
        {
            // Referencia al documento del usuario
            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document("123456@gmail.com"); // Cambiar por el email del usuario actual

            // Obtener la subcolección "equipments" del usuario
            var snapshot = await userDocument
                .Collection("Equipment")
                .GetAsync();

            var equipments = snapshot.Documents
                                     .Select(doc => doc.ToObject<EquipmentModel>())
                                     .Where(equipment => equipment != null)
                                     .ToList();

            //Recogemos todo el equipamiento disponible (checked) y los agregamos a la lista de strings
            List<string> SelectedEquipment = new List<string>();
            foreach (var equipment in equipments)
            {
                if (equipment == null)
                    continue;

                EquipmentModelView equipmentModelView = ModelMapper.EquipmentModelToEquipmentModelView(equipment);
                equipmentModelView.IsChecked = equipment.disponible;

                if (equipmentModelView.IsChecked = equipment.disponible)
                {
                    SelectedEquipment.Add(equipmentModelView.Name);
                }

            }

            // Referencia al documento del usuario
            var exerciseDocument = await CrossCloudFirestore.Current
                .Instance
                .Collection("Exercises")
                .GetAsync();

            var exercises = exerciseDocument.Documents //solo fecha de entrenamiento y name
                .Select(exercise => exercise.ToObject<ExerciseModel>())
                .Where(exercise => exercise != null)
                .ToList();


            //Obtenemos todos los ejercicios y los agregamos a la lista de strings
            List<string> ExerciseEquipmentList = new List<string>();
            foreach (var exercise in exercises)
            {
                if (exercise == null)
                    continue;
                ExerciseModelView exerciseModelView = ModelMapper.ExerciseModelToExerciseModelView(exercise);
                ExerciseEquipmentList.AddRange(exerciseModelView.Equipment);
            }
            //Ahora recogemos el nombre que tienen en comun las 2 listas
            var AvailableEquipment = SelectedEquipment.Intersect(ExerciseEquipmentList).ToList();

            //Por último agregamos a ExerciseList solo los nombres que tienen en comun las 2 listas y voilá
            ExerciseList.Clear();
            ExerciseCount = 0;
            foreach (var exercise in exercises)
            {
                if (exercise == null)
                    continue;
                ExerciseModelView exerciseModelView = ModelMapper.ExerciseModelToExerciseModelView(exercise);

                if (exerciseModelView.Equipment.All(equipment => AvailableEquipment.Contains(equipment)))
                {
                    ExerciseList.Add(exerciseModelView);
                    ExerciseCount++;
                }
            }

        }



        // Hace referencia al CustomExercisePopup
        [RelayCommand]
        public async Task NewTrainingSession()
        {
            // Filtra la lista para obtener solo los ejercicios seleccionados
            var SelectedExercises = ExerciseList
                .Where(exercise => exercise.IsChecked)
                .ToList();

            if (SelectedExercises.Any())
            {
                // Crear una subcolección dentro de "Users" con el nombre Sesiones, en caso de guardar entrenamiento custom
                //var userDocument = CrossCloudFirestore.Current
                //    .Instance
                //    .Collection("Users")
                //    .Document("123456@gmail.com");
                ExerciseList.Clear();
                ExerciseCount = 0;
                foreach (var selectedExercise in SelectedExercises)
                {
                    //// Agrega cada ejercicio a la subcolección Sesiones, se podría llamar MiCustomLista
                    //await userDocument
                    //    .Collection("Sesiones")
                    //    .AddAsync(selectedExercise.Exercise);
                    ExerciseList.Add(selectedExercise);
                    ExerciseCount++;
                }
                IsCustomExerciseButtonVisible = false;
                CustomExercisePopup.Close();
            }
            else
            {
                // cambiar de color
            }
        }



        [RelayCommand]
        public async Task DeleteSelectedEvent()
        {
            // Filtra los elementos seleccionados mediante CheckBox
            ExercisesToDelete = EventsList.Where(@event => @event.IsChecked).ToList();
            if (ExercisesToDelete.Count == 0)
            {
                return;
            }
            foreach (var eventToDelete in ExercisesToDelete)
            {
                // Realiza la eliminación en la base de datos
                // Query the database to find the document with a specific condition
                var query = CrossCloudFirestore.Current
                    .Instance
                    .Collection("Users")
                    .Document("123456@gmail.com")
                    .Collection("Sesiones")
                    .WhereEqualsTo("Id", eventToDelete.Id); // Reemplaza con la propiedad y la condición reales

                var querySnapshot = await query.GetDocumentsAsync();

                // Itera a través de los documentos de resultado y los elimina
                foreach (var document in querySnapshot.Documents)
                {
                    await document.Reference.DeleteAsync();
                }

                // Elimina el elemento de la lista local
                EventsList.Remove(eventToDelete);
            }
            IsDeleteExerciseButtonVisible = false;
            IsDetailsExerciseButtonVisible = false;
        }



        // Hace referencia al boton "obtener entrenamientos" del primer tab del MainPage.xaml
        [RelayCommand]
        public async Task GetExercisesEvents()
        {
            try
            {
                var snapshot = await CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document("123456@gmail.com")
                .Collection("Sesiones")
                .GetAsync();

                var exercises = snapshot.Documents
                                          .Select(doc => doc.ToObject<ExerciseModel>())
                                          .Where(exercise => exercise != null)
                                          .ToList();

                ExerciseList.Clear();
                Events.Clear();
                int totalWorkoutDuration = 0;
                totalWorkoutDurations.Clear();
                foreach (var exercise in exercises)
                {

                    ExerciseView = ModelMapper.ExerciseModelToExerciseModelView(exercise);
                    ExerciseList.Add(ExerciseView);

                    string[] dateArray = ExerciseView.FechaEntrenamiento.Split(' ');
                    var dateTime = dateArray[0];

                    DateTime dateTimeKey = DateTime.Parse(dateTime);
                    ExerciseView.FechaEntrenamiento = dateTimeKey.ToString("dd/MM/yyyy");

                    // Update the total workout duration for the specific day
                    if (!totalWorkoutDurations.ContainsKey(dateTimeKey))
                    {
                        totalWorkoutDurations.Add(dateTimeKey, ConvertStopwatchLabelToMinutes(exercise.Duration));
                    }
                    else
                    {
                        totalWorkoutDurations[dateTimeKey] += ConvertStopwatchLabelToMinutes(exercise.Duration);
                    }

                    // Update the totalWorkoutDuration for the UI
                    //totalWorkoutDuration = totalWorkoutDurations.Values.Sum();  //Suma todos los ejercicios de la db
                    totalWorkoutDuration = totalWorkoutDurations[dateTimeKey];   // Suma el último dia (15)
                    TotalWorkoutDurationLabel = totalWorkoutDuration.ToString();
                    // Verificar si la clave ya existe en Events antes de agregarla

                    if (!Events.ContainsKey(dateTimeKey))
                    {
                        // Crear una nueva instancia de ObservableCollection y agregar el ejercicio
                        var eventsList = new ObservableCollection<ExerciseModelView> { ExerciseView };
                        Events.Add(dateTimeKey.Date, eventsList);
                    }
                    else
                    {
                        // Obtener la colección asociada a la clave y agregar el ejercicio
                        var collection = (ObservableCollection<ExerciseModelView>)Events[dateTimeKey.Date];
                        collection.Add(ExerciseView);

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener ejercicios: {ex.Message}");
            }

        }
        private Dictionary<DateTime, int> totalWorkoutDurations = new Dictionary<DateTime, int>();

        public static int ConvertStopwatchLabelToMinutes(string stopwatchLabel)
        {
            if (int.TryParse(stopwatchLabel, out int duration))
            {
                return duration;
            }
            return 0; // Default value or handle the conversion failure
        }



        // Método que carga el equipamiento de un usuario específico en el CustomEquipmentPopup
        public async Task LoadUserEquipments()
        {
            // Referencia al documento del usuario
            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document("123456@gmail.com"); //cambiar por email

            // Obtener la subcolección "equipments" del usuario
            var snapshot = await userDocument
                .Collection("Equipment")
                .GetAsync();

            var equipments = snapshot.Documents
                                     .Select(doc => doc.ToObject<EquipmentModel>())
                                     .Where(equipment => equipment != null)
                                     .ToList();

            EquipmentList.Clear();
            foreach (var equipment in equipments)
            {
                if (equipment == null)
                    continue;

                EquipmentModelView equipmentModelView = ModelMapper.EquipmentModelToEquipmentModelView(equipment);
                equipmentModelView.IsChecked = equipment.disponible;
                EquipmentList.Add(equipmentModelView);
            }
        }



        //Filtra por los checkBox de EquipmentPopup
        [RelayCommand]
        public async Task UpdateUserEquipments()
        {
            foreach (var equipment in EquipmentList)
            {
                if (equipment.IsChecked || !equipment.IsChecked)
                {
                    SelectedEquipment.Add(equipment);
                }
            }

            // Referencia al documento del usuario
            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document("123456@gmail.com");

            foreach (var equipment in SelectedEquipment)
            {

                var name = equipment.Name; //.ToLower().Replace(" ", "")
                var available = equipment.IsChecked;


                // Actualizar el documento del equipamiento en la subcolección "equipments" del usuario
                await userDocument
                    .Collection("Equipment")
                    .Document(name)
                  .UpdateAsync("disponible", available);
            }

            CloseEquipmentPopup();
            await SelectedDurationWorkout();


        }

        public async Task SelectedDurationWorkout()
        {
            if (DurationHeader.Equals("15 min"))
            {
                await SelectDurationWorkout("15");
            }
            else if (DurationHeader.Equals("30 min"))
            {
                await SelectDurationWorkout("30");
            }
            else if (DurationHeader.Equals("45 min"))
            {
                await SelectDurationWorkout("45");
            }
            else if (DurationHeader.Equals("1 hr"))
            {
                await SelectDurationWorkout("60");
            }
            else if (DurationHeader.Equals("1:30 hr"))
            {
                await SelectDurationWorkout("90");
            }
            else if (DurationHeader.Equals("2 hr"))
            {
                await SelectDurationWorkout("120");
            }
            else if (DurationHeader.Equals("2:30 hr"))
            {
                await SelectDurationWorkout("150");
            }
            else if (DurationHeader.Equals("3 hr"))
            {
                await SelectDurationWorkout("180");
            }
        }




        //Metodo para mostrar el nombre del usuario que ha hecho el Login
        public async void ShowName()
        {

            var nombreusuario = await CrossCloudFirestore.Current
                         .Instance
                         .Collection("Users")
                         .Document("123456@gmail.com") //LoginViewModel.Email_name
                         .GetAsync();
            User = nombreusuario.ToObject<UserModel>();
            UserName = User.Name;
        }


        //Metodo que muestra todos los ejercicios en el CustomExercisePopup 
        [RelayCommand]
        public async Task ShowAllExercises()
        {
            var snapshot = await CrossCloudFirestore.Current
                            .Instance
                            .Collection("Exercises")
                            .GetAsync();

            var exercises = snapshot.Documents
                                     .Select(doc => doc.ToObject<ExerciseModel>())
                                     .Where(exercise => exercise != null)
                                     .ToList();

            ExerciseList.Clear();
            foreach (var exercise in exercises)
            {
                if (exercise == null)
                    continue;
                ExerciseModelView exerciseModelView = ModelMapper.ExerciseModelToExerciseModelView(exercise);
                ExerciseList.Add(exerciseModelView);
            }
        }


        //Para seleccionar una fila de CollectionView
        [RelayCommand]
        private void ExerciseTapped(ExerciseModelView exercise)
        {
            exercise.IsChecked = !exercise.IsChecked;
            if (exercise.IsChecked)
            {
                MiContador++;
                IsDeleteExerciseButtonVisible = true;
                IsDetailsExerciseButtonVisible = true;
                IsCustomExerciseButtonVisible = true;
            }
            else if (!exercise.IsChecked)
            {
                MiContador--;
                //IsDeleteExerciseButtonVisible = false;
                //IsDetailsExerciseButtonVisible = false;

            }
            if (MiContador == 0)
            {
                IsDeleteExerciseButtonVisible = false;
                IsDetailsExerciseButtonVisible = false;
            }
        }

        //Para seleccionar una fila de CollectionView
        [RelayCommand]
        private void CustomExerciseTapped(DateGroup exercise)
        {
            exercise.IsChecked = !exercise.IsChecked;
            if (exercise.IsChecked)
            {
                MiContador++;
                IsDeleteExerciseButtonVisible = true;
                IsDetailsExerciseButtonVisible = true;
                IsCustomExerciseButtonVisible = true;
            }
            else if (!exercise.IsChecked)
            {
                MiContador--;
                //IsDeleteExerciseButtonVisible = false;
                //IsDetailsExerciseButtonVisible = false;

            }
            if (MiContador == 0)
            {
                IsDeleteExerciseButtonVisible = false;
                IsDetailsExerciseButtonVisible = false;
            }
        }


        [RelayCommand]
        public async Task DeleteSelectedExercise()
        {
            ExercisesToDelete = ExerciseSessionlist.Where(exercise => exercise.IsChecked).ToList();

            foreach (var exerciseToDelete in ExercisesToDelete)
            {
                // Query the database to find the document with a specific condition
                var query = CrossCloudFirestore.Current
                    .Instance
                    .Collection("Users")
                    .Document("123456@gmail.com")
                    .Collection("Sesiones")
                    .WhereEqualsTo("Id", exerciseToDelete.Id); // Replace with the actual property and condition

                var querySnapshot = await query.GetDocumentsAsync();

                // Iterate through the result documents and delete them
                foreach (var document in querySnapshot.Documents)
                {
                    await document.Reference.DeleteAsync();
                }

                // Remove the exercise from the local list
                ExerciseSessionlist.Remove(exerciseToDelete);
                IsDeleteExerciseButtonVisible = false;
            }

        }

        [RelayCommand]
        public async Task DeleteSession()
        {
            bool confirmation = await App.Current.MainPage.DisplayAlert("Confirmación",
           "¿Estás seguro de que quieres borrar todos los entrenamientos?", "Borrar Todo", "Cancelar");
            if (confirmation)
            {
                var snapshot = await CrossCloudFirestore.Current
                           .Instance
                           .Collection("Users")
                           .Document("123456@gmail.com")
                           .Collection("Sesiones")
                           .GetAsync();

                foreach (var document in snapshot.Documents)
                {
                    await CrossCloudFirestore.Current
                        .Instance
                        .Collection("Users")
                        .Document("123456@gmail.com")
                        .Collection("Sesiones")
                        .Document(document.Id)
                        .DeleteAsync();
                }
            }

        }

        [RelayCommand]
        public async Task GetSessionExercises()
        {

            var snapshot = await CrossCloudFirestore.Current
            .Instance
            .Collection("Users")
            .Document("123456@gmail.com")
            .Collection("Sesiones")
            .GetAsync();

            var exercises = snapshot.Documents
                                      .Select(doc => doc.ToObject<ExerciseModel>())
                                      .Where(exercise => exercise != null)
                                      .ToList();

            ExerciseSessionlist.Clear();
            foreach (var exercise in exercises)
            {

                ExerciseView = ModelMapper.ExerciseModelToExerciseModelView(exercise);
                ExerciseSessionlist.Add(ExerciseView);
            }
        }


        [RelayCommand]
        private void EquipmentTapped(EquipmentModelView equipment)
        {
            equipment.IsChecked = !equipment.IsChecked;
        }




        //Subir imagenes a Firebase / Storage
        [RelayCommand]
        public async Task ChangeProfilePic()
        {
            var foto = await MediaPicker.PickPhotoAsync();
            if (foto != null)
            {
                var storageTask = new FirebaseStorage("logingym-1df51.appspot.com")
                    .Child("Avatars")
                    .Child(foto.FileName)
                    .PutAsync(await foto.OpenReadAsync());

            var downloadUrl = await storageTask;
            var fotoUrl = downloadUrl.ToString();


            // Actualizar el campo Avatar del usuario con la URL de la foto
            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document("123456@gmail.com");

                // Agregar el nombre del workout al campo Colecciones del documento del usuario
                await userDocument.UpdateDataAsync(new { Avatar = fotoUrl });

            }
            GetProfilePic();


        }

        [RelayCommand]
        public async Task GetProfilePic()
        {
            
            var avatar = await CrossCloudFirestore.Current
                            .Instance
                            .Collection("Users")
                            .Document("123456@gmail.com") //LoginViewModel.Email_name
                            .GetAsync();
            User = avatar.ToObject<UserModel>();

            AvatarImage = User.Avatar;
            
            
        }



        //FILTROS
        //Filtro en tiempo real para el searchbar del CustomExercisePopup
        private async Task RealTimeUpdateFilter()
        {
            var exercisesSnapshot = await CrossCloudFirestore.Current
             .Instance
             .Collection("Exercises")
             .GetAsync();

            if (exercisesSnapshot != null && exercisesSnapshot.Documents.Any())
            {
                ExerciseList.Clear(); // Limpiar la lista existente

                foreach (var exerciseDocument in exercisesSnapshot.Documents)
                {
                    var exercise = exerciseDocument.ToObject<ExerciseModel>();
                    var exerciseModelView = ModelMapper.ExerciseModelToExerciseModelView(exercise);
                    ExerciseList.Add(exerciseModelView);
                }

                UpdateFilteredExercises();
            }
            else
            {
                // No se encontraron documentos
                // Puedes manejar este caso según tus necesidades
            }

        }
        //Filtra el texto del search bar en el CustomExercisePopup
        public void UpdateFilteredExercises()
        {
            // Obtén la cadena de búsqueda o establece una cadena vacía si es nula
            string searchQuery = SearchText?.ToLower() ?? string.Empty; //Si el resultado anterior es nulo, utiliza una cadena vacía 
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                //ShowAllExercises();
                ShowExercisesWithAvailableEquipment();

            }

            else
            {
                ExerciseList = new ObservableCollection<ExerciseModelView>(
                ExerciseList.Where(exercise => exercise.Name?.ToLower().Contains(searchQuery) == true));
                // Verifica si el nombre del ejercicio (si no es nulo) en minúsculas contiene la cadena de búsqueda, devolviendo true si la condición se cumple.
            }
        }

        //Filtra por los nombres/botones de CustomExercisePopup
        [RelayCommand]
        public async Task WhereEqualsToPrimaryMuscles(string muscle)
        {
            var query = await CrossCloudFirestore.Current
                                          .Instance
                                          .Collection("Exercises")
                                          .WhereEqualsTo("PrimaryMuscles", muscle)
                                          .GetAsync();

            var exercises = query.Documents
                        .Select(doc => doc.ToObject<ExerciseModel>())
                        .Where(exercise => exercise != null)
                        .ToList();

            ExerciseList.Clear();
            foreach (var exercise in exercises)
            {
                ExerciseModelView exerciseModelView = ModelMapper.ExerciseModelToExerciseModelView(exercise);
                ExerciseList.Add(exerciseModelView);
            }
        }

        [RelayCommand]
        public async Task ReplaceWhereEqualsToPrimaryMuscles(string muscle)
        {
            var query = await CrossCloudFirestore.Current
                                          .Instance
                                          .Collection("Exercises")
                                          .WhereEqualsTo("PrimaryMuscles", muscle)
                                          .GetAsync();

            var exercises = query.Documents
                        .Select(doc => doc.ToObject<ExerciseModel>())
                        .Where(exercise => exercise != null)
                        .ToList();

            var selectedEquipment = await GetSelectedEquipment();

            // Filtrar los ejercicios por el equipamiento disponible
            var filteredExercises = exercises
                .Where(exercise => exercise.Equipment.Any(e => selectedEquipment.Contains(e)))
                .ToList();




            MuscleExerciselist.Clear();
            foreach (var exercise in filteredExercises)
            {
                ExerciseModelView exerciseModelView = ModelMapper.ExerciseModelToExerciseModelView(exercise);
                MuscleExerciselist.Add(exerciseModelView);
            }
        }

        private async Task<List<string>> GetSelectedEquipment()
        {
            // Referencia al documento del usuario
            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document("123456@gmail.com"); // Cambiar por el email del usuario actual

            // Obtener la subcolección "equipments" del usuario
            var snapshot = await userDocument
                .Collection("Equipment")
                .GetAsync();

            var equipments = snapshot.Documents
                                     .Select(doc => doc.ToObject<EquipmentModel>())
                                     .Where(equipment => equipment != null && equipment.disponible)
                                     .ToList();

            // Obtener los nombres del equipamiento disponible
            return equipments.Select(equipment => equipment.name).ToList();
        }




        // POPUPS
        private async Task<bool> MostrarPopup(string titulo, string mensaje)
        {
            // Mostrar el popup y esperar la respuesta del usuario
            bool result = await App.Current.MainPage.DisplayAlert(titulo, mensaje, "Aceptar", "Cancelar");
            return result;
        }

        [RelayCommand]
        public async Task OpenCustomExercisePopup(string mode)
        {

            CustomExercisePopup = new CustomExercises();
            await App.Current.MainPage.ShowPopupAsync(CustomExercisePopup);
        }

        [RelayCommand]
        public async Task ClosecustomExercisePopup()
        {
            //ShowExercisesWithAvailableEquipment();
            //RealTimeUpdateFilter();
            await SelectedDurationWorkout();
            CustomExercisePopup.Close();


        }

        [RelayCommand]
        public async Task OpenEquipmentPopup()
        {

            EquipmentPopup = new EquipmentPopup();
            await App.Current.MainPage.ShowPopupAsync(EquipmentPopup);
        }

        [RelayCommand]
        public async Task CloseEquipmentPopup()
        {
            EquipmentPopup.Close();

        }

        [RelayCommand]
        public async Task OpenMuscleSelectionPopup()
        {
            SelectedMuscles.Clear();
            SelectedMuscles.Add("Pecho");
            SelectedMuscles.Add("Hombro");
            MusclePopup = new MuscleSelectionPopup();
            await App.Current.MainPage.ShowPopupAsync(MusclePopup);
        }

        [RelayCommand]
        public async Task CloseMuscleSelectionPopup()
        {

            MusclePopup.Close();
            MusclePopup = null;

        }

        [RelayCommand]
        public async Task OpenMuscleReplacementPopup(ExerciseModelView exercise)
        {

            MuscleReplacementPopup = new MuscleReplacementPopup();
            ReplaceWhereEqualsToPrimaryMuscles(exercise.PrimaryMuscles);

            await App.Current.MainPage.ShowPopupAsync(MuscleReplacementPopup);
        }

        [RelayCommand]
        public async Task CloseMuscleReplacementPopup(ExerciseModelView exercise)
        {
            ExerciseList.Add(exercise);
            MuscleReplacementPopup.Close();

        }

        [RelayCommand]
        public async Task OpenInfoExercisePopup()
        {
            InfoEventsList.Clear();
            InfoEventsList = new ObservableCollection<ExerciseModelView>(EventsList.Where(exercise => exercise.IsChecked));
            if (InfoEventsList.Count == 0)
            {
                return;
            }
            InfoExercisePopup = new InfoExercisePopup();
            await App.Current.MainPage.ShowPopupAsync(InfoExercisePopup);
        }



        [RelayCommand]
        public async Task CloseInfoExercisePopup()
        {
            InfoExercisePopup.Close();

        }



        [RelayCommand]
        public async Task LoadTappedExercise(ExerciseModel selectedExercise)
        {
            IsVisible = false;
            IsBackButtonVisible = true;
            await Shell.Current.GoToAsync("//ExerciseTappedPage", new Dictionary<string, object>()
            {
                ["MyExercise"] = selectedExercise,
                ["IsVisible"] = IsVisible,
                ["IsBackButtonVisible"] = IsBackButtonVisible
            });
        }


        [RelayCommand]
        public async Task LogOut()
        {
            await Shell.Current.GoToAsync("//LoginPage");

            await SecureStorage.SetAsync("credentialsStored", "false");
            await SecureStorage.SetAsync("username", "");
            await SecureStorage.SetAsync("password", "");
        }

        [RelayCommand]
        public async Task LoadStartedWorkout(ObservableCollection<ExerciseModelView> exerciseList)
        {
            await Shell.Current.GoToAsync("//StartedWorkoutPage", new Dictionary<string, object>()
            {
                ["ExerciseList"] = exerciseList
            });

        }

        //[RelayCommand]
        //public async Task ShowNotification()
        //{
        //    if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
        //    {
        //        // Pregunta al usuario si permite la recepcion de notificaciones
        //        if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
        //        {
        //            await LocalNotificationCenter.Current.RequestNotificationPermission();
        //        }

        //        // Creamos la notificacion
        //        var notification = new NotificationRequest
        //        {
        //            NotificationId = 100,
        //            Title = "Esto es el titulo",
        //            Sound = "sound",
        //            Description = "Esto es el contenido del mensaje"
        //        };

        //        // Muestra la notificacion
        //        await LocalNotificationCenter.Current.Show(notification);


        //    }
        //}




        //}
        //// Si la plataforma es windows...
        //else if (DeviceInfo.Platform == DevicePlatform.WinUI)
        //{
        //    // Muestra el toast con el mensaje
        //    await Toast.Make("Tienes un mensaje", ToastDuration.Short).Show();





    }
}
