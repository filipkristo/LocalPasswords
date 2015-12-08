using LocalPasswordsLib.BLL;
using LocalPasswordsLib.Common;
using LocalPasswordsLib.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.ApplicationModel.Resources.Core;

namespace LocalPasswords.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        public RegisterModel Model { get; set; }

        private CredentialBLL BLL;

        public RegisterViewModel()
        {
            Model = new RegisterModel();
            BLL = new CredentialBLL(resourceContextForCurrentView);
        }

        private void Register()
        {
            Status = "";

            try
            {                                
                BLL.SavePassword(Model.MasterPassword, Model.ConfirmPassword);
                App.RootFrame.Navigate(typeof(AppShell));
            }
            catch (Exception ex)
            {                
                Status = ex.Message;
            }
        }

        public RelayCommand RegisterCommand
        {
            get { return new RelayCommand(Register); }
        }

        public void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Register();
            }
        }
    }
}
