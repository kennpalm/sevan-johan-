using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XOCV.iOS.Renderers;
using XOCV.iOS.Services;
using XOCV.Pages;

[assembly: ExportRenderer (typeof (PollPage), typeof (PollPageRenderer))]
namespace XOCV.iOS.Renderers
{
    public class PollPageRenderer : PageRenderer
    {
        public PollPageRenderer ()
        {
            PictureService.renderer = this;
        }

        public void SavePictureToGallery (string filename, byte [] imageData)
        {
            var chartImage = new UIImage (NSData.FromArray (imageData));
            InvokeOnMainThread (() => {
                chartImage.SaveToPhotosAlbum ((image, error) => {
                    if (error != null) {
                        Console.WriteLine (error);
                    }
                });
            });
        }
    }
}


