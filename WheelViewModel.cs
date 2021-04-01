using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP2_Flight_Inspection_App
{
    class WheelViewModel
    {

        private IADP2Model model;

        public int VM_Time
        {
            get
            {
                return model.Time;
            }
            set
            {
                if (model.Time != value)
                {
                    model.Time = value;

                }
            }
        }
        public double VM_Speed
        {
            get
            {
                return model.Speed;
            }
            set
            {
                model.Speed = value;
            }
        }

        public WheelViewModel(IADP2Model m)
        {
            model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };


        }

        public void NotifyPropertyChanged(string propName)
        {
            // notify view
        }
    }
}
