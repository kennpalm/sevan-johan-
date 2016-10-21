using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
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
using System.IO;
using XOCV.Pages;

namespace XOCV.PageModels
{
    [ImplementPropertyChanged]
    public class PollPageModel : BasePageModel
    {
        private ImageSource _picture;
        bool continueTimer = true;
        int _currentCapturePosition;

        private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext ();

        public int CountOfSecondsForLocalSaving { get; set; }
        public bool IsLastCapture { get; set; }
        public bool IsFirstCapture { get; set; }
        public long LastCaptureId { get; set; }
        public FormModel BuildModel { get; set; }
        public List<FormModel> ListOfCaptures { get; set; }
        public ComplexFormsModel ComplexForms { get; set; }
        public IWebApiHelper WebApiHelper { get; set; }
        public ICameraProvider CameraProvider { get; set; }
        public IUserDialogs UserDialog { get; set; }
        public ObservableCollection<string> FormNames { get; set; }
        public ObservableCollection<List<ImageSource>> PollPictures { get; set; }
        public string Status { get; set; }
        public int timeSpanOnForm { get; set; }
        public DateTime TimeOfCompletion { get; set; }
        public int CurrentCapturePosition {
            get { return _currentCapturePosition; }
            set {
                IsFirstCapture = false;
                IsLastCapture = false;
                if (value == 0) {
                    IsFirstCapture = true;
                } else if (value == ListOfCaptures.Count - 1) {
                    IsLastCapture = true;
                }

                if (ListOfCaptures [value] != null) {
                    UserDialog.ShowLoading ();
                    BuildModel = ListOfCaptures [value];
                    _currentCapturePosition = value;
                    PollPictures.Clear ();
                    foreach (var model in BuildModel.ComplexItems) {
                        PollPictures.Add (new List<ImageSource> ());
                    }
                    UserDialog.HideLoading ();

                }
            }
        }
        public ImageSource Picture {
            get { return _picture; }
            set { if (!Equals (value, _picture)) _picture = value; }
        }


        public ICommand SavePollResultCommand => new Command (async () => await SavePollResultCommandExecute ());
        public ICommand TakePictureCommand => new Command<int> (async (id) => await TakePictureCommandExecute (id));
        public ICommand SelectPictureCommand => new Command<int> (async (id) => await SelectPictureCommandExecute (id));
        public ICommand GoToNextCaptureCommand => new Command (async () => await GoToNextCaptureCommandExecute ());
        public ICommand GoToPrevCaptureCommand => new Command (async () => await GoToPrevCaptureCommandExecute ());
        //public ICommand GoToCaptureCommand => new Command<int> (async (index) => await GoToCaptureCommandExecute (index));

        public PollPageModel ()
        {
            CountOfSecondsForLocalSaving = 15;  // Currently every 15 seconds will save state of capture. 
        }

        public PollPageModel (IWebApiHelper webApiHelper, IUserDialogs userDialog, ICameraProvider cameraProvider)
        {
            WebApiHelper = webApiHelper;
            UserDialog = userDialog;
            CameraProvider = cameraProvider;
            IsFirstCapture = true;
        }

        public override async void Init (object initData)
        {
            base.Init (initData);
            BuildModel = initData as FormModel;
            ComplexForms = initData as ComplexFormsModel;
            if (ComplexForms != null) {
                ListOfCaptures = ComplexForms.Forms;
                LastCaptureId = ListOfCaptures.LastOrDefault ().Id;
                CurrentCapturePosition = 0;
                BuildModel = ListOfCaptures.FirstOrDefault ();
                IsLastCapture = false;
            }
            PollPictures = new ObservableCollection<List<ImageSource>> ();
            foreach (var model in BuildModel.ComplexItems) {
                PollPictures.Add (new List<ImageSource> ());
            }
            FormNames = new ObservableCollection<string> ();
            foreach (var form in ListOfCaptures) {
                FormNames.Add (string.Format ("{0}. {1}", ListOfCaptures.IndexOf (form) + 1, form.Title));
            }

            //Device.StartTimer (new System.TimeSpan (0, 0, 1), TimerCallback);
            //Device.StartTimer (new TimeSpan (0, 0, CountOfSecondsForLocalSaving), () => {
            //    SavePollResultCommandExecute ().Wait ();
            //    return true;
            //});
        }

        //bool TimerCallback ()
        //{
        //    timeSpanOnForm++;
        //    return continueTimer;
        //}

        private async Task SavePollResultCommandExecute ()
        {
            continueTimer = false;
            TimeOfCompletion = DateTime.Now;
            //ToDo: implement saving poll result
        }

        private async Task GoToNextCaptureCommandExecute ()
        {
            //if (IsLastCapture) {
            //    // ToDo: implament local saving and sending to the server
            //    bool result = await UserDialog.ConfirmAsync ("Do you want to submit?", "Confirmation", "OK", "Cancel");
            //    if (result) {
            //        UserDialog.ShowLoading ("Loading");
            //        await CoreMethods.PushPageModel<FormDetailsPageModel> (ComplexForms);
            //        UserDialog.HideLoading ();
            //    }
            //    return;
            //}

            //if (ListOfCaptures [CurrentCapturePosition + 1].Id == LastCaptureId) {
            //    IsLastCapture = true;
            //}

            //IsFirstCapture = false;

            //if (ListOfCaptures [CurrentCapturePosition + 1] != null) {
            //    UserDialog.ShowLoading ();
            //    BuildModel = ListOfCaptures [CurrentCapturePosition + 1];
            CurrentCapturePosition++;
            //    PollPictures.Clear ();
            //    foreach (var model in BuildModel.ComplexItems) {
            //        PollPictures.Add (new List<ImageSource> ());
            //    }
            //    UserDialog.HideLoading ();
            //}
        }

        private async Task GoToPrevCaptureCommandExecute ()
        {
            //if (CurrentCapturePosition - 1 == 0) {
            //    IsFirstCapture = true;
            //}

            //if (ListOfCaptures [CurrentCapturePosition - 1] != null) {
            //    UserDialog.ShowLoading ();
            //    BuildModel = ListOfCaptures [CurrentCapturePosition - 1];
            CurrentCapturePosition--;
            //PollPictures.Clear ();
            //foreach (var model in BuildModel.ComplexItems) {
            //    PollPictures.Add (new List<ImageSource> ());
            //}
            //UserDialog.HideLoading ();
            //}
        }

        //private async Task GoToCaptureCommandExecute (int index)
        //{
        //    IsFirstCapture = false;
        //    IsLastCapture = false;
        //    if (index == 0) 
        //    {
        //        IsFirstCapture = true;
        //    } else if (index == ListOfCaptures.Count - 1) 
        //    {
        //        IsLastCapture = true;
        //    }

        //    if (ListOfCaptures [index] != null) {
        //        UserDialog.ShowLoading ();
        //        BuildModel = ListOfCaptures [index];
        //        CurrentCapturePosition = index;
        //        PollPictures.Clear ();
        //        foreach (var model in BuildModel.ComplexItems) {
        //            PollPictures.Add (new List<ImageSource> ());
        //        }
        //        UserDialog.HideLoading ();
        //    }
        //}

        private async Task<MediaFile> TakePictureCommandExecute (int id)
        {
            return await CameraProvider.TakePhotoAsync (new CameraMediaStorageOptions {
                DefaultCamera = CameraDevice.Rear,
                MaxPixelDimension = 400
            }).ContinueWith (
                t => {
                    if (t.IsFaulted) {
                        Status = t.Exception.InnerException.ToString ();
                    } else if (t.IsCanceled) {
                        Status = "Canceled";
                    } else {
                        var mediaFile = t.Result;

                        MemoryStream stream = new MemoryStream ();

                        mediaFile.Source.CopyTo (stream);

                        DependencyService.Get<IPictureService> ().SavePictureToGallery (string.Format ("SevanPhoto{0}", DateTime.Now), stream.ToArray ());

                        PollPictures [id].Add (ImageSource.FromStream (() => mediaFile.Source));
                        ObservableCollection<List<ImageSource>> Pic = new ObservableCollection<List<ImageSource>> ();
                        foreach (var p in PollPictures) {
                            Pic.Add (p);
                        }
                        PollPictures.Clear ();
                        PollPictures = Pic;
                        return mediaFile;
                    }
                    return null;
                });
        }

        private async Task SelectPictureCommandExecute (int id)
        {
            Picture = null;
            try {
                var mediaFile = await CameraProvider.SelectPhotoAsync (new CameraMediaStorageOptions {
                    DefaultCamera = CameraDevice.Front,
                    MaxPixelDimension = 400
                });
                Picture = ImageSource.FromStream (() => mediaFile.Source);
            } catch (System.Exception ex) {
                Status = ex.Message;
            }
        }
    }
}