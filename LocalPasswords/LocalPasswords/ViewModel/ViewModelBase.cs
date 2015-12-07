using LocalPasswordsLib.Common;
using LocalPasswordsLib.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace LocalPasswords.ViewModel
{
    public class ViewModelBase : NotifyBase
    {
        protected ResourceContext resourceContextForCurrentView;

        public String ErrorHeader { get; private set; }
        public String PleaseViewHeader { get; private set; }
        public String YesText { get; private set; }
        public String NoText { get; private set; }
        public String SuccessfullyText { get; private set; }

        private Dictionary<String, RelayCommand> commands = new Dictionary<String, RelayCommand>();
        private Dictionary<String, Boolean> executing = new Dictionary<String, Boolean>();

        public ViewModelBase()
        {
            resourceContextForCurrentView = ResourceContext.GetForCurrentView();

            ErrorHeader = ResourceManager.Current.MainResourceMap.GetValue("Resources/ErrorHeader", resourceContextForCurrentView).ValueAsString;
            PleaseViewHeader = ResourceManager.Current.MainResourceMap.GetValue("Resources/PleaseWait", resourceContextForCurrentView).ValueAsString;

            YesText = ResourceManager.Current.MainResourceMap.GetValue("Resources/Yes", resourceContextForCurrentView).ValueAsString;
            NoText = ResourceManager.Current.MainResourceMap.GetValue("Resources/No", resourceContextForCurrentView).ValueAsString;

            SuccessfullyText = ResourceManager.Current.MainResourceMap.GetValue("Resources/Successfully", resourceContextForCurrentView).ValueAsString;

            ProgressVisible = Visibility.Collapsed;
            Status = "";            
        }

        public Visibility ProgressVisible
        {
            get { return Get<Visibility>(); }
            set { Set(value); }
        }

        public String Status
        {
            get { return Get<String>(); }
            set { Set(value); }
        }

        public virtual async Task OnNavigatedTo(object sender, NavigationEventArgs e)
        {
            await Task.Yield();
        }

        public virtual async Task OnNavigatedFrom(object sender, NavigationEventArgs e)
        {
            await Task.Yield();
        }

        public RelayCommand DefaultCommand(Func<Task> Action, [CallerMemberName] string name = "")
        {
            if (commands.ContainsKey(name))
            {
                return commands[name];
            }
            else
            {
                var relay = new RelayCommand(async () => await executeAction(Action, name), () => GetIsExecuting(name));
                commands.Add(name, relay);

                return relay;
            }
        }

        private async Task executeAction(Func<Task> Action, String name)
        {
            ProgressVisible = Visibility.Visible;
            Status = PleaseViewHeader;

            var Command = commands[name];

            try
            {
                executing[name] = true;
                Command.RaiseCanExecuteChanged();

                await Action.Invoke();
            }
            catch (Exception ex)
            {
                var result = new MessageDialog(ex.Message, ErrorHeader).ShowAsync();
            }
            finally
            {
                Status = "";
                ProgressVisible = Visibility.Collapsed;

                executing[name] = false;
                Command.RaiseCanExecuteChanged();
            }
        }

        private Boolean GetIsExecuting(String name)
        {
            if (executing.ContainsKey(name))
            {
                return !executing[name];
            }
            else
            {
                var value = false;
                executing.Add(name, value);

                return !value;
            }
        }
    }
}
