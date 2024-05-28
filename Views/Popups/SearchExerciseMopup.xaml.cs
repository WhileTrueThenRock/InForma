using mobileAppTest.ViewModels;
using Mopups.Pages;

namespace mobileAppTest.Views.Popups;

public partial class SearchExerciseMopup : PopupPage
{
	public SearchExerciseMopup(MainViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
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