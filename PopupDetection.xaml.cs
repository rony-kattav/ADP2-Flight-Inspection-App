using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADP2_Flight_Inspection_App
{
    /// <summary>
    /// Interaction logic for PopupDetection.xaml
    /// </summary>
    public partial class PopupDetection : Window
    {
        private PopupDetectionViewModel vm;
        public PopupDetection(PopupDetectionViewModel viewmodel)
        {
            
            vm = viewmodel;
            InitializeComponent();
            //DataContext = vm;
            vm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }



        public void NotifyPropertyChanged(string propName)
        {
            if (String.Compare(propName, "VM_Alarm") == 0)
            {
                this.Dispatcher.Invoke(() =>
                {
                    //List<Detection> detections = new List<Detection>();
                    Detection d = new Detection() { Feature1 = vm.Feature1.ToString(), Feature2 = vm.Feature2.ToString(), Time = vm.AnomalyTime };
                    /*
                    detections.Add(new Detection()
                    {
                        Feature1 = vm.Feature1.ToString(),
                        Feature2 = vm.Feature2.ToString(),
                        Time = vm.AnomalyTime
                    });
                    */
                    detectionList.Items.Add(d);
                    //detectionList.ItemsSource = detections;
                    //detectionList.Items.Add(new Detection { Feature1 = vm.Feature1.ToString(), Feature2 = vm.Feature2.ToString(), Time = vm.AnomalyTime });
                });
                //get feature1, feature2 , anomalyTime from vm and display
                // show a window, wait ~5 sec and then close it.
            }

        }

    }

    public class Detection
    {
        public string Feature1 { get; set; }
        public string Feature2 { get; set; }
        public int Time { get; set; }
    }
}
