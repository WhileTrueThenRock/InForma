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
        public UserModel() 
        {
            Avatar = "defaultavatar.png";
        }

        [ObservableProperty]
        private string name;
        public string Email { get; set; }
        public string Avatar { get; set; }
        public double Reps { get; set; }
        public double Weight { get; set; }
        public int Break { get; set; }
        public bool NotificationOutside { get; set; }
        public bool VideoPlaying { get; set; }
    }
}
