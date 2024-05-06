using mobileAppTest.Models;
using mobileAppTest.ViewModels;
using Plugin.CloudFirestore;
using Syncfusion.Maui.Accordion;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Core.Carousel;
using System.ComponentModel;

namespace mobileAppTest.Views;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    private async void ThisPage_Loaded(object sender, EventArgs e)
    {
        MainViewModel? viewModel = BindingContext as MainViewModel;
        // Llama al nuevo método que espera a que todas las operaciones asincrónicas hayan terminado
        await viewModel?.StartShimmerAndWait(); 
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
