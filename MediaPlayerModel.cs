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

        private string[] dataArray;
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




        public void start()
        {
            new Thread(delegate ()
            {
                int length = dataArray.Length;
                for(int i=0; i< length; i++) { 
                    Time++;
                    Thread.Sleep((int)( 10 / speed));
                }

            }).Start();

        }

        public void stop()
        {
            //isStop = true;

        }
    }

}
