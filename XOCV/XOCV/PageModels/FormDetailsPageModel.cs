using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using Xamarin.Forms;
using XOCV.Interfaces;
using XOCV.Models;
using XOCV.Models.ResponseModels;
using XOCV.ViewModels.Base;

namespace XOCV.PageModels
{
    [ImplementPropertyChanged]
    public class FormDetailsPageModel : BasePageModel
    {
        private FormModel _tempForm;

        public ComplexFormsModel AvailableForms { get; set; }
        public IWebApiHelper WebApiHelper { get; set; }

        public ICommand AddNewCaptureCommand => new Command(async () => await AddNewCaptureCommandExecute());
        public ICommand EditCaptureCommand => new Command<CaptureModel>(async (capture) => await EditCaptureCommandExecute(capture));
        public ICommand DeleteCaptureCommand => new Command<CaptureModel>(async (capture) => await DeleteCaptureCommandExecute(capture));

        public FormDetailsPageModel ()
        {

        }

        public FormDetailsPageModel (IWebApiHelper webApiHelper)
        {
            WebApiHelper = webApiHelper;
        }

        public override void Init (object initData)
        {
            base.Init (initData);
            AvailableForms = initData as ComplexFormsModel;
        }

        private async Task AddNewCaptureCommandExecute()
        {
            await CoreMethods.PushPageModel<RegistrationFormPageModel>(AvailableForms);
        }

        private async Task EditCaptureCommandExecute(CaptureModel capture)
        {
            // ToDo: implementation for editing
        }

        private async Task DeleteCaptureCommandExecute(CaptureModel capture)
        {
            // ToDo: implementation for deleting
        }
    }
}