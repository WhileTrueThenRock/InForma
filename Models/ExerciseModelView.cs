using CommunityToolkit.Mvvm.ComponentModel;

namespace mobileAppTest.Models
{
    public partial class ExerciseModelView : ObservableObject
    {
        [ObservableProperty]
        private string _id;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string[] _equipment;

        [ObservableProperty]
        private string _duration;

        [ObservableProperty]
        private string[] _urls;

        [ObservableProperty]
        private string _fechaEntrenamiento;

        [ObservableProperty]
        private string _primaryMuscles;

        [ObservableProperty]
        private double[] _reps;

        [ObservableProperty]
        private double[] _weight;

        [ObservableProperty]
        private bool _IsChecked;

        [ObservableProperty]
        private ExerciseModel _exercise;

    }
}
