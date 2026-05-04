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
        public ICommand LoadSessionsCommand { get; }
        private MainViewModel()
        {
            _userService = new UserService();
            _sessionService = new SessionService();
            LoadUsersCommand = new RelayCommand(LoadUsers);
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

        public void LoadUsers(object parameter)
        {
            Users = new ObservableCollection<Users>(_userService.GetAllUsers());
        }
        public void LoadSessions(object parameter)
        {
            Sessions = new ObservableCollection<Sessions>(_sessionService.GetAllSessions());
        }

        public void AddUser(Users user)
        {
            _userService.AddUser(user);
            LoadUsers(null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
