using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
        private bool WindowisOpen;
        public PopupDetection(PopupDetectionViewModel viewmodel)
        {
            
            vm = viewmodel;
            InitializeComponent();
            WindowisOpen = true;
            //DataContext = vm;
            vm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }


        void window_closed(object sender, CancelEventArgs e)
        {
            WindowisOpen = false;
        }

        public void NotifyPropertyChanged(string propName)
        {
            if(WindowisOpen == false)
            {
                return;
            }
            if (String.Compare(propName, "VM_Alarm") == 0)
            {
                
                this.Dispatcher.Invoke(() =>
                {
                    //List<Detection> detections = new List<Detection>();
                    //Detection d = new Detection() { Feature1 = vm.Feature1.ToString(), Feature2 = vm.Feature2.ToString(), Time = vm.AnomalyTime };
                    //Console.WriteLine(vm.Feature1 + "," + vm.Feature2 + "," + vm.AnomalyTime);
                    /*
                    dlist.Add(new Detection()
                    {
                        Feature1 = vm.Feature1.ToString(),
                        Feature2 = vm.Feature2.ToString(),
                        Time = vm.AnomalyTime
                    });
                    */
                    int time = vm.AnomalyTime;
                    string minutes = ((time / 10) / 60).ToString();
                    minutes = minutes.PadLeft(2, '0');
                    string seconds = ((time / 10) % 60).ToString();
                    seconds = seconds.PadLeft(2, '0');
                    //string milisec = ((time % 10) % 60).ToString();
                   // milisec = milisec.PadLeft(2, '0');
                    string deisplayTime = minutes + ":" + seconds;
                    Detection d =(new Detection()
                    {
                        Feature1 = vm.Feature1.ToString(),
                        Feature2 = vm.Feature2.ToString(),
                        Time = deisplayTime
                    });

                    if (detectionList.Items.Contains(d) == false)
                    {
                        if (detectionList.Items.Count == 10)
                        {
                            detectionList.Items.RemoveAt(0);
                        }

                        detectionList.Items.Add(d);
                    }

                    //detectionList.Items.Clear();



                    //detectionList.ItemsSource = detections;
                    //detectionList.Items.Add(new Detection { Feature1 = vm.Feature1.ToString(), Feature2 = vm.Feature2.ToString(), Time = vm.AnomalyTime });
                });

            }

            if (String.Compare(propName, "VM_Time") == 0)
            {
                this.Dispatcher.Invoke(() =>
                {
                    detectionList.Items.Clear();
                });
            }

            if (String.Compare(propName, "VM_Done") == 0)
            {
                this.Dispatcher.Invoke(() =>
                {
                    WindowisOpen = false;
                    this.Close();
                });
            }

        }

        private void switchDLL_clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (file.ShowDialog() == true)
            {
                if (File.Exists(file.FileName))
                {
                    vm.DLLPath = file.FileName;

                }
            }
        }
    }

    public class Detection : IComparable
    {
        public override bool Equals(Object o)
        {
            if ((o == null) || !this.GetType().Equals(o.GetType()))
            {
                return false;
            }
            return (this == (Detection)o);
        }

        public static bool operator ==(Detection d1, Detection d2)
        {


            if (String.Compare(d1.Feature1, d2.Feature1) == 0)
            {
                if (String.Compare(d1.Feature2, d2.Feature2) == 0)
                {
                    if (String.Compare(d1.Time, d2.Time) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool operator !=(Detection a, Detection b)
        {
            if(a == b)
            {
                return false;
            }
            return true;
        }


        int IComparable.CompareTo(object obj)
        {
            Detection d = (Detection)obj;
            if(String.Compare(this.Feature1,d.Feature1) == 0)
            {
                if(String.Compare(this.Feature2, d.Feature2) == 0){
                    if(String.Compare(this.Time, d.Time) == 0)
                    {
                        return 0;
                    }
                }
            }
            return 1;
        }
        public string Feature1 { get; set; }
        public string Feature2 { get; set; }
        public string Time { get; set; }
    }
}
