using System;
namespace XOCV.Interfaces
{
    public interface IPictureService
    {
        void SavePictureToGallery (string filename, byte [] imageData);
    }
}
