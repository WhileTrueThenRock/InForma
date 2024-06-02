using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using mobileAppTest.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace mobileAppTest.ViewModels
{
    internal partial class RegisterViewModel : ObservableObject, INotifyPropertyChanged
    {
        public string webApiKey = "AIzaSyAJ2z8_aTTkgCz-dYXhLt-bt4nEcXqjPxY";

        [ObservableProperty]
        private ObservableCollection<IntroScreenModel> _introScreenModels;

        public event PropertyChangedEventHandler PropertyChanged;


        private int _position;

        private string _nextTextButton;

        public int Position
        {
            get => _position;
            set
            {
                if (_position != value)
                {
                    _position = value;
                    OnPropertyChanged(nameof(Position));
                    UpdateButtonText();
                }
            }
        }

        public string NextTextButton
        {
            get => _nextTextButton;
            set
            {
                if (_nextTextButton != value)
                {
                    _nextTextButton = value;
                    OnPropertyChanged(nameof(NextTextButton));
                }
            }
        }

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private bool _emailOK;

        [ObservableProperty]
        private bool _passOK;

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
        private string _passErrorText;

        [ObservableProperty]
        private string _nameErrorText;

        private bool _hasNoEquipment;
        public bool HasNoEquipment
        {
            get => _hasNoEquipment;
            set
            {
                _hasNoEquipment = value;
                OnPropertyChanged(nameof(HasNoEquipment));
            }
        }





        private bool _isCollectionVisible;
        public bool IsCollectionVisible
        {
            get => _isCollectionVisible;
            set
            {
                _isCollectionVisible = value;
                OnPropertyChanged(nameof(IsCollectionVisible));
            }
        }


        private bool _isShimmerPlaying;
        public bool IsShimmerPlaying
        {
            get => _isShimmerPlaying;
            set
            {
                _isShimmerPlaying = value;
                OnPropertyChanged(nameof(IsShimmerPlaying));
            }
        }


        public RegisterViewModel()
        {

            IsCollectionVisible = true;
            IntroScreenModels = new ObservableCollection<IntroScreenModel>();
            NextTextButton = "Siguiente";

            IntroScreenModels.Add(new IntroScreenModel()
            {
                titleVisible = true,
                EquipmentVisible = false,
                CollectionVisible = 50

            });

      

            IntroScreenModels.Add(new IntroScreenModel()
            {
                titleVisible = false,
                EquipmentVisible = true,
                EquipmentTitle = "Elige tu equipamiento:",

                EquipmentList = new ObservableCollection<EquipmentModel>
                {
                new EquipmentModel { name = "Banco", disponible = true, url = "Images/Equipment/bancohorizontal.jpg" },
                new EquipmentModel { name = "Banco Declinado", disponible = true, url = "Images/Equipment/bancodeclinado.jpg" },
                new EquipmentModel { name = "Banco Inclinado", disponible = true, url = "Images/Equipment/bancoinclinado.jpg" },
                new EquipmentModel { name = "Banco Romano", disponible = true, url = "Images/Equipment/bancoromano.jpg" },
                new EquipmentModel { name = "Banco Scott", disponible = true, url = "Images/Equipment/bancoscott.jpg" },
                new EquipmentModel { name = "Barras", disponible = true, url = "Images/Equipment/barras.jpg" },
                new EquipmentModel { name = "Barra T", disponible = true, url = "Images/Equipment/landmine.jpg" },
                new EquipmentModel { name = "Barra Z", disponible = true, url = "Images/Equipment/barraz.jpg" },
                new EquipmentModel { name = "Rueda Abdominal", disponible = true, url = "Images/Equipment/ruedaabd.jpg" },
                new EquipmentModel { name = "Soga", disponible = true, url = "Images/Equipment/soga.jpg" },
                new EquipmentModel { name = "Soporte Dominadas", disponible = true, url = "Images/Equipment/dominadas.jpg" },
                new EquipmentModel { name = "Mancuernas", disponible = true, url = "Images/Equipment/dumbbells.jpg" },
                new EquipmentModel { name = "Máquinas Convergentes", disponible = true, url = "Images/Equipment/maquinaconvergente.jpg" },
                new EquipmentModel { name = "Máquina de Poleas", disponible = true, url = "Images/Equipment/maquinapoleas.jpg" },
                new EquipmentModel { name = "Máquina Scott", disponible = true, url = "Images/Equipment/maquinascottplacas.jpg" },
                new EquipmentModel { name = "Máquina Patada Glúteos", disponible = true, url = "Images/Equipment/maquinapatada.jpg" },
                new EquipmentModel { name = "Máquina Abductores y Aductores", disponible = true, url = "Images/Equipment/maquinaaductores.jpg" },
                new EquipmentModel { name = "Máquina Trapecio", disponible = true, url = "Images/Equipment/maquinatrapecio.jpg" },
                new EquipmentModel { name = "Máquina Hip Thrust", disponible = true, url = "Images/Equipment/maquinahip.jpg" },
                new EquipmentModel { name = "Máquina Hombros Press", disponible = true, url = "Images/Equipment/maquinahombrospress.jpg" },
                new EquipmentModel { name = "Máquina Hombros Laterales", disponible = true, url = "Images/Equipment/maquinahombroslat.jpg" },
                new EquipmentModel { name = "Máquina para gemelos de pie", disponible = true, url = "Images/Equipment/maquinagemelospie.jpg" },
                new EquipmentModel { name = "Máquina para gemelos Tipo Burro", disponible = true, url = "Images/Equipment/maquinaburro.jpg" },
                new EquipmentModel { name = "Máquina Multipower", disponible = true, url = "Images/Equipment/maquinamulti.jpg" },
                new EquipmentModel { name = "Máquina Aperturas", disponible = true, url = "Images/Equipment/maquinaaperturas.jpg" },
                new EquipmentModel { name = "Máquina Remo Alto", disponible = true, url = "Images/Equipment/maquinaremoalto.jpg" },
                new EquipmentModel { name = "Máquina Remo", disponible = true, url = "Images/Equipment/maquinaremo.jpg" },
                new EquipmentModel { name = "Máquina Remo Bajo", disponible = true, url = "Images/Equipment/maquinaremobajo.jfif" },
                new EquipmentModel { name = "Máquina Fondos", disponible = true, url = "Images/Equipment/maquinafondos.jpg" },
                new EquipmentModel { name = "Máquina Asistida", disponible = true, url = "Images/Equipment/maquinaasistida.jpg" },
                new EquipmentModel { name = "Máquina Sentadillas", disponible = true, url = "Images/Equipment/maquinasentadillas.png" },
                new EquipmentModel { name = "Máquina Femoral Tumbado", disponible = true, url = "Images/Equipment/maquinafemoraltumbado.jpg" },
                new EquipmentModel { name = "Máquina Femoral Sentado", disponible = true, url = "Images/Equipment/maquinafemoralsentado.jpg" },
                new EquipmentModel { name = "Máquina Femoral de Pie", disponible = true, url = "Images/Equipment/maquinafemoraldepie.jpg" },
                new EquipmentModel { name = "Máquina Extensiones Cuádriceps", disponible = true, url = "Images/Equipment/maquinaquads.jpg" },
                new EquipmentModel { name = "Máquina Press Pierna", disponible = true, url = "Images/Equipment/maquinaprensa.jpg" },
                new EquipmentModel { name = "Máquina Press Pierna Horizontal", disponible = true, url = "Images/Equipment/maquinapresshoriz.jpg" },
                new EquipmentModel { name = "Sin Equipamiento", disponible = false, url = "Images/Equipment/sinequipamiento.png" },


                },
                CollectionVisible = 500


            });



            IntroScreenModels.Add(new IntroScreenModel()
            {
                titleVisible = false,
                EquipmentVisible = false,
                RegisterVisible = true,
                RegisterTitle = "Por último, tus datos:",
                CollectionVisible = 0

            });
        }


        public async Task StartShimmerAndWait()
        {
            IsShimmerPlaying = true;

            await Task.Delay(5000);
            IsShimmerPlaying = false;
        }

        [RelayCommand]
        public void EquipmentTapped()
        {
            HasNoEquipment = !HasNoEquipment;
            if (!HasNoEquipment)
            {
                IsCollectionVisible = true;
            }
            else
            {
                IsCollectionVisible = false;
            }
       
        }


        // Método para agregar equipamientos a la subcolección "User"
        [RelayCommand]
        public async Task AddEquipmentsToUser()
        {
            ObservableCollection<EquipmentModel> equipments;


            if (HasNoEquipment)
            {
                equipments = new ObservableCollection<EquipmentModel>
            {
                new EquipmentModel { name = "Sin Equipamiento", disponible = true, url = "Images/Equipment/sinequipamiento.png" },
                //Resto del equipamiento a false
                new EquipmentModel { name = "Banco", disponible = false, url = "Images/Equipment/bancohorizontal.jpg" },
                new EquipmentModel { name = "Banco Declinado", disponible = false, url = "Images/Equipment/bancodeclinado.jpg" },
                new EquipmentModel { name = "Banco Inclinado", disponible = false, url = "Images/Equipment/bancoinclinado.jpg" },
                new EquipmentModel { name = "Banco Romano", disponible = false, url = "Images/Equipment/bancoromano.jpg" },
                new EquipmentModel { name = "Banco Scott", disponible = false, url = "Images/Equipment/bancoscott.jpg" },
                new EquipmentModel { name = "Barras", disponible = false, url = "Images/Equipment/barras.jpg" },
                new EquipmentModel { name = "Barra T", disponible = false, url = "Images/Equipment/landmine.jpg" },
                new EquipmentModel { name = "Barra Z", disponible = false, url = "Images/Equipment/barraz.jpg" },
                new EquipmentModel { name = "Rueda Abdominal", disponible = false, url = "Images/Equipment/ruedaabd.jpg" },
                new EquipmentModel { name = "Soga", disponible = false, url = "Images/Equipment/soga.jpg" },
                new EquipmentModel { name = "Soporte Dominadas", disponible = false, url = "Images/Equipment/dominadas.jpg" },
                new EquipmentModel { name = "Mancuernas", disponible = false, url = "Images/Equipment/dumbbells.jpg" },
                new EquipmentModel { name = "Máquinas Convergentes", disponible = false, url = "Images/Equipment/maquinaconvergente.jpg" },
                new EquipmentModel { name = "Máquina de Poleas", disponible = false, url = "Images/Equipment/maquinapoleas.jpg" },
                new EquipmentModel { name = "Máquina Scott", disponible = false, url = "Images/Equipment/maquinascottplacas.jpg" },
                new EquipmentModel { name = "Máquina Patada Glúteos", disponible = false, url = "Images/Equipment/maquinapatada.jpg" },
                new EquipmentModel { name = "Máquina Abductores y Aductores", disponible = false, url = "Images/Equipment/maquinaaductores.jpg" },
                new EquipmentModel { name = "Máquina Trapecio", disponible = false, url = "Images/Equipment/maquinatrapecio.jpg" },
                new EquipmentModel { name = "Máquina Hip Thrust", disponible = false, url = "Images/Equipment/maquinahip.jpg" },
                new EquipmentModel { name = "Máquina Hombros Press", disponible = false, url = "Images/Equipment/maquinahombrospress.jpg" },
                new EquipmentModel { name = "Máquina Hombros Laterales", disponible = false, url = "Images/Equipment/maquinahombroslat.jpg" },
                new EquipmentModel { name = "Máquina para gemelos de pie", disponible = false, url = "Images/Equipment/maquinagemelospie.jpg" },
                new EquipmentModel { name = "Máquina para gemelos Tipo Burro", disponible = false, url = "Images/Equipment/maquinaburro.jpg" },
                new EquipmentModel { name = "Máquina Multipower", disponible = false, url = "Images/Equipment/maquinamulti.jpg" },
                new EquipmentModel { name = "Máquina Aperturas", disponible = false, url = "Images/Equipment/maquinaaperturas.jpg" },
                new EquipmentModel { name = "Máquina Remo Alto", disponible = false, url = "Images/Equipment/maquinaremoalto.jpg" },
                new EquipmentModel { name = "Máquina Remo", disponible = false, url = "Images/Equipment/maquinaremo.jpg" },
                new EquipmentModel { name = "Máquina Remo Bajo", disponible = false, url = "Images/Equipment/maquinaremobajo.jfif" },
                new EquipmentModel { name = "Máquina Fondos", disponible = false, url = "Images/Equipment/maquinafondos.jpg" },
                new EquipmentModel { name = "Máquina Asistida", disponible = false, url = "Images/Equipment/maquinaasistida.jpg" },
                new EquipmentModel { name = "Máquina Sentadillas", disponible = false, url = "Images/Equipment/maquinasentadillas.png" },
                new EquipmentModel { name = "Máquina Femoral Tumbado", disponible = false, url = "Images/Equipment/maquinafemoraltumbado.jpg" },
                new EquipmentModel { name = "Máquina Femoral Sentado", disponible = false, url = "Images/Equipment/maquinafemoralsentado.jpg" },
                new EquipmentModel { name = "Máquina Femoral de Pie", disponible = false, url = "Images/Equipment/maquinafemoraldepie.jpg" },
                new EquipmentModel { name = "Máquina Extensiones Cuádriceps", disponible = false, url = "Images/Equipment/maquinaquads.jpg" },
                new EquipmentModel { name = "Máquina Press Pierna", disponible = false, url = "Images/Equipment/maquinaprensa.jpg" },
                new EquipmentModel { name = "Máquina Press Pierna Horizontal", disponible = false, url = "Images/Equipment/maquinapresshoriz.jpg" },
            };



            }
            else
            {
                var equipmentScreen = IntroScreenModels.FirstOrDefault(model => model.EquipmentList != null);
                if (equipmentScreen == null)
                {
                    return;
                }
                equipments = equipmentScreen.EquipmentList;
            }
        

            //var equipments = new List<EquipmentModel>
            //{
      
            //};

            // Referencia al documento del usuario

            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document(Email);

            // Iterar sobre la lista de equipamientos y agregar documentos a la subcolección "equipments"
            foreach (var equipment in equipments)
            {

                // Agregar el documento del equipamiento a la subcolección "equipments"
                userDocument
                   .Collection("Equipment")
                   .Document(equipment.name)
                   .SetAsync(equipment);
            }
        }


            [RelayCommand]
        public async Task NextSlide()
        {

            if(Position < 1)
            {
                Position += 1;


            }
            else if(Position == 1) 
            {
                Position += 1;
            }

            else
            {
                IsShimmerPlaying = true;
                //AddEquipmentsToUser();
                RegisterUserTappedAsync();
                IsShimmerPlaying = false;


            }


        }
        private void UpdateButtonText()
        {
            if (Position < 2)
            {
                NextTextButton = "Siguiente";
            }
            else if (Position == 2)
            {
                NextTextButton = "Empezar";
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private bool ValidateName()
        {
            if (string.IsNullOrEmpty(Name) || Name.Trim().Length == 0)
            {
                HasNameError = true;
                NameErrorText = "Introduce un nombre";
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

        private bool ValidatePattern(string email)
        {

            if (string.IsNullOrEmpty(Name) || Name.Trim().Length == 0)
            {
                HasEmailError = true;
                EmailErrorText = "Introduce un email!";
                EmailOK = false;
                return false;
            }

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email,pattern);
        }

        private async Task ValidateEmail()
        {
            HasEmailError = true;
            EmailOK = false;

            if (!ValidatePattern(Email))
            {
                return;
            }


            HasEmailError = false;
            EmailOK = true;

        }

        private bool ValidatePass()
        {
            if (Password == null || Password.Any(Char.IsWhiteSpace))
            {
                HasPassError = true;
                PassErrorText = "La contraseña no debe tener espacios";
                return false;
            }

            if (Password.Length >= 6 && Password.Length <= 10)
            {
                HasPassError = false;
                PassErrorText = "";
                return true;
            }
            else
            {
                HasPassError = true;
                PassErrorText = "La contraseña debe tener entre 6 y 10 caracteres!";
                return false;
            }

        }

        public async Task CheckStoredCredentials()
        {
            IsShimmerPlaying = true;
             await Task.Delay(500);
            bool credentialsStored = await SecureStorage.GetAsync("credentialsStored") == "true";
            if (credentialsStored)
            {
                Email = await SecureStorage.GetAsync("username");
                await Shell.Current.GoToAsync("//MainPage", new Dictionary<string, object>()
                {
                    ["Email"] = Email
                });

               
                string password = await SecureStorage.GetAsync("password");

                // Lógica para iniciar sesión automáticamente con las credenciales guardadas
            }
            else
            {
                

            }
            IsShimmerPlaying = false;

        }



        [RelayCommand]
        private async void RegisterUserTappedAsync()
        {
            try
            {
                if (!ValidateName())
                {
                    await App.Current.MainPage.DisplayAlert("Info", NameErrorText, "ACEPTAR");
                    return;
                }

                await ValidateEmail();
                if (!EmailOK)
                {
                    await App.Current.MainPage.DisplayAlert("Info", "Email inválido", "ACEPTAR");
                    return;
                }

                if (!ValidatePass())
                {
                    await App.Current.MainPage.DisplayAlert("Info", PassErrorText, "ACEPTAR");
                    return;
                }



                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password);
                string token = auth.FirebaseToken;

                if (token!=null)
                {
                    UserModel user = new UserModel();
                    user.Name = Name;
                    user.Email = Email;
                    user.Avatar = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Avatars%2F1000000034.png?alt=media&token=0821cf94-912f-43cc-9240-a286788e6af9";
                    user.Reps = 10;
                    user.Weight = 12.5;
                    user.Break = 60;
                    user.NotificationOutside = true;
                    user.VideoPlaying = true;


                    await CrossCloudFirestore.Current
                                                 .Instance
                                                 .Collection("Users")//he agregado document y setAsync por add async
                                                 .Document(Email)
                                                 .SetAsync(user);
                    Name = user.Name;
                    await SecureStorage.SetAsync("credentialsStored", "true");
                    await SecureStorage.SetAsync("username", Email);
                    await SecureStorage.SetAsync("password", Password);
                    AddEquipmentsToUser();
                    CheckStoredCredentials(); 
                    Name = "";
                    Email = "";
                    Password = "";
                    }


            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("InvalidEmailAddress"))
                {
                    EmailErrorText="El Email tiene un formato incorrecto";
                    await App.Current.MainPage.DisplayAlert("Info", EmailErrorText, "ACEPTAR");

                    HasEmailError = true;
                }
                else if (ex.Message.Contains("WeakPassword"))
                {
                    PassErrorText = "La contraseña debe tener al menos 6 caracteres";
                    await App.Current.MainPage.DisplayAlert("Info", PassErrorText, "ACEPTAR");

                    HasPassError = true;
                }
                else if (ex.Message.Contains("EmailExists"))
                {
                    EmailErrorText="El Email introducido ya existe";
                    await App.Current.MainPage.DisplayAlert("Info", EmailErrorText, "ACEPTAR");
                    HasEmailError = true;
                }
                else
                {

                    PassErrorText = ex.Message;
                    await App.Current.MainPage.DisplayAlert("Info", "Ingresa un email", "ACEPTAR");


                }
            }
      
        }


        [RelayCommand]
        public async Task NavegarLoginPage()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }



    }
}
