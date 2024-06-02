using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using mobileAppTest.Mappers;
using mobileAppTest.Models;
using mobileAppTest.Views.Popups;
using Mopups.Services;
using Plugin.CloudFirestore;
using Plugin.LocalNotification;
using Plugin.Maui.Audio;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace mobileAppTest.ViewModels
{
    [QueryProperty("MyExercise", "MyExercise")]
    [QueryProperty("IsVisible", "IsVisible")]
    [QueryProperty("Email", "Email")]
    [QueryProperty("IsBackButtonVisible", "IsBackButtonVisible")]
    [QueryProperty("ExerciseList", "ExerciseList")]

    public partial class ExerciseTappedViewModel : ObservableObject
    {
        [ObservableProperty]
        private UserModel _user;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        public ExerciseModel _myExercise;

        private ExerciseBreakMopup exerciseBreakMopup { get; set; }

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _exerciseList;

        [ObservableProperty]
        private ObservableCollection<ExerciseModel> _exercises;

        [ObservableProperty]
        private ObservableCollection<EquipmentModelView> _equipmentList;

        [ObservableProperty]
        private ExerciseModelView _exerciseView;

        [ObservableProperty]
        private ObservableCollection<ExerciseModelView> _exerciseEntries;

        [ObservableProperty]
        public ObservableCollection<ExerciseModelView> _seriesRegistered;


        [ObservableProperty]
        public ObservableCollection<ExerciseModelView> _exerciseFinishedList;

        [ObservableProperty]
        public ObservableCollection<ExerciseModelView> _uniquePrimaryMuscles;

        private DateTime time;
        private DateTime timeExercise;

        [ObservableProperty]
        private bool _isVisible; 

        [ObservableProperty]
        private bool _isBackButtonVisible;

        [ObservableProperty]
        private bool _isVideoPlaying;

        private bool _isRunning;
        private bool _isExerciseRunning;

        [ObservableProperty]
        private bool _isGifVisible;

        [ObservableProperty]
        private bool _isSerieEnabled;

        [ObservableProperty]
        private bool _isRemoveSerieEnabled = false;

        [ObservableProperty]
        private bool _isRegisterSerieEnabled;

        [ObservableProperty]
        private string _timeLabel;

        [ObservableProperty]
        private ImageSource _seriesGif;

        [ObservableProperty]
        private int _seriesCounter;

        [ObservableProperty]
        private double[] _reps = {10};

        [ObservableProperty]
        private double[] _weight = { 12.5 };

        [ObservableProperty]
        private double _defaultrep;

        [ObservableProperty]
        private double _defaultWeight;

        List<double> repsList = new List<double>();
        List<double> weightList = new List<double>();

        [ObservableProperty]
        private int _segundosDescanso;

        [ObservableProperty]
        private string _durationHeader;

        [ObservableProperty]
        private bool _durationExpander;

        [ObservableProperty]
        private string _exerciseTime;

        [ObservableProperty]
        private string _fecha;

        [ObservableProperty]
        private bool _isFinishedButtonEnabled;

        [ObservableProperty]
        private bool _isVideoVisible;

        [ObservableProperty]
        private  IAudioManager _audioManager = new AudioManager();

        [ObservableProperty]
        private bool _gifPlaying;

        public ExerciseTappedViewModel()
        {
            User = new UserModel();
            ExerciseView = new ExerciseModelView();
            ExerciseEntries = new ObservableCollection<ExerciseModelView>();
            SeriesRegistered = new ObservableCollection<ExerciseModelView>();
            ExerciseFinishedList = new ObservableCollection<ExerciseModelView>();
            UniquePrimaryMuscles = new ObservableCollection<ExerciseModelView>();
            exerciseBreakMopup = new ExerciseBreakMopup(this);
            IsSerieEnabled = true;
            //AddSeriesRow();
            IsVideoPlaying = true; //code behind start video
            IsVisible = false;
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
            Reps[0]= User.Reps;
            Weight[0] = User.Weight;
            SegundosDescanso = User.Break;
            DurationHeader = SegundosDescanso.ToString();
            IsVideoVisible = User.VideoPlaying;

        }

        [RelayCommand]
        public async Task AddSeriesRow()
        {

            ExerciseModelView newEntry = new ExerciseModelView
            {
                Reps = Reps,
                Weight = Weight
            };
            ExerciseEntries.Add(newEntry);

            SeriesCounter++;
            IsRegisterSerieEnabled = true;
            IsFinishedButtonEnabled = false;
            IsSerieEnabled = false;
            GifPlaying = true; 
        }


        [RelayCommand]
        public void LogSeries()
        {
            SeriesGif = "seriescompleted.gif";
            StartStopwatch();
            IsGifVisible = true;
            OpenExerciseBreakMopupAsync();
            IsSerieEnabled = true;
            SeriesRegistered = ExerciseEntries;
            IsRegisterSerieEnabled = false;
            IsFinishedButtonEnabled = true;
            repsList.AddRange(Reps);
            weightList.AddRange(Weight);
        }

   
        private void UpdateUI()
        {
            time = time.Add(TimeSpan.FromSeconds(1));

            TimeLabel = time.ToString("ss");
            var sec = SegundosDescanso - 5;

            if (time.Second >= sec)
            {
                Send_Notification();
            }

            if (time.Second >= SegundosDescanso || time.Second == 0)
            {
                //Play_Sound();
                StopStopwatch();
                CloseExerciseBreakMopup();
            }
        }


     

        [RelayCommand]
        public async void Send_Notification()
        {
            if (!User.NotificationOutside)
            {
                return;
            }

            //var player = AudioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("rick.mp3"));
            //player.Play();
            //await Task.Delay(4000);
            var request = new NotificationRequest
            {
                NotificationId = 1337,
                Title = "Tu descanso ha finalizado",
                Subtitle = SegundosDescanso.ToString()+"''",
                Description = "",
                //Sound = null,
                BadgeNumber = 1,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now,

                },
                Android = new Plugin.LocalNotification.AndroidOption.AndroidOptions
                {
                    Color = new Plugin.LocalNotification.AndroidOption.AndroidColor(System.Drawing.Color.Blue.ToArgb()),
                    LedColor = System.Drawing.Color.Yellow.ToArgb(),

                },

                iOS = new Plugin.LocalNotification.iOSOption.iOSOptions
                {
                    PlayForegroundSound = true

                }
            };
            await LocalNotificationCenter.Current.Show(request);
           // player.Dispose();
        }

        [RelayCommand]
        public async Task StartVideo()
        {
            if (User.VideoPlaying)
            {
                IsVideoPlaying = true;

            }
        }


    

        public static int ConvertStopwatchLabelToMinutes(string stopwatchLabel)
        {
            if (TimeSpan.TryParse(stopwatchLabel, out TimeSpan duration))
            {
                return (int)duration.TotalMinutes;
            }
            return 0; // Default value or handle the conversion failure
        }

        public async Task NewTrainingSession()
        {
            //Tiempo por Ejercicio parseado a minutos:
            MyExercise.Duration = ConvertStopwatchLabelToMinutes(ExerciseTime).ToString();
            MyExercise.FechaEntrenamiento = DateTime.Now.Date.ToString("dd/MM/yyyy");
            Fecha = MyExercise.FechaEntrenamiento;

            // Assign the arrays to MyExercise
            MyExercise.Reps = repsList.ToArray();
            MyExercise.Weight = weightList.ToArray();
            // Crear una subcolección dentro de "Users" con el nombre Sesiones
            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document(Email); 

            await userDocument
                         .Collection("Sesiones")
                         .AddAsync(MyExercise);

            repsList.Clear();
            weightList.Clear();

        }


        [RelayCommand]
        public async Task SelectDurationBrak(string segundos)
        {

            if (segundos == "15")
            {
                DurationHeader = "15";
                TimeLabel = "15";
                SegundosDescanso = 15;

            }
            else if (segundos == "30")
            {
                DurationHeader = "30";
                TimeLabel = "30";
                SegundosDescanso = 30;
            }
            else if (segundos == "45")
            {
                DurationHeader = "45";
                TimeLabel = "45";
                SegundosDescanso = 45;
            }
            else if (segundos == "60")
            {
                DurationHeader = "60";
                TimeLabel = "60";
                SegundosDescanso = 60;
            }
            else if (segundos == "90")
            {
                DurationHeader = "90";
                TimeLabel = "90";
                SegundosDescanso = 90;

            }
            else if (segundos == "120")
            {
                DurationHeader = "120";
                TimeLabel = "120";
                SegundosDescanso = 120;

            }
            else if (segundos == "150")
            {
                DurationHeader = "150";
                TimeLabel = "150";
                SegundosDescanso = 150;
            }
            DurationExpander = false;

        }


  

        [RelayCommand]
        public async Task OpenExerciseBreakMopupAsync()
        {
            await MopupService.Instance.PushAsync(new ExerciseBreakMopup(this));

        }

        [RelayCommand]
        public async Task CloseExerciseBreakMopup()
        {
            await MopupService.Instance.PopAllAsync();
            StopStopwatch();
            IsGifVisible = false;
            IsSerieEnabled = true;
        }


        [RelayCommand]
        public void StartStopwatch()
        {
            IsVisible = true;
            if (_isRunning == false)
            {
                time = new();
                TimeLabel = "0";
                _isRunning = true;

                Task.Run(async () =>
                {
                    while (_isRunning)
                    {
                        // Esperar 1 segundo
                        Task.Delay(1000).Wait();

                        // Actualizar el tiempo y la etiqueta de tiempo en el hilo de la interfaz de usuario
                        UpdateUI();
                    }
                });
            }
        }

        [RelayCommand]
        public void StopStopwatch()
        {
            TimeLabel = "0";
            _isRunning = false;

        }
     




        [RelayCommand]
        public void RemoveLastSeries()
        {
            if (ExerciseEntries.Count > 0)
            {
                // Remover el último elemento de la colección
                ExerciseEntries.RemoveAt(ExerciseEntries.Count - 1);

            }
            if (repsList.Count == 0)
            {
                return;
            }
            repsList.RemoveAt(repsList.Count-1);
            weightList.RemoveAt(weightList.Count-1);
            IsRemoveSerieEnabled = false;
            if (SeriesCounter > 0)
            {
                SeriesCounter--;
            }
        }


        [RelayCommand]
        public async Task NavegarMainPage()
        {
            if (!IsFinishedButtonEnabled)
            {
                await App.Current.MainPage.DisplayAlert("Info", "Registra primero la serie antes de volver", "Aceptar");
                return;
            }

            IsVideoPlaying = false;
            SeriesGif = "";

            if (SeriesRegistered.Count == 0)
            {
                    ExerciseEntries.Clear();
                    SeriesRegistered.Clear();
                    IsSerieEnabled = true;
                    SeriesCounter = 0;

                    if (IsVisible)
                    {
                        timeExercise = new();
                        ExerciseTime = "00:00:00";
                        _isExerciseRunning = false;
                        await Shell.Current.GoToAsync("//StartedWorkoutPage", new Dictionary<string, object>()
                        {
                            ["ExerciseList"] = ExerciseList,
                            ["Email"] = Email,
                            ["UniquePrimaryMuscles"] = UniquePrimaryMuscles,
                            ["ExerciseFinishedList"] = ExerciseFinishedList,
                        });
                    }
                    else
                    {
                        timeExercise = new();
                        ExerciseTime = "00:00:00";
                        _isExerciseRunning = false;
                        await Shell.Current.GoToAsync("//MainPage", new Dictionary<string, object>()
                        {
                            ["Email"] = Email

                        });
                }
            }
            else
            {
                // Find the ExerciseModelView instance corresponding to MyExercise
                ExerciseModelView exerciseModelViewToRemove = ExerciseList.FirstOrDefault(ex => ex.Exercise.Name == MyExercise.Name);
                
                if (exerciseModelViewToRemove != null)
                {
                    ExerciseModelView exerciseFinishedModel = new ExerciseModelView
                    {
                        Exercise = MyExercise,
                        Reps = repsList.ToArray(),
                        Weight = weightList.ToArray()
                    };
                    ExerciseFinishedList.Add(exerciseFinishedModel);
                    foreach (var exerciseModelView in ExerciseList)
                    {
                        // Ensure Exercise is not null
                        if (exerciseModelView.Exercise != null && exerciseModelView.Exercise.Name == MyExercise.Name)
                        {
                            // Check if PrimaryMuscles is already added
                            var existingItem = UniquePrimaryMuscles.FirstOrDefault(item => item.Exercise?.PrimaryMuscles == exerciseModelView.Exercise.PrimaryMuscles);

                            if (existingItem == null)
                            {
                                UniquePrimaryMuscles.Add(exerciseModelView);
                            }
                        }
                    
                    }
             

                    ExerciseList.Remove(exerciseModelViewToRemove);
                    NewTrainingSession();
                    ExerciseEntries.Clear();
                    SeriesRegistered.Clear();
                    IsSerieEnabled = true; //confirmarEnabledFlase
                    IsRegisterSerieEnabled = false;
                    SeriesCounter = 0;
                    Reps[0] = User.Reps;
                    Weight[0] = User.Weight;
                    repsList.Clear();
                    weightList.Clear();

                    if (ExerciseList.Count == 0)
                    {

                        //await App.Current.MainPage.DisplayAlert("Info", "Ejercicio " + MyExercise.Name + " registrado!", "ACEPTAR");
                        timeExercise = new();
                        time = new(); //new code
                        ExerciseTime = "00:00:00";
                        _isExerciseRunning = false;

                        await Shell.Current.GoToAsync("//FinishedWorkoutPage", new Dictionary<string, object>()
                        {
                            ["ExerciseFinishedList"] = ExerciseFinishedList,
                            ["Fecha"] = Fecha,
                            ["Email"] = Email,
                            ["UniquePrimaryMuscles"] = UniquePrimaryMuscles


                        });
                        return;
                    }

                    if (IsVisible)
                    {
                        //await App.Current.MainPage.DisplayAlert("Info", "Ejercicio "+MyExercise.Name+" registrado!", "ACEPTAR");
                        timeExercise = new();
                        ExerciseTime = "00:00:00";
                        _isExerciseRunning = false;

                        await Shell.Current.GoToAsync("//StartedWorkoutPage", new Dictionary<string, object>()
                        {
                            ["ExerciseList"] = ExerciseList,
                            ["ExerciseFinishedList"] = ExerciseFinishedList,
                            ["Fecha"] = Fecha,
                            ["Email"] = Email,
                            ["UniquePrimaryMuscles"] = UniquePrimaryMuscles
                        });
                    }
                    else
                    {
                        timeExercise = new();
                        ExerciseTime = "00:00:00";
                        _isExerciseRunning = false;
                        await Shell.Current.GoToAsync("//MainPage", new Dictionary<string, object>()
                        {
                            ["Email"] = Email

                        });
                    }
                }
            }
        }

        [RelayCommand]
        public async Task LoadMainPage()
        {
            _isExerciseRunning = false;
            IsVisible = true;
            timeExercise = new ();
            ExerciseTime = "00:00:00";
            IsVideoPlaying = false;
            await Shell.Current.GoToAsync("//MainPage", new Dictionary<string, object>()
            {
                ["Email"] = Email

            });
        }
     

        [RelayCommand]
        public void StartStopwatchExercise()
        {
            if (_isExerciseRunning == false)
            {
                timeExercise = new();
                ExerciseTime = "00:00:00";
                _isExerciseRunning = true;

                Task.Run(async () =>
                {
                    while (_isExerciseRunning)
                    {
                        // Esperar 1 segundo
                        await Task.Delay(1000);

                        // Actualizar el tiempo y la etiqueta de tiempo en el hilo de la interfaz de usuario
                        UpdateUIExercise();
                    }
                });
            }
        }

        [RelayCommand]
        public void StopStopwatchExercise()
        {
            //time = new();
            ExerciseTime = "00:00:00";
            _isExerciseRunning = false;

        }


        private void UpdateUIExercise()
        {
            timeExercise = timeExercise.Add(TimeSpan.FromSeconds(1));
            ExerciseTime = timeExercise.TimeOfDay.ToString();
        }





    }
}



