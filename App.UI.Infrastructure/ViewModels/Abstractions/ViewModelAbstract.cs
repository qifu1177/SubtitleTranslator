using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App.UI.Infrastructure.ViewModels.Abstractions
{
    public abstract class ViewModelAbstract : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public ViewModelAbstract()
        {
            
        }
        
        protected void OnPropertyChanged(string propertyName)
        {
            if (propertyName == null)
            {
                return;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            property = value;
            OnPropertyChanged(propertyName);
        }
        protected void SetProperty<T>(Action<T> update, T value, [CallerMemberName] string propertyName = null)
        {
            if (update != null)
            {
                update(value);
            }
            OnPropertyChanged(propertyName);
        }
        
          
    }
}
