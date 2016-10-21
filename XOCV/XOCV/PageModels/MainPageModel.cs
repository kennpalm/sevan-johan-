using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using PropertyChanged;
using Xamarin.Forms;
using XOCV.Helpers;
using XOCV.Interfaces;
using XOCV.Models;
using XOCV.Models.ResponseModels;
using XOCV.ViewModels.Base;

namespace XOCV.PageModels
{
    [ImplementPropertyChanged]
    public class MainPageModel : BasePageModel
    {
        private ImageSource _picture;
        private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();

        public ImageSource Picture
        {
            get { return _picture; }
            set { if (!Equals(value, _picture)) _picture = value; }
        }
        public string Status { get; set; }
        public HtmlWebViewSource TestHtmlModel { get; set; }
        public IWebApiHelper WebApiHelper { get; set; }
        public IUserDialogs UserDialog { get; set; }
        public ICameraProvider CameraProvider { get; set; }
        public ICommand GoToPollCommand => new Command(async () => await GoToPollCommandExecute());
        //public ICommand TakePictureCommand => new Command(async () => await TakePictureCommandExecute()); //ToDo: uncomment this after testing on real device

        public override void Init(object initData)
        {
            base.Init(initData);

            MessagingCenter.Subscribe<MenuPageModel, ContentModel>(this, "OnDetailChanged", (sender, arg) => {
                //Title = arg.ActivityName;
                //Tasks = arg.Tasks;
                //How = arg.How;
                //AddCommandToTasks ();
            });
        }

        public MainPageModel() { }

        public MainPageModel(IWebApiHelper webApiHelper, IUserDialogs userDialog, ICameraProvider cameraProvider)
        {
            WebApiHelper = webApiHelper;
            UserDialog = userDialog;
            CameraProvider = cameraProvider;
        }

        private async Task GoToPollCommandExecute()
        {
            UserDialog.ShowLoading("Loading");
            await CoreMethods.PushPageModel<PollPageModel>();
            UserDialog.HideLoading();
        }

        //ToDo: need test on real device
        //private async Task<MediaFile> TakePictureCommandExecute()
        //{
        //    return await CameraProvider.TakePhotoAsync(new CameraMediaStorageOptions
        //    {
        //        DefaultCamera = CameraDevice.Rear,
        //        MaxPixelDimension = 400
        //    }).ContinueWith(
        //        t =>
        //        {
        //            if (t.IsFaulted)
        //            {
        //                Status = t.Exception.InnerException.ToString();
        //            }
        //            else if (t.IsCanceled)
        //            {
        //                Status = "Canceled";
        //            }
        //            else
        //            {
        //                var mediaFile = t.Result;

        //                Picture = ImageSource.FromStream(() => mediaFile.Source);

        //                return mediaFile;
        //            }
        //            return null;
        //        });
        //}

        private async Task SelectPicture()
        {
            Picture = null;
            try
            {
                var mediaFile = await CameraProvider.SelectPhotoAsync(new CameraMediaStorageOptions
                {
                    DefaultCamera = CameraDevice.Front,
                    MaxPixelDimension = 400
                });
                Picture = ImageSource.FromStream(() => mediaFile.Source);
            }
            catch (System.Exception ex)
            {
                Status = ex.Message;
            }
        }
    }
}