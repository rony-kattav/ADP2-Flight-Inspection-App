using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP2_Flight_Inspection_App
{
    public class WheelViewModel
    {

        private WheelModel model;


        private double aileron;
        public double VM_Aileron
        {
            get { return model.Aileron; }

        }

        private double elevator;
        public double VM_Elevator
        {
            get { return model.Elevator; }

        }

        private double rudder;
        public double VM_Rudder
        {
            get { return model.Rudder; }

        }

        private double throttle;
        public double VM_Throttle
        {
            get { return model.Throttle; }

        }


        public WheelViewModel(WheelModel m)
        {
            model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };


        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if(string.Compare(propName, "VM_Throttle") == 0)
            {
                if(this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("VM_Throttle"));
                }
            }
            if (string.Compare(propName, "VM_Rudder") == 0)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("VM_Rudder"));
                }
            }
            if (string.Compare(propName, "VM_Elevator") == 0)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("VM_Elevator"));
                }
            }
            if (string.Compare(propName, "VM_Aileron") == 0)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("VM_Aileron"));
                }
            }

        }


    }
}
