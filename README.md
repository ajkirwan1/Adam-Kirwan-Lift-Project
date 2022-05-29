# Adam-Kirwan-Lift-Project
The program was written using the Visual Studio IDE.

Input:
Taking the data contained in the Cloud Software Engineer Coding Exercise Data CSV input file, this program is designed to move each employee ID (column 1 input file) 
from their current location (column 2 input file) to their required destination (column 3 input file). The path of the input csv file should be entered as an argument
of the importData function in the main program (line 22). 

Output: 
The data is outputted in the file named Output.csv. The desired output location should be given in the string variable outputPath which is an attribute of the
FileControl
Class. Ten columns are found in the output file. 1) Current Floor: the lift's current location; 2) Collection/Droop-off: has the lift collected or delivered an
employee to this location; 3) Time: the current time in seconds; 4) Moving Up Stops: the list of stops to be made by the lift when its trajectory is upwards; 5) Moving
Down Stops: the list of stops to be made by the lift when its trajectory is downwards; 6) Lift Trajectory: the lift's current trajectory, up or down; 7) Stop Order:
this combines columns 4 and 5 and arranges them into the pojected stopping order based on the lift's trajectory; 8) Employee IDs in Lift: which employees are currently
in the lift. 9) Journey Completed by: the IDs of employees who have arrived at their destination; 10) No. of Employees in Lift: the number of employees currently
in the lift.

The program operates on the basis given in the initial proposal outlined at stage 1. This is now summerised below. 

1) At any one time the lift moves in an upwards or downwards trajectory.
2) If the lift's trajectory is upwards, it will prioritise handling requests made to and from floors above its current location in numerical order.
3) If the lift's trajectory is downwards, it will prioritise handling requests made to and from floors below its current location in reverse numerical order.
4) The above is acheived by partitioning requests into one of two lists, the choice of which is dependent on the position of the request relative to the lift's
location.
6) Requests made above the lift's location are placed into a moving-up list. Requests made below the lift's location are placed into a moving-down list.
7) If the lift's trajectory is up, the moving-up list is used to handle requests. If the lift's trajectory is down, the moving-up list is used to handle requests.
8) The lift moves in steps of one floor and 10 seconds increments.
9) The destination of the employee is only known (processed) upon entry to the lift.
10) After each step (one floor; 10 seconds), all calls for the lift made within the last 10 seconds are processed and added to the appropriate list (discussed above).
11) After each step (one floor; 10 seconds), the lift first allows employees to leave, and then allows employees to enter from the lift's current location.
12) No more than 8 employees can be in the lift at any one time.     

This means that the lift handles and responds to requests based on whether the floor requested to enter or leave the lift is above or below the lift's current
location. 


Assumptions made in the program:
In additon to the assumption that the lift takes 10 seconds to move between floors, other assumptions have been made. First, it is assumed that entry/exit of an
employee to/from the lift is instantaneous. This is clearly not going to be the case. The decision to use this assumption was to a) simplify the problem at this stage, 
and b)reduce variabilities between this and other programs that have been written. For instance, it could have been the case that in this program 5 seconds was taken 
to enter/leave the lift. In another program a time of 10 seconds could have been assumed. This would make comparisons between the programs more challenging. A fut  
