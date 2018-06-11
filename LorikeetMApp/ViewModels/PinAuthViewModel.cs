using FormsPinView.PCL;
using System;
using System.Diagnostics;
using System.Linq;

namespace LorikeetMApp.ViewModels
{
    public class PinAuthViewModel : ViewModelBase
    {
        private readonly char[] _correctPin;
        private readonly PinViewModel _pinViewModel;

        public PinViewModel PinViewModel => _pinViewModel;

        public PinAuthViewModel()
        {
            string pinAsString = LorikeetMApp.Helpers.Settings.Pin;
            _correctPin = new[] { pinAsString[0], pinAsString[1], pinAsString[2], pinAsString[3] };
            _pinViewModel = new PinViewModel
            {
                TargetPinLength = 4,
                ValidatorFunc = (arg) => Enumerable.SequenceEqual(arg, _correctPin)
            };
        }
    }
}
