using System;
using Acr.UserDialogs;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XOCV.Interfaces;
using XOCV.PageModels;
using XOCV.Services;

#if !DEBUG || TRUE
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
#endif
namespace XOCV
{
    [ImplementPropertyChanged]
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class App : Application
    {
        public bool IsVisible { get; set; }

        public event EventHandler<EventArgs> LoggedOut;
        public static DataBaseService database { get; private set; }

        public App()
        {
            InitializeComponent();
            SetupDependencyInjection();
            SetStartPage();
            if (database == null)
            {
                database = new DataBaseService();
            }
        }

        private void SetupDependencyInjection()
        {
            FreshIOC.Container.Register<IWebApiHelper, WebApiHelper>();
            FreshIOC.Container.Register(DependencyService.Get<ICameraProvider>());
            FreshIOC.Container.Register(UserDialogs.Instance);
            FreshIOC.Container.Register(this);
        }

        public void SetStartPage()
        {
            var page = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            MainPage = new FreshNavigationContainer(page);
        }

        public virtual void OnLoggedOut()
        {
            LoggedOut?.Invoke(this, EventArgs.Empty);
        }

        #region States
        protected override void OnStart()
        {
            IsVisible = true;
            base.OnStart();
        }
        protected override void OnSleep()
        {
            IsVisible = false;
            base.OnSleep();
        }
        protected override void OnResume()
        {
            IsVisible = true;
            base.OnResume();
        }
        #endregion
    }
}