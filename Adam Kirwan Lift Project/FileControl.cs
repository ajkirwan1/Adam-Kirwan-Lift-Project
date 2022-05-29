using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adam_Kirwan_Lift_Project
{
    class FileControl
    {

        public FileControl(int time, int currentLocation, bool stoppedHere, List<int> movingUpint, List<int> movingDownint, string liftTrajectory, List<Employee> listOfRequests, bool liftMovingUp, string combinedList, int noInLift) { }
        public void createTempList()
        {
            string path = "C:/Users/Adam/source/repos/Lift/Lift/Cloud Software Engineer Coding Exercise Data.csv";
            string[] lines = System.IO.File.ReadAllLines(path);
            System.IO.File.WriteAllLines(@"C:/Users/Adam/source/repos/Lift/Lift/ListCopy.txt", lines);
        }



        public void importData()
        {
            string path = "C:/Users/Adam/source/repos/Lift/Lift/Cloud Software Engineer Coding Exercise Data.csv";
            string[] lines = System.IO.File.ReadAllLines(path);

            for (int i = 1; i < lines.Length; i++)
            {
                //Console.WriteLine(lines[i]);
                string[] sub_string = lines[i].Split(',');
                Employee ob = new Employee() { id = Int32.Parse(sub_string[0]), location = Int32.Parse(sub_string[1]), destination = Int32.Parse(sub_string[2]), time_requested = Int32.Parse(sub_string[3]) };
                Program.mblah.Add(ob);
            }
        }


        public void WriteCSVLine(int time, int currentLocation, bool stoppedHere, List<int> movingUpint, List<int> movingDownint, string liftTrajectory, List<Employee> listOfRequests, bool liftMovingUp, string combinedList, int noInLift)
        {
            string lifttLocation = currentLocation + ",";
            string liftStoppedHere = stoppedHere + ",";
            string currentTime = time + ",";
            string movingUpCalls = String.Format("[{0}],", String.Join(" ", movingUpint));
            string movingDownCalls = string.Format("[{0}],", string.Join(" ", movingDownint));
            string trajectory = liftTrajectory + ",";
            string completedJourney;
            List<int> inlift = new List<int>() { };
            List<int> completed = new List<int>() { };
            string m = String.Join(" ", movingUpint) + ",";
            foreach (Employee i in listOfRequests)
            {
                if (i.inLift == true)
                {
                    inlift.Add(i.id);
                }
            }
            foreach (Employee i in listOfRequests)
            {
                if (i.jounreyCompleted == true)
                {
                    completed.Add(i.id);
                }
            }
            completedJourney = string.Format("[{0}],", string.Join(" ", completed));
            string IDsInLift = string.Format("[{0}],", string.Join(" ", inlift));
            if (liftMovingUp == true)
            {
                combinedList = string.Format("[{0}]", string.Join(" ", movingUpint)) + string.Format("[{0}],", string.Join(" ", movingDownint));
            }
            else if (liftMovingUp == false)
            {
                combinedList = string.Format("[{0}]", string.Join(" ", movingDownint)) + string.Format("[{0}],", string.Join(" ", movingUpint));
            }

            string e = "\n" + lifttLocation + liftStoppedHere + currentTime + movingUpCalls + movingDownCalls + trajectory + combinedList + IDsInLift + completedJourney + noInLift;
            File.AppendAllText(@"C:/Users/Adam/source/repos/Adam Kirwan Lift Project/TestFile.csv", e);

        }






    }
}