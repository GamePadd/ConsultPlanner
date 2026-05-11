using ConsultPlanner.Commands;
using ConsultPlanner.Models;
using ConsultPlanner.Services;
using ConsultPlanner.Views;
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
        ObservableCollection<Consultants> _consultants;

        //Properties

        public ObservableCollection<Consultants> Consultants
        {
            get { return _consultants; }
            set
            {
                _consultants = value;
                OnPropertyChanged(nameof(Consultants));
            }
        }

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
        private readonly IConsultantService _consultantService;

        public IUserInterface UserInterface { get { return _userService; } }
        public ISessionInterface SessionsInterface { get { return _sessionService; } }

        //Commands
        public ICommand LoadUsersCommand { get; }
        public ICommand LoadSessionsCommand { get; }
        public ICommand LoadConsultantsCommand { get; }

        public ICommand DeleteUserCommand { get; }
        public ICommand DeleteConsultantCommand { get; }

        public ICommand EditUserCommand { get; }
        public ICommand EditConsultantCommand { get; }

        public ICommand AddUserCommand { get; }
        public ICommand AddConsultantCommand { get; }

        private MainViewModel()
        {
            _userService = new UserService();
            _sessionService = new SessionService();
            _consultantService = new ConsultantService();

            //Load Commands

            LoadUsersCommand = new RelayCommand((object parameter) => {
                Users = new ObservableCollection<Users>(_userService.GetAllUsers());
            });

            LoadSessionsCommand = new RelayCommand((object parameter) =>
            {
                Sessions = new ObservableCollection<Sessions>(_sessionService.GetAllSessions());
            });

            LoadConsultantsCommand = new RelayCommand((object parameter) =>
            {
                Consultants = new ObservableCollection<Consultants>(_consultantService.GetAllConsultantsWithNames());
            });

            //Edit Commands

            EditUserCommand = new RelayCommand((object parameter) =>
            {
                if (parameter is Users user)
                {
                    UserDialog editUser = new UserDialog(user);
                    editUser.Show();
                }
            });

            EditConsultantCommand = new RelayCommand((object parameter) =>
            {
                if (parameter is Consultants consultants)
                {
                    //UserDialog editUser = new UserDialog(user);
                    //editUser.Show();
                }
            });

            //Delete Commands

            DeleteUserCommand = new RelayCommand((object parameter) => {
                if (parameter is Users user)
                {
                    var result = MessageBox.Show($"Удалить пользователя {user.LastName} {user.FirstName}?",
                        "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        _userService.DeleteUser(user.ID);
                        LoadUsersCommand.Execute(null);
                    }
                }
            });

            DeleteConsultantCommand = new RelayCommand((object parameter) => {
                if (parameter is Consultants consultant)
                {
                    var result = MessageBox.Show($"Удалить консультанта {consultant.Users.LastName} {consultant.Users.FirstName}?",
                        "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        _consultantService.DeleteConsultant(consultant.ID);
                        LoadConsultantsCommand.Execute(null);
                    }
                }
            });

            //Add Commands

            AddUserCommand = new RelayCommand((object parameter) =>
            {
                UserDialog addUser = new UserDialog();
                addUser.Show();
            });

            AddConsultantCommand = new RelayCommand((object parameter) =>
            {
                ConsultantDialog addConsultant = new ConsultantDialog();
                addConsultant.Show();
            });

            LoadUsersCommand.Execute(null);
            LoadConsultantsCommand.Execute(null);
            LoadSessionsCommand.Execute(null);
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

        //Consultants

        //Users

        public void AddUser(Users user, List<int> roles)
        {
            _userService.AddUser(user, roles);
            LoadUsersCommand.Execute(null);
        }

        public void UpdateUser(Users user, List<int> updatedRoles)
        {
            _userService.UpdateUser(user, updatedRoles);
            LoadUsersCommand.Execute(null);
        }

        //Pages

        //Sessions

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
