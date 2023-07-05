using System;
using System.Collections.ObjectModel;
using System.IO;
using GoalTracker.Enums;
using System.Windows;
using GoalTracker.Models;
using GoalTracker.MVVM;
using GoalTracker.Views;
using Microsoft.Win32;
using System.Reflection;

namespace GoalTracker.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Goal> Goals { get; set; }

        public RelayCommand ShowGoalWindowCommand { get; set; }
        public MainWindowViewModel()
        {
            ShowGoalWindowCommand = new RelayCommand(ShowGoalWindow);
            object openFileDialog;
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"..\..\..\Images\");
            Goals = new ObservableCollection<Goal>
            {
                new() { Aim = 9, Description = "caption", Deadline = new DateTime(2024, 7, 12), Image = File.ReadAllBytes(path + "goalImage1.png"), Title = "Title", CurrentAchievement = 3, UnitOfMeasure = "People"},
                new() { Aim = 50, Description = "caption", Deadline = new DateTime(2023, 12, 12), Image = File.ReadAllBytes(path + "goalImage2.png"), Title = "Another", CurrentAchievement = 13, UnitOfMeasure = "Trainings"},
                new() { Aim = 1000000, Description = "To earn", Deadline = new DateTime(2023, 07, 04), Image = File.ReadAllBytes(path + "goalImage4.jpg"), Title = "Become rich", CurrentAchievement = 10000, UnitOfMeasure = "Dollars"}
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
