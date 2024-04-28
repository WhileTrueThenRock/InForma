using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileAppTest.Models
{
    public class ExerciseModel // para no hacer un observableObject se crea otro modelo con lo que queremos mostrar y un generic mapper
    {
        public ExerciseModel() 
        {
            FechaEntrenamiento = DateTime.Now.Date.ToString("dd/MM/yyyy");
            Id = new Random().Next().ToString();
            //string[] dateArray = FechaEntrenamiento.Split(' ');
            //FechaEntrenamiento = dateArray[0];

        }
        public string Name { get; set; }
        public string Force {  get; set; }
        public string Level { get; set; }
        public string Id { get; set; }
        public string [] Equipment { get; set; }
        public string PrimaryMuscles { get; set; }
        public string [] SecondaryMuscles { get; set; }
        public string [] Instructions { get; set; }
        public string Category { get; set; }
        public string Duration { get; set; }
        public string []Urls { get; set; }
        public string FechaEntrenamiento { get; set; }
        public double [] Reps { get; set; }
        public double[] Weight { get; set; }
        public string DateGroup { get; set; } 


    }


}
