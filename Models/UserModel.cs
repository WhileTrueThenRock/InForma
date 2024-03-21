using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileAppTest.Models
{
     public partial class UserModel : ObservableObject
    {
        [ObservableProperty]
        private string name;
        public string Email { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
    }
}
