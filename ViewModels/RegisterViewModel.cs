using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using mobileAppTest.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public RegisterViewModel()
        {

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
                if (Email == null) // controlar espacio despues de introducir email
                {
                    MostrarPopup("Error", "Te falta introducir el email");
                    return;
                }
                if (Password == null)
                {
                    MostrarPopup("Error", "Te falta introducir la contraseña");
                    return;
                }
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password);
                string token = auth.FirebaseToken;
                if (token!=null)
                {
                    bool usuarioRegistrado = await MostrarPopup("Alert", "Usuario registrado correctamente");
                    if (usuarioRegistrado)
                    {
                        UserModel user = new UserModel();
                        user.Name = Name;
                        user.Email = Email;
                        await CrossCloudFirestore.Current
                                                 .Instance
                                                 .Collection("Users")//he agregado document y setAsync por add async
                                                 .Document(Email)
                                                 .SetAsync(user);
                        Name = user.Name;

                        NavegarLoginPage(); //No navegamos a LoginPage hasta que no acceptemos el mensaje POPUP
                    }
                }


            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("InvalidEmailAddress"))
                {
                    MostrarPopup("Error", "El Email tiene un formato incorrecto");
                }
                else if (ex.Message.Contains("WeakPassword"))
                {
                    MostrarPopup("Error", "La contraseña debe tener al menos 6 caracteres");
                }
                else if (ex.Message.Contains("EmailExists"))
                {
                    MostrarPopup("Error", "El Email introducido ya existe");
                }
                else
                {
                    MostrarPopup("Error", "CREDENCIALES INCORRECTAS");

                }
            }
            Name = "";
            Email = "";
            Password = "";
        }
        private async Task<bool> MostrarPopup(string titulo, string mensaje) //Corregir para pulsar solo en aceptar
        {
            // Mostrar el popup y esperar la respuesta del usuario
            bool result = await App.Current.MainPage.DisplayAlert(titulo, mensaje,"Aceptar","Cancelar");
            return result;
        }



    }
}
