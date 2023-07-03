using System;

namespace GoalTracker.Models
{
    public class Goal
    {
        public string Title { get; set; }

        public DateTime? Deadline { get; set; }

        public byte[] Image { get; set; }

        public string Description { get; set; }

        public int Aim { get; set; }
            
        public string UnitOfMeasure { get; set; }

        public int CurrentAchievement { get; set; } = 0;

        public double Progress => (double)CurrentAchievement * 100 / Aim ;

        public bool IsValid()
        {
            return true;
        }
    }
}
