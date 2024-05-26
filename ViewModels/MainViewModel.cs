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
using CommunityToolkit.Maui.Alerts;
using System.Diagnostics;
using Firebase.Auth;
using System.Text.RegularExpressions;
using System.ComponentModel;
using Plugin.Maui.Audio;

/*
 * 
Firebase : Explore extensions :Delete User Data     
notification
<ContentPage ...>
    <StackLayout Margin="20">
        <Label Text="This text is green in light mode, and red in dark mode."
               TextColor="{AppThemeBinding Light=Green, Dark=Red}" />
        <Image Source="{AppThemeBinding Light=lightlogo.png, Dark=darklogo.png}" />
    </StackLayout>
</ContentPage>
*
*/
namespace mobileAppTest.ViewModels
{
    [QueryProperty("ExerciseFinishedLists", "ExerciseFinishedLists")]
    [QueryProperty("Email", "Email")]
    internal partial class MainViewModel : ObservableObject, INotifyPropertyChanged
    {
        public string webApiKey = "AIzaSyAJ2z8_aTTkgCz-dYXhLt-bt4nEcXqjPxY";

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

        [ObservableProperty]
        private ProfilePopup _profilePopup;

        [ObservableProperty]
        private ConfigurationPopup _configurationPopup;

        #endregion

        #region UserRelated
        [ObservableProperty]
        private UserModel _user;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private double _reps;

        [ObservableProperty]
        private double _weight;

        [ObservableProperty]
        private double _break;

        [ObservableProperty]
        private bool _notificationOutside;

        [ObservableProperty]
        private bool _yeahBuddy;

        [ObservableProperty]
        private bool _whatsapp;

        [ObservableProperty]
        private bool _videoPlaying;

        [ObservableProperty]
        private string _passwordText;

        [ObservableProperty]
        private Color _passwordTextColor;

        [ObservableProperty]
        private Color _drawerColor;

        [ObservableProperty]
        private bool _isResetPassEnabled;

        [ObservableProperty]
        private DateTime _lastResetTime;

        [ObservableProperty]
        private bool _emailOK;

        [ObservableProperty]
        private bool _nameOK;

        [ObservableProperty]
        private bool _hasEmailError;

        [ObservableProperty]
        private bool _hasPassError;

        [ObservableProperty]
        private bool _hasNameError;

        [ObservableProperty]
        private string _emailErrorText;

        [ObservableProperty]
        private string _nameErrorText;

        [ObservableProperty]
        private string _emailSentLabel;

        [ObservableProperty]
        private bool _emailSent;

        #endregion

        [ObservableProperty]
        private ExerciseModel _exercise;

        [ObservableProperty]
        private ExerciseModelView _exerciseView;

        [ObservableProperty]
        private CultureInfo culture;

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private string _equipmentSearchText;

        [ObservableProperty]
        private int _exerciseCount;

        [ObservableProperty]
        private string _startTrainingText;

        [ObservableProperty]
        private bool _isStartTrainingVisible;

        [ObservableProperty]
        private int _miContador;

        [ObservableProperty]
        private int _miCustomContador;

        [ObservableProperty]
        private int _searchExerciseContador;

        [ObservableProperty]
        private bool _showNewTrainingButton;

        [ObservableProperty]
        private string _newTrainingButtonText;

        [ObservableProperty]
        private string _durationHeader;

        [ObservableProperty]
        private bool _durationExpander;

        [ObservableProperty]
        private bool _isDurationChecked;


        [ObservableProperty]
        private bool _isVisible;

        [ObservableProperty]
        private bool _isDeleteExerciseButtonVisible;

        [ObservableProperty]
        private bool _isDeleteCustomExerciseButtonVisible;

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

        //[ObservableProperty]
        //private string _avatarImage;
        private string _avatarImage;
        public string AvatarImage
        {
            get { return _avatarImage; }
            set
            {
                _avatarImage = value;
                OnPropertyChanged();
            }
        }

        [ObservableProperty]
        private bool _isShimmerPlaying;

        [ObservableProperty]
        private IAudioManager _audioManager = new AudioManager();

        public MainViewModel()
        {
            InitComponents();
            //ShowName();
            //GetUserCredentials();
            //ShowAllExercises();
            //GetSessionExercises();
            //RealTimeUpdateFilter();//xd
            GetExercisesEvents();
            LoadUserEquipments();
            //ShowExercisesWithAvailableEquipment();
            //DurationHeader = "15 min";
            // IsDurationChecked = true;
            SelectDurationWorkout("15");
            //FilterBySelectedMuscle();
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

            PasswordText = "Restablecer contraseña";
            PasswordTextColor = Colors.White;
            IsResetPassEnabled = true;
            NotificationOutside = true;
            EmailSent = false;
            MiContador = 0;
            MiCustomContador = 0;
            SearchExerciseContador = 0;
            ShowNewTrainingButton = false;

            ChestColor = "pecho2.png";
            IsChestSelected = true;
            LeftShoulderColor = "hombroizq2.png";
            RightShoulderColor = "hombroder2.png";
            IsRightShoulderSelected = true;
            SelectedMuscles.Add("Pecho");
            SelectedMuscles.Add("Hombro");

        }

        [RelayCommand]
        public async Task SaveUserConfiguration()
        {

            var userDocument = CrossCloudFirestore.Current
               .Instance
               .Collection("Users")
               .Document(Email);

            await userDocument.UpdateDataAsync(new
            {
                Reps = Reps,
                Weight = Weight,
                Break = Break,
                NotificationOutside = NotificationOutside,
                Yeahbuddy = YeahBuddy,
                Whatsapp = Whatsapp,
                VideoPlaying = VideoPlaying,
            });

            CloseConfigurationPopup();

    }

        [RelayCommand]
        public async Task Play_Sound(string sound)
        {
            if (sound=="whatsapp")
            {
              var player = AudioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("noti1.mp3"));
              player.Play();
                await Task.Delay(4000);
                player.Dispose();

            }
            else
            {
                var player = AudioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("yeahbuddy.mp3"));
                player.Play();
                await Task.Delay(4000);
                player.Dispose();
            }
          

        }


        [RelayCommand]
        public async Task TestDarkMode()
        {


            if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                Application.Current.UserAppTheme = AppTheme.Light;
                DrawerColor = Colors.White;

            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
                DrawerColor = Colors.Black;

            }

        }


        public async Task StartShimmerAndWait()
        {
            IsShimmerPlaying = true;


            try
            {
                // Realiza todas las operaciones asincrónicas
                await GetExercisesEvents();
                await FilterBySelectedMuscle();
                await SelectDurationWorkoutRandom("15");
                IsStartTrainingVisible = true;
                MiContador = 0;
                IsDeleteExerciseButtonVisible = false;
                IsDetailsExerciseButtonVisible = false;

            }
            finally
            {
                // Independientemente de si las operaciones son exitosas o no, detén el shimmer
                IsShimmerPlaying = false;
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
                    if (ExerciseCount == 1)
                    {
                        StartTrainingText = "Entrenar " + ExerciseCount + " ejercicio";

                    }
                    else
                    {
                        StartTrainingText = "Entrenar " + ExerciseCount + " ejercicios";
                    }
                },"Rehacer",TimeSpan.FromSeconds(5), snackbarOptions);
         
            await snackbar.Show();

            if(ExerciseCount == 0)
            {
                IsStartTrainingVisible = false;
            }

            else if (ExerciseCount == 1)
            {
                IsStartTrainingVisible = true;
                StartTrainingText = "Entrenar " + ExerciseCount + " ejercicio";

            }
            else
            {
                IsStartTrainingVisible = true;
                StartTrainingText = "Entrenar " + ExerciseCount + " ejercicios";
            }
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

            if (ExerciseCount == 0)
            {
                IsStartTrainingVisible = false;
            }

            else if (ExerciseCount == 1)
            {
                IsStartTrainingVisible = true;
                StartTrainingText = "Entrenar " + ExerciseCount + " ejercicio";

            }
            else
            {
                IsStartTrainingVisible = true;
                StartTrainingText = "Entrenar " + ExerciseCount + " ejercicios";
            }

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

            if (ExerciseCount == 1)
            {
                StartTrainingText = "Entrenar " + ExerciseCount + " ejercicio";

            }
            else
            {
                StartTrainingText = "Entrenar " + ExerciseCount + " ejercicios";
            }
            IsStartTrainingVisible = true;

        }

        [RelayCommand]
        public async Task SelectDurationWorkoutRandom(string minutos)
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
                var random = new Random();
                var randomizedList = filteredByTimeAndMuscles.OrderBy(x => random.Next()).ToList();

                foreach (var exercise in randomizedList)
                {
                    int exerciseMinutes = int.Parse(exercise.Duration);
                    totalMinutes += exerciseMinutes;

                    if (totalMinutes <= selectedMinutes)
                    {
                        ExerciseList.Add(exercise);
                        ExerciseCount++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            DurationExpander = false;
            IsStartTrainingVisible = true;

            if (ExerciseCount == 1)
            {
                StartTrainingText = "Entrenar " + ExerciseCount + " ejercicio";

            }
            else
            {
                StartTrainingText = "Entrenar " + ExerciseCount + " ejercicios";
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

            if(ExerciseCount == 1)
            {
                StartTrainingText = "Entrenar " + ExerciseCount + " ejercicio";

            }
            else
            {
                StartTrainingText = "Entrenar " + ExerciseCount + " ejercicios";
            }
            IsStartTrainingVisible = true;
            DurationExpander = false;
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

            if (ExerciseCount == 1)
            {
                StartTrainingText = "Entrenar " + ExerciseCount + " ejercicio";

            }
            else
            {
                StartTrainingText = "Entrenar " + ExerciseCount + " ejercicios";
            }

        }



        // Hace referencia al CustomExercisePopup
        [RelayCommand]
        public async Task NewTrainingSession()
        {

            // Filtra la lista para obtener solo los ejercicios seleccionados
            var SelectedExercises = _selectedExercises
                .Where(exercise => exercise.IsChecked)
                .ToList();

            if (SelectedExercises.Any())
            {
                ExerciseList.Clear();
                ExerciseCount = 0;
                foreach (var selectedExercise in SelectedExercises)
                {
                    if (!ExerciseList.Contains(selectedExercise))
                    {
                        ExerciseList.Add(selectedExercise);
                    }
                    ExerciseCount++;
                }
                CustomExercisePopup.Close();
            }
            else
            {
                // cambiar de color
            }
            _selectedExercises.Clear();
            SearchExerciseContador = 0;
            ShowNewTrainingButton = false;
            NewTrainingButtonText = "";
            IsStartTrainingVisible = true;

            StartTrainingText = ExerciseCount == 1 ? "Entrenar 1 ejercicio" : $"Entrenar {ExerciseCount} ejercicios";



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
            StartShimmerAndWait();
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
                foreach (var exercise in exercises)
                {

                    if( exercise.Name != null )
                    {
        
                    }
                           ExerciseView = ModelMapper.ExerciseModelToExerciseModelView(exercise);
                        ExerciseList.Add(ExerciseView);

                    string[] dateArray = ExerciseView.FechaEntrenamiento.Split(' ');
                    var dateTime = dateArray[0];

                    DateTime dateTimeKey = DateTime.Parse(dateTime);
                    ExerciseView.FechaEntrenamiento = dateTimeKey.ToString("dd/MM/yyyy");


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
            EquipmentSearchText = "";

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
        //public async void ShowName()
        //{

        //    var nombreusuario = await CrossCloudFirestore.Current
        //                 .Instance
        //                 .Collection("Users")
        //                 .Document("123456@gmail.com") //LoginViewModel.Email_name
        //                 .GetAsync();
        //    User = nombreusuario.ToObject<UserModel>();
        //    UserName = User.Name;
        //}


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

        private List<ExerciseModelView> _selectedExercises = new List<ExerciseModelView>();

        //Para seleccionar una fila de CollectionView
        [RelayCommand]
        private void ExerciseTapped(ExerciseModelView exercise)
        {
            exercise.IsChecked = !exercise.IsChecked;
            if (exercise.IsChecked)
            {
                if (!_selectedExercises.Contains(exercise))
                {
                    _selectedExercises.Add(exercise);
                    SearchExerciseContador++;
                    ShowNewTrainingButton = true;
                    NewTrainingButtonText = SearchExerciseContador > 1 ? $"Añadir {SearchExerciseContador} ejercicios" : "Añadir ejercicio";
                }
            }
            else if (!exercise.IsChecked)
            {
                SearchExerciseContador--;
                _selectedExercises.Remove(exercise);


                NewTrainingButtonText = SearchExerciseContador > 1 ? $"Añadir {SearchExerciseContador} ejercicios" : "Añadir ejercicio";


            }
            if (SearchExerciseContador == 0)
            {
                ShowNewTrainingButton = false;
            }
        }

        [RelayCommand]
        private void CalendarExerciseTapped(ExerciseModelView exercise)
        {
            exercise.IsChecked = !exercise.IsChecked;
            if (exercise.IsChecked)
            {
                MiContador++;
                IsDeleteExerciseButtonVisible = true;
                IsDetailsExerciseButtonVisible = true;
            }
            else if (!exercise.IsChecked)
            {
                MiContador--;

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
                MiCustomContador++;
                IsDeleteCustomExerciseButtonVisible = true;
            }
            else if (!exercise.IsChecked)
            {
                MiCustomContador--;

            }
            if (MiCustomContador == 0)
            {
                IsDeleteCustomExerciseButtonVisible = false;
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
                IsDeleteCustomExerciseButtonVisible = false;
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
            GetUserCredentials();


        }

        [RelayCommand]
        public async Task GetUserCredentials()
        {
            
            var avatar = await CrossCloudFirestore.Current
                            .Instance
                            .Collection("Users")
                            .Document(Email) //LoginViewModel.Email_name
                            .GetAsync();
            User = avatar.ToObject<UserModel>();
           

            AvatarImage = User.Avatar;
            Email = User.Email;
            Name = User.Name;
            Reps = User.Reps;
            Weight = User.Weight;
            Break = User.Break;
            NotificationOutside = User.NotificationOutside;
            YeahBuddy = User.Yeahbuddy;
            Whatsapp = User.Whatsapp;
            VideoPlaying = User.VideoPlaying;
            
        }


        //Filtra el texto del search bar en el CustomExercisePopup
        public void UpdateFilteredExercises()
        {
            // Obtén la cadena de búsqueda o establece una cadena vacía si es nula
            string searchQuery = SearchText?.ToLower() ?? string.Empty; //Si el resultado anterior es nulo, utiliza una cadena vacía 
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                ShowExercisesWithAvailableEquipment();

            }

            else
            {
                var searchTerms = searchQuery.Split(' ',StringSplitOptions.RemoveEmptyEntries);
                var filteredExercises = ExerciseList.Where(exercise =>
                     searchTerms.All(term => exercise.Name?.ToLower().Contains(term) == true)).ToList();
                foreach (var exercise in filteredExercises)
                {
                    exercise.IsChecked = _selectedExercises.Contains(exercise);
                }

                ExerciseList = new ObservableCollection<ExerciseModelView>(filteredExercises);
            }
        }

        public void FilterAvailableEquipment()
        {
            string searchQuery = EquipmentSearchText?.ToLower() ?? string.Empty; //Si el resultado anterior es nulo, utiliza una cadena vacía 
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                LoadUserEquipments();

            }

            else
            {
                var searchTerms = searchQuery.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                EquipmentList = new ObservableCollection<EquipmentModelView>(
                EquipmentList.Where(equipment => searchTerms.All(term => equipment.Name?.ToLower().Contains(term) == true))
                );
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
          await  ShowExercisesWithAvailableEquipment();

            CustomExercisePopup = new CustomExercises();
            await App.Current.MainPage.ShowPopupAsync(CustomExercisePopup);
        }

        [RelayCommand]
        public async Task ClosecustomExercisePopup()
        {
            SearchText = "";
            //await SelectedDurationWorkout();
            await SelectDurationWorkoutRandom("15");
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
        public async Task OpenProfilePopup()
        {

            ProfilePopup = new ProfilePopup();
            await App.Current.MainPage.ShowPopupAsync(ProfilePopup);
        }

        [RelayCommand]
        public async Task CloseProfilePopup()
        {
            ProfilePopup.Close();

        }

        [RelayCommand]
        public async Task OpenConfigurationPopup()
        {

            ConfigurationPopup = new ConfigurationPopup();
            await App.Current.MainPage.ShowPopupAsync(ConfigurationPopup);
        }

        [RelayCommand]
        public async Task CloseConfigurationPopup()
        {
            ConfigurationPopup.Close();

        }


        [RelayCommand]
        public async Task UpdateUserCredentials()
        {
            if (!ValidateName())
            {
                return;
            }

            var userDocument = CrossCloudFirestore.Current
               .Instance
               .Collection("Users")
               .Document(Email);

            // Agregar el nombre del workout al campo Colecciones del documento del usuario
            await userDocument.UpdateDataAsync(new { Name = Name });

            CloseProfilePopup();
        }


        [RelayCommand]
        private async Task ResetPassword()
        {

            // var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            // await authProvider.SendPasswordResetEmailAsync(Email);
            PasswordText = "Correo enviado !";
            EmailSentLabel = "Puedes volver a solicitar otro cambio dentro de 10 minutos.";

            IsResetPassEnabled = false;

            LastResetTime = DateTime.Now;

            // Iniciar el temporizador para habilitar el botón después de 10 minutos
            await Task.Delay(TimeSpan.FromMinutes(1));
            TimeSpan elapsedTime = DateTime.Now - LastResetTime; 
            if (elapsedTime.TotalMinutes >= 1)
            {
                IsResetPassEnabled = true;
                PasswordText = "Restablecer contraseña";
                EmailSentLabel = "";
            }
        }

        private bool ValidateName()
        {
            if (Name == null || Name.Any(Char.IsWhiteSpace))
            {
                HasNameError = true;
                NameErrorText = "Se te olvidó el nombre wey!";
                return false;
            }

            if (Name.Length >= 1 && Name.Length <= 15)
            {
                HasNameError = false;
                NameErrorText = "";
                return true;
            }
            else
            {
                NameErrorText = "Debe tener entre 1 y 15 caracteres!";
                HasNameError = true;
                return false;
            }

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

        [RelayCommand]
        public async Task LoadSavedWorkouts()
        {
            await Shell.Current.GoToAsync("//SavedWorkoutsPage", new Dictionary<string, object>()
            {
                ["Email"] = Email
            });

        }
    }
}
