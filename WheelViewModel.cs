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

        private double altimeter;
        public double VM_Altimeter
        {
            get { return model.Altimeter; }
        }

        private double airSpeed;
        public double VM_AirSpeed
        {
            get { return model.AirSpeed; }
        }

        private double flightDirection;
        public double VM_FlightDirection
        {
            get { return model.FlightDirection; }
        }

        private double pitch;
        public double VM_Pitch
        {
            get { return model.Pitch; }
        }

        private double roll;
        public double VM_Roll
        {
            get { return model.Roll; }
        }

        private double yaw;
        public double VM_Yaw
        {
            get { return model.Yaw; }
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
            if (string.Compare(propName, "VM_Altimeter") == 0)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("VM_Altimeter"));
                }
            }
            if (string.Compare(propName, "VM_AirSpeed") == 0)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("VM_AirSpeed"));
                }
            }
            if (string.Compare(propName, "VM_FlightDirection") == 0)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("VM_FlightDirection"));
                }
            }
            if (string.Compare(propName, "VM_Pitch") == 0)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("VM_Pitch"));
                }
            }
            if (string.Compare(propName, "VM_Roll") == 0)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("VM_Roll"));
                }
            }
            if (string.Compare(propName, "VM_Yaw") == 0)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("VM_Yaw"));
                }
            }
        }


    }
}
