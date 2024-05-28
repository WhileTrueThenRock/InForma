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

        public string Title { get; set; }
        public UserModel User { get; set; }
        public ObservableCollection<EquipmentModel> EquipmentList { get; set; }
        public int CollectionVisible { get; set; }
        public bool RegisterVisible { get; set; }


    }
}
