using mobileAppTest.ViewModels;
using Syncfusion.Maui.NavigationDrawer;

namespace mobileAppTest.Views;

public partial class LoginPage : ContentPage
{

    public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel();
	}

}