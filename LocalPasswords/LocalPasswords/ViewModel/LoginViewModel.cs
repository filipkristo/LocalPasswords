using LocalPasswordsLib.BLL;
using LocalPasswordsLib.Common;
using LocalPasswordsLib.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace LocalPasswords.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginModel Model { get; set; }

        public LoginViewModel()
        {
            Model = new LoginModel();
        }

        private void Login()
        {
            Status = "";

            try
            {
                var credential = new CredentialBLL(resourceContextForCurrentView);
                var pass = credential.RetrivePassword();

                if (pass != Model.MasterPassword)
                {
                    var error = ResourceManager.Current.MainResourceMap.GetValue("Resources/LoginError", resourceContextForCurrentView).ValueAsString;
                    throw new Exception(error);
                }

                App.RootFrame.Navigate(typeof(AppShell));
            }
            catch (Exception ex)
            {
                Status = ex.Message;
            }
        }

        public RelayCommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }

        public void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                Login();
            }
        }
    }
}
