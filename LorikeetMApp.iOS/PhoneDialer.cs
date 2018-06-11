using Foundation;
using LorikeetMApp.Interfaces;
using LorikeetMApp.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneDialer))]
namespace LorikeetMApp.iOS
{
	public class PhoneDialer : IDialer
    {
		public bool Dial(string number)
        {
            return UIApplication.SharedApplication.OpenUrl(
                new NSUrl("tel:" + number));
        }
    }
}
