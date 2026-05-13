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

        public ObservableCollection<Feedbacks> SessionFeedbacks
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
        private readonly IRoleInterface _roleService;
        private readonly ITopicInterface _topicService;
        private readonly ISessionInterface _sessionService;
        private readonly IConsultantService _consultantService;
        private readonly IRequestInterface _requestService;
        private readonly IMessageService _messageService;
        private readonly IFeedbackService _feedbackService;

        //Commands
        //Load
        public ICommand LoadUsersCommand { get; }
        public ICommand LoadSessionsCommand { get; }
        public ICommand LoadConsultantsCommand { get; }
        public ICommand LoadRequestCommand { get; }
        public ICommand LoadRolesCommand { get; }
        public ICommand LoadTopicsCommand { get; }
        public ICommand LoadMessagesCommand { get; }
        public ICommand LoadFeedbacksCommand { get; }

        //Delete
        public ICommand DeleteUserCommand { get; }
        public ICommand DeleteSessionCommand { get; }
        public ICommand DeleteConsultantCommand { get; }
        public ICommand DeleteRequestCommand { get; }
        public ICommand DeleteRoleCommand { get; }
        public ICommand DeleteTopicCommand { get; }
        public ICommand DeleteMessageCommand { get; }
        public ICommand DeleteFeedbackCommand { get; }

        //Edit
        public ICommand EditUserCommand { get; }
        public ICommand EditSessionCommand { get; }
        public ICommand EditConsultantCommand { get; }
        public ICommand EditRequestCommand { get; }
        public ICommand EditRoleCommand { get; }
        public ICommand EditTopicCommand { get; }
        //Add
        public ICommand AddUserCommand { get; }
        public ICommand AddSessionCommand { get; }
        public ICommand AddConsultantCommand { get; }
        public ICommand AddRequestCommand { get; }
        public ICommand AddRoleCommand { get; }
        public ICommand AddTopicCommand { get; }

        private MainViewModel()
        {
            _userService = new UserService();
            _sessionService = new SessionService();
            _consultantService = new ConsultantService();
            _requestService = new RequestService();
            _roleService = new RoleService();
            _topicService = new TopicService();
            _messageService = new MessageService();
            _feedbackService = new FeedbackService();

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

            LoadRolesCommand = new RelayCommand((object parameter) =>
            {
                Roles = new ObservableCollection<Roles>(_roleService.GetAllRoles());
            });

            LoadTopicsCommand = new RelayCommand((object parameter) =>
            {
                Topics = new ObservableCollection<Topics>(_topicService.GetAllTopics());
            });

            LoadMessagesCommand = new RelayCommand((object parameter) =>
            {
                SessionsMessages = new ObservableCollection<SessionMessages>(_messageService.GetAllMessages());
            });

            LoadFeedbacksCommand = new RelayCommand((object parameter) =>
            {
                SessionFeedbacks = new ObservableCollection<Feedbacks>(_feedbackService.GetAllFeedback());
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
                    RequestDialog editRequest = new RequestDialog(request);
                    editRequest.Show();
                }
            });

            EditSessionCommand = new RelayCommand((object parameter) =>
            {
                if (parameter is Sessions session)
                {
                    SessionDialog editSession = new SessionDialog(session);
                    editSession.Show();
                }
            });

            EditRoleCommand = new RelayCommand((object parameter) =>
            {
                if (parameter is Roles role)
                {
                    RoleDialog editRole = new RoleDialog(role);
                    editRole.Show();
                }
            });

            EditTopicCommand = new RelayCommand((object parameter) =>
            {
                if (parameter is Topics topic)
                {
                    TopicDialog editTopic = new TopicDialog(topic);
                    editTopic.Show();
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

            DeleteSessionCommand = new RelayCommand((object parameter) =>
            {
                if (parameter is Sessions session)
                {
                    var result = MessageBox.Show($"Удалить сессию {session.Name}?",
                        "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        _sessionService.DeleteSession(session.ID);
                        LoadSessionsCommand.Execute(null);
                    }
                }
            });

            DeleteRoleCommand = new RelayCommand((object parameter) =>
            {
                if (parameter is Roles role)
                {
                    var result = MessageBox.Show($"Удалить роль {role.Name}?",
                        "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        _roleService.DeleteRole(role.ID);
                        LoadRolesCommand.Execute(null);
                    }
                }
            });

            DeleteTopicCommand = new RelayCommand((object parameter) =>
            {
                if (parameter is Topics topic)
                {
                    var result = MessageBox.Show($"Удалить тему {topic.Name}?",
                        "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        _topicService.DeleteTopic(topic.ID);
                        LoadTopicsCommand.Execute(null);
                    }
                }
            });

            DeleteFeedbackCommand = new RelayCommand((object parameter) =>
            {
                if (parameter is Feedbacks feedback)
                {
                    var result = MessageBox.Show($"Удалить отзыв?",
                        "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        _feedbackService.DeleteFeedback(feedback.ID);
                        LoadFeedbacksCommand.Execute(null);
                    }
                }
            });

            DeleteMessageCommand = new RelayCommand((object parameter) =>
            {
                if (parameter is SessionMessages message)
                {
                    var result = MessageBox.Show($"Удалить сообщение?",
                        "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        _messageService.DeleteMessage(message.ID);
                        LoadMessagesCommand.Execute(null);
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

            AddSessionCommand = new RelayCommand((object parameter) =>
            {
                SessionDialog addSession = new SessionDialog();
                addSession.Show();
            });

            AddRoleCommand = new RelayCommand((object parameter) =>
            {
                RoleDialog addRole = new RoleDialog();
                addRole.Show();
            });

            AddTopicCommand = new RelayCommand((object parameter) =>
            {
                TopicDialog addTopic = new TopicDialog();
                addTopic.Show();
            });

            LoadUsersCommand.Execute(null);
            LoadConsultantsCommand.Execute(null);
            LoadSessionsCommand.Execute(null);
            LoadRequestCommand.Execute(null);
            LoadRolesCommand.Execute(null);
            LoadTopicsCommand.Execute(null);
            LoadMessagesCommand.Execute(null);
            LoadFeedbacksCommand.Execute(null);
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

        //Sessions

        public void AddSession(Sessions session, List<int> topics)
        {
            _sessionService.AddSession(session, topics);
            LoadSessionsCommand.Execute(null);
        }

        public void UpdateSession(Sessions session, List<int> topics)
        {
            _sessionService.UpdateSession(session, topics);
            LoadSessionsCommand.Execute(null);
        }

        //Roles

        public void AddRole(Roles role)
        {
            _roleService.AddRole(role);
            LoadRolesCommand.Execute(null);
        }

        public void UpdateRole(Roles role)
        {
            _roleService.UpdateRole(role);
            LoadRolesCommand.Execute(null);
        }

        //Topics

        public void AddTopic(Topics topic)
        {
            _topicService.AddTopic(topic);
            LoadTopicsCommand.Execute(null);
        }

        public void UpdateTopic(Topics topic)
        {
            _topicService.UpdateTopic(topic);
            LoadTopicsCommand.Execute(null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
