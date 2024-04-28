using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using mobileAppTest.Models;
using mobileAppTest.Views;
using Newtonsoft.Json;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileAppTest.ViewModels
{
    internal partial class LoginViewModel : ObservableObject
    {
        public string webApiKey = "AIzaSyAJ2z8_aTTkgCz-dYXhLt-bt4nEcXqjPxY";

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private ForgotPasswordPage _forgotPassPopup;

        public static string Email_name { get; set; }

        private bool _rememberMe;

        public bool RememberMe
        {
            get => _rememberMe;
            set => SetProperty(ref _rememberMe, value);
        }

        [ObservableProperty]
        private bool _emailOK;

        [ObservableProperty]
        private bool _passOK;

        [ObservableProperty]
        private bool _hasEmailError;

        [ObservableProperty]
        private bool _hasPassError;

        [ObservableProperty]
        private string _emailErrorText;

        [ObservableProperty]
        private string _passErrorText;

        [ObservableProperty]
        private string _emailHintText;

        public LoginViewModel()
        {
            CheckStoredCredentials();
            
            PassOK = false;
            HasEmailError = false;
            HasPassError = false;
        }

  
        private async Task ValidateEmail()
        {
            HasEmailError = true;
            EmailOK = false;

            if (Email==null || Email.Any(Char.IsWhiteSpace))
            { 
                HasEmailError = true;
                EmailErrorText = "Introduce un email!";
                EmailOK = false;
                return;
            }


            var userDocuments = await CrossCloudFirestore.Current
               .Instance
               .Collection("Users")
               .GetAsync();

            var users = userDocuments.Documents
                                    .Select(doc => doc.ToObject<UserModel>())
                                    .Where(user => user != null)
                                    .ToList();
            foreach (var user in users)
            {
                if (Email.Equals(user.Email))
                {
                    EmailOK = true;
                    HasEmailError = false;
                    return;
                }
                else
                {
                    EmailOK = false;
                    HasEmailError = true;
                    EmailErrorText = "El email introducido no existe!";
                }
            }
        }

        private bool ValidatePass()
        {
            if (Password==null || Password.Any(Char.IsWhiteSpace))
            {
                HasPassError = true;
                PassErrorText = "Se te olvidó la contraseña wey!";
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
                PassErrorText = "Contraseña incorrecta!";
                HasPassError = true;
                return false;
            }

        }


        private async void CheckStoredCredentials()
        {
            bool credentialsStored = await SecureStorage.GetAsync("credentialsStored") == "true";
            if (credentialsStored)
            {
                await Shell.Current.GoToAsync("//MainPage");

                string username = await SecureStorage.GetAsync("username");
                string password = await SecureStorage.GetAsync("password");

                // Lógica para iniciar sesión automáticamente con las credenciales guardadas
            }
            else
            {
                // El usuario no ha iniciado sesión previamente
            }
        }


        [RelayCommand]
        public async Task NavegarMainPage()
        {
            if (RememberMe && Email != null && Password != null)
            {
                await SecureStorage.SetAsync("credentialsStored", "true");
                await SecureStorage.SetAsync("username", Email);
                await SecureStorage.SetAsync("password", Password);
            }
            else
            {
                // Si el usuario no quiere recordar las credenciales, borrarlas
                await SecureStorage.SetAsync("credentialsStored", "false");
                await SecureStorage.SetAsync("username", "");
                await SecureStorage.SetAsync("password", "");
            }
            await Shell.Current.GoToAsync("//MainPage");
        }

        [RelayCommand]
        public async Task OnRegisterTapped()
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }

        [RelayCommand]
        public async Task OnForgotTapped()
        {
            await Shell.Current.GoToAsync("//ForgotPasswordPage");
        }

   


        // Método para agregar equipamientos a la subcolección "User"
        [RelayCommand]
        public async Task AddEquipmentsToUser()
        {
          
            // Lista de equipamientos
            var equipments = new List<EquipmentModel>
    { //Alomejor conviene mejor agregar las fotos del equipamiento al proyecto ya que son pocos
        new EquipmentModel { name = "Banco", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fbancohorizontal.jpg?alt=media&token=e3e3bb52-efd4-4126-b00d-9dd1afa3afd9" },
        new EquipmentModel { name = "Banco Declinado", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fbancodeclinado.jpg?alt=media&token=e36e25c3-00f3-4a6a-8924-01796102671d"},
        new EquipmentModel { name = "Banco Inclinado", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fbancoinclinado.jpg?alt=media&token=a25b650a-d915-41a1-942a-4d7fcd5713ff" },
        new EquipmentModel { name = "Banco Romano", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fbancoromano.jpg?alt=media&token=514f4ceb-cd61-46b4-9333-809f3ffc5598" },
        new EquipmentModel { name = "Banco Scott", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fbancoscott.jpg?alt=media&token=d708f12f-8348-4d1c-9d8c-147b4852b551" },
        new EquipmentModel { name = "Barras", disponible = false, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fbarras.jpg?alt=media&token=556c2dea-57fd-4459-8252-9d7d37184ae5" },
        new EquipmentModel { name = "Barra T", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Flandmine.jpg?alt=media&token=dbf12a17-3f53-4f10-aa9c-295fe89e385b" },
        new EquipmentModel { name = "Barra Z", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fbarraz.jpg?alt=media&token=d2752562-7c0e-4a4f-b615-6790d74d804c" },
        new EquipmentModel { name = "Rueda Abdominal", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fruedaabd.jpg?alt=media&token=7f6314b0-561e-460b-9328-7b15a4070ca0" },
        new EquipmentModel { name = "Sin Equipamiento", disponible = false, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fsinequipamiento.png?alt=media&token=87692747-7d42-4e1a-a809-586082e3a652" },
        new EquipmentModel { name = "Soga", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fsoga.jpg?alt=media&token=6834b680-96d3-4ec8-a994-d184515524bf" },
        new EquipmentModel { name = "Soporte Dominadas", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fdominadas.jpg?alt=media&token=c238858a-abc8-4f80-87bc-e34f73cf11fc" },
        new EquipmentModel { name = "Mancuernas", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fdumbbells.jpg?alt=media&token=dc835f06-de42-4329-a378-31666a20adcc" },
        new EquipmentModel { name = "Máquinas Convergentes", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinaconvergente.jpg?alt=media&token=8b7c775f-5a1e-4d85-86f9-689f8663a67b" },
        new EquipmentModel { name = "Máquina de Poleas", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinapoleas.jpg?alt=media&token=63b9e8ca-7481-42ef-87a8-57cf0bcb82d5" },
        new EquipmentModel { name = "Máquina Scott", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinascottplacas.jpg?alt=media&token=9cf7c98c-e7d5-4cf9-85d3-cab39b97e73d"},
        new EquipmentModel { name = "Máquina Patada Glúteos", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinapatada.jpg?alt=media&token=f3e186f7-5393-4315-917a-53aef051d6d6" },
        new EquipmentModel { name = "Máquina Abductores y Aductores", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinaaductores.jpg?alt=media&token=f36220b5-5185-4268-a2dd-a7a14edd202f" },
        new EquipmentModel { name = "Máquina Trapecio", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinatrapecio.jpg?alt=media&token=76cb0401-9609-4435-b590-d31d0ce57ed0" },
        new EquipmentModel { name = "Máquina Hip Thrust", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinahip.jpg?alt=media&token=fc718145-8b5b-4f1b-9afb-0e5b74488087" },
        new EquipmentModel { name = "Máquina Hombros Press", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinahombrospress.jpg?alt=media&token=8905357b-3649-4ae9-b43e-a39b2c81154a" },
        new EquipmentModel { name = "Máquina Hombros Laterales", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinahombroslat.jpg?alt=media&token=a3005045-5d5e-495b-88ae-a7cd26aef84e" },
        new EquipmentModel { name = "Máquina para gemelos de pie", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinagemelospie.jpg?alt=media&token=f9fdd2fc-6a18-4b47-a571-2127e64742c3" },
        new EquipmentModel { name = "Máquina para gemelos Tipo Burro", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinaburro.jpg?alt=media&token=06c4bf99-8c0a-47ca-9dc0-af45473e0d56" },
        new EquipmentModel { name = "Máquina Multipower", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinamulti.jpg?alt=media&token=603c6c2a-cf18-48ae-8856-112013887465" },
        new EquipmentModel { name = "Máquina Aperturas", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinaaperturas.jpg?alt=media&token=75b3d99a-b386-4bec-a7f4-4acae6637e1c" },
        new EquipmentModel { name = "Máquina Remo Alto", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinaremoalto.jpg?alt=media&token=2c16fdf4-8e62-4d1a-9e7d-0992363ddf65" },
        new EquipmentModel { name = "Máquina Remo", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinaremo.jpg?alt=media&token=e81c603c-2aba-4132-83aa-ec9d4142b08a" },
        new EquipmentModel { name = "Máquina Remo Bajo", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinaremobajo.jfif?alt=media&token=1408e4b0-d15f-4738-a4c1-f2a048b64d7d" },
        new EquipmentModel { name = "Máquina Fondos", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinafondos.jpg?alt=media&token=7b6e94ba-21d3-4d7f-b3f1-ede0d2dc0c0d" },
        new EquipmentModel { name = "Máquina Asistida", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinaasistida.jpg?alt=media&token=42fe9ee4-cb3e-40ed-9c96-23eb35b5ee9f" },
        new EquipmentModel { name = "Máquina Sentadillas", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinasentadillas.png?alt=media&token=7e49ca2f-bd74-4b74-8cff-e7cf70e509e4" },
        new EquipmentModel { name = "Máquina Femoral Tumbado", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinafemoraltumbado.jpg?alt=media&token=1e737601-4210-4832-b6c4-7b81cac227e7" },
        new EquipmentModel { name = "Máquina Femoral Sentado", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinafemoralsentado.jpg?alt=media&token=d84b9987-66cf-4be7-946d-4d7b8572b434" },
        new EquipmentModel { name = "Máquina Femoral de Pie", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinafemoraldepie.jpg?alt=media&token=c171b3e0-e1c5-44e3-b219-4548a0d3b570" },
        new EquipmentModel { name = "Máquina Extensiones Cuádriceps", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinaquads.jpg?alt=media&token=3b9a0c94-c0e4-4c0e-8d59-544f71a49994" },
        new EquipmentModel { name = "Máquina Press Pierna", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinaprensa.jpg?alt=media&token=325f66af-5b54-4db5-b37a-1eb7a152d2da" },
        new EquipmentModel { name = "Máquina Press Pierna Horizontal", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquinapresshoriz.jpg?alt=media&token=6ca1b755-a3df-4d70-9d5f-bf97f5340a11" },

    };

            // Referencia al documento del usuario

            var userDocument = CrossCloudFirestore.Current
                .Instance
                .Collection("Users")
                .Document(Email);

            // Iterar sobre la lista de equipamientos y agregar documentos a la subcolección "equipments"
            foreach (var equipment in equipments)
            {

                // Agregar el documento del equipamiento a la subcolección "equipments"
                await userDocument
                    .Collection("Equipment")
                    .Document(equipment.name)
                    .SetAsync(equipment);
            }
        }




        [RelayCommand]
        private async void LoginTappedAsync()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            try
            {
                await ValidateEmail();
                if (!EmailOK)
                {
                    return;
                }

                if (!ValidatePass())
                {
                    return;
                }

                UserModel user = new UserModel();
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(Email, Password);
                var content = await auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);
                Preferences.Set("FreshFirebaseToken", serializedContent);
                //AddDefaultEquipmentToUser(); //Falta parametro user
                Email_name = Email;

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("InvalidEmailAddress"))
                {
                    EmailErrorText="Introduzca un Email correcto";
                    HasEmailError = true;
                    return;
                }
                else if (ex.Message.Contains("WeakPassword"))
                {
                    PassErrorText="La contraseña debe tener al menos 6 caracteres";
                    HasPassError = true;
                    return;
                }
                else
                {
                    PassErrorText = "CREDENCIALES INCORRECTAS";
                    HasPassError = true;
                    return;

                }
            }
            AddEquipmentsToUser();
            NavegarMainPage();

        }
        private async Task<bool> MostrarPopup(string titulo, string mensaje)
        {
            // Mostrar el popup y esperar la respuesta del usuario
            bool result = await App.Current.MainPage.DisplayAlert(titulo, mensaje, "Aceptar", "Cancelar");
            return result;
        }

        [RelayCommand]
        private  async Task ResetPassword()
        {
            await ValidateEmail();
            if (!EmailOK)
            {
                return;
            }

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            await authProvider.SendPasswordResetEmailAsync(Email);
            EmailHintText = "Correo enviado !";
            Email = "";

        }

        [RelayCommand]
        public async Task OpenForgotPassPopup()
        {

            ForgotPassPopup = new ForgotPasswordPage();
            await App.Current.MainPage.ShowPopupAsync(ForgotPassPopup);
        }



    }
}
