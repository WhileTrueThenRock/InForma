using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileAppTest.Models
{
   public class DateGroup
    {
        public string FechaEntrenamiento { get; set; }
        public List<ExerciseModelView> Exercises { get; set; }
    }
}
