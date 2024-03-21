using mobileAppTest.ViewModels;

namespace mobileAppTest.Views;

public partial class ForgotPasswordPage : ContentPage
{
    LoginViewModel _login = new LoginViewModel();
    public ForgotPasswordPage()
    {
        InitializeComponent();

    }

    private async void bt_buscar_Clicked(object sender, EventArgs e)
    {
        string email = txt_email.Text;
        if (string.IsNullOrEmpty(email))
        {
            await DisplayAlert("Warning", "Debes introducir un email", "ok");
            return;
        }
        bool isSend = await _login.ResetPassword(email);
        if (isSend)
        {
            await DisplayAlert("Reset Password", "Email enviado", "ok");
        }
        else
        {
            await DisplayAlert("Reset Password", "Error al enviar el link", "ok");
        }
    }

}