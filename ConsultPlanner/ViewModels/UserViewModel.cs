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
using System.Collections.ObjectModel;
using ConsultPlanner.Services;

namespace ConsultPlanner.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        //Vars
        public Action Close { get; set; }
        private Users _user;
        private bool _isEditing;
        private string _dialogTitle;
        private int _editingUserId;
        public class RoleItem
        {
            public int ID { set; get; }
            public string Name { set; get; }
            public bool IsSelected { set; get; }
        }

        public ObservableCollection<RoleItem> Roles { get; set; }

        //Services

        private readonly IRoleInterface _roleService;

        //Properties
        public string DialogTitle
        {
            get => _dialogTitle; 
            set
            {
                _dialogTitle = value; OnPropertyChanged(nameof(DialogTitle));
            }
        }
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

        public ICommand SaveUserCommand { get; }
        public ICommand CancelCommand { get; }

        public UserViewModel(Users user)
        {
            SaveUserCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);

            _roleService = new RoleService();

            if (user == null)
            {
                _isEditing = false;
                _user = new Users();
                _user.Birthday = DateTime.Now;
                DialogTitle = "Добавить пользователя";
                LoadRoles(null);
            }
            else
            {
                DialogTitle = "Редактировать пользователя";
                _editingUserId = user.ID;
                _isEditing = true;

                _user = new Users
                {
                    ID = user.ID,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    Patronymic = user.Patronymic,
                    Birthday = user.Birthday,
                    Phone = user.Phone,
                    Email = user.Email,
                    RegisterDate = user.RegisterDate,
                    Password = user.Password
                };

                var selectedRoles = _roleService.GetAllRolesID(user.ID);

                LoadRoles(selectedRoles);
            }
        }

        public void LoadRoles(List<int> userRolesId = null)
        {
            var AllRoles = _roleService.GetAllRoles();
            Roles = new ObservableCollection<RoleItem>();

            foreach(var role in AllRoles)
            {
                var isSelected = userRolesId != null && userRolesId.Contains(role.ID);
                Roles.Add(new RoleItem
                {
                    ID = role.ID,
                    Name = role.Name,
                    IsSelected = isSelected
                });
            }
        }

        public void Save(object parameter)
        {
            if (_isEditing)
            {
                var updatedUser = new Users
                {
                    ID = _editingUserId,
                    LastName = this.LastName,
                    FirstName = this.FirstName,
                    Patronymic = this.Patronymic,
                    Birthday = this.Birthday,
                    Phone = this.Phone,
                    Email = this.Email,
                    Password = this.Password,
                    RegisterDate = _user.RegisterDate
                };

                var selectedRoleIds = Roles.Where(r => r.IsSelected).Select(r => r.ID).ToList();

                MainViewModel.Instance.UpdateUser(updatedUser, selectedRoleIds);
                Close?.Invoke();
            }
            else
            {
                Users newUser = new Users
                {
                    LastName = this.LastName,
                    FirstName = this.FirstName,
                    Patronymic = this.Patronymic,
                    Birthday = this.Birthday,
                    Phone = this.Phone,
                    Email = this.Email,
                    Password = this.Password,
                    RegisterDate = DateTime.Now
                };

                var selectedRoles = Roles.Where(r => r.IsSelected).Select(r => r.ID).ToList();

                MainViewModel.Instance.AddUser(newUser, selectedRoles);
                Close?.Invoke();
            }
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
