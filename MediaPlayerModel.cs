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

        private float[][] dataArray;
        private bool isStop;


        public MediaPlayerModel(float[][] array)
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
                while (!isStop)
                {
                    time++;
                    Thread.Sleep((int)(speed * 100));
                }

            }).Start();

        }

        public void stop()
        {
            isStop = true;

        }
    }

}
