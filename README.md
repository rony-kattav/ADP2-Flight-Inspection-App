# ADP2-Flight-Inspection-App
The app represents flight data, using the FlightGear simulator, and investigate it.
The flight data contains the steering mode, the speed, the direction, the altitude and so forth.
The app plays the plain itself and the flight data from the beginning to the end. The user can control the flight
timing, to pause/stop the flight, and then play it again. In addition, if the user wants to compare the flight with
another flight, he can upload it, along with an algorithem, and the app will display the detections of it's flight.
The app gives you number of ways to investigate the flight.
It gives you the option to watch the joystick of the plain along with the throttle and the rudder measures from the flight.
In this window you will also be able to see the values of few measures such as pitch, roll, yaw and more in the current moment.
In addition the app gives you the option to investigate the flight via graphs. 
In this window you will be able to select a specific measure you would like to investigate,
and see the values of it as a function of time from the last 30 seconds.

In our app we used the architecture of MVVM- model, view and ViewModel. We used the data binding in order to connect between featuers 
in the view with the ViewModel and used property in order to connect between the ViewModel with the model.
We used polymorphism and created an interface of ViewModel and model, and each of our models or ViewModel implemnted the appropriate interface.
This way we could hold a list of models for example and activate shared methods regardless to specific type of it. 


# Folders and files
The FlightGear program should be installed at: C:\Program Files.
After downloading it, inside Program Files a new FlightGear folder will be created with a bin and a data folders in it.
In the GIT folder of the ADP2-Flight-Inspection-App project there is the main branch with the lastest changes, a plugin folder 
with two detection algorithems that can be used with the app, a folder with PDFs explanations about the main classes and of
course a folder named "ADP2-Flight Inspection App" with all the project files.

# Prerequisites
Below is a list of things you need to install and the software:
- FlightGear version 2018 or below. You can download it from this link: https://www.flightgear.org/ .
- Oxyplot Extention in visual studio: right click on references-> Manage NuGet packages-> Brows-> download OxyPlot.Wpf

# Installing and first running
Fisrt, you need access to the files. You can go to your cmd, get to to your wanted directory and clone the project
using the command "git clone **SSH**" when ssh is the one you can fine in the git project under "code".
You can also dounload the zip folder of the project and extract the files from the zip to your wanted directory.
Now that you are in the directory with our project's files, go to: ADP2-Flight Inspection App->bin->x86->Debug.
Inside the Debug directory you should open the exe file: "ADP2-Flight Inspection App.exe" and the app will start.
Please note, when running the application, the executable file needs to be inside the directory "ADP2-Flight Inspection App"
can be also inside subfloders under this directory) and inside the "ADP2-Flight Inspection App" directory the has to be the
plugin folder with the dll files.
This is all already in the correct order in the folder you clone from github so as long as you don't make changes in the
hirarchy or altist keep it as the instructions, there should not be a problem.

Now you need to follow the instruction as mentioned bellow and also in the app:
Copy your XML file to the: "C:\Program Files\FlightGear 2020.3.6\data\Protocol" folder.
After installing the FlightGear simulator, open the program and on the setting copy the folowing lines:
--generic=socket,in,10,127.0.0.1,5400,tcp,**XMLFileName**
notice that you need to replace the **XMLFileName** with the wanted file name, and without the .xml at the end.
--fdm=null

Now, Press the Fly button on FlightGear and wait for the program to upload. When the program is up, Press the finish button on
the Flight-Inspection-App.
Now, select the pathes of the anomaly CSV file and the XML file and press the Apply button.
A menu of different options for the flight's data will apear now. Select the ones you want the app to show you.
If you choose to see the detections of the flight, you also need to choose the algorithem for the detections and a regular
flight to compare the anomaly flight to.

If you would like to make any changes in the code, make sure that you are building and debugging on x86.
# links to the full project's explenation in git

https://github.com/rony-kattav/ADP2-Flight-Inspection-App/tree/main/Explanations%20of%20the%20main%20classes

# link to the video
below is a link to an explenation video of the abilities of the ADP2-Flight-Inspection-App:

https://youtu.be/YhMcndkVHMA
