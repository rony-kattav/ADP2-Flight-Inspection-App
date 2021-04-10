using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Xml;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;

namespace ADP2_Flight_Inspection_App
{
    public class ADP2myModel : IADP2Model
    {

        private Menu notifier;

        private double speed;
        public double Speed
        {
            get { return speed; }
            set
            {
                speed = value;
            }
        }

        private int time;
        public int Time
        {
            get { return time; }
            set
            {
                time = value;
            }
        }

        private int numofrows;
        public int numOfRows
        {
            get { return numofrows; }
        }

        private string XMLpath;
        private string[] dataArray;
        private bool isStop;
        private Socket fg;

        public event PropertyChangedEventHandler PropertyChanged;

        public ADP2myModel(string[] array, string xml, Menu men)
        {
            dataArray = array;
            XMLpath = xml;
            speed = 1;
            time = 0;
            isStop = false;
            numofrows = dataArray.Length;
            notifier = men;
            notifier.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };

        }


        

        public void connect()
        {
            


            // copy the xml file into the flightgear folder
            //copyXMLToFG();
            
            /*
            // run the FlightGear application
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = null;
            start.FileName = "C:\\Program Files\\FlightGear 2020.3.6\\bin\\fgfs.exe";
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                //proc.WaitForExit();

                // Retrieve the app's exit code
                //exitCode = proc.ExitCode;
            }*/
            

            // connect to port 5400
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 5400);
            try
            {
                fg = new Socket(ipAddr.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

                fg.Connect(localEndPoint);

            }
            catch (Exception e){
                isStop = true;
            }







        }

        // copy the xml file from the given path to the flightgear folder
        private void copyXMLToFG()
        {
            
            string newFileName = "C:\\Program Files\\FlightGear 2020.3.6\\data\\Protocol\\playback_small_user.xml";
            //string newFileName = @"C:\Users\User\Desktop\playback_small.xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(XMLpath);

            Console.WriteLine(doc.OuterXml);
            if (File.Exists(newFileName))
                File.Delete(newFileName);
            
            doc.Save(newFileName);
            
            
        }

        

        public void start()
        {

            new Thread(delegate ()
            {
                StreamWriter writer = null;
                try
                {
                    writer = new StreamWriter(new NetworkStream(fg));

                }
                catch (Exception e)
                {
                    isStop = true;
                }

                int length = dataArray.Length;
                while (time != length && isStop != true)
                {
                    string line = dataArray[time];
                    try
                    {
                        writer.WriteLine(line);

                    }
                    catch (Exception e)
                    {
                        isStop = true;
                    }
                    Time++;
                    while (speed == 0)
                    {

                    }
                    Thread.Sleep((int)(100 / speed));
                }
                stop();


            }).Start();

        }

        public void stop()
        {
            isStop = true;
            try
            {
                fg.Close();


            }
            catch (Exception e)
            {
            }

        }


        public void NotifyPropertyChanged(string propName)
        {
            
            if (String.Compare(propName, "Speed") == 0)
            {
                speed = notifier.Speed;
            }
            else if (String.Compare(propName, "Time") == 0)
            {
                time = notifier.Time;
            }
            
            
        }

    }
}
