using Xamarin.Forms;
using XOCV.Pages.Base;

namespace XOCV.Pages
{
    public interface IBaseUrl { string Get(); }

    public partial class MainPage : XOCVPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}