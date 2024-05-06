using CommunityToolkit.Maui.Views;
using mobileAppTest.ViewModels;

namespace mobileAppTest.Views.Popups;

public partial class MuscleSelectionPopup : Popup
{
	public MuscleSelectionPopup()
	{
		InitializeComponent();
        effectsView.TouchDown += EffectsView_TouchDown;
    }

    private void EffectsView_TouchDown(object sender, EventArgs e)
    {
        MainViewModel? viewModel = BindingContext as MainViewModel;
        // Obtener la rotaci�n actual y agregar un valor para rotar m�s
        int currentRotation = effectsView.Angle;
        int newRotation = currentRotation + 180; // Por ejemplo, rotar 45 grados m�s

        // Aplicar la nueva rotaci�n
        effectsView.Angle = newRotation;
        viewModel?.RotateBody();
    }
}