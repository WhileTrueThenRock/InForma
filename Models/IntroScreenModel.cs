using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileAppTest.Models
{
    public class IntroScreenModel
    {
        public IntroScreenModel()
        {

        }

        public string EquipmentTitle { get; set; }
        public string RegisterTitle { get; set; }
        public ObservableCollection<EquipmentModel> EquipmentList { get; set; }
        public int CollectionVisible { get; set; }
        public bool RegisterVisible { get; set; }
        public bool titleVisible { get; set; }
        public bool EquipmentVisible { get; set; }


    }
}
