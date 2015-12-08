using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.Security.Credentials;

namespace LocalPasswordsLib.BLL
{
    public class CredentialBLL
    {
        public const String Resource = "LocalPasswords";
        public const String Username = "Username";

        private ResourceContext resourceContext;

        public CredentialBLL()
        {

        }

        public CredentialBLL(ResourceContext ResourceContext)
        {
            this.resourceContext = ResourceContext;
        }

        public Boolean CheckIfExists()
        {
            var vault = new PasswordVault();
            return vault.RetrieveAll().ToList().Where(x => x.Resource == Resource).Any();            
        }

        public String RetrivePassword()
        {
            var vault = new PasswordVault();

            var cred = vault.FindAllByResource(Resource).FirstOrDefault();
            cred.RetrievePassword();

            return cred.Password;
        }

        public void SavePassword(String Password, String ConfirmPassword)
        {
            if (String.IsNullOrWhiteSpace(Password) && String.IsNullOrWhiteSpace(ConfirmPassword))
            {
                var error = ResourceManager.Current.MainResourceMap.GetValue("Resources/RegisterErrorFieldNull", resourceContext).ValueAsString;
                throw new Exception(error);
            }
            else if (String.IsNullOrWhiteSpace(Password) || String.IsNullOrWhiteSpace(ConfirmPassword))
            {
                var error = ResourceManager.Current.MainResourceMap.GetValue("Resources/RegisterErrorOneFielNull", resourceContext).ValueAsString;
                throw new Exception(error);
            }
            else if (Password != ConfirmPassword)
            {
                var error = ResourceManager.Current.MainResourceMap.GetValue("Resources/RegisterErrorConfirm", resourceContext).ValueAsString;
                throw new Exception(error);
            }

            var vault = new PasswordVault();
            var cred = new PasswordCredential(Resource, Username, Password);

            if(CheckIfExists())
            {
                var passList = vault.FindAllByResource(Resource).ToList();

                foreach (var item in passList)
                {
                    vault.Remove(item);
                }
            }            

            vault.Add(cred);
        }

        public void ChangeMasterPassword(String OldPassword, String NewPassword, String ConfirmPassword)
        {
            var currentPassword = RetrivePassword();

            if (currentPassword != OldPassword)
            {
                var error = ResourceManager.Current.MainResourceMap.GetValue("Resources/ResetPasswordError", resourceContext).ValueAsString;
                throw new Exception(error);
            }

            SavePassword(NewPassword, ConfirmPassword);
        }
    }
}
