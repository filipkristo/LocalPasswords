using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LocalPasswordsLib.ObjectModel
{
    public class NotifyBase : INotifyPropertyChanged
    {
        private Dictionary<Object, Object> propertyValues = new Dictionary<Object, Object>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set(Object value, [CallerMemberName] string name = "")
        {
            if (propertyValues.ContainsKey(name))
            {
                var oldValue = propertyValues[name];
                if (oldValue == null || !oldValue.Equals(value))
                {
                    propertyValues[name] = value;

                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs(name));

                }
            }
            else
            {
                propertyValues.Add(name, value);

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        protected T Get<T>([CallerMemberName] string name = "")
        {
            if (propertyValues.ContainsKey(name))
            {
                return (T)propertyValues[name];
            }
            else
            {
                return default(T);
            }
        }
    }
}
