using System;
using System.Collections.ObjectModel;
using GoalTracker.Enums;
using System.Windows;
using GoalTracker.Models;
using GoalTracker.MVVM;
using GoalTracker.Views;

namespace GoalTracker.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Goal> Goals { get; set; }

        public RelayCommand ShowGoalWindowCommand { get; set; }
        public MainWindowViewModel()
        {
            ShowGoalWindowCommand = new RelayCommand(ShowGoalWindow);
            Goals = new ObservableCollection<Goal>
            {
                new() { Aim = 9, Description = "caption", Deadline = new DateTime(2024, 7, 12), Image = null, Title = "Title", CurrentAchievement = 3, UnitOfMeasure = "People"},
                new() { Aim = 50, Description = "caption", Deadline = new DateTime(2023, 12, 12), Image = null, Title = "Another", CurrentAchievement = 13, UnitOfMeasure = "Trainings"}
            };
        }

        private Goal? selectedGoal;
        public Goal? SelectedGoal
        {
            get => selectedGoal;
            set
            {
                selectedGoal = value;
                OnPropertyChanged();
            }
        }

        private void ShowGoalWindow()
        {
            GoalWindow goalWindow = new GoalWindow(WindowUsage.AddingGoal, this);
            goalWindow.ShowDialog();
        }
    }
}
