using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GoalTracker.Enums;
using GoalTracker.ViewModels;

namespace GoalTracker.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new MainWindowViewModel();
            DataContext = viewModel;
        }
    }
}
