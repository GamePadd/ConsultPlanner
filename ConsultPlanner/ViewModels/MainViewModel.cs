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
        private ObservableCollection<Users> _users;
        private ObservableCollection<Sessions> _sessions;
        private ObservableCollection<Consultants> _consultants;
        private ObservableCollection<ConsultationRequests> _consultationRequest;
        private ObservableCollection<SessionMessages> _sessionMessages;
        private ObservableCollection<Feedbacks> _feedbacks;
        private ObservableCollection<Topics> _topics;
        private ObservableCollection<Roles> _roles;

        //Properties

        public ObservableCollection<Roles> Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }

        public ObservableCollection<Topics> Topics
        {
            get { return _topics; }
            set
            {
                _topics = value;
                OnPropertyChanged(nameof(Topics));
            }
        }

        public ObservableCollection<Feedbacks> Feedbacks
        {
            get { return _feedbacks; }
            set
            {
                _feedbacks = value;
                OnPropertyChanged(nameof(Feedbacks));
            }
        }

        public ObservableCollection<SessionMessages> SessionsMessages
        {
            get { return _sessionMessages; }
            set
            {
                _sessionMessages = value;
                OnPropertyChanged(nameof(SessionsMessages));
            }
        }

        public ObservableCollection<ConsultationRequests> ConsultationRequests
        {
            get { return _consultationRequest; }
            set
            {
                _consultationRequest = value;
                OnPropertyChanged(nameof(ConsultationRequests));
            }
        }

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
        private readonly IRequestInterface _requestService;

        //Commands
        //Load
        public ICommand LoadUsersCommand { get; }
        public ICommand LoadSessionsCommand { get; }
        public ICommand LoadConsultantsCommand { get; }
        public ICommand LoadRequestCommand { get; }

        //Delete
        public ICommand DeleteUserCommand { get; }
        public ICommand DeleteConsultantCommand { get; }
        public ICommand DeleteRequestCommand { get; }

        //Edit
        public ICommand EditUserCommand { get; }
        public ICommand EditConsultantCommand { get; }
        public ICommand EditRequestCommand { get; }
        //Add
        public ICommand AddUserCommand { get; }
        public ICommand AddConsultantCommand { get; }
        public ICommand AddRequestCommand { get; }

        private MainViewModel()
        {
            _userService = new UserService();
            _sessionService = new SessionService();
            _consultantService = new ConsultantService();
            _requestService = new RequestService();

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

            LoadRequestCommand = new RelayCommand((object parameter) =>
            {
                ConsultationRequests = new ObservableCollection<ConsultationRequests>(_requestService.GetAllRequestsWithNames());
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
                    ConsultantDialog editConsultant = new ConsultantDialog(consultants);
                    editConsultant.Show();
                }
            });

            EditRequestCommand = new RelayCommand((object parameter) => {
                if (parameter is ConsultationRequests request)
                {
                    RequestDialog requestDialog = new RequestDialog(request);
                    requestDialog.Show();
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

            DeleteRequestCommand = new RelayCommand((object parameter) => {
                if (parameter is ConsultationRequests request)
                {
                    var result = MessageBox.Show($"Удалить запрос на консультацию от {request.Users.LastName} {request.Users.FirstName}?",
                        "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        _requestService.DeleteRequest(request.ID);
                        LoadRequestCommand.Execute(null);
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

            AddRequestCommand = new RelayCommand((object parameter) => {
                RequestDialog addRequest = new RequestDialog();
                addRequest.Show();
            });

            LoadUsersCommand.Execute(null);
            LoadConsultantsCommand.Execute(null);
            LoadSessionsCommand.Execute(null);
            LoadRequestCommand.Execute(null);
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

        public void AddConsultant(Consultants consultant, List<int> topics)
        {
            _consultantService.AddConsultant(consultant, topics);
            LoadConsultantsCommand.Execute(null);
        }

        public void UpdateConsultant(Consultants consultant, List<int> updatedTopics)
        {
            _consultantService.UpdateConsultant(consultant, updatedTopics);
            LoadConsultantsCommand.Execute(null);
        }

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

        //Requests

        public void AddRequest(ConsultationRequests request)
        {
            _requestService.AddRequest(request);
            LoadRequestCommand.Execute(null);
        }

        public void UpdateRequest(ConsultationRequests request)
        {
            _requestService.UpdateRequest(request);
            LoadRequestCommand.Execute(null);
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
