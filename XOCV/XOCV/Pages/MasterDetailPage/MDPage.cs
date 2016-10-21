using FreshMvvm;
using Xamarin.Forms;
using XOCV.Models.ResponseModels;
using XOCV.PageModels;

namespace XOCV.Pages.MasterDetailPage
{
    public class MDPage : Xamarin.Forms.MasterDetailPage
    {
        public MDPage ()
        {
            MessagingCenter.Subscribe<MenuPageModel, ComplexFormsModel> (this, "OnDetailChanged1", (sender, arg) => {
                IsPresented = false;
                var detailPage = new Page ();
                detailPage = FreshPageModelResolver.ResolvePageModel<FormDetailsPageModel> (arg);
                Detail = new FreshNavigationContainer (detailPage);

            });

            var detail = FreshPageModelResolver.ResolvePageModel<FormDetailsPageModel> ();
            Detail = new FreshNavigationContainer (detail);

            Master = new MenuPage ();
            MasterBehavior = MasterBehavior.Popover;
        }
        private bool initialized;
    }
}