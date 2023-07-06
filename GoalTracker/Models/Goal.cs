using System;

namespace GoalTracker.Models
{
    public class Goal
    {
        public Goal()
        {
            
        }
        public Goal(Goal goal, byte[] imageData)
        {
            Title = goal.Title;
            Deadline = goal.Deadline;
            Image = imageData;
            Description = goal.Description;
            Aim = goal.Aim;
            UnitOfMeasure = goal.UnitOfMeasure;
            CurrentAchievement = goal.CurrentAchievement;
        }

        public Goal(Goal goal, DateTime deadline)
        {
            Title = goal.Title;
            Deadline = deadline;
            Image = goal.Image;
            Description = goal.Description;
            Aim = goal.Aim;
            UnitOfMeasure = goal.UnitOfMeasure;
            CurrentAchievement = goal.CurrentAchievement;
        }

        public string Title { get; set; }

        public DateTime? Deadline { get; set; }

        public byte[] Image { get; set; }

        public string Description { get; set; }

        public int Aim { get; set; }
            
        public string UnitOfMeasure { get; set; }

        public int CurrentAchievement { get; set; } = 0;

        public double Progress
        {
            get
            {
                if (Aim == 0)
                {
                    return 0.0;
                }
                return (double)CurrentAchievement * 100 / Aim;
            }
        }

        public bool IsValid()
        {
            return true;
        }
    }
}
