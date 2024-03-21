using CommunityToolkit.Maui.Views;
using mobileAppTest.ViewModels;

namespace mobileAppTest.Views.Popups;

public partial class CustomExercises : Popup
{
	public CustomExercises()
	{
		InitializeComponent();
	}
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is MainViewModel viewModel)
        {
            viewModel.SearchText = e.NewTextValue;
            viewModel.UpdateFilteredExercises();
        }
    }
}