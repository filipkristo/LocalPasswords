using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPasswordsLib.ObjectModel
{
    public class RegisterModel
    {
        public String MasterPassword { get; set; }

        public String ConfirmPassword { get; set; }
    }
}
