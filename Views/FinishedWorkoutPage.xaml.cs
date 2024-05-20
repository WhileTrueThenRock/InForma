namespace mobileAppTest.Views;

public partial class FinishedWorkoutPage : ContentPage
{
	public FinishedWorkoutPage()
	{
		InitializeComponent();
	}
    private void ClickToShowPopup_Clicked(object sender, EventArgs e)
    {
        SavePopup.Show(40, 350);
    }

    private void ClickToClosePopup_Clicked(object sender, EventArgs e)
    {
        SavePopup.Dismiss();
    }
}