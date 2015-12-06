using LocalPasswordsLib.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace LocalPasswords.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string password = "";
        public string Password { get { return password; } set { Set(ref password, value); } }

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (state.ContainsKey(nameof(Password)))
            {
                Password = state[nameof(Password)]?.ToString();
                state.Clear();
            }
            else
            {
                Password = parameter?.ToString();
            }
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            if (suspending)
                state[nameof(Password)] = Password;

            await Task.Yield();
        }

        public override void OnNavigatingFrom(NavigatingEventArgs args)
        {
            args.Cancel = false;
        }

        public void SavePassword()
        {
            var credential = new CredentialBLL();

            var pass = credential.RetrivePassword();

            if (pass == Password)
                this.NavigationService.Navigate(typeof(AppShell));
        }
    }
}
