using CommunityToolkit.Maui.Views;
using mobileAppTest.ViewModels;

namespace mobileAppTest.Views.Popups;

public partial class EquipmentPopup : Popup
{
	public EquipmentPopup()
	{
		InitializeComponent();
	}

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is MainViewModel viewModel)
        {
            viewModel.EquipmentSearchText = e.NewTextValue;
            //viewModel.FilterAvailableEquipment();
        }
    }
}