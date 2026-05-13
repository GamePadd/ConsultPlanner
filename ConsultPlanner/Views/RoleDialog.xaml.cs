using ConsultPlanner.Models;
using ConsultPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ConsultPlanner.Views
{
    /// <summary>
    /// Логика взаимодействия для RoleDialog.xaml
    /// </summary>
    public partial class RoleDialog : Window
    {
        public RoleDialog(Roles role = null)
        {
            InitializeComponent();
            RoleViewModel viewModel = new RoleViewModel(role);
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
