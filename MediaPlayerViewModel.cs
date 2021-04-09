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
                if(VM_play != value)
                {
                    VM_play = value;
                    if (value)
                    {
                        VM_Speed = lastSpeed;
                        VM_Pause = false;
                    }
                }

            }
        }
        private double lastSpeed;
        private bool VM_pause;
        public bool VM_Pause
        {
            set
            {
                if(VM_pause != value)
                {
                    VM_pause = value;
                    if (value)
                    {
                        lastSpeed = VM_Speed;
                        VM_Speed = 0;
                        VM_Play = false;
                    }
                }

            }
        }
        private bool VM_stop;
        public bool VM_Stop
        {
            set
            {
                VM_Time = numOfRows;
            }
        }

        public int VM_Time
        {
            get {
                return model.Time;
            }
            set {
                if(model.Time != value)
                {
                    model.Time = value;

                }
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
            // if the time in the model changed
            if (String.Compare(propName, "VM_TimePassed") == 0)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("VM_Time"));
                }
            }
        }

        public void startModel()
        {
            model.start();
        }



    }
}
