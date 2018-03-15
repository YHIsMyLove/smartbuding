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

        public bool IsBusy {
            get { return isBusy; }
            set {
                if (isBusy == value) return;
                isBusy = value;
                NotifyPropertyChanged(nameof(IsBusy));
            }
        }

        public bool HasError {
            get { return hasError; }
            set {
                if (hasError == value) return;
                hasError = value;
                NotifyPropertyChanged(nameof(HasError));
            }
        }

        public Error Error {
            get { return error; }
            set {
                if (error == value) return;
                error = value;
                NotifyPropertyChanged(nameof(Error));
            }
        }

        #endregion

        #region Methods

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private bool isBusy;
        private bool hasError;
        private Error error;
    }
}
