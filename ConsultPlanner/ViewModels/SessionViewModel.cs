using ConsultPlanner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultPlanner.ViewModels
{
    public class SessionViewModel : INotifyPropertyChanged
    {
        public Action Close { get; set; }

        public SessionViewModel(Sessions session)
        {
            if (session == null)
            {

            }
            else
            {

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
