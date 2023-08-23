using System.Collections;
using System.Collections.Generic;
using GoalTracker.Models;

namespace GoalTracker.Services
{
    public interface IFileSaver
    {
        void CreateNewFile();
        void OpenFile();
        void SaveAs();
        void Save();
    }
}
