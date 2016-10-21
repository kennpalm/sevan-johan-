using System;
using Foundation;
using UIKit;
using XOCV.Interfaces;
using XOCV.iOS.Renderers;
using XOCV.iOS.Services;

[assembly: Xamarin.Forms.Dependency (typeof (PictureService))]
namespace XOCV.iOS.Services
{
    public class PictureService : IPictureService
    {
        public static PollPageRenderer renderer;
        public void SavePictureToGallery (string filename, byte [] imageData)
        {
            renderer.SavePictureToGallery (filename, imageData);
        }
    }
}
