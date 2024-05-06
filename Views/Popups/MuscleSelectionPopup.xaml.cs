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
        // Obtener la rotación actual y agregar un valor para rotar más
        int currentRotation = effectsView.Angle;
        int newRotation = currentRotation + 180; // Por ejemplo, rotar 45 grados más

        // Aplicar la nueva rotación
        effectsView.Angle = newRotation;
        viewModel?.RotateBody();
    }
}