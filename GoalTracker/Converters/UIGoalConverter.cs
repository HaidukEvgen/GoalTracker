using System.Windows.Automation.Provider;
using GoalTracker.Models;

namespace GoalTracker.Converters
{
    public static class UIGoalConverter
    {
        public static Goal? Convert(UIGoal? uiGoal)
        {
            if (uiGoal == null)
            {
                return null;
            }
            return new Goal()
            {
                Aim = uiGoal.Aim,
                CurrentAchievement = uiGoal.CurrentAchievement,
                Deadline = uiGoal.Deadline,
                Description = uiGoal.Description,
                Image = uiGoal.Image,
                Title = uiGoal.Title,
                UnitOfMeasure = uiGoal.UnitOfMeasure
            };
        }

        public static UIGoal? Convert(Goal? goal)
        {
            if (goal == null)
            {
                return null;
            }
            return new UIGoal()
            {
                Aim = goal.Aim,
                CurrentAchievement = goal.CurrentAchievement,
                Deadline = goal.Deadline,
                Description = goal.Description,
                Image = goal.Image,
                Title = goal.Title,
                UnitOfMeasure = goal.UnitOfMeasure
            };
        }
    }
}
