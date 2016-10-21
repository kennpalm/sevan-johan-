using System;
using System.IO;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms;using XOCV.iOS.Helpers;using XOCV.iOS.PlatformSpecific;using XOCV.Interfaces;using XOCV.Models;
//[assembly: Dependency(typeof(CameraProvider))]
namespace XOCV.iOS.PlatformSpecific
{
    //public class CameraProvider : ICameraProvider
    //{
    //    public Task<CameraResultModel> TakePictureAsync()
    //    {
    //        var tcs = new TaskCompletionSource<CameraResultModel>();

    //        Camera.TakePicture(UIApplication.SharedApplication.KeyWindow.RootViewController, (imagePickerResult) => {

    //            if (imagePickerResult == null)
    //            {
    //                tcs.TrySetResult(null);
    //                return;
    //            }

    //            var photo = imagePickerResult.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;

    //            var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
    //            string jpgFilename = Path.Combine(documentsDirectory, Guid.NewGuid() + ".jpg");
    //            NSData imgData = photo.AsJPEG();
    //            NSError err = null;

    //            if (imgData.Save(jpgFilename, false, out err))
    //            {
    //                CameraResultModel result = new CameraResultModel();
    //                result.Picture = ImageSource.FromStream(imgData.AsStream);
    //                result.FilePath = jpgFilename;

    //                tcs.TrySetResult(result);
    //            }
    //            else
    //            {
    //                tcs.TrySetException(new Exception(err.LocalizedDescription));
    //            }
    //        });

    //        return tcs.Task;
    //    }
    //}
}