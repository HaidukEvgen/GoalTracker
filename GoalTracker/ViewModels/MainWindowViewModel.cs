﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using GoalTracker.Enums;
using System.Windows;
using GoalTracker.Models;
using GoalTracker.MVVM;
using GoalTracker.Views;
using System.Reflection;
using GoalTracker.Services;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace GoalTracker.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<UIGoal> Goals { get; set; }

        public RelayCommand ShowGoalWindowCommand { get; set; }

        public RelayCommand EditGoalCommand { get; set; }

        public RelayCommand GoalIsAchievedCommand { get; set; }

        public RelayCommand RemoveGoalCommand { get; set; }

        public RelayCommand NewFileCommand { get; set; }
        
        public RelayCommand OpenFileCommand { get; set; }
        
        public RelayCommand SaveFileCommand { get; set; }

        public RelayCommand SaveAsFileCommand { get; set; }


        private readonly IFileSaver _fileSaver;

        public MainWindowViewModel()
        {
            ShowGoalWindowCommand = new RelayCommand(ShowGoalWindow);
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"..\..\..\Images\");
            Goals = new ObservableCollection<UIGoal>();
            _fileSaver = new FileSaver(Goals);
            EditGoalCommand = new RelayCommand(EditGoal);
            GoalIsAchievedCommand = new RelayCommand(GoalIsAchieved);
            RemoveGoalCommand = new RelayCommand(RemoveGoal);
            NewFileCommand = new RelayCommand(_fileSaver.CreateNewFile);
            OpenFileCommand = new RelayCommand(_fileSaver.OpenFile);
            SaveAsFileCommand = new RelayCommand(_fileSaver.SaveAs);
            SaveFileCommand = new RelayCommand(_fileSaver.Save);
        }

        private void RemoveGoal()
        {
            if (selectedGoal == null)
                return;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the selected goal?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Goals.Remove(selectedGoal);
            }
        }

        private void GoalIsAchieved()
        {
            if (selectedGoal == null)
                return;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to mark the selected goal as achieved?", "Confirm Achievement", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Goals[Goals.IndexOf(selectedGoal)].Achieve();
            }
        }

        private void EditGoal()
        {
            if (selectedGoal != null)
            {
                GoalWindow goalWindow = new GoalWindow(WindowUsage.EditingGoal, this, selectedGoal.CreateCopy());
                goalWindow.ShowDialog();
            }
        }

        private UIGoal? selectedGoal;
        public UIGoal? SelectedGoal
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
            GoalWindow goalWindow = new GoalWindow(WindowUsage.AddingGoal, this, null);
            goalWindow.ShowDialog();
        }

        public void OnClosing()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "Unsaved data", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _fileSaver.Save();
            }
        }
    }
}
