using LocalPasswords.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LocalPasswords.Layout
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        // strongly-typed view models enable x:bind
        public LoginViewModel ViewModel => this.DataContext as LoginViewModel;

        public LoginPage()
        {
            this.InitializeComponent();

            DataContext = new LoginViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await ViewModel.OnNavigatedTo(this, e);
        }

        protected override async void OnNavigatedFrom(NavigationEventArgs e)
        {            
            base.OnNavigatedFrom(e);
            await ViewModel.OnNavigatedFrom(this, e);
        }
    }
}
