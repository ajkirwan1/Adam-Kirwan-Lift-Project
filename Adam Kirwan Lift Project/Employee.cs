using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adam_Kirwan_Lift_Project
{
    class Employee
    {
        public int id { get; set; }
        public int location { get; set; }

        public bool inLift = false;
        public int destination { get; set; }
        public int time_requested { get; set; }

        public bool jounreyCompleted = false;
    }
}
