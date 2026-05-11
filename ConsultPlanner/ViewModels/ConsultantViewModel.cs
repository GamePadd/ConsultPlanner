using ConsultPlanner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultPlanner.ViewModels
{
    public class ConsultantViewModel : INotifyPropertyChanged
    {
        public Action Close { get; set; }
        private Consultants _consultant;

        private bool _isEditing;
        private string _dialogTitle;
        private int _editingConsultantId;

        public string DialogTitle
        {
            get => _dialogTitle;
            set
            {
                _dialogTitle = value; OnPropertyChanged(nameof(DialogTitle));
            }
        }

        public ConsultantViewModel(Consultants consultant) {
            if (consultant == null)
            {
                DialogTitle = "Добавить консультанта";
            }
            else
            {
                DialogTitle = "Редактировать консультанта";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
