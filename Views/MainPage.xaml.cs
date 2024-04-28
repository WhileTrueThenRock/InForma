using mobileAppTest.Models;
using mobileAppTest.ViewModels;
using Plugin.CloudFirestore;
using Syncfusion.Maui.Accordion;
using Syncfusion.Maui.Core.Carousel;
using System.ComponentModel;

namespace mobileAppTest.Views;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
    }

    private async void ThisPage_Loaded(object sender, EventArgs e)
    {
        MainViewModel? viewModel = BindingContext as MainViewModel;
        viewModel?.FilterBySelectedMuscle();
        viewModel?.ChangeFinishedExercisesColor();
        viewModel?.StartShimmer();
        await Task.Delay(2000);
        viewModel.IsShimmerPlaying = false;
    }

    private void HamburgerButton_Clicked(object sender, EventArgs e)
    {
        navigationDrawer.ToggleDrawer(); 
    }


    private void ClickToShowPopup_Clicked(object sender, EventArgs e)
    {
        popup.Show(40,350);
    }

}
