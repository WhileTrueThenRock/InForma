using mobileAppTest.ViewModels;
using Plugin.LocalNotification;
using Syncfusion.Maui.NavigationDrawer;
using UIKit;

namespace mobileAppTest.Views;

public partial class LoginPage : ContentPage
{

    public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel();
    }


    private async void Send_Notification(object sender, EventArgs e)
    {
        var request = new NotificationRequest
        {
            NotificationId = 1337,
            Title = "Testing",
            Subtitle = "subtitle :D",
            Description = "You need to go back to training fella",
            BadgeNumber = 10,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(5),

            },
            Android = new Plugin.LocalNotification.AndroidOption.AndroidOptions
            {
                Color = new Plugin.LocalNotification.AndroidOption.AndroidColor("#FF0000")
            },

            iOS = new Plugin.LocalNotification.iOSOption.iOSOptions
            {
                PlayForegroundSound = true
                
            }
        };

    }



}