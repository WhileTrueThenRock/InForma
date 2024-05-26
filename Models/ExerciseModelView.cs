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
        private string[] _secondaryMuscles;

        [ObservableProperty]
        private string[] _instructions;

        [ObservableProperty]
        private double[] _reps;

        [ObservableProperty]
        private double[] _weight;

        [ObservableProperty]
        private bool _IsChecked;

        [ObservableProperty]
        private ExerciseModel _exercise;

        //Este método se usa para determinar si dos instancias de la clase ExerciseModelView son iguales.
        //Aquí comparamos las propiedades que hacen único a un ejercicio.
        public override bool Equals(object obj)
        {
            if (obj is ExerciseModelView other)
            {
                return this.Name == other.Name; // Comparar por nombre u otra propiedad única
            }
            return false;
        }
        //GetHashCode usa la propiedad Name para generar un valor hash.
        //Esto asegura que si dos objetos son iguales según el método Equals, tendrán el mismo código hash.
        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0; // Usar la misma propiedad única para el hashcode
        }

        //Al sobrescribir Equals y GetHashCode, le dices al sistema cómo comparar y organizar instancias de ExerciseModelView,
        //asegurando que los métodos de colección funcionen correctamente y evitando duplicados en tu lista de ejercicios seleccionados.

    }
}
