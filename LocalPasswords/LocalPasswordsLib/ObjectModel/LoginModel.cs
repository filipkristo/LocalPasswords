using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPasswordsLib.ObjectModel
{
    public class LoginModel : NotifyBase
    {
        public String MasterPassword { get { return Get<String>(); } set { Set(value); } }    
    }
}
