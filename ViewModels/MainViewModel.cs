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



namespace mobileAppTest.ViewModels
{
    internal partial class MainViewModel : ObservableObject
    {
        private readonly Services.IPopupService popupService;


        [ObservableProperty]
        private CustomExercises _customExercisePopup;

        [ObservableProperty]
        private EquipmentPopup _equipmentPopup;

        [ObservableProperty]
        private MuscleSelectionPopup _musclePopup;

        [ObservableProperty]
        private MuscleReplacementPopup _muscleReplacementPopup;

        private InfoMuscleMopup InfoMuscleMopup { get; set; }
        private InfoMuscleMopupViewModel InfoMuscleMopupViewModel { get; set; }
        private Dictionary<string, ObservableCollection<string>> MuscleImagesDict { get; set; }

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
        private bool _isGlutesSelected;

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
        private bool _isGlutesVisible;

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
        private string _glutesColor;

        #endregion

        [ObservableProperty]
        private EventCollection events;

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> eventsList;  //Para el punto rojo del calendar.

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> exerciseList;

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _exerciseSessionlist;

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _muscleExerciselist;

        [ObservableProperty]
        private ObservableCollection<EquipmentModelView> equipmentList;

        [ObservableProperty]
        private ObservableCollection<EquipmentModelView> selectedEquipment;

        [ObservableProperty]
        private ObservableCollection<string> selectedMuscles;

        [ObservableProperty]
        private string _imageSource;

        [ObservableProperty]
        private string totalWorkoutDurationLabel;

        public MainViewModel()
        {
            //this.popupService = popupService;
            User = new UserModel();
            Exercise = new ExerciseModel();
            ExerciseView = new ExerciseModelView();
            Events = new EventCollection();
            Culture = CultureInfo.CurrentCulture;
            EventsList = new ObservableCollection<ExerciseModelView>();
            ExerciseSessionlist = new ObservableCollection<ExerciseModelView>();
            ExerciseList = new ObservableCollection<ExerciseModelView>();
            EquipmentList = new ObservableCollection<EquipmentModelView>();
            SelectedEquipment = new ObservableCollection<EquipmentModelView>();
            SelectedMuscles = new ObservableCollection<string>();
            MuscleExerciselist = new ObservableCollection<ExerciseModelView>();
            IsFiltrarMusclesButtonVisible = false;
            IsFrontBodyVisible = true;
            IsBackBodyVisible = false;
           // IsChestSelected = true;
           // IsQuadricepsSelected = true;
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
            IsGlutesVisible = false;



            ShowName();
            //ShowAllExercises();
            GetSessionExercises();
            //RealTimeUpdateFilter();
            GetExercisesEvents();
            LoadUserEquipments();
            ShowExercisesWithAvailableEquipment();
            //DurationHeader = "15 min";
            // IsDurationChecked = true;
            MuscleHeader = "Cuerpo Entero";
            // MuscleExpander = false;
            InfoMuscleMopupViewModel = new InfoMuscleMopupViewModel();
            InfoMuscleMopup = new InfoMuscleMopup(InfoMuscleMopupViewModel);
            InitImagesDict();
            SelectDurationWorkout("15");
        }

        private void InitImagesDict()
        {
            MuscleImagesDict = new Dictionary<string, ObservableCollection<string>>()
            {
                { "body", ["body.png"] },
                { "biceps", ["biceps.png"] },
                { "cuadriceps", ["cuadriceps.png"] },
                { "pecho", ["pecho.png"] },
                { "trapecio", ["trapecio.png"] }
            };
        }


        [RelayCommand]
        public async Task ShowInfoMuscle(string muscleType)
        {
            MuscleImagesDict.TryGetValue(muscleType, out ObservableCollection<string> imagesPath);
            if (imagesPath != null)
            {
                InfoMuscleMopupViewModel.SetMuscleImagePathList(imagesPath);
                await MopupService.Instance.PushAsync(InfoMuscleMopup);
            }
        }

        [RelayCommand]
        private void DeleteSwipedRow(ExerciseModelView rowToDelete)
        {
            ExerciseList.Remove(rowToDelete);
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

            if (muscle.Contains("Glúteos"))
            {
                IsGlutesSelected = !IsGlutesSelected;

                if (IsGlutesSelected)
                {
                    GlutesColor = "glutes.png";
                    SelectedMuscles.Add(muscle);
                }
                else
                {
                    GlutesColor = "glutesdefault.png";
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
                IsGlutesVisible = false;
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
                IsGlutesVisible = true;
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

            if (DurationHeader.Equals("15 min"))
            {
                await SelectDurationWorkout("15");
            }
            else if (DurationHeader.Equals("30 min"))
            {
                await SelectDurationWorkout("30");
            }
            await CloseMuscleSelectionPopup();
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


   





        // Método que carga los ejercicios segun el equipamiento de un usuario en el CustomExercisePopup
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
            var selectedExercises = ExerciseList
                .Where(exercise => exercise.IsChecked)
                .ToList();

            if (selectedExercises.Any())
            {
                // Crear una subcolección dentro de "Users" con el nombre Sesiones
                var userDocument = CrossCloudFirestore.Current
                    .Instance
                    .Collection("Users")
                    .Document("123456@gmail.com");

                foreach (var selectedExercise in selectedExercises)
                {
                    // Agrega cada ejercicio a la subcolección Sesiones
                    await userDocument
                        .Collection("Sesiones")
                        .AddAsync(selectedExercise.Exercise);
                }
            }
            else
            {
                // Manejar el caso en que no se ha seleccionado ningún ejercicio
            }
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
            MuscleHeader = "Cuerpo Entero";
            DurationHeader = "15 min";
            CloseEquipmentPopup();
            ShowExercisesWithAvailableEquipment();

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
                IsDeleteExerciseButtonVisible = true;
            }
            else if (!exercise.IsChecked)
            {
                IsDeleteExerciseButtonVisible = false;
            }
        }

        [RelayCommand]
        public async Task DeleteSelectedExercise()
        {
            var exercisesToDelete = ExerciseSessionlist.Where(exercise => exercise.IsChecked).ToList();

            foreach (var exerciseToDelete in exercisesToDelete)
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
        public async void Uploadpic()
        {
            var foto = await MediaPicker.PickPhotoAsync();
            if (foto != null)
            {
                var task = new FirebaseStorage("logingym-1df51.appspot.com")
                    .Child("Imagenes")
                    .Child(foto.FileName)
                    .PutAsync(await foto.OpenReadAsync());
            }
        }



        //FILTROS
        //Filtro en tiempo real para el searchbar del CustomExercisePopup
        private void RealTimeUpdateFilter()
        {
            CrossCloudFirestore.Current
                .Instance
                .Collection("Exercises")
                .AddSnapshotListener((snapshot, error) =>
                {
                    if (snapshot != null)
                    {
                        foreach (var documentChange in snapshot.DocumentChanges)
                        {
                            var newExercise = documentChange.Document.ToObject<ExerciseModel>();

                            if (newExercise != null)
                            {
                                var newExerciseModelView = ModelMapper.ExerciseModelToExerciseModelView(newExercise);

                                switch (documentChange.Type)
                                {
                                    case DocumentChangeType.Added:
                                        ExerciseList.Add(newExerciseModelView);
                                        break;
                                    case DocumentChangeType.Modified:
                                        var existingExercise = ExerciseList.FirstOrDefault(e => e.Name == newExerciseModelView.Name);
                                        if (existingExercise != null)
                                        {
                                            // Actualiza las propiedades que desees
                                            existingExercise.Name = newExerciseModelView.Name;
                                            // ... Actualiza otras propiedades si es necesario
                                        }
                                        break;
                                    case DocumentChangeType.Removed:
                                        var removedExercise = ExerciseList.FirstOrDefault(e => e.Name == newExerciseModelView.Name);
                                        if (removedExercise != null)
                                        {
                                            ExerciseList.Remove(removedExercise);
                                        }
                                        break;
                                }

                                UpdateFilteredExercises();
                            }
                        }
                    }
                });
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
            ShowExercisesWithAvailableEquipment();
            RealTimeUpdateFilter();
            CustomExercisePopup.Close();

        }

        [RelayCommand]
        public async Task OpenEquipmentPopup(string mode)
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

            MusclePopup = new MuscleSelectionPopup();
            await App.Current.MainPage.ShowPopupAsync(MusclePopup);
        }

        [RelayCommand]
        public async Task CloseMuscleSelectionPopup()
        {
            MusclePopup.Close();

        }

        [RelayCommand]
        public async Task OpenMuscleReplacementPopup(ExerciseModelView exercise)
        {

            MuscleReplacementPopup = new MuscleReplacementPopup();
            ReplaceWhereEqualsToPrimaryMuscles(exercise.PrimaryMuscles);
            MuscleReplacementPopup.BindingContext = this; //nao funsona

            await App.Current.MainPage.ShowPopupAsync(MuscleReplacementPopup);
        }

        [RelayCommand]
        public async Task CloseMuscleReplacementPopup(ExerciseModelView exercise)
        {
           ExerciseList.Add(exercise);
           MuscleReplacementPopup.Close();

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
        public async Task LogOut(String pagina)
        {
            await Shell.Current.GoToAsync("//" + pagina);
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
