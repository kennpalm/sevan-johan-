﻿using System.Linq;
using Xamarin.Forms;
using XOCV.Helpers.ControlHelpers.Base;
using XOCV.Models.ResponseModels;
using XOCV.Resources;

namespace XOCV.Helpers.ControlHelpers
{
    public class EntryHelper : BaseControlHelper
    {
        public static Entry SetEntryProperties(Item model)
        {
            Entry entry = new Entry();

            entry.Placeholder = "Comment";
            entry.HorizontalOptions = LayoutOptions.FillAndExpand;
            entry.BackgroundColor = Color.White;
            entry.TextColor = Color.Black;
            entry.PlaceholderColor = Color.Gray;
            entry.FontSize = 18;
            //entry.Placeholder = model.Properties.FirstOrDefault(x => x.Key == XOCVRes.Placeholder).Value;
            //entry.FontSize = GetNumericValue(model.Properties.FirstOrDefault(x => x.Key == XOCVRes.FontSize).Value);
            //entry.HorizontalOptions = GetPositionOption(model.Properties.FirstOrDefault(x => x.Key == XOCVRes.HorizontalOptions).Value);
            //entry.VerticalOptions = GetPositionOption(model.Properties.FirstOrDefault(x => x.Key == XOCVRes.VerticalOptions).Value);
            //entry.FontAttributes = GetFontAttributes(model.Properties.FirstOrDefault(x => x.Key == XOCVRes.FontAttributes).Value);
            //entry.HorizontalTextAlignment = GetTextAlignmentPosition(model.Properties.FirstOrDefault(x => x.Key == XOCVRes.HorizontalTextAlignment).Value);
            //entry.TextColor = GetColor(model.Properties.FirstOrDefault(x => x.Key == XOCVRes.TextColor).Value);
            //entry.PlaceholderColor = GetColor(model.Properties.FirstOrDefault(x => x.Key == XOCVRes.PlaceholderColor).Value);

            return entry;
        }
    }
}