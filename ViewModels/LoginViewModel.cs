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

        public static string Email_name { get; set; }

        public LoginViewModel()
        {
        }

        [RelayCommand]
        public async Task NavegarMainPage()
        {
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
        new EquipmentModel { name = "Mancuernas", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fdumbbells.jpg?alt=media&token=a6fb53c2-e371-46b3-bd1d-33c4aa81f3bd" },
        new EquipmentModel { name = "Barras", disponible = false, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fbarra.jpg?alt=media&token=d0426a41-6f44-4993-9996-7be06d9b25d6" },
        new EquipmentModel { name = "Barra Z", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fbarraz.jpg?alt=media&token=d2eb743f-8085-416c-8212-28b0d2179337" },
        new EquipmentModel { name = "Barra Hexagonal", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fbarraz.jpg?alt=media&token=d2eb743f-8085-416c-8212-28b0d2179337" },
        new EquipmentModel { name = "Máquinas", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina de Poleas", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina Scott", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina Patada Glúteos", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina Abductores y Aductores", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina Trapecio", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina Hip Thrust", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina Hombros Press", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina Hombros Laterales", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina para gemelos de pie", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina para gemelos Tipo Burro", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina Multipower", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina Aperturas", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina Remo Alto", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina Remo", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina Remo Bajo", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Máquina Fondos", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fdips.jpg?alt=media&token=4d4ae935-182a-489a-9745-bb294c4857f5" },
        new EquipmentModel { name = "Máquina Asistida", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fdips.jpg?alt=media&token=4d4ae935-182a-489a-9745-bb294c4857f5" },
        new EquipmentModel { name = "Máquina Sentadillas", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fdips.jpg?alt=media&token=4d4ae935-182a-489a-9745-bb294c4857f5" },
        new EquipmentModel { name = "Máquina Femoral Tumbado", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fdips.jpg?alt=media&token=4d4ae935-182a-489a-9745-bb294c4857f5" },
        new EquipmentModel { name = "Máquina Femoral Sentado", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fdips.jpg?alt=media&token=4d4ae935-182a-489a-9745-bb294c4857f5" },
        new EquipmentModel { name = "Máquina Femoral de Pie", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fdips.jpg?alt=media&token=4d4ae935-182a-489a-9745-bb294c4857f5" },
        new EquipmentModel { name = "Máquina Extensiones Cuádriceps", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fdips.jpg?alt=media&token=4d4ae935-182a-489a-9745-bb294c4857f5" },
        new EquipmentModel { name = "Máquina Press Pierna", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fdips.jpg?alt=media&token=4d4ae935-182a-489a-9745-bb294c4857f5" },
        new EquipmentModel { name = "Máquina Press Pierna Horizontal", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fdips.jpg?alt=media&token=4d4ae935-182a-489a-9745-bb294c4857f5" },
        new EquipmentModel { name = "Banco Romano", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fdips.jpg?alt=media&token=4d4ae935-182a-489a-9745-bb294c4857f5" },
        new EquipmentModel { name = "Banco", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Banco Inclinado", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Banco Declinado", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Banco Scott", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Barra T", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },
        new EquipmentModel { name = "Soga", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fsoga.jpg?alt=media&token=a179665f-d244-4edf-ad2a-f3267c2503dc" },
        new EquipmentModel { name = "Comba", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fcomba.jpg?alt=media&token=4075764d-d51c-4b5a-913d-bf75458c4da0" },
        new EquipmentModel { name = "Soporte Dominadas", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fdominadas.jpg?alt=media&token=d7390185-ffe2-4cbf-8410-7d076c1a6a40" },
        new EquipmentModel { name = "Pesas Rusas", disponible = false, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fkettlebell.jpg?alt=media&token=9c7a54d6-77bf-415d-848c-c98aae165e42" },
        new EquipmentModel { name = "Anillas", disponible = false, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fanillas.jpg?alt=media&token=e9860f56-1d10-4d8c-8fd6-fd9d97af01f9" },
        new EquipmentModel { name = "Plataforma", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fstep.png?alt=media&token=bd653d0b-fcf1-472b-aa03-60fd9e010cc4" },
        new EquipmentModel { name = "Rueda Abdominal", disponible = true, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fruedaabd.jpg?alt=media&token=ccfa153f-bf18-4663-a2fc-5ac052070511" },
        new EquipmentModel { name = "Sin Equipamiento", disponible = false, url = "https://firebasestorage.googleapis.com/v0/b/logingym-1df51.appspot.com/o/Equipment%2Fmaquina.jpg?alt=media&token=cfd4f02d-4be4-4875-bc47-25728cab1d58" },

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
            LoginTappedAsync();
            NavegarMainPage();
        }




        [RelayCommand]
        private async void LoginTappedAsync()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            try
            {
                if (Email == null)
                {
                    MostrarPopup("Error", "Te falta introducir el email");
                    return;
                }
                if (Password == null)
                {
                    MostrarPopup("Error", "Te falta introducir la contraseña");
                    return;
                }
                UserModel user = new UserModel();
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(Email, Password);
                var content = await auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);
                Preferences.Set("FreshFirebaseToken", serializedContent);
                //AddDefaultEquipmentToUser(); //Falta parametro user
                Email_name = Email;
                //AddEquipmentsToUser(Email);
                //NavegarMainPage();

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("InvalidEmailAddress"))
                {
                    MostrarPopup("Error", "Introduzca un Email correcto");
                }
                else if (ex.Message.Contains("WeakPassword"))
                {
                    MostrarPopup("Error", "La contraseña debe tener al menos 6 caracteres");
                }
                else
                {
                    MostrarPopup("Error", "CREDENCIALES INCORRECTAS");

                }
            }
        }
        private async Task<bool> MostrarPopup(string titulo, string mensaje)
        {
            // Mostrar el popup y esperar la respuesta del usuario
            bool result = await App.Current.MainPage.DisplayAlert(titulo, mensaje, "Aceptar", "Cancelar");
            return result;
        }

        public async Task<bool> ResetPassword(string email)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            await authProvider.SendPasswordResetEmailAsync(email);
            return true;
        }

    }
}
