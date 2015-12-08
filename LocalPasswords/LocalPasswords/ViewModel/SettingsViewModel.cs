using LocalPasswordsLib.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;

namespace LocalPasswords.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private SettingsBLL BLL;

        public List<String> Languages { get; set; }

        public String Language
        {
            get
            {
                return BLL.GetLanguage();
            }
            set
            {                
                BLL.SaveLanguage(value);
            }
        }

        public SettingsViewModel()
        {
            BLL = new SettingsBLL(resourceContextForCurrentView);
            Languages = ApplicationLanguages.Languages.ToList();            
        }


    }
}
