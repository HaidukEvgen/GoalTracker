using System.Collections.Generic;
using GoalTracker.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace GoalTracker.Services;

public class FileSaver : IFileSaver
{
    private string _currentFilePath;

    private IList<UIGoal> _goals;
    public FileSaver(IList<UIGoal> goals)
    {
        _goals = goals;
    }
    public void CreateNewFile()
    {
        var result = MessageBox.Show("Save changes before creating a new file?", "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
        if (result == MessageBoxResult.Yes)
        {
            Save();
        }
        else if (result == MessageBoxResult.Cancel)
        {
            return;
        }

        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
        if (saveFileDialog.ShowDialog() == true)
        {
            _currentFilePath = saveFileDialog.FileName;
            _goals.Clear();
        }
    }

    public void OpenFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";

        if (openFileDialog.ShowDialog() == true)
        {
            _currentFilePath = openFileDialog.FileName;
            var json = File.ReadAllText(_currentFilePath);
            var loadedGoals = JsonConvert.DeserializeObject<ObservableCollection<UIGoal>>(json);
            _goals.Clear();
            foreach (var goal in loadedGoals)
            {
                _goals.Add(goal);
            }
        }
    }

    public void Save()
    {
        if (string.IsNullOrEmpty(_currentFilePath))
        {
            SaveAs();
        }
        else
        {
            var json = JsonConvert.SerializeObject(_goals, Formatting.Indented);
            File.WriteAllText(_currentFilePath, json);
        }
    }

    public void SaveAs()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";

        if (saveFileDialog.ShowDialog() == true)
        {
            _currentFilePath = saveFileDialog.FileName;
            var json = JsonConvert.SerializeObject(_goals, Formatting.Indented);
            File.WriteAllText(_currentFilePath, json);
        }
    }
}