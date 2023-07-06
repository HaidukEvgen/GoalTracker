using GoalTracker.Enums;
using GoalTracker.Models;
using GoalTracker.MVVM;
using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace GoalTracker.ViewModels
{
    class GoalWindowViewModel : ViewModelBase
    {
        public Goal Goal { get; set; } = new Goal()
        {
            Deadline = DateTime.Today.AddDays(1),
            Image = File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"..\..\..\Images\goalImage1.png")),
        };
        public required MainWindowViewModel MainViewModel { get; set; }
        public RelayCommand SaveGoalCommand { get; set; }
        public RelayCommand SelectPictureCommand { get; set; }
        public RelayCommand SetWeekCommand { get; set; }
        public RelayCommand SetTwoWeeksCommand { get; set; }
        public RelayCommand SetMonthCommand { get; set; }
        public RelayCommand SetThreeMonthsCommand { get; set; }
        public RelayCommand SetSixMonthsCommand { get; set; }
        public RelayCommand SetYearCommand { get; set; }

        private WindowUsage windowUsage;

        public GoalWindowViewModel(WindowUsage windowUsage)
        {
            SaveGoalCommand = new RelayCommand(SaveGoal, () => Goal.IsValid());
            SelectPictureCommand = new RelayCommand(SelectPicture);
            SetWeekCommand = new RelayCommand(SetWeek);
            SetTwoWeeksCommand = new RelayCommand(SetTwoWeeks);
            SetMonthCommand = new RelayCommand(SetMonth);
            SetThreeMonthsCommand = new RelayCommand(SetThreeMonths);
            SetSixMonthsCommand = new RelayCommand(SetSixMonths);
            SetYearCommand = new RelayCommand(SetYear);
            this.windowUsage = windowUsage;
        }

        private void SetWeek()
        {
            //Goal.Deadline = DateTime.Today.AddDays(7);
            Goal = new Goal(Goal, DateTime.Today.AddDays(7));
            OnPropertyChanged(nameof(Goal));
        }

        private void SetTwoWeeks()
        {
            //Goal.Deadline = DateTime.Today.AddDays(14);
            Goal = new Goal(Goal, DateTime.Today.AddDays(14));
            OnPropertyChanged(nameof(Goal));
        }

        private void SetMonth()
        {
            //Goal.Deadline = DateTime.Today.AddMonths(1);
            Goal = new Goal(Goal, DateTime.Today.AddMonths(1));
            OnPropertyChanged(nameof(Goal));
        }

        private void SetThreeMonths()
        {
            //Goal.Deadline = DateTime.Today.AddMonths(3);
            Goal = new Goal(Goal, DateTime.Today.AddMonths(3));
            OnPropertyChanged(nameof(Goal));
        }
        private void SetSixMonths()
        {
            //Goal.Deadline = DateTime.Today.AddMonths(6);
            Goal = new Goal(Goal, DateTime.Today.AddMonths(6));
            OnPropertyChanged(nameof(Goal));
        }

        private void SetYear()
        {
            //Goal.Deadline = DateTime.Today.AddYears(1);
            Goal = new Goal(Goal, DateTime.Today.AddYears(1));
            OnPropertyChanged(nameof(Goal));
        }

        private void SaveGoal()
        {
            if (windowUsage == WindowUsage.AddingGoal)
            {
                MainViewModel.Goals.Add(Goal);
            }
            else
            {
                MainViewModel.Goals[MainViewModel.Goals.IndexOf(MainViewModel.SelectedGoal)] = Goal;
            }
        }

        private void SelectPicture()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                byte[] imageData = File.ReadAllBytes(openFileDialog.FileName);
                Goal = new Goal(Goal, imageData);
                OnPropertyChanged(nameof(Goal));
            }
        }
    }
}
