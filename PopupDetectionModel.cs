using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP2_Flight_Inspection_App
{
    class PopupDetectionModel : IADP2Model
    {
        private bool isStop;
        private Menu notifier;
        private double speed;
        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private int time;
        public int Time
        {
            get { return time; }
            set { time = value; }
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

        public PopupDetectionModel(string[] array, string xml, Menu men)
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

        public void connect()
        {
            
        }

        public void start()
        {
        }

        public void stop()
        {
        }
    }
}
