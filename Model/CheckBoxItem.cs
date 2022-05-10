using PLANSA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANSA.Model
{
    internal class CheckBoxItem : Observer
    {
        private bool myVar;

        public bool MyProperty
        {
            get { return myVar; }
            set { myVar = value; OnPropertyChanged(); }
        }

    }
}
