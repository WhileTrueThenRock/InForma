using mobileAppTest.ViewModels;
using Mopups.Pages;

namespace mobileAppTest.Views.Popups;

public partial class InfoMuscleMopup : PopupPage
{
	public InfoMuscleMopup(InfoMuscleMopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}