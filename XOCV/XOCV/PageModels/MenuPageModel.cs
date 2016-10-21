using XOCV.ViewModels.Base;
using Xamarin.Forms;
using XOCV.Models;
using PropertyChanged;
using XOCV.Interfaces;
using XOCV.Models.ResponseModels;
using System.Windows.Input;
using System.Threading.Tasks;

namespace XOCV.PageModels
{
    [ImplementPropertyChanged]
    public class MenuPageModel : BasePageModel
    {
        public ContentModel Content { get; set; }
        public IWebApiHelper WebApiHelper { get; set; }

        public ICommand OpenFormsDetailCommand => new Command<ComplexFormsModel> (async (model) => await OpenFormsDetailCommandCommandExecute (model));

        public MenuPageModel ()
        {

        }

        public MenuPageModel (IWebApiHelper webApiHelper)
        {
            WebApiHelper = webApiHelper;
            Content = WebApiHelper.GetAllContent ("testToken").Result;
            var arg = Content.SetOfForms [0];
            MessagingCenter.Send (this, "OnDetailChanged1", arg);
        }

        public override async void Init (object initData)
        {
            base.Init (initData);
        }

        async Task OpenFormsDetailCommandCommandExecute (ComplexFormsModel model)
        {
            var arg = model;
            MessagingCenter.Send<MenuPageModel, ComplexFormsModel> (this, "OnDetailChanged1", arg);
        }
    }
}