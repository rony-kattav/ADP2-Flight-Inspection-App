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
        private string feature2;
        private int anomalyTime;

        public PopupDetectionViewModel(PopupDetectionModel m)
        {
            model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (String.Compare(propName, "VM_Alarm") == 0)
            {
                string anomaly = model.NextAnomaly;
                anomalyTime = model.AnomalyTime;
                string[] splitstr = anomaly.Split('-');
                feature1 = splitstr[0];
                feature2 = splitstr[1];
                Console.WriteLine(anomaly);
            }



        }
    }
}
