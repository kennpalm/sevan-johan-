using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
using XOCV.Converters;
using XOCV.Helpers.ControlHelpers;
using XOCV.Models.ResponseModels;
using XOCV.PageModels;
using XOCV.Pages.Base;
using XOCV.Views.RadioButtons;
using System.Collections.Generic;
using System.Linq;

namespace XOCV.Pages
{
    public partial class PollPage : XOCVPage
    {
        public int currentComplexItem = 0;
        bool isInited;

        public static FormModel BuildModel { get; set; }
        public ObservableCollection<Image> images { get; set; }
        public ObservableCollection<Grid> imageGrids { get; set; }

        public ObservableCollection<List<ImageSource>> PollPictures {
            get { return (ObservableCollection<List<ImageSource>>)GetValue (PollPicturesProperty); }
            set { SetValue (PollPicturesProperty, value); }
        }

        public static readonly BindableProperty PollPicturesProperty = BindableProperty.Create
            (
                nameof (PollPictures), typeof (object), typeof (PollPage), null, BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) => {
                    ((PollPage)bindable).UpdatePollPhotoSections ();
                }
            );

        public PollPage ()
        {
            InitializeComponent ();
            images = new ObservableCollection<Image> ();
            imageGrids = new ObservableCollection<Grid> ();
            NavigationPage.SetHasNavigationBar (this, false);

        }

        #region Building page
        private void BuildPage ()
        {
            isInited = false;
            var titleLabel = new Label {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                FontSize = 18
            };
            titleLabel.SetBinding (Label.TextProperty, "BuildModel.Title");
            currentPage.Children.Add (titleLabel);

            foreach (var pageItem in BuildModel.ComplexItems) {
                var cameraButton = new CameraButton {
                    ButtonId = currentComplexItem,
                    Text = "Take a picture",
                    TextColor = Color.White,
                    BackgroundColor = Color.Black,
                    HorizontalOptions = LayoutOptions.Center,
                    CommandParameter = currentComplexItem++
                };
                cameraButton.SetBinding (Button.CommandProperty, "TakePictureCommand");
                foreach (var item in pageItem.Items) {
                    switch (item.Name) {
                    case "labelKey": {
                            var label = LabelHelper.SetLabelProperties (item);
                            currentPage.Children.Add (label);
                            break;
                        }
                    case "entryKey": {
                            var entry = EntryHelper.SetEntryProperties (item);
                            entry.SetBinding (Entry.TextProperty, new Binding ("Value", BindingMode.TwoWay, null, null, null, item));
                            currentPage.Children.Add (entry);
                            break;
                        }
                    case "radioGroupKey": {
                            var radioGroup = BindableRadioGroupHelper.SetRadioGroupProperties (item);
                            radioGroup.SetBinding (BindableRadioGroup.SelectedIndexProperty, new Binding ("Value", BindingMode.TwoWay, null, null, null, item));
                            currentPage.Children.Add (radioGroup);
                            break;
                        }
                    }
                }
                var imageGrid = new Grid ();

                imageGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });
                imageGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (7, GridUnitType.Star) });
                imageGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });

                var image = new Image {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Aspect = Aspect.AspectFit
                };
                imageGrid.Children.Add (image, 1, 0);
                imageGrid.HeightRequest = 500;
                images.Add (image);

                var prevImageButton = new Button {
                    TextColor = Color.Black,
                    BackgroundColor = Color.Transparent,
                    BorderRadius = 3,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Text = "◄",
                    FontSize = 30,
                };
                imageGrid.Children.Add (prevImageButton, 0, 0);
                var nextImageButton = new Button {
                    TextColor = Color.Black,
                    BackgroundColor = Color.Transparent,
                    BorderRadius = 3,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Text = "►",
                    FontSize = 30,
                };
                prevImageButton.Clicked += (sender, e) => {
                    int i = images.IndexOf (image);
                    int imageIndex = PollPictures [i].IndexOf (PollPictures [i].Where (s => s.Id == image.Source.Id).First ());
                    int newIndex = (imageIndex == 0) ? imageIndex : imageIndex - 1;
                    image.Source = PollPictures [i] [newIndex];
                };
                nextImageButton.Clicked += (sender, e) => {
                    int i = images.IndexOf (image);
                    int imageIndex = PollPictures [i].IndexOf (PollPictures [i].Where (s => s.Id == image.Source.Id).First ());
                    int newIndex = (imageIndex == PollPictures [i].Count - 1) ? imageIndex : imageIndex + 1;
                    image.Source = PollPictures [i] [newIndex];
                };
                imageGrid.Children.Add (nextImageButton, 2, 0);

                imageGrid.IsVisible = false;

                imageGrids.Add (imageGrid);

                currentPage.Children.Add (imageGrid);

                currentPage.Children.Add (cameraButton);

                var line = new BoxView {
                    HeightRequest = 2,
                    BackgroundColor = Color.Green
                };
                currentPage.Children.Add (line);
            }

            var buttonsGrid = new Grid ();

            buttonsGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });
            buttonsGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });

            var prevButton = new Button {
                TextColor = Color.White,
                BackgroundColor = Color.Black,
                BorderRadius = 3,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Opacity = 0.5,
                Text = "Prev",
                WidthRequest = 100
            };
            prevButton.Clicked += NextButtonOnClicked;
            prevButton.SetBinding (Button.CommandProperty, "GoToPrevCaptureCommand");
            prevButton.SetBinding (Button.IsVisibleProperty, new Binding ("IsFirstCapture", BindingMode.Default, new InverseBooleanConverter ()));
            buttonsGrid.Children.Add (prevButton, 0, 0);

            var nextButton = new Button {
                TextColor = Color.White,
                BackgroundColor = Color.Black,
                BorderRadius = 3,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Opacity = 0.5,
                WidthRequest = 100
            };
            nextButton.Clicked += NextButtonOnClicked;
            nextButton.SetBinding (Button.TextProperty, new Binding ("IsLastCapture", BindingMode.Default, new CaptureNavigationBoolToStringConverter ()));
            nextButton.SetBinding (Button.CommandProperty, "GoToNextCaptureCommand");
            buttonsGrid.Children.Add (nextButton, 1, 0);
            currentPage.Children.Add (buttonsGrid);
            this.SetBinding (PollPicturesProperty, "PollPictures");
            isInited = true;
        }

        private void NextButtonOnClicked (object sender, EventArgs eventArgs)
        {
            currentPage.Children.Clear ();
            images.Clear ();
            currentComplexItem = 0;
            OnBindingContextChanged ();
            currentScrollView.ScrollToAsync (currentFrame, ScrollToPosition.Start, false);
        }

        #endregion

        protected override void OnBindingContextChanged ()
        {
            base.OnBindingContextChanged ();
            var pageModel = BindingContext as PollPageModel;
            if (pageModel != null) {
                BuildModel = pageModel.BuildModel;
                imageGrids.Clear ();
                //formPicker.SetBinding(Picker.SelectedIndexProperty, new Binding("CurrentCapturePosition", BindingMode.OneWayToSource));
                //formPicker.SelectedIndexChanged += (sender, e) => {
                //    var _pageModel = BindingContext as PollPageModel;
                //    _pageModel.CurrentCapturePosition = ((Picker)sender).SelectedIndex;
                //};

                //formPicker.Items.Clear ();
                //if (formPicker.Items.Count == 0)
                //{
                //    foreach (var form in pageModel.FormNames)
                //    {
                //        formPicker.Items.Add(form);
                //    }
                //}
                BuildPage ();
            }
        }

        private void UpdatePollPhotoSections ()
        {
            for (int i = 0; i < PollPictures.Count; i++) {
                if (PollPictures [i].Count > 0) {
                    imageGrids [i].IsVisible = true;
                    images [i].Source = PollPictures [i].Last ();
                }
            }
        }
    }
}