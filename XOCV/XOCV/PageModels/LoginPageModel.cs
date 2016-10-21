using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using PropertyChanged;
using Xamarin.Forms;
using XOCV.Interfaces;
using XOCV.Models;
using XOCV.Pages.MasterDetailPage;
using XOCV.ViewModels.Base;

namespace XOCV.PageModels
{
    [ImplementPropertyChanged]
    public class LoginPageModel : BasePageModel
    {
        public LoginModel Login { get; set; }

        public IWebApiHelper WebApiHelper { get; set; }
        public IUserDialogs Dialogs { get; set; }
        
        public ICommand LoginCommand => new Command(async () => await LoginCommandExecute());

        public override void Init(object initData)
        {
            base.Init(initData);
            Login = new LoginModel { UserName = "admin", Password = "123qwe" };   // ToDo: test data. Remove this.
        }

        public LoginPageModel() { }

        public LoginPageModel(IWebApiHelper webApiHelper, IUserDialogs dialogService)
        {
            WebApiHelper = webApiHelper;
            Dialogs = dialogService;
        }

        private async Task LoginCommandExecute()
        {
            Dialogs.ShowLoading("Loading");
            //Application.Current.MainPage = new MDPage();
            if (!string.IsNullOrEmpty(Login.UserName) && !string.IsNullOrEmpty(Login.Password))
            {
                var result = await WebApiHelper.Authorization(Login);
                if (result.Token != null)
                {
                    Application.Current.MainPage = new MDPage();
                }
                else
                {
                    await Dialogs.AlertAsync(result.Error.Details, result.Error.Message, "OK");
                }
            }
            else
            {
                await Dialogs.AlertAsync("Username or password cannot be empty!", "Warning!", "OK");
            }
            Dialogs.HideLoading();
        }
    }
}