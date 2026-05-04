using ConsultPlanner.Models;
using ConsultPlanner.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace ConsultPlanner.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        //Vars
        public Action Close {  get; set; }
        private Users _user;

        //Properties
        public string LastName
        {
            get => _user.LastName;
            set
            {
                _user.LastName = value; OnPropertyChanged(nameof(LastName));
            }
        }

        public string FirstName
        {
            get => _user.FirstName;
            set
            {
                _user.FirstName = value; OnPropertyChanged(nameof(FirstName));
            }
        }

        public string Patronymic
        {
            get => _user.Patronymic;
            set
            {
                _user.Patronymic = value; OnPropertyChanged(nameof(Patronymic));
            }
        }

        public System.DateTime Birthday
        {
            get => _user.Birthday;
            set
            {
                _user.Birthday = value; OnPropertyChanged(nameof(Birthday));
            }
        }

        public string Phone
        {
            get => _user.Phone;
            set
            {
                _user.Phone = value; OnPropertyChanged(nameof(Phone));
            }
        }

        public string Email
        {
            get => _user.Email;
            set
            {
                _user.Email = value; OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get => _user.Password;
            set
            {
                _user.Password = value; OnPropertyChanged(nameof(Password));
            }
        }
        //Commands

        public ICommand AddUserCommand { get; }
        public ICommand CancelCommand { get; }

        public UserViewModel()
        {
            _user = new Users();
            _user.Birthday = DateTime.Now;
            AddUserCommand = new RelayCommand(AddUser);
            CancelCommand = new RelayCommand(Cancel);
        }

        public void AddUser(object parameter)
        {
            Users newUser = new Users
            {
                LastName = _user.LastName,
                FirstName = _user.FirstName,
                Patronymic = _user.Patronymic,
                Birthday = _user.Birthday,
                Phone = _user.Phone,
                Email = _user.Email,
                Password = _user.Password,
                RegisterDate = DateTime.Now
            };

            MainViewModel.Instance.AddUser(newUser);
            Close?.Invoke();
        }

        public void Cancel(object parameter)
        {
            Close?.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
