using mobileAppTest.Services;
using mobileAppTest.Views;

namespace mobileAppTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            App.Current.UserAppTheme = AppTheme.Light;
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzI4NDg1NkAzMjM1MmUzMDJlMzBLSFhCeWMyZ2JlZGJJR2lPczF3SExJNjB2S0RNSmxmRjk1MytOZGllSUtRPQ==");
            MainPage = new AppShell();
        }
    }
}
