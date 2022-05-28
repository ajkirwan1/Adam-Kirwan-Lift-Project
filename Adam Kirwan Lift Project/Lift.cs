using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adam_Kirwan_Lift_Project
{
    class Lift
    {
        int[] previousStops = { };
        public bool liftInUse = false;
        public bool liftMovingUp = true;
        public int currentLocation = 1;
        public int noInLift = 0;
        public string header = "Current Floor, Collection/Drop-off,Time / s,Moving up Stops,Moving Down Stops, Lift Trajectory, Stop Order, Employee IDs in Lift, Journey Completed by, No. of Employees in Lift";
        public string liftTrajectory;
        public bool stoppedHere;
        public string combinedList;

        public List<int> liftHistory = new List<int>() { };
        public List<int> movingUpint = new List<int>() { };
        public List<int> movingDownint = new List<int>() { };
        public List<Employee> listOfRequests = new List<Employee>() { };
        public List<int> distinctMovingUp = new List<int>() { };
        public List<int> IDsCompleted = new List<int>() { };


        public void move()
        {
            if (movingUpint.Count != 0 || movingDownint.Count != 0)
            {
                if (liftMovingUp == true && currentLocation != movingUpint[0])
                {
                    currentLocation += 1;
                }
                else if (liftMovingUp == true && currentLocation == movingUpint[0])
                {
                    if (currentLocation != 10)
                    {
                        currentLocation += 1;
                    }
                    else if (currentLocation == 10)
                    {
                        currentLocation -= 1;
                    }
                }
                else if (liftMovingUp == false && currentLocation != movingDownint[0])
                {
                    currentLocation -= 1;
                }
                else if (liftMovingUp == false && currentLocation == movingDownint[0])
                {
                    if (currentLocation != 1)
                    {
                        currentLocation -= 1;
                    }
                    else if (currentLocation == 1)
                    {
                        currentLocation += 1;
                    }
                }
            }
            else
            {
                // Do nothing
            }

        }

        public void CheckExternalRequest(int time)
        {

            if (Program.mblah.Count > 0)
            {
                int count = 0;
                foreach (Employee i in Program.mblah)
                {
                    if (i.time_requested <= time)
                    {
                        listOfRequests.Add(i);
                        appendToMovingUpORDownint(i, 1);
                        count += 1;
                    }
                }
                Program.mblah.RemoveRange(0, count);
            }
        }

        public void CheckBoardingLift()
        {
            if (noInLift < 8)
            {
                foreach (Employee i in listOfRequests)
                {

                    if (i.jounreyCompleted == false)
                    {

                        if (i.location == currentLocation && i.inLift == false)
                        {
                            i.inLift = true;
                            noInLift += 1;
                            appendToMovingUpORDownint(i, 2);
                            if (noInLift == 8)
                            {
                                break;
                            }
                        }
                        else
                        {
                        }

                    }
                }
            }
            else
            {
            }

        }
        public void CheckLeavingLift()
        {
            foreach (Employee i in listOfRequests)
            {
                if (i.jounreyCompleted == false)
                {
                    if (i.destination == currentLocation && i.inLift == true)
                    {
                        noInLift -= 1;
                        i.inLift = false;
                        i.jounreyCompleted = true;
                    }
                    else
                    {
                    }
                }
            }
        }

        public void appendToMovingUpORDownint(Employee person, int x)
        {
            if (x == 1)
            {
                if (person.location > currentLocation && movingUpint.Contains(person.location) == false)
                {
                    movingUpint.Add(person.location);
                    movingUpint.Sort();

                }
                else if (person.location < currentLocation && movingDownint.Contains(person.location) == false)
                {
                    movingDownint.Add(person.location);
                    movingDownint.Sort();
                    movingDownint.Reverse();
                }
            }
            else if (x == 2)
            {
                if (person.destination > currentLocation && movingUpint.Contains(person.destination) == false)
                {
                    movingUpint.Add(person.destination);
                    movingUpint.Sort();
                }
                else if (person.destination < currentLocation && movingDownint.Contains(person.destination) == false)
                {
                    movingDownint.Add(person.destination);
                    movingDownint.Sort();
                    movingDownint.Reverse();
                }
            }
        }

        public bool determineIfLiftMovingUpOrDown()
        {
            if (movingUpint.Count == 0 && movingDownint.Count > 0)
            {
                liftMovingUp = false;
                liftTrajectory = "Down";
            }
            else if (movingUpint.Count > 0 && movingDownint.Count == 0)
            {
                liftMovingUp = true;
                liftTrajectory = "Up";
            }
            else if (movingUpint.Count > 0 && movingDownint.Count > 0)
            {
                if (liftHistory.Count() >= 2)
                {
                    int i = liftHistory.Count();
                    if (liftHistory.ElementAt(i - 1) >= liftHistory.ElementAt(i - 2))
                    {
                        liftMovingUp = true;
                        liftTrajectory = "Up";
                    }
                    else if (liftHistory.ElementAt(i - 1) < liftHistory.ElementAt(i - 2))
                    {
                        liftMovingUp = false;
                        liftTrajectory = "Down";
                    }
                }
                else
                {
                    liftMovingUp = true;
                    liftTrajectory = "Up";
                }

            }
            else if (movingUpint.Count == 0 && movingDownint.Count == 0)
            {
                liftMovingUp = false;
                liftTrajectory = "Lift stationary";
            }
            return liftMovingUp;
        }

        public int moveLift(int time)
        {
            Console.WriteLine("Lift is curretnly moving upwards?: " + determineIfLiftMovingUpOrDown());
            while (liftInUse == true)
            {
                CheckExternalRequest(time);

                if (movingUpint.Count != 0 || movingDownint.Count != 0)
                {



                    Console.WriteLine("movingUpint.Count != 0 || movingDownint.Count != 0");
                    if (liftMovingUp == true && currentLocation != movingUpint[0])
                    {
                        Console.WriteLine("liftMovingUp == true && currentLocation != movingUpint[0]");
                        stoppedHere = false;
                    }
                    else if (liftMovingUp == true && currentLocation == movingUpint[0])
                    {
                        Console.WriteLine("liftMovingUp == true && currentLocation == movingUpint[0] ");
                        stoppedHere = true;
                        CheckLeavingLift();
                        CheckBoardingLift();
                        movingUpint.RemoveAt(0);
                    }
                    else if (liftMovingUp == false && currentLocation != movingDownint[0])
                    {
                        Console.WriteLine("liftMovingUp == false && currentLocation != movingDownint[0]");
                        stoppedHere = false;
                    }
                    else if (liftMovingUp == false && currentLocation == movingDownint[0])
                    {
                        Console.WriteLine("liftMovingUp == false && currentLocation == movingDownint[0]");
                        stoppedHere = true;
                        CheckLeavingLift();
                        CheckBoardingLift();
                        movingDownint.RemoveAt(0);

                    }
                }
                else
                {
                }
                determineIfLiftMovingUpOrDown();
                liftHistory.Add(currentLocation);
                liftInUse = false;
            }
            Console.WriteLine("Time +10 seconds  .........");
            return time;
        }



    }
}
