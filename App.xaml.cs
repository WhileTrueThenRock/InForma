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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF5cWWJCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWH1ceXVURGNYU0JyWkY=");
            MainPage = new AppShell();
        }
    }
}
