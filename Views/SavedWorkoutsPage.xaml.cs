using mobileAppTest.Models;
using mobileAppTest.ViewModels;

namespace mobileAppTest.Views;

public partial class SavedWorkoutsPage : ContentPage
{

    public SavedWorkoutsPage()
	{
		InitializeComponent();
    }

    private async void ThisPage_Loaded(object sender, EventArgs e)
    {
        SavedWorkoutsViewModel? viewModel = BindingContext as SavedWorkoutsViewModel;
        viewModel?.StartShimmerAndWait();
    }

    
}