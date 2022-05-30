using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adam_Kirwan_Lift_Project
{
    class Lift
        {
        /* The Class contains the bulk of the code. Multiple data types and lists are used to hold information such as the current position (floor)
         * of the lift, the trajectory of the lift, no. of employees in the lift, etc. 
         * As outlined in the Readme file, the trajectory of the lift at any one moment is either up or down. As such, two lists are used to store 
         * the lift's target floors: movingUpint and movingDownint. The employee list 'listOfRequests'is initially empty, but is added to every 
         * time an employee makes a request for the lift.*/

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
        /* This method is called in the main Program. It controls the motion of the lift. The method assess the lift's current trajectory, current location,
         * and the next stop, and uses this information to move the lift up one floor or down one floor. */
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
        /* This method checks requests made external to the lift, i.e., on a floor. It is called in the method liftEvent (Lift Class) to check if an employee
         * has requested the lift. It does this by checking each employee in the employeeList list to see which employees have requested the lift since the last
         * time increment of 10 seconds. For each request made, the employee object is added to the listOfRequests list. Each employee object is then passed to the 
         * appendToMovingUpORDownint list, which itself seperates requests into the lists movingUpint or movingDownint. Finally, the employee objects who have requested the lift
         *are removed from the employeeList list. */
        {

            if (Program.employee.Count > 0)
            {
                int count = 0;
                foreach (Employee i in Program.employee)
                {
                    if (i.time_requested <= time)
                    {
                        listOfRequests.Add(i);
                        appendToMovingUpORDownint(i, 1);
                        count += 1;
                    }
                }
                Program.employee.RemoveRange(0, count);
            }
        }

        public void CheckBoardingLift()
        /* This method is called each time the lift's location is equal to the 0th element in the appropriate list of requested stops (movingUpint or movingDownint). 
         * This method handles employees who move into the lift. It ensures that no more than eight employees can be in the lift at any given time. */
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
        /* This method is called each time the lift's location is equal to the 0th element in the appropriate list of requested stops (movingUpint or movingDownint).
         * This method identifies employees who are in the lift and are exiting the onto the current floor.*/
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
        /* This method assess to which list an employee's request is added. It takes two arguments: 1) An employee object, and 2) an int value x. 
         * The value x is 1 when the request passed to this method is made external to the lift (the employee's initial/starting location). The value of x is 2
         * when the request passed to this method is the employee's destination, which is made upon the employee entering the lift. This method partitions the 
         * requests into either the movingUpint or movingDownint lists, depending on whether the floor request is above or below the lift's current location. */
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
        /* This method determines whether the lift should move up or down. If the movingUpint list containts elements and the movingDownint list does not,
         * the lift's trajectory is set as up. Conversely, if the movingDownint list containts elements and the movingUpint list does not, the trajectory is set as down.
         * If no list contains elements, the list is set as stationary. If both movingUpint and movingDownint contain elements, an assessment is made with respect 
         * to the lift's location history.*/
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

        public int liftAction(int time)
        /* This is the main method of the Lift Class. It is called in the main program. Each time it is called, a check of external requests (those not in the lift) for
         * the lift are assessed. Next, it uses the lift's current trajectory to determine where to stop. For instance, if the 
         * lift is moving up, the lift's current location is compared to the 0th element of the list of stops in the movingUpint list. If the lift is moving down, 
         * the lift's current location is compared to the 0th element of the list of stops in the movingDownint list. Each time the lift's location is equal to either of
         * these 0th elements, the lift stops. Employees in the lift can then leave, then and employees on the floor can enter. This stop is then removed from the given
         * list. */
        {
            while (liftInUse == true)
            {
                CheckExternalRequest(time);
                if (movingUpint.Count != 0 || movingDownint.Count != 0)
                {
                    if (liftMovingUp == true && currentLocation != movingUpint[0])
                    {
                        stoppedHere = false;
                    }
                    else if (liftMovingUp == true && currentLocation == movingUpint[0])
                    {
                        stoppedHere = true;
                        CheckLeavingLift();
                        CheckBoardingLift();
                        movingUpint.RemoveAt(0);
                    }
                    else if (liftMovingUp == false && currentLocation != movingDownint[0])
                    {
                        stoppedHere = false;
                    }
                    else if (liftMovingUp == false && currentLocation == movingDownint[0])
                    {
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
            return time;
        }
    }
}