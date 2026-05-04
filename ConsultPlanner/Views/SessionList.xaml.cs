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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConsultPlanner.Views
{
    /// <summary>
    /// Логика взаимодействия для SessionList.xaml
    /// </summary>
    public partial class SessionList : Page
    {
        public SessionList()
        {
            InitializeComponent();
            DataContext = ViewModels.MainViewModel.Instance;
        }
    }
}
