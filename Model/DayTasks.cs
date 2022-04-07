using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Model
{
    public class DayTasks:ObservableObject
    {
        public DayTasks() { }

        public DayTasks(DateTime Date)
        {
            this.Date = Date;
            string weekDay = Date.ToString("dddd");
            string day = weekDay[0].ToString().ToUpper() + weekDay.Substring(1);
            DateToString = $"{day}{Date.ToString(", dd MMMM yyyy ")}г.";
            Tasks = new ObservableCollection<LocalTask>();
        }
        public int index { get; set; }

        [JsonIgnore]
        public LocalTask SelectedTask { get; set; }
        [JsonIgnore]
        private string taskNameToAdd;
        [JsonIgnore]
        public string TaskNameToAdd
        {
            get { return taskNameToAdd; }
            set 
            {
                taskNameToAdd = value;
                OnPropertChanged();
            }
        }

        public DateTime Date { get; set; }

        public string DateToString { get; set; }

        public ObservableCollection<LocalTask> Tasks { get; set; }

        public void AddTask(LocalTask task)
        {
            Tasks.Add(task);
        }
        
    }
}
