using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GoalTracker.Models
{
    public class UIGoal : INotifyPropertyChanged
    {
        public UIGoal()
        {
            
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        private DateTime? _deadline;
        public DateTime? Deadline
        {
            get => _deadline;
            set
            {
                SetField(ref _deadline, value);
                _isDedlineMet = _deadline > DateTime.Today;
            }
        }

        private byte[] _image;
        public byte[] Image
        {
            get => _image;
            set => SetField(ref _image, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetField(ref _description, value);
        }

        private int _aim;

        public int Aim
        {
            get => _aim;
            set
            {
                SetField(ref _aim, value);
                if (Aim == 0)
                {
                    Progress = 0.0;
                }

                Progress = (double)CurrentAchievement * 100 / Aim;
                IsCompleted = Progress >= 100;
            }
        }

        private string _unitOfMeasure;
        public string UnitOfMeasure
        {
            get => _unitOfMeasure;
            set => SetField(ref _unitOfMeasure, value);
        }

        private int _currentAchievement;
        public int CurrentAchievement
        {
            get => _currentAchievement;
            set
            {
                SetField(ref _currentAchievement, value);
                if (Aim == 0)
                {
                    Progress = 0.0;
                }

                Progress = (double)CurrentAchievement * 100 / Aim;
                IsCompleted = Progress >= 100;
            }
        }

        private double _progress;
        public double Progress
        {
            get => _progress;
            set => SetField(ref _progress, value);
        }


        public bool IsValid()
        {
            if (Aim < CurrentAchievement || string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(UnitOfMeasure))
                return false;
            return true;
        }

        public void Achieve()
        {
            CurrentAchievement = Aim;
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set => SetField(ref _isCompleted, value);
        }

        private bool _isDedlineMet;
        public bool IsDeadlineMet
        {
            get => _isDedlineMet;
            set => SetField(ref _isDedlineMet, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public UIGoal CreateCopy()
        {
            UIGoal copy = new UIGoal
            {
                Title = Title,
                Deadline = Deadline,
                Image = Image != null ? Image.ToArray() : null,
                Description = Description,
                Aim = Aim,
                UnitOfMeasure = UnitOfMeasure,
                CurrentAchievement = CurrentAchievement
            };

            return copy;
        }
    }

}
