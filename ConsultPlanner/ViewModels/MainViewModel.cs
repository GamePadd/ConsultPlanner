using ConsultPlanner.Commands;
using ConsultPlanner.Models;
using ConsultPlanner.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConsultPlanner.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //Private Vars
        List<Users> _users;
        List<Sessions> _sessions;
        //Properties
        public List<Users> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public List<Sessions> Sessions
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

        //Commands
        public ICommand LoadUsersCommand { get; }
        public ICommand LoadSessionsCommand { get; }
        public MainViewModel()
        {
            _userService = new UserService();
            _sessionService = new SessionService();
            LoadUsersCommand = new RelayCommand(LoadUsers);
            LoadSessionsCommand = new RelayCommand(LoadSessions);

            LoadUsers(null);
            LoadSessions(null);
        }
        public void LoadUsers(object parameter)
        {
            _users = _userService.GetAllUsers();
        }
        public void LoadSessions(object parameter)
        {
            _sessions = _sessionService.GetAllSessions();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
