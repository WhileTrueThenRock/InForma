using mobileAppTest.Models;

namespace mobileAppTest.Mappers
{
    public static class ModelMapper
    {
        public static ExerciseModelView ExerciseModelToExerciseModelView(ExerciseModel exerciseModel)
        {
            ExerciseModelView exerciseModelView = new ExerciseModelView();
            exerciseModelView.Id = exerciseModel.Id;
            exerciseModelView.Name = exerciseModel.Name;
            exerciseModelView.Equipment = exerciseModel.Equipment;
            exerciseModelView.Duration = exerciseModel.Duration;
            exerciseModelView.Urls = exerciseModel.Urls;
            exerciseModelView.FechaEntrenamiento = exerciseModel.FechaEntrenamiento;
            exerciseModelView.PrimaryMuscles = exerciseModel.PrimaryMuscles;
            exerciseModelView.Reps = exerciseModel.Reps;
            exerciseModelView.Weight = exerciseModel.Weight;

            exerciseModelView.Exercise = exerciseModel;
            return exerciseModelView;
        }

        public static ExerciseModel ExerciseModelViewToExerciseModel(ExerciseModelView exerciseModelView)
        {
            return new ExerciseModel
            {
                Id = exerciseModelView.Id,
                Name = exerciseModelView.Name,
                Equipment = exerciseModelView.Equipment,
                Duration = exerciseModelView.Duration,
                Urls = exerciseModelView.Urls,
                FechaEntrenamiento = exerciseModelView.FechaEntrenamiento,
                PrimaryMuscles = exerciseModelView.PrimaryMuscles,
                SecondaryMuscles = exerciseModelView.SecondaryMuscles,
                Instructions = exerciseModelView.Instructions,
                Reps = exerciseModelView.Reps,
                Weight = exerciseModelView.Weight
            };
        }




        public static EquipmentModelView EquipmentModelToEquipmentModelView(EquipmentModel equipment)
        {
            EquipmentModelView equipmentModelView = new EquipmentModelView();
            equipmentModelView.Name = equipment.name;
            equipmentModelView.Url = equipment.url;
            return equipmentModelView;
        }
    }
}
