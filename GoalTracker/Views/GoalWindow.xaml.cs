using System.Windows;
using GoalTracker.Enums;
using GoalTracker.ViewModels;

namespace GoalTracker.Views 
{
    public partial class GoalWindow : Window
    {
        private WindowUsage windowUsage;
        private readonly GoalWindowViewModel viewModel;
        public GoalWindow(WindowUsage windowUsage, MainWindowViewModel mainViewModel)
        {
            this.windowUsage = windowUsage;
            InitializeComponent();
            Title = windowUsage == WindowUsage.AddingGoal ? "Adding Goal" : "Editing Goal";
            viewModel = new GoalWindowViewModel(this, this.windowUsage)
            {
                MainViewModel = mainViewModel
            };
            DataContext = viewModel;
        }
    }
}
