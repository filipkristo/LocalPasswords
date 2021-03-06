﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml;

namespace LocalPasswordsLib.BLL
{
    public class SettingsBLL
    {
        private ResourceContext resourceContext;

        public SettingsBLL()
        {

        }

        public SettingsBLL(ResourceContext ResourceContext)
        {
            this.resourceContext = ResourceContext;
        }

        public void SaveLanguage(String Language)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["Language"] = Language;
        }

        public String GetLanguage()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            return localSettings.Values["Language"]?.ToString();
        }


        public void SaveTheme(String Theme)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["Theme"] = Theme;
        }

        public String GetTheme()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            return localSettings.Values["Theme"]?.ToString();
        }

    }
}
