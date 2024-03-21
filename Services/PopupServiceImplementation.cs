using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileAppTest.Services
{
    public interface IPopupService
    {
        void ShowPopup(Popup popup);
        void ClosePopup(Popup popup);
    }

    public class PopupServiceImplementation : IPopupService
    {
        public void ClosePopup(Popup popup)
        {
            popup.CloseAsync().Wait();
        }

        public void ShowPopup(Popup popup)
        {
            Page page = Application.Current?.MainPage ?? throw new NullReferenceException();
            page.ShowPopup(popup);

        }

    }
}
