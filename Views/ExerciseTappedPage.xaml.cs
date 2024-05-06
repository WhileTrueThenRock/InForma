using mobileAppTest.ViewModels;

namespace mobileAppTest.Views;

public partial class ExerciseTappedPage : ContentPage
{

    public ExerciseTappedPage()
    {
        InitializeComponent();
        BindingContext = new ExerciseTappedViewModel();
    }

    private void thisPage_Loaded(object sender, EventArgs e)
    {
        ExerciseTappedViewModel? viewModel = BindingContext as ExerciseTappedViewModel;
        viewModel?.StartStopwatchExercise();
        viewModel?.StartVideo();
    }

}