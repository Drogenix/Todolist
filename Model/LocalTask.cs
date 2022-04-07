using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Model
{
    public class LocalTask
    {
        public LocalTask()
        {

        }

        public LocalTask(string Name)
        {
            this.Name = Name;
        }
        public string Name{ get; set; }

        public bool IsComplete { get; set; } 
    }
}
