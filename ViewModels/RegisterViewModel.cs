using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using mobileAppTest.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace mobileAppTest.ViewModels
{
    internal partial class RegisterViewModel : ObservableObject
    {
        public string webApiKey = "AIzaSyAJ2z8_aTTkgCz-dYXhLt-bt4nEcXqjPxY";

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

        public RegisterViewModel()
        {

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

        private bool ValidatePattern(string email)
        {

            if (Email == null || Email.Any(Char.IsWhiteSpace))
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
                PassErrorText = "La contraseña debe tener entre 6 y 10 caracteres!";
                HasPassError = true;
                return false;
            }

        }




        [RelayCommand]
        public async Task NavegarLoginPage()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        [RelayCommand]
        private async void RegisterUserTappedAsync(object obj)
        {
            try
            {
                if (!ValidateName())
                {
                    return;
                }

                await ValidateEmail();
                if (!EmailOK)
                {
                    return;
                }

                if (!ValidatePass())
                {
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
                        await CrossCloudFirestore.Current
                                                 .Instance
                                                 .Collection("Users")//he agregado document y setAsync por add async
                                                 .Document(Email)
                                                 .SetAsync(user);
                        Name = user.Name;

                        NavegarLoginPage(); //No navegamos a LoginPage hasta que no acceptemos el mensaje POPUP
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
                    HasEmailError = true;
                }
                else if (ex.Message.Contains("WeakPassword"))
                {
                    PassErrorText = "La contraseña debe tener al menos 6 caracteres";
                    HasPassError = true;
                }
                else if (ex.Message.Contains("EmailExists"))
                {
                    EmailErrorText="El Email introducido ya existe";
                    HasEmailError = true;
                }
                else
                {
                    // MostrarPopup("Error", "CREDENCIALES INCORRECTAS");

                    PassErrorText = ex.Message;

                }
            }
        }
        private async Task<bool> MostrarPopup(string titulo, string mensaje) //Corregir para pulsar solo en aceptar
        {
            // Mostrar el popup y esperar la respuesta del usuario
            bool result = await App.Current.MainPage.DisplayAlert(titulo, mensaje,"Aceptar","");
            return result;
        }



    }
}
