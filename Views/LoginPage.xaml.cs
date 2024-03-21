using mobileAppTest.ViewModels;

namespace mobileAppTest.Views;

public partial class LoginPage : ContentPage
{

    public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel();
	}
}