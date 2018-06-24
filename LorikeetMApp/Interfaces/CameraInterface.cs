using System;
using System.Threading.Tasks;
using LorikeetMApp.Models;
using Xamarin.Forms;

namespace LorikeetMApp.Interfaces
{
    public interface CameraInterface
    {
        void LaunchCamera(FileFormatEnum imageType, string imageId = null);
        void LaunchGallery(FileFormatEnum imageType, string imageId = null);
    }
}
