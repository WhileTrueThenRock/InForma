using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using mobileAppTest.Views;
using Mopups.Hosting;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.Maui.Audio;
using Syncfusion.Maui.Core.Hosting;
namespace mobileAppTest
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiCommunityToolkit().ConfigureMopups()
                .ConfigureSyncfusionCore()
                .UseLocalNotification(config =>
                {
                    config.AddAndroid(android =>
                    {
                        android.AddChannel(new NotificationChannelRequest
                        {
                            Sound = "rick"
                        });
                    });
                })
                .UseMauiCommunityToolkitMediaElement()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddTransient<LoginPage>();
#endif
            return builder.Build();
        }
    }
}