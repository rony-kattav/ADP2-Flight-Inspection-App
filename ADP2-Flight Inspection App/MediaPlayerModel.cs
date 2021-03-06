using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace ADP2_Flight_Inspection_App
{
    class MediaPlayerModel : IADP2Model
    {

        private double speed;
        public double Speed {
            get { return speed; }
            set { 
                speed = value ;
                NotifyPropertyChanged("Speed");
            } 
        }
        private int time;
        public int Time {
            get { return time; }
            set {
                time = value;
                NotifyPropertyChanged("Time");
            }
        }

        public int numOfRows
        {
            get { return dataArray.Length; }
        }

        public string[] dataArray;
        private bool isStop;


        public MediaPlayerModel(string[] array)
        {
            isStop = false;
            Speed = 1;
            Time = 0;
            dataArray = array;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void connect()
        {

        }


        public void start()
        {
            new Thread(delegate ()
            {
                int length = dataArray.Length;
                while(time!= length) { 
                    time++;
                    // notifies only when the time changes because the time passed
                    // and not because someone changed it manually by the media player
                    NotifyPropertyChanged("TimePassed");
                    while(speed == 0)
                    {

                    }
                    Thread.Sleep((int)( 100 / speed));
                }

            }).Start();

        }

        public void stop()
        {
            //isStop = true;

        }
    }

}
