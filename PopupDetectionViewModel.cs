using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP2_Flight_Inspection_App
{
    public class PopupDetectionViewModel
    {

        private PopupDetectionModel model;
        private string feature1;
        public string Feature1
        {
            get { return feature1; }
        }

        private string feature2;
        public string Feature2
        {
            get { return feature2; }
        }
        private int anomalyTime;
        public int AnomalyTime
        {
            get { return anomalyTime; }
        }

        public PopupDetectionViewModel(PopupDetectionModel m)
        {
            model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propName)
        {
            if (String.Compare(propName, "VM_Alarm") == 0)
            {
                string anomaly = model.NextAnomaly;
                anomalyTime = model.AnomalyTime;
                string[] splitstr = anomaly.Split('-');
                feature1 = model.getFeature(int.Parse(splitstr[0]));
                feature2 = model.getFeature(int.Parse(splitstr[1]));
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }
            }



        }
    }
}
