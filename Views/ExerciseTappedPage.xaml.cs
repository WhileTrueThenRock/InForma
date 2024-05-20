using mobileAppTest.ViewModels;

namespace mobileAppTest.Views;

public partial class ExerciseTappedPage : ContentPage
{

    public ExerciseTappedPage()
    {
        InitializeComponent();
    }

    private void thisPage_Loaded(object sender, EventArgs e)
    {
        ExerciseTappedViewModel? viewModel = BindingContext as ExerciseTappedViewModel;
        viewModel?.GetUserInfo();
        viewModel?.StartStopwatchExercise();
        viewModel?.StartVideo();
    }

}