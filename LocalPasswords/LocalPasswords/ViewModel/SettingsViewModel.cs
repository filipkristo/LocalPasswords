using LocalPasswordsLib.BLL;
using LocalPasswordsLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace LocalPasswords.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private SettingsBLL BLL;

        public List<String> Languages { get; set; }

        public String Language
        {
            get
            {
                return BLL.GetLanguage();
            }
            set
            {                
                BLL.SaveLanguage(value);
            }
        }

        public String Theme
        {
            get
            {
                return BLL.GetTheme();
            }
            set
            {
                BLL.SaveTheme(value);
            }
        }

        public String OldPassword { get { return Get<String>(); } set { Set(value); } }
        public String MasterPassword { get { return Get<String>(); } set { Set(value); } }
        public String ConfirmPassword { get { return Get<String>(); } set { Set(value); } }

        public SettingsViewModel()
        {
            BLL = new SettingsBLL(resourceContextForCurrentView);
            Languages = ApplicationLanguages.Languages.ToList();
        }

        public RelayCommand ChangePasswordCommand
        {
            get { return new RelayCommand(ChangePassword); }
        }

        public void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                ChangePassword();
            }
        }

        public RelayCommand ChangeThemeCommand
        {
            get { return new RelayCommand(ChangeTheme); }
        }

        private void ChangeTheme()
        {
            Status = "";

            try
            {
                var settings = new SettingsBLL(resourceContextForCurrentView);
                var theme = settings.GetTheme();

                if (theme == "Dark")
                {
                    AppShell.Current.RequestedTheme = ElementTheme.Light;
                    settings.SaveTheme("Light");
                }
                else
                {
                    AppShell.Current.RequestedTheme = ElementTheme.Dark;
                    settings.SaveTheme("Dark");
                }
            }
            catch (Exception ex)
            {
                Status = ex.Message;
            }
        }

        private void ChangePassword()
        {
            Status = "";

            try
            {
                var credential = new CredentialBLL(resourceContextForCurrentView);
                credential.ChangeMasterPassword(OldPassword, MasterPassword, ConfirmPassword);
            }
            catch (Exception ex)
            {
                Status = ex.Message;
            }
        }


    }
}
