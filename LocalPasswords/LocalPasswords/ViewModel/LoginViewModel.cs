using LocalPasswordsLib.BLL;
using LocalPasswordsLib.Common;
using LocalPasswordsLib.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var credential = new CredentialBLL();
            var pass = credential.RetrivePassword();

            if (pass != Model.MasterPassword)
                throw new Exception("Check password");

            App.RootFrame.Navigate(typeof(AppShell));
        }

        public RelayCommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }
    }
}
