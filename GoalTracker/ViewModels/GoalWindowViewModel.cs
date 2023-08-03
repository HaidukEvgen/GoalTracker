using GoalTracker.Enums;
using GoalTracker.Models;
using GoalTracker.MVVM;
using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using GoalTracker.Converters;
using GoalTracker.Views;
using Path = System.IO.Path;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace GoalTracker.ViewModels
{
    class GoalWindowViewModel : ViewModelBase
    {
        private UIGoal _goal = new()
        {
            Deadline = DateTime.Today.AddDays(1),
            Image = File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"..\..\..\Images\goalImage1.png")),
        };

        public UIGoal Goal
        {
            get => _goal;
            set
            {
                if (_goal != value)
                {
                    _goal = value;
                    OnPropertyChanged();
                }
            }
        } 
        public required MainWindowViewModel MainViewModel { get; set; }
        public RelayCommand SaveGoalCommand { get; set; }
        public RelayCommand SelectPictureCommand { get; set; }
        public RelayCommand SetWeekCommand { get; set; }
        public RelayCommand SetTwoWeeksCommand { get; set; }
        public RelayCommand SetMonthCommand { get; set; }
        public RelayCommand SetThreeMonthsCommand { get; set; }
        public RelayCommand SetSixMonthsCommand { get; set; }
        public RelayCommand SetYearCommand { get; set; }

        private readonly WindowUsage windowUsage;

        private readonly GoalWindow goalWindow;

        public GoalWindowViewModel(GoalWindow goalWindow, WindowUsage windowUsage)
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
            this.goalWindow = goalWindow;
        }
        
        private void SetWeek()
        {
            Goal.Deadline = DateTime.Today.AddDays(7);
        }

        private void SetTwoWeeks()
        {
            Goal.Deadline = DateTime.Today.AddDays(14);
        }

        private void SetMonth()
        {
            Goal.Deadline = DateTime.Today.AddMonths(1);
        }

        private void SetThreeMonths()
        {
            Goal.Deadline = DateTime.Today.AddMonths(3);
        }
        private void SetSixMonths()
        {
            Goal.Deadline = DateTime.Today.AddMonths(6);
        }

        private void SetYear()
        {
            Goal.Deadline = DateTime.Today.AddYears(1);
        }

        private void SaveGoal()
        {
            if (AnyValidationErrors() || !Goal.IsValid())
            {
                MessageBox.Show("Fix all the errors before saving a goal!", "Errors detected", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (windowUsage == WindowUsage.AddingGoal)
            {
                MainViewModel.Goals.Add(Goal);
            }
            else
            {
                MainViewModel.Goals[MainViewModel.Goals.IndexOf(MainViewModel.SelectedGoal)] = Goal;
            }

            goalWindow.Close();
        }

        private void SelectPicture()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                byte[] imageData = File.ReadAllBytes(openFileDialog.FileName);
                Goal.Image = imageData;
            }
        }

        private bool AnyValidationErrors()
        {
            List<Control> invalidControls = new List<Control>();

            // Traverse the visual tree
            FindInvalidControls(goalWindow, invalidControls);

            // Check if any controls have validation errors
            return invalidControls.Any(control => Validation.GetHasError(control));
        }

        private void FindInvalidControls(DependencyObject parent, List<Control> invalidControls)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is Control control && Validation.GetHasError(control))
                {
                    invalidControls.Add(control);
                }
                else
                {
                    FindInvalidControls(child, invalidControls);
                }
            }
        }
    }
}
