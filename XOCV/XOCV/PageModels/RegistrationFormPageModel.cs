using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using Xamarin.Forms;
using XOCV.Models.ResponseModels;
using XOCV.ViewModels.Base;

namespace XOCV.PageModels
{
    [ImplementPropertyChanged]
    public class RegistrationFormPageModel : BasePageModel
    {
        public ComplexFormsModel ComplexForm { get; set; }
        public List<string> ListOfSurveyorsName { get; set; }
        public List<string> ListOfStores { get; set; }
        public int IndexOfSurveyor { get; set; }
        public int IndexOfStore { get; set; }
        public ICommand OpenNewCaptureCommand => new Command(async () => await OpenNewCaptureCommandExecute());

        public RegistrationFormPageModel()
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            ComplexForm = initData as ComplexFormsModel;
            ListOfSurveyorsName = new List<string>();
            ListOfStores = new List<string>();


            if (ComplexForm == null) return;

            foreach (var surveyorModel in ComplexForm.Surveyors)
            {
                ListOfSurveyorsName.Add(surveyorModel.Name);
            }
            foreach (var storeNumberModel in ComplexForm.StoreNumbers)
            {
                ListOfStores.Add(storeNumberModel.StoreNumber.ToString());
            }
        }

        private async Task OpenNewCaptureCommandExecute()
        {
            await CoreMethods.PushPageModel<PollPageModel>(ComplexForm);
        }
    }
}