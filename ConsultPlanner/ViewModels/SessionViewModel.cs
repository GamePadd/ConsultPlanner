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
    public class SessionViewModel : INotifyPropertyChanged
    {
        public Action Close { get; set; }

        private Sessions _session;
        private ConsultationRequests _selectedRequest;

        private bool _isEditing;
        private string _dialogTitle;
        private int _editingSessionId;

        public class TopicItem
        {
            public int ID { set; get; }
            public string Name { set; get; }
            public bool IsSelected { set; get; }
        }
        public ObservableCollection<TopicItem> Topics { get; set; }
        public ObservableCollection<ConsultationRequests> Requests { get; set; }

        private readonly ITopicInterface _topicService;
        private readonly IRequestInterface _requestService;

        //props

        public System.DateTime EndTime
        {
            get => _session.EndTime;
            set
            {
                _session.EndTime = value; OnPropertyChanged(nameof(EndTime));
            }
        }

        public System.DateTime StartTime
        {
            get => _session.StartTime;
            set
            {
                _session.StartTime = value; OnPropertyChanged(nameof(StartTime));
            }
        }

        public string Status
        {
            get => _session.Status;
            set
            {
                _session.Status = value; OnPropertyChanged(nameof(Status));
            }
        }

        public string Description
        {
            get => _session.Description;
            set
            {
                _session.Description = value; OnPropertyChanged(nameof(Description));
            }
        }

        public string Name
        {
            get => _session.Name;
            set
            {
                _session.Name = value; OnPropertyChanged(nameof(Name));
            }
        }

        public ConsultationRequests SelectedRequest
        {
            get => _selectedRequest;
            set
            {
                _selectedRequest = value; OnPropertyChanged(nameof(SelectedRequest));
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

        public ICommand SaveSessionCommand { get; }
        public ICommand CancelCommand { get; }
        public SessionViewModel(Sessions session)
        {
            _topicService = new TopicService();
            _requestService = new RequestService();

            SaveSessionCommand = new RelayCommand((object parameter) =>
            {
                bool isCorrect = Validate();

                if (isCorrect)
                {
                    if (_isEditing)
                    {
                        var updatedSession = new Sessions
                        {
                            ID = _editingSessionId,
                            Name = _session.Name,
                            Description = _session.Description,
                            RequestID = _selectedRequest.ID,
                            Status = _session.Status,
                            StartTime = _session.StartTime,
                            EndTime = _session.EndTime
                        };

                        var selectedTopics = Topics.Where(s => s.IsSelected).Select(s => s.ID).ToList();

                        MainViewModel.Instance.UpdateSession(updatedSession, selectedTopics);
                        Close?.Invoke();
                    }
                    else
                    {
                        var newSession = new Sessions
                        {
                            Name = _session.Name,
                            Description = _session.Description,
                            RequestID = _selectedRequest.ID,
                            Status = _session.Status,
                            StartTime = _session.StartTime,
                            EndTime = _session.EndTime
                        };

                        var selectedTopics = Topics.Where(s => s.IsSelected).Select(s => s.ID).ToList();

                        MainViewModel.Instance.AddSession(newSession, selectedTopics);
                        Close?.Invoke();
                    }
                }
            });

            CancelCommand = new RelayCommand((object parameter) =>
            {
                Close?.Invoke();
            });

            if (session == null)
            {
                DialogTitle = "Добавить сессию";
                _session = new Sessions();
                _isEditing = false;
                _session.StartTime = DateTime.Now;
                _session.EndTime = DateTime.Now;
                LoadRequests(null);
                LoadTopics(null);
            }
            else
            {
                DialogTitle = "Редактировать сессию";
                _isEditing = true;
                _editingSessionId = session.ID;
                _session = new Sessions
                {
                    ID = session.ID,
                    Name = session.Name,
                    Description = session.Description,
                    RequestID = session.RequestID,
                    Status = session.Status,
                    StartTime = session.StartTime,
                    EndTime = session.EndTime
                };

                var selectedTopics = _topicService.GetAllSessionTopics(session.ID);

                LoadRequests(_session);
                LoadTopics(selectedTopics);
            }
        }

        void LoadRequests(Sessions session = null)
        {
            var allRequests = _requestService.GetAllRequestsWithNames();
            Requests = new ObservableCollection<ConsultationRequests>(allRequests);

            if (session != null)
            {
                SelectedRequest = Requests.FirstOrDefault(u => u.ID == session.RequestID);
            }
        }
        void LoadTopics(List<int> selectedTopics = null)
        {
            var allTopics = _topicService.GetAllTopics();
            Topics = new ObservableCollection<TopicItem>();

            foreach (var topic in allTopics)
            {
                var isSelected = selectedTopics != null && selectedTopics.Contains(topic.ID);
                Topics.Add(new TopicItem
                {
                    ID = topic.ID,
                    Name = topic.Name,
                    IsSelected = isSelected
                });
            }
        }

        public bool Validate()
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(Name))
                errors.Add("Не заполнено имя");
            if (string.IsNullOrEmpty(Description))
                errors.Add("Не заполнено описание");
            if (_selectedRequest == null)
                errors.Add("Не выбран запрос");
            if (string.IsNullOrEmpty(Status))
                errors.Add("Не заполнен статус");

            string errorMessage = errors.Count > 0 ? string.Join("\n", errors) : "";

            if (errors.Count != 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return errors.Count == 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
