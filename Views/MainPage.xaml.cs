using mobileAppTest.Models;
using mobileAppTest.ViewModels;
using Plugin.CloudFirestore;

namespace mobileAppTest.Views;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    private void thisPage_Loaded(object sender, EventArgs e)
    {
        MainViewModel? viewModel = BindingContext as MainViewModel;
        viewModel?.FilterBySelectedMuscle();
        viewModel?.ChangeFinishedExercisesColor();
    }
}
