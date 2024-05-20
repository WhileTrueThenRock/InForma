using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Microsoft.Maui.ApplicationModel;
using mobileAppTest.Models;
using mobileAppTest.Views;
using mobileAppTest.Views.Popups;
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

        [ObservableProperty]
        private ConfigurationPopup _configurationPopup;

        public LoginViewModel()
        {
            CheckStoredCredentials();
            RememberMe = false;
            PassOK = false;
            HasEmailError = false;
            HasPassError = false;
        }

        [RelayCommand]
        public async Task OpenProfilePopup()
        {
            if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
            }


            //ConfigurationPopup = new ConfigurationPopup();
            //await App.Current.MainPage.ShowPopupAsync(ConfigurationPopup);
        }

        [RelayCommand]
        public async Task CloseProfilePopup()
        {
            ConfigurationPopup.Close();

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
            await Shell.Current.GoToAsync("//MainPage", new Dictionary<string, object>()
            {
                ["Email"] = Email
            }); 
         
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
            NavegarMainPage();

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
