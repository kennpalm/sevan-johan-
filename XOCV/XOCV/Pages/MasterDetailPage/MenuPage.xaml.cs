using Xamarin.Forms;
using XOCV.Helpers.ControlHelpers;
using XOCV.Interfaces;
using XOCV.Models;
using XOCV.PageModels;

namespace XOCV.Pages.MasterDetailPage
{
    public partial class MenuPage : ContentPage
    {
        private new ContentModel Content { get; set; }

        public MenuPage ()
        {
            BindingContext = new MenuPageModel (FreshMvvm.FreshIOC.Container.Resolve<IWebApiHelper> ());
            var pageModel = BindingContext as MenuPageModel;
            InitializeComponent ();
            //var pageModel = BindingContext as MenuPageModel;
            if (pageModel != null) {
                Content = pageModel.Content;
                BuildMenu ();
            }
            
            foreach (var a in Content.SetOfForms)
            {
                foreach (var b in a.Forms)
                {
                    foreach (var c in b.ComplexItems)
                    {
                        foreach (var d in c.Items)
                        {
                            App.database.SavePropItem(d.Properties);
                        }
                    }
                }
            }
            App.database.SaveItem(Content.SetOfForms[0]);
            var rez = App.database.GetItems();
            NavigationPage.SetHasNavigationBar (this, false);
        }

        #region Building page
        private void BuildMenu ()
        {
            menuPage.Children.Add (new Image () { Source = "Line2.png", HorizontalOptions = LayoutOptions.StartAndExpand, HeightRequest = 2, BackgroundColor = Color.White });
            foreach (var menuItems in Content.SetOfForms) {
                //foreach (var form in menuItems.Forms) {
                var menuButton = ButtonHelper.SetPolButtonProperties (menuItems);
                menuPage.Children.Add (menuButton);
                menuPage.Children.Add (new Image () { Source = "Line2.png", HorizontalOptions = LayoutOptions.StartAndExpand, HeightRequest = 2, BackgroundColor = Color.White });
            }
            //}
        }
        #endregion
    }
}