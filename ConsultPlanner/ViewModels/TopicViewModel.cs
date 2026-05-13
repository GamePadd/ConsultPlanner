using ConsultPlanner.Commands;
using ConsultPlanner.Models;
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
    public class TopicViewModel : INotifyPropertyChanged
    {
        //Vars
        public Action Close { get; set; }

        Topics _topic;

        private bool _isEditing;
        private string _dialogTitle;
        private int _editingTopicId;

        public ICommand SaveTopicCommand { get; }
        public ICommand CancelCommand { get; }

        //Props


        public string Description
        {
            get => _topic.Description;
            set
            {
                _topic.Description = value; OnPropertyChanged(nameof(Description));
            }
        }
        public string Name
        {
            get => _topic.Name;
            set
            {
                _topic.Name = value; OnPropertyChanged(nameof(Name));
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

        public bool Validate()
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(_topic.Name))
                errors.Add("Пустое имя");
            if (string.IsNullOrEmpty(_topic.Description))
                errors.Add("Пустое описание");

            string errorMessage = errors.Count > 0 ? string.Join("\n", errors) : "";

            if (errors.Count != 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return errors.Count == 0;
        }

        public TopicViewModel(Topics topic)
        {
            SaveTopicCommand = new RelayCommand((object parameter) =>
            {
                bool isCorrect = Validate();

                if (isCorrect)
                {
                    if (_isEditing)
                    {
                        var updatedTopic = new Topics
                        {
                            ID = _editingTopicId,
                            Name = _topic.Name,
                            Description = _topic.Description
                        };

                        MainViewModel.Instance.UpdateTopic(updatedTopic);
                        Close?.Invoke();
                    }
                    else
                    {
                        var newTopic = new Topics
                        {
                            Name = _topic.Name,
                            Description = _topic.Description
                        };

                        MainViewModel.Instance.AddTopic(newTopic);
                        Close?.Invoke();
                    }
                }
            });

            CancelCommand = new RelayCommand((object parameter) =>
            {
                Close?.Invoke();
            });

            if (topic == null)
            {
                _dialogTitle = "Добавить тему";
                _isEditing = false;

                _topic = new Topics();
            }
            else
            {
                _dialogTitle = "Редактировать тему";
                _isEditing = true;
                _editingTopicId = topic.ID;

                _topic = new Topics
                {
                    ID = topic.ID,
                    Name = topic.Name,
                    Description = topic.Description
                };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
