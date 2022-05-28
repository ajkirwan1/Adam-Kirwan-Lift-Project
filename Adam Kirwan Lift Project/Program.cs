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
        public static List<Employee> mblah = new List<Employee>() { };
        static void Main(string[] args)
        {
            int time = 0;
            Lift myObj = new Lift();
            FileControl myObj2 = new FileControl(time, myObj.currentLocation, myObj.stoppedHere, myObj.movingUpint, myObj.movingDownint, myObj.liftTrajectory, myObj.listOfRequests, myObj.liftMovingUp, myObj.combinedList, myObj.noInLift);

            myObj2.importData();
            File.WriteAllText(@"C:/Users/Adam/source/repos/Adam Kirwan Lift Project/TestFile.csv", myObj.header);
            int NoOfEmployees = mblah.Count;

            while (myObj.listOfRequests.Count == 0)
            {
                myObj.CheckExternalRequest(time);
                time += 1;
            }

            int sum = 0;
            while (NoOfEmployees != sum)
            {
                sum = 0;
                myObj.liftInUse = true;
                time = myObj.moveLift(time);
                foreach (Employee i in myObj.listOfRequests)
                {
                    if (i.jounreyCompleted == true)
                    {
                        sum += 1;
                    }
                }
                Console.WriteLine("Number of employees is: " + NoOfEmployees);
                Console.WriteLine("Number of IDs completed is: " + sum);
                myObj2.WriteCSVLine(time, myObj.currentLocation, myObj.stoppedHere, myObj.movingUpint, myObj.movingDownint, myObj.liftTrajectory, myObj.listOfRequests, myObj.liftMovingUp, myObj.combinedList, myObj.noInLift);
                myObj.move();
                time += 10;
            }

        }
    }
}
