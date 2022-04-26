﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PLANSA.Services
{
    internal class Observer : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyname = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyname);
            return true;
        }
    }
}