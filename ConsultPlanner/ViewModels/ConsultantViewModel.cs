using ConsultPlanner.Models;
using ConsultPlanner.Services;
using System;
using ConsultPlanner.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static ConsultPlanner.ViewModels.UserViewModel;
using System.Runtime.InteropServices;
using System.Windows;

namespace ConsultPlanner.ViewModels
{
    public class ConsultantViewModel : INotifyPropertyChanged
    {
        public Action Close { get; set; }
        private Consultants _consultant;
        private Users _selectedUser;

        private bool _isEditing;
        private string _dialogTitle;
        private int _editingConsultantId;

        public class TopicItem
        {
            public int ID { set; get; }
            public string Name { set; get; }
            public bool IsSelected { set; get; }
        }

        public ObservableCollection<Users> Users { get; set; }
        public ObservableCollection<TopicItem> Topics { get; set; }

        private readonly ITopicInterface _topicService;
        private readonly IUserInterface _userService;

        public Users SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
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

        public string Description
        {
            get => _consultant.Description;
            set
            {
                _consultant.Description = value; OnPropertyChanged(nameof(Description));
            }
        }

        public int Exp
        {
            get => _consultant.Experience;
            set
            {
                _consultant.Experience = value; OnPropertyChanged(nameof(Exp));
            }
        }

        public ICommand SaveConsultantCommand { get; }
        public ICommand CancelCommand { get; }

        public ConsultantViewModel(Consultants consultant) {
            _userService = new UserService();
            _topicService = new TopicService();

            SaveConsultantCommand = new RelayCommand((object parameter) =>
            {
                if (_isEditing)
                {
                    bool isCorrect = Validate();
                    if (isCorrect)
                    {
                        var updatedConsultant = new Consultants
                        {
                            ID = _editingConsultantId,
                            UserID = _selectedUser.ID,
                            Description = this.Description,
                            Experience = this.Exp
                        };

                        var selectedTopics = Topics.Where(s => s.IsSelected).Select(s => s.ID).ToList();
                        MainViewModel.Instance.UpdateConsultant(updatedConsultant, selectedTopics);

                        Close?.Invoke();
                    }
                }
                else
                {
                    bool isCorrect = Validate();

                    if (isCorrect)
                    {
                        var newConsultant = new Consultants
                        {
                            UserID = _selectedUser.ID,
                            Description = this.Description,
                            Experience = this.Exp
                        };

                        var selectedTopics = Topics.Where(s => s.IsSelected).Select(s => s.ID).ToList();

                        MainViewModel.Instance.AddConsultant(newConsultant, selectedTopics);
                        Close?.Invoke();
                    }
                }
            });

            CancelCommand = new RelayCommand((object parameter) =>
            {
                Close?.Invoke();
            });

            if (consultant == null)
            {
                DialogTitle = "Добавить консультанта";
                _consultant = new Consultants();
                _consultant.Experience = 0;
                _isEditing = false;
                LoadTopics(null);
                LoadUsers(null);
            }
            else
            {
                DialogTitle = "Редактировать консультанта";
                _isEditing = true;
                _editingConsultantId = consultant.ID;

                _consultant = new Consultants
                {
                    ID = consultant.ID,
                    UserID = consultant.UserID,
                    Description = consultant.Description,
                    Experience = consultant.Experience
                };

                var selectedTopics = _topicService.GetAllConsultTopics(consultant.ID);
                LoadTopics(selectedTopics);
                LoadUsers(consultant);
            }
        }

        public void LoadUsers(Consultants consultant = null)
        {
            var allUsers = _userService.GetAllUsers();
            Users = new ObservableCollection<Users>(allUsers);

            if (consultant != null)
            {
                SelectedUser = Users.FirstOrDefault(u => u.ID == consultant.UserID);
            }
        }
        public void LoadTopics(List<int> consultTopicId = null)
        {
            var allTopics = _topicService.GetAllTopics();
            Topics = new ObservableCollection<TopicItem>();

            foreach(var topic in allTopics)
            {
                var isSelected = consultTopicId != null && consultTopicId.Contains(topic.ID);
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

            if (_selectedUser == null)
                errors.Add("Не выбран пользователь");
            if (Exp <= 0 )
                errors.Add("Опыт работы должен быть больше нуля");

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
