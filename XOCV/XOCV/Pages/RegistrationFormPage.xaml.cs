using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using XOCV.Models.ResponseModels;
using XOCV.PageModels;
using XOCV.Pages.Base;
using XOCV.Views;

namespace XOCV.Pages
{
    public partial class RegistrationFormPage : XOCVPage
    {
        public List<string> ListOfSurveyorsName { get; set; }
        public List<string> ListOfStores { get; set; }
        public ComplexFormsModel ComplexForm { get; set; }

        public RegistrationFormPage()
        {
            InitializeComponent();

            bindableRadioGroup.ItemsSource = new ObservableCollection<string>
            {
                null,
                "Full PSN",
                "NAV Only"
            };
            NavigationPage.SetHasNavigationBar(this, false);

            // ToDo: need test it and redo functionality like this!!!
            //surveyorPicker.SetBinding(BindablePicker.ItemsSourceProperty, new Binding("ListOfSurveyorsName", BindingMode.TwoWay));
            //surveyorPicker.SetBinding(BindablePicker.SelectedIndexProperty, new Binding("IndexOfSurveyor", BindingMode.TwoWay));
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var pageModel = BindingContext as RegistrationFormPageModel;
            if (pageModel != null)
            {
                ListOfSurveyorsName = new List<string>();
                ListOfStores = new List<string>();

                ComplexForm = pageModel.ComplexForm;
                ListOfSurveyorsName = pageModel.ListOfSurveyorsName;
                ListOfStores = pageModel.ListOfStores;

                foreach (var s in ListOfSurveyorsName)
                {
                    surveyorPicker.Items.Add(s);
                }
                foreach (var storeNumber in ListOfStores)
                {
                    storeNumbersPicker.Items.Add(storeNumber);
                }
            }
        }

        private void StoreNumbersChanged(object sender, EventArgs e)
        {
            var currentIndex = storeNumbersPicker.SelectedIndex;
            var tempStoreNumbersModel = ComplexForm.StoreNumbers[currentIndex];
            cityLabel.Text = tempStoreNumbersModel.City;
            stateState.Text = tempStoreNumbersModel.State;
            zipLabel.Text = tempStoreNumbersModel.Zip.ToString();
            contractorLabel.Text = tempStoreNumbersModel.Contractor;
        }

        private void SurveyorChanged(object sender, EventArgs e)
        {
            var currentIndex = surveyorPicker.SelectedIndex;
            var tempSurveyorModel = ComplexForm.Surveyors[currentIndex];
            emailLabel.Text = tempSurveyorModel.Email;
        }
    }
}