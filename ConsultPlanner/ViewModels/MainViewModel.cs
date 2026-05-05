using ConsultPlanner.Commands;
using ConsultPlanner.Models;
using ConsultPlanner.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ConsultPlanner.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private static MainViewModel _instance;
        //Private Vars
        ObservableCollection<Users> _users;
        ObservableCollection<Sessions> _sessions;
        //Properties
        public ObservableCollection<Users> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public ObservableCollection<Sessions> Sessions
        {
            get { return _sessions; }
            set
            {
                _sessions = value;
                OnPropertyChanged(nameof(Sessions));
            }
        }

        //Services
        private readonly IUserInterface _userService;
        private readonly ISessionInterface _sessionService;

        public IUserInterface UserInterface { get { return _userService; } }
        public ISessionInterface SessionsInterface { get { return _sessionService; } }

        //Commands
        public ICommand LoadUsersCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand LoadSessionsCommand { get; }
        private MainViewModel()
        {
            _userService = new UserService();
            _sessionService = new SessionService();

            LoadUsersCommand = new RelayCommand(LoadUsers);
            DeleteUserCommand = new RelayCommand(DeleteUser);

            LoadSessionsCommand = new RelayCommand(LoadSessions);

            LoadUsers(null);
            LoadSessions(null);
        }

        public static MainViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MainViewModel();
                }

                return _instance;
            }
        }

        //Users
        public void LoadUsers(object parameter)
        {
            Users = new ObservableCollection<Users>(_userService.GetAllUsers());
        }

        public void AddUser(Users user, List<int> roles)
        {
            _userService.AddUser(user, roles);
            LoadUsers(null);
        }
        public void DeleteUser(object parameter)
        {
            if (parameter is Users user)
            {
                var result = MessageBox.Show($"Удалить пользователя {user.LastName} {user.FirstName}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _userService.DeleteUser(user.ID);
                    LoadUsers(null);
                }
            }
        }

        //Sessions
        public void LoadSessions(object parameter)
        {
            Sessions = new ObservableCollection<Sessions>(_sessionService.GetAllSessions());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
