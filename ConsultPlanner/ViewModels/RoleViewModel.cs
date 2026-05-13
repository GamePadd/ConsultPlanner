using ConsultPlanner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ConsultPlanner.Services;
using ConsultPlanner.Commands;

namespace ConsultPlanner.ViewModels
{
    public class RoleViewModel : INotifyPropertyChanged
    {
        //Vars
        public Action Close {  get; set; }

        Roles _role;

        private bool _isEditing;
        private string _dialogTitle;
        private int _editingRoleId;

        public ICommand SaveRoleCommand { get; }
        public ICommand CancelCommand { get; }

        //Props

        public string Name
        {
            get => _role.Name;
            set
            {
                _role.Name = value; OnPropertyChanged(nameof(Name));
            }
        }
        public string DialogTitle
        {
            get => _dialogTitle;
            set
            {
                _dialogTitle = value; OnPropertyChanged(nameof(DialogTitle));
            }
        }

        public bool Validate()
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(_role.Name))
                errors.Add("Пустое имя");

            string errorMessage = errors.Count > 0 ? string.Join("\n", errors) : "";

            if (errors.Count != 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return errors.Count == 0;
        }

        public RoleViewModel(Roles role)
        {
            SaveRoleCommand = new RelayCommand((object parameter) =>
            {
                bool isCorrect = Validate();

                if (isCorrect)
                {
                    if (_isEditing)
                    {
                        var updatedRole = new Roles
                        {
                            ID = _editingRoleId,
                            Name = _role.Name
                        };

                        MainViewModel.Instance.UpdateRole(updatedRole);
                        Close?.Invoke();
                    }
                    else
                    {
                        var newRole = new Roles
                        {
                            Name = _role.Name
                        };

                        MainViewModel.Instance.AddRole(newRole);
                        Close?.Invoke();
                    }
                }
            });

            CancelCommand = new RelayCommand((object parameter) =>
            {
                Close?.Invoke();
            });

            if (role == null) {
                _dialogTitle = "Добавить роль";
                _isEditing = false;

                _role = new Roles();
            }
            else
            {
                _dialogTitle = "Редактировать роль";
                _isEditing = true;
                _editingRoleId = role.ID;

                _role = new Roles
                {
                    ID = role.ID,
                    Name = role.Name
                };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
