using LocalPasswordsLib.BLL;
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
        private LoginModel Model;

        public LoginViewModel()
        {
            Model = new LoginModel();
        }

        public void Login()
        {
            var credential = new CredentialBLL();
            var pass = credential.RetrivePassword();

            if (pass != Model.MasterPassword)
                throw new Exception("Check password");

            App.RootFrame.Navigate(typeof(AppShell));
        }
    }
}
