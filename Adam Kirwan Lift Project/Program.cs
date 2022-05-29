using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adam_Kirwan_Lift_Project
{
    class Program
    {
        /* This is the main program. It first creates an empty list of employees which is subsequently filled with employee objects from the Cloud Software Engineer 
         * Coding Exercise Data CSV file. The time is initialised to 0. Two objects are instantiated: a Lift object, and a FileControl object. 
         */
        public static List<Employee> mblah = new List<Employee>() { };
        static void Main(string[] args)
        {
            int time = 0;
            Lift lift = new Lift();
            FileControl file = new FileControl(time, lift.currentLocation, lift.stoppedHere, lift.movingUpint, lift.movingDownint, lift.liftTrajectory, lift.listOfRequests, lift.liftMovingUp, lift.combinedList, lift.noInLift);

            file.importData();
            File.WriteAllText(@"C:/Users/Adam/source/repos/Adam Kirwan Lift Project/TestFile.csv", lift.header);
            int NoOfEmployees = mblah.Count;

            while (lift.listOfRequests.Count == 0)
            {
                lift.CheckExternalRequest(time);
                time += 1;
            }

            int sum = 0;
            while (NoOfEmployees != sum)
            {
                sum = 0;
                lift.liftInUse = true;
                time = lift.liftAction(time);
                foreach (Employee i in lift.listOfRequests)
                {
                    if (i.jounreyCompleted == true)
                    {
                        sum += 1;
                    }
                }
                file.WriteCSVLine(time, lift.currentLocation, lift.stoppedHere, lift.movingUpint, lift.movingDownint, lift.liftTrajectory, lift.listOfRequests, lift.liftMovingUp, lift.combinedList, lift.noInLift);
                lift.move();
                time += 10;
            }

        }
    }
}