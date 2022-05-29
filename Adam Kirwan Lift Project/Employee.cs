using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adam_Kirwan_Lift_Project
{
    class Employee
    /* This Employee Class is instantiated into an employee object via the importData method in the FileControl Class. An employee object is made
    for each employee ID, i.e. each row, in the Cloud Software Engineer Coding Exercise Data CSV file.The attributes id, location, destination, and time_requested
    are extracted from the CSV file. The booleans inLift and journeyCompleted are initialised as false but are updated in the CheckBoardingLift
    and CheckLeavingLift methods in the Lift Class.*/
    {
        public int id { get; set; }
        public int location { get; set; }

        public bool inLift = false;
        public int destination { get; set; }
        public int time_requested { get; set; }

        public bool jounreyCompleted = false;
    }
}