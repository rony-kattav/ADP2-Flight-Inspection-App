using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ADP2_Flight_Inspection_App
{
    class WheelModel : IADP2Model
    {
        private double speed;
        public double Speed
        {
            get { return speed; }
            set {
                Speed = value;
                NotifyPropertyChanged("Speed");
            }
        }

        private int time;
        public int Time
        {
            get { return time; }
            set { time = value;
                NotifyPropertyChanged("Time");
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


        public WheelModel(string[] array, string xml)
        {
            XMLpath = xml;
            dataArray = array;
            numofrows = array.Length;
            speed = 1;
            time = 0;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propName)
        {
            if (String.Compare(propName, "Time") == 0)
            {
                if (this.PropertyChanged != null)
                {

                }
            }
            else if(String.Compare(propName, "Speed") == 0)
            {
                if (this.PropertyChanged != null)
                {

                }
            }
        }

        private void readXML()
        {
            /*
            int i = 0;
            using (XmlReader reader = XmlReader.Create(XMLpath)) 
            {


                while (reader.Read())
                {

                    if (String.Compare(reader.Name, "output") == 0)
                    {

                        while (reader.Read())
                        {
                            switch (reader.Name.ToString())
                            {
                                case "Name":
                                    Console.WriteLine("Name of the Element is : " + reader.ReadString());
                                    break;
                                case "Location":
                                    Console.WriteLine("Your Location is : " + reader.ReadString());
                                    break;
                            }


                        }

                    }
                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {
                            case "Name":
                                Console.WriteLine("Name of the Element is : " + reader.ReadString());
                                break;
                            case "Location":
                                Console.WriteLine("Your Location is : " + reader.ReadString());
                                break;
                        }
                    }
                    Console.WriteLine("");
                }
            }
            */
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
