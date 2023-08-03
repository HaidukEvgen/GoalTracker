using System.Windows;
using GoalTracker.Converters;
using GoalTracker.Enums;
using GoalTracker.Models;
using GoalTracker.ViewModels;

namespace GoalTracker.Views 
{
    public partial class GoalWindow : Window
    {
        private WindowUsage windowUsage;
        private readonly GoalWindowViewModel viewModel;
        public GoalWindow(WindowUsage windowUsage, MainWindowViewModel mainViewModel, UIGoal? goal)
        {
            this.windowUsage = windowUsage;
            InitializeComponent();
            Title = windowUsage == WindowUsage.AddingGoal ? "Adding Goal" : "Editing Goal";
            FormCaptionTextBlock.Text = windowUsage == WindowUsage.AddingGoal ? "Add Goal" : "Edit Goal";
            viewModel = new GoalWindowViewModel(this, this.windowUsage)
            {
                MainViewModel = mainViewModel,
            };
            if (goal != null)
            {
                viewModel.Goal = goal;
            }
            DataContext = viewModel;
        }
    }
}
