using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ConsultPlanner.Models;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ConsultPlanner.ViewModels;

namespace ConsultPlanner.Views
{
    /// <summary>
    /// Логика взаимодействия для SessionDialog.xaml
    /// </summary>
    public partial class SessionDialog : Window
    {
        public SessionDialog(Sessions session = null)
        {
            InitializeComponent();
            SessionViewModel viewModel = new SessionViewModel(session);
            viewModel.Close = () => { this.Close(); };
            DataContext = viewModel;
        }

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
