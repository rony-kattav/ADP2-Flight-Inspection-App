using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP2_Flight_Inspection_App
{
    public class MediaPlayerViewModel
    {
        private IADP2Model model;
        private bool VM_play;
        public bool VM_Play
        {
            set
            {
                VM_play = value;
                if (value)
                {
                    VM_Speed = lastSpeed;
                }
            }
        }
        private double lastSpeed;
        private bool VM_pause;
        public bool VM_Pause
        {
            set
            {
                VM_pause = value;
                if (value)
                {
                    lastSpeed = VM_Speed;
                    VM_Speed = 0;
                }
            }
        }
        private bool VM_stop;

        public int VM_Time
        {
            get {
                return model.Time;
            }
            set {

                model.Time = value;

            }
        }
        public double VM_Speed
        {
            get {
                return model.Speed;
            }
            set {
                model.Speed = value;
            }        
        }

        public int numOfRows
        {
            get { return model.numOfRows; }
        }


        public MediaPlayerViewModel(IADP2Model m)
        {
            model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };


        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged(string propName)
        {
            if (String.Compare(propName, "VM_Time") == 0)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }
            }
        }

        public void startModel()
        {
            model.start();
        }



    }
}
