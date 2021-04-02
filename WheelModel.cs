using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ADP2_Flight_Inspection_App
{
    public class WheelModel : IADP2Model
    {
        private double speed;
        public double Speed
        {
            get { return speed; }
            set {
                speed = value;
            }
        }

        private int time;
        public int Time
        {
            get { return time; }
            set { time = value;
            }
        }

        private int numofrows;
        public int numOfRows {
            get { return numofrows; }
        }
        private string XMLpath;
        private string[] dataArray;
        // map between the coloumn name in the csv to it's coloumn number
        private Dictionary<string,int> coloumns;

        private double aileron;
        public double Aileron
        {
            get { return aileron; }
            set
            {
                aileron = value;
                NotifyPropertyChanged("Aileron");
            }
        }

        private double elevator;
        public double Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value ;
                NotifyPropertyChanged("Elevator");
            }
        }

        private double rudder;
        public double Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value; ;
                NotifyPropertyChanged("Rudder");
            }
        }

        private double throttle;
        public double Throttle
        {
            get { return throttle; }
            set
            {
                throttle = value; ;
                NotifyPropertyChanged("Throttle");
            }
        }

        private bool isStop;
        private Menu notifier;


        public WheelModel(string[] array, string xml, Menu men)
        {
            XMLpath = xml;
            dataArray = array;
            numofrows = array.Length;
            speed = 1;
            time = 0;
            isStop = false;
            coloumns = new Dictionary<string, int>();
            notifier = men;
            notifier.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propName)
        {
            if (String.Compare(propName, "Speed") == 0)
            {
                Speed = notifier.Speed;
            }
            else if (String.Compare(propName, "Time") == 0)
            {
                Time = notifier.Time;
            }
            
            else if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));

            }
            
        }

        private void readXML()
        {
            // counter for the rows on the csv
            int i = 0;
            // counter for the throttles (according to number of engines)
            using (XmlReader reader = XmlReader.Create(XMLpath)) 
            {
                while (reader.Read() && String.Compare(reader.Name, "output") != 0)
                {
                }

                while (reader.Read() && String.Compare(reader.Name, "output")!=0)
                {
                                       
                    if (String.Compare(reader.Name, "name") == 0) 
                    {
                        string name = reader.ReadString();
                        switch (name)
                        {
                            case "throttle":
                            case "aileron":
                            case "elevator":
                            case "rudder":
                                // if there is the same name but for different engines,
                                //the first one will be as it's written but the nexts will be with numbers.
                                // for example - if there are 2 throttles - the first one will be "throttle" and the next "throttle1"
                                int k = 1;
                                while (coloumns.ContainsKey(name))
                                {
                                    if (Char.IsDigit(name[name.Length - 1]))
                                    {
                                        name.Remove(name.Length - 1, 1);
                                    }
                                    name += k;
                                    k++;
                                }
                                coloumns.Add(name, i);
                                break;
                                
                        }
                        i++;
                    }
                }                  
            }
        }

        public void connect()
        {
            readXML();
        }

        private void updateData(string line)
        {
            string[] pars = line.Split(',');
            int index = coloumns["aileron"];
            Aileron = Double.Parse(pars[index]);
            index = coloumns["elevator"];
            Elevator = Double.Parse(pars[index]);
            index = coloumns["rudder"];
            Rudder = Double.Parse(pars[index]);
            index = coloumns["throttle"];
            Throttle = Double.Parse(pars[index]);
        }

        public void start()
        {
            new Thread(delegate ()
            {
                int length = dataArray.Length;
                while (time != length && isStop != true)
                {
                    updateData(dataArray[time]);
                    Time++;
                    while (speed == 0)
                    {

                    }
                    Thread.Sleep((int)(100 / speed));
                }

            }).Start();
        }

        public void stop()
        {
            isStop = true;
        }
    }
}
