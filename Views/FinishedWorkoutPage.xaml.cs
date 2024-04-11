using Syncfusion.Maui.Cards;
using Syncfusion.Maui.Data;

namespace mobileAppTest.Views;

public partial class FinishedWorkoutPage : ContentPage
{
	public FinishedWorkoutPage()
	{
		InitializeComponent();
	}

    private void SfCardLayout_Tapped_1(object sender, TappedEventArgs e)

    {



        SfCardView? cardView = e.Parameter as SfCardView;

        int cardIndex;

        if (cardView == null)

        {

            return;

        }

        cardIndex = cardLayout.Children.IndexOf(cardView);



        if (cardLayout.VisibleIndex == cardIndex)

        {

            cardView.InputTransparent = !cardView.InputTransparent;

        }



    }
}