using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP2_Flight_Inspection_App
{
    class MediaPlayerViewModel
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
                //return time;
            }
            set { 
                //time = value;
                model.Time = value;
            }
        }
        public double VM_Speed
        {
            get {
                return model.Speed;
                //return speed;
            }
            set { 
                //speed = value;
                model.Speed = value;
            }        
        }


        public MediaPlayerViewModel(IADP2Model m)
        {
            model = m;
            //time = 0;
            //speed = 1;

        }



    }
}
