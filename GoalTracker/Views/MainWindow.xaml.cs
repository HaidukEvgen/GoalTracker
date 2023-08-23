using System.ComponentModel;
using System.Windows;
using GoalTracker.ViewModels;

namespace GoalTracker.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
        }

        private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
        {
            _viewModel.OnClosing();
        }
    }
}
