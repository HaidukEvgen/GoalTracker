using GoalTracker.Enums;
using GoalTracker.Models;
using GoalTracker.MVVM;
using Microsoft.Win32;
using System.IO;

namespace GoalTracker.ViewModels
{
    class GoalWindowViewModel : ViewModelBase
    {
        public Goal Goal { get; set; } = new Goal();
        public required MainWindowViewModel MainViewModel { get; set; }
        public RelayCommand SaveGoalCommand { get; set; }
        public RelayCommand SelectPictureCommand { get; set; }

        private WindowUsage windowUsage;

        public GoalWindowViewModel(WindowUsage windowUsage)
        {
            SaveGoalCommand = new RelayCommand(SaveGoal, () => Goal.IsValid());
            SelectPictureCommand = new RelayCommand(SelectPicture);
            this.windowUsage = windowUsage;
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
