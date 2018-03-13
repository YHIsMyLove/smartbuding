using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConstructionServices.Common
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelBase()
        {
        }

        #region Properties

        public bool IsBusy
        {
            get { return isBusy; }
        }

        public bool HasError {
            get { return hasError; }
        }

        public Error Error {
            get { return error; }
        }

        #endregion

        #region Methods

        protected void SetProperty<T>(ref T property, T value, string propertyName)
        {
            property = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        protected bool isBusy;
        protected bool hasError;
        protected Error error;
    }
}
