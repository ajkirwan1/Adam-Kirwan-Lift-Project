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

        /* This Class contains one attribute: outputPath. The desired path/location of the output should be placed here as a string. 
         * This Class also contains two mnethods. The importData method takes one string argument to specify the path/location of 
         * the Cloud Software Engineer Coding Exercise Data.csv file. The WriteCSVLine method handles writing to the CSV file. No user changes are needed 
         * to WriteCSVLine */


        // The output path location should be inputted below in the variable outputPath
        public string outputPath = "C:/Users/Adam/source/repos/Adam Kirwan Lift Project/Output.csv";

        public FileControl(int time, int currentLocation, bool stoppedHere, List<int> movingUpint, List<int> movingDownint, string liftTrajectory, List<Employee> listOfRequests, bool liftMovingUp, string combinedList, int noInLift) { }

        public void importData(string inputPath)
        {
            // The importData method takes a single string argument to specify the location of Cloud Software Engineer Coding Exercise Data.csv.
            // The argument is addded in the main program (line 22).
            string[] lines = System.IO.File.ReadAllLines(inputPath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] sub_string = lines[i].Split(',');
                Employee ob = new Employee() { id = Int32.Parse(sub_string[0]), location = Int32.Parse(sub_string[1]), destination = Int32.Parse(sub_string[2]), time_requested = Int32.Parse(sub_string[3]) };
                Program.employee.Add(ob);
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
            File.AppendAllText(outputPath, e);
        }
    }
}