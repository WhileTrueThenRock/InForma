using mobileAppTest.ViewModels;
using Mopups.Pages;

namespace mobileAppTest.Views.Popups;

public partial class ExerciseBreakMopup : PopupPage
{
    public ExerciseBreakMopup(ExerciseTappedViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

    }
}