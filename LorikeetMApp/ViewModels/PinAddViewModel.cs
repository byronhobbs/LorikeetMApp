using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FormsPinView.PCL;

namespace LorikeetMApp.ViewModels
{
    public class PinAddViewModel : ViewModelBase
    {
        /*
        public PinViewModel _pinViewModel { get; private set; } = new PinViewModel
        {
            TargetPinLength = 4,
            ValidatorFunc = (arg) =>
            {
                Helpers.Settings.Pin = "" + arg;
                Helpers.Settings.IsInitialized = true;
                Helpers.Settings.typeOfLogin = "Pin";
                return true;
            }
        };*/

        private readonly PinViewModel _pinViewModel;

        public PinViewModel PinViewModel => _pinViewModel;

        public PinAddViewModel()
        {
            _pinViewModel = new PinViewModel
            {
                TargetPinLength = 4,
                ValidatorFunc = (arg) => {
                    Helpers.Settings.Pin = "" + arg[0] + arg[1] + arg[2] + arg[3];
                    Helpers.Settings.IsInitialized = true;
                    Helpers.Settings.typeOfLogin = "Pin";
                    return true;   
                }
            };
            /*
            _pinViewModel.Success += (object sender, EventArgs e) =>
            {
                
            };*/
        }
    }
}
