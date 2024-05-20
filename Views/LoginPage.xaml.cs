using mobileAppTest.ViewModels;
using Plugin.LocalNotification;
using Plugin.Maui.Audio;
using Syncfusion.Maui.NavigationDrawer;

namespace mobileAppTest.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel();

        LocalNotificationCenter.Current.AreNotificationsEnabled();
        LocalNotificationCenter.Current.RequestNotificationPermission();
    }

}