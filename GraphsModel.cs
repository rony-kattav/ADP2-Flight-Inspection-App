using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ADP2_Flight_Inspection_App
{
    class GraphsModel : IADP2Model
    {
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
        // map between the coloumn name in the csv to it's coloumn number
        private Dictionary<string, int> coloumns;

        private bool isStop;
        private Menu notifier;


        public GraphsModel(string[] array, string xml, Menu men)
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
            int j = 1;
            using (XmlReader reader = XmlReader.Create(XMLpath))
            {
                while (reader.Read() && String.Compare(reader.Name, "output") != 0)
                {
                }

                while (reader.Read() && String.Compare(reader.Name, "output") != 0)
                {

                    if (String.Compare(reader.Name, "name") == 0)
                    {
                        string name = reader.ReadString();
                        // if there is the same name but for different engines,
                        //the first one will be as it's written but the nexts will be with numbers.
                        // for example - if there are 2 throttles - the first one will be "throttle" and the next "throttle1"
                        int k = 1;
                        while (coloumns.ContainsKey(name))
                        {
                            if(Char.IsDigit(name[name.Length - 1]))
                            {
                                name.Remove(name.Length - 1, 1);
                            }
                            name += k;
                            k++;
                        }
                        coloumns.Add(name, i);
                        i++;
                    }
                }
            }

        }

        public void connect()
        {
            readXML();
        }


        public void start()
        {
        }
        public void stop()
        {
        }


    }
}
