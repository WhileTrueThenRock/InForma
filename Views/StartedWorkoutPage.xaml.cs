using mobileAppTest.ViewModels;

namespace mobileAppTest.Views;

public partial class StartedWorkoutPage : ContentPage
{
	public StartedWorkoutPage()
	{
		InitializeComponent();
		BindingContext = new StartedWorkoutViewModel();
		
	}

    private void thisPage_Loaded(object sender, EventArgs e)
    {
		StartedWorkoutViewModel? viewModel = BindingContext as StartedWorkoutViewModel;
		viewModel?.StartStopwatch();
    }
}