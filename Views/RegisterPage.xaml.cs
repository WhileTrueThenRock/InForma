using mobileAppTest.Models;
using mobileAppTest.ViewModels;

namespace mobileAppTest.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }
    private async void ThisPage_Loaded(object sender, EventArgs e)
    {
        RegisterViewModel? viewModel = BindingContext as RegisterViewModel;
        // Llama al nuevo método que espera a que todas las operaciones asincrónicas hayan terminado
        await viewModel.CheckStoredCredentials();

    }


}