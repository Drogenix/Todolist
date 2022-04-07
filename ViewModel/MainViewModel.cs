using System;
using WpfApp.Model;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace WpfApp.ViewModel
{
    public class MainViewModel
    {
        public static ObservableCollection<DayTasks> AllTasks{ get; set; }
        public static DayTasks? SelectedDay { get; set; }
        public MainViewModel()
        {
            LoadDataFromJSON();

            _RemoveDayTask = new RelayCommand(obj =>
            {
                SelectedDay.Tasks.Remove(SelectedDay.SelectedTask);
            });

            _AddDayTask = new RelayCommand(obj =>
            {
                string TaskName = SelectedDay.TaskNameToAdd;
                if (TaskName.Length > 0)
                    SelectedDay.AddTask(new LocalTask(TaskName));
                SelectedDay.TaskNameToAdd = "";


            });

            _SaveChanges = new RelayCommand(async obj => await Task.Run(() => SaveDataInJSON()));


        }

        private static RelayCommand? _AddDayTask;
        public static RelayCommand AddDayTask
        {
            get
            {
                return _AddDayTask;
            }
        }

        private static RelayCommand? _RemoveDayTask;
        public static RelayCommand RemovedDayTask
        {
            get
            {
                return _RemoveDayTask;
            }
        }

        private static RelayCommand _SaveChanges;

        public static RelayCommand SaveChanges
        {
            get { return _SaveChanges; }
        }

        private static async void LoadDataFromJSON()
        {
            try
            {
                using (FileStream fs = new FileStream("Tasks.json", FileMode.Open))
                {
                    AllTasks = JsonSerializer.Deserialize<ObservableCollection<DayTasks>>(fs);
                }
            }
            catch when(AllTasks == null)
            {
                await Task.Run(async () => await Task.Run(()=> CreateEmptyDayTasksList()));
            }
            catch { }

        }
        private static async void SaveDataInJSON()
        {
            try
            {
                using (FileStream fs = new FileStream("Tasks.json", FileMode.OpenOrCreate))
                {

                    JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
                    JsonSerializer.Serialize(fs, AllTasks, options);
                }
            }
            catch { }
            
        }
        private static async void CreateEmptyDayTasksList()
        {
            AllTasks = new ObservableCollection<DayTasks>();
            await Task.Run(() =>
            {
                DateTime day = DateTime.Now;
                for (int i = 0; i < 365; i++)
                {
                    DayTasks dayTasks = new DayTasks(day) { index = i };
                    AllTasks.Add(dayTasks);
                    day = day.AddDays(1);
                }

            });

        }


    }
    
}
