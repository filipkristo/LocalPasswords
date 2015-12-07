using LocalPasswordsLib.BLL;
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
        private RegisterModel Model;

        public RegisterViewModel()
        {
            Model = new RegisterModel();
        }

        public void RegisterModel()
        {
            var credential = new CredentialBLL();

            if (Model.MasterPassword != Model.ConfirmPassword)
                throw new Exception("Check password");

            credential.SavePassword(Model.MasterPassword);
        }
    }
}
