// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using XOCV.Models;
using XOCV.Models.ResponseModels;

namespace XOCV.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        #region Setting Constants
        private const string SettingsKey = "settings_key";
        private const string TokenKey = "token_key";
        private static readonly string SettingsDefault = string.Empty;
        private const string ContentKey = "content_key";
        #endregion

        public static string GeneralSettings
        {
            get { return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault); }
            set { AppSettings.AddOrUpdateValue(SettingsKey, value); }
        }

        public static string LocalToken
        {
            get { return AppSettings.GetValueOrDefault(TokenKey, string.Empty); }
            set { AppSettings.AddOrUpdateValue(TokenKey, value); }
        }

        public static ComplexFormsModel ComplexContentModel
        {
            get { return AppSettings.GetValueOrDefault(ContentKey, new ComplexFormsModel()); }
            set { AppSettings.AddOrUpdateValue(ContentKey, value); }
        }
    }
}