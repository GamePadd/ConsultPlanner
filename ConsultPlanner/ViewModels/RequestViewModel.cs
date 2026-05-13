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
    public class RequestViewModel : INotifyPropertyChanged
    {
        //vars
        public Action Close { get; set; }
        private ConsultationRequests _request;

        private Consultants _selectedConsultant;
        private Users _selectedUser;

        private bool _isEditing;
        private string _dialogTitle;
        private int _editingRequestId;

        public ObservableCollection<Users> Users { get; set; }
        public ObservableCollection<Consultants> Consultants { get; set; }

        private readonly IUserInterface _userService;
        private readonly IConsultantService _consultantService;

        //Prop

        public Users SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public Consultants SelectedConsultant
        {
            get => _selectedConsultant;
            set
            {
                _selectedConsultant = value;
                OnPropertyChanged(nameof(SelectedConsultant));
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
            get => _request.Description;
            set
            {
                _request.Description = value; OnPropertyChanged(nameof(Description));
            }
        }

        public string Status
        {
            get => _request.Status;
            set
            {
                _request.Status = value; OnPropertyChanged(nameof(Status));
            }
        }

        public System.DateTime RequestDate
        {
            get => _request.RequestDate;
            set
            {
                _request.RequestDate = value; OnPropertyChanged(nameof(RequestDate));
            }
        }

        public ICommand SaveRequestCommand { get; }
        public ICommand CancelCommand { get; }

        public bool Validate()
        {
            List<string> errors = new List<string>();

            if (_selectedUser == null)
                errors.Add("Не выбран пользователь");
            if (_selectedConsultant == null)
                errors.Add("Не выбран консультант");
            if (string.IsNullOrEmpty(_request.Description))
                errors.Add("Нет описания");
            if (string.IsNullOrEmpty(_request.Status))
                errors.Add("Нет статуса");

            string errorMessage = errors.Count > 0 ? string.Join("\n", errors) : "";

            if (errors.Count != 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return errors.Count == 0;
        }

        public RequestViewModel(ConsultationRequests request)
        {
            _userService = new UserService();
            _consultantService = new ConsultantService();

            SaveRequestCommand = new RelayCommand((object parameter) =>
            {
                if (_isEditing)
                {
                    bool isCorrect = Validate();

                    if (isCorrect)
                    {
                        var updatedRequest = new ConsultationRequests
                        {
                            ID = _editingRequestId,
                            UserID = _selectedUser.ID,
                            ConsultantID = _selectedConsultant.ID,
                            Description = _request.Description,
                            Status = _request.Status,
                            RequestDate = _request.RequestDate
                        };

                        MainViewModel.Instance.UpdateRequest(updatedRequest);
                        Close?.Invoke();
                    }
                }
                else
                {
                    bool isCorrect = Validate();

                    if (isCorrect)
                    {
                        var newRequest = new ConsultationRequests
                        {
                            UserID = _selectedUser.ID,
                            ConsultantID = _selectedConsultant.ID,
                            Description = _request.Description,
                            Status = _request.Status,
                            RequestDate = _request.RequestDate
                        };

                        MainViewModel.Instance.AddRequest(newRequest);
                        Close?.Invoke();
                    }
                }
            });

            CancelCommand = new RelayCommand((object parameter) =>
            {
                Close?.Invoke();
            });

            if (request == null)
            {
                DialogTitle = "Добавить запрос";
                _request = new ConsultationRequests();
                _request.RequestDate = DateTime.Now;

                _isEditing = false;
                LoadUsers(null);
                LoadConsultants(null);
            }
            else
            {
                DialogTitle = "Редактировать запрос";
                _request = new ConsultationRequests
                {
                    ID = request.ID,
                    UserID = request.UserID,
                    ConsultantID = request.ConsultantID,
                    Description = request.Description,
                    Status = request.Status,
                    RequestDate = request.RequestDate
                };

                _editingRequestId = _request.ID;

                _isEditing = true;
                LoadUsers(_request);
                LoadConsultants(_request);
            }
        }

        public void LoadUsers(ConsultationRequests request = null)
        {
            var allUsers = _userService.GetAllUsers();
            Users = new ObservableCollection<Users>(allUsers);

            if (request != null)
            {
                SelectedUser = Users.FirstOrDefault(u => u.ID == request.UserID);
            }
        }

        public void LoadConsultants(ConsultationRequests request = null)
        {
            var allConsultants = _consultantService.GetAllConsultantsWithNames();
            Consultants = new ObservableCollection<Consultants>(allConsultants);

            if (request != null)
            {
                SelectedConsultant = Consultants.FirstOrDefault(u => u.ID == request.ConsultantID);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
