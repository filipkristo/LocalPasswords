using LocalPasswordsLib.BLL;
using LocalPasswordsLib.Common;
using LocalPasswordsLib.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var credential = new CredentialBLL();

            if (Model.MasterPassword != Model.ConfirmPassword)
                throw new Exception("Check password");

            credential.SavePassword(Model.MasterPassword);

            App.RootFrame.Navigate(typeof(AppShell));
        }

        public RelayCommand RegisterCommand
        {
            get { return new RelayCommand(Register); }
        }
    }
}
