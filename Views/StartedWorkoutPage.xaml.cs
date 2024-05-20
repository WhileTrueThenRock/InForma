using mobileAppTest.ViewModels;

namespace mobileAppTest.Views;

public partial class StartedWorkoutPage : ContentPage
{
	public StartedWorkoutPage()
	{
		InitializeComponent();
		
	}

    private void ThisPage_Loaded(object sender, EventArgs e)
    {
		StartedWorkoutViewModel? viewModel = BindingContext as StartedWorkoutViewModel;
        viewModel?.GetExerciseCount();
        //viewModel?.GetUserInfo();
        //viewModel?.StartStopwatch();
    }
}