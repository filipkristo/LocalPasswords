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

        public RegisterViewModel()
        {
            Model = new RegisterModel();
        }

        private void Register()
        {
            Status = "";

            try
            {
                var credential = new CredentialBLL();

                if(String.IsNullOrWhiteSpace(Model.MasterPassword) && String.IsNullOrWhiteSpace(Model.ConfirmPassword))
                {
                    var error = ResourceManager.Current.MainResourceMap.GetValue("Resources/RegisterErrorFieldNull", resourceContextForCurrentView).ValueAsString;
                    throw new Exception(error);
                }
                else if(String.IsNullOrWhiteSpace(Model.MasterPassword) || String.IsNullOrWhiteSpace(Model.ConfirmPassword))
                {
                    var error = ResourceManager.Current.MainResourceMap.GetValue("Resources/RegisterErrorOneFielNull", resourceContextForCurrentView).ValueAsString;
                    throw new Exception(error);
                }
                else if (Model.MasterPassword != Model.ConfirmPassword)
                {
                    var error = ResourceManager.Current.MainResourceMap.GetValue("Resources/RegisterErrorConfirm", resourceContextForCurrentView).ValueAsString;
                    throw new Exception(error);
                }

                credential.SavePassword(Model.MasterPassword);

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
